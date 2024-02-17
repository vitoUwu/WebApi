using WebApi.Dtos;
using WebApi.Entities;

namespace WebApi.Repositories
{
    public interface IUserRepository
    {
        UserEntity? GetUserByName(string name);
        UserEntity CreateUser(UserCreateDto user);
        bool DeleteUser(string name);
        UserEntity? UpdateUser(string name, UserUpdateDto user);
        UserEntity[] GetAllUsers();
        bool UserExists(string name);
    }
}