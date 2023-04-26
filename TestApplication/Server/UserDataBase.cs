namespace Server;

public class UserDataBase
{
    private readonly List<User> Users = new List<User>
    {
        new User
        {
            Dn = "cn=test1,dc=example,dc=com",
            Attributes = new Dictionary<string, List<string>>()
            {
                { "email", new List<string>() { "test1@example.com" } },
                { "role", new List<string>() { "Administrator" } },
                { "objectclass", new List<string>() { "inetOrgPerson" } },
                { "displayname", new List<string>() { "Test User 1" } },
                { "uid", new List<string>() { "test1" } },
            },
        }
    };

    internal List<User> GetUserDatabase()
    {
        return Users;
    }
}

internal class User
{
    internal string Dn { get; set; }
    internal Dictionary<string, List<string>> Attributes { get; set; }
}
    
