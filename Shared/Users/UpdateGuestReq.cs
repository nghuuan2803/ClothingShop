namespace Shared.Users
{
    public class UpdateGuestReq
    {
        public string GuestId { get; set; }
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
