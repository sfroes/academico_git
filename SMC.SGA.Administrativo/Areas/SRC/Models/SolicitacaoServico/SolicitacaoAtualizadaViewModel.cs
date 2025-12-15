using SMC.Framework.UI.Mvc;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoAtualizadaViewModel : SMCViewModelBase
    {
        public string AtualizadoPor { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public string DescricaoSolicitacao { get; set; }
    }
}