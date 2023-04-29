using Gatekeeper.LdapServerLibrary;
using Gatekeeper.LdapServerLibrary.Session.Events;
using Gatekeeper.LdapServerLibrary.Session.Replies;

namespace Ldap;

public class LdapEventListener : LdapEvents
{
    public override Task<bool> OnAuthenticationRequest(ClientContext context, IAuthenticationEvent authenticationEvent)
    {
        List<string> cnValue = null;
        authenticationEvent.Rdn.TryGetValue("cn", out cnValue);
        List<string> dcValue = null;
        authenticationEvent.Rdn.TryGetValue("dc", out dcValue);

        if (cnValue.Contains("test") && dcValue.Contains("test") && dcValue.Contains("com") && authenticationEvent.Password == "test")
        {
            return Task.FromResult(true);
        }
        
        return Task.FromResult(false);
    }
    
}