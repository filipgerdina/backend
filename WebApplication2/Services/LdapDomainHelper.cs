using Novell.Directory.Ldap;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class LdapDomainHelper
{
    public static async Task<List<LdapEntry>> SearchUsersAsync(
        string ldapHost, int ldapPort, string bindDn, string password, string searchBase, string filter)
    {
        var results = new List<LdapEntry>();
        using (var connection = new LdapConnection())
        {
            await connection.ConnectAsync(ldapHost, ldapPort);
            await connection.BindAsync(bindDn, password);

            // The async method returns an ILdapSearchResults
            var searchResults = await connection.SearchAsync(
                searchBase,
                LdapConnection.ScopeSub,
                filter,
                null, // all attributes
                false
            );

            while (await searchResults.HasMoreAsync())
            {
                try
                {
                    var entry = await searchResults.NextAsync();
                    results.Add(entry);
                }
                catch (LdapException ex)
                {
                    // Optionally handle/search errors
                }
            }
        }
        return results;
    }
}
