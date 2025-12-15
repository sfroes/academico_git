using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class ClassificacaoInvalidadeDocumentoDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24)]
        public override long Seq { get; set; }

        [SMCRequired]
        [SMCDescription]
        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid10_24)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCRegularExpression(REGEX.TOKEN)]
        [SMCHidden(SMCViewMode.List)]
        public string Token { get; set; }

        [SMCRequired]
        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCHidden(SMCViewMode.List)]
        public string DescricaoXSD { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid7_24)]
        public TipoInvalidade TipoInvalidade { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid7_24)]
        [SMCHidden(SMCViewMode.List)]
        public bool Ativo { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .ModalSize(SMCModalWindowSize.Medium)
                   .Tokens(tokenInsert: UC_CNC_004_08_01.MANTER_CLASSIFICACAO_INVALIDADE_DOCUMENTO,
                           tokenEdit: UC_CNC_004_08_01.MANTER_CLASSIFICACAO_INVALIDADE_DOCUMENTO,
                           tokenRemove: UC_CNC_004_08_01.MANTER_CLASSIFICACAO_INVALIDADE_DOCUMENTO,
                           tokenList: UC_CNC_004_08_01.MANTER_CLASSIFICACAO_INVALIDADE_DOCUMENTO);
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            if(viewMode == SMCViewMode.Insert)
            {
                this.Ativo = true;
            }
        }
    }
}