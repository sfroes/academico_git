using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class OrgaoRegistroDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCReadOnly]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCRequired]
        [SMCSortable(true)]
        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid20_24)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCSortable(true)]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid8_24)]
        public string Sigla { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24)]
        public bool Ativo { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Tokens(tokenInsert: UC_CNC_004_06_01.MANTER_ORGAO_REGISTRO,
                           tokenEdit: UC_CNC_004_06_01.MANTER_ORGAO_REGISTRO,
                           tokenRemove: UC_CNC_004_06_01.MANTER_ORGAO_REGISTRO,
                           tokenList: UC_CNC_004_06_01.MANTER_ORGAO_REGISTRO);
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);

            if (viewMode == SMCViewMode.Insert)
                Ativo = true;
        }
    }
}