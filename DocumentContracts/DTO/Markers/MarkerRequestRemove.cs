using DalParametersConverter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO.Markers
{
  public   class MarkerRequestRemove
    {
        [DBParameter("MarkerId")]
        public Guid MarkerId { get; set; }
    }
}
