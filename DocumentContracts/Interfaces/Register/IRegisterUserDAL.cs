using DocumentContracts.DTO.Register;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.Interfaces.Register
{
    public interface IRegisterUserDAL
    {
        RegisterUserResponse RegisterUser(RegisterUserRequest request);
        RegisterUserResponse Login(RegisterUserRequest request);
    }
}
