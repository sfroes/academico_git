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
using System;
using System.Linq;

namespace SMC.Academico.Domain.Areas.FIN.DomainServices
{
    public class InstituicaoNivelBeneficioDomainService : AcademicoContextDomain<InstituicaoNivelBeneficio>
    {
        #region DomaianService

        private BeneficioDomainService BeneficioDomainService
        {
            get { return this.Create<BeneficioDomainService>(); }
        }

        private PessoaAtuacaoBeneficioDomainService PessoaAtuacaoBeneficioDomainService
        {
            get { return this.Create<PessoaAtuacaoBeneficioDomainService>(); }
        }

        #endregion DomaianService

        /// <summary>
        /// Salvar uma instituição nivel beneficio e suas entidades
        /// </summary>
        /// <param name="instituicaoNivelBeneficio">Dados da instituição nivel beneficio a ser salva</param>
        /// <returns>Sequencial da instituição nivel beneficio</returns>
        public long SalvarInstituicaoNivelBeneficio(InstituicaoNivelBeneficioVO instituicaoNivelBeneficioVO)
        {
            var instituicaoNivelBeneficio = instituicaoNivelBeneficioVO.Transform<InstituicaoNivelBeneficio>();

            /// Removido por mudança de regra de negocio
            //var beneficio = BeneficioDomainService.BuscarBeneficio(instituicaoNivelBeneficio.SeqBeneficio);

            //if (instituicaoNivelBeneficio.IsNew() && beneficio.DeducaoValorParcelaTitular)
            //{
            //    instituicaoNivelBeneficio.ConfiguracoesBeneficio = new List<ConfiguracaoBeneficio>();
            //    instituicaoNivelBeneficio.ConfiguracoesBeneficio.Add(
            //        new ConfiguracaoBeneficio
            //        {
            //            DataFimValidade = instituicaoNivelBeneficioVO.DataFimValidade,
            //            DataInicioValidade = instituicaoNivelBeneficioVO.DataInicioValidade,
            //            FormaDeducao = instituicaoNivelBeneficioVO.FormaDeducao,
            //            TipoDeducao = instituicaoNivelBeneficioVO.TipoDeducao,
            //            ValorDeducao = instituicaoNivelBeneficioVO.ValorDeducao
            //        });
            //}

            //this.ValidarCamposObrigatorios(pessoaJuridica);

            this.SaveEntity(instituicaoNivelBeneficio);

            return instituicaoNivelBeneficio.Seq;
        }

        public SMCPagerData<InstituicaoNivelBeneficio> BuscarInstituicoesNieveisBeneficios(InstituicaoNivelBeneficioFilterSpecification filtros)
        {
            int total = 0;

            var dataAtual = DateTime.Today;

            var includes = IncludesInstituicaoNivelBeneficio.BeneficiosHistoricosValoresAuxilio
                         | IncludesInstituicaoNivelBeneficio.ConfiguracoesBeneficio
                         | IncludesInstituicaoNivelBeneficio.Beneficio
                         | IncludesInstituicaoNivelBeneficio.InstituicaoNivel_NivelEnsino;

            var result = this.SearchBySpecification(filtros, out total, includes).ToList();

            result.SMCForEach(beneficio =>
            {
                beneficio.BeneficiosHistoricosValoresAuxilio = beneficio.BeneficiosHistoricosValoresAuxilio.Where(w => w.DataInicioValidade <= dataAtual
                                                                                                                  && (!w.DataFimValidade.HasValue ||
                                                                                                                      w.DataFimValidade >= dataAtual)).ToList();

                beneficio.ConfiguracoesBeneficio = beneficio.ConfiguracoesBeneficio.Where(w => w.DataInicioValidade <= dataAtual
                                                                                          && (!w.DataFimValidade.HasValue ||
                                                                                              w.DataFimValidade >= dataAtual)).ToList();
            });

            return new SMCPagerData<InstituicaoNivelBeneficio>(result, total);
        }

        public void ExcluirInstituicoesNiveisBeneficios(long seq)
        {
            InstituicaoNivelBeneficio registro = this.SearchByKey(new SMCSeqSpecification<InstituicaoNivelBeneficio>(seq));

            var sec = new PessoaAtuacaoBeneficioFilterSpecification
            {
                SeqBeneficio = registro.SeqBeneficio
            };

            if (PessoaAtuacaoBeneficioDomainService.Count(sec) > 0)
                throw new InstituicaoNivelPessoaAtuacaoBeneficioException();

            this.DeleteEntity(registro);
        }
    }
}