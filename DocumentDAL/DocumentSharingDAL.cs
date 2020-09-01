using DocumentContracts.Interfaces;
using DocumentContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using InfraDALContracts;
using DalParametersConverter;
using DocumentContracts.DTO.DocumentSharing;
using SQLServerInfraDAL;

namespace DocumentSQLDALImpl
{
    [Register(Policy.Transient, typeof(IDocumentSharingDAL))]
    public class DocumentSharingDAL : IDocumentSharingDAL
    {
        private IInfraDal _SQLDAL;
        DBParameterConverter _paramConverter;
        public DocumentSharingDAL(IInfraDal SQLDAL)
        {
            _SQLDAL = SQLDAL;
            _paramConverter = new DBParameterConverter(_SQLDAL);
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
                var con = _SQLDAL.Connect("Server=LAPTOP-B6F4SVRM;Database=DocumentProject;" + "Trusted_Connection=True;");
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


    }
}
