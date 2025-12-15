using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Util;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ConfiguracaoComponenteSelecaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public string ConfiguracaoComponenteDescricaoCompleta { get; set; }

    }
}