using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ReportHost.Areas.ALN.Models
{
    public class RelatorioPrevisaoConclusaoOrientacaoVO : ISMCMappable
    {
        public long SeqEntidade { get; set; }
        public string DescricaoEntidade { get; set; }
        public List<RelatorioPrevisaoConclusaoOrientacaoAlunosVO> Alunos { get; set; }
    }
}