using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.TUR.Lookups
{
    public class TurmaLookupFiltroViewModel : SMCLookupFilterViewModel
	{
		#region [ DataSources ]

		public List<SMCDatasourceItem> DataSourceEntidadeResponsavel { get; set; }

        #endregion [ DataSources ]

        [SMCHidden]
        public long? Seq { get; set; }

        [CicloLetivoLookup]
        [SMCConditionalReadonly(nameof(CicloLetivoReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCRequired]
        public CicloLetivoLookupViewModel SeqCicloLetivoInicio { get; set; }

        [SMCConditionalReadonly(nameof(EntidadeResponsaveisReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCSelect(nameof(DataSourceEntidadeResponsavel), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid20_24)]
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        [CursoOfertaLookup]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid19_24)]
        public CursoOfertaLookupViewModel SeqCursoOferta { get; set; }

        [SMCKey]
        [SMCFilter(true, true)]
        [SMCRegularExpression(@"^[0-9]{0,}[.]{0,1}[0-9]{0,}$", FormatErrorResourceKey = "ERR_Expression")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        public string CodigoFormatado { get; set; }

		[SMCDescription]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid20_24)]
        public string DescricaoConfiguracao { get; set; }

        #region Campos Apoio

        [SMCHidden]
        public bool CicloLetivoReadOnly { get; set; }

        [SMCHidden]
        public bool EntidadeResponsaveisReadOnly { get; set; }

        [SMCHidden]
        public long? SeqCicloLetivo { get; set; }

        [SMCHidden]
        public bool? TurmasPeriodoEncerrado { get; set; }

        [SMCHidden]
        public bool? TurmaSituacaoNaoCancelada { get; set; }

        #endregion Campos Apoio
    }
}