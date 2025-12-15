using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Framework;
using SMC.Framework.Specification;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.DCT.Specifications
{
    public class RelatorioLogAtualizacaoColaboradorFilterSpecification : SMCSpecification<LogAtualizacaoColaborador>
    {
        public DateTime? DataInicioReferencia { get; set; }

        public DateTime? DataFimReferencia { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqColaborador { get; set; }

        public override Expression<Func<LogAtualizacaoColaborador, bool>> SatisfiedBy()
        {
            DateTime? dataHoraFimReferencia = DataFimReferencia?.Date.AddDays(1);
            AddExpression(DataInicioReferencia, w => w.DataProcessamento >= this.DataInicioReferencia); 
            AddExpression(dataHoraFimReferencia, w => w.DataProcessamento < dataHoraFimReferencia);
            AddExpression(SeqsEntidadesResponsaveis, w => w.Vinculos.Any(v => this.SeqsEntidadesResponsaveis.Contains(v.ColaboradorVinculo.SeqEntidadeVinculo)));
            AddExpression(SeqColaborador, w => w.SeqColaborador == this.SeqColaborador);

            return GetExpression();
        }
    }
}