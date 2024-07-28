using System.Security.Cryptography;
using System.Text;

namespace ShoeStore.Ultility.VnPay
{
    public class RefundRequest
    {
        public string vnp_RequestId { get; set; }
        public string vnp_Version { get; set; } = "2.1.0";
        public string vnp_Command { get; set; } = "refund";
        public string vnp_TmnCode { get; set; }
        public string vnp_TransactionType { get; set; }
        public string vnp_TxnRef { get; set; }
        public string vnp_Amount { get; set; }
        public string vnp_OrderInfo { get; set; }
        public string vnp_TransactionNo { get; set; }
        public string vnp_TransactionDate { get; set; }
        public string vnp_CreateBy { get; set; }
        public string vnp_CreateDate { get; set; }
        public string vnp_IpAddr { get; set; }
        public string vnp_SecureHash { get; set; }
        public string CreateChecksum(string key, string inputData)
        {
            var hash = new StringBuilder();
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                var hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }
    }

    public class RefundResponse
    {
        public string Vnp_ResponseId { get; set; }
        public string Vnp_Command { get; set; }
        public string Vnp_ResponseCode { get; set; }
        public string Vnp_Message { get; set; }
        public string Vnp_TmnCode { get; set; }
        public string Vnp_TxnRef { get; set; }
        public string Vnp_Amount { get; set; }
        public string Vnp_OrderInfo { get; set; }
        public string Vnp_BankCode { get; set; }
        public string Vnp_PayDate { get; set; }
        public string Vnp_TransactionNo { get; set; }
        public string Vnp_TransactionType { get; set; }
        public string Vnp_TransactionStatus { get; set; }
        public string Vnp_SecureHash { get; set; }
    }


}
