namespace BookStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using BookStore.Models;
    using AutoMapper;
    using BookStore.Interfaces;
    using BookStore.DTO;

    public class UserController : Controller
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET: User
        public IActionResult Index(string userName)
        {
            if (_userService.Get() == null)
            {
                return Problem("Users list is empty.");
            }

            var users = from u in _userService.Users()
                           select u;

            if (!String.IsNullOrEmpty(userName))
            {
                users = users.Where(u => u.Name!.Contains(userName));
            }
            return View(_mapper.Map<List<SecureUserDto>>(users.ToList()));
        }

        // GET: User/Details/5
        public IActionResult Details(int id)
        {
            if (id == null || _userService.Get(id) == null)
            {
                return NotFound();
            }

            var product = _userService.Get(id);

            return View(product);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_userService.GetRoles(), "id", "id");
            return View();
        }

        // POST: User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Adress,Email,Login,Password,RoleId")] UserDto user)
        {
            if (ModelState.IsValid)
            {
                var userMap = _mapper.Map<User>(user);
                if (!_userService.Post(userMap))
                {
                    ModelState.AddModelError("", "Something went wrong, user was not saved");
                    return StatusCode(500, ModelState);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_userService.GetRoles(), "id", "id", user.RoleId);
            return View(user);
        }

        // GET: User/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null || _userService.Get(id) == null)
            {
                return NotFound();
            }

            var user = _userService.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_userService.GetRoles(), "id", "id", user.RoleId);
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Name,Adress,Email,Login,Password,RoleId")] UserDto userUpdate)
        {
            if (ModelState.IsValid)
            {
                if (userUpdate == null)
                    return BadRequest(ModelState);

                if (id != userUpdate.Id)
                    return BadRequest(ModelState);

                if (!_userService.Exists(id))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userMap = _mapper.Map<User>(userUpdate);

                if (!_userService.Put(userMap))
                {
                    ModelState.AddModelError("", "Something went wrong while updating user");
                    return StatusCode(500, ModelState);
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["RoleId"] = new SelectList(_userService.GetRoles(), "id", "id", userUpdate.RoleId);
            return View(userUpdate);
        }

        // GET: User/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user =_userService.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var userToDelete = _userService.Get(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userService.Delete(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting user");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
