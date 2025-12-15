using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.CNC.Controllers;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class InstituicaoNivelTipoDocumentoFormacaoEspecificaViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqInstituicaoNivelTipoDocumentoAcademico { get; set; }

        [SMCSelect(nameof(InstituicaoNivelTipoDocumentoAcademicoDynamicModel.TiposFormacaoEspecifica), NameDescriptionField = nameof(DescricaoFormacaoEspecifica))]
        [SMCDependency(nameof(InstituicaoNivelTipoDocumentoAcademicoDynamicModel.SeqInstituicaoNivel), nameof(InstituicaoNivelTipoDocumentoAcademicoController.BuscarTiposFormacaoEspecificaPorInstituicaoNivelSelect), "InstituicaoNivelTipoDocumentoAcademico", true)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        public long SeqTipoFormacaoEspecifica { get; set; }

        [SMCHidden]
        public string DescricaoFormacaoEspecifica { get; set; }
    }
}