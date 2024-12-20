using Microsoft.AspNetCore.Mvc;

using SimpleAPI.Controllers.Requests;
using SimpleAPI.Controllers.Responses;
using SimpleAPI.Services;

namespace SimpleAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(UsersService usersService) : ControllerBase
{
    private readonly UsersService _usersService = usersService;

    [HttpGet("{userId:guid}")]
    public IActionResult Get(Guid userId)
    {
        var user = _usersService.Get(userId);

        return Ok(
            new
            {
                message = "User Found",
                user = UserResponse.FromDomain(user)
            }
        );
    }

    [HttpPost]
    public IActionResult Create(CreateUserRequest request)
    {
        if (request.IsValid() == false)
        {
            return BadRequest(
                new
                {
                    message = "Password and Confirm Password do not match"
                }
            );
        }

        var user = request.ToDomain();

        _usersService.Create(user);

        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { userId = user.Id },
            value: new
            {
                message = "User Created",
                user = UserResponse.FromDomain(user)
            }
        );
    }

    [HttpPut("{userId:guid}")]
    public IActionResult Update(Guid userId, UpdateUserRequest request)
    {
        var user = request.ToDomain();

        _usersService.Update(user, userId);

        return Ok(
            new
            {
                message = "User Updated",
                user = UserResponse.FromDomain(user)
            }
        );
    }

    [HttpDelete("{userId:guid}")]
    public IActionResult Delete(Guid userId)
    {
        _usersService.Delete(userId);

        return Ok(
            new
            {
                message = "User Deleted"
            }
        );
    }
}
