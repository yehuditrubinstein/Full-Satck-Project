using DocumentContracts.DTO.Register;
using DocumentContracts.Interfaces.Register;
using DocumentServiceImpl;
using DocuusmentDALImpl;
using NUnit.Framework;
using SQLServerInfraDAL;

namespace NUnitTestBLL
{
    public class Tests
    {
        private IRegisterUserService service;
        [SetUp]
        public void Setup()
        {
            service = new RegisterUserService(new RegisterUserDAL(new SQLDAL()));
        }

        [Test]
        public void Login()
        {
            var req = new RegisterUserRequest();
            req.UserDTO = new UserDTO();
            req.UserDTO.UserID = "ym5871816@gmsil.com";
            req.UserDTO.UserName = "yehudit";
          var res = service.Login(req);
            
            Assert.IsInstanceOf(typeof(RegisterUserExistsResponse),res);
        }
    }
}