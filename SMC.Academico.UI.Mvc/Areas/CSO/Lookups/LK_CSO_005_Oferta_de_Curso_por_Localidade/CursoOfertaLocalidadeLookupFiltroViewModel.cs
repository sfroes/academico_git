using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class CursoOfertaLocalidadeLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> Situacoes { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> TiposFormacaoEspecifica { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> Localidades { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> Modalidades { get; set; }

        #endregion [ DataSources ]

        [SMCHidden]
        [SMCKey]
        public long? Seq { get; set; }

        [SMCConditionalReadonly(nameof(SeqCursoIDLeitura), SMCConditionalOperation.Equals, true)]
        [SMCGroupedProperty("LookupCursoOfertaLocalidadeFiltroCurso")]
        [SMCSize(SMCSize.Grid2_24)]
        [SMCHidden]
        public long? SeqCurso { get; set; }

        [SMCConditionalReadonly(nameof(NomeLeitura), SMCConditionalOperation.Equals, true)]
        [SMCGroupedProperty("LookupCursoOfertaLocalidadeFiltroCurso")]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid8_24)]
        public string NomeCurso { get; set; }

        [SMCDescription]
        [SMCHidden]
        public string DescricaoOferta { get; set; }

        [SMCHidden]
        public bool? ListarDepartamentosGruposProgramas { get; set; }

        [SMCHidden]
        [SMCSize(SMCSize.Grid6_24)]
        public long? SeqEntidadeResponsavel { get; set; }

        /// <summary>
        /// Sequencial do HierarquiaItem que representa a entidade responsável pelo curso
        /// É um array pelo fato de existir um dependecy associados a ele.
        /// </summary>
        [SMCConditionalReadonly(nameof(SeqEntidadeResponsavelLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCGroupedProperty("LookupCursoOfertaLocalidadeFiltroCurso")]
        [SMCSelect(nameof(EntidadesResponsaveis), UseCustomSelect = true)]
        [SMCSize(SMCSize.Grid6_24)]
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        [SMCConditionalReadonly(nameof(SeqNivelEnsinoLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCConditionalDisplay(nameof(SeqsNiveisEnsinoDisplay), SMCConditionalOperation.Equals, false)]
        [SMCGroupedProperty("LookupCursoOfertaLocalidadeFiltroCurso")]
        [SMCSelect(nameof(NiveisEnsino), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid6_24)]
        public long? SeqNivelEnsino { get; set; }

        [SMCConditionalReadonly(nameof(SeqsNiveisEnsinoLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCConditionalDisplay(nameof(SeqsNiveisEnsinoDisplay), SMCConditionalOperation.Equals, true)]
        [SMCGroupedProperty("LookupCursoOfertaLocalidadeFiltroCurso")]
        [SMCSelect(nameof(NiveisEnsino), AutoSelectSingleItem = true, UseCustomSelect = true)]
        [SMCSize(SMCSize.Grid6_24)]
        public List<long> SeqsNiveisEnsino { get; set; }

        [SMCConditionalReadonly(nameof(SeqSituacaoAtualLeitura), SMCConditionalOperation.Equals, true, RuleName = "SituacoR01")]
        [SMCGroupedProperty("LookupCursoOfertaLocalidadeFiltroCurso")]
        [SMCSelect(nameof(Situacoes), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid4_24)]
        public long? SeqSituacaoAtual { get; set; }

        [SMCConditionalReadonly(nameof(SeqTipoFormacaoEspecificaLeitura), SMCConditionalOperation.Equals, true)]
        [SMCDependency(nameof(SeqNivelEnsino), "BuscarTipoFormacaoEspecificaPorNivelEnsinoSelect", "TipoFormacaoEspecifica", "CSO", false)]
        [SMCGroupedProperty("LookupCursoOfertaLocalidadeFiltroOferta")]
        [SMCSelect(nameof(TiposFormacaoEspecifica), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqTipoFormacaoEspecifica { get; set; }

        [SMCGroupedProperty("LookupCursoOfertaLocalidadeFiltroOferta")]
        [SMCSelect(nameof(Localidades), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqLocalidade { get; set; }

        [SMCDependency(nameof(SeqNivelEnsino), "BuscarModalidadesNivelEnsino", "ColaboradorVinculo", "DCT", false)]
        [SMCGroupedProperty("LookupCursoOfertaLocalidadeFiltroOferta")]
        [SMCSelect(nameof(Modalidades), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqModalidade { get; set; }

        /// <summary>
        /// Desativa todos os filtros exceto o filtro de instituição
        /// </summary>
        [SMCHidden]
        public bool IgnorarFiltros { get; set; }

        #region [ Readonly e Display ]

        [SMCHidden]
        public bool SeqCursoIDLeitura { get; set; }

        [SMCHidden]
        public bool NomeLeitura { get; set; }

        [SMCHidden]
        public bool SeqEntidadeResponsavelLeitura { get; set; }

        [SMCHidden]
        public bool SeqNivelEnsinoLeitura { get; set; }

        [SMCHidden]
        public bool SeqsNiveisEnsinoLeitura { get; set; }

        [SMCHidden]
        public bool SeqsNiveisEnsinoDisplay { get; set; }

        [SMCHidden]
        public bool SeqSituacaoAtualLeitura { get; set; }

        [SMCHidden]
        public bool SeqTipoFormacaoEspecificaLeitura { get; set; }

        #endregion [ Readonly ]
    }
}