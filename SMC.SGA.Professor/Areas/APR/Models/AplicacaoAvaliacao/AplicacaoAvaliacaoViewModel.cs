using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Professor.Areas.APR.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Professor.Areas.APR.Models
{
    public class AplicacaoAvaliacaoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public long SeqAvaliacao { get; set; }

        public long SeqOrigemAvaliacao { get; set; }

        public string Sigla { get; set; }

        public bool EntregaWeb { get; set; }

        public string Observacao { get; set; }

        public DateTime? DataCancelamento { get; set; }

        public string MotivoCancelamento { get; set; }

        public long? SeqEventoAgd { get; set; }

        public string Local { get; set; }

        public long SeqTipoEventoAgd { get; set; }

        public DateTime DataInicioAplicacaoAvaliacao { get; set; }

        public DateTime? DataFimAplicacaoAvaliacao { get; set; }

        public short? QuantidadeMaximaPessoasGrupo { get; set; }
    }
}