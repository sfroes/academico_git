using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Mapper;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class EventoLetivoSpecification : SMCSpecification<EventoLetivo>, ISMCMappable
    {
        public long SeqCicloLetivo { get; set; }

        public string TokenTipoEvento { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqLocalidade { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public TipoAluno? TipoAluno { get; set; }

        public override Expression<Func<EventoLetivo, bool>> SatisfiedBy()
        {
            AddExpression(p => p.CicloLetivoTipoEvento.SeqCicloLetivo == this.SeqCicloLetivo);
            AddExpression(p => p.CicloLetivoTipoEvento.InstituicaoTipoEvento.Token == this.TokenTipoEvento
                            && p.CicloLetivoTipoEvento.InstituicaoTipoEvento.SeqInstituicaoEnsino == this.SeqInstituicaoEnsino);
            AddExpression(this.SeqNivelEnsino, p => p.NiveisEnsino.Any(n => n.Seq == SeqNivelEnsino.Value));
            AddExpression(this.TipoAluno, p => p.TiposAluno.Any(t => t.TipoAluno == this.TipoAluno.Value));
            AddExpression(p => p.ParametrosEntidade
                            .Any(f =>
                                    (!SeqLocalidade.HasValue || (!p.CicloLetivoTipoEvento.Parametros.Any(h => h.InstituicaoTipoEventoParametro.TipoParametroEvento == TipoParametroEvento.Localidade))
                                                                    || (f.TipoParametroEvento == TipoParametroEvento.Localidade && f.SeqEntidade == SeqLocalidade.Value))

                                    && (!SeqEntidadeResponsavel.HasValue || (!p.CicloLetivoTipoEvento.Parametros.Any(h => h.InstituicaoTipoEventoParametro.TipoParametroEvento == TipoParametroEvento.EntidadeResponsavel))
                                                                    || (f.TipoParametroEvento == TipoParametroEvento.EntidadeResponsavel && f.SeqEntidade == SeqEntidadeResponsavel.Value))

                                    && (!SeqCursoOfertaLocalidade.HasValue || (!p.CicloLetivoTipoEvento.Parametros.Any(h => h.InstituicaoTipoEventoParametro.TipoParametroEvento == TipoParametroEvento.CursoOfertaLocalidade))
                                                                    || (f.TipoParametroEvento == TipoParametroEvento.CursoOfertaLocalidade && f.SeqEntidade == SeqCursoOfertaLocalidade.Value))));

            return GetExpression();
        }
    }
}
