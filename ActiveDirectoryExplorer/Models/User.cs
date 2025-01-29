using ActiveDirectoryExplorer.Extensions;
using ActiveDirectoryExplorer.Models.Abstract;
using ActiveDirectoryExplorer.Repositories;
using Novell.Directory.Ldap;
using System.Runtime.Serialization;

namespace ActiveDirectoryExplorer.Models
{
    public class User : ActiveDirectoryObject
    {
        private string[] _memberOf;
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Company { get; set; }

        public string Manager { get; set; }

        public string[] MemberOf
        {
            get => _memberOf;
            set
            {
                _memberOf = value.BuildGroupArray();
            }
        }

        public User(LdapEntry ldapData)
        {
            Name = ldapData.GetAttribute("name").StringValue;
            DisplayName = ldapData.GetAttribute("displayName").StringValue;
            Email = LdapRepository.ValidateLdapAttribute(ldapData, "mail") ?? "";
            DistinguishedName = ldapData.GetAttribute("distinguishedName").StringValue;
            Department = ldapData.GetAttribute("department").StringValue;
            Company = ldapData.GetAttribute("company").StringValue;
            Manager = ldapData.GetAttribute("manager").StringValue;
            WhenCreated = ldapData.GetAttribute("whenCreated").StringValue;
            WhenChanged = ldapData.GetAttribute("whenChanged").StringValue;
            MemberOf = LdapRepository.ValidateLdapArray(ldapData, "memberOf") ?? [];
        }
    }
}
