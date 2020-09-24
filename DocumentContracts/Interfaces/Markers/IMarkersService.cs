using DocumentContracts.DTO;
using DocumentContracts.DTO.Markers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.Interfaces.Markers
{
    public interface IMarkersService
    {
        MarkerRsponse AddMarker(MarkerRequestAdd request);
        MarkerRsponse RemoveMarker(MarkerRequestRemove request);
        MarkerRsponse GetMarkers(RequestGetMarkers request);
    }
}
