using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ALN.Models;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class SituacaoDocumentoAcademicoDynamicModel : SMCDynamicViewModel
    {
        
        [SMCKey]        
        [SMCSortable(true)]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCMaxLength(255)]        
        [SMCSortable(true, true)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        public string Descricao { get; set; }        
        
        [SMCMinLength(3)]
        [SMCMaxLength(255)]
        [SMCRegularExpression(REGEX.TOKEN)]
        [SMCOrder(2)]        
        [SMCRequired]
        [SMCSize(SMCSize.Grid10_24)]
        public string Token { get; set; }

        [SMCSelect(SortBy = SMCSortBy.Description)]
        [SMCOrder(3)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid10_24)]
        public ClasseSituacaoDocumento ClasseSituacaoDocumento { get; set; }

        [SMCMinValue(1)]
        [SMCOrder(4)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid5_24)]
        public short Ordem { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(5)]
        public SMCMasterDetailList<SituacaoDocumentoAcademicoGrupoDoctoViewModel> GruposDocumento { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Service<ISituacaoDocumentoAcademicoService>(index: nameof(ISituacaoDocumentoAcademicoService.BuscarSituacaoDocumentoAcademicoFiltro), save: nameof(ISituacaoDocumentoAcademicoService.Salvar));            
            options.Tokens(tokenInsert: UC_CNC_004_05_01.MANTER_SITUACAO_DOCUMENTO_ACADEMICO,
                           tokenEdit: UC_CNC_004_05_01.MANTER_SITUACAO_DOCUMENTO_ACADEMICO,
                           tokenRemove: UC_CNC_004_05_01.MANTER_SITUACAO_DOCUMENTO_ACADEMICO,
                           tokenList: UC_CNC_004_05_01.MANTER_SITUACAO_DOCUMENTO_ACADEMICO)
                   .EditInModal();
        }
    }
}