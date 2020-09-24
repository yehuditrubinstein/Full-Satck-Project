using Contracts;
using Contracts.DTO;
using DocumentContracts.DTO;
using DocumentContracts.DTO.Document;
using DocumentContracts.DTO.DocumentSharing;
using DocumentContracts.DTO.Markers;
using DocumentContracts.Interfaces;
using DocumentContracts.Interfaces.Markers;
using DocumentDALImpl;
using MessangerContracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServiceImpl
{
    [Register(Policy.Transient, typeof(IMarkersService))]
    public class MarkersService : IMarkersService
    {
        IDocumentDAL _DocumentDAL;
        IMarkersDAL _dal;
        IMessanger _messanger;
        ILogger<IMarkersService> _logger;
        IDocumentSharingService _documentSharingService;
        public MarkersService(IMarkersDAL dal, IDocumentDAL DocumentDAL ,IMessanger messanger, IDocumentSharingService documentSharingService)
        {
            _DocumentDAL = DocumentDAL;
            _documentSharingService = documentSharingService;
            _dal = dal;
            _messanger = messanger;
        }

        public MarkerRsponse AddMarker(MarkerRequestAdd request)
        {
            MarkerRsponse response = default;
            try
            {
                //if available
   
                response = _dal.AddMarker(request);
                List<DocumentSharingDTO> shared = _documentSharingService.GetShareForDoc(request.MarkerDTO.DocID).DocumentSharingDTO;
                List<string> mylist = new List<string>();
                shared.ForEach(s => mylist.Add(s.UserId));
                DocumentDTO doc = _DocumentDAL.GetDocument(request.MarkerDTO.DocID).documentDTO[0];
                mylist.Add(doc.UserID);
                _messanger.SendMarkerToAll(mylist, new MarkerDTO() { 
                    DocID=request.MarkerDTO.DocID,
                    MarkerType=request.MarkerDTO.MarkerType,
                    userId=request.MarkerDTO.userId,
                    CenterX=request.MarkerDTO.CenterX,
                    CenterY=request.MarkerDTO.CenterY,
                    RadiusX=request.MarkerDTO.RadiusX,
                    RadiusY=request.MarkerDTO.RadiusY,
                    ForeColor=request.MarkerDTO.ForeColor,
                    BackColor=request.MarkerDTO.BackColor
                });
            }
            catch (Exception e)
            {
                //log e 
                response = new MarkerRsponseDontAdd();

            }
            return response;
        }
        public MarkerRsponse RemoveMarker(MarkerRequestRemove request)
        {
            MarkerRsponse response = default;
            try
            {
                response = _dal.RemoveMarker(request);
            }
            catch (Exception e)
            {
                //log
                response = new MarkerRsponseDontRemove();
                throw;
            }
            return response;
        }
        public MarkerRsponse GetMarkers(RequestGetMarkers request)
        {
            MarkerRsponse retval = default;
            try
            {
                if(request.DocID!=null)
                retval = _dal.GetMarkers(request);
            }
            catch (Exception e)
            {
                throw;
            }
            return retval;
        }
        
    }
}

