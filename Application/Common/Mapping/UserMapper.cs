using Domain.Entities;
using Shared.Users;

namespace Application.Common.Mapping
{
    public static class UserMapper
    {
        public static GuestInfoRes ToGuestInfo(this Customer customer)
        {
            var guest = new GuestInfoRes
            {
                Id = customer.Id,
                FullName = customer.Name,
                IsRegistered = customer.IsRegistered
            };
            return guest;
        }
    }
}
