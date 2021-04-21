using LongTalkDemo.Tables;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

/*
    Do not take this code as an example of good architecture.
*/
namespace LongTalkDemo.Other
{
    public class FlaskAppBuilder
    {
        private string _name = "app";
        public FlaskAppBuilder WithName(string name)
        {
            this._name = name;
            return this;
        }

        private string _environment = "development";
        public FlaskAppBuilder WithEnvironment(string environment)
        {
            this._environment = environment;
            return this;
        }

        public (Flask flaskApp, Injector injector) Build()
        {
            var app = new Flask(this._name);
            app.Config.FromConfig(new DevelopmentConfig());
            var injector = new Injector(new AppModule(app));
            var flaskApp = injector.Get<FlaskApp>();
            flaskApp.Create();

            return (app, injector);
        }
    }

    public class FlaskApp
    {
        public Flask flaskApp;
        public AppDBContext context;

        public FlaskApp(Flask flaskApp, AppDBContext context)
        {
            this.flaskApp = flaskApp;
            this.context = context;
        }

        public void Create()
        {
            
        }
    }

    public class Flask
    {
        public string name;
        public BaseConfig Config { get; private set; }
        public Flask(string name)
        {
            this.name = name;
            Config = new BaseConfig();
        }

        public void HealthCheck() { }
        public Base TestDb(string table)
        {
            return TestInstances.GetByName(table).First();
        }
    }

    public class BaseConfig
    {
        public virtual bool DEBUG { get; private set; }
        public virtual bool TESTING { get; private set; }
        public virtual string DATABASE_CONNECTION_STRING { get; private set; } = null;

        public void FromConfig(BaseConfig config)
        {
            this.DEBUG = config.DEBUG;
            this.TESTING = config.TESTING;
            this.DATABASE_CONNECTION_STRING = config.DATABASE_CONNECTION_STRING;
        }
    }

    public class DevelopmentConfig: BaseConfig
    {
        public override bool DEBUG => true;
        public override bool TESTING => false;
        public override string DATABASE_CONNECTION_STRING => "sqlite:///.app.db";
    }

    public class AppDBContext
    {

    }

    public class Injector
    {
        private AppModule _module;
        public Injector(AppModule module)
        {
            this._module = module;
        }

        public T Get<T>()
            where T: FlaskApp
        {
            return (T)new FlaskApp(this._module._app, new AppDBContext());
        }
    }

    public class AppModule
    {
        public Flask _app;
        public AppModule (Flask app)
        {
            this._app = app;
        }
    }
}

public class Database
{
    public ScopedSession GetSession() => new ScopedSession();
}

public class ScopedSession
{
    public IEnumerable<T> Query<T>()
        where T: Base
    {
        return TestInstances.GetByType<T>().OfType<T>();
    }
}
