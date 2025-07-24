using MediatR;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.Responses;
using System.Security.Cryptography;
using WebApplication2.Services;
using WebApplication2.Controllers;

namespace WebApplication2.Business.User.Commands.SyncDomainUsersCommand
{
    public class SyncDomainUsersCommandHandler : IRequestHandler<SyncDomainUsersCommand, RecordIDResponse>
    {
        private readonly UserService _userService;
        private readonly RoleService _roleService;

        public SyncDomainUsersCommandHandler(UserService userService, RoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<RecordIDResponse> Handle(SyncDomainUsersCommand request, CancellationToken cancellationToken)
        {
            var response = new RecordIDResponse();

            var domain = request.Data.ExtraParamsFormValues.Domain;
            if (domain == null)
                domain = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;

            var searchBase = "";
            var searchBaseArr = domain.Split(".");
            for (var i = 0; i < searchBaseArr.Length; i++)
            {
                if (!searchBase.Equals(""))
                {
                    searchBase += ",";
                }

                searchBase += "DC=" + searchBaseArr[i];
            }
            int ldapPort = 389;
            string bindDn = request.Data.ExtraParamsFormValues.Username + "@" + domain;
            string password = request.Data.ExtraParamsFormValues.Password;



            var res = new SyncUsersResponse();

            var roles = _roleService.GetRoles().ToList();

            foreach (var role in roles)
            {

                string groupFilter = $"(&(objectClass=group)(cn={role.Name}))";
                var groups = await LdapDomainHelper.SearchUsersAsync(domain, ldapPort, bindDn, password, searchBase, groupFilter);
                var group = groups.FirstOrDefault();
                if (group == null)
                    continue;
                Console.WriteLine("group.Dn: " + group.Dn);
                var groupDn = group.Dn;

                string filter = $"(&(objectClass=user)(memberOf={group.Dn}))";


                var users = await LdapDomainHelper.SearchUsersAsync(domain, ldapPort, bindDn, password, searchBase, filter);

                foreach (var entry in users)
                {
                    var username = entry.GetStringValueOrDefault("sAMAccountName");
                    var displayName = entry.GetStringValueOrDefault("displayName");
                    var givenName = entry.GetStringValueOrDefault("givenName");
                    var fullName = entry.GetStringValueOrDefault("name");
                    var email = entry.GetStringValueOrDefault("mail");

                    var lastName = " ";
                    if (fullName != null && givenName != null && fullName.Split(givenName + " ").Length > 1) {
                        lastName = fullName.Split(givenName + " ")[1];
                    }
                    var user = _userService.GetUsers().ToList().Find(u => u.Username.Equals(username));
                    var userId = -1;
                    if (user != null)
                    {
                        userId = user.Id;
                    }
                    else
                    {

                        userId = _userService.AddUser(new UserClassEdit()
                        {
                            Username = username,
                            DisplayName = displayName,
                            Email = email,
                            Domain = domain,
                            FirstName = givenName,
                            LastName = lastName
                            
                        });
                    }

                    _userService.AddRoleToUser(new UserClassEdit()
                    {
                        RoleId = role.Id,
                        Id = userId
                    });
                }
            }

            return response;
        }
    }
}
