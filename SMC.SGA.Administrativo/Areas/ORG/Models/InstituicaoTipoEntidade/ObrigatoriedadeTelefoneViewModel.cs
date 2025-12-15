using SMC.Academico.Common.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class ObrigatoriedadeTelefoneViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqInstituicaoTipoEntidade { get; set; }

        [SMCRequired]
        [SMCSelect()]        
        [SMCSize(Framework.SMCSize.Grid8_24)]
        public TipoTelefone TipoTelefone { get; set; }

        [SMCRequired]
        [SMCRadioButtonList]
        [SMCSize(Framework.SMCSize.Grid6_24)]
        [SMCMapForceFromTo]
        public bool Obrigatorio { get; set; }
                
        [SMCSelect()] 
        [SMCSize(Framework.SMCSize.Grid8_24)]
        public CategoriaTelefone? CategoriaTelefone { get; set; }
    }
}