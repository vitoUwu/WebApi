using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Repositories;

namespace WebApi.Tests.Repositories
{
    public class UserRepositoryTest : IUserRepository
    {
        // The only difference between the original user repository and this one is that the list is not static, it is recreated with each instance
        private readonly List<UserEntity> _users = [];

        public UserEntity? GetUserByName(string name)
        {
            return _users.FirstOrDefault(user => user.Name == name);
        }

        public UserEntity CreateUser(UserCreateDto user)
        {
            UserEntity newUser = new()
            {
                Name = user.Name,
                CreatedAt = DateTime.Now,
            };

            _users.Add(newUser);

            return newUser;
        }

        public bool DeleteUser(string name)
        {
            UserEntity? user = GetUserByName(name);

            if (user != null)
            {
                _users.Remove(user);
                return true;
            }

            return false;
        }

        public UserEntity? UpdateUser(string name, UserUpdateDto user)
        {
            UserEntity? userToUpdate = GetUserByName(name);

            if (userToUpdate != null)
            {
                userToUpdate.Name = user.Name;
            }

            return userToUpdate;
        }

        public UserEntity[] GetAllUsers()
        {
            // same as _users.ToArray()
            return [.. _users];
        }

        public bool UserExists(string name)
        {
            return _users.Any(user => user.Name == name);
        }
    }
}