using SMC.Academico.Common.Areas.APR.Includes;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.APR.Services
{
    public class InstituicaoNivelEscalaApuracaoService : SMCServiceBase, IInstituicaoNivelEscalaApuracaoService
    {
        #region [ Serviços ]

        private InstituicaoNivelEscalaApuracaoDomainService InstituicaoNivelEscalaApuracaoDomainService
        {
            get { return this.Create<InstituicaoNivelEscalaApuracaoDomainService>(); }
        }

        private EscalaApuracaoDomainService EscalaApuracaoDomainService
        {
            get { return this.Create<EscalaApuracaoDomainService>(); }
        }

        #endregion

        public List<SMCDatasourceItem> BuscarEscalaApuracaoFinalDaInstituicao()
        {
            // Sequenciais de todas EscalaApuracao associadas a InstituicaoNivel da instituição atual
            var seqsEscalaApuracaoInstituicao = this.InstituicaoNivelEscalaApuracaoDomainService
                .SearchBySpecification(new SMCTrueSpecification<InstituicaoNivelEscalaApuracao>(), IncludesInstituicaoNivelEscalaApuracao.InstituicaoNivel)
                .Select(s => s.SeqEscalaApuracao)
                .Distinct()
                .ToArray();

            // Escalas de Appuração final
            var specFinal = new EscalaApuracaoFilterSpecification() { ApuracaoFinal = true };
            // Escalas de Apuração associadas à Instituição
            var specInstituicao = new SMCContainsSpecification<EscalaApuracao, long>(p => p.Seq, seqsEscalaApuracaoInstituicao);
            var specFinalInstituicao = new SMCAndSpecification<EscalaApuracao>(specFinal, specInstituicao);

            var selectItens = this.EscalaApuracaoDomainService
                .SearchBySpecification(specFinalInstituicao)
                .Select(s => new SMCDatasourceItem(s.Seq, s.Descricao))
                .OrderBy(o => o.Descricao)
                .ToList();

            return selectItens;
        }
    }
}
