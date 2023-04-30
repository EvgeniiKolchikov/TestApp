using System.Net;
using Gatekeeper.LdapServerLibrary;
using Ldap;

var server = new LdapServer
{
    Port  = 389,
};
server.RegisterEventListener(new LdapEventListener());

await server.Start();