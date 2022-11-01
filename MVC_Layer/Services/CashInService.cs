using X.Paymob.CashIn;
using X.Paymob.CashIn.Models.Orders;
using X.Paymob.CashIn.Models.Payment;

namespace MVC_Layer.Services
{
    public class CashInService
    {
        private readonly IPaymobCashInBroker _broker;

        public CashInService(IPaymobCashInBroker broker)
        {
            _broker = broker;
        }

        public async Task<string> RequestCardPaymentKey()
        {
            // Create order.
            var amountCents = 1000; // 10 LE
            var orderRequest = CashInCreateOrderRequest.CreateOrder(amountCents);
            var orderResponse = await _broker.CreateOrderAsync(orderRequest);

            // Request card payment key.
            var billingData = new CashInBillingData(
                firstName: "Mahmoud",
                lastName: "Shaheen",
                phoneNumber: "010000000",
                email: "mxshaheen@gmail.com");

            var paymentKeyRequest = new CashInPaymentKeyRequest(
                integrationId: 1650381,
                orderId: orderResponse.Id,
                billingData: billingData,
                amountCents: amountCents);

            var paymentKeyResponse = await _broker.RequestPaymentKeyAsync(paymentKeyRequest);

            // Create iframe src.
            return _broker.CreateIframeSrc(iframeId: "323300", token: paymentKeyResponse.PaymentKey);
        }
    }
}
