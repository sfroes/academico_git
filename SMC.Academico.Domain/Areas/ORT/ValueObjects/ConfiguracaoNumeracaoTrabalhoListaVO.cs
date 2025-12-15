using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class ConfiguracaoNumeracaoTrabalhoListaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string EntidadeResponsavel { get; set; }

        public string Descricao { get; set; }

        public int NumeroUltimaNumeracao { get; set; }

        public List<ConfiguracaoNumeracaoTrabalhoOfertaListaVO> Ofertas { get; set; }
    }
}
