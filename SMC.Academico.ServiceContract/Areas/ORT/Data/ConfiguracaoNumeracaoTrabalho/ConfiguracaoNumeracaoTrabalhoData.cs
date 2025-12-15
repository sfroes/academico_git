using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class ConfiguracaoNumeracaoTrabalhoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqEntidadeResponsavel { get; set; }

        public string Descricao { get; set; }

        public int NumeroUltimaNumeracao { get; set; }

        public List<ConfiguracaoNumeracaoTrabalhoOfertaData> Ofertas { get; set; }
    }
}
