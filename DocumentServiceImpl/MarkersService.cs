using Contracts;
using Contracts.DTO;
using DocumentContracts.DTO;
using DocumentContracts.DTO.Markers;
using DocumentContracts.Interfaces.Markers;
using DocumentDALImpl;
using System;
using System.Collections.Generic;
using System.Text;
namespace DocumentServiceImpl
{
    [Register(Policy.Transient, typeof(IMarkersService))]
    public class MarkersService : IMarkersService
    {
        IMarkersDAL _dal;
        public MarkersService(IMarkersDAL dal)
        {
            _dal = dal;
        }

        public MarkerRsponse AddMarker(MarkerRequestAdd request)
        {
            MarkerRsponse response=default;
            try
            {
                //if available
              response= _dal.AddMarker(request);

            }
            catch (Exception e)
            {
                //log
                response = new MarkerRsponseDontAdd();
                throw;
            }
            return response;
        }
        public MarkerRsponse RemoveMarker(MarkerRequestRemove request)
        {
            MarkerRsponse response = default;
            try
            {
               response= _dal.RemoveMarker(request);
            }
            catch (Exception e)
            {
                //log
                response = new MarkerRsponseDontRemove();
                throw;
            }
            return response;
        }
      
    }
}

