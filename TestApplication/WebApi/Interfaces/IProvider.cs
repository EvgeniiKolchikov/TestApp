using System.DirectoryServices.AccountManagement;

namespace WebApi.Interfaces;

public interface IProvider
{
    bool UserExist();
}