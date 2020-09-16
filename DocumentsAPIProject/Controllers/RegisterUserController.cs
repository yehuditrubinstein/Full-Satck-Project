using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentContracts.DTO.Register;
using DocumentContracts.Interfaces.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsAPIProject.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {
        private IRegisterUserService _Service;
        public RegisterUserController(IRegisterUserService service)
        {
            _Service = service;
        }
        [HttpPost]
        public RegisterUserResponse RegisterUser([FromBody] RegisterUserRequest registerUserRequest)
        {

            return _Service.RegisterUser(registerUserRequest);
        }
        [HttpPost]
        public RegisterUserResponse Login([FromBody] RegisterUserRequest registerUserRequest)
        {

            return _Service.Login(registerUserRequest);
        }
    }
}
