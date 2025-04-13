using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Users.Commands
{
    //Cập nhật thông tin khách vãng lai khi đặt hàng
    public class UpdateGuestCommand : IRequest<string>
    {
        public string GuestId { get; set; }
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
    }
    public class UpdateGuestCommandHandler : IRequestHandler<UpdateGuestCommand, string>
    {
        private IRepository<Customer> _customerRepo;
        public async Task<string> Handle(UpdateGuestCommand request, CancellationToken cancellationToken)
        {
            var guest = await _customerRepo.GetByIdAsync(request.GuestId);
            if (guest is null)
            {
                guest = new Customer
                {
                    Id = request.GuestId,
                    Name = request.FullName,
                    PhoneNumber = request.PhoneNumber
                };
                await _customerRepo.AddAsync(guest);
            }
            else if (guest.UserId is not null)
                throw new ApplicationException("Guest already has an Account!");
            else
            {
                guest.Name = request.FullName;
                guest.PhoneNumber = request.PhoneNumber;
                _customerRepo.Update(guest);
            }
            await _customerRepo.SaveChangesAsync();
            return "Success";
        }
    }
}
