using Contracts;
using DocumentContracts.DTO;
using DocumentContracts.DTO.DocumentSharing;
using DocumentContracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentServiceImpl
{
    [Register(Policy.Transient, typeof(IDocumentSharingService))]
    public class DocumentSharingService : IDocumentSharingService
    {

        IDocumentSharingDAL _dal;
        public DocumentSharingService(IDocumentSharingDAL dal)
        {
            _dal = dal;
        }
        public DocumentsharingResponse AddSharing(DocumentSharingRequest request)
        {
            DocumentsharingResponse retval = default;
            try
            {

                if (SharingAvailable(request))
                {

                    retval = _dal.AddSharing(request);
                }
                else
                {
                    retval = new DocumentSharingResponseDontAdd();
                }

            }
            catch (Exception e)
            {
                //  Console.log(e)
                retval = new DocumentSharingResponseDontAdd();
            }
            return retval;
        }

        public DocumentsharingResponse RemoveSharing(DocumentSharingRequest request)
        {
            DocumentsharingResponse retval = default;

            try
            {
                retval = _dal.RemoveSharing(request);

            }
            catch (Exception e)
            {
                retval = new DocumentSharingResponseDontRemove();
                throw;
            }
            return retval;
        }

        private bool SharingAvailable(DocumentSharingRequest req)
        {
            //אם יש יוזר אם יש דוקיומנט
            //
            ///  לבנתיים
            //
            return true;
        }
        public DocumentsharingResponse GetShareForDoc(DocumentSharingRequestGetForDoc request)
        {
            DocumentsharingResponse retval = default;
            try
            {
                if (request.DocID != Guid.Empty && request.DocID != null)
                    retval = _dal.GetShareForDoc(request);
            }
            catch (Exception e)
            {

                throw;
            }
            return retval;
        }
    }
}
