using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class ConfiguracaoNumeracaoTrabalhoOfertaListaVO : ISMCMappable
    {
        public long Seq { get; set; }
        public string Descricao { get; set; }
    }
}
