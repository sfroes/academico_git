using SMC.Academico.Common.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaParametrosViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoComponente { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public LegendaPrincipal ConfiguracaoPrincipal { get; set; }

        [SMCIgnoreProp]
        public bool ExiteOfertasConfiguracao { get; set; }

        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid24_24)]
        public List<TurmaParametrosOfertaViewModel> ParametrosOfertas { get; set; }
    }
}