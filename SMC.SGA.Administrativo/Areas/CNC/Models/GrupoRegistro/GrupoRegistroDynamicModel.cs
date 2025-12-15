using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class GrupoRegistroDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCSortable(true)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCSize(SMCSize.Grid3_24,SMCSize.Grid24_24, SMCSize.Grid3_24,SMCSize.Grid3_24)]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCDescription]
        [SMCMaxLength(255)]
        [SMCSortable(true, true)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid13_24, SMCSize.Grid24_24,SMCSize.Grid13_24,SMCSize.Grid13_24)]
        public string Descricao { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCMaxLength(10)]
        [SMCSize(SMCSize.Grid4_24)]
        public string Prefixo { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public long? NumeroUltimoRegistro { get; set; }

        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        public string PrefixoNumeroUltimoRegistro { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Service<IGrupoRegistroService>(index: nameof(IGrupoRegistroService.BuscarGruposRegistros))
                    .EditInModal()
                   .Tokens(tokenInsert: UC_CNC_004_07_01.MANTER_GRUPO_REGISTRO,
                           tokenEdit: UC_CNC_004_07_01.MANTER_GRUPO_REGISTRO,
                           tokenRemove: UC_CNC_004_07_01.MANTER_GRUPO_REGISTRO,
                           tokenList: UC_CNC_004_07_01.MANTER_GRUPO_REGISTRO);
        }
    }
}