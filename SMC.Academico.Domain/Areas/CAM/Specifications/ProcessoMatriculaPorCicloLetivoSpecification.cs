using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.SRC.Constants;
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
    public class ProcessoMatriculaPorCicloLetivoSpecification : SMCSpecification<Processo>
    {
        public ProcessoMatriculaPorCicloLetivoSpecification(long seqCicloLetivo, long seqEntidadeResponsavel)
        {
            SeqCicloLetivo = seqCicloLetivo;
            SeqEntidadeResponsavel = seqEntidadeResponsavel;
        }

        public long SeqCicloLetivo { get; set; }

        public long SeqEntidadeResponsavel { get; set; }

        public override Expression<Func<Processo, bool>> SatisfiedBy()
        {
            AddExpression(x => x.Servico.TipoServico.Token == TOKEN_TIPO_SERVICO.MATRICULA_INGRESSANTE);
            AddExpression(x => x.UnidadesResponsaveis.Any(f => f.SeqEntidadeResponsavel == SeqEntidadeResponsavel));
            AddExpression(x => x.SeqCicloLetivo == SeqCicloLetivo);

            return GetExpression();
        }
    }
}
