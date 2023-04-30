using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Net;
using WebApi.Interfaces;
using SearchScope = System.DirectoryServices.SearchScope;

namespace WebApi.Services;

public class Provider : IProvider
{
    private LdapDirectoryIdentifier _ident;
    private NetworkCredential _credential;
    private string _username = "cn=test,dc=test,dc=com";
    private string _password = "test";
   
    public bool UserExist()
    {
        try
        {
            _ident = new LdapDirectoryIdentifier("localhost:389");
            _credential = new NetworkCredential(_username,_password);
            using var ldapConnection = new LdapConnection(_ident,_credential, AuthType.Basic);
            ldapConnection.Bind();
            Console.WriteLine("Connected");
            return true;
        }
        catch (Exception) {
            Console.WriteLine("Error with ldap server ");
            return false;
        }
    }
}

