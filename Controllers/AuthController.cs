using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreManagment.Auth;
using StoreManagment.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace StoreManagment.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;

         private readonly IConfiguration _configuration;


    public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost("register")]

      public  async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        var user = new ApplicationUser
        {
            UserName = request.UserName,
            Email = request.Email
        };
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok("User registered successfully");

    }


    [HttpPost("Login")]

    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
       
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user == null)
            return Unauthorized("Invalid email or password.");



        var isPasswordValid = await _userManager.CheckPasswordAsync(
       user,
       request.Password);

        if (!isPasswordValid)
            return Unauthorized("Invalid username or password.");



        string issuer = _configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("Issuer key is missing."); 
        string audience = _configuration["Jwt:Audience"] ?? throw new InvalidOperationException("Aude key is missing."); ;
        string jwtKey = _configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("JWT Key is missing.");

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey));

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName)
        };
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

      

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);


        var token = new JwtSecurityToken(
        issuer: issuer,
        audience: audience,
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(30),
        signingCredentials: credentials);


        var tokenString = new JwtSecurityTokenHandler()  .WriteToken(token);

        return Ok(new
        {
            message = "Login successful",
            token = tokenString
        });


    }










}

