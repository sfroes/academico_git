using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class CursoListaData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Nome { get; set; }

        [SMCMapProperty("SituacaoAtual.Seq")]
        public long SeqSituacaoAtual { get; set; }

        public long SeqTipoEntidade { get; set; }

        [SMCMapProperty("SituacaoAtual.Descricao")]
        public string DescricaoSituacaoAtual { get; set; }

        public long SeqNivelEnsino { get; set; }

        [SMCMapProperty("NivelEnsino.Descricao")]
        public string DescricaoInstituicaoNivelEnsino { get; set; }

        public List<CursoOfertaData> CursosOferta { get; set; }

        public List<HierarquiaEntidadeItemData> HierarquiasEntidades { get; set; }
    }
}