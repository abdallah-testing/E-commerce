using E_CommerceSystem.DTO.Account;
using E_CommerceSystem.Models;
using E_CommerceSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_CommerceSystem.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository accountRepo;

        public AccountController(IAccountRepository accountRepo)
        {
            this.accountRepo = accountRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Register user)
        {
            if (ModelState.IsValid)
            {
                User userFromDb = new User();
                userFromDb.Username = user.Username;
                userFromDb.Email = user.Email;
                userFromDb.Role = Role.Customer;
                userFromDb.CreatedAt = DateTime.Now;
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                userFromDb.Password = hashedPassword;

                await accountRepo.Create(userFromDb);
                return Ok("Registered Successfully !!");
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login user)
        {
            if (ModelState.IsValid)
            {
                User userFromDb = await accountRepo.FindByUsername(user.Username);
                if (userFromDb != null)
                {
                    bool found = await accountRepo.CheckPassword(userFromDb, user.Password);
                    if (found)
                    {
                        // token

                        List<Claim> userClaims = new List<Claim>();
                        userClaims.Add(new Claim(ClaimTypes.NameIdentifier, userFromDb.Id.ToString()));
                        userClaims.Add(new Claim(ClaimTypes.Name, userFromDb.Username));
                        userClaims.Add(new Claim(ClaimTypes.Role, userFromDb.Role.ToString()));
                        userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("e431f8ce4dcf-51dd52_79b1253#@d346129@#cf23"));
                        SigningCredentials signCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                        JwtSecurityToken myToken = new JwtSecurityToken
                        (
                            issuer: "https://localhost:7152/",
                            audience: "https://localhost:4200/",
                            claims: userClaims,
                            signingCredentials: signCred,
                            expires: DateTime.Now.AddDays(1)
                        );


                        return Ok(new
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(myToken),
                            Expiration = DateTime.Now.AddDays(1)
                        });
                    }
                }
                ModelState.AddModelError("userInfo", "Wrong Username or password!");
            }
            return BadRequest(ModelState);
        }


        [HttpGet("profile")]
        [Authorize]
        public IActionResult GetProfile()
        {
            Claim claimId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            User userFromDb = accountRepo.Profile(Convert.ToInt32(claimId.Value));

            GetUser getUser = new GetUser();
            getUser.Id = userFromDb.Id;
            getUser.Username = userFromDb.Username;
            getUser.Email = userFromDb.Email;
            getUser.Role = userFromDb.Role.ToString();
            getUser.CreatedAt = userFromDb.CreatedAt;
            return Ok(getUser);
        }
    }
}
