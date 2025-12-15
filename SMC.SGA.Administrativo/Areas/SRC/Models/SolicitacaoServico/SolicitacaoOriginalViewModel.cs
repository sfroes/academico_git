using SMC.Framework.UI.Mvc;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoOriginalViewModel : SMCViewModelBase
    {
        public string CriadoPor { get; set; }

        public DateTime DataCriacao { get; set; }

        public string Justificativa { get; set; }

        public string JustificativaComplementar { get; set; }

        public string DescricaoSolicitacao { get; set; }
    }
}