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
    public class CursoReplicaFormacaoEspecificaProgramaFilterSpecification : SMCSpecification<Curso>, ISMCMappable
    {

        public long? SeqFormacaoEspecifica { get; set; }
        public List<long?> SeqsEntidadesResponsaveis { get; set; }
        public List<CategoriaAtividade> CategoriasAtividadesSituacoesAtuais { get; set; }

        public override Expression<Func<Curso, bool>> SatisfiedBy()
        {
            DateTime dataAtual = DateTime.Today;

            AddExpression(SeqFormacaoEspecifica, w => w.CursosFormacaoEspecifica.All(f => f.SeqFormacaoEspecifica != this.SeqFormacaoEspecifica));
            AddExpression(SeqsEntidadesResponsaveis, w => w.HierarquiasEntidades.Any(a => this.SeqsEntidadesResponsaveis.Contains(a.SeqItemSuperior.Value)));
            AddExpression(CategoriasAtividadesSituacoesAtuais, x => x.HistoricoSituacoes.FirstOrDefault(p => dataAtual >= p.DataInicio && (!p.DataFim.HasValue || dataAtual <= p.DataFim.Value)).SituacaoEntidade == null
            || CategoriasAtividadesSituacoesAtuais.Contains(x.HistoricoSituacoes.FirstOrDefault(p => dataAtual >= p.DataInicio && (!p.DataFim.HasValue || dataAtual <= p.DataFim.Value)).SituacaoEntidade.CategoriaAtividade));

            return GetExpression();
        }
    }
}
