using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class AlunoHistoricoSituacaoFilterSpecification : SMCSpecification<AlunoHistoricoSituacao>
    {
        public long? SeqPessoaAtuacaoAluno { get; set; }
        
        public long[] SeqsPessoaAtuacaoAluno { get; set; }

        public bool? SituacaoFutura { get; set; }

        public string TokenSituacao { get; set; }

        public string TokenTipoSituacao { get; set; }

        public long? SeqCicloLetivo { get; set; }
        public bool? FiltrarEntreCiclos { get; set; }
        public long? SeqCicloLetivoInicio { get; set; }
        public long? SeqCicloLetivoFim { get; set; }

        public DateTime? DataReferencia { get; set; }

		public bool? Excluido { get; set; }

		public long? SeqAlunoHistoricoCicloLetivo { get; set; }

        public bool? Atual { get; set; }

        public List<long> SeqsSituacaoMatricula { get; set; }

        public DateTime? DataInicioSituacao { get; set; }

        public override Expression<Func<AlunoHistoricoSituacao, bool>> SatisfiedBy()
        {
            if (!DataReferencia.HasValue)
                DataReferencia = DateTime.Now;

            AddExpression(Excluido, x => x.DataExclusao.HasValue == Excluido);
            AddExpression(SeqPessoaAtuacaoAluno, x => !x.AlunoHistoricoCicloLetivo.DataExclusao.HasValue &&
                               x.AlunoHistoricoCicloLetivo.AlunoHistorico.Atual &&
                               x.AlunoHistoricoCicloLetivo.AlunoHistorico.SeqAluno == SeqPessoaAtuacaoAluno);
            AddExpression(SeqsPessoaAtuacaoAluno, x => SeqsPessoaAtuacaoAluno.Contains(x.AlunoHistoricoCicloLetivo.AlunoHistorico.SeqAluno));

            if (SituacaoFutura.HasValue)
            {
                if (SituacaoFutura.Value)
                    AddExpression(x => x.DataInicioSituacao >= DataReferencia.Value && !x.DataExclusao.HasValue);
                else
                    AddExpression(x => x.DataInicioSituacao <= DataReferencia.Value && !x.DataExclusao.HasValue);
            }

            AddExpression(TokenSituacao, x => x.SituacaoMatricula.Token.Equals(TokenSituacao));
            AddExpression(SeqCicloLetivo, x => x.AlunoHistoricoCicloLetivo.SeqCicloLetivo == SeqCicloLetivo);
            AddExpression(TokenTipoSituacao, x => x.SituacaoMatricula.TipoSituacaoMatricula.Token.Equals(TokenTipoSituacao));
            AddExpression(SeqAlunoHistoricoCicloLetivo, x => x.SeqAlunoHistoricoCicloLetivo == SeqAlunoHistoricoCicloLetivo);
            AddExpression(Atual, x => x.Seq == x.AlunoHistoricoCicloLetivo
                .AlunoHistoricoSituacao.OrderByDescending(o => o.DataInicioSituacao)
                .FirstOrDefault(f => !f.DataExclusao.HasValue && f.DataInicioSituacao <= DataReferencia)
                .Seq);
            AddExpression(SeqsSituacaoMatricula, x => SeqsSituacaoMatricula.Contains(x.SeqSituacaoMatricula));
            AddExpression(DataInicioSituacao, x => x.DataInicioSituacao == this.DataInicioSituacao);

            if (this.FiltrarEntreCiclos.HasValue && this.FiltrarEntreCiclos.Value)
            {
                AddExpression(FiltrarEntreCiclos, x => x.AlunoHistoricoCicloLetivo.SeqCicloLetivo >= SeqCicloLetivoInicio && x.AlunoHistoricoCicloLetivo.SeqCicloLetivo <= SeqCicloLetivoFim);
            }

            return GetExpression();
        }

    }
}