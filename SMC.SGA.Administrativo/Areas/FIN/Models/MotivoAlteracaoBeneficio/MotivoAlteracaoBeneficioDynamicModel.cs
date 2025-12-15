using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class MotivoAlteracaoBeneficioDynamicModel : SMCDynamicViewModel
    {
        [SMCSortable(true)]
        [SMCKey]
        [SMCReadOnly(SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCSize(SMCSize.Grid4_24)]
        public override long Seq { get; set; }

        [SMCSortable(true, true)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCDescription]
        [SMCSize(SMCSize.Grid10_24)]
        public string Descricao { get; set; }

        [SMCSortable(true)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCRegularExpression(REGEX.TOKEN)]
        public string Token { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        #region Configurações

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .EditInModal()
                .ModalSize(SMCModalWindowSize.Large)
                .Tokens(tokenList: UC_FIN_001_07_01.MANTER_MOTIVO_ALTERACAO_BENEFICIO,
                           tokenInsert: UC_FIN_001_07_01.MANTER_MOTIVO_ALTERACAO_BENEFICIO,
                           tokenEdit: UC_FIN_001_07_01.MANTER_MOTIVO_ALTERACAO_BENEFICIO,
                           tokenRemove: UC_FIN_001_07_01.MANTER_MOTIVO_ALTERACAO_BENEFICIO);
        }

        #endregion Configurações
    }
}