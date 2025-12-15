using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Mapper;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class CursoOfertaLocalidadeReplicaFormacaoEspecificaProgramaFilterSpecification : SMCSpecification<CursoOfertaLocalidade>, ISMCMappable
    {
        public long? SeqFormacaoEspecifica { get; set; }
        public List<long> SeqsCursos { get; set; }
        public List<CategoriaAtividade> CategoriasAtividadesSituacoesAtuais { get; set; }

        public override Expression<Func<CursoOfertaLocalidade, bool>> SatisfiedBy()
        {
            DateTime dataAtual = DateTime.Today;

            AddExpression(SeqsCursos, w => this.SeqsCursos.Contains(w.CursoOferta.SeqCurso));

            AddExpression(CategoriasAtividadesSituacoesAtuais, x => x.HistoricoSituacoes.FirstOrDefault(p => dataAtual >= p.DataInicio && (!p.DataFim.HasValue || dataAtual <= p.DataFim.Value)).SituacaoEntidade == null
            || CategoriasAtividadesSituacoesAtuais.Contains(x.HistoricoSituacoes.FirstOrDefault(p => dataAtual >= p.DataInicio && (!p.DataFim.HasValue || dataAtual <= p.DataFim.Value)).SituacaoEntidade.CategoriaAtividade));
            
            AddExpression(SeqFormacaoEspecifica, w => w.FormacoesEspecificas.All(f => f.SeqFormacaoEspecifica != this.SeqFormacaoEspecifica) && 
                         (!w.CursoOferta.SeqFormacaoEspecifica.HasValue || w.CursoOferta.SeqFormacaoEspecifica == this.SeqFormacaoEspecifica));
            
            return GetExpression();
        }
    }
}
