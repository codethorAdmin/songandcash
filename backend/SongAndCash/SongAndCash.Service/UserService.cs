using SongAndCash.Exceptions;
using SongAndCash.Model.Entity;
using SongAndCash.Repository;

namespace SongAndCash.Service;

public class UserService(IUserRepository userRepository): IUserService
{
    public async Task<User> GetUser(int id)
    {
        var user = await userRepository.GetUser(id);
        if (user == null) throw new EntityNotFoundException($"User with id {id} was not found.");

        return user;
    }

    public async Task<User> CreateUser(CreateUser createUser)
    {
        var createdUser = await userRepository.CreateUser(createUser);
        return createdUser;
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
        if (string.IsNullOrEmpty(updateUser.Nationality?.Trim())) throw new EntityValidationException($"{nameof(updateUser.Nationality)} is required.");
        if (string.IsNullOrEmpty(updateUser.FiscalIdentificationNumber?.Trim())) throw new EntityValidationException($"{nameof(updateUser.FiscalIdentificationNumber)} is required.");
        if (string.IsNullOrEmpty(updateUser.Iban?.Trim())) throw new EntityValidationException($"{nameof(updateUser.Iban)} is required.");
        if (string.IsNullOrEmpty(updateUser.SpotifyLink?.Trim())) throw new EntityValidationException($"{nameof(updateUser.SpotifyLink)} is required.");
        if (string.IsNullOrEmpty(updateUser?.Username.Trim())) throw new EntityValidationException($"{nameof(updateUser.Username)} is required.");

        if (updateUser.IsCompany.HasValue && !updateUser.IsCompany.Value)
        {
            if (updateUser.DateOfBirth == null) throw new EntityValidationException($"{nameof(updateUser.DateOfBirth)} is required.");
            if (string.IsNullOrEmpty(updateUser.FirstName?.Trim())) throw new EntityValidationException($"{nameof(updateUser.FirstName)} is required.");
            if (string.IsNullOrEmpty(updateUser.LastName?.Trim())) throw new EntityValidationException($"{nameof(updateUser.LastName)} is required.");
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