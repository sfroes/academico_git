using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class DadosModalSolicitacaoAtualViewModel
    {
        public string Descricao { get; set; }

        public DateTime? DataSituacao { get; set; }

        public string UsuarioResponsavel { get; set; }

        public string Observacao { get; set; }

    }
}