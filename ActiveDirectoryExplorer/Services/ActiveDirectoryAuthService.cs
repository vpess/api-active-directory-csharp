using ActiveDirectoryExplorer.Models;

namespace ActiveDirectoryExplorer.Services
{
    public class ActiveDirectoryAuthService
    {
        private ConfigurationRoot _configuration = (ConfigurationRoot)new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        public ActiveDirectoryAuth SignIn(string domain)
        {
            domain = domain.ToLower();

            ActiveDirectoryAuth authUser = new ActiveDirectoryAuth
            {
                UserDN = _configuration[$"Domains:{domain}:userDN"],
                UserPswd = _configuration[$"Domains:{domain}:password"],
                Host = _configuration[$"Domains:{domain}:host"],
                DomainController = _configuration[$"Domains:{domain}:controller"],
            };

            return authUser;
        }

    }
}
