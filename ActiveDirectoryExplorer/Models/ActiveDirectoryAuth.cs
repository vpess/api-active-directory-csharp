namespace ActiveDirectoryExplorer.Models
{
    public class ActiveDirectoryAuth
    {
        public string Host { get; set; }
        public string DomainController { get; set; }
        public string UserDN { get; set; }
        public string UserPswd { get; set; }
    }
}
