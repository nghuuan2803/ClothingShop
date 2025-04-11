using Contracts.Repositories;
using Domain.Entities;
using Shared.Requests;

namespace Application.Categories.Commands
{
    public class AddCategoryCommand : ICommand<AddCategoryReq, int>
    {
        private readonly IRepository<Category> _repo;

        public AddCategoryCommand(IRepository<Category> repo)
        {
            _repo = repo;
        }

        public async Task<int> Execute(AddCategoryReq request)
        {
            var category = new Category
            {
                Name = request.Name,
                ParentId = request.ParentId
            };
            await _repo.AddAsync(category);
            await _repo.SaveChangesAsync();
            return category.Id;
        }
    }
}
