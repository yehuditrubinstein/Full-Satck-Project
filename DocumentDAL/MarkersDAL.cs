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
                if (dataset.Tables[0].Rows.Count!=0)
                {
                    var markers = new List<MarkerDTO>();
                    markers.Add(new MarkerDTO()
                    {
                        BackColor = request.MarkerDTO.BackColor,
                        CenterX = request.MarkerDTO.CenterX,
                        CenterY = request.MarkerDTO.CenterY,
                        DocID = request.MarkerDTO.DocID,
                        ForeColor = request.MarkerDTO.ForeColor,
                        MarkerID = dataset.Tables[0].Rows[0].Field<Guid>("MarkerID"),
                        MarkerType = request.MarkerDTO.MarkerType,
                        RadiusX = request.MarkerDTO.RadiusX,
                        RadiusY = request.MarkerDTO.RadiusY,
                        userId = request.MarkerDTO.userId
                    });
                    response = new MarkersResponseAddOK() {Markers=markers };
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
                var parameters = _paramConverter.ConvertToParameter(request, "MarkerId");
                var dataset = _SQLdal.ExecSPQuery("RemoveMarker", con, parameters);
                if (dataset.Tables[0].Rows.Count!=0)
                {
                    var markers = new List<MarkerDTO>();
                    markers.Add(new MarkerDTO() { MarkerID = request.MarkerId });
                    response = new MarkerResponseRemoveOk() { Markers=markers};
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
                var parameter = _paramConverter.ConvertToParameter(request, "DocId");

                if (parameter != null)
                {
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
                            userId = dataRow.Field<string>("userId"),
                            MarkerID = dataRow.Field<Guid>("MarkerID")
                        }).ToList();
                        retval.Markers = markersList;
                    }
                }
            }
            catch (Exception e)
            {
                //log
                throw;
            }
            return retval;
        }
        public MarkerRsponse GetMarkerByID(RequestGetMarkers request)
        {
            MarkerRsponse retval = default;
            try
            {
                var con = _SQLdal.Connect("Server=LAPTOP-B6F4SVRM;Database=DocumentProject;" + "Trusted_Connection=True;");
                var parameter = _paramConverter.ConvertToParameter(request, "DocId");
                parameter.ParameterName = "MarkerID";
                if (parameter != null)
                {
                    var dataset = _SQLdal.ExecSPQuery("GetMarkerByID", con, parameter);
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
                            userId = dataRow.Field<string>("userId"),
                            MarkerID = dataRow.Field<Guid>("MarkerID")
                        }).ToList();
                        retval.Markers = markersList;
                    }
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
