using ActiveDirectoryExplorer.Models;
using ActiveDirectoryExplorer.Models.Enums;
using ActiveDirectoryExplorer.Models.DTOs;
using ActiveDirectoryExplorer.Services;
using Novell.Directory.Ldap;

namespace ActiveDirectoryExplorer.Repositories
{
    public class LdapRepository
    {
        public static LdapEntry? Query(GetDataDTO objectData)
        {
            string searchFilter = CreateLdapFilter(objectData);

            using (LdapConnection connection = CreateLdapConnection(objectData.Domain, search: true))
            {
                ILdapSearchResults searchResults = connection.Search(
                    GetLdapSearchBase(objectData.Domain),
                    LdapConnection.ScopeSub,
                    searchFilter,
                    null,
                    false
                );

                return searchResults.HasMore() ? searchResults?.Next() : null;
            }
        }

        public static bool Mutation(PatchDataDTO objectData)
        {
            LdapEntry ldapObject = Query(new GetDataDTO
            {
                Name = objectData.Name,
                Domain = objectData.Domain,
                Type = objectData.Type
            });

            LdapEntry ldapGroup = Query(new GetDataDTO
            {
                Name = objectData.Group,
                Domain = objectData.Domain,
                Type = "group"
            });

            LdapModification[] modGroup = new LdapModification[1];

            LdapAttribute member = new LdapAttribute("member", ldapObject.Dn);

            modGroup[0] = objectData.Action == ActiveDirectoryOperation.ADD
                ? new LdapModification(LdapModification.Add, member)
                : new LdapModification(LdapModification.Delete, member);

            using (LdapConnection connection = CreateLdapConnection(objectData.Domain, search: false))
            {
                try
                {
                    connection.Modify(ldapGroup.Dn, modGroup);
                }
                catch (LdapException ldapEx)
                {
                    throw new Exception("LDAP error during mutation", ldapEx);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return true;

        }

        private static LdapConnection CreateLdapConnection(string domain, bool search)
        {
            var auth = new ActiveDirectoryAuthService().SignIn(domain);

            LdapConnection connection = new LdapConnection();

            try
            {
                ConfigureLdapConnection(connection, auth, search);
                return connection;
            }
            catch (LdapException ldapEx)
            {
                throw new Exception("Erro ao conectar ao LDAP", ldapEx);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string GetLdapSearchBase(string domain)
        {
            ConfigurationRoot configuration = (ConfigurationRoot)new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            return configuration[$"Domains:{domain}:controller"];
        }

        private static string CreateLdapFilter(GetDataDTO objectData)
        {
            return objectData.Name.Contains("@")
                ? $"(&(mail={objectData.Name})(objectClass={objectData.Type}))"
                : $"(&(name={objectData.Name})(objectClass={objectData.Type}))";
        }

        private static void ConfigureLdapConnection(LdapConnection connection, ActiveDirectoryAuth auth, bool search)
        {
            int ldapPort = search ? 3268 : 389; //389 to mutate and 3268 to search

            connection.Connect(auth.Host, ldapPort);
            connection.Bind(auth.UserDN, auth.UserPswd);
            connection.ConnectionTimeout = 60;

            var constraints = new LdapSearchConstraints
            {
                ReferralFollowing = true,
            };

            connection.Constraints = constraints;
        }

        public static string[]? ValidateLdapArray(LdapEntry ldapObj, string attribute)
        {
            try
            {
                return ldapObj.GetAttribute($"{attribute}")?.StringValueArray;
            }
            catch (Exception)
            {
                return [];
            }
        }

        public static string? ValidateLdapAttribute(LdapEntry ldapObj, string attribute)
        {
            try
            {
                return ldapObj.GetAttribute($"{attribute}")?.StringValue;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}