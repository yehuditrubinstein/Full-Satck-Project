using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO.Markers
{
    public class MarkerRsponse : Response
    {
        public List<MarkerDTO> Markers { get; set; }
    }
}
