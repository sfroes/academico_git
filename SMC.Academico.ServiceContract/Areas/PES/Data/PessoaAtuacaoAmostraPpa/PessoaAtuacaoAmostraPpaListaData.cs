using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoAmostraPpaListaData : ISMCMappable
    {
        public long SeqPessoaAtuacao { get; set; }
        public int CodigoAmostraPpa { get; set; }
        public string Nome { get; set; }
        public DateTime? DataPreenchimento { get; set; }
    }
}
