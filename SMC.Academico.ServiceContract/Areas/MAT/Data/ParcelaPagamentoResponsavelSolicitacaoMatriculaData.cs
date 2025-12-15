using SMC.Framework.Mapper;
using SMC.Financeiro.Common.Areas.GRA.Enums;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class ParcelaPagamentoResponsavelSolicitacaoMatriculaData : ISMCMappable
    {
        public long SeqResponsavel { get; set; }

        public string NomeResponsavel { get; set; }

        public long SeqTitulo { get; set; }

        public long SeqServico { get; set; }

        public string DescricaoTitulo { get; set; }

        public SituacaoTitulo? SituacaoTitulo { get; set; }

        public decimal? ValorTitulo { get; set; }
    }
}