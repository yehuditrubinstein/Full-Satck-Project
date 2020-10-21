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
            List<DocumentSharingDTO> shared=default ;
            List<string> mylist =new List<string>();

            try
            {
                response = _dal.AddMarker(request);

                shared=_documentSharingService.GetShareForDoc(new DocumentSharingRequestGetForDoc() { DocID=request.MarkerDTO.DocID}).DocumentSharingDTO;
                if (shared != null)
                {
                    //create list type string for send to all
                    shared.ForEach(s => mylist.Add(s.UserId));
                    //add the usrid of the usr whose document he owns
                   
                    mylist.Remove(request.MarkerDTO.userId);
                    _messanger.SendMarkerToAll(mylist,response);
                }
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
            List<DocumentSharingDTO> shared = default;
            List<string> mylist = new List<string>();
            MarkerRsponse response = default;
            try
            {
                response = _dal.RemoveMarker(request);
                shared = _documentSharingService.GetShareForDoc(new DocumentSharingRequestGetForDoc() { DocID = request.DocID }).DocumentSharingDTO;
                if (shared != null)
                {
                    shared.ForEach(s => mylist.Add(s.UserId));
                   
                    mylist.Remove(request.UserID);
                    _messanger.SendMarkerToAll(mylist,response);
                }
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

