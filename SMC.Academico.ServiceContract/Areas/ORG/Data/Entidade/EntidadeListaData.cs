using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class EntidadeListaData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Nome { get; set; }

        public long SeqTipoEntidade { get; set; }

        [SMCMapProperty("TipoEntidade.Descricao")]
        public string DescricaoTipoEntidade { get; set; }

        public string Sigla { get; set; }

        public string NomeReduzido { get; set; }

        [SMCMapProperty("SituacaoAtual.Descricao")]
        public string DescricaoSituacaoAtual { get; set; }

        [SMCMapProperty("SituacaoAtual.CategoriaAtividade")]
        public CategoriaAtividade CategoriaAtividadeSituacaoAtual { get; set; }

        public int? CodigoUnidadeSeo { get; set; }

        [SMCMapProperty("TipoEntidade.Token")]
        public string TokenTipoEntidade { get; set; }
    }
}