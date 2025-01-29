using ActiveDirectoryExplorer.Models.Abstract;
using Novell.Directory.Ldap;

namespace ActiveDirectoryExplorer.Models
{
    public class Group : ActiveDirectoryObject
    {
        public string Description { get; set; }

        public Group (LdapEntry ldapData)
        {
            Name = ldapData.GetAttribute("name").StringValue;
            DistinguishedName = ldapData.GetAttribute("distinguishedName").StringValue;
            WhenCreated = ldapData.GetAttribute("whenCreated").StringValue;
            WhenChanged = ldapData.GetAttribute("whenChanged").StringValue;
        }
    }
}
