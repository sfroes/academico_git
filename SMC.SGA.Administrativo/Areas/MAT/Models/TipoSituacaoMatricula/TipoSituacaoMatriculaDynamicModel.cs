using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.MAT.Views.TipoSituacaoMatricula.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class TipoSituacaoMatriculaDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCReadOnly(SMCViewMode.All)] 
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid2_24)]
        public override long Seq { get; set; }
         
        [SMCMaxLength(100)] 
        [SMCSize(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid12_24)]
        [SMCRequired]
        [SMCOrder(0)]
        public string Descricao { get; set; }

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        [SMCRequired]
        [SMCSelect]
        public bool? VinculoAlunoAtivo { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCMaxLength(100)] 
        [SMCOrder(2)]
        [SMCRegularExpression(REGEX.TOKEN)]
        public string Token { get; set; }

        [SMCOrder(3)]
        [SMCDetail(min: 1)]
        [SMCRequired]
        [SMCSortable(true,true)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<SituacaoMatriculaViewModel> SituacoesMatricula { get; set; }

        #region Configurações

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .Detail<TipoSituacaoMatriculaListarDynamicModel>("_DetailList", allowSort: true)
                .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((TipoSituacaoMatriculaListarDynamicModel)x).Descricao))
                .Tokens(tokenList: UC_MAT_005_01_01.PESQUISAR_TIPO_SITUACAO_MATRICULA,
                           tokenInsert: UC_MAT_005_01_02.MANTER_TIPO_SITUACAO_MATRICULA,
                           tokenEdit: UC_MAT_005_01_02.MANTER_TIPO_SITUACAO_MATRICULA,
                           tokenRemove: UC_MAT_005_01_02.MANTER_TIPO_SITUACAO_MATRICULA);
        }

        #endregion

    }
}