using DalParametersConverter;
using InfraDALContracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace DalParametersConverterExpression
{
    public class DBParameterConverter
    {

        IInfraDal _dal;
        static ConcurrentDictionary<Type, Dictionary<string, IGetter>> _getters =
            new ConcurrentDictionary<Type, Dictionary<string, IGetter>>();
        public DBParameterConverter(IInfraDal dal)
        {
            _dal = dal;
        }
        public void RegisterTypes<T>() where T : class
        {
            MakeGetters<T>();
        }
        private Dictionary<string, IGetter> MakeGetters<T>() where T : class
        {
            var retval = new Dictionary<string, IGetter>();
            foreach (var property in typeof(T).GetProperties())
            {
                var attribute = property.GetCustomAttribute<DBParameterAttribute>();
                if (attribute != null)//This property is a parameter 
                {

                    /*var param = Expression.Parameter(dto.GetType());

                    var getter = Expression.Lambda<Func<object, object>>(
                        Expression.Convert(Expression.Property(param, property.Name), typeof(object)),
                        param
                        ).Compile();*/
                    var getter = new Getter<T>(property.Name);

                    retval.Add(property.Name, getter);
                }
            }
            return retval;

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
                        retval = _dal.GetParameter(parameterName, paramValue);
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

        //Expression 
        public IDBParameter[] ConvertToParameters2<T>(T dto) where T : class
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
            if (!_getters.ContainsKey(dto.GetType()))
            {
                var getters = MakeGetters<T>();
                _getters.TryAdd(dto.GetType(), getters);


            }
            foreach (var paramName in _getters[dto.GetType()].Keys)
            {
                var parameter = _dal.GetParameter(paramName,
                    _getters[dto.GetType()][paramName].call(dto));
                retval.Add(parameter);
            }
            return retval.ToArray();
        }
    }
}
