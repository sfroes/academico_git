using SMC.Framework.UI.Mvc;
using System;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class DadosModalSolicitacaoAtualizadaViewModel
    {
        public string AtualizadoPor { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public string DescricaoSolicitacao { get; set; }
    }
}