using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class TipoDocumentoAcademicoDynamicModel : SMCDynamicViewModel
    {
        #region DataSources

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(IServicoService), nameof(IServicoService.BuscarServicosGeraSolicitacaoTipoDocumentoSelect), values: new[] { nameof(SeqInstituicaoEnsino) })]
        public List<SMCDatasourceItem> ServicosDataSource { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoMensagemService), nameof(ITipoMensagemService.BuscarTagsSelect), values: new[] { nameof(TipoTag) })]
        public List<SMCDatasourceItem> ListaTags { get; set; }

        #endregion DataSources 

        [SMCHidden]
        [SMCIgnoreProp]
        public TipoTag TipoTag { get; set; } = TipoTag.DeclaracaoGenerica;

        [SMCKey]
        [SMCSortable(true)]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24,SMCSize.Grid24_24, SMCSize.Grid4_24,SMCSize.Grid4_24)]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCDescription]
        [SMCMaxLength(255)]
        [SMCSortable(true, true)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24,SMCSize.Grid20_24,SMCSize.Grid20_24)]
        public string Descricao { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCMinLength(3)]
        [SMCMaxLength(255)]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCRegularExpression(REGEX.TOKEN)]
        [SMCRequired]
        public string Token { get; set; }

        [SMCSelect]
        [SMCRequired]
        [SMCOrder(3)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid7_24)]
        public GrupoDocumentoAcademico GrupoDocumentoAcademico { get; set; }

        [SMCDetail(min: 0)]
        [SMCOrder(6)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<TipoDocumentoAcademicoServicoViewModel> Servicos { get; set; }

        [SMCOrder(7)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCDetail(SMCDetailType.Tabular)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCConditionalDisplay(nameof(GrupoDocumentoAcademico), SMCConditionalOperation.Equals, new object[] { GrupoDocumentoAcademico.DeclaracoesGenericasProfessor, GrupoDocumentoAcademico.DeclaracoesGenericasAluno })]
        public SMCMasterDetailList<TipoDocumentoAcademicoTagViewModel> Tags { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Service<ITipoDocumentoAcademicoService>(
                save: nameof(ITipoDocumentoAcademicoService.SalvarTipoDocumentoAcademico), 
                edit: nameof(ITipoDocumentoAcademicoService.BuscarTipoDocumentoAcademico))
                   .Tokens(tokenInsert: UC_CNC_004_01_01.MANTER_TIPO_DOCUMENTO_ACADEMICO,
                           tokenEdit: UC_CNC_004_01_01.MANTER_TIPO_DOCUMENTO_ACADEMICO,
                           tokenRemove: UC_CNC_004_01_01.MANTER_TIPO_DOCUMENTO_ACADEMICO,
                           tokenList: UC_CNC_004_01_01.MANTER_TIPO_DOCUMENTO_ACADEMICO);
        }
    }
}