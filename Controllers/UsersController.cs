namespace BookStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using BookStore.Properties.Models;
    using BookStore.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;


        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var result = _userService.Get();
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return StatusCode(200, user); 
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            try
            {
                var result = _userService.Put( user);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [ProducesResponseType(201)]
        [HttpPost]
        public IActionResult PostUser(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _userService.Post(user);
                    return StatusCode(201, result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var result = _userService.Delete(id);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {

                return NotFound(ex);
            }
        }
    }
}
