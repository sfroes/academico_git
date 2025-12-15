using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class EventoLetivoFilterSpecification : SMCSpecification<EventoLetivo>
    {
        public long? Seq { get; set; }

        public long? SeqTipoAgenda { get; set; }

        public long? SeqTipoEvento { get; set; }

        public string Descricao { get; set; }

        public int? AnoCiclo { get; set; }

        public int? NumeroCiclo { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqLocalidade { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqModalidade { get; set; }

        public TipoAluno? TipoAluno { get; set; }

        public override Expression<Func<EventoLetivo, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq.Value);
            AddExpression(this.SeqTipoAgenda, p => p.CicloLetivoTipoEvento.InstituicaoTipoEvento.SeqTipoAgenda == this.SeqTipoAgenda.Value);
            AddExpression(this.SeqTipoEvento, p => p.CicloLetivoTipoEvento.InstituicaoTipoEvento.SeqTipoEventoAgd == this.SeqTipoEvento.Value);
            AddExpression(this.Descricao, p => p.Descricao.Contains(this.Descricao));
            AddExpression(this.AnoCiclo, p => p.CicloLetivoTipoEvento.CicloLetivo.Ano == this.AnoCiclo.Value);
            AddExpression(this.NumeroCiclo, p => p.CicloLetivoTipoEvento.CicloLetivo.Numero == this.NumeroCiclo.Value);
            AddExpression(this.SeqNivelEnsino, p => p.NiveisEnsino.Any(n => n.Seq == this.SeqNivelEnsino.Value));
            AddExpression(this.SeqsEntidadesResponsaveis, p => p.ParametrosEntidade.Any(a => this.SeqsEntidadesResponsaveis.Contains(a.SeqEntidade)));
            ///AddExpression(this.SeqLocalidade); TODO
            AddExpression(this.SeqCursoOferta, p => p.NiveisEnsino.Any(n => n.Cursos.Any(c => c.CursosOferta.Any(co => co.Seq == this.SeqCursoOferta.Value))));
            AddExpression(this.SeqModalidade, p => p.NiveisEnsino.Any(n => n.Cursos.Any(c => c.CursosOferta.Any(co => co.CursosOfertaLocalidade.Any(col => col.SeqModalidade == this.SeqModalidade.Value)))));
            AddExpression(this.TipoAluno, p => p.TiposAluno.Any(t => t.TipoAluno == this.TipoAluno.Value));

            return GetExpression();
        }
    }
}