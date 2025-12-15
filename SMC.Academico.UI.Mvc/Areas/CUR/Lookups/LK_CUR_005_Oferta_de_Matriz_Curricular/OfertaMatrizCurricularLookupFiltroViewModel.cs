using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Academico.UI.Mvc.Areas.ORG.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class OfertaMatrizCurricularLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        #region [ DataSources ]

        [SMCDataSource(SMCStorageType.None)]
        public List<SMCDatasourceItem> Modalidades { get; set; }

        [SMCDataSource(SMCStorageType.None)]
        public List<SMCDatasourceItem> Turnos { get; set; }

        #endregion [ DataSources ]

        [SMCHidden]
        [SMCParameter]
        public long? SeqDispensa { get; set; }

        [SMCConditionalReadonly(nameof(CursoOfertaLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid8_24)]
        [CursoOfertaLookup]
        public CursoOfertaLookupViewModel SeqCursoOferta { get; set; }

        [SMCConditionalReadonly(nameof(ModalidadeLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCDependency("SeqCursoOferta.Seq", "BuscarModalidadePorCursoOfertaSelectLookup", "Modalidade", "CSO", false)]
        [SMCOrder(2)]
        [SMCSelect(nameof(Modalidades))]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqModalidade { get; set; }

        [HierarquiaEntidadeLookup]
        [SMCConditionalReadonly(nameof(LocalidadeLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCDependency(nameof(TipoVisaoHierarquia))]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid8_24)]
        public HierarquiaEntidadeLookupViewModel SeqLocalidade { get; set; }

        [SMCConditionalReadonly(nameof(TurnoLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCDependency("SeqCursoOferta.Seq", "BuscarTurnoPorCursoOfertaSelectLookup", "Turno", "CSO", false)]
        [SMCOrder(4)]
        [SMCSelect(nameof(Turnos))]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqTurno { get; set; }

        [SMCConditionalReadonly(nameof(CodigoLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid4_24)]
        public string Codigo { get; set; }

        [SMCConditionalReadonly(nameof(DescricaoLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCDescription]
        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid12_24)]
        public string DescricaoMatrizCurricular { get; set; }

        #region [ Readonly ]

        [SMCHidden]
        public bool CursoOfertaLeitura { get; set; }

        [SMCHidden]
        public bool ModalidadeLeitura { get; set; }

        [SMCHidden]
        public bool LocalidadeLeitura { get; set; }

        [SMCHidden]
        public TipoVisao? TipoVisaoHierarquia { get { return TipoVisao.VisaoLocalidades; } }

        [SMCHidden]
        public bool TurnoLeitura { get; set; }

        [SMCHidden]
        public bool CodigoLeitura { get; set; }

        [SMCHidden]
        public bool DescricaoLeitura { get; set; }

        [SMCHidden]
        public long? SeqMatrizCurricularOferta { get; set; }

        [SMCHidden]
        public long? SeqConfiguracaoComponente { get; set; }

        #endregion [ Readonly ]

        [SMCHidden]
        public long? SeqCicloLetivo { get; set; }
    }
}