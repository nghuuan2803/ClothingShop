namespace Shared.Auth
{
    public class PhoneNumberLoginReq
    {
        public required string GuestId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
