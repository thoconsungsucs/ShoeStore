using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
namespace ShoeStore.Ultility.VnPay
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _config;

        public VnPayService(IConfiguration config)
        {
            _config = config;
        }

        public string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model)
        {

            var vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", _config["VnPay:Version"]);
            vnpay.AddRequestData("vnp_Command", _config["VnPay:PayCommand"]);
            vnpay.AddRequestData("vnp_TmnCode", _config["VnPay:TmnCode"]);
            vnpay.AddRequestData("vnp_Amount", (model.Amount * 100 * 24000).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000

            vnpay.AddRequestData("vnp_CreateDate", model.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _config["VnPay:CurrCode"]);
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", _config["VnPay:Locale"]);

            vnpay.AddRequestData("vnp_OrderInfo", model.Description);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", $"{_config["VnPay:PaymentBackReturnUrl"]}?orderHeaderId={model.OrderId}");

            vnpay.AddRequestData("vnp_TxnRef", model.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            var paymentUrl = vnpay.CreateRequestUrl(_config["VnPay:BaseUrl"], _config["VnPay:HashSecret"]);

            return paymentUrl;
        }

        /*public async Task<string> SendRefundRequest(HttpContext context, VnPaymentRequestModel model)
        {
            var vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_RequestId", model.OrderId.ToString());
            vnpay.AddRequestData("vnp_Version", _config["VnPay:Version"]);
            vnpay.AddRequestData("vnp_Command", _config["VnPay:RefundCommand"]);
            vnpay.AddRequestData("vnp_TmnCode", _config["VnPay:TmnCode"]);
            vnpay.AddRequestData("vnp_TransactionType", "02"); // 02: Full refund; 03: Partial refund
            vnpay.AddRequestData("vnp_Amount", (model.Amount * 100).ToString()); // Amount in VND, multiplied by 100 (e.g., 100,000 VND -> 10000000)

            vnpay.AddRequestData("vnp_TransactionDate", model.TransactionDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CreateBy", model.Name);
            vnpay.AddRequestData("vnp_CreateDate", model.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _config["VnPay:CurrCode"]);
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));

            vnpay.AddRequestData("vnp_OrderInfo", model.Description);
            vnpay.AddRequestData("vnp_TxnRef", model.OrderId.ToString()); // Unique transaction reference ID

            vnpay.AddSecureHash(_config["VnPay:HashSecret"]);
            var parameters = vnpay.GetParameters();

            // Debugging or Logging
            foreach (var param in parameters)
            {
                Console.WriteLine($"{param.Key}: {param.Value}");
            }

            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(vnpay.GetParameters());
                var response = await client.PostAsync(_config["VnPay:RefundUrl"], content);
                return await response.Content.ReadAsStringAsync();
            }
        }*/
        public string CreateRefundUrl(HttpContext context, VnPaymentRequestModel model)
        {
            var vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_RequestId", model.OrderId.ToString());
            vnpay.AddRequestData("vnp_Version", _config["VnPay:Version"]);
            vnpay.AddRequestData("vnp_Command", _config["VnPay:RefundCommand"]);
            vnpay.AddRequestData("vnp_TmnCode", _config["VnPay:TmnCode"]);
            vnpay.AddRequestData("vnp_TransactionType", "02"); // 02: Full refund; 03: Partial refund
            vnpay.AddRequestData("vnp_Amount", (model.Amount * 100).ToString()); // Amount in VND, multiplied by 100 (e.g., 100,000 VND -> 10000000)

            vnpay.AddRequestData("vnp_TransactionDate", model.TransactionDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CreateBy", model.Name);
            vnpay.AddRequestData("vnp_CreateDate", model.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _config["VnPay:CurrCode"]);
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));

            vnpay.AddRequestData("vnp_OrderInfo", model.Description);
            vnpay.AddRequestData("vnp_TxnRef", model.OrderId.ToString()); // Unique transaction reference ID

            vnpay.AddSecureHash(_config["VnPay:HashSecret"]);
            var paymentUrl = vnpay.CreateRequestUrl(_config["VnPay:RefundUrl"], _config["VnPay:HashSecret"]);

            return paymentUrl;
        }

        public async Task<string> SendRefundRequest(HttpContext context, VnPaymentRequestModel model)
        {
            var vnp_Api = _config["VnPay:RefundUrl"];
            var vnp_HashSecret = _config["VnPay:HashSecret"]; //Secret KEy
            var vnp_TmnCode = _config["VnPay:TmnCode"]; // Terminal Id

            var vnp_RequestId = DateTime.Now.Ticks.ToString(); //Mã hệ thống merchant tự sinh ứng với mỗi yêu cầu hoàn tiền giao dịch. Mã này là duy nhất dùng để phân biệt các yêu cầu truy vấn giao dịch. Không được trùng lặp trong ngày.
            var vnp_Version = _config["VnPay:Version"]; //2.1.0
            var vnp_Command = "refund";
            var vnp_TransactionType = "02";
            var vnp_Amount = Convert.ToInt64(model.Amount * 24000) * 100;
            var vnp_TxnRef = model.OrderId.ToString(); // Mã giao dịch thanh toán tham chiếu
            var vnp_OrderInfo = "Renfund transaction: " + model.OrderId.ToString();
            var vnp_TransactionNo = ""; //Giả sử giá trị của vnp_TransactionNo không được ghi nhận tại hệ thống của merchant.
            var vnp_TransactionDate = model.TransactionDate.ToString("yyyyMMddHHmmss");
            var vnp_CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var vnp_CreateBy = "Thanh";
            var vnp_IpAddr = Utils.GetIpAddress(context);

            var signData = vnp_RequestId + "|" + vnp_Version + "|" + vnp_Command + "|" + vnp_TmnCode + "|" + vnp_TransactionType + "|" + vnp_TxnRef + "|" + vnp_Amount + "|" + vnp_TransactionNo + "|" + vnp_TransactionDate + "|" + vnp_CreateBy + "|" + vnp_CreateDate + "|" + vnp_IpAddr + "|" + vnp_OrderInfo;
            var vnp_SecureHash = Utils.HmacSHA512(vnp_HashSecret, signData);

            var rfData = new
            {
                vnp_RequestId = vnp_RequestId,
                vnp_Version = vnp_Version,
                vnp_Command = vnp_Command,
                vnp_TmnCode = vnp_TmnCode,
                vnp_TransactionType = vnp_TransactionType,
                vnp_TxnRef = vnp_TxnRef,
                vnp_Amount = vnp_Amount,
                vnp_OrderInfo = vnp_OrderInfo,
                vnp_TransactionNo = vnp_TransactionNo,
                vnp_TransactionDate = vnp_TransactionDate,
                vnp_CreateBy = vnp_CreateBy,
                vnp_CreateDate = vnp_CreateDate,
                vnp_IpAddr = vnp_IpAddr,
                vnp_SecureHash = vnp_SecureHash

            };


            using (var client = new HttpClient())
            {
                // Chuyển đổi đối tượng request thành JSON
                var jsonContent = JsonSerializer.Serialize(rfData);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Gửi yêu cầu POST
                var response = await client.PostAsync(_config["VnPay:RefundUrl"], httpContent);

                // Kiểm tra kết quả trả về
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
        }

        public VnPaymentResponseModel PaymentExecute(IQueryCollection collections)
        {
            var vnpay = new VnPayLibrary();
            foreach (var (key, value) in collections)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }

            var vnp_orderId = vnpay.GetResponseData("vnp_TxnRef");
            var vnp_TransactionId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            var vnp_SecureHash = collections.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");
            var vnp_PayDate = vnpay.GetResponseData("vnp_PayDate");
            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _config["VnPay:HashSecret"]);
            if (!checkSignature)
            {
                return new VnPaymentResponseModel
                {
                    Success = false
                };
            }

            return new VnPaymentResponseModel
            {
                Success = true,
                PaymentMethod = "VnPay",
                OrderDescription = vnp_OrderInfo,
                OrderId = vnp_orderId.ToString(),
                TransactionId = Convert.ToInt32(vnp_TransactionId),
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode,
                PaymentDate = DateTime.ParseExact(vnp_PayDate, "yyyyMMddHHmmss", null)
            };
        }
    }
}