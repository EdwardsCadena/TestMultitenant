using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestProject.Api.Response;
using TestProject.Core.Entities;
using TestProject.Core.Interfaces;
using TestProject.Infrastructure.Interfaces;

namespace TestProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _maper;
        private readonly IPasswordService _passwordService;
        public UserController(IUserRepository userRepository, IMapper mapper, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _maper = mapper;
            _passwordService = passwordService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post(UserDTOs userDTOs)
        {
            var user = _maper.Map<User>(userDTOs);
            user.Password = _passwordService.Hash(user.Password);
            await _userRepository.RegisterUser(user);
            var response = new ApiResponse<UserDTOs>(userDTOs);
            return Ok(response);
        }
    }
}
