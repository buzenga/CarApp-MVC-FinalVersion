using NHibernate;
using NHibernate.Cfg;

namespace CarApp.Models
{
    public class NHibernateSession
    {
        public static NHibernate.ISession OpenSession()
        {
            var configuration = new Configuration();
            configuration.Configure();
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();

            return sessionFactory.OpenSession();    
        }
    }
}
