using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class InstituicaoNivelSituacaoMatriculaViewModel : SMCViewModelBase
    { 
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid20_24)]
        [SMCSelect("SituacoesMatriculaSelect")]       
        public long SeqSituacaoMatricula { get; set; }
    }
}