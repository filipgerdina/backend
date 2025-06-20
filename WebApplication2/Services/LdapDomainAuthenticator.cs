using Novell.Directory.Ldap;
using System.Threading.Tasks;

public static class LdapDomainAuthenticator
{
    public static async Task<bool> AuthenticateAsync(string username, string password, string domain)
    {
        try
        {
            using (var connection = new LdapConnection())
            {
                await connection.ConnectAsync(domain, 389);
                var userDn = $"{username}@{domain}";
                await connection.BindAsync(userDn, password);
                return connection.Bound;
            }
        }
        catch
        {
            return false;
        }
    }
}
