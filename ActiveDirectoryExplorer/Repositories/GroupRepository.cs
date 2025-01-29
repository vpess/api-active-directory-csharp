using ActiveDirectoryExplorer.Models;
using ActiveDirectoryExplorer.Models.DTOs;

namespace ActiveDirectoryExplorer.Repositories
{
    public class GroupRepository
    {
        public Group Get(GetDataDTO obj)
        {
            var ldapData = LdapRepository.Query(obj);

            try
            {
                Group groupData = new Group(ldapData);

                return groupData;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
