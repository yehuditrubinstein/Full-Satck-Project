using Contracts;
using DalParametersConverterExpression;
using DocumentContracts.DTO.Document;
using DocumentContracts.Interfaces;
using InfraDALContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DocumentDALImpl
{
    [Register(Policy.Transient, typeof(IDocumentDAL))]
    public class DocumentDAL : IDocumentDAL
    {
        private IInfraDal _SQLDAL;
        DBParameterConverter _paramConverter;
        private IDBConnection con;
        public DocumentDAL(IInfraDal SQLDAL)
        {
            _SQLDAL = SQLDAL;
            _paramConverter = new DBParameterConverter(_SQLDAL);
            con = _SQLDAL.Connect("Server=LAPTOP-B6F4SVRM;Database=DocumentProject;" + "Trusted_Connection=True;");
        }
        public DocumentResponse AddDocument(DocumentRequest req)
        {
            DocumentResponse retval = default;
            try
            {
                var parameters = _paramConverter.ConvertToParameters(req.documentDTO);
                var dataset = _SQLDAL.ExecSPQuery("CreateDocument", con, parameters);

                if (dataset != null)
                {
                    retval = new DocumentResponseAddOK();
                }
            }
            catch (Exception e)
            {
                retval = new DocumentResponseDontAdd();
                //log
            }
            return retval;
        }

        public DocumentResponse GetDocument(Guid docID)
        {

            DocumentResponse retval = default;
            try
            {
                var param = _SQLDAL.GetParameter("DocID", docID);
                var dataset = _SQLDAL.ExecSPQuery("GetDocument", con, param);
                if (dataset.Tables[0].Rows.Count != 0)
                {

                    retval = new DocumentResponse()
                    {
                        documentDTO = new List<DocumentDTO>()

                    };
                    retval.documentDTO.Add(new DocumentDTO()
                    {
                        DocName = dataset.Tables[0].Rows[0].Field<string>("DocName"),
                        ImageURL = dataset.Tables[0].Rows[0].Field<string>("ImageURL"),
                        UserID = dataset.Tables[0].Rows[0].Field<string>("UserId")
                    });
                }
            }
            catch (Exception e)
            {

                //log
            }

            return retval;
        }

        public DocumentResponse GetDocumentsForUser(string userID)
        {
            DocumentResponse retval = default;
            try
            {
                var param = _SQLDAL.GetParameter("UserID", userID);
                var dataset = _SQLDAL.ExecSPQuery("GetDocumentsForUser", con, param);
                if (dataset.Tables[0].Rows.Count != 0)
                {

                    retval = new DocumentResponse()
                    {
                        documentDTO = new List<DocumentDTO>()

                    };
                    var empList = dataset.Tables[0].AsEnumerable().Select(dataRow => new DocumentDTO
                    {
                        DocName = dataRow.Field<string>("DocName"),
                        ImageURL = dataRow.Field<string>("ImageURL"),
                        UserID = dataRow.Field<string>("UserID")
                    }).ToList<DocumentDTO>();
                    retval.documentDTO = empList;

                }
                else
                {
                    retval = new DocumentResponseEmpty();
                }
            }
            catch (Exception e)
            {
              
                //log
            }

            return retval;
        }

        public DocumentResponse Removedocument(Guid docID)
        {
            DocumentResponse retval = default;
            try
            {
                var param = _SQLDAL.GetParameter("DocID", docID);
                var dataset = _SQLDAL.ExecSPQuery("RemoveDocument", con, param);
                retval = new DocumentResponseRemoveOK();
            }
            catch (Exception e)
            {
                retval = new DocumentResponseDontRemove();
                //log
            }
            return retval;
        }
    }
}
