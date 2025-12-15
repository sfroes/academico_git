using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class OrientacaoPessoaAtuacaoData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqOrientacao { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string DadosAlunoCompleto { get; set; }

        public string DadosCicloCompleto { get; set; }

        public string RA { get; set; }

        public string Nome { get; set; }

        public string DescricaoOfertaCursoLocalidade { get; set; }

        public string Turno { get; set; }
    }
}
