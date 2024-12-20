using System.ComponentModel.DataAnnotations;

using SimpleAPI.Domain;

namespace SimpleAPI.Controllers.Requests;

public record CreateUserRequest(
    [Required]
    [StringLength(50, MinimumLength = 2)]
    string FirstName,

    [Required]
    [StringLength(50, MinimumLength = 2)]
    string LastName,

    [Required]
    [EmailAddress]
    string Email,

    [Required]
    DateOnly? DateOfBirth,

    [Required]
    [StringLength(6, MinimumLength = 4)]
    string Gender,

    [Required]
    [Phone]
    string PhoneNumber,

    [Required]
    [StringLength(100, MinimumLength = 10)]
    string Address,

    [Required]
    [StringLength(100, MinimumLength = 8)]
    string Password,

    [Required]
    [StringLength(100, MinimumLength = 8)]
    string ConfirmPassword
)
{
    public bool IsValid()
    {
        return Password == ConfirmPassword;
    }
    
    public User ToDomain()
    {
        return new User
        {
            Id = Guid.NewGuid(),
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Address = Address,
            DateOfBirth = DateOfBirth!.Value,
            Gender = Gender,
            Password = Password
        };
    }
}