using DocumentContracts.DTO;
using DocumentContracts.DTO.DocumentSharing;
using DocumentContracts.Interfaces;
using DocumentSQLDALImpl;
using NUnit.Framework;
using SQLServerInfraDAL;
using System;

namespace NUnitTestDAL
{
    public class Tests
    {
        private IDocumentSharingDAL _DocumentSharingDAL;
        [SetUp]
        public void Setup()
        {
            _DocumentSharingDAL = new DocumentSharingDAL(new SQLDAL());
        }

        [Test]
        public void AddSharing()
        {
            var req = new DocumentSharingRequest();
            req.sharingDTO = new SharingDTO();
            req.sharingDTO.DocID = new Guid("58EE74C3-0DFA-4BA9-A8F6-FAC6943DAF61") { };
            req.sharingDTO.UserId = "ym5871816@gmail.com";
           
             var res=_DocumentSharingDAL.AddSharing(req);
            Assert.IsInstanceOf(typeof(DocumentSharingResponseAddOK),res);
        }
        [Test]
        public void removeSharing()
        {

            var req = new DocumentSharingRequest();
            req.sharingDTO = new SharingDTO();
            req.sharingDTO.DocID = new Guid("58EE74C3-0DFA-4BA9-A8F6-FAC6943DAF61") { };
            req.sharingDTO.UserId = "ym5871816@gmail.com";

           var res=_DocumentSharingDAL.RemoveSharing(req);
            Assert.IsInstanceOf(typeof(DocumentSharingResponseRemoveOK),res);
        }
    }

}