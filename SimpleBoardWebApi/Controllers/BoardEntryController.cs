using System;
using System.Collections.Generic;
using System.Linq;
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
    public class BoardEntryController : ControllerBase
    {
        private readonly IBoardEntryService _boardEntryService;
        private readonly Func<Owned<ISession>> _sessionFactory;

        public BoardEntryController(IBoardEntryService boardEntryService, Func<Owned<ISession>> sessionFactory)
        {
            _boardEntryService = boardEntryService ?? throw new ArgumentNullException(nameof(boardEntryService));
            _sessionFactory = sessionFactory ?? throw new ArgumentNullException(nameof(sessionFactory));
        }

        [Authorize]
        [HttpGet]
        public async Task<int> Get()
        {
            using (var session = _sessionFactory())
            {
                return await _boardEntryService.Count(session.Value);
            }
        }

        // GET: api/BoardEntry
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<BoardEntry>> Get(int page = 0, int pageSize = 10)
        {
            using (var session = _sessionFactory())
            {
                return await _boardEntryService.Get(session.Value, page, pageSize);
            }
        }

        // GET: api/BoardEntry/5
        [Authorize]
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<BoardEntry>> Get(int id)
        {
            using (var session = _sessionFactory())
            {
                var entry = await _boardEntryService.Get(session.Value, id);
                if (entry == null)
                    return NotFound();
                return Ok(entry);
            }
        }

        // POST: api/BoardEntry
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<BoardEntry>> Post([FromBody] BoardEntry value)
        {
            using (var session = _sessionFactory())
            using (var trans = session.Value.BeginTransaction())
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.Name).Value);
                value.CreatedBy = userId;
                value.CreatedAt = DateTime.Now;
                var entry = _boardEntryService.Save(session.Value, value);
                await trans.CommitAsync();
                return Ok(entry);
            }
        }

        // PUT: api/BoardEntry/5
        [Authorize]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
