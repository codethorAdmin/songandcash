using Microsoft.AspNetCore.Authentication;
using SongAndCash.Exceptions;
using SongAndCash.Model.Entity;
using SongAndCash.Repository;
using SongAndCash.Service.Mapper;

namespace SongAndCash.Service.Business;

public class UserService(IUserRepository userRepository, IUserMapper userMapper) : IUserService
{
    public async Task<User> GetUser(int id)
    {
        var user = await userRepository.GetUser(id);
        if (user == null)
            throw new EntityNotFoundException($"User with id {id} was not found.");

        return user;
    }

    public async Task<User> LoginOrRegister(AuthenticateResult authenticateResult)
    {
        var username =
            authenticateResult
                .Principal?.Claims?.FirstOrDefault(x =>
                    x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
                )
                ?.Value ?? "";
        var user = await userRepository.GetUserByUsername(username);

        if (user == null)
        {
            var createdUser = await CreateUser(
                new CreateUser
                {
                    Username = username,
                    Email = username,
                    SpotifyLink = "",
                }
            );

            return createdUser;
        }

        return user;
    }

    public async Task<User> CreateUser(CreateUser createUser)
    {
        var existingUser = await GetUserByUsername(createUser.Username);
        if (existingUser != null)
            throw new EntityValidationException(
                $"User with username {createUser.Username} already exists."
            );

        var userToCreate = userMapper.FromCreateToUser(createUser);
        var createdUser = await userRepository.CreateUser(userToCreate);

        return createdUser;
    }

    public async Task<User> GetUserByUsername(string username)
    {
        var user = await userRepository.GetUserByUsername(username);
        return user;
    }

    public async Task<User> UpdateUser(int id, UpdateUser updateUser)
    {
        var user = await GetUser(id);
        ValidateUserProperties(updateUser);
        UpdateUserProperties(user, updateUser);
        await userRepository.UpdateUser(user);

        return user;
    }

    private void ValidateUserProperties(UpdateUser updateUser)
    {
        if (string.IsNullOrEmpty(updateUser.Nationality?.Trim()))
            throw new EntityValidationException($"{nameof(updateUser.Nationality)} is required.");
        if (string.IsNullOrEmpty(updateUser.FiscalIdentificationNumber?.Trim()))
            throw new EntityValidationException(
                $"{nameof(updateUser.FiscalIdentificationNumber)} is required."
            );
        if (string.IsNullOrEmpty(updateUser.Iban?.Trim()))
            throw new EntityValidationException($"{nameof(updateUser.Iban)} is required.");
        if (string.IsNullOrEmpty(updateUser.SpotifyLink?.Trim()))
            throw new EntityValidationException($"{nameof(updateUser.SpotifyLink)} is required.");
        if (string.IsNullOrEmpty(updateUser?.Username.Trim()))
            throw new EntityValidationException($"{nameof(updateUser.Username)} is required.");

        if (updateUser.IsCompany.HasValue && !updateUser.IsCompany.Value)
        {
            if (updateUser.DateOfBirth == null)
                throw new EntityValidationException(
                    $"{nameof(updateUser.DateOfBirth)} is required."
                );
            if (string.IsNullOrEmpty(updateUser.FirstName?.Trim()))
                throw new EntityValidationException($"{nameof(updateUser.FirstName)} is required.");
            if (string.IsNullOrEmpty(updateUser.LastName?.Trim()))
                throw new EntityValidationException($"{nameof(updateUser.LastName)} is required.");
        }
    }

    private void UpdateUserProperties(User user, UpdateUser updateUser)
    {
        user.FirstName = updateUser.FirstName;
        user.LastName = updateUser.LastName;
        user.Username = updateUser.Username;
        user.IsCompany = updateUser.IsCompany;
        user.FiscalIdentificationNumber = updateUser.FiscalIdentificationNumber;
        user.DateOfBirth = updateUser.DateOfBirth;
        user.Iban = updateUser.Iban;
        user.Nationality = updateUser.Nationality;
        user.SpotifyLink = updateUser.SpotifyLink;
    }
}
