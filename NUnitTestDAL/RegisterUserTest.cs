using DocumentContracts.DTO.Register;
using DocumentContracts.Interfaces.Register;
using DocumentDALImpl;
using DocuusmentDALImpl;
using NUnit.Framework;
using SQLServerInfraDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestDAL
{
    class RegisterUserTest
    {

        private IRegisterUserDAL _registerDal;

        [SetUp]
        public void Setup()
        {
            _registerDal = new RegisterUserDAL(new SQLDAL());
        }
        [Test]
        public void Register()
        {
            var req = new RegisterUserRequest();
            req.UserDTO = new UserDTO();
            req.UserDTO.UserID = "ym5871816@gmsil.com";
            req.UserDTO.UserName = "yehudit";
            var res=_registerDal.RegisterUser(req);
            Assert.IsInstanceOf(typeof(RegisterUserExistsResponse), res);
        }
        public void Login()
        {
            var req = new RegisterUserRequest();
            req.UserDTO = new UserDTO();
            req.UserDTO.UserID = "ym5871816@gmsil.com";
            req.UserDTO.UserName = "yehudit";
            var res = _registerDal.Login(req);
            Assert.IsInstanceOf(typeof(RegisterUserExistsResponse), res);
        }
    }
}
