using DalParametersConverter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.DTO.Register
{
  public  class UserDTO
    {
        [DBParameter("emailAddres")]
        public string UserID { get; set; }
        [DBParameter("userName")]
        public string UserName { get; set; }

    }
}
