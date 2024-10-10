using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces;
using EventsManagement.BusinessLogic.UseCases.Interfaces.EventUser;
using EventsManagement.BusinessLogic.UseCases.Interfaces.User;
using EventsManagement.WebAPI.Server;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly ICreateUseCase<UserDTO> _createUserUseCase;
    private readonly IUpdateUseCase<UserDTO> _updateUserUseCase;
    private readonly IGetByIdUseCase<UserDTO> _getUserByIdUseCase;
    private readonly IGetUserByEmailUseCase _getUserByEmailUseCase;
    private readonly IVerifyLoginDataUseCase _verifyLoginDataUseCase;
    private readonly IGetEventsOfUserUseCase _getEventsOfUserUseCase;
    private readonly ICheckRegistrationUseCase _eventUserCheckRegistrationUseCase;
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
        IVerifyLoginDataUseCase verifyLoginDataUseCase,
        IGetEventsOfUserUseCase getEventsOfUserUseCase,
        ICheckRegistrationUseCase eventUserCheckRegistrationUseCase,
        IRegisterUserInEventUseCase registerUserInEventUseCase,
        IUnregisterUserInEventUseCase unregisterUserInEventUseCase,
        IGenerateJwtTokenUseCase generateJwtTokenUseCase)
    {
        _createUserUseCase = createUserUseCase;
        _updateUserUseCase = updateUserUseCase;
        _getUserByIdUseCase = getUserByIdUseCase;
        _getUserByEmailUseCase = getUserByEmailUseCase;
        _verifyLoginDataUseCase = verifyLoginDataUseCase;
        _getEventsOfUserUseCase = getEventsOfUserUseCase;
        _eventUserCheckRegistrationUseCase = eventUserCheckRegistrationUseCase;
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
        try
        {
            var user = await _verifyLoginDataUseCase.Execute(request);
            var token = _generateJwtTokenUseCase.Execute((user, _secretKey, _issuer, _audience));
            return Ok(new { AccessToken = token });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
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
        try
        {
            await _registerUserInEventUseCase.Execute((userId, eventId));
            return Ok("User registered for the event successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("UnregisterFromEvent")]
    public async Task<IActionResult> UnregisterFromEvent(
        [FromQuery] int? userId,
        [FromQuery] int? eventId)
    {
        try
        {
            await _unregisterUserInEventUseCase.Execute((userId, eventId));
            return Ok("User unregistered from the event successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}