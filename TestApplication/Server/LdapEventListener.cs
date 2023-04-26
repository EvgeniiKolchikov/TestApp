using Gatekeeper.LdapServerLibrary;
using Gatekeeper.LdapServerLibrary.Session.Events;

namespace Server;

public class LdapEventListener : LdapEvents
{
    public override Task<bool> OnAuthenticationRequest(ClientContext context, IAuthenticationEvent authenticationEvent)
    {
        authenticationEvent.Rdn.TryGetValue("cn", out var cnValue);
        authenticationEvent.Rdn.TryGetValue("dc", out var dcValue);

        if (cnValue.Contains("test1") && dcValue.Contains("example") && dcValue.Contains("com"))
        {
            return Task.FromResult(true);
        }
        else if (cnValue.Contains("OnlyBindUser") && authenticationEvent.Password == "OnlyBindUserPassword")
        {
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }
}