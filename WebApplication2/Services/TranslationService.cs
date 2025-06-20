using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class TranslationService
    {

        public TranslationService()
        {

        }

        private List<TranslationClass> _translatetions = new List<TranslationClass>
        {
            new() { Id = 1, Key = "s:mySettings", LanguageId = 1, Value = "My Settings" },
            new() { Id = 2, Key = "s:mySettings", LanguageId = 2, Value = "Moje nastavitve" },

            new() { Id = 3, Key = "s:signOut", LanguageId = 1, Value = "Sign Out" },
            new() { Id = 4, Key = "s:signOut", LanguageId = 2, Value = "Odjava" },

            new() { Id = 5, Key = "s:applicationManagement", LanguageId = 1, Value = "Application Management" },
            new() { Id = 6, Key = "s:applicationManagement", LanguageId = 2, Value = "Upravljanje aplikacij" },

            new() { Id = 7, Key = "s:usersAndRoles", LanguageId = 1, Value = "Users and Roles" },
            new() { Id = 8, Key = "s:usersAndRoles", LanguageId = 2, Value = "Uporabniki in vloge" },

            new() { Id = 9, Key = "s:usersManagement", LanguageId = 1, Value = "User Management" },
            new() { Id = 10, Key = "s:usersManagement", LanguageId = 2, Value = "Upravljanje uporabnikov" },

            new() { Id = 11, Key = "s:rolesManagement", LanguageId = 1, Value = "Role Management" },
            new() { Id = 12, Key = "s:rolesManagement", LanguageId = 2, Value = "Upravljanje vlog" },

            new() { Id = 13, Key = "s:username", LanguageId = 1, Value = "Username" },
            new() { Id = 14, Key = "s:username", LanguageId = 2, Value = "Uporabniško ime" },

            new() { Id = 15, Key = "s:displayName", LanguageId = 1, Value = "Display Name" },
            new() { Id = 16, Key = "s:displayName", LanguageId = 2, Value = "Prikazno ime" },

            new() { Id = 17, Key = "s:email", LanguageId = 1, Value = "Email" },
            new() { Id = 18, Key = "s:email", LanguageId = 2, Value = "E-pošta" },

            new() { Id = 19, Key = "s:isSystem", LanguageId = 1, Value = "Is System" },
            new() { Id = 20, Key = "s:isSystem", LanguageId = 2, Value = "Je sistemski" },

            new() { Id = 21, Key = "s:isLocked", LanguageId = 1, Value = "Is Locked" },
            new() { Id = 22, Key = "s:isLocked", LanguageId = 2, Value = "Je zaklenjen" },

            new() { Id = 23, Key = "s:domain", LanguageId = 1, Value = "Domain" },
            new() { Id = 24, Key = "s:domain", LanguageId = 2, Value = "Domena" },

            new() { Id = 25, Key = "s:name", LanguageId = 1, Value = "Name" },
            new() { Id = 26, Key = "s:name", LanguageId = 2, Value = "Ime" },

            new() { Id = 27, Key = "s:defaultPage", LanguageId = 1, Value = "Default Page" },
            new() { Id = 28, Key = "s:defaultPage", LanguageId = 2, Value = "Privzeta stran" },

            new() { Id = 29, Key = "s:newUser", LanguageId = 1, Value = "New User" },
            new() { Id = 30, Key = "s:newUser", LanguageId = 2, Value = "Nov uporabnik" },

            new() { Id = 31, Key = "s:editUser", LanguageId = 1, Value = "Edit User" },
            new() { Id = 32, Key = "s:editUser", LanguageId = 2, Value = "Uredi uporabnika" },

            new() { Id = 33, Key = "s:syncDomainUSers", LanguageId = 1, Value = "Sync Domain Users" },
            new() { Id = 34, Key = "s:syncDomainUSers", LanguageId = 2, Value = "Sinhroniziraj uporabnike domene" },

            new() { Id = 35, Key = "s:assignRoleToUser", LanguageId = 1, Value = "Assign Role To User" },
            new() { Id = 36, Key = "s:assignRoleToUser", LanguageId = 2, Value = "Dodeli vlogo uporabniku" },

            new() { Id = 37, Key = "s:removeRoleFromUser", LanguageId = 1, Value = "Remove Role From User" },
            new() { Id = 38, Key = "s:removeRoleFromUser", LanguageId = 2, Value = "Odstrani vlogo uporabniku" },

            new() { Id = 39, Key = "s:users", LanguageId = 1, Value = "Users" },
            new() { Id = 40, Key = "s:users", LanguageId = 2, Value = "Uporabniki" },

            new() { Id = 41, Key = "s:userRoles", LanguageId = 1, Value = "User Roles" },
            new() { Id = 42, Key = "s:userRoles", LanguageId = 2, Value = "Uporabniške vloge" },

            new() { Id = 43, Key = "s:roles", LanguageId = 1, Value = "Roles" },
            new() { Id = 44, Key = "s:roles", LanguageId = 2, Value = "Vloge" },

            new() { Id = 45, Key = "s:refresh", LanguageId = 1, Value = "Refresh" },
            new() { Id = 46, Key = "s:refresh", LanguageId = 2, Value = "Osveži" },
            new() { Id = 47, Key = "s:search", LanguageId = 1, Value = "Search" },
            new() { Id = 48, Key = "s:search", LanguageId = 2, Value = "Išči" },

            new() { Id = 49, Key = "s:save", LanguageId = 1, Value = "Save" },
            new() { Id = 49, Key = "s:save", LanguageId = 1, Value = "Save" },
            new() { Id = 50, Key = "s:save", LanguageId = 2, Value = "Shrani" },

            new() { Id = 51, Key = "s:firstName", LanguageId = 1, Value = "First Name" },
            new() { Id = 52, Key = "s:firstName", LanguageId = 2, Value = "Ime" },
            
            new() { Id = 53, Key = "s:lastName", LanguageId = 1, Value = "Last Name" },
            new() { Id = 54, Key = "s:lastName", LanguageId = 2, Value = "Priimek" },

            new() { Id = 55, Key = "s:domainUsername", LanguageId = 1, Value = "Domain User Name" },
            new() { Id = 56, Key = "s:domainUsername", LanguageId = 2, Value = "Domensko uporabniško ime" },

            new() { Id = 57, Key = "s:profile", LanguageId = 1, Value = "Profile" },
            new() { Id = 58, Key = "s:profile", LanguageId = 2, Value = "Profil" },
            new() { Id = 59, Key = "s:basicInformation", LanguageId = 1, Value = "Basic Information" },
            new() { Id = 60, Key = "s:basicInformation", LanguageId = 2, Value = "Osnovne informacije" },

            new() { Id = 61, Key = "s:decimalSeperator", LanguageId = 1, Value = "Decimal Separator" },
            new() { Id = 62, Key = "s:decimalSeperator", LanguageId = 2, Value = "Decimalno ločilo" },

            new() { Id = 63, Key = "s:dateTimeFormat", LanguageId = 1, Value = "Date and Time Format" },
            new() { Id = 64, Key = "s:dateTimeFormat", LanguageId = 2, Value = "Oblika datuma in časa" },

            new() { Id = 65, Key = "s:language", LanguageId = 1, Value = "Language" },
            new() { Id = 66, Key = "s:language", LanguageId = 2, Value = "Jezik" },

            new() { Id = 67, Key = "s:english", LanguageId = 1, Value = "English" },
            new() { Id = 68, Key = "s:english", LanguageId = 2, Value = "Angleščina" },

            new() { Id = 69, Key = "s:slovenian", LanguageId = 1, Value = "Slovenian" },
            new() { Id = 70, Key = "s:slovenian", LanguageId = 2, Value = "Slovenščina" },

            new() { Id = 71, Key = "s:comma", LanguageId = 1, Value = "Comma" },
            new() { Id = 72, Key = "s:comma", LanguageId = 2, Value = "Vejica" },

            new() { Id = 73, Key = "s:dot", LanguageId = 1, Value = "Dot" },
            new() { Id = 74, Key = "s:dot", LanguageId = 2, Value = "Pika" },

            new() { Id = 75, Key = "s:assignRoleToUser", LanguageId = 1, Value = "Assign Role To User" },
            new() { Id = 76, Key = "s:assignRoleToUser", LanguageId = 2, Value = "Dodeli vlogo uporabniku" },

            new() { Id = 77, Key = "s:role", LanguageId = 1, Value = "Role" },
            new() { Id = 78, Key = "s:role", LanguageId = 2, Value = "Vloga" },

            new() { Id = 79, Key = "s:editUser", LanguageId = 1, Value = "Edit User" },
            new() { Id = 80, Key = "s:editUser", LanguageId = 2, Value = "Uredi uporabnika" },

            new() { Id = 81, Key = "s:password", LanguageId = 1, Value = "Password" },
            new() { Id = 82, Key = "s:password", LanguageId = 2, Value = "Geslo" },

            new() { Id = 83, Key = "s:domain", LanguageId = 1, Value = "Domain" },
            new() { Id = 84, Key = "s:domain", LanguageId = 2, Value = "Domena" },

            new() { Id = 85, Key = "s:syncDomainUsers", LanguageId = 1, Value = "Sync Domain Users" },
            new() { Id = 86, Key = "s:syncDomainUsers", LanguageId = 2, Value = "Sinhroniziraj uporabnike domene" },

            new() { Id = 87, Key = "s:addUser", LanguageId = 1, Value = "Add User" },
            new() { Id = 88, Key = "s:addUser", LanguageId = 2, Value = "Dodaj uporabnika" },

            new() { Id = 89, Key = "s:unlockUser", LanguageId = 1, Value = "Unlock User" },
            new() { Id = 90, Key = "s:unlockUser", LanguageId = 2, Value = "Odkleni uporabnika" },

            new() { Id = 91, Key = "s:lockUser", LanguageId = 1, Value = "Lock User" },
            new() { Id = 92, Key = "s:lockUser", LanguageId = 2, Value = "Zakleni uporabnika" },

            new() { Id = 93, Key = "s:removeRoleFromUser", LanguageId = 1, Value = "Remove Role From User" },
            new() { Id = 94, Key = "s:removeRoleFromUser", LanguageId = 2, Value = "Odstrani vlogo uporabniku" },

            new() { Id = 95, Key = "s:newRole", LanguageId = 1, Value = "New Role" },
            new() { Id = 96, Key = "s:newRole", LanguageId = 2, Value = "Nova vloga" },

            new() { Id = 97, Key = "s:pagePermissions", LanguageId = 1, Value = "Page Permissions" },
            new() { Id = 98, Key = "s:pagePermissions", LanguageId = 2, Value = "Dovoljenja strani" },

            new() { Id = 99, Key = "s:navigationGroupPermissions", LanguageId = 1, Value = "Navigation Group Permissions" },
            new() { Id = 100, Key = "s:navigationGroupPermissions", LanguageId = 2, Value = "Dovoljenja navigacijskih skupin" },

            new() { Id = 101, Key = "s:editRole", LanguageId = 1, Value = "Edit Role" },
            new() { Id = 102, Key = "s:editRole", LanguageId = 2, Value = "Uredi vlogo" },

            new() { Id = 103, Key = "s:addPagePermission", LanguageId = 1, Value = "Add Page Permission" },
            new() { Id = 104, Key = "s:addPagePermission", LanguageId = 2, Value = "Dodaj dovoljenje strani" },

            new() { Id = 105, Key = "s:removePagePermission", LanguageId = 1, Value = "Remove Page Permission" },
            new() { Id = 106, Key = "s:removePagePermission", LanguageId = 2, Value = "Odstrani dovoljenje strani" },

            new() { Id = 107, Key = "s:addNavigationGroupPermission", LanguageId = 1, Value = "Add Navigation Group Permission" },
            new() { Id = 108, Key = "s:addNavigationGroupPermission", LanguageId = 2, Value = "Dodaj navigacijskih skupin" },

            new() { Id = 109, Key = "s:removeNavigationGroupPermission", LanguageId = 1, Value = "Remove Navigation Group Permission" },
            new() { Id = 110, Key = "s:removeNavigationGroupPermission", LanguageId = 2, Value = "Odstrani dovoljenje navigacijskih skupin" },

            new() { Id = 111, Key = "s:navigationGroup", LanguageId = 1, Value = "Navigation Group" },
            new() { Id = 112, Key = "s:navigationGroup", LanguageId = 2, Value = "Navigacijska skupina" },

            new() { Id = 113, Key = "s:page", LanguageId = 1, Value = "Page" },
            new() { Id = 114, Key = "s:page", LanguageId = 2, Value = "Stran" },

        };

        public IEnumerable<TranslationClass> GetTranslations(int langId)
        {
            return _translatetions.FindAll(t => t.LanguageId == langId);
        }
    }
}
