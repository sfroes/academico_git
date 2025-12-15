using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class ComponenteCurricularLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        #region [ DataSource ]

        [SMCDataSource(SMCStorageType.None)]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> TiposComponenteCurricular { get; set; }

        [SMCDataSource(SMCStorageType.None)]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> EntidadesResponsavel { get; set; }

        [SMCDataSource(SMCStorageType.None)]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        #endregion [ DataSource ]

        /// <summary>
        /// Sequencial do grupo curricular do componente a ser substituído
        /// </summary>
        [SMCHidden]
        [SMCParameter]
        public long? SeqGrupoCurricularComponente { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqGrupoCurricular { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqNivelEnsino { get; set; }

        [SMCHidden]
        [SMCParameter]
        public bool? TipoComponenteDispensa { get; set; }

        /// <summary>
        /// Quando setado, define que o SeqTipoComponenteCurricular deverá ser setado com o tipo que tenha uma divisão com gestão do tipo AssuntoComponente
        /// </summary>
        [SMCHidden]
        [SMCParameter]
        public bool? AssuntoComponente { get; set; }

        [SMCHidden]
        [SMCParameter]
        public TipoGestaoDivisaoComponente[] TiposGestaoDivisaoComponente { get; set; }

        [SMCHidden]
        [SMCParameter]
        public FormatoConfiguracaoGrupo? FormatoConfiguracaoGrupo { get; set; }

        [SMCHidden]
        public long[] SeqTipoComponentesCurriculares { get; set; }

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCSelect(nameof(NiveisEnsino), AutoSelectSingleItem = true)]
		[SMCConditionalReadonly(nameof(SeqTipoComponenteCurricularReadOnly), true, PersistentValue = true)]
		public long? SeqInstituicaoNivelResponsavel { get; set; }

        [SMCConditionalReadonly(nameof(SeqTipoComponenteCurricularReadOnly), true, PersistentValue = true)]
        [SMCDependency(nameof(SeqInstituicaoNivelResponsavel), "BuscarTipoComponenteCurricularSelectLookup", "ComponenteCurricularRoute", "", true, includedProperties: new[] { nameof(SeqGrupoCurricular) })]
        [SMCOrder(2)]
        [SMCSelect(nameof(TiposComponenteCurricular), SortBy = SMCSortBy.Description, AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqTipoComponenteCurricular { get; set; }

        [SMCDependency(nameof(SeqInstituicaoNivelResponsavel), "BuscarEntidadesPorTipoComponenteSelectLookup", "ComponenteCurricularRoute", "", true)]
        [SMCDependency(nameof(SeqTipoComponenteCurricular), "BuscarEntidadesPorTipoComponenteSelectLookup", "ComponenteCurricularRoute", "", true)]
        [SMCOrder(3)]
        [SMCSelect(nameof(EntidadesResponsavel), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqEntidade { get; set; }

        [SMCKey]
        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid2_24)]
        public string Codigo { get; set; }

        [SMCDescription]
        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid12_24)]
        public string Descricao { get; set; }

        [SMCOrder(6)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        public bool? Ativo { get; set; }

        [SMCHidden]
        public bool SeqTipoComponenteCurricularReadOnly { get; set; }

        [SMCHidden]
        public long? SeqAluno { get; set; }

        [SMCHidden]
        public long? SeqCicloLetivo { get; set; }

        [SMCHidden]
        public long? SeqMatrizCurricular { get; set; }

        /// <summary>
        /// Quando informado junto com a SeqMatrizCurricular, retorna apenas os assuntos deste componente
        /// </summary>
        [SMCHidden]
        public long? SeqComponenteCurricular { get; set; }
    }
}