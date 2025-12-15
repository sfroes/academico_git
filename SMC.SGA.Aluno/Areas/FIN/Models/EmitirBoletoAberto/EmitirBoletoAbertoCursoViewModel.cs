using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.FIN.Models
{
    public class EmitirBoletoAbertoCursoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqServico { get; set; }

        public string DescricaoServico { get; set; }

        public List<EmitirBoletoAbertoParcelaViewModel> Parcelas { get; set; }
    }
}