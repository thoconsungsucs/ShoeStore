using Microsoft.AspNetCore.Http;

namespace ShoeStore.Ultility.VnPay
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
        string CreateRefundUrl(HttpContext context, VnPaymentRequestModel model);
        Task<string> SendRefundRequest(HttpContext context, VnPaymentRequestModel model);
        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}