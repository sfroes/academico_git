using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaExistenteListaData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        [SMCMapMethod("RecuperarNomeAtual")]
        public string Nome { get; set; }

        [SMCMapProperty("Pessoa.Cpf")]
        public string Cpf { get; set; }

        [SMCMapProperty("Pessoa.NumeroPassaporte")]
        public string NumeroPassaporte { get; set; }

        public DateTime? DataNascimento { get; set; }

        public bool Selecionado { get; set; }

        public List<string> Filiacao { get; set; }

        public List<TipoAtuacao> Atuacoes { get; set; }
    }
}