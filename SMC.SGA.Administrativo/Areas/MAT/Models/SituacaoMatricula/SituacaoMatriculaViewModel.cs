using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class SituacaoMatriculaViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        [SMCSize(SMCSize.Grid2_24)]        
        public long Seq { get; set; }

        [SMCMaxLength(100)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCOrder(0)] 
        [SMCSortable(true,true)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid7_24)]
        [SMCMaxLength(100)] 
        [SMCOrder(1)]
        [SMCRegularExpression(REGEX.TOKEN)]
        public string Token { get; set; }

        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid7_24)]
        [SMCOrder(2)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        public string DescricaoXSD { get; set; }
    }
} 