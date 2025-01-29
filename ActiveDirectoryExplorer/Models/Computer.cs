using ActiveDirectoryExplorer.Extensions;
using ActiveDirectoryExplorer.Models.Abstract;
using ActiveDirectoryExplorer.Repositories;
using Novell.Directory.Ldap;

namespace ActiveDirectoryExplorer.Models
{
    public class Computer : ActiveDirectoryObject
    {
        private string[] _memberOf;
        public string[] MemberOf
        {
            get => _memberOf;

            set
            {
                _memberOf = value.BuildGroupArray();
            }
        }

        public Computer(LdapEntry ldapData)
        {
            Name = ldapData.GetAttribute("name").StringValue;
            DistinguishedName = ldapData.GetAttribute("distinguishedName").StringValue;
            WhenCreated = ldapData.GetAttribute("whenCreated").StringValue;
            WhenChanged = ldapData.GetAttribute("whenChanged").StringValue;
            MemberOf = LdapRepository.ValidateLdapArray(ldapData, "memberOf") ?? [];
        }
    }
}
