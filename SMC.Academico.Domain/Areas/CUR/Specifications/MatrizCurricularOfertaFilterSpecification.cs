using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class MatrizCurricularOfertaFilterSpecification : SMCSpecification<MatrizCurricularOferta>
    {
        public long? Seq { get; set; }

        public long? SeqCursoOfertaLocalidadeTurno { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public long? SeqTurno { get; set; }

        public DateTime? DataHistorico { get; set; }

        public DateTime? DataAtivacaoMatriz { get; set; }

        public List<long> Seqs { get; set; }

        public long? SeqCurso { get; set; }

        public List<long> GrupoOrigemSeqComponentesCurriculares { get; set; }

        public List<long> GrupoDispensadoSeqComponentesCurriculares { get; set; }

        public long? SeqMatrizCurricular { get; set; }

        public long? SeqCurriculo { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqCurriculoCursoOferta { get; set; }

        public long? SeqModalidade { get; set; }

        public string Codigo { get; set; }

        public string DescricaoMatrizCurricular { get; set; }

        public string DescricaoComplementarMatrizCurricular { get; set; }

        public long? SeqLocalidade { get; set; }

        public List<long> SeqsCurriculoCursoOfertas { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }


        public override Expression<Func<MatrizCurricularOferta, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.Seqs, p => this.Seqs.Contains(p.Seq));
            AddExpression(this.SeqCursoOfertaLocalidadeTurno, p => p.SeqCursoOfertaLocalidadeTurno == this.SeqCursoOfertaLocalidadeTurno);
            AddExpression(this.SeqCursoOfertaLocalidade, p => p.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade == this.SeqCursoOfertaLocalidade);
            AddExpression(this.SeqTurno, p => p.CursoOfertaLocalidadeTurno.SeqTurno == this.SeqTurno);
            AddExpression(this.DataAtivacaoMatriz, p => p.HistoricosSituacao.Any(a => (this.DataAtivacaoMatriz <= a.DataInicio || !a.DataFim.HasValue || this.DataAtivacaoMatriz <= a.DataFim) && a.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.Ativa));
            AddExpression(this.SeqCurso, p => p.MatrizCurricular.CurriculoCursoOferta.CursoOferta.SeqCurso == this.SeqCurso);
            AddExpression(this.SeqMatrizCurricular, p => p.SeqMatrizCurricular == this.SeqMatrizCurricular);
            AddExpression(this.SeqCurriculo, p => p.MatrizCurricular.CurriculoCursoOferta.SeqCurriculo == this.SeqCurriculo);
            AddExpression(this.SeqCursoOferta, p => p.MatrizCurricular.CurriculoCursoOferta.SeqCursoOferta == this.SeqCursoOferta);
            AddExpression(this.SeqCurriculoCursoOferta, p => p.MatrizCurricular.SeqCurriculoCursoOferta == this.SeqCurriculoCursoOferta);
            AddExpression(this.SeqModalidade, p => p.MatrizCurricular.CurriculoCursoOferta.CursoOferta.CursosOfertaLocalidade.Count(c => c.SeqModalidade == this.SeqModalidade) > 0);
            AddExpression(this.Codigo, p => p.MatrizCurricular.Codigo.Contains(this.Codigo));
            AddExpression(this.DescricaoMatrizCurricular, p => p.MatrizCurricular.Descricao.Contains(this.DescricaoMatrizCurricular));
            AddExpression(this.DescricaoComplementarMatrizCurricular, p => p.MatrizCurricular.DescricaoComplementar.Contains(this.DescricaoComplementarMatrizCurricular));
            AddExpression(this.SeqLocalidade, p => p.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade == this.SeqLocalidade);
            AddExpression(this.GrupoOrigemSeqComponentesCurriculares, p => this.GrupoOrigemSeqComponentesCurriculares.Count(c => p.MatrizCurricular.ConfiguracoesComponente.Select(a => a.GrupoCurricularComponente.SeqComponenteCurricular).Contains(c)) > 0);
            AddExpression(this.GrupoDispensadoSeqComponentesCurriculares, p => this.GrupoDispensadoSeqComponentesCurriculares.Count(c => p.MatrizCurricular.ConfiguracoesComponente.Select(a => a.GrupoCurricularComponente.SeqComponenteCurricular).Contains(c)) == 0);
            AddExpression(this.SeqsCurriculoCursoOfertas, p => this.SeqsCurriculoCursoOfertas.Contains(p.MatrizCurricular.SeqCurriculoCursoOferta));
            AddExpression(this.SeqConfiguracaoComponente, p => p.MatrizCurricular.ConfiguracoesComponente.Any(c => c.SeqConfiguracaoComponente == this.SeqConfiguracaoComponente));

            return GetExpression();
        }
    }
}