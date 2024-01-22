using ApiCatalogo.Models;
using ApiCatalogo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers;

[Route("[controller]")]
[AllowAnonymous]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public LoginController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    public ActionResult Login(UserModel userModel, ITokenService tokenService)
    {
        if (userModel == null)
            return BadRequest("Login inválido");
        if (userModel.UserName == "string" && userModel.Password == "string")
        {
            var tokenString = tokenService.GerarToken(_configuration["Jwt:Key"],
                                                      _configuration["Jwt:Issuer"],
                                                      _configuration["Jwt:Audience"],
                                                      userModel);

            return Ok(new { token = tokenString });
        }
        else
        {
            return BadRequest("Login Inválido");
        }
    }
}
