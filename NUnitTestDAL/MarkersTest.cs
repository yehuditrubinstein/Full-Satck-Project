using DocumentContracts.DTO.Markers;
using DocumentContracts.Interfaces.Markers;
using DocumentDALImpl;
using DocumentSQLDALImpl;
using NUnit.Framework;
using SQLServerInfraDAL;
using System;

namespace NUnitTestDAL
{
    class MarkersTest
    {
        private IMarkersDAL _MarkerDal;

        [SetUp]
        public void Setup()
        {
            _MarkerDal = new MarkersDAL(new SQLDAL());
        }
        [Test]
        public void AddMarker()
        {
            var req = new MarkerRequestAdd();
            req.MarkerDTO = new MarkerDTO();
            req.MarkerDTO.DocID = new Guid("58EE74C3-0DFA-4BA9-A8F6-FAC6943DAF61") { };
            req.MarkerDTO.userId = "ym5871816@gmail.com";
            req.MarkerDTO.Fore_backColor = "green";
            req.MarkerDTO.MarkerLocation1X = 1;
            req.MarkerDTO.MarkerLocation1Y = 2;
            req.MarkerDTO.MarkerLocation2X = 3;
            req.MarkerDTO.MarkerLocation2Y = 4;
            req.MarkerDTO.MarkerType = "ellipse";
            

            var res = _MarkerDal.AddMarker(req);
            Assert.IsInstanceOf(typeof(MarkersResponseAddOK), res);
        }
        [Test]
        public void RemoveMarker()
        {
            var req = new MarkerRequestRemove();
            req.MarkerId = new Guid("58EE74C3-0DFA-4BA9-A8F6-FAC6943DAF61") { };


            var res = _MarkerDal.RemoveMarker(req);
            Assert.IsInstanceOf(typeof(MarkerResponseRemoveOk), res);
        }

    }
}
