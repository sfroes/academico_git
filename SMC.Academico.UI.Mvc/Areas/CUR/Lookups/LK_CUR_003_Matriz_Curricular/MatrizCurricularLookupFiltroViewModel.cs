using SMC.Academico.Common.Constants;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class MatrizCurricularLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        #region [ DataSource ]

        [SMCDataSource(SMCStorageType.None)]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> Curriculos { get; set; }

        [SMCDataSource(SMCStorageType.None)]
        public List<SMCDatasourceItem> Modalidades { get; set; }

        #endregion [ DataSource ]

        [SMCConditionalReadonly(nameof(CursoLeitura), SMCConditionalOperation.Equals, true)]
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid8_24)]
        [CursoLookup]
        public CursoLookupViewModel SeqCurso { get; set; }

        [SMCConditionalReadonly(nameof(CurriculoLeitura), SMCConditionalOperation.Equals, true)]
        [SMCDependency("SeqCurso.Seq", "BuscarCurriculoPorCursoSelectLookup", "Curriculo", true)]
        [SMCOrder(1)]
        [SMCSelect(nameof(Curriculos), SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqCurriculo { get; set; }

        [SMCConditionalReadonly(nameof(CursoOfertaLeitura), SMCConditionalOperation.Equals, true)]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid8_24)]
        [CursoOfertaLookup]
        public CursoOfertaLookupViewModel SeqCursoOferta { get; set; }

        [SMCConditionalReadonly(nameof(ModalidadeLeitura), SMCConditionalOperation.Equals, true)]
        [SMCDependency("SeqCursoOferta", "BuscarModalidadePorCursoOfertaSelectLookup", "Modalidade", "CSO", false)]
        [SMCOrder(3)]
        [SMCSelect(nameof(Modalidades))]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqModalidade { get; set; }

        [SMCConditionalReadonly(nameof(CodigoLeitura), SMCConditionalOperation.Equals, true)]
        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid6_24)]
        public string Codigo { get; set; }

        [SMCConditionalReadonly(nameof(DescricaoLeitura), SMCConditionalOperation.Equals, true)]
        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCDescription]
        public string Descricao { get; set; }

        [SMCHidden]
        public bool CursoLeitura { get; set; }

        [SMCHidden]
        public bool CurriculoLeitura { get { return CurriculoParametro || (SeqCurso != null && SeqCurso.Seq > 0); } }

        [SMCHidden]
        public bool CurriculoParametro { get; set; }

        [SMCHidden]
        public bool CursoOfertaLeitura { get; set; }

        [SMCHidden]
        public bool ModalidadeLeitura { get; set; }

        [SMCHidden]
        public bool CodigoLeitura { get; set; }

        [SMCHidden]
        public bool DescricaoLeitura { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }
    }
}