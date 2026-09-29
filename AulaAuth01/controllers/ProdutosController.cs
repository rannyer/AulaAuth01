using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AulaAuth01.controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController: ControllerBase
{
 
    [HttpGet("publico")]
    public IActionResult Publico()
    {
        
        return Ok("Qualquer pode ver isso aqui");
    }
    [Authorize(Roles = "Aluno")]
    [HttpGet("logado")]
    public IActionResult Logado()
    {
        return Ok($"Ola, {User.Identity!.Name})");
    }
    [Authorize(Policy = "SoFinanceiro")]
    [HttpGet("admin")]
    public IActionResult Admin()
    {
        return Ok("Só admin deveria ver isso");
    }
}