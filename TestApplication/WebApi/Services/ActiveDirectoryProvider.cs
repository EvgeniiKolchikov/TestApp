using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Net;
using Microsoft.AspNetCore.Authentication;
using WebApi.Interfaces;
using static System.DirectoryServices.AccountManagement.ContextType;

namespace WebApi.Services;

public class ActiveDirectoryProvider : IActiveDirectoryProvider
{

    // private PrincipalContext _context;
    // private bool LdapOk;
    private LdapDirectoryIdentifier ident;
    private NetworkCredential _credential;
    public ActiveDirectoryProvider()
    {
        ident = new LdapDirectoryIdentifier("localhost:389");
        _credential = new NetworkCredential("test.test.com", "test");
    }
    
    // bool LdapConnected() 
    // {
    //     var ldapConnection = new LdapConnection(ident);
    //     var myTimeout = new TimeSpan(0, 0, 0, 1);
    //     try {
    //         using (ldapConnection)
    //         {
    //             ldapConnection.AuthType = AuthType.Anonymous;
    //             ldapConnection.AutoBind = false;
    //             ldapConnection.Timeout = myTimeout;
    //             ldapConnection.Bind();
    //             Console.WriteLine("Successfully authenticated to ldap server ");
    //             return true;
    //         }
    //     }
    //     catch (Exception) {
    //         Console.WriteLine("Error with ldap server ");
    //         return false;
    //     }
    // }

    
    public bool UserExist()
    {
        try
        {
            using var ldapConnection = new LdapConnection(ident,_credential, AuthType.Basic);
            ldapConnection.Bind();
            return true;
        }
        catch (Exception) {
            Console.WriteLine("Error with ldap server ");
            return false;
        }
    }

}

