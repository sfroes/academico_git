using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.APR.DomainServices
{
    public class InstituicaoNivelCriterioAprovacaoDomainService : AcademicoContextDomain<InstituicaoNivelCriterioAprovacao>
    {
        #region [ Serviços ]

        private InstituicaoNivelEscalaApuracaoDomainService InstituicaoNivelEscalaApuracaoDomainService
        {
            get { return this.Create<InstituicaoNivelEscalaApuracaoDomainService>(); }
        }

        private CriterioAprovacaoDomainService CriterioAprovacaoDomainService
        {
            get { return this.Create<CriterioAprovacaoDomainService>(); }
        }

        #endregion

        public List<SMCDatasourceItem> BuscarCriteriosAprovacaoDaInstituicaoNivelSelect(long seqInstituicaoNivel)
        {
            // Recupera os sequenciais das escalas associadas ao nível de ensino informado na instituição
            var specEscalaNivel = new InstituicaoNivelEscalaApuracaoFilterSpecification() { SeqInstituicaoNivel = seqInstituicaoNivel };
            var seqsEscalasNivel = this.InstituicaoNivelEscalaApuracaoDomainService
                .SearchBySpecification(specEscalaNivel)
                .Select(s => new Nullable<long>(s.SeqEscalaApuracao))
                .Distinct()
                .ToArray();

            // Monta spec para especificações associadas à escalas do nível informado ou sem escala
            var specCriterioAprovacaoComEscalaNivel = new SMCContainsSpecification<CriterioAprovacao, long?>(p => p.SeqEscalaApuracao, seqsEscalasNivel);
            var specCriterioAprovacaoSemEscala = new CriterioAprovacaoFilterSpecification() { SemEscalaApuracao = true };
            var specCriterioAprovacaoOr = new SMCOrSpecification<CriterioAprovacao>(specCriterioAprovacaoComEscalaNivel, specCriterioAprovacaoSemEscala);

            var criteriosAprovacaoSelect = this.CriterioAprovacaoDomainService
                .SearchBySpecification(specCriterioAprovacaoOr)
                .OrderBy(o => o.Descricao)
                .Select(s => new SMCDatasourceItem(s.Seq, s.Descricao))
                .ToList();

            return criteriosAprovacaoSelect;
        }
    }
}
