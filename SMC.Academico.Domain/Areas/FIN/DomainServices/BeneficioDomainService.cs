using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Common.Areas.FIN.Exceptions;
using SMC.Academico.Common.Areas.FIN.Includes;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.FIN.ValueObjects;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.FIN.DomainServices
{
    public class BeneficioDomainService : AcademicoContextDomain<Beneficio>
    {
        #region [ Services ]

        private InstituicaoNivelBeneficioDomainService InstituicaoNivelBeneficioDomainService
        {
            get { return this.Create<InstituicaoNivelBeneficioDomainService>(); }
        }

        private ConfiguracaoBeneficioDomainService ConfiguracaoBeneficioDomainService
        {
            get { return this.Create<ConfiguracaoBeneficioDomainService>(); }
        }

        private PessoaAtuacaoBeneficioDomainService PessoaAtuacaoBeneficioDomainService { get => Create<PessoaAtuacaoBeneficioDomainService>(); }

        #endregion [ Services ]

        /// <summary>
        /// Busca um beneficio
        /// </summary>
        /// <param name="seq">Sequencial do beneficio</param>
        /// <returns>Dados do beneficio</returns>
        public BeneficioVO BuscarBeneficio(long seq)
        {
            var beneficioVO = this.SearchByKey(new SMCSeqSpecification<Beneficio>(seq), IncludesBeneficio.ResponsaveisFinanceiros).Transform<BeneficioVO>();
            return beneficioVO;
        }

        /// <summary>
        /// Busca os benefícios por nível de ensino na instituição
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Dados dos benefícios do nível de ensino informado</returns>
        public List<SMCDatasourceItem> BuscarBeneficioPorNivelEnsinoSelect(long seqNivelEnsino)
        {
            var spec = new InstituicaoNivelBeneficioFilterSpecification() { SeqNivelEnsino = seqNivelEnsino };
            spec.SetOrderBy(o => o.Beneficio.Descricao);

            return this.InstituicaoNivelBeneficioDomainService
                .SearchProjectionBySpecification(spec, p => new SMCDatasourceItem() { Seq = p.SeqBeneficio, Descricao = p.Beneficio.Descricao }).ToList();
        }

        /// <summary>
        /// Salvar um benefício
        /// </summary>
        /// <param name="beneficio">Dados do beneficio a ser salva</param>
        /// <returns>Sequencial do beneficio</returns>
        public long SalvarBeneficio(Beneficio beneficio)
        {
            if (beneficio.Seq > 0)
            {
                this.ValidarCampos(beneficio);

                if (beneficio.AssociarResponsavelFinanceiro != Common.Areas.FIN.Enums.AssociarResponsavelFinanceiro.Exige)
                {
                    beneficio.ResponsaveisFinanceiros = new List<PessoaJuridica>();
                }
            }

            this.SaveEntity(beneficio);

            return beneficio.Seq;
        }

        public void ValidarCampos(Beneficio beneficio)
        {
            var beneficioBD = this.SearchByKey(new SMCSeqSpecification<Beneficio>(beneficio.Seq), IncludesBeneficio.ResponsaveisFinanceiros);

            if (beneficioBD.DeducaoValorParcelaTitular != beneficio.DeducaoValorParcelaTitular)
            {
                if (!beneficio.DeducaoValorParcelaTitular)
                {
                    if (this.ConfiguracaoBeneficioDomainService.VerificaConfiguracaoBeneficioNiveis(beneficio.Seq))
                    {
                        throw new BeneficioDeducaoValorParcelaTitularSimException();
                    }
                }
            }

            //TODO: Implentar UC_FIN_001_02_02.Salvar quando estiver disponivel

            if (beneficio.AssociarResponsavelFinanceiro == AssociarResponsavelFinanceiro.NaoPermite)
            {
                var beneficiosAssociados = this.PessoaAtuacaoBeneficioDomainService.SearchBySpecification(new PessoaAtuacaoBeneficioFilterSpecification() { SeqBeneficio = beneficio.Seq });

                if (beneficiosAssociados.SMCAny())
                {
                    if (beneficioBD.ResponsaveisFinanceiros.Count() > 0)
                    {
                        throw new BeneficioResponsavelFinanceiroException();
                    }
                }
            }
        }

        /// <summary>
        /// Busca beneficios
        /// </summary>
        /// <returns>Lista de benefícios</returns>
        public List<SMCDatasourceItem> BuscarBeneficiosSelect()
        {
            return SearchAll().Select(p => new SMCDatasourceItem()
            {
                Seq = p.Seq,
                Descricao = p.Descricao
            }).OrderBy(o => o.Descricao).ToList();
        }
    }
}