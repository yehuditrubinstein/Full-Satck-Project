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
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
namespace DocumentDALImpl
{
    [Register(Policy.Transient, typeof(IMarkersDAL))]
    public class MarkersDAL : IMarkersDAL
    {

        IInfraDal _SQLdal = default;
        DBParameterConverter _paramConverter;

        public MarkersDAL(IInfraDal dal)
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
                var con = _SQLdal.Connect("Server=LAPTOP-B6F4SVRM;Database=DocumentProject;" + "Trusted_Connection=True;");
                var parameters = _paramConverter.ConvertToParameters(request.MarkerDTO);
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
        public MarkerRsponse GetMarkers(RequestGetMarkers request)
        {
            MarkerRsponse retval = default;
            try
            {
                var con = _SQLdal.Connect("Server=LAPTOP-B6F4SVRM;Database=DocumentProject;" + "Trusted_Connection=True;");
                //var parameter = new DBParameterConverter(_SQLdal).ConvertToParameter(request.DocId, "DocId");
                var parameter = _SQLdal.GetParameter("DocID", request.DocID);

                var dataset = _SQLdal.ExecSPQuery("GetMarkers", con, parameter);
                if (dataset.Tables[0].Rows.Count != 0)
                {
                    retval = new MarkerRsponse()
                    {
                        Markers = new List<MarkerDTO>()

                    };
                    var markersList = dataset.Tables[0].AsEnumerable().Select(dataRow => new MarkerDTO
                    {
                        CenterX = dataRow.Field<int>("CenterX"),
                        CenterY = dataRow.Field<int>("CenterY"),
                        RadiusX = dataRow.Field<int>("RadiusX"),
                        RadiusY = dataRow.Field<int>("RadiusY"),
                        ForeColor = dataRow.Field<string>("ForeColor"),
                        BackColor = dataRow.Field<string>("BackColor"),
                        MarkerType = dataRow.Field<string>("MarkerType"),
                        DocID = dataRow.Field<Guid>("DocID"),
                        userId= dataRow.Field<string>("userId")
                    }).ToList();
                    retval.Markers = markersList;
                }
            }
            catch (Exception e)
            {
                //log
                throw;
            }
            return retval;
        }
    }
}
