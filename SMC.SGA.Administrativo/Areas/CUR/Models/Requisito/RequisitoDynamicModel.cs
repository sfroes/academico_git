using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CUR.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class RequisitoDynamicModel : SMCDynamicViewModel, ISMCMappable
    {
        #region [ DataSource ]

        [SMCDataSource(SMCStorageType.Session)]
        [SMCServiceReference(typeof(IDivisaoMatrizCurricularService), nameof(IDivisaoMatrizCurricularService.BuscarDivisoesMatrizCurricularTipoSelect),
           values: new string[] { nameof(SeqMatrizCurricular) })]
        public List<SMCDatasourceItem> DivisoesMatrizCurricular { get; set; }

        [SMCDataSource(SMCStorageType.Session)]
        [SMCServiceReference(typeof(IComponenteCurricularService), nameof(IComponenteCurricularService.BuscarComponenteCurricularPorMatrizRequisitoSelect),
           values: new string[] { nameof(SeqMatrizCurricular), nameof(SeqDivisaoCurricularItem) })]
        public List<SMCDatasourceItem> ComponentesCurricular { get; set; }

        [SMCDataSource(SMCStorageType.Session)]
        [SMCServiceReference(typeof(ICurriculoCursoOfertaGrupoService), nameof(ICurriculoCursoOfertaGrupoService.BuscarGruposCurricularesCurriculoCursoOfertaSelect),
          values: new string[] { nameof(SeqCurriculoCursoOferta) })]
        public List<SMCDatasourceItem> GruposCurriculares { get; set; }

        #endregion [ DataSource ]

        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqMatrizCurricular { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqCurriculoCursoOferta { get; set; }

        [SMCOrder(1)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCSelect(nameof(DivisoesMatrizCurricular))]
        [SMCSize(SMCSize.Grid12_24)]
        public long? SeqDivisaoCurricularItem { get; set; }

        [SMCDependency(nameof(SeqDivisaoCurricularItem), nameof(RequisitoController.BuscarComponentesCurricularPorMatrizDivisao), "Requisito", false, nameof(SeqMatrizCurricular))]
        [SMCOrder(2)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCSelect(nameof(ComponentesCurricular))]
        [SMCSize(SMCSize.Grid12_24)]
        public long? SeqComponenteCurricular { get; set; }

        [SMCDetail(SMCDetailType.Modal, min: 1)]
        [SMCMapForceFromTo]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<RequisitoItemViewModel> Itens { get; set; }

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Detail<RequisitoListarDynamicModel>("_DetailList", g => g.SeqComponenteCurricular, "_CabecalhoGrupo")
                   .Header("CabecalhoRequisitoMatrizCurricular")
                   .HeaderIndex("CabecalhoRequisitoMatrizCurricular")
                   .HeaderIndexList("CabecalhoLista")
                   .ButtonBackIndex("Index", "MatrizCurricular", model => new
                   {
                       SeqCurriculoCursoOferta = SMCDESCrypto.EncryptNumberForURL((model as RequisitoFiltroDynamicModel).SeqCurriculoCursoOferta)
                   })
                   .Service<IRequisitoService>(delete: nameof(IRequisitoService.ExcluirRequisito),
                                                  edit: nameof(IRequisitoService.BuscarRequisito),
                                                 index: nameof(IRequisitoService.BuscarRequisitos),
                                                insert: nameof(IRequisitoService.BuscarConfiguracoesRequisito),
                                                  save: nameof(IRequisitoService.SalvarRequisito))
                   .Tokens(tokenList: UC_CUR_003_01_01.PESQUISAR_REQUISITO,
                           tokenInsert: UC_CUR_003_01_02.MANTER_REQUISITO,
                           tokenEdit: UC_CUR_003_01_02.MANTER_REQUISITO,
                           tokenRemove: UC_CUR_003_01_02.MANTER_REQUISITO);
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new MatrizCurricularNavigationGroup(this);
        }

        #endregion [ Configurações ]
    }
}