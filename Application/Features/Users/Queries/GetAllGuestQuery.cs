using Application.Common.Mapping;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Shared.Users;

namespace Application.Features.Users.Queries
{
    public class GetAllGuestQuery : IRequest<IEnumerable<GuestInfoRes>>
    {
    }
    public class GetAllGuestQueryHandler : IRequestHandler<GetAllGuestQuery, IEnumerable<GuestInfoRes>>
    {
        private IRepository<Customer> _repository;

        public GetAllGuestQueryHandler(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GuestInfoRes>> Handle(GetAllGuestQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetAllAsync(p=>!string.IsNullOrEmpty(p.Name));
            return data.Select(p => p.ToGuestInfo());
        }
    }
}
