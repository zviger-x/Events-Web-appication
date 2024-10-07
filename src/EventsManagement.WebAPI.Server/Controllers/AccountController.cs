using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.EventService;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.WebAPI.Server;
using IdentityServer4.Hosting.LocalApiAuthentication;
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
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;

    public AccountController(ICreateUseCase<UserDTO> createUserUseCase,
        IUpdateUseCase<UserDTO> updateUserUseCase,
        IGetByIdUseCase<UserDTO> getUserByIdUseCase,
        IGetUserByEmailUseCase getUserByEmailUseCase,
        IGetEventsOfUserUseCase getEventsOfUserUseCase,
        IVerifyUserPasswordUseCase userVerifyPasswordUseCase)
    {
        _createUserUseCase = createUserUseCase;
        _updateUserUseCase = updateUserUseCase;
        _getUserByIdUseCase = getUserByIdUseCase;
        _getUserByEmailUseCase = getUserByEmailUseCase;
        _getEventsOfUserUseCase = getEventsOfUserUseCase;
        _userVerifyPasswordUseCase = userVerifyPasswordUseCase;
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

        var user = await _getUserByEmailUseCase.GetByEmailAsync(request.Email);
        if (user == null)
            return BadRequest(errorMessageInvalid);

        bool isPasswordValid = _userVerifyPasswordUseCase.VerifyPassword(user.Password, request.Password);
        if (!isPasswordValid)
            return BadRequest(errorMessageInvalid);

        var token = GenerateTokenAsync(user);
        return Ok(new { AccessToken = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDTO user)
    {
        try
        {
            user.Role = "user";
            await _createUserUseCase.CreateAsync(user);
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
            await _updateUserUseCase.UpdateAsync(user);
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
        var user = await _getUserByIdUseCase.GetByIdAsync(id);

        return Ok(user);
    }

    [HttpGet("byemail/{email}")]
    public async Task<ActionResult<UserDTO>> GetByEmail(string email)
    {
        var user = await _getUserByEmailUseCase.GetByEmailAsync(email);

        return Ok(user);
    }

    [HttpGet("events/{id}")]
    public ActionResult<UserDTO> GetUserEvents(int id)
    {
        var user = _getEventsOfUserUseCase.GetEventsOfUser(id);

        return Ok(user);
    }

    private string GenerateTokenAsync(UserDTO user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("Id", user.Id.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}