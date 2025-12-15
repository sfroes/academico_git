using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class ProcessoMatriculaPorNivelEnsinoSpecification : SMCSpecification<Processo>
    {
        public ProcessoMatriculaPorNivelEnsinoSpecification(long seqProcesso, IEnumerable<long> seqNivelEnsino, IEnumerable<long> seqVinculo)
        {
            SeqProcesso = seqProcesso;
            SeqNivelEnsino = seqNivelEnsino;
            SeqVinculo = seqVinculo;
        }

        public long SeqProcesso { get; set; }

        public IEnumerable<long> SeqVinculo { get; set; }

        public IEnumerable<long> SeqNivelEnsino { get; set; }        

        public override Expression<Func<Processo, bool>> SatisfiedBy()
        {
            AddExpression(x => x.Seq == SeqProcesso);

            AddExpression(x => SeqNivelEnsino.All(n => x.Configuracoes.Any(c => c.NiveisEnsino.Any(f => f.SeqNivelEnsino == n))));

            AddExpression(x => SeqVinculo.All(n => x.Configuracoes.Any(c => c.TiposVinculoAluno.Any(f => f.SeqTipoVinculoAluno == n))));

            return GetExpression();
        }
    }
}
