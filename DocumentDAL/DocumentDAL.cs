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
        private IDocumentSharingDAL _DocumentSharingDAL;
        private IInfraDal _SQLDAL;
        DBParameterConverter _paramConverter;
        private IDBConnection con;
        public DocumentDAL(IInfraDal SQLDAL, IDocumentSharingDAL DocumentSharingDAL)
        {
            _DocumentSharingDAL = DocumentSharingDAL;
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
                        DocID = dataset.Tables[0].Rows[0].Field<Guid>("DocID"),
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
                    var docList = dataset.Tables[0].AsEnumerable().Select(dataRow => new DocumentDTO
                    {
                        DocName = dataRow.Field<string>("DocName"),
                        ImageURL = dataRow.Field<string>("ImageURL"),
                        UserID = dataRow.Field<string>("UserID"),
                        DocID = dataRow.Field<Guid>("DocID")
                    }).ToList();
                    retval.documentDTO = docList;
                    var sharing = _DocumentSharingDAL.GetShareForUser(userID);
                    List<DocumentDTO> sharedDocs = new List<DocumentDTO>();
                    if (sharing!=null&&sharing.DocumentSharingDTO.Count!=0)
                    {
                        foreach (var item in sharing.DocumentSharingDTO)
                        {
                            var doc = GetDocument(item.DocID);
                            sharedDocs.Add(doc.documentDTO[0]);
                        }
                        retval.documentDTO.AddRange(sharedDocs);
                    }

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
