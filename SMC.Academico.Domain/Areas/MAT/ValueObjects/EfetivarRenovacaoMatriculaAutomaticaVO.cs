using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class EfetivarRenovacaoMatriculaAutomaticaVO : ISMCMappable
    {
		public long SeqProcesso { get; set; }

		public long SeqSolicitacaoServico { get; set; }

		public long SeqConfiguracaoProcesso { get; set; }

        public SituacaoDocumentacao SituacaoDocumentacao { get; set; }

        public long SeqSolicitacaoServicoEtapa { get; set; }

		public long SeqConfiguracaoEtapaPagina { get; set; }

		public long SeqConfiguracaoEtapa { get; set; }

		public long SeqSituacaoEtapaSgf { get; set; }

		public string DescricaoProcesso { get; set; }

		public string NumProtocolo { get; set; }

		public string NomePessoa { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string TokenServico { get; set; }

    }
}