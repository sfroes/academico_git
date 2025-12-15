using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.DataAnnotations;
using SMC.SGA.Administrativo.Areas.FIN.Controllers;
using SMC.SGA.Administrativo.Areas.FIN.Views.Contrato.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class ContratoDynamicModel : SMCDynamicViewModel, ISMCMappable, ISMCStatefulView
    {
        #region Data Sources

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoSelect))]
        public List<SMCDatasourceItem> NiveisDeEnsino { get; set; }

        #endregion Data Sources

        [SMCKey]
        [SMCSortable(true)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCReadOnly(SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        public override long Seq { get; set; } 

        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        [SMCRequired] 
        public string NumeroRegistro { get; set; }

        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid8_24)]
        public string Descricao { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        [SMCMaxDate(nameof(DataFimValidade))]
        public DateTime DataInicioValidade { get; set; }
         
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        [SMCMinDate(nameof(DataInicioValidade))]
        public DateTime? DataFimValidade { get; set; }

        [SMCHidden]
        public long SeqArquivoAnexado { get; set; } 

        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid24_24)]
        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true, MaxFileSize = 26214400)]
        public SMCUploadFile ArquivoAnexado { get; set; }

        [CursoLookup]
        [SMCHidden(SMCViewMode.List)] 
        [SMCSize(SMCSize.Grid24_24)] 
        public List<CursoViewModel> Cursos { get; set; } 

        [SMCDetail(SMCDetailType.Tabular, min: 0)]
        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid24_24)] 
        public SMCMasterDetailList<ContratoNiveisEnsinoViewModel> NiveisEnsino { get; set; }

        #region Configurações

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .Service<IContratoService>(
                      index: nameof(IContratoService.ListarContrato),
                      edit: nameof(IContratoService.BuscarContrato),
                      save: nameof(IContratoService.SalvarContrato) 
                 )
                .Detail<IContratoService>("_DetailList", allowSort: false)
                .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((ContratoListarDynamicModel)x).Descricao))
                .Tokens(tokenList: UC_FIN_002_02_01.PESQUISAR_CONTRATO,
                        tokenEdit: UC_FIN_002_02_02.MANTER_CONTRATO,
                        tokenRemove: UC_FIN_002_02_02.MANTER_CONTRATO,
                        tokenInsert: UC_FIN_002_02_02.MANTER_CONTRATO);
        }

        #endregion
    }
}