using System.DirectoryServices.AccountManagement;

namespace WebApi.Interfaces;

public interface IActiveDirectoryProvider
{
    bool UserExist();
}