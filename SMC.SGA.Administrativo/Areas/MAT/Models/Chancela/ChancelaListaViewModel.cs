using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class ChancelaListaViewModel : SMCViewModelBase
    {
        //x.ConfiguracaoProcesso.Processo.Seq
        public long SeqProcesso { get; set; }

        //x.ConfiguracaoProcesso.Processo.Descricao
        public string DescricaoProcesso { get; set; }

        public List<ChancelaItemListaViewModel> Chancelas { get; set; }
    }
}