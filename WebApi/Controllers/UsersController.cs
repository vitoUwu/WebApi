using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController(IUserRepository userRepository) : Controller
    {
        private readonly IUserRepository _userRepository = userRepository;

        [HttpGet]
        [Route("{name}", Name = nameof(GetUserByName))]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public ActionResult<UserEntity> GetUserByName(string name)
        {
            UserEntity? user = _userRepository.GetUserByName(name);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost(Name = nameof(CreateUser))]
        public ActionResult<UserEntity> CreateUser([FromBody] UserCreateDto user)
        {
            bool userExists = _userRepository.UserExists(user.Name);

            if (userExists)
            {
                return Conflict();
            }

            var newUser = _userRepository.CreateUser(user);

            return CreatedAtRoute(user, newUser);
        }

        [HttpDelete]
        [Route("{name}", Name = nameof(DeleteUser))]
        public ActionResult DeleteUser(string name)
        {
            bool userDeleted = _userRepository.DeleteUser(name);

            if (!userDeleted)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPut]
        [Route("{name}", Name = nameof(UpdateUser))]
        public ActionResult<UserEntity> UpdateUser(string name, [FromBody] UserUpdateDto user)
        {
            UserEntity? updatedUser = _userRepository.UpdateUser(name, user);

            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        [HttpGet(Name = nameof(GetAllUsers))]
        public ActionResult<UserEntity[]> GetAllUsers()
        {
            UserEntity[] users = _userRepository.GetAllUsers();

            return Ok(users);
        }
    }
}
