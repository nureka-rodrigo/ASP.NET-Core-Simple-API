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
        return Ok("User Found");
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
            value: new {
                message = "User Created",
                user = UserResponse.FromDomain(user)
            }
        );
    }
}
