using ActiveDirectoryExplorer.Models;
using ActiveDirectoryExplorer.Models.DTOs;

namespace ActiveDirectoryExplorer.Repositories
{
    public class ComputerRepository
    {
        public Computer Get(GetDataDTO obj)
        {
            var ldapData = LdapRepository.Query(obj);

            return new Computer(ldapData) ?? null;
        }

        public bool Update(PatchDataDTO obj) //TODO Melhorar a implementação
        {
            return LdapRepository.Mutation(obj);
        }
    }
}
