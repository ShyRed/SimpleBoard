using Autofac;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using SimpleBoardWebApi.Services;
using System.IO;

namespace SimpleBoardWebApi
{
    public class AutofacModule : Module
    {
        private static readonly string DatabaseFile = Path.Combine(Path.GetTempPath(), "SimpleBoardWebApi.db");

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<HashService>().As<IHashService>();
            builder.RegisterType<BoardEntryService>().As<IBoardEntryService>();

            builder.Register(c => BuildSessionFactory())
                .As<ISessionFactory>()
                .SingleInstance();
            builder.Register<ISession>(c => c.Resolve<ISessionFactory>().OpenSession());
        }

        private static ISessionFactory BuildSessionFactory()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile(DatabaseFile))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserService>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            if (File.Exists(DatabaseFile))
                return;

            new SchemaExport(config).Create(false, true);
        }
    }
}
