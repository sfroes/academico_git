using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
	public class TrabalhoAcademicoFiltroData : SMCPagerFilterData, ISMCMappable
	{
		public long? SeqTrabalhoAcademico { get; set; }

		public long SeqInstituicaoLogada { get; set; }

		public string PalavraChave { get; set; }

		public bool? PesquisaPalavraChave { get; set; }

		public long? SeqTipoTrabalho { get; set; }

		public List<long?> SeqsTipoTrabalho { get; set; }

		public long? SeqAreaConhecimento { get; set; }

		public long? SeqPrograma { get; set; }

		public DateTime? DataInicio { get; set; }

		public DateTime? DataFim { get; set; }

		public string Letra { get; set; }

		public List<TipoPesquisaTrabalhoAcademico> TipoPesquisa { get; set; }

		public long? SeqAluno { get; set; }

		public List<long?> SeqsEntidadesResponsaveis { get; set; }

		public long? SeqCursoOfertaLocalidade { get; set; }

		public List<long> SeqsTurnos { get; set; }

		public long? SeqCicloLetivo { get; set; }

		public long? SeqTipoSituacao { get; set; }

		public long? SeqTurno { get; set; }

        public string Titulo { get; set; }

		public List<FiltroSituacaoTrabalhoAcademico> Situacao { get; set; }

		public bool? PesquisaDetalhada { get; set; }

		public bool? EmPublicacao { get; set; }

		public bool? EmFuturasDefesas { get; set; }

		public bool? UltimasPublicacoes { get; set; }

		public List<string> FiltrosDescricao { get; set; }

        public string Nome { get; set; }

        public List<TipoPesquisaTrabalhoAcademico> TipoPesquisaTrabalho { get; set; }

        public string TituloResumo { get; set; }
    }
}