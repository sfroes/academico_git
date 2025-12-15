using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ComponenteCurricularCabecalhoViewModel : SMCViewModelBase
    {
        public string DescricaoTipoComponenteCurricular { get; set; }

        public string DescricaoCompleta { get; set; }

        public TipoOrganizacao? TipoOrganizacao { get; set; }
    }
}