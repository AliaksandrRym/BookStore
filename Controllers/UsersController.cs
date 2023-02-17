namespace BookStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using BookStore.Interfaces;
    using AutoMapper;
    using BookStore.DTO;
    using BookStore.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET: api/Users 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {               
            var result = _mapper.Map<List<SecureUserDto>>(_userService.Get());

            if (!ModelState.IsValid)
                return BadRequest();

                return StatusCode(200, result);            
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(User))]
        public IActionResult GetUser(int id)
        {
            if (!_userService.Exists(id))
                return NotFound();

                var user = _mapper.Map<SecureUserDto>(_userService.Get(id));

            if (!ModelState.IsValid)
                return BadRequest();

            return StatusCode(200, user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult PutUser(int id, UserDto updatedUser)
        {
            if (updatedUser == null)
                return BadRequest(ModelState);

            if (id != updatedUser.Id)
                return BadRequest(ModelState);

            if (!_userService.Exists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(updatedUser);

            if (!_userService.Put(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating user");
                return StatusCode(500, ModelState);
            }
            return StatusCode(204, "User was updated");
        }

        // POST: api/Users
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult PostUser(UserDto createUser)
        {
            if(createUser == null)
                return BadRequest(ModelState);

            var user = _mapper.Map<List<UserDto>>(_userService.Get()).Where(u => u.Email == createUser.Email).FirstOrDefault();

            if(user != null)
            {
                ModelState.AddModelError("", "User with same Email exists");
                return StatusCode(422, ModelState);
            }

            var userMap = _mapper.Map<User>(createUser);
            if(!_userService.Post(userMap))
            {
                ModelState.AddModelError("", "Something went wrong, user was not saved");
                return StatusCode(500, ModelState);
            }
            return StatusCode(201, "User was created");
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int id)
        {
            if(!_userService.Exists(id))
                return NotFound();

            var userToDelete = _userService.Get(id);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);  

            if(!_userService.Delete(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting user");
            }

            return StatusCode(204, "User was deleted");
        }
    }
}
