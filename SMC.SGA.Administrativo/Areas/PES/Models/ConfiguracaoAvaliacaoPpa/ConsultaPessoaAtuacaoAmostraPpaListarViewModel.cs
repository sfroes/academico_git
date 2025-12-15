using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class ConsultaPessoaAtuacaoAmostraPpaListarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string Nome { get; set; }

        public int CodigoAmostraPpa { get; set; }

        public DateTime? DataPreenchimento { get; set; }

    }
}