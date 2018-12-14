using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.AspNet.Identity.Owin;

[assembly: OwinStartup(typeof(PutMeOnTheList.Startup))]

namespace PutMeOnTheList
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDb;Initial Catalog=PartyDBIdentity;Integrated Security=True;Pooling=False";

            app.CreatePerOwinContext(() => new IdentityDbContext(connectionString));


            app.CreatePerOwinContext<UserStore<IdentityUser>>((opt, cont) => new UserStore<IdentityUser>(cont.Get<IdentityDbContext>()));            app.CreatePerOwinContext<UserManager<IdentityUser>>((opt, cont) => new UserManager<IdentityUser>(cont.Get<UserStore<IdentityUser>>()));


            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/RSVP/Login")
            });
        }
    }
}
