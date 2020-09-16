using DocumentContracts.DTO.Document;
using DocumentContracts.Interfaces.Document;
using DocumentDALImpl;
using DocumentServiceImpl;
using NUnit.Framework;
using SQLServerInfraDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestBLL
{
    class DocumentTest
    {
        private IDocumentSerivce service;
        [SetUp]
        public void Setup()
        {
            service = new DocumentService(new DocumentDAL(new SQLDAL()));
        }

        [Test]
        public void getAllforuser()
        {
            var res = service.GetDocumentForUser("ym5871816@gmail.com");
            Assert.IsInstanceOf(typeof(DocumentResponse), res);
        }
        [Test]
        public void adduser()
        {
            var req = new DocumentRequest();
            req.documentDTO = new DocumentDTO() {  DocName="qqq",ImageURL="c/c",UserID="ym5871816@gmail.com"};
           var res= service.AddDocument(req);
            Assert.IsInstanceOf(typeof(DocumentResponseAddOK), res);
        }
    }
}
