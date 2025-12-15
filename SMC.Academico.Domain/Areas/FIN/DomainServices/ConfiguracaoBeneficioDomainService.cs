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
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.FIN.DomainServices
{
    public class ConfiguracaoBeneficioDomainService : AcademicoContextDomain<ConfiguracaoBeneficio>
    {
        #region Propriedade

        private const string INCLUSAO = "Inclusão";
        private const string ALTERAR = "Alteração";

        #endregion Propriedade

        #region DomaianService

        private InstituicaoNivelBeneficioDomainService InstituicaoNivelBeneficioDomainService
        {
            get { return this.Create<InstituicaoNivelBeneficioDomainService>(); }
        }

        private PessoaAtuacaoBeneficioDomainService PessoaAtuacaoBeneficioDomainService
        {
            get { return this.Create<PessoaAtuacaoBeneficioDomainService>(); }
        }

        #endregion DomaianService

        /// <summary>
        /// Verificar se existe configuração para o beneficio
        /// </summary>
        /// <param name="seqBeneficio">Sequencial do beneficio</param>
        /// <returns>sim ou não</returns>
        public bool VerificaConfiguracaoBeneficioNiveis(long seqBeneficio)
        {
            var spec = new ConfiguracaoBeneficioFilterSpecification() { SeqBeneficio = seqBeneficio };

            return this.Count(spec) > 0;
        }

        /// <summary>
        /// Buscar Configuracoes de beneficio conforme regra de negocio
        /// </summary>
        /// <param name="seqInstituicaoNivelBeneficio">sequicial instituicao nivel beneficio</param>
        /// <returns>Lista configuracao de beneficio conforme regra de negocio</returns>
        public SMCPagerData<ConfiguracaoBeneficioVO> BuscarConfiguracoesBeneficios(ConfiguracaoBeneficioFilterSpecification filtro)
        {
            int total = 0;

            var registro = this.SearchBySpecification(filtro, out total, IncludesConfiguracaoBeneficio.InstituicaoNivelBeneficio).TransformList<ConfiguracaoBeneficioVO>();

            if (registro.Any())
            {
                var spec = new ConfiguracaoBeneficioFilterSpecification
                {
                    SeqInstituicaoNivelBeneficio = filtro.SeqInstituicaoNivelBeneficio
                };

                spec.SetOrderBy(s => s.DataInicioValidade);

                var maiorDataInicio = this.SearchProjectionBySpecification(spec, p => p.DataInicioValidade).Max();

                registro.SMCForEach(f => f.FlagUltimaConfiguracao = f.DataInicioValidade == maiorDataInicio);
            }

            return new SMCPagerData<ConfiguracaoBeneficioVO>(registro, total);
        }

        /// <summary>
        /// Valida se beneficio pode ser configurado
        /// </summary>
        /// <param name="seqInstituicaoNivelBeneficio">Seq instituicao nivel beneficio</param>
        /// <returns>Retorna execption seguindo a regra de negocio</returns>
        public bool VerificarDeducaoBeneficio(long seqInstituicaoNivelBeneficio)
        {
            var specBeneficio = new SMCSeqSpecification<InstituicaoNivelBeneficio>(seqInstituicaoNivelBeneficio);

            var beneficio = this.InstituicaoNivelBeneficioDomainService.SearchProjectionByKey(specBeneficio, p => p.Beneficio);

            if (!beneficio.DeducaoValorParcelaTitular)
            {
                throw new InstituicaoNivelBeneficioDeducaoValorException();
            }

            return true;
        }

        /// <summary>
        /// Buscar a configuração beneficio referente a uma determinda instiuição nivel beneficio
        /// </summary>
        /// <param name="seqInstituicaoNivelBenefico">Seq intituição nivel beneficio</param>
        /// <returns>As configurações de um determinado nivel beneficio</returns>
        public ConfiguracaoBeneficio BuscarDadosConfiguracaoBeneficio(long seqInstituicaoNivelBenefico)
        {
            var dataAtual = DateTime.Today;

            var item = new ConfiguracaoBeneficio();

            var lista = ListaDadosConfiguracaoBeneficio(seqInstituicaoNivelBenefico);

            if (lista.Any())
            {
                //Não é possivel criar um novo registro se o registro anterior não tiver termino
                if (lista.Any(a => !a.DataFimValidade.HasValue))
                {
                    throw new ConfiguracaoBeneficioNovoException();
                }
            }

            return item;
        }

        /// <summary>
        /// Listar todas as configurações daquela instituicao nivel beneficio
        /// </summary>
        /// <param name="seqInstituicaoNivelBeneficio">Seq instituição nivel Beneficio</param>
        /// <returns>Lista de todas configurações de beneficios</returns>
        public List<ConfiguracaoBeneficio> ListaDadosConfiguracaoBeneficio(long seqInstituicaoNivelBeneficio)
        {
            var spec = new ConfiguracaoBeneficioFilterSpecification
            {
                SeqInstituicaoNivelBeneficio = seqInstituicaoNivelBeneficio
            };

            var lista = this.SearchBySpecification(spec).ToList();

            return lista;
        }

        /// <summary>
        /// Salvar a configuração de beneficio
        /// </summary>
        /// <param name="beneficioHistorico">Modelo configuração beneficio</param>
        /// <returns>Seq configuração beneficio</returns>
        public long SalvarConfiguracaoBeneficio(ConfiguracaoBeneficio configuracaoBeneficio)
        {
            var listaConfiguracaoBeneficio = ListaDadosConfiguracaoBeneficio(configuracaoBeneficio.SeqInstituicaoNivelBeneficio).OrderByDescending(o => o.DataFimValidade).ToList();

            if (listaConfiguracaoBeneficio.Count > 0)
            {
                ///Valida se é uma nova configuração
                if (configuracaoBeneficio.IsNew())
                {
                    ///Valida se a data é menor
                    if (configuracaoBeneficio.DataInicioValidade <= listaConfiguracaoBeneficio[0].DataFimValidade)
                    {
                        throw new ConfiguracaoBeneficioIncluirException();
                    }
                }
                else
                {
                    foreach (var item in listaConfiguracaoBeneficio)
                    {
                        ///Valida se existe algum valor auxilio cadastrado que seja menor ou igual ao cadastrado desde que
                        ///não seja ele mesmo
                        if (configuracaoBeneficio.DataInicioValidade <= item.DataFimValidade && configuracaoBeneficio.Seq != item.Seq)
                        {
                            throw new ConfiguracaoBeneficioAlterarException();
                        }
                    }
                }
            }

            foreach (var item in listaConfiguracaoBeneficio)
            {
                if (item.TipoDeducao != configuracaoBeneficio.TipoDeducao && item.Seq != configuracaoBeneficio.Seq)
                {
                    if (configuracaoBeneficio.IsNew())
                    {
                        throw new ConfiguracaoBeneficioTipoDeducaoException(INCLUSAO);
                    }
                    else
                    {
                        throw new ConfiguracaoBeneficioTipoDeducaoException(ALTERAR);
                    }
                }
            }

            if (configuracaoBeneficio.DataInicioValidade.Day != 1)
            {
                if (configuracaoBeneficio.IsNew())
                {
                    throw new ConfiguracaoBeneficioDataInicioException(INCLUSAO);
                }
                else
                {
                    throw new ConfiguracaoBeneficioDataInicioException(ALTERAR);
                }
            }

            /// Verifica se mais um dia na data irá se manter no mesmo mes e verifica se houve inclusão de data
            if (configuracaoBeneficio.DataFimValidade != null
                && ((DateTime)configuracaoBeneficio.DataFimValidade).AddDays(1).Month == ((DateTime)configuracaoBeneficio.DataFimValidade).Month)
            {
                if (configuracaoBeneficio.IsNew())
                {
                    throw new ConfiguracaoBeneficioDataFimException(INCLUSAO);
                }
                else
                {
                    throw new ConfiguracaoBeneficioDataFimException(ALTERAR);
                }
            }

            this.SaveEntity(configuracaoBeneficio);

            return configuracaoBeneficio.Seq;
        }

        /// <summary>
        /// Excluir Configuração Beneficio
        /// </summary>
        /// <param name="seq">seq configuração beneficio</param>
        public void ExcluirConfiguracaoBeneficio(long seq)
        {
            ConfiguracaoBeneficio configuracaoBeneficio = this.SearchByKey(new SMCSeqSpecification<ConfiguracaoBeneficio>(seq));

            //var secInstituicaoNivelBeneficio = new InstituicaoNivelBeneficioFilterSpecification { Seq = configuracaoBeneficio.SeqInstituicaoNivelBeneficio };

            //var instituicaoNivelBeneficio = InstituicaoNivelBeneficioDomainService.SearchBySpecification(secInstituicaoNivelBeneficio);

            //var secPessoaAtuacaoBeneficio = new PessoaAtuacaoBeneficioFilterSpecification { SeqBeneficio = instituicaoNivelBeneficio.First().SeqBeneficio };

            //var pessoaAtuacaoBeneficio = PessoaAtuacaoBeneficioDomainService.SearchBySpecification(secPessoaAtuacaoBeneficio);

            //if(pessoaAtuacaoBeneficio.Count() > 0)
            //{
            //    foreach(var item in pessoaAtuacaoBeneficio)
            //    {
            //        if((item.DataInicioVigencia >= configuracaoBeneficio.DataInicioValidade && item.DataInicioVigencia <= configuracaoBeneficio.DataFimValidade) ||
            //            (configuracaoBeneficio.DataInicioValidade >= item.DataInicioVigencia && configuracaoBeneficio.DataInicioValidade <= item.DataFimVigencia)){
            //            throw new ConfiguracaoBeneficioExcluirException();
            //        }
            //    }
            //}

            this.DeleteEntity(configuracaoBeneficio);
        }

        public bool VerificarAssociacaoPessoaBeneficio(long seq)
        {
            var secPessoaAtuacaoBeneficio = new PessoaAtuacaoBeneficioFilterSpecification { SeqConfiguracaoBeneficio = seq };

            var pessoaAtuacaoBeneficio = PessoaAtuacaoBeneficioDomainService.SearchBySpecification(secPessoaAtuacaoBeneficio);

            if (pessoaAtuacaoBeneficio.Count() > 0)
            {
                return true;
            }

            return false;
        }

        public ConfiguracaoBeneficioVO AlterarConfiguracoesBeneficios(long seq)
        {
            var configuracaoBeneficio = this.SearchByKey(new SMCSeqSpecification<ConfiguracaoBeneficio>(seq));

            var configuracaoBeneficioVO = configuracaoBeneficio.Transform<ConfiguracaoBeneficioVO>();

            if (VerificarAssociacaoPessoaBeneficio(seq))
            {
                configuracaoBeneficioVO.AssociacaoPessoaBeneficio = true;
            };

            configuracaoBeneficioVO.DataBanco = configuracaoBeneficio.DataFimValidade;

            return configuracaoBeneficioVO;
        }
    }
}