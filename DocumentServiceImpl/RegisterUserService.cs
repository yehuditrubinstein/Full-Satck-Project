using Contracts;
using DocumentContracts.DTO;
using DocumentContracts.DTO.Register;
using DocumentContracts.DTO.RegisterUser;
using DocumentContracts.Interfaces.Register;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentServiceImpl
{
    [Register(Policy.Transient, typeof(IRegisterUserService))]
    public class RegisterUserService : IRegisterUserService
    {
        private IRegisterUserDAL _DAL;
        public RegisterUserService(IRegisterUserDAL DAL)
        {
            _DAL = DAL;
        }

      

        public RegisterUserResponse Login(RegisterUserRequest request)
        {
            RegisterUserResponse retval = default;
            try
            {
                retval = _DAL.Login(request);
            }
            catch (Exception e)
            {
                //log
                retval = new RegisterUserResponseDontLogin();
            }
            return retval;
        }
        

        public RegisterUserResponse RegisterUser(RegisterUserRequest request)
        {
            RegisterUserResponse retval = default;
            try
            {
                retval = _DAL.RegisterUser(request);
            }
            catch (Exception e)
            {
                //log
                retval = new RegisterUserResponseDontAdd();
            }
            return retval;
        }
    }
}
