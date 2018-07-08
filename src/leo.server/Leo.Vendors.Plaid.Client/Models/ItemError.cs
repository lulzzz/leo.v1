using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    public enum ItemErrorCodes
    {
        INVALID_CREDENTIALS,
        INVALID_MFA,
        ITEM_LOCKED,
        ITEM_LOGIN_REQUIRED,
        ITEM_NO_ERROR,
        ITEM_NOT_SUPPORTED,
        USER_SETUP_REQUIRED,
        MFA_NOT_SUPPORTED,
        NO_ACCOUNTS,
        NO_AUTH_ACCOUNTS,
        PRODUCT_NOT_READY,
        PRODUCT_NOT_SUPPORTED
    }

    [DataContract]
    public class ItemError
    {
        [DataMember(Name = "display_message")]
        public string DisplayMessage { get; set; }

        [DataMember(Name = "error_code")]
        public ItemErrorCodes ErrorCode { get; set; }

        [DataMember(Name = "error_message")]
        public string ErrorMessage { get; set; }

        [DataMember(Name = "error_type")]
        public string ErrorType { get; set; }

        [DataMember(Name = "http_code")]
        public int HttpCode { get; set; }

        [DataMember(Name = "status")]
        public int StatusCode { get; set; }

        [DataMember(Name = "request_id")]
        public string RequestId { get; set; }
    }
}