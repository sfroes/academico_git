using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaAtuacaoAmostraPpaListaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public int CodigoAmostraPpa { get; set; }

        public string Nome { get; set; }

        public DateTime? DataPreenchimento { get; set; }
    }
}
