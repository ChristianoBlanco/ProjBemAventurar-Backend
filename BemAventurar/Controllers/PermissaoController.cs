using BemAventurar.DTO;
using BemAventurar.Models;
using BemAventurar.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BemAventurar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissaoController : ControllerBase
    {
        //Variáveis de controle 
        private readonly IPermissaoInterface _permissaoService;
        private readonly IConfiguration _configuration;
        private readonly IPermissaoInterface _permissaoInterface;

        public PermissaoController(IPermissaoInterface permissaoService, IConfiguration configuration, IPermissaoInterface permissaoInterface)
        {
            _permissaoService = permissaoService;
            _configuration = configuration;
            _permissaoInterface = permissaoInterface;
        }



    }
}
