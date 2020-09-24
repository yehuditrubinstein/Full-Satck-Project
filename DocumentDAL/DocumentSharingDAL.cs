using DocumentContracts.Interfaces;
using DocumentContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using InfraDALContracts;
using DocumentContracts.DTO.DocumentSharing;
using SQLServerInfraDAL;
using DalParametersConverter;
using DalParametersConverterExpression;
using System.Data;
using System.Linq;

namespace DocumentSQLDALImpl
{
    [Register(Policy.Transient, typeof(IDocumentSharingDAL))]
    public class DocumentSharingDAL : IDocumentSharingDAL
    {
        private IInfraDal _SQLDAL;
        DBParameterConverter _paramConverter;
        private IDBConnection con;
      
        public DocumentSharingDAL(IInfraDal SQLDAL)
        {
           
            _SQLDAL = SQLDAL;
            _paramConverter = new DBParameterConverter(_SQLDAL);
            con = _SQLDAL.Connect("Server=LAPTOP-B6F4SVRM;Database=DocumentProject;" + "Trusted_Connection=True;");

        }
        public DocumentsharingResponse AddSharing(DocumentSharingRequest request)
        {
            DocumentsharingResponse retval = default;
            try
            {
                var con = _SQLDAL.Connect("Server=LAPTOP-B6F4SVRM;Database=DocumentProject;" + "Trusted_Connection=True;");
                var parameters = _paramConverter.ConvertToParameters(request.sharingDTO);
                var dataset = _SQLDAL.ExecSPQuery("CreateShare", con, parameters);

                if (dataset != null)
                {
                    retval = new DocumentSharingResponseAddOK();
                }
            }
            catch (Exception e)
            {
                retval = new DocumentSharingResponseDontAdd();
                //log
            }
            return retval;
        }
        public DocumentsharingResponse RemoveSharing(DocumentSharingRequest request)
        {
            DocumentsharingResponse retval = default;

            try
            {
                var parameters = _paramConverter.ConvertToParameters(request.sharingDTO);
                var dataset = _SQLDAL.ExecSPQuery("DeleteShare", con, parameters);
                if (dataset != null)
                {
                    retval = new DocumentSharingResponseRemoveOK();
                }
            }
            catch (Exception e)
            {
                retval = new DocumentSharingResponseDontRemove();
            }
            return retval;
        }
        public IDBParameter GetParameter(string parameterName, object paramValue)
        {
            IDBParameter param = new SqlParameterAdapter() { ParameterName = parameterName, Value = paramValue };
            return param;
        }
        public DocumentsharingResponse GetShareForDoc(Guid DocID)
        {
            DocumentsharingResponse res = default;
            try
            {
                var con = _SQLDAL.Connect("Server=LAPTOP-B6F4SVRM;Database=DocumentProject;" + "Trusted_Connection=True;");
                var param = GetParameter("DocID", DocID);
                var dataset = _SQLDAL.ExecSPQuery("GetShareForDoc", con, param);
                if (dataset.Tables[0].Rows.Count != 0)
                {
                    res = new DocumentsharingResponse()
                    {
                        DocumentSharingDTO = new List<DocumentSharingDTO>()

                    };
                    var sharesList = dataset.Tables[0].AsEnumerable().Select(dataRow => new DocumentSharingDTO
                    {
                        DocID = dataRow.Field<Guid>("DocID"),
                        UserId = dataRow.Field<string>("UserId")

                    }).ToList();
                    res.DocumentSharingDTO = sharesList;
                 
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return res;

        }
        public DocumentsharingResponse GetShareForUser(string userID)
        {
            DocumentsharingResponse retval = default;
            var param = _SQLDAL.GetParameter("userID", userID);
            var dataset = _SQLDAL.ExecSPQuery("GetShareForUser", con, param);
            try
            {
                if (dataset.Tables[0].Rows.Count != 0)
                {
                    retval = new DocumentsharingResponse()
                    {
                        DocumentSharingDTO = new List<DocumentSharingDTO>()

                    };
                    var docShareList = dataset.Tables[0].AsEnumerable().Select(dataRow => new DocumentSharingDTO
                    {
                        DocID = dataRow.Field<Guid>("DocID"),
                        UserId = dataRow.Field<string>("userId")

                    }).ToList();
                    retval.DocumentSharingDTO = docShareList;
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return retval;
        }
    }
}
