using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class PrevisaoConclusaoOrientacaoRelatorioData : ISMCMappable
    {
        public long SeqEntidade { get; set; }
        public string DescricaoEntidade { get; set; }
        public List<PrevisaoConclusaoOrientacaoRelatorioAlunosData> Alunos { get; set; }
    }
}
