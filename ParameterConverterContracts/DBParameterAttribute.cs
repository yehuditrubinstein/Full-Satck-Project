using System;
using System.Collections.Generic;
using System.Text;

namespace DalParametersConverter
{
    public class DBParameterAttribute : Attribute
    {
        public string ParameterName { get;  }
        public DBParameterAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }
    }
}
