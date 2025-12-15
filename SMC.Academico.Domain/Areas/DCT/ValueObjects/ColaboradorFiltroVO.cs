using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.DCT.ValueObjects
{
    public class ColaboradorFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long[] Seqs { get; set; }

        public long? SeqPessoa { get; set; }

        public long? SeqEntidadeVinculo { get; set; }

        public long? SeqTipoVinculoColaborador { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public TipoAtividadeColaborador? TipoAtividade { get; set; }

        public long? SeqInstituicaoExterna { get; set; }

        public SituacaoColaborador? SituacaoColaboradorNaInstituicao { get; set; }

        public bool? Oritentador { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long[] SeqsAlunos { get; set; }

        public OrigemColaborador? OrigemColaborador { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public long? SeqCampanhaOferta { get; set; }

        public bool? VinculoAtivo { get; set; }

        public bool? TipoEntidadePermiteVinculo { get; set; }

        public bool? CriaVinculoInstitucional { get; set; }

        public long? SeqTurma { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        /// <summary>
        /// Desativa todos os filtros exceto o filtro de instituição
        /// </summary>
        public bool IgnorarFiltros { get; set; }

        public long[] SeqsCursoOfertaLocalidade { get; set; }

        public long[] SeqsColaboradorVinculoCurso { get; set; }

        public bool? AptoLecionarComponenteTurma { get; set; }

        #region CamposColaboradorVinculo

        public string TokenEntidadeVinculo { get; set; }

        public List<string> TokensEntidadeVinculo { get; set; }

        public long[] SeqsTiposEntidadesVinculo { get; set; }

        public bool? PermiteInclusaoManualVinculo { get; set; }
        public long[] SeqsEntidadesResponsaveis { get; set; }

        #endregion CamposColaboradorVinculo

        #region CamposColaboradorVinculoCurso

        public long[] SeqsEntidadesVinculo { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long[] SeqsCursoOferta { get; set; }

        #endregion CamposColaboradorVinculoCurso
    }
}