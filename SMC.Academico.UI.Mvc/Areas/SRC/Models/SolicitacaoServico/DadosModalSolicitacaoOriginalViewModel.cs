using SMC.Framework.UI.Mvc;
using System;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class DadosModalSolicitacaoOriginalViewModel
    {
        public bool ExigeJustificativa { get; set; }

        public string CriadoPor { get; set; }

        public DateTime DataCriacao { get; set; }

        public string Justificativa { get; set; }

        public string JustificativaComplementar { get; set; }

        public string DescricaoSolicitacao { get; set; }
    }
}