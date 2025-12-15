using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.UI.Mvc.Areas.ALN.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class OrientacaoPessoaAtuacaoViewModel : SMCViewModelBase
    {
        public string DadosAlunoCompleto { get; set; }

        public string DadosCicloCompleto { get; set; }

        public string RA { get; set; }

        public string Nome { get; set; }

        public string DescricaoOfertaCursoLocalidade { get; set; }

        public string Turno { get; set; }
    }
}