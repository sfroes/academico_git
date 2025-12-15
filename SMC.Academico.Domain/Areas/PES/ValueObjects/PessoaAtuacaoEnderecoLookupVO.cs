using SMC.Academico.Common.Areas.PES.Enums;
using System;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaAtuacaoEnderecoLookupVO : PessoaEnderecoLookupVO
    {
        public long SeqPessoaEndereco { get; set; }

        public EnderecoCorrespondencia EnderecoCorrespondencia { get; set; }

        public DateTime DataInclusao { get; set; }
    }
}