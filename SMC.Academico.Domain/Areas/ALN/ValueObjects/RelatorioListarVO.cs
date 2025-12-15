using SMC.Framework.Mapper;
using SMC.Financeiro.Common.Areas.GRA.Enums;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class RelatorioListarVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public string DescricaoSituacaoMatricula { get; set; }

        public TipoVinculoAlunoFinanceiro TipoVinculoAlunoFinanceiro { get; set; }

        public string DescricaoPessoaAtuacao { get; set; }
        
        public string DescricaoCicloLetivo { get; set; }

        public string DescricaoCursoOferta { get; set; }
        
        public long SeqCicloLetivoIngresso { get; set; }
    }
}