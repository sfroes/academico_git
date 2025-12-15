using SMC.Academico.Common.Areas.DCT.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class CategoriaInstituicaoEnsinoDynamicModel : SMCDynamicViewModel
    {
        [SMCFilter(true, true)]
        [SMCKey]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCSortable(true)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCFilter(true, true)]
        [SMCMaxLength(100)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid21_24)]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Tokens(tokenInsert: UC_DCT_001_04_01.MANTER_CATEGORIA_INSTITUICAO_ENSINO,
                           tokenEdit: UC_DCT_001_04_01.MANTER_CATEGORIA_INSTITUICAO_ENSINO,
                           tokenRemove: UC_DCT_001_04_01.MANTER_CATEGORIA_INSTITUICAO_ENSINO,
                           tokenList: UC_DCT_001_04_01.MANTER_CATEGORIA_INSTITUICAO_ENSINO);
        }
    }
}