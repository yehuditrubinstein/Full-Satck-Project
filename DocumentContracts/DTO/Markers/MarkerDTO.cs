using DalParametersConverter;
using System;

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
        [DBParameter("RadiusX")]
        public int RadiusX { get; set; }
        [DBParameter("RadiusY")]
        public int RadiusY { get; set; }
        [DBParameter("CenterX")]
        public int CenterX { get; set; }
        [DBParameter("CenterY")]
        public int CenterY { get; set; }
        [DBParameter("BackColor")]
        public string BackColor { get; set; }
        [DBParameter("ForeColor")]
        public string ForeColor { get; set; }

    }
}
