//using Application.Features.Users.Commands;
//using Application.Services.Auth;
//using Domain.Entities;
//using Domain.Repositories;
//using Infrastructure.Persistence;
//using MediatR;
//using Moq;
//using Shared.Users;

//namespace Test
//{
//    public class CreateUserCommandHandlerTests
//    {
//        private readonly Mock<IRepository<Customer>> _customerRepoMock;
//        private readonly Mock<IUserRepository> _userRepoMock;
//        private readonly Mock<IAuthService> _authServiceMock;
//        private readonly Mock<ISender> _senderMock;
//        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
//        private readonly CreateUserCommandHandler _handler;

//        public CreateUserCommandHandlerTests()
//        {
//            _customerRepoMock = new Mock<IRepository<Customer>>();
//            _userRepoMock = new Mock<IUserRepository>();
//            _authServiceMock = new Mock<IAuthService>();
//            _senderMock = new Mock<ISender>();
//            _unitOfWorkMock = new Mock<IUnitOfWork>();
//            _handler = new CreateUserCommandHandler(
//                _customerRepoMock.Object,
//                _userRepoMock.Object,
//                _authServiceMock.Object,
//                _senderMock.Object,
//                _unitOfWorkMock.Object);
//        }

//        [Fact]
//        public async Task Handle_Should_CreateUser_When_GuestDoesNotExist()
//        {
//            var command = new CreateUserCommand
//            {
//                Request = new AddUserReq
//                {
//                    GuestId = "guest1",
//                    FullName = "Test User",
//                    Email = "test@example.com",
//                    Password = "Password123"
//                }
//            };
//            var user = new AppUser { Id = "user1", Email = "test@example.com" };
//            _customerRepoMock.Setup(r => r.GetByIdAsync("guest1")).ReturnsAsync((Customer)null);
//            _senderMock.Setup(s => s.Send(It.IsAny<AddGuestCommand>(), default))
//                       .ReturnsAsync("Guest added");
//            _customerRepoMock.Setup(r => r.GetByIdAsync("guest1")).ReturnsAsync(new Customer { Id = "guest1" });
//            _userRepoMock.Setup(r => r.AddUserAsync(It.IsAny<AddUserReq>())).ReturnsAsync(user);
//            _authServiceMock.Setup(s => s.GenerateAccessTokenAsync(user)).ReturnsAsync("access_token");
//            _authServiceMock.Setup(s => s.GenerateRefreshToken()).Returns("refresh_token");

//            var result = await _handler.Handle(command, default);

//            Assert.True(result.Success);
//            Assert.NotNull(result.AccessToken);
//            Assert.NotNull(result.RefreshToken);
//        }
//    }
//}
