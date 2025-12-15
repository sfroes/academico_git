using SMC.Academico.Common.Areas.FIN.Exceptions;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.FIN.ValueObjects;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.FIN.DomainServices
{
    public class BeneficioHistoticoValorAuxilioDomainService : AcademicoContextDomain<BeneficioHistoricoValorAuxilio>
    {
        #region Propriedade

        private const string INCLUSAO = "Inclusão";
        private const string ALTERAR = "Alteração";

        #endregion Propriedade

        #region [ Services ]

        private ConfiguracaoBeneficioDomainService ConfiguracaoBeneficioDomainService
        {
            get { return this.Create<ConfiguracaoBeneficioDomainService>(); }
        }

        #endregion [ Services ]

        /// <summary>
        /// Buscar o valor de auxilio rererente a uma determinda instiuição nivel beneficio
        /// </summary>
        /// <param name="seqInstituicaoNivelBenefico">Seq intituição nivel beneficio</param>
        /// <returns>Os valores auxilio de um determinado nivel beneficio</returns>
        public BeneficioHistoricoValorAuxilio BuscarDadosValorAuxilio(long seqInstituicaoNivelBenefico)
        {
            var dataAtual = DateTime.Today;

            var item = new BeneficioHistoricoValorAuxilio();

            var lista = ListaDadosValorAuxlio(seqInstituicaoNivelBenefico);

            if (lista.Any())
            {
                //Não é possivel criar um novo registro após o registro anterior sem termino
                if (lista.Any(a => !a.DataFimValidade.HasValue))
                {
                    throw new BeneficioHistoricoValorAuxilioNovoException();
                }

                item.DataInicioValidade = lista.Max(m => m.DataFimValidade.Value).AddDays(1);
            }
            else
            {
                item.DataInicioValidade = dataAtual;
            }

            return item;
        }

        /// <summary>
        /// Listar todos os valores daquela instituicao nivel beneficio
        /// </summary>
        /// <param name="seqInstituicaoNivelBeneficio">Seq instituição nivel Beneficio</param>
        /// <returns>Lista de todos os valores auxilios</returns>
        public List<BeneficioHistoricoValorAuxilio> ListaDadosValorAuxlio(long seqInstituicaoNivelBeneficio)
        {
            var spec = new BeneficioHistoricoValorAuxilioFilterSpecification
            {
                SeqInstituicaoNivelBeneficio = seqInstituicaoNivelBeneficio
            };

            var lista = this.SearchBySpecification(spec).ToList();

            return lista;
        }

        /// <summary>
        /// Paginação dos valores auxilios de uma determinada intituição nivel beneficio
        /// </summary>
        /// <param name="filtro">Fitros</param>
        /// <returns>Lista paginada de valores auxilio de uma determianda instituição nivel beneficio </returns>
        public SMCPagerData<BeneficioHistoricoValorAuxilioVO> BuscarDadosValoresAuxilio(BeneficioHistoricoValorAuxilioFilterSpecification filtro)
        {
            int total = 0;

            var lista = this.SearchBySpecification(filtro, out total).TransformList<BeneficioHistoricoValorAuxilioVO>();

            if (lista.Count > 0)
            {
                var spec = new BeneficioHistoricoValorAuxilioFilterSpecification
                {
                    SeqInstituicaoNivelBeneficio = filtro.SeqInstituicaoNivelBeneficio
                };

                spec.SetOrderBy(s => s.DataInicioValidade);

                var maiorDataInicio = this.Max(spec, p => p.DataInicioValidade);

                lista.SMCForEach(f => f.FlagUltimoValorAuxilio = f.DataInicioValidade == maiorDataInicio);
            }

            return new SMCPagerData<BeneficioHistoricoValorAuxilioVO>(lista, total);
        }

        /// <summary>
        /// Salvar o historico valor auxilio
        /// </summary>
        /// <param name="beneficioHistorico">Modelo benefico historico valor auxlio</param>
        /// <returns>Seq beneficio historico valor auxilio</returns>
        public long SalvarBeneficioHistoricoValorAuxilio(BeneficioHistoricoValorAuxilio beneficioHistorico)
        {
            ///Lista todos os auxilios daquele nivel de beneficio em ordem decrescente na data fim
            var listaHistoricosValoresAuxilios = ListaDadosValorAuxlio(beneficioHistorico.SeqInstituicaoNivelBeneficio).OrderByDescending(o => o.DataFimValidade).ToList();

            ///Verifica se existe algum auxilio
            if (listaHistoricosValoresAuxilios.Count > 0)
            {
                ///Valida se é um novo Auxilio
                if (beneficioHistorico.IsNew())
                {
                    ///Valida se a data é menor
                    if (beneficioHistorico.DataInicioValidade <= listaHistoricosValoresAuxilios[0].DataFimValidade)
                    {
                        throw new BeneficioHistoricoValorAuxilioIncluirException();
                    }
                }
                else
                {
                    foreach (var item in listaHistoricosValoresAuxilios)
                    {
                        ///Valida se existe algum valor auxilio cadastrado que seja menor ou igual ao cadastrado desde que
                        ///não seja ele mesmo
                        if (beneficioHistorico.DataInicioValidade <= item.DataFimValidade && beneficioHistorico.Seq != item.Seq)
                        {
                            throw new BeneficioHistoricoValorAuxilioAlterarException();
                        }
                    }
                }
            }

            if (beneficioHistorico.DataInicioValidade.Day != 1)
            {
                if (beneficioHistorico.IsNew())
                {
                    throw new ConfiguracaoBeneficioDataInicioException(INCLUSAO);
                }
                else
                {
                    throw new ConfiguracaoBeneficioDataInicioException(ALTERAR);
                }
            }

            /// Verifica se mais um dia na data irá se manter no mesmo mes e verifica se houve inclusão de data
            if (beneficioHistorico.DataFimValidade != null
                && ((DateTime)beneficioHistorico.DataFimValidade).AddDays(1).Month == ((DateTime)beneficioHistorico.DataFimValidade).Month)
            {
                if (beneficioHistorico.IsNew())
                {
                    throw new ConfiguracaoBeneficioDataFimException(INCLUSAO);
                }
                else
                {
                    throw new ConfiguracaoBeneficioDataFimException(ALTERAR);
                }
            }

            this.SaveEntity(beneficioHistorico);

            return beneficioHistorico.Seq;
        }
    }
}