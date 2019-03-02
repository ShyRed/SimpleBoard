using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using SimpleBoardWebApi.Models;
using SimpleBoardWebApi.Services;

namespace SimpleBoardWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly Func<Owned<ISession>> _sessionFactory;

        public LoginController(IUserService userService, Func<Owned<ISession>> sessionFactory)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _sessionFactory = sessionFactory ?? throw new ArgumentNullException(nameof(sessionFactory));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<User>> Get()
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.Name).Value);
            using (var session = _sessionFactory())
            {
                var user = await _userService.GetById(session.Value, id);
                if (user == null)
                    return NotFound();
                user.Password = null;
                return Ok(user);
            }
        }

        // POST: api/Login
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User user)
        {
            using (var session = _sessionFactory())
            {
                var userWithToken = await _userService.Authenticate(session.Value, user.Username, user.Password);
                if (userWithToken == null)
                    return Unauthorized();
                return Ok(userWithToken);
            }
        }
    }
}
