using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.Academico.WebApi.Models
{
    public class DocumentoAcademicoGADModel : ISMCMappable
    {
        public long seqDocumentoAcademicoGAD { get; set; }
        public string Usuario { get; set; }
    }
}