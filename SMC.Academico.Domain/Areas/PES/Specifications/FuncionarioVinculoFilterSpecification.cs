using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class FuncionarioVinculoFilterSpecification : SMCSpecification<FuncionarioVinculo>
    {
        public FuncionarioVinculoFilterSpecification()
        {
            SetOrderBy(o => o.TipoFuncionario.DescricaoMasculino);
        }

        public long? Seq { get; set; }
        public long? SeqFuncionario { get; set; }
        public long? SeqTipoFuncionario { get; set; }
        public List<long> SeqsTipoFuncionario { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string TokenTipoFuncionario { get; set; }
        public bool? VinculoAtivo { get; set; }
        public long? SeqEntidadeVinculo { get; set; }
        public bool? PossuiEntidade { get; set; }

        public override Expression<Func<FuncionarioVinculo, bool>> SatisfiedBy()
        {
            AddExpression(Seq, x => x.Seq == Seq);
            AddExpression(SeqFuncionario, x => x.SeqFuncionario == SeqFuncionario);
            AddExpression(SeqTipoFuncionario, x => x.SeqTipoFuncionario == SeqTipoFuncionario);
            AddExpression(SeqsTipoFuncionario, a => SeqsTipoFuncionario.Contains(a.SeqTipoFuncionario));
            AddExpression(DataInicio, x => x.DataInicio == DataInicio);
            AddExpression(DataFim, x => x.DataFim == DataFim);
            AddExpression(TokenTipoFuncionario, x => x.TipoFuncionario.Token == TokenTipoFuncionario);
            AddExpression(SeqEntidadeVinculo, a => a.EntidadeVinculo.Seq == SeqEntidadeVinculo);
            if (VinculoAtivo.HasValue && VinculoAtivo.Value)
            {
                var dataAtual = DateTime.Today;

                AddExpression(x => x.DataInicio <= dataAtual && (!x.DataFim.HasValue || x.DataFim >= dataAtual));
            }

            if (PossuiEntidade.HasValue && PossuiEntidade.Value)
            {
                AddExpression(a => a.SeqEntidadeVinculo != null);
            }

            return GetExpression();
        }
    }
}