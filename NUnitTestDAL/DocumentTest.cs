using DocumentContracts.DTO.Document;
using DocumentContracts.Interfaces;
using DocumentDALImpl;
using DocumentSQLDALImpl;
using NUnit.Framework;
using SQLServerInfraDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestDAL
{
    class DocumentTest
    {
        private IDocumentDAL _DocumentDAL;

        [SetUp]
        public void Setup()
        {
            _DocumentDAL = new DocumentDAL(new SQLDAL(),new DocumentSharingDAL(new SQLDAL()));
        }
        [Test]
        public void AddDocument()
        {
            var req = new DocumentRequest();
            req.documentDTO = new DocumentDTO();
            req.documentDTO.UserID = "ym5871816@gmail.com";
            req.documentDTO.ImageURL = "c/user/..."; 
            req.documentDTO.DocName = "yehudit";


            var res = _DocumentDAL.AddDocument(req);
            Assert.IsInstanceOf(typeof(DocumentResponseAddOK), res);
        }
        [Test]
        public void GetDocument()
        {
            var res = _DocumentDAL.GetDocument(new Guid("58EE74C3-0DFA-4BA9-A8F6-FAC6943DAF61") { });
            Assert.IsInstanceOf(typeof(DocumentResponse), res);
        }
        [Test]
        public void RemoveDocument()
        {
            var res = _DocumentDAL.Removedocument(new Guid("4AEF93E8-05E9-4C72-8FAE-76C197DE11B3") { });
            Assert.IsInstanceOf(typeof(DocumentResponseRemoveOK), res);
        }
    }
}
