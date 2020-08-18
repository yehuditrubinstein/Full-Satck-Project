using DocumentContracts.Interfaces;
using DocumentContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Contracts;

namespace DocumentSQLDALImpl
{
    [Register(Policy.Transient, typeof(IDocumentSharingDAL))]
    public class DocumentSharingDAL : IDocumentSharingDAL
    {
        private IDocumentSharingSQLDAL _DocumentSharingSQLDAL;
        public DocumentSharingDAL(IDocumentSharingSQLDAL DocumentSharingSQLDAL)
        {
            _DocumentSharingSQLDAL = DocumentSharingSQLDAL;
        }
       
        void IDocumentSharingDAL.AddSharing(DocumentSharingRequest request)
        {
            //    if( כזה מסמך יש  כזה יוזר יש)
              _DocumentSharingSQLDAL.AddSharing(request);

            
           
        }
    }
}
