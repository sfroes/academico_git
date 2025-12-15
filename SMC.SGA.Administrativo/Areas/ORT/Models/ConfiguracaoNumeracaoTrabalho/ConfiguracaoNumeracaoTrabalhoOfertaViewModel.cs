using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class ConfiguracaoNumeracaoTrabalhoOfertaViewModel : SMCViewModelBase, ISMCMappable
    {

        [CursoOfertaLocalidadeLookup]
        [SMCRequired]
        [SMCFilter]
        [SMCDependency(nameof(ConfiguracaoNumeracaoTrabalhoDynamicModel.SeqEntidadeResponsavel))]
        [SMCSize(SMCSize.Grid14_24)]
        public CursoOfertaLocalidadeLookupViewModel SeqCursoOfertaLocalidade { get; set; }

        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoNumeracaoTrabalho { get; set; }
        
    }
}