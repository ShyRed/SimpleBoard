using FluentNHibernate.Mapping;
using System;

namespace SimpleBoardWebApi.Models
{
    public class BoardEntry
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual int CreatedBy { get; set; }
        public virtual string Content { get; set; }
    }

    public class BoardEntryMap : ClassMap<BoardEntry>
    {
        public BoardEntryMap()
        {
            Id(x => x.Id);
            Map(x => x.CreatedAt).Not.Nullable();
            Map(x => x.CreatedBy).Not.Nullable();
            Map(x => x.Content).Length(1024).Not.Nullable();
        }
    }
}
