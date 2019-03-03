using System;
using System.Collections.Generic;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly Func<Owned<ISession>> _sessionFactory;

        public UsersController(IUserService userService, Func<Owned<ISession>> sessionFactory)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _sessionFactory = sessionFactory ?? throw new ArgumentNullException(nameof(sessionFactory));
        }

        // GET api/users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            using (var session = _sessionFactory())
            {
                return Ok(await _userService.GetAll(session.Value));
            }
        }

        // GET api/users/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            using (var session = _sessionFactory())
            {
                var user = await _userService.GetById(session.Value, id);
                if (user == null)
                    return NotFound();
                return Ok(user);
            }
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User user)
        {
            using (var session = _sessionFactory())
            using (var trans = session.Value.BeginTransaction())
            {
                trans.Begin();
                try
                {
                    await _userService.Register(session.Value, user);
                    await trans.CommitAsync();
                    return Ok();
                }
                catch
                {
                    await trans.RollbackAsync();
                    return BadRequest();
                }
            }
        }

        // PUT api/users/5
        [Authorize]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/users/5
        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
