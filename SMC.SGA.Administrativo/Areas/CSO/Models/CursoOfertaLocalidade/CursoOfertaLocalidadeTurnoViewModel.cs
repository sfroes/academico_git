using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoOfertaLocalidadeTurnoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCSelect(nameof(CursoOfertaLocalidadeDynamicModel.TurnosInstituicaoNivel))]
        [SMCSize(SMCSize.Grid6_24)]
        public long SeqTurno { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit)]
        public string Descricao { get; set; }

        public override string ToString()
        {
            return Descricao;
        }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid3_24)]
        public bool Ativo { get; set; }
    }
}