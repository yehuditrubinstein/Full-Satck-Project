using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DalParametersConverterExpression
{  interface IGetter
    {
        public object call(object param);
    }
    class Getter<T> : IGetter where T : class
    {
 
        Func<T, object> _getter;
        public Getter(string propName)
        {
            var param = Expression.Parameter(typeof(T));

            _getter = Expression.Lambda<Func<T, object>>(
                Expression.Convert(Expression.Property(param, propName), typeof(object)),
               param
                ).Compile();

        }

        public object call(object param)
        {
            return _getter(param as T);
        }
    }
}
