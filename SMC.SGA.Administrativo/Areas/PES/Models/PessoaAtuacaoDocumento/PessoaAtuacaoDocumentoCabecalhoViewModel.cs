using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoDocumentoCabecalhoViewModel : SMCViewModelBase
    {
        [SMCIgnoreProp]
        public string Nome { get; set; }

        [SMCCpf]
        [SMCIgnoreProp]
        public string Cpf { get; set; }

        [SMCValueEmpty("-")]
        public string NumeroPassaporte { get; set; }

        [SMCValueEmpty("-")]
        public long? CodigoMigracaoAluno { get; set; }
        public long? RA { get; set; }

        [SMCHidden]
        public List<string> Mensagem { get; set; }

        [SMCValueEmpty("-")]
        public string DadosVinculo { get; set; }

    }
}