using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class RelatorioAlunosPorComponenteVO : ISMCMappable
    {
        public short NumeroAgrupador { get; set; }
        public long? SeqTurma { get; set; }
        public long? SeqConfiguracaoComponente { get; set; }
        public int? CodigoTurma { get; set; }
        public short? NumeroTurma { get; set; }
        public string DescricaoTurmaConfiguracaoComponente { get; set; }
        public string DescricaoConfiguracaoComponente { get; set; }
        public short NumeroCicloLetivo { get; set; }
        public short AnoCicloLetivo { get; set; }
        public long SeqPessoaAtuacao { get; set; }
        public string NomePessoaAtuacao { get; set; }
        public string DescricaoTipoAtuacao { get; set; }
        public string DescricaoTipoVinculoAluno { get; set; }
        public string NumeroProtocolo { get; set; }

    }
}
