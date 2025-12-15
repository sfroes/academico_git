using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class OrientacaoTurmaListarDynamicModel : SMCDynamicViewModel
    {
        public override long Seq { get; set; }

        public long SeqTurma { get; set; }

        public long SeqAluno { get; set; }

        public string NomeAluno { get; set; }

        public List<OrientacaoColaboradorViewModel> OrientacoesColaborador { get; set; }
    }
}