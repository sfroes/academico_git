using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ConfiguracaoComponenteFiltroDynamicModel : SMCDynamicFilterViewModel, ISMCMappable
    {
        public long? Seq { get; set; }

        public long?[] SeqConfiguracoesComponentes { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoComplementar { get; set; }

        public bool? Ativo { get; set; }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new ConfiguracaoComponenteNavigationGroup(this);
        }

        public bool IgnorarFiltroDados { get; set; }
    }
}