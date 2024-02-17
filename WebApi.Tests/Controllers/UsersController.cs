using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Tests.Repositories;

namespace WebApi.Tests.Controllers
{
    public class UsersControllerTest
    {
        [Fact]
        public void UserController_CreateUser_ReturnsNewUser()
        {
            // Arrange
            var userRepository = new UserRepositoryTest();
            var userController = new UsersController(userRepository);

            // Act
            var user = new UserCreateDto()
            {
                Name = "Test",
            };
            var result = userController.CreateUser(user);

            // Assert
            var createdAtRouteResult = Assert.IsAssignableFrom<CreatedAtRouteResult>(result.Result);
            var returnValue = Assert.IsType<UserEntity>(createdAtRouteResult.Value);
            Assert.Equal(user.Name, returnValue.Name);
        }

        [Fact]
        public void UserController_CreateUser_ReturnsConflict()
        {
            // Arrange
            var userRepository = new UserRepositoryTest();
            var userController = new UsersController(userRepository);

            // Act
            var user = new UserCreateDto()
            {
                Name = "Test",
            };
            userController.CreateUser(user);
            var result = userController.CreateUser(user);

            // Assert
            Assert.IsAssignableFrom<ConflictResult>(result.Result);
        }

        [Fact]
        public void UserController_GetUserByName_ReturnsUser()
        {
            // Arrange
            var userRepository = new UserRepositoryTest();
            var userController = new UsersController(userRepository);
            var user = new UserCreateDto()
            {
                Name = "Test"
            };

            // Act
            userController.CreateUser(user);
            var result = userController.GetUserByName("Test");

            // Assert
            var okActionResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<UserEntity>(okActionResult.Value);
            Assert.Equal(user.Name, returnValue.Name);
        }

        [Fact]
        public void UserController_GetUserByName_ReturnsNotFound()
        {
            // Arrange
            var userRepository = new UserRepositoryTest();
            var userController = new UsersController(userRepository);

            // Act
            var result = userController.GetUserByName("UserThatShouldBeNotFound");

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Fact]
        public void UserController_DeleteUser_ReturnsOk()
        {
            // Arrange
            var userRepository = new UserRepositoryTest();
            var userController = new UsersController(userRepository);
            var user = new UserCreateDto()
            {
                Name = "Test"
            };

            // Act
            userController.CreateUser(user);
            var result = userController.DeleteUser("Test");

            // Assert
            Assert.IsAssignableFrom<OkResult>(result);
        }

        [Fact]
        public void UserController_DeleteUser_ReturnsNotFound()
        {
            // Arrange
            var userRepository = new UserRepositoryTest();
            var userController = new UsersController(userRepository);

            // Act
            var result = userController.DeleteUser("UserThatShouldBeNotFound");

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public void UserController_UpdateUser_ReturnsUpdatedUser()
        {
            // Arrange
            var userRepository = new UserRepositoryTest();
            var userController = new UsersController(userRepository);
            var user = new UserCreateDto()
            {
                Name = "Test"
            };
            var updatedUser = new UserUpdateDto()
            {
                Name = "Updated"
            };

            // Act
            userController.CreateUser(user);
            var result = userController.UpdateUser(user.Name, updatedUser);

            // Assert
            var okActionResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<UserEntity>(okActionResult.Value);
            Assert.Equal(updatedUser.Name, returnValue.Name);
        }

        [Fact]
        public void UserController_UpdateUser_ReturnsNotFound()
        {
            // Arrange
            var userRepository = new UserRepositoryTest();
            var userController = new UsersController(userRepository);
            var updatedUser = new UserUpdateDto()
            {
                Name = "Updated"
            };

            // Act
            var result = userController.UpdateUser("UserThatShouldBeNotFound", updatedUser);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Fact]
        public void UserController_GetAll_ReturnsUsers()
        {
            // Arrange
            var userRepository = new UserRepositoryTest();
            var userController = new UsersController(userRepository);
            UserCreateDto[] users = [
                new() { Name = "1" },
                new() { Name = "2" },
                new() { Name = "3" }
            ];

            // Act
            foreach (var user in users)
            {
                userController.CreateUser(user);
            }

            var result = userController.GetAllUsers();

            // Assert
            var okActionResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<UserEntity[]>(okActionResult.Value);
            Assert.Equal(users.Length, returnValue.Length);
        }

        [Fact]
        public void UserController_GetAll_ReturnsEmptyList()
        {
            // Arrange
            var userRepository = new UserRepositoryTest();
            var userController = new UsersController(userRepository);

            // Act
            var result = userController.GetAllUsers();

            // Assert
            var okActionResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<UserEntity[]>(okActionResult.Value);
            Assert.Empty(returnValue);
        }
    }
}
