using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class ConfiguracaoEventoLetivoItemFilterSpecification : SMCSpecification<ConfiguracaoEventoLetivoItem>
    {
        public long? SeqCicloLetivo { get; set; }

        public long? SeqCursoOfertaLocalidadeTurno { get; set; }

        public TipoAluno? TipoAluno { get; set; }

        public string TokenTipoEvento { get; set; }

        public DateTime? DataReferencia { get; set; }

        public override Expression<Func<ConfiguracaoEventoLetivoItem, bool>> SatisfiedBy()
        {
            AddExpression(SeqCicloLetivo, x => x.ConfiguracaoEventoLetivo.SeqCicloLetivo == SeqCicloLetivo);
            AddExpression(SeqCursoOfertaLocalidadeTurno, x => x.ConfiguracaoEventoLetivo.SeqCursoOfertaLocalidadeTurno == SeqCursoOfertaLocalidadeTurno);
            AddExpression(TipoAluno, x => !x.ConfiguracaoEventoLetivo.TipoAluno.HasValue || x.ConfiguracaoEventoLetivo.TipoAluno == TipoAluno);

            if (!string.IsNullOrEmpty(TokenTipoEvento))
            {
                if (!DataReferencia.HasValue)
                    AddExpression(x => x.EventoLetivo.CicloLetivoTipoEvento.InstituicaoTipoEvento.Token == TokenTipoEvento);
                else
                {
                    // Eventos letivos não são cadastrados com horário. Desconsidera a hora da data de referencia
                    this.DataReferencia = this.DataReferencia.Value.Date;

                    AddExpression(x => x.EventoLetivo.CicloLetivoTipoEvento.InstituicaoTipoEvento.Token == TokenTipoEvento &&
                                       x.EventoLetivo.DataInicio <= DataReferencia.Value &&
                                       x.EventoLetivo.DataFim >= DataReferencia.Value);
                }
            }

            return GetExpression();
        }

    }
}