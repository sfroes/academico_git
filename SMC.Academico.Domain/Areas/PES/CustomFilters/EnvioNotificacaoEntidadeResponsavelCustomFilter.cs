using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System.Linq.Expressions;
using System;
using System.Linq;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.DCT.Models;

namespace SMC.Academico.Domain.Areas.PES.CustomFilters
{
    public class EnvioNotificacaoEntidadeResponsavelCustomFilter : SMCCustomFilter<EnvioNotificacao>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<EnvioNotificacao, bool>> SatisfiedBy()
        {
            return x => (x.TipoAtuacao == TipoAtuacao.Aluno &&
                            x.Destinatarios.Any(d => SeqsHierarquias.Contains((d.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(f => f.Atual).EntidadeVinculo.HierarquiasEntidades.FirstOrDefault().Seq))
                        ) ||
                        (x.TipoAtuacao == TipoAtuacao.Colaborador &&
                            x.Destinatarios.Any(d => (d.PessoaAtuacao as Colaborador).Vinculos.Any(a => SeqsHierarquias.Contains(a.EntidadeVinculo.HierarquiasEntidades.FirstOrDefault().Seq)))
                        );
        }

    }
}