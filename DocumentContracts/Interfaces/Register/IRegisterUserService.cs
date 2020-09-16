using DocumentContracts.DTO.Register;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.Interfaces.Register
{
    public interface IRegisterUserService
    {
        RegisterUserResponse RegisterUser(RegisterUserRequest registerUserRequest);
        RegisterUserResponse Login(RegisterUserRequest registerUserRequest);
    }
}
