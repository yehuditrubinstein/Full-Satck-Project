﻿using DocumentContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentContracts.Interfaces
{
   public interface IDocumentSharingDAL
    {
        void AddSharing(DocumentSharingRequest request);

    }
}
