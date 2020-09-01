using Contracts;
using DocumentContracts.DTO;
using DocumentContracts.DTO.Markers;
using DocumentContracts.Interfaces.Markers;
using InfraDALContracts;
using SQLServerInfraDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentSQLDALImpl
{
    [Register(Policy.Transient, typeof(IMarkersSQLDAL))]
    public class MarkersSQLDAL 
    {
        IInfraDal dal = new SQLDAL();

        public Response Exec(IDBConnection con, string SP,params IDBParameter[] parameters)
        {
            Response response = default;
            try
            {
                var dataset = dal.ExecSPQuery(SP, con, parameters);
                if (dataset != null)
                {
                    response = new ResponseOK(); 
                }
                //IDBParameter p1 = new SqlParameterAdapter();
                //IDBParameter p2 = new SqlParameterAdapter();
                //IDBParameter p3 = new SqlParameterAdapter();
                //IDBParameter p4 = new SqlParameterAdapter();
                //IDBParameter p5 = new SqlParameterAdapter();
                //IDBParameter p6 = new SqlParameterAdapter();
                //IDBParameter p7 = new SqlParameterAdapter();
                //IDBParameter p8 = new SqlParameterAdapter();


                //p1.ParameterName = "DocID";
                //p1.Value = request.DocID;

                //p2.ParameterName = "MarkerType";
                //p2.Value = request.MarkerType;

                //p3.ParameterName = "MarkerLocation1X";
                //p3.Value = request.MarkerLocation1X;

                //p4.ParameterName = "MarkerLocation1Y";
                //p4.Value = request.MarkerLocation1Y;

                //p5.ParameterName = "MarkerLocation2X";
                //p5.Value = request.MarkerLocation2X;

                //p6.ParameterName = "MarkerLocation2Y";
                //p6.Value = request.MarkerLocation2Y;

                //p7.ParameterName = "Fore_backColor";
                //p7.Value = request.Fore_backColor;

                //p8.ParameterName = "userId";
                //p8.Value = request.userId;

                 //con = dal.Connect("Server=LAPTOP-B6F4SVRM;Database=DocumentProject;" +
                 //             "Trusted_Connection=True;");

                //var dataset = dal.ExecSPQuery("CreateMarker", con, p1, p2, p3, p4, p5, p6, p7, p8);
                //if (dataset != null)
                //{
                //    response = new MarkersResponseAddOK();
                //}
            }
            catch (Exception e)
            {
                response = new ResponseError();

            }
            return response;
        }

        //public Response RemoveMarker(RemoveMarkerRequest request)
        //{
        //    Response response = default;

        //    try
        //    {

        //        IDBParameter p1 = new SqlParameterAdapter();
        //        p1.ParameterName = "MarkerId";
        //        p1.Value = request.MarkerId;
        //        var con = dal.Connect("Server=LAPTOP-B6F4SVRM;Database=DocumentProject;" +
        //                 "Trusted_Connection=True;");

        //        var dataset = dal.ExecSPQuery("RemoveMarker", con, p1);
        //        if (dataset != null)
        //        {
        //            response = new MarkerRemoveOkResponse();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        response = new ResponseError();
        //    }
        //    return response;
        //}
    }
}





