using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
	public class AtendimentoIntercambioVO : ISMCMappable
	{
		public long Seq { get; set; }

		public long SeqInstituicaoEnsino { get; set; }

		public long SeqNivelEnsino { get; set; }

		public long SeqTipoVinculoAluno { get; set; }

		public bool CotutelaJaAssociada { get; set; }

		public string Cpf { get; set; }

		public string NumeroPassaporte { get; set; }

		public long? SeqTermoIntercambio { get; set; }

		public long? SeqTipoTermoIntercambio { get; set; }

		public string DescricaoTipoTermo { get; set; }

		public string DescricaoInstituicaoExterna { get; set; }

		public bool? ExigePeriodo { get; set; }

		public DateTime? DataInicio { get; set; }

		public DateTime? DataFim { get; set; }

		public long? SeqTipoOrientacao { get; set; }

		public CadastroOrientacao? OrientacaoAluno { get; set; }

		public List<SolicitacaoIntercambioParticipanteVO> Participantes { get; set; }

		public List<SMCDatasourceItem> TiposOrientacoes { get; set; }

		public List<SMCDatasourceItem> Colaboradores { get; set; }

		public List<SMCDatasourceItem> TiposParticipacoes { get; set; }

		public List<SMCDatasourceItem> TiposTermoIntercambio { get; set; }

		public bool ExisteTipoOrientacaoParametrizado { get; set; }

		public long? SeqOrientacao { get; set; }

	}
}