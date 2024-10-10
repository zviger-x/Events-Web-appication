using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UseCases.Interfaces;
using EventsManagement.BusinessLogic.UseCases.Interfaces.EventUser;
using EventsManagement.BusinessLogic.UseCases.Interfaces.User;
using EventsManagement.WebAPI.Server;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly ICreateUseCase<UserDTO> _createUserUseCase;
    private readonly IUpdateUseCase<UserDTO> _updateUserUseCase;
    private readonly IGetByIdUseCase<UserDTO> _getUserByIdUseCase;
    private readonly IGetUserByEmailUseCase _getUserByEmailUseCase;
    private readonly IGetEventsOfUserUseCase _getEventsOfUserUseCase;
    private readonly IVerifyUserPasswordUseCase _userVerifyPasswordUseCase;
    private readonly ICheckRegistrationUseCase _eventUserCheckRegistrationUseCase;
    private readonly IEventUserGetByUserIdAndEventIdUseCase _eventUserGetByUserIdAndEventIdUseCase;
    private readonly IRegisterUserInEventUseCase _registerUserInEventUseCase;
    private readonly IUnregisterUserInEventUseCase _unregisterUserInEventUseCase;
    private readonly IGenerateJwtTokenUseCase _generateJwtTokenUseCase;
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;

    public AccountController(ICreateUseCase<UserDTO> createUserUseCase,
        IUpdateUseCase<UserDTO> updateUserUseCase,
        IGetByIdUseCase<UserDTO> getUserByIdUseCase,
        IGetUserByEmailUseCase getUserByEmailUseCase,
        IGetEventsOfUserUseCase getEventsOfUserUseCase,
        IVerifyUserPasswordUseCase userVerifyPasswordUseCase,
        ICheckRegistrationUseCase eventUserCheckRegistrationUseCase,
        IEventUserGetByUserIdAndEventIdUseCase eventUserGetByUserIdAndEventIdUseCase,
        IRegisterUserInEventUseCase registerUserInEventUseCase,
        IUnregisterUserInEventUseCase unregisterUserInEventUseCase,
        IGenerateJwtTokenUseCase generateJwtTokenUseCase)
    {
        _createUserUseCase = createUserUseCase;
        _updateUserUseCase = updateUserUseCase;
        _getUserByIdUseCase = getUserByIdUseCase;
        _getUserByEmailUseCase = getUserByEmailUseCase;
        _getEventsOfUserUseCase = getEventsOfUserUseCase;
        _userVerifyPasswordUseCase = userVerifyPasswordUseCase;
        _eventUserCheckRegistrationUseCase = eventUserCheckRegistrationUseCase;
        _eventUserGetByUserIdAndEventIdUseCase = eventUserGetByUserIdAndEventIdUseCase;
        _registerUserInEventUseCase = registerUserInEventUseCase;
        _unregisterUserInEventUseCase = unregisterUserInEventUseCase;
        _generateJwtTokenUseCase = generateJwtTokenUseCase;
        _secretKey = JwtSettings.SecretKey;
        _issuer = JwtSettings.Issuer;
        _audience = JwtSettings.Audience;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var errorMessageInvalid = "Invalid email or password";

        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            return BadRequest(errorMessageInvalid);

        var user = await _getUserByEmailUseCase.Execute(request.Email);
        if (user == null)
            return BadRequest(errorMessageInvalid);

        bool isPasswordValid = _userVerifyPasswordUseCase.Execute((user.Password, request.Password));
        if (!isPasswordValid)
            return BadRequest(errorMessageInvalid);

        var token = _generateJwtTokenUseCase.Execute((user, _secretKey, _issuer, _audience));
        return Ok(new { AccessToken = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDTO user)
    {
        try
        {
            user.Role = "user";
            await _createUserUseCase.Execute(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(user);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<UserDTO>> Edit(int id, [FromBody] UserDTO user)
    {
        if (id != user.Id)
        {
            return NotFound();
        }

        try
        {
            await _updateUserUseCase.Execute(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(user);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetById(int id)
    {
        var user = await _getUserByIdUseCase.Execute(id);

        return Ok(user);
    }

    [HttpGet("byemail/{email}")]
    public async Task<ActionResult<UserDTO>> GetByEmail(string email)
    {
        var user = await _getUserByEmailUseCase.Execute(email);

        return Ok(user);
    }

    [HttpGet("events/{id}")]
    public ActionResult<UserDTO> GetUserEvents(int id)
    {
        var user = _getEventsOfUserUseCase.Execute(id);

        return Ok(user);
    }

    [HttpGet("IsUserRegisteredForEvent")]
    public async Task<IActionResult> IsUserRegisteredForEvent(
        [FromQuery] int? userId,
        [FromQuery] int? eventId)
    {
        if (userId is null || eventId is null)
            return BadRequest("Id cannot be null");

        var isRegistered = await _eventUserCheckRegistrationUseCase.Execute((userId.Value, eventId.Value));

        return Ok(isRegistered);
    }

    [HttpPost("RegisterForEvent")]
    public async Task<IActionResult> RegisterForEvent(
        [FromQuery] int? userId,
        [FromQuery] int? eventId)
    {
        if (userId is null)
            return BadRequest("User id cannot be null");
        if (eventId is null)
            return BadRequest("Event id cannot be null");

        var isRegistered = await _eventUserCheckRegistrationUseCase.Execute((userId.Value, eventId.Value));
    
        if (isRegistered)
        {
            return BadRequest("User is already registered for this event.");
        }
    
        var eventUser = new EventUserDTO
        {
            UserId = userId.Value,
            EventId = eventId.Value,
            RegistrationDate = DateTime.UtcNow
        };
        await _registerUserInEventUseCase.Execute(eventUser);
        return Ok("User registered for the event successfully.");
    }

    [HttpPost("UnregisterFromEvent")]
    public async Task<IActionResult> UnregisterFromEvent(
        [FromQuery] int? userId,
        [FromQuery] int? eventId)
    {
        if (userId is null)
            return BadRequest("User id cannot be null");
        if (eventId is null)
            return BadRequest("Event id cannot be null");

        var eventUser = await _eventUserGetByUserIdAndEventIdUseCase.Execute((userId.Value, eventId.Value));
        if (eventUser == null)
        {
            return NotFound("User is not registered for this event.");
        }

        await _unregisterUserInEventUseCase.Execute(new EventUserDTO() { Id = eventUser.Id });

        return Ok("User unregistered from the event successfully.");
    }
}