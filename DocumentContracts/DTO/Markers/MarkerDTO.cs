using DalParametersConverter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO.Markers
{
    public class MarkerDTO
    {
        [DBParameter("DocID")]
        public Guid DocID { get; set; }
        [DBParameter("MarkerType")]
        public string MarkerType { get; set; }
        [DBParameter("userId")]
        public string userId { get; set; }
        [DBParameter("MarkerLocation1X")]
        public int MarkerLocation1X { get; set; }
        [DBParameter("MarkerLocation1Y")]
        public int MarkerLocation1Y { get; set; }
        [DBParameter("MarkerLocation2X")]
        public int MarkerLocation2X { get; set; }
        [DBParameter("MarkerLocation2Y")]
        public int MarkerLocation2Y { get; set; }
        [DBParameter("Fore_backColor")]
        public string Fore_backColor { get; set; }

    }
}
