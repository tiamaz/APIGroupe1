using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Projet.API.Dto;
using Projet.API.Model;

namespace Projet.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;

        private readonly IConfiguration _config;

        public AuthController(UserManager<User> userManager, 
        IMapper mapper, RoleManager<Role> roleManager,
        SignInManager<User> signInManager,
        IConfiguration config )
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            /// Data Transfer Object
            
            /// AutoMapper

            var roles = new List<Role> {
                new Role{Name= "Prof"},
                new Role{Name = "Etudiant"}
            };

            foreach(var role in roles)
            {
                await _roleManager.CreateAsync(role);
            }

            var user = _mapper.Map<User>(userForRegisterDto);

          var result =   await _userManager.CreateAsync(user, userForRegisterDto.password);
          if(result.Succeeded)
          {
              if(user.Status =="prof")
              {
                await _userManager.AddToRoleAsync(user, roles[0].Name); 
              }
              else{
                  await _userManager.AddToRoleAsync(user, roles[1].Name); 
              }
              
              return Ok("L'enregistrement réussi");
          }
          return BadRequest(result.Errors);

        }
        
    [HttpPost("login")]
       public async Task<IActionResult> Login(UserForLoginDto userForLogin)
        {
            var user = await _userManager.FindByNameAsync(userForLogin.UserName);

            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLogin.password,false);


            if(result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                    //// Créer un JWT : Json Web Token
                return Ok(
                    new {
                        token = GenerateJwtToken(user, roles)
                    }
                    );
            }

            return BadRequest("username ou password sont erronés");

        }

        [Authorize]
        [HttpGet("salim")]
        public IActionResult hello(){
            return Ok("Bonjour 4IIR1");
        }
 
        
          private async Task<string> GenerateJwtToken(User user, IList<string> roles){

                //// Définir les informations à transmettre
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)     
                };

                foreach(var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                //// Choisir le code et le crypter
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

                    ////Décrir le Token
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = creds,
                Expires = DateTime.Now.AddDays(1)
            };

                    /// Ajouter un porteur de mon token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

                    ////Ecrire mon Token
            return tokenHandler.WriteToken(token);


        } 


    }

   
}