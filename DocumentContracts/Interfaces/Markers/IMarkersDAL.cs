using DocumentContracts.DTO;
using DocumentContracts.DTO.Markers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.Interfaces.Markers
{
   public interface IMarkersDAL
    {
        MarkerRsponse AddMarker(MarkerRequestAdd request);
        MarkerRsponse RemoveMarker(MarkerRequestRemove request);


    }
}
