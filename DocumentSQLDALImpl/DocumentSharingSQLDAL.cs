using Contracts;
using DocumentContracts.DTO;
using DocumentContracts.DTO.DocumentSharing;
using DocumentContracts.Interfaces;
using InfraDALContracts;
using SQLServerInfraDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentSQLDALImpl
{
    [Register(Policy.Transient, typeof(IDocumentSharingSQLDAL))]
    public class DocumentSharingSQLDAL : IDocumentSharingSQLDAL
    {
        IInfraDal dal = new SQLDAL();


        public DocumentSharingSQLDAL()
        {
                
        }
        public Response AddSharing(DocumentSharingRequest request)
        {
            Response response = default;
            try
            {
                IDBParameter p1 = new SqlParameterAdapter();
                IDBParameter p2 = new SqlParameterAdapter();
                p1.ParameterName = "DocId";
                p1.Value = request.DocID;
                p2.ParameterName = "userId";
                p2.Value = request.UserId;
                var con = dal.Connect("Server=LAPTOP-B6F4SVRM;Database=DocumentProject;" +
                              "Trusted_Connection=True;");

                var dataset = dal.ExecSPQuery("CreateShare", con, p1, p2);
                if (dataset != null)
                {
                    response = new DocumentSharingResponseAddOK();
                }
            }
            catch (Exception e)
            {
                response = new ResponseError();

            }

            return response;
        }

        public Response RemoveSharing(DocumentSharingRequest request)
        {

            Response response = default;

            try
            {
                IDBParameter p1 = new SqlParameterAdapter();
                IDBParameter p2 = new SqlParameterAdapter();
                p1.ParameterName = "DocId";
                p1.Value = request.DocID;
                p2.ParameterName = "userId";
                p2.Value = request.UserId;
                var con = dal.Connect("Server=LAPTOP-B6F4SVRM;Database=DocumentProject;" +
                              "Trusted_Connection=True;");

                var dataset = dal.ExecSPQuery("DeleteShare", con, p1, p2);
                if (dataset != null)//איך יודעים אם הפעולה הצליחה
                {
                    response = new DocumentSharingResponseRemoveOK();
                }
            }
            catch (Exception)
            {
                response = new ResponseError();

            }
            return response;
        }
    }
}
