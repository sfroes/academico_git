using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class CursoLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        #region [ DataSources ]

        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        public List<SMCDatasourceItem> Situacoes { get; set; }

        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        #endregion [ DataSources ]

        [SMCConditionalReadonly(nameof(SeqCursoIDLeitura), SMCConditionalOperation.Equals, true)]
        [SMCKey]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public long? Seq { get; set; }

        [SMCConditionalReadonly(nameof(NomeLeitura), SMCConditionalOperation.Equals, true)]
        [SMCDescription]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid10_24)]
        [SMCMaxLength(100)]
        public string Nome { get; set; }

        [SMCConditionalReadonly(nameof(SeqNivelEnsinoLeitura), SMCConditionalOperation.Equals, true)]
        [SMCSelect("NiveisEnsino")]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid10_24)]
        public List<long> SeqNivelEnsino { get; set; }

        /// <summary>
        /// Sequencial do EntidadeHistoricoSituacao atual do curso
        /// </summary>
        [SMCConditionalReadonly(nameof(SeqSituacaoAtualLeitura), SMCConditionalOperation.Equals, true)]
        [SMCSelect("Situacoes")]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public long? SeqSituacaoAtual { get; set; }

        /// <summary>
        /// Sequencial do HierarquiaItem que representa a entidade responsável pelo curso
        /// </summary>
        [SMCConditionalReadonly(nameof(SeqEntidadeResponsavelLeitura), SMCConditionalOperation.Equals, true)]
        [SMCMapProperty("SeqEntidade")]
        [SMCSelect("EntidadesResponsaveis", UseCustomSelect = true)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid10_24)]
        public List<long?> SeqsEntidadesResponsaveis { get; set; }

        /// <summary>
        /// Listar apenas níveis de ensino reconhecidos LDB
        /// </summary>
        [SMCHidden]
        public bool ApenasNiveisEnsinoReconhecidosLDB { get; set; }

        /// <summary>
        /// Listar apenas entidades cuja categoria da sua situação vigente seja igual a "Em Ativação" ou "Ativa"
        /// </summary>
        [SMCHidden]
        public bool ApenasEntidadesComCategoriasAtivas { get; set; }

        #region [ Readonly ]

        [SMCHidden]
        public bool SeqCursoIDLeitura { get; set; }

        [SMCHidden]
        public bool NomeLeitura { get; set; }

        [SMCHidden]
        public bool SeqEntidadeResponsavelLeitura { get; set; }

        [SMCHidden]
        public bool SeqNivelEnsinoLeitura { get; set; }

        [SMCHidden]
        public bool SeqSituacaoAtualLeitura { get; set; }

        [SMCHidden]
        public bool SeqSituacaoLeitura { get { return SeqSituacaoAtualLeitura || !(SeqNivelEnsino != null && SeqNivelEnsino.SMCCount() > 0); } }

        #endregion [ Readonly ]
    }
}