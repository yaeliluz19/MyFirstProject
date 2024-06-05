using AutoMapper;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MyFirstProject.Controllers
{   
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        //UserService UserService = new UserService();
        // GET: UserController/Details/5
        private IUserService _UserService;
        private IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        

        public UserController(IUserService userService, IMapper mapper,ILogger<UserController> logger)
        {
            _UserService = userService;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        //[Route("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            try
            {
                return await _UserService.GetById(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        // POST: UserController/Create
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Register([FromBody] userDTO userDTO)
        {
            try
            { 
                User user1 = _mapper.Map<userDTO, User>(userDTO);
                User user= await _UserService.Register(user1);
                if (user != null)
                    return Ok(user);
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<User>> Login([FromBody] UserLoginDTO userLogin)
        {
            try
            {
                User user1 = _mapper.Map<UserLoginDTO, User>(userLogin);
                _logger.LogInformation($"Login attempted with User Name,{user1.Email} and password {user1.Password}");
                User user = await _UserService.Login(user1);
                if (user != null)
                    return Ok(user);
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser([FromBody]userDTO user,int id)
        {
            try
            {
                User user1 = _mapper.Map<userDTO, User>(user);
                User user2 = await _UserService.UpdateUser(user1,id);
                if (user2 != null)
                    return Ok(user2);
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("CheckPassword")]
        public ActionResult<int> CheckPassword([FromBody] string password)
        {
            try
            {
                return Ok(_UserService.CheckPassword(password));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
