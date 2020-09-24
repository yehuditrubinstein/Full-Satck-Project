using DalParametersConverter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO.Markers
{
   public class RequestGetMarkers
    {
        [DBParameter("DocId")]
        public Guid DocID { get; set; }
    }
}
