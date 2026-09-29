using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AulaAuth01.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AulaAuth01.controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IConfiguration config) : ControllerBase
{
    private static readonly List<Usuario> _usuarios =
    [
        new("Celia Csharp", "123", "Admin", "TI"),
        new("Asaaf Asp.Net", "124", "Aluno", "Vendas"),
        new("Amy Winehouse", "124", "Aluno", "Financeiro"),
    ];
    [HttpPost("login")]
    public IActionResult Login(LoginDto req)
    {
        var usuario = _usuarios.FirstOrDefault(u => req.login == u.User && req.password == u.Password);
        if (usuario is null) return Unauthorized("Login ou senha inválidos");

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usuario.User),
            new Claim(ClaimTypes.Role, usuario.Role),
            new Claim("setor", usuario.Setor)
        };
        
        var chave =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));

        var token = new JwtSecurityToken(
            issuer: config["JWT:Issuer"],
            audience: config["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: new SigningCredentials(chave, SecurityAlgorithms.HmacSha256)
        );
        
        return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});

    }

    public record LoginDto(String login, string password);


}