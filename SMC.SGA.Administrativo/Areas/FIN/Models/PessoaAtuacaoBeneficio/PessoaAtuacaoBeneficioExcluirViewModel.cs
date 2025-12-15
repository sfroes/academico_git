using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class PessoaAtuacaoBeneficioExcluirViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacaoBeneficio { get; set; }

        public string Nome { get; set; }

        public string DescricaoBeneficio { get; set; }

        [SMCRequired]
        [SMCMultiline(Rows = 3)]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string Justificativa { get; set; }
    }
}