using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NHibernate;
using NHibernate.Linq;
using SimpleBoardWebApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBoardWebApi.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(ISession session, string username, string password);
        Task Register(ISession session, User user);
        Task<User> GetById(ISession session, int id);
        Task<IEnumerable<User>> GetAll(ISession session);
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IHashService _hashService;


        public UserService(IOptions<AppSettings> appSettings, IHashService hashService)
        {
            if (appSettings == null) throw new ArgumentNullException(nameof(appSettings));

            this._appSettings = appSettings.Value;
            this._hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
        }

        public async Task<User> Authenticate(ISession session, string username, string password)
        {
            var user = await session.Query<User>().Where(x => x.Username == username).FirstOrDefaultAsync();
            if (user == null)
                return null;

            if (!_hashService.IsPasswordHashedPassword(password, user.Password))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = null;
            return user;
        }

        public async Task<User> GetById(ISession session, int id)
        {
            var user = await session.GetAsync<User>(id);
            if (user != null)
                user.Password = null;
            return user;
        }

        public async Task<IEnumerable<User>> GetAll(ISession session)
        {
            var users = await session.Query<User>().ToListAsync();
            users.ForEach(x => x.Password = null);
            return users;
        }

        public async Task Register(ISession session, User user)
        {
            user.Password = _hashService.CreateHashFromPassword(user.Password);
            await session.SaveAsync(user);
        }
    }
}
