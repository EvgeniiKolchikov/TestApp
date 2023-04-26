using System.Net;
using Gatekeeper.LdapServerLibrary;
using Server;

var server = new LdapServer
{
  Port  = 3389
};

server.RegisterEventListener(new LdapEventListener());

await server.Start();