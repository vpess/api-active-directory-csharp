namespace ActiveDirectoryExplorer.Services
{
    public class ValidationService
    {
        private ConfigurationRoot _configuration = (ConfigurationRoot)new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        public bool IsValidDomain(string domain) //TODO Instanciar nos Controllers 
        {
            //Verificar se o domínio digitado está presente em 'Domains'
            try
            {
                var domains = _configuration[$"Domains:{domain}:host"];
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
