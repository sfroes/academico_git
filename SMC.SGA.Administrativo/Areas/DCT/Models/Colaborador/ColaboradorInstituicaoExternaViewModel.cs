using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class ColaboradorInstituicaoExternaViewModel : SMCViewModelBase, ISMCLookupData
    {

        [SMCHidden]
        public bool RetornarInstituicaoEnsinoLogada { get => true; }

        [SMCHidden]
        [SMCKey]
        public long? Seq { get; set; }
        
        [SMCRequired]
        [SMCUnique]
        [InstituicaoExternaLookup]
        [SMCDependency(nameof(RetornarInstituicaoEnsinoLogada))]
        [SMCDependency(nameof(ColaboradorDynamicModel.SeqColaborador))]
        [SMCDependency(nameof(ColaboradorDynamicModel.SeqInstituicaoEnsino))]
        [SMCSize(SMCSize.Grid18_24)]
        public InstituicaoExternaLookupViewModel SeqInstituicaoExterna { get; set; }

        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCRadioButtonList]
        public bool Ativo { get; set; } = true;
    }
}