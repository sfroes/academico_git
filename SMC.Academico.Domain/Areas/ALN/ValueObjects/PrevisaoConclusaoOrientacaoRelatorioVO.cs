using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class PrevisaoConclusaoOrientacaoRelatorioVO : ISMCMappable
    {
        public long SeqEntidade { get; set; }
        public string DescricaoEntidade { get; set; }
        public List<PrevisaoConclusaoOrientacaoRelatorioAlunosVO> Alunos { get; set; }
    }
}
