using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class PeriodicoImportacaoVO : ISMCMappable
    {
        public string ISSN { get; set; }

        public string Título { get; set; }

        public string AreaAvaliacao { get; set; }

        public string Estrato { get; set; }
    }
}
