namespace ActiveDirectoryExplorer.Models.Interfaces
{
    public interface IActiveDirectoryObject
    {
        public string Name { get; set; }

        public string DistinguishedName { get; set; }

        public string WhenCreated { get; set; }

        public string WhenChanged { get; set; }
    }
}
