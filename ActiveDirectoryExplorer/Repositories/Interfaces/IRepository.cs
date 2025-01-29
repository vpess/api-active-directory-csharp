using ActiveDirectoryExplorer.Models.DTOs;
using Novell.Directory.Ldap;

namespace ActiveDirectoryExplorer.Repositories.Interfaces
{
    public interface IRepository
    {
        LdapEntry Get(GetDataDTO objectData);
    }
}
