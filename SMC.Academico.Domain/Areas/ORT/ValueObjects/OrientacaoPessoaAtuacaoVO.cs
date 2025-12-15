using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class OrientacaoPessoaAtuacaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqOrientacao { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public PessoaAtuacaoVO PessoaAtuacao { get; set; }

        public string DadosAlunoCompleto { get; set; }

        public string DadosCicloCompleto { get; set; }

        public string RA { get; set; }

        public string Nome { get; set; }

        public string DescricaoOfertaCursoLocalidade  { get; set; }

        public string Turno { get; set; }
    }
}
