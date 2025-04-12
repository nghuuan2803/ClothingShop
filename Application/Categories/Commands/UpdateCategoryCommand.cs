using Contracts.Repositories;
using Domain.Entities;
using MediatR;
using Shared.Requests;

namespace Application.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<bool>
    {
       public UpdateCategoryReq Request { get; set; }
    }
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IRepository<Category> _repository;

        public UpdateCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken = default)
        {
            var cate = await _repository.GetByIdAsync(command.Request.Id);
            if (cate == null)
                return false;
            cate.Name = command.Request.Name;
            cate.ParentId = command.Request.ParentId;
            _repository.Update(cate);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
