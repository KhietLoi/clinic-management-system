using Clinic.Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

public class PasswordHasherService : IPasswordHasherService
{
    private readonly PasswordHasher<object> _hasher = new();

    public string Hash(string password)
    {
        return _hasher.HashPassword(null!, password);
    }

    public bool Verify(string hashedPassword, string providedPassword)
    {
        var result = _hasher.VerifyHashedPassword(
            null!, hashedPassword, providedPassword);

        return result == PasswordVerificationResult.Success;
    }
}
