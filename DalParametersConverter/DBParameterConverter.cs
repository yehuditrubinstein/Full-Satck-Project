using InfraDALContracts;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DalParametersConverter
{
    public class DBParameterConverter
    {
        IInfraDal _dal;
        public DBParameterConverter(IInfraDal dal)
        {
            _dal = dal;
        }
        public IDBParameter ConvertToParameter(object dto, string parameterName)
        {
            IDBParameter retval = null;
            foreach (var property in dto.GetType().GetProperties())
            {
                var attribute = property.GetCustomAttribute<DBParameterAttribute>();
                if (attribute != null)//This property is a parameter 
                {
                    if (attribute.ParameterName.Equals(parameterName))
                    {
                        var paramValue = property.GetValue(dto);
                        ///Create Parameter 
                        retval = _dal.GetParameter(parameterName,paramValue);
                        break;

                    }

                }
            }
            return retval;
        }
        public IDBParameter[] ConvertToParameters(object dto)
        {
           
            //Return value 
            List<IDBParameter> retval = new List<IDBParameter>();
            //for Each property in DTO check for DBParameter Atribute
            //Create parameter for that property 
            /*
                     * DTO EXample
                     *  [DBParameter("UserID")]
                        public string UserID { get; set; }
                        [DBParameter("UserName")]
                        public string UserName { get; set; }
                     */
            foreach (var property in dto.GetType().GetProperties())
            {
                var attribute = property.GetCustomAttribute<DBParameterAttribute>();
                if (attribute != null)//This property is a parameter 
                {
                    //get param name from attribute
                    var paramName = attribute.ParameterName;
                    //invoke GetValue for example: if propety is UserID the invoke will execute 
                    //paramValue = dto.UserID 
                    var paramValue = property.GetValue(dto);
                    ///Create Parameter 
                    var parameter = _dal.GetParameter(paramName, paramValue);
                    retval.Add(parameter);
                }

            }
            return retval.ToArray();
        }
    }
}
