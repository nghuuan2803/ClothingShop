using Contracts.Repositories;
using Domain.Entities;
using MediatR;
using Shared.Requests;

namespace Application.Categories.Commands
{
    public class AddCategoryCommand : IRequest<int>
    {
        public AddCategoryCommand(AddCategoryReq request)
        {
            Request = request;
        }

        public AddCategoryReq Request { get; set; }
    }
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, int>
    {
        private readonly IRepository<Category> _repo;

        public AddCategoryCommandHandler(IRepository<Category> repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(AddCategoryCommand command, CancellationToken cancellationToken = default)
        {
            var category = new Category
            {
                Name = command.Request.Name,
                ParentId = command.Request.ParentId
            };
            await _repo.AddAsync(category);
            await _repo.SaveChangesAsync();
            return category.Id;
        }
    }
}
