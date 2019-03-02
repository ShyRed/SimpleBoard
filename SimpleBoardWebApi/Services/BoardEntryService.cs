using NHibernate;
using NHibernate.Linq;
using SimpleBoardWebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBoardWebApi.Services
{
    public interface IBoardEntryService
    {
        Task<BoardEntry> Save(ISession session, BoardEntry entry);
        Task<int> Count(ISession session);
        Task<BoardEntry> Get(ISession session, int id);
        Task<IEnumerable<BoardEntry>> Get(ISession session, int page, int pageSize);
    }

    public class BoardEntryService : IBoardEntryService
    {
        public async Task<BoardEntry> Save(ISession session, BoardEntry entry)
        {
            var id = (int)await session.SaveAsync(entry);
            return await session.GetAsync<BoardEntry>(id);
        }

        public async Task<int> Count(ISession session)
        {
            return await session.QueryOver<BoardEntry>().RowCountAsync();
        }

        public async Task<BoardEntry> Get(ISession session, int id)
        {
            return await session.GetAsync<BoardEntry>(id);
        }

        public async Task<IEnumerable<BoardEntry>> Get(ISession session, int page, int pageSize)
        {
            return await session.Query<BoardEntry>()
                .OrderByDescending(x => x.CreatedAt)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
