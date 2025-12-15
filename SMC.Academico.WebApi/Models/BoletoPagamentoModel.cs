using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.Academico.WebApi.Models
{
    public class BoletoPagamentoModel
    {
        public long SeqTitulo { get; set; }
        public long SeqServico { get; set; }
        public long SeqPessoaAtuacao { get; set; }
    }
}