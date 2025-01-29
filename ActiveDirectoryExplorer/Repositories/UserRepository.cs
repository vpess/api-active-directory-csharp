using ActiveDirectoryExplorer.Models;
using ActiveDirectoryExplorer.Models.DTOs;

namespace ActiveDirectoryExplorer.Repositories
{
    public class UserRepository
    {
        public User Get(GetDataDTO obj)
        {
            var ldapData = LdapRepository.Query(obj);

            User userData = new User(ldapData);

            return userData;
        }

        public bool Update(PatchDataDTO obj) //TODO Melhorar a implementação
        {
            return LdapRepository.Mutation(obj);
        }
    }
}
