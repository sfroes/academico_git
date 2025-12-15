using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SMC.SGA.Aluno.Areas.ORT.Models
{
    public class CabecalhoPublicacaoBdpViewModel : SMCViewModelBase
    {
        public string EntidadeResponsavel { get; set; }

        public string OfertaCursoLocalidadeTurno { get; set; }

        public List<string> AreaConhecimento { get; set; }

        public string AreaConcentracao { get; set; }
    }
}