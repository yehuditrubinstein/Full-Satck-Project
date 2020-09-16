using Contracts;
using DalParametersConverter;
using DalParametersConverterExpression;
using DocumentContracts.DTO;
using DocumentContracts.DTO.Markers;
using DocumentContracts.Interfaces.Markers;
using DocumentSQLDALImpl;
using InfraDALContracts;
using SQLServerInfraDAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
namespace DocumentDALImpl
{
    [Register(Policy.Transient, typeof(IMarkersDAL))]
    public class MarkersDAL : IMarkersDAL
    {
       // MarkersSQLDAL _SQLDAL;
        IInfraDal _SQLdal = new SQLDAL();
        DBParameterConverter _paramConverter;

        public MarkersDAL( IInfraDal dal)
        {
            _SQLdal = dal;
            _paramConverter = new DBParameterConverter(_SQLdal);
        }
        public MarkerRsponse AddMarker(MarkerRequestAdd request)
        {
            MarkerRsponse response = default;
            //------------
            try
            {
              var con = _SQLdal.Connect("Server=LAPTOP-B6F4SVRM;Database=DocumentProject;" +"Trusted_Connection=True;");
                var parameters =_paramConverter.ConvertToParameters(request.MarkerDTO);
                var dataset = _SQLdal.ExecSPQuery("CreateMarker", con, parameters);
                if (dataset != null)
                {
                    response = new MarkersResponseAddOK();
                }

            }
            catch (Exception e)
            {
                response = new MarkerRsponseDontAdd();
                throw;
            }
            return response;
        }
        public MarkerRsponse RemoveMarker(MarkerRequestRemove request)
        {
            MarkerRsponse response = default;
            try
            {
                var con = _SQLdal.Connect("Server=LAPTOP-B6F4SVRM;Database=DocumentProject;" + "Trusted_Connection=True;");
                var parameters = new DBParameterConverter(_SQLdal).ConvertToParameters(request);
                var dataset = _SQLdal.ExecSPQuery("RemoveMarker", con, parameters);
                if (dataset != null)
                {
                    response = new MarkerResponseRemoveOk();
                }
            }
            catch (Exception e)
            {
                response = new MarkerRsponseDontRemove();
                throw;
            }
            return response;
        }
    }
}
