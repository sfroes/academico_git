using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class RelatorioAlunosPorSituacaoVO : ISMCMappable
    {
        public long SeqPessoa { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string Nome { get; set; }

        public string DescricaoTipoAtuacao { get; set; }

        public string DescricaoTipoSituacaoMatricula { get; set; }

        public string DescricaoSituacaoMatricula { get; set; }

        public string DescricaoVinculo { get; set; }

        public string DescricaoCursoOferta { get; set; }

        public long SeqEntidadeResponsavel { get; set; }
    }
}
