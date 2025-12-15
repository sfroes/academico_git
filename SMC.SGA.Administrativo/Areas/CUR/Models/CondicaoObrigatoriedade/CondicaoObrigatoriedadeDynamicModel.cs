using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class CondicaoObrigatoriedadeDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCReadOnly(SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCSortable(true)]
        [SMCFilter]
        [SMCSize(SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCSortable(true, true)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCDescription]
        [SMCSize(SMCSize.Grid14_24)]
        public string Descricao { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        #region Configurações

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .EditInModal()
                .ModalSize(SMCModalWindowSize.Large)
                .Tokens(tokenList: UC_CUR_001_09_01.MANTER_CONDICAO_OBRIGATORIEDADE,
                           tokenInsert: UC_CUR_001_09_01.MANTER_CONDICAO_OBRIGATORIEDADE,
                           tokenEdit: UC_CUR_001_09_01.MANTER_CONDICAO_OBRIGATORIEDADE,
                           tokenRemove: UC_CUR_001_09_01.MANTER_CONDICAO_OBRIGATORIEDADE);
        }

        #endregion Configurações
    }
}