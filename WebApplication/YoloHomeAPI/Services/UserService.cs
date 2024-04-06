using YoloHomeAPI.Data;
using YoloHomeAPI.Repositories;
using YoloHomeAPI.Repositories.Interfaces;
using YoloHomeAPI.Services.Interfaces;

namespace YoloHomeAPI.Services;

public class UserService : IUserService
{
    private readonly IUserRepo _userRepo;

    public UserService(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<IUserService.UserResult> GetUserInformation(string username)
    {
        try
        {
            var userInformation = await _userRepo.GetByUserInformationAsync(username);
            return userInformation == null
                ? new IUserService.UserResult(false, null!)
                : new IUserService.UserResult(true, userInformation);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new IUserService.UserResult(false, null!);
        }
    }

    public async Task<IUserService.UserResult> RemoveUser(string username)
    {
        try
        {
            await _userRepo.DeleteAsync(username);
            return new IUserService.UserResult(true, null!);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            return new IUserService.UserResult(false, null!);
        }
    }

    public async Task<IUserService.UserResult> UpdateUserPassword(string username, string password)
    {
        try
        {
            var user = new UserAuthenticationData()
            {
                UserName = username,
                PasswordHash = password,
                PasswordSalt = password
            };
            await _userRepo.UpdateUserPasswordAsync(user);
            return new IUserService.UserResult(true, null!);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new IUserService.UserResult(false, null!);
        }
    }

    public async Task<IUserService.UserResult> UpdateUserInformation(string username, string firstName, string lastName,
        string email)
    {
        try
        {
            var userInformation = new UserData()
            {
                UserName = username,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
            await _userRepo.UpdateUserInformationAsync(userInformation);
            return new IUserService.UserResult(true, null!);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new IUserService.UserResult(false, null!);
        }
    }
}