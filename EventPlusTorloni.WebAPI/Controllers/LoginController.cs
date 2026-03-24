using EventPlusTorloni.WebAPI.DTO;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using EventPlusTorloni.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EventPlusTorloni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuariorRepository;

        public LoginController(IUsuarioRepository usuariorRepository)
        {
            _usuariorRepository = usuariorRepository;
        }

        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO)
        {
            try
            {
                if(loginDTO.IdTipoUsuario == null)
                    {
                    return BadRequest("IdTipoUsuario e obrigatorio");
                }
                Usuario usuarioBuscado = _usuariorRepository.BuscarPorEmailESenha(loginDTO.Email!, loginDTO.Senha!, loginDTO.IdTipoUsuario.Value);

                if (usuarioBuscado == null)
                {
                    return NotFound("Email ou Senha inválidos!");
                }

                var Claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email!),
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Nome!),
                new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuarioNavigation!.Titulo),
            };

                //2 - definir a chave de acesso ao token
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("eventplus-chave-autenticacao-webapi-dev"));

                //3 - Definir as credencias do token (HEADER)
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //4 - Gerar token
                var token = new JwtSecurityToken
                (
                    //emissor do token
                    issuer: "api_eventplus",

                    //destinatário do token
                    audience: "api_eventplus",

                    //dados definidos nas claims(informações)
                    claims: Claims,

                    //tempo de expiração do token
                    expires: DateTime.Now.AddMinutes(5),

                    //credenciais do token
                    signingCredentials: creds
                );

                //5 - Retornar o token criado
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });


            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }
    }
}
