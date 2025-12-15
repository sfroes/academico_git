using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaAtuacaoVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }
                
        public long SeqPessoa { get; set; }
                
        public long SeqPessoaDadosPessoais { get; set; }
                
        public TipoAtuacao TipoAtuacao { get; set; }
                
        public bool Ativo { get; set; }

        public List<PessoaEnderecoEletronicoVO> EnderecosEletronicos { get; set; }

        public List<PessoaTelefoneVO> Telefones { get; set; }

        public PessoaDadosPessoais DadosPessoais { get; set; }
    }
}
