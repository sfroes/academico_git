using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;
using SMC.SGA.Administrativo.Areas.GRD.Views.GradeHorariaCompartilhada.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.GRD.Models
{
    public class GradeHorariaCompartilhadaListarDynamicModel : SMCDynamicViewModel
    {
        public override long Seq { get; set; }
        public string CicloLetivo { get; set; }
        public string EntidadeResponsavel { get; set; }
        public string Descricao { get; set; }
        public List<string> Divisoes { get; set; }
        public bool PerimiteExclusao { get; set; }
        public string FillIntructionBotaoExclusao
        {
            get
            {
                if (!PerimiteExclusao)
                {
                    return UIResource.FillInstructionBotaoExclusao;
                }

                return "";
            }
        }

    }
}