using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CUR.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class MatrizCurricularOfertaViewModel : SMCViewModelBase, ISMCMappable
    {
        #region [ DataSource ]

        [SMCDataSource]
        [SMCHidden]
        [SMCServiceReference(typeof(ITurnoService), nameof(ITurnoService.BuscarTurnosPorCursoOfertaLocalidadeSelect), values: new string[] { nameof(SeqCursoOfertaLocalidade) })]
        public List<SMCDatasourceItem> Turnos { get; set; }

        #endregion [ DataSource ]

        [SMCHidden]
        [SMCKey]
        [SMCReadOnly]
        public long SeqDivisao { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqCurriculoCursoOferta { get; set; }

        [SMCDependency(nameof(SeqCursoOfertaLocalidade), nameof(MatrizCurricularController.PreencherCodigoMatrizCurricularItem), "MatrizCurricular", true, new string[] { nameof(MatrizCurricularDynamicModel.CodigoMatrizCurricular) })]
        [SMCOrder(0)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid3_24)]
        public string Codigo { get; set; }

        [SMCOrder(1)]
        [SMCRequired]
        [SMCSelect(nameof(MatrizCurricularDynamicModel.Localidades))]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid19_24, SMCSize.Grid14_24)]
        public long SeqCursoOfertaLocalidade { get; set; }

        [SMCHidden]
        [SMCIgnoreProp]
        public string DescricaoUnidade { get; set; }

        [SMCHidden]
        [SMCIgnoreProp]
        public string DescricaoLocalidade { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCIgnoreProp]
        [SMCOrder(2)]
        public string DescricaoUnidadeLocalidade
        {
            get { return $"{DescricaoUnidade} / {DescricaoLocalidade}"; }
        }

        [SMCDependency(nameof(SeqCursoOfertaLocalidade), nameof(MatrizCurricularController.BuscarTurnosPorCursoOfertaLocalidadeSelect), "MatrizCurricular", "CUR", true)]
        [SMCMapForceFromTo]
        [SMCOrder(3)]
        [SMCRequired]
        [SMCSelect(nameof(Turnos), NameDescriptionField = nameof(DescricaoTurno))]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid3_24)]
        public long SeqCursoOfertaTurno { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCIgnoreProp]
        [SMCOrder(5)]
        public string DescricaoTurno { get; set; }

        [SMCConditionalReadonly(nameof(SeqDivisao), SMCConditionalOperation.NotEqual, 0, PersistentValue = true)]
        [SMCOrder(6)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public DateTime DataInicioVigencia { get; set; }

        [SMCHidden]
        [SMCOrder(7)]
        public DateTime? DataFinalVigencia { get; set; }

        [SMCHidden]
        [SMCOrder(8)]
        public short? NumeroPeriodoAtivo { get; set; }

        [SMCHidden]
        [SMCOrder(9)]
        public SituacaoMatrizCurricularOferta? HistoricoSituacaoAtual { get; set; }

        [SMCDetail(SMCDetailType.Tabular)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCMapForceFromTo]
        [SMCOrder(10)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<MatrizCurricularOfertaExcecaoLocalidadeViewModel> ExcecoesLocalidade { get; set; }

        [SMCHidden]
        [SMCOrder(11)]
        public string DescricaoMatrizCurricular { get; set; }

        [SMCHidden]
        [SMCOrder(12)]
        public string DescricaoComplementarMatrizCurricular { get; set; }
    }
}