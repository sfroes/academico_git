using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class RelatorioAlunosPorComponenteListaVO : ISMCMappable
    {
        public int NumeroAgrupador { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string NomePessoaAtuacao { get; set; }

        public string DescricaoTipoAtuacao { get; set; }

        public string DescricaoTipoVinculoAluno { get; set; }

        public string NumeroProtocolo { get; set; }

        public string DescricaoCursoOferta { get; set; }
    }
}
