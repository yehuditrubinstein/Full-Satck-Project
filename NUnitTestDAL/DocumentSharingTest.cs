using DocumentContracts.DTO;
using DocumentContracts.Interfaces;
using DocumentSQLDALImpl;
using NUnit.Framework;
using System;

namespace NUnitTestDAL
{
    public class Tests
    {
        private IDocumentSharingDAL _DocumentSharingDAL;
        [SetUp]
        public void Setup()
        {
            _DocumentSharingDAL = new DocumentSharingDAL(new DocumentSharingSQLDAL());
        }

        [Test]
        public void AddSharing()
        {
            var req = new DocumentSharingRequest();
            req.DocID = new Guid("58EE74C3-0DFA-4BA9-A8F6-FAC6943DAF61") { };
            req.UserId = "ym5871816@gmail.com";

            _DocumentSharingDAL.AddSharing(req);
            Assert.Pass();
        }
    }

}