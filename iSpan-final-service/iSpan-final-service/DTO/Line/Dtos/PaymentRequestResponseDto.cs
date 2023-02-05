namespace iSpan_final_service.DTO.Line.Dtos
{
    public class PaymentRequestResponseDto
    {
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public ConfirmResponseInfoDto Info { get; set; }
    }

    public class ConfirmResponseInfoDto
    {
        public string OrderId { get; set; }
        public long TransactionId { get; set; }
        public string AuthorizationExpireDate { get; set; }
        public string RegKey { get; set; }
        public ConfirmResponsePayInfoDto[] PayInfo { get; set; }
    }

    public class ConfirmResponsePayInfoDto
    {
        public string Method { get; set; }
        public int Amount { get; set; }
        public string CreditCardNickname { get; set; }
        public string CreditCardBrand { get; set; }
        public string MaskedCreditCardNumber { get; set; }
        public ConfirmResponsePackageDto[] Packages { get; set; }
        public ConfirmResponseShippingOptionsDto Shipping { get; set; }
    }
    public class ConfirmResponsePackageDto
    {
        public string Id { get; set; }
        public int Amount { get; set; }
        public int UserFeeAmount { get; set; }
    }
    public class ConfirmResponseShippingOptionsDto
    {
        public string MethodId { get; set; }
        public int FeeAmount { get; set; }
        public ShippingAddressDto Address { get; set; }
    }
    public class PaymentResponseDto
    {
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public ResponseInfoDto Info { get; set; }
    }

    public class ResponseInfoDto
    {
        public ResponsePaymentUrlDto PaymentUrl { get; set; }
        public long TransactionId { get; set; }
        public string PaymentAccessToken { get; set; }
    }

    public class ResponsePaymentUrlDto
    {
        public string Web { get; set; }
        public string App { get; set; }
    }
}

