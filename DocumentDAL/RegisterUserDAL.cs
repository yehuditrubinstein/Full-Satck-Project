
using DocumentContracts.DTO.Register;
using DocumentContracts.Interfaces.Register;
using InfraDALContracts;
using SQLServerInfraDAL;
using Contracts;
using DalParametersConverter;
using DalParametersConverterExpression;

namespace DocuusmentDALImpl
{
    [Register(Policy.Singleton, typeof(IRegisterUserDAL))]
    public class RegisterUserDAL : IRegisterUserDAL
    {
        private IInfraDal _SQLDAL;
        private DBParameterConverter _DalParametersConverter;
        private IDBConnection con;
        public RegisterUserDAL(IInfraDal SQLDAL)
        {
            _SQLDAL = SQLDAL;
            _DalParametersConverter = new DBParameterConverter(new SQLDAL());
            con = _SQLDAL.Connect("Server=LAPTOP-B6F4SVRM;Database=DocumentProject;" + "Trusted_Connection=True;");
        }

       

        public RegisterUserResponse Login(RegisterUserRequest request)
        {
            RegisterUserResponse retval = default;
            var parameters = _DalParametersConverter.ConvertToParameters(request.UserDTO);
            var dataset = _SQLDAL.ExecSPQuery("LoginUser", con, parameters);
            if (dataset.Tables[0].Rows.Count != 0)
            {
                retval = new RegisterUserExistsResponse();
            }
            return retval;
        }

        public RegisterUserResponse RegisterUser(RegisterUserRequest request)
        {
            RegisterUserResponse retval = default;
            var user = Login(request);
            if (user == null)
            {
                var parameters = _DalParametersConverter.ConvertToParameters(request.UserDTO);
                var dataset = _SQLDAL.ExecSPQuery("CreateUser", con, parameters);
                if (dataset != null)
                {
                    retval = new RegisterUserResponseAddOK();
                }
            }
            else
            {
                retval = new RegisterUserExistsResponse();
            }
            return retval;

        }
    }
}
