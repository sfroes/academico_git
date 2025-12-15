using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class TipoSituacaoMatriculaListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCOrder(3)]
        public override long Seq { get; set; } 

        [SMCFilter]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCOrder(0)]
        [SMCSortable(true,true)]
        public string Descricao { get; set; }

        [SMCSize(SMCSize.Grid3_24)]
        [SMCSelect]
        [SMCOrder(3)]
        public bool VinculoAlunoAtivo { get; set; }

        [SMCFilter]
        [SMCSize(SMCSize.Grid7_24)]
        [SMCMaxLength(100)]
        [SMCOrder(1)]
        [SMCSortable(true, true)]
        [SMCRegularExpression(REGEX.TOKEN)]
        public string Token { get; set; }

        [SMCOrder(2)]
        [SMCDetail(min: 1)] 
        public SMCMasterDetailList<SituacaoMatriculaViewModel> SituacoesMatricula { get; set; }
    }
}