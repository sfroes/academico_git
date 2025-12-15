using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class RelatorioBolsistasData : ISMCMappable
    {
        public long? SeqPessoaAtuacaoBeneficio { get; set; }

        public string SiglaEntidadeResponsavel { get; set; }

        public string Nome { get; set; }

		public string CPF { get; set; }

		public string DescricaoNivelEnsino { get; set; }

		public string TipoAtuacao { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public string DescricaoBeneficio { get; set; }

        public DateTime? DataInicioVigencia { get; set; }

        public DateTime? DataFimVigencia { get; set; }

        public string SituacaoChancelaBeneficio { get; set; }

        public string ReferenciaFinanceira { get; set; }

        public string ParcelasAbertas { get; set; }

        public int? CodigoAlunoMigracao { get; set; }
    }
}
