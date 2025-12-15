using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class DispensaFilterSpecification : SMCSpecification<Dispensa>
    {
        public long? Seq { get; set; }

        public long? QuantidadeGrupoOrigem { get; set; }

        public long? QuantidadeGrupoDispensado { get; set; }

        public long? SeqComponenteCurricularOrigem { get; set; }

        public long? SeqComponenteCurricularDispensado { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public TipoDispensa? TipoFiltro { get; set; }

        public DateTime? DataInicioVigencia { get; set; }

        public DateTime? DataFimVigencia { get; set; }

        public MatrizExcecaoDispensa? MatrizAssociada { get; set; }

        public ModoExibicaoHistoricoEscolar? ModoExibicaoHistoricoEscolar { get; set; }

        /// <summary>
        /// Busca no grupo curricular se todos os seqs informados existem como componentes curriculares de origem
        /// </summary>
        public List<long> SeqsComponentesCurricularesOrigem { get; set; }

        /// <summary>
        /// Busca no grupo curricular se todos os seqs informados existem como componentes curriculares de dispensa
        /// </summary>
        public List<long> SeqsComponentesCurricularesDispensado { get; set; }

        /// <summary>
        /// Sequencial da matriz curricular para validar exceção
        /// </summary>
        public long? SeqMatrizCurricularOfertaExcecao { get; set; }

        public bool? Ativo { get; set; }

        public override Expression<Func<Dispensa, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => this.Seq.Value == p.Seq);
            AddExpression(this.QuantidadeGrupoOrigem, p => this.QuantidadeGrupoOrigem.Value == p.GrupoOrigem.Componentes.Count);
            AddExpression(this.QuantidadeGrupoDispensado, p => this.QuantidadeGrupoDispensado.Value == p.GrupoDispensado.Componentes.Count);
            AddExpression(this.SeqComponenteCurricularOrigem, p => p.GrupoOrigem.Componentes.Count(c => c.SeqComponenteCurricular == this.SeqComponenteCurricularOrigem.Value) > 0);
            AddExpression(this.SeqComponenteCurricularDispensado, p => p.GrupoDispensado.Componentes.Count(c => c.SeqComponenteCurricular == this.SeqComponenteCurricularDispensado.Value) > 0);
            AddExpression(this.SeqComponenteCurricular, p =>
                            (
                                (!this.TipoFiltro.HasValue || this.TipoFiltro == TipoDispensa.Nenhum || this.TipoFiltro == TipoDispensa.Todos) &&
                                (p.GrupoOrigem.Componentes.Count(c => c.SeqComponenteCurricular == this.SeqComponenteCurricular.Value) > 0 || p.GrupoDispensado.Componentes.Count(c => c.SeqComponenteCurricular == this.SeqComponenteCurricular.Value) > 0)
                            ) ||
                            (
                                (this.TipoFiltro == TipoDispensa.DispensadoPor && p.GrupoOrigem.Componentes.Count(c => c.SeqComponenteCurricular == this.SeqComponenteCurricular.Value) > 0)
                            ) ||
                            (
                                (this.TipoFiltro == TipoDispensa.Componente && p.GrupoDispensado.Componentes.Count(c => c.SeqComponenteCurricular == this.SeqComponenteCurricular.Value) > 0)
                            )
                            );
            AddExpression(this.DataInicioVigencia, p => p.HistoricosVigencia.Any(a => a.DataInicioVigencia == this.DataInicioVigencia.Value));
            AddExpression(this.DataFimVigencia, p => p.HistoricosVigencia.Any(a => a.DataFimVigencia == this.DataFimVigencia.Value));
            AddExpression(this.MatrizAssociada, p => (this.MatrizAssociada == MatrizExcecaoDispensa.Todos) || (this.MatrizAssociada == MatrizExcecaoDispensa.MatrizAssociado && p.MatrizesExcecao.Count > 0) || (this.MatrizAssociada == MatrizExcecaoDispensa.SemMatrizAssociado && p.MatrizesExcecao.Count == 0));
            AddExpression(this.SeqsComponentesCurricularesOrigem, p => SeqsComponentesCurricularesOrigem.All(a => p.GrupoOrigem.Componentes.Any(c => c.SeqComponenteCurricular == a)));
            AddExpression(this.SeqsComponentesCurricularesDispensado, p => SeqsComponentesCurricularesDispensado.All(a => p.GrupoDispensado.Componentes.Any(c => c.SeqComponenteCurricular == a)));
            AddExpression(this.SeqMatrizCurricularOfertaExcecao, p => !p.MatrizesExcecao.Any(a => a.SeqMatrizCurricularOferta == SeqMatrizCurricularOfertaExcecao));
            AddExpression(this.ModoExibicaoHistoricoEscolar, p => p.ModoExibicaoHistoricoEscolar == this.ModoExibicaoHistoricoEscolar);

            var agora = DateTime.Today;
            AddExpression(this.Ativo, p => !this.Ativo.Value ||
                                           (this.Ativo.Value &&
                                           p.HistoricosVigencia.Any(h => agora >= h.DataInicioVigencia && (!h.DataFimVigencia.HasValue || agora <= h.DataFimVigencia.Value))));

            return GetExpression();
        }
    }
}