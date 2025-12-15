using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class ParceriaIntercambioInstituicaoExternaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long? Seq { get; set; }

        [SMCHidden]
        public long? SeqParceriaIntercambio { get; set; }

        [SMCHidden]
        public bool RetornarInstituicaoEnsinoLogada { get; set; } = false;

        [SMCHidden]
        public bool ListarSomenteInstituicoesEnsino { get; set; } = true;

        //[SMCKey]
        //[SMCHidden]
        //public long? SeqInstituicaoExterna { get; set; }

        [InstituicaoExternaLookup]
       [SMCDependency(nameof(RetornarInstituicaoEnsinoLogada))]
        [SMCDependency(nameof(ListarSomenteInstituicoesEnsino))]
        [SMCKey]
        [SMCRequired]
        [SMCSize(SMCSize.Grid18_24)]
        [SMCUnique]
        public InstituicaoExternaViewModel SeqInstituicaoExterna { get; set; }

        [SMCHidden]
        [SMCSize(SMCSize.Grid18_24)]
        public string Nome { get; set; }

        [SMCHidden]
        public string Sigla { get; set; }

        [SMCDescription]
        [SMCHidden]
        public string Descricao
        {
            get
            {
                if (Nome != null || Sigla != null)
                {
                    return $"{Nome} - {Sigla}";
                }
                return null;
            }
        }
        
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public bool Ativo { get; set; }
    }
}