using FluentNHibernate.Mapping;

namespace SimpleBoardWebApi.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string Token { get; set; }
    }

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id).Not.Nullable();
            Map(x => x.FirstName).Length(32).Not.Nullable();
            Map(x => x.LastName).Length(32).Not.Nullable();
            Map(x => x.Username).Length(32).Unique().Index("idx__username").Not.Nullable();
            Map(x => x.Password).Length(512).Not.Nullable();
        }
    }
}
