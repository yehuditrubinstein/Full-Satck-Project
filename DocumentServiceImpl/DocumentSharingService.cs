using Contracts;
using DocumentContracts.DTO;
using DocumentContracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentServiceImpl
{
    [Register(Policy.Transient, typeof(IDocumentSharingService))]
   public class DocumentSharingService : IDocumentSharingService
    {

        IDocumentSharingSQLDAL _dal;
        public DocumentSharingService(IDocumentSharingSQLDAL dal)
        {
            _dal = dal;
        }
        public Response AddSharing(DocumentSharingRequest request)
        {

            try
            {
                Response retval = null;
                if (SharingAvailable(request))
                {
                    _dal.AddSharing(request);
                    retval = new DocumentSharingResponseAddOK();
                }
                else
                {
                    retval = new ResponseError();
                }
                return retval;
            }
            catch (Exception e)
            {
                
                throw;
            }

        }
        private bool SharingAvailable(DocumentSharingRequest req)
        {
            //
            ///  לבנתיים
            //
            return true;
        }
    }
}
