using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class DivisaoTurmaColaboradorOrganizacaoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        public TipoOrganizacao? TipoOrganizacao { get; set; }

        public string DescricaoTipoOrganizacaoComponente { get; set; }

        public short QuantidadeCargaHoraria { get; set; }
    }
}