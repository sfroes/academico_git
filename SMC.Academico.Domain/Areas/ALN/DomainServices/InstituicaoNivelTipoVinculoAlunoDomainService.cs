using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class InstituicaoNivelTipoVinculoAlunoDomainService : AcademicoContextDomain<InstituicaoNivelTipoVinculoAluno>
    {
        #region [ DomainService ]

        private AlunoDomainService AlunoDomainService => this.Create<AlunoDomainService>();

        private IngressanteDomainService IngressanteDomainService => this.Create<IngressanteDomainService>();

        private InstituicaoNivelTipoProcessoSeletivoDomainService InstituicaoNivelTipoProcessoSeletivoDomainService => this.Create<InstituicaoNivelTipoProcessoSeletivoDomainService>();

        private InstituicaoNivelTipoTermoIntercambioDomainService InstituicaoNivelTipoTermoIntercambioDomainService => this.Create<InstituicaoNivelTipoTermoIntercambioDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => this.Create<PessoaAtuacaoDomainService>();

        private ProcessoSeletivoDomainService ProcessoSeletivoDomainService => this.Create<ProcessoSeletivoDomainService>();

        #endregion [ DomainService ]

        public long SalvarInstituicaoNivelTipoVinculoAluno(InstituicaoNivelTipoVinculoAluno dominio)
        {
            ValidarRegras(dominio);

            this.SaveEntity(dominio);

            return dominio.Seq;
        }

        private void ValidarRegras(InstituicaoNivelTipoVinculoAluno dominio)
        {
            //O campo "Exige oferta de matriz curricular?"(Parâmetros Gerais) só poderá assumir o"Sim",
            //se o valor do campo "Exige curso?"(Parâmetros Gerais) for "Sim"
            if (!dominio.ExigeCurso && dominio.ExigeOfertaMatrizCurricular)
                throw new ExigeOfertaMatrizCurricularExigeCursoException();

            //O campo "Concede formação?"(Parâmetros Gerais) só poderá assumir o valor "Sim", se o valor do
            //campo "Exige oferta de matriz curricular?"(Parâmetros Gerais) for "Sim".
            if (!dominio.ExigeOfertaMatrizCurricular && dominio.ConcedeFormacao)
                throw new ExigeOfertaMatrizCurricularConcedeFormacaoException();

            if (dominio.TiposTermoIntercambio != null && dominio.TiposTermoIntercambio.Count > 0)
            {
                foreach (var item in dominio.TiposTermoIntercambio)
                {
                    if (!dominio.ExigeOfertaMatrizCurricular && item.ConcedeFormacao)
                    {
                        //O campo "Concede formação?"(Passo 3-Tipo de Termo de Intercâmbio) só poderá assumir o valor "Sim", se o valor
                        //do campo "Exige oferta de matriz curricular?"(Parâmetros Gerais) for "Sim"
                        throw new ConcedeFormacaoTermoIntercambioException();
                    }

                    if (item.ConcedeFormacao && item.ExigePeriodoIntercambioTermo)
                    {
                        //Se o valor do campo "Concede formação?" for "Sim", setar o valor "Não" para o campo
                        //"Exige período de intercâmbio no termo ? " e desabilitá-lo para alteração.
                        throw new ConcedeFormacaoExigePeriodoException();
                    }

                    if (!item.ConcedeFormacao && !item.ExigePeriodoIntercambioTermo)
                    {
                        //Se o valor do campo "Concede formação?" for "Não", setar o valor "Sim" para o campo
                        //"Exige período de intercâmbio no termo ? " e desabilitá-lo para alteração.
                        throw new NaoConcedeFormacaoNaoExigePeriodoException();
                    }

                    var registroValidacao = InstituicaoNivelTipoTermoIntercambioDomainService.ValidarTermoIntercambioInstituicaoNivel(dominio.Seq, item.SeqTipoTermoIntercambio, dominio.SeqInstituicaoNivel, (bool)item.ConcedeFormacao);
                    if (registroValidacao != null && registroValidacao.Count > 0)
                    {
                        string mensagem = $"</br> { string.Join(",</br> ", registroValidacao)} </br>";
                        throw new ConcedeFormacaoInstituicaoValorDiferenteException(mensagem);
                    }
                }
            }
        }

        /// <summary>
        /// Busca instituição nível tipo vínculo aluno de acordo com o sequencial do ingressante para validar parâmetros na solicitação de matrícula
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial do ingressante</param>
        /// <returns>Parâmetros de instituição nível tipo vínculo aluno</returns>
        public InstituicaoNivelTipoVinculoAlunoVO BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(long seqPessoaAtuacao, bool desativarFiltroDados = false)
        {
            //Recupera os sequenciais do ingressante para recuperar os parâmetros de acordo com o vínculo
            var sequenciais = PessoaAtuacaoDomainService.BuscarInstituicaoNivelEnsinoESequenciaisPorPessoaAtuacao(seqPessoaAtuacao, desativarFiltroDados);

            var filtro = new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
            {
                SeqInstituicao = sequenciais.SeqInstituicao,
                SeqNivelEnsino = sequenciais.SeqNivelEnsino,
                SeqTipoVinculoAluno = sequenciais.SeqTipoVinculoAluno
            };

            var registro = this.SearchBySpecification(filtro, IncludesInstituicaoNivelTipoVinculoAluno.InstituicaoNivel
                | IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio
                | IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio_TipoTermoIntercambio
                | IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao).FirstOrDefault();

            return registro.Transform<InstituicaoNivelTipoVinculoAlunoVO>();
        }

        /// <summary>
        /// Busca o vínculo de aluno pelo tipo e nível de ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Nível de ensino do vínculo</param>
        /// <param name="seqTipoVinculoAluno">Sequencial do tipo de vínculo</param>
        /// <returns>Configuração do vínculo par o tipo e nível informada na instituição</returns>
        public InstituicaoNivelTipoVinculoAluno BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(long seqNivelEnsino, long seqTipoVinculoAluno)
        {
            var filtro = new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
            {
                SeqNivelEnsino = seqNivelEnsino,
                SeqTipoVinculoAluno = seqTipoVinculoAluno
            };

            return SearchByKey(filtro, IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio);
        }

        /// <summary>
        /// Busca o vínculo de aluno pelo tipo e nível de ensino
        /// </summary>
        /// <param name="seqInstituicao">Sequencial da institução de ensino</param>
        /// <param name="seqNivelEnsino">Nível de ensino do vínculo</param>
        /// <param name="seqTipoVinculoAluno">Sequencial do tipo de vínculo</param>
        /// <param name="includes">Includes para retorno de dados</param>
        /// <returns>Configuração do vínculo par o tipo e nível informada na instituição</returns>
        public InstituicaoNivelTipoVinculoAluno BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(long seqInstituicao, long seqNivelEnsino, long seqTipoVinculoAluno, Enum includes)
        {
            var filtro = new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
            {
                SeqInstituicao = seqInstituicao,
                SeqNivelEnsino = seqNivelEnsino,
                SeqTipoVinculoAluno = seqTipoVinculoAluno
            };

            return SearchByKey(filtro, includes);
        }

        /// <summary>
        /// Busca os tipos de vínculos das forma de ingresso associadas ao tipo do processo seletivo informado
        /// </summary>
        /// <param name="seqProcessoSeletivo">Sequencial do processo seletivo</param>
        /// <returns>Dados dos vínculos</returns>
        public List<SMCDatasourceItem> BuscarTipoVinculoAlunoPorProcesso(long seqProcessoSeletivo)
        {
            // Busca o tipo do processo seletivo
            var specProcesso = new SMCSeqSpecification<ProcessoSeletivo>(seqProcessoSeletivo);
            var seqTipoProcesso = this.ProcessoSeletivoDomainService.SearchProjectionByKey(specProcesso, p => p.SeqTipoProcessoSeletivo);

            //TODO: Verificar regra  UC_ALN_002_01_02.NV14 -Manter Ingressante
            // Busca o tipo de vínculo da forma de ingresso do tipo do processo seletivo
            var specTipoProcesso = new InstituicaoNivelTipoProcessoSeletivoFilterSpecification() { SeqTipoProcessoSeletivo = seqTipoProcesso };
            var tipoVinculo = this.InstituicaoNivelTipoProcessoSeletivoDomainService
                .SearchProjectionByKey(specTipoProcesso, p => p.InstituicaoNivelFormaIngresso.InstituicaoNivelTipoVinculoAluno.TipoVinculoAluno);

            var retorno = new List<SMCDatasourceItem>();

            if (tipoVinculo != null)
                retorno.Add(new SMCDatasourceItem()
                {
                    Seq = tipoVinculo.Seq,
                    Descricao = tipoVinculo.Descricao
                });

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarTipoVinculoPorNivelEnsino(long seqNivelEnsino)
        {
            var filtro = new InstituicaoNivelTipoVinculoAlunoFilterSpecification() { SeqNivelEnsino = seqNivelEnsino };

            var includes = IncludesInstituicaoNivelTipoVinculoAluno.TipoVinculoAluno;

            var result = this.SearchBySpecification(filtro, includes).ToList();

            var retorno = new List<SMCDatasourceItem>();

            foreach (var item in result)
            {
                retorno.Add(new SMCDatasourceItem
                {
                    Seq = item.TipoVinculoAluno.Seq,
                    Descricao = item.TipoVinculoAluno.Descricao
                });
            }

            return retorno.SMCDistinct(d => d.Seq).ToList();
        }

        /// <summary>
        /// Buscar os tipos de vinculos no qual exista tipo de orientação que esteja configurado para permitir manutenção manual da orientação
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nivel de ensino</param>
        /// <returns></returns>
        public List<SMCDatasourceItem> BuscarTipoVinculoPorNivelEnsinoPermiteManutencaoManual(long seqNivelEnsino)
        {
            var filtro = new InstituicaoNivelTipoVinculoAlunoFilterSpecification() { SeqNivelEnsino = seqNivelEnsino };

            var includes = IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao
                           | IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao_TipoOrientacao
                           | IncludesInstituicaoNivelTipoVinculoAluno.TipoVinculoAluno;

            var result = this.SearchBySpecification(filtro, includes).ToList();

            var retorno = new List<SMCDatasourceItem>();

            foreach (var item in result)
            {
                foreach (var tipoOrientacao in item.TiposOrientacao)
                {
                    if (tipoOrientacao.TipoOrientacao.PermiteManutencaoManual)
                    {
                        retorno.Add(new SMCDatasourceItem
                        {
                            Seq = item.TipoVinculoAluno.Seq,
                            Descricao = item.TipoVinculoAluno.Descricao
                        });
                    }
                }
            }
            return retorno.SMCDistinct(d => d.Seq).ToList();
        }

        /// <summary>
        /// Buscar os tipos de intercambio no qual exista tipo de orientação que esteja configurado para permitir manutenção manual da orientação
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nivel de ensino</param>
        /// <param name="seqTipoVinculo">Sequencial tipo vinculo</param>
        /// <returns></returns>
        public List<SMCDatasourceItem> BuscarTermoIntercambioPorNivelEnsinoPermiteManutencaoManual(long seqNivelEnsino, long seqTipoVinculo)
        {
            var filtro = new InstituicaoNivelTipoVinculoAlunoFilterSpecification() { SeqNivelEnsino = seqNivelEnsino, SeqTipoVinculoAluno = seqTipoVinculo };

            var includes = IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao
                           | IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao_TipoOrientacao
                           | IncludesInstituicaoNivelTipoVinculoAluno.TipoVinculoAluno
                           | IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio
                           | IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio_TipoTermoIntercambio
                           | IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao_InstituicaoNivelTipoTermoIntercambio
                           | IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao_InstituicaoNivelTipoTermoIntercambio_TipoTermoIntercambio;

            var result = this.SearchBySpecification(filtro, includes).ToList();

            var retorno = new List<SMCDatasourceItem>();

            foreach (var item in result)
            {
                foreach (var tipoOrientacao in item.TiposOrientacao)
                {
                    if (tipoOrientacao.TipoOrientacao.PermiteManutencaoManual)
                    {
                        if (tipoOrientacao.InstituicaoNivelTipoTermoIntercambio != null)
                        {
                            retorno.Add(new SMCDatasourceItem
                            {
                                Seq = tipoOrientacao.InstituicaoNivelTipoTermoIntercambio.TipoTermoIntercambio.Seq,
                                Descricao = tipoOrientacao.InstituicaoNivelTipoTermoIntercambio.TipoTermoIntercambio.Descricao,
                            });
                        }
                    }
                }
            }

            return retorno.SMCDistinct(d => d.Seq).ToList();
        }

        /// <summary>
        /// Buscar os tipos de operação no qual exista tipo de orientação que esteja configurado para permitir manutenção manual da orientação
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nivel de ensino</param>
        /// <param name="seqTipoVinculo">Sequencial tipo vinculo</param>
        /// <param name="SeqTipoTermoIntercambio">Sequencial tipo de intercambio</param>
        /// <returns></returns>
        public List<SMCDatasourceItem> BuscarTipoOperacaoPorNivelEnsinoPermiteManutencaoManual(long seqNivelEnsino, long seqTipoVinculo, long? SeqTipoTermoIntercambio)
        {
            var filtro = new InstituicaoNivelTipoVinculoAlunoFilterSpecification() { SeqNivelEnsino = seqNivelEnsino, SeqTipoVinculoAluno = seqTipoVinculo, SeqTipoTermoIntercambio = SeqTipoTermoIntercambio };

            var includes = IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao
                           | IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao_TipoOrientacao
                           | IncludesInstituicaoNivelTipoVinculoAluno.TipoVinculoAluno
                           | IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio
                           | IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio_TipoTermoIntercambio
                           | IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao_InstituicaoNivelTipoTermoIntercambio
                           | IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao_InstituicaoNivelTipoTermoIntercambio_TipoTermoIntercambio;

            var result = this.SearchBySpecification(filtro, includes).ToList();

            var retorno = new List<SMCDatasourceItem>();

            foreach (var item in result)
            {
                foreach (var tipoOrientacao in item.TiposOrientacao)
                {
                    if (tipoOrientacao.TipoOrientacao.PermiteManutencaoManual)
                    {
                        if (SeqTipoTermoIntercambio == null)
                        {
                            retorno.Add(new SMCDatasourceItem
                            {
                                Seq = tipoOrientacao.TipoOrientacao.Seq,
                                Descricao = tipoOrientacao.TipoOrientacao.Descricao
                            });
                        }
                        else
                        {
                            if (tipoOrientacao.InstituicaoNivelTipoTermoIntercambio != null &&
                                tipoOrientacao.InstituicaoNivelTipoTermoIntercambio.SeqTipoTermoIntercambio == SeqTipoTermoIntercambio)
                            {
                                retorno.Add(new SMCDatasourceItem
                                {
                                    Seq = tipoOrientacao.TipoOrientacao.Seq,
                                    Descricao = tipoOrientacao.TipoOrientacao.Descricao
                                });
                            }
                        }
                    }
                }
            }
            return retorno.SMCDistinct(d => d.Seq).ToList();
        }

        /// <summary>
        /// Buscar os tipos de orientação no qual exista tipo de orientação que esteja configurado para permitir inclusão manual da orientação
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nivel de ensino</param>
        /// <param name="seqTipoVinculo">Sequencial tipo vinculo</param>
        /// <param name="SeqTipoTermoIntercambio">Sequencial tipo de intercambio</param>
        /// <returns></returns>
        public List<SMCDatasourceItem> BuscarTipoOrientacaoPorNivelEnsinoPermiteInclusaoManual(long seqNivelEnsino, long seqTipoVinculo, long? SeqTipoTermoIntercambio)
        {
            var filtro = new InstituicaoNivelTipoVinculoAlunoFilterSpecification() { SeqNivelEnsino = seqNivelEnsino, SeqTipoVinculoAluno = seqTipoVinculo, SeqTipoTermoIntercambio = SeqTipoTermoIntercambio };

            var includes = IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao
                           | IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao_TipoOrientacao
                           | IncludesInstituicaoNivelTipoVinculoAluno.TipoVinculoAluno
                           | IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio
                           | IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio_TipoTermoIntercambio
                           | IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao_InstituicaoNivelTipoTermoIntercambio
                           | IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao_InstituicaoNivelTipoTermoIntercambio_TipoTermoIntercambio;

            var result = this.SearchBySpecification(filtro, includes).ToList();

            var retorno = new List<SMCDatasourceItem>();

            foreach (var item in result)
            {
                foreach (var tipoOrientacao in item.TiposOrientacao)
                {
                    if (tipoOrientacao.TipoOrientacao.PermiteInclusaoManual)
                    {
                        if (SeqTipoTermoIntercambio == null)
                        {
                            retorno.Add(new SMCDatasourceItem
                            {
                                Seq = tipoOrientacao.TipoOrientacao.Seq,
                                Descricao = tipoOrientacao.TipoOrientacao.Descricao
                            });
                        }
                        else
                        {
                            if (tipoOrientacao.InstituicaoNivelTipoTermoIntercambio != null &&
                                tipoOrientacao.InstituicaoNivelTipoTermoIntercambio.SeqTipoTermoIntercambio == SeqTipoTermoIntercambio)
                            {
                                retorno.Add(new SMCDatasourceItem
                                {
                                    Seq = tipoOrientacao.TipoOrientacao.Seq,
                                    Descricao = tipoOrientacao.TipoOrientacao.Descricao
                                });
                            }
                        }
                    }
                }
            }
            return retorno.SMCDistinct(d => d.Seq).ToList();
        }

        /// <summary>
        /// Busca as configurações de vínculo de aluno com as dependências necessárias para a validação da regra RN_ALN_031
        /// </summary>
        /// <param name="seqsNiveisEnsino">Níveis de ensino dos alunos a serem validados</param>
        /// <param name="seqsTiposVinculoAluno">Tipos dos vínculos dos alunos a serem validados</param>
        /// <returns>Todas configurações de vínculo que sejam dos níveis de ensino informados ou tipos de vínculo de aluno</returns>
        public List<InstituicaoNivelTipoVinculoAluno> BuscarConfiguracoesVinculos(long[] seqsNiveisEnsino, long[] seqsTiposVinculoAluno)
        {
            var specConfigNivelEnsino = new SMCContainsSpecification<InstituicaoNivelTipoVinculoAluno, long>(p => p.InstituicaoNivel.SeqNivelEnsino, seqsNiveisEnsino);
            var specConfigVinculo = new SMCContainsSpecification<InstituicaoNivelTipoVinculoAluno, long>(p => p.SeqTipoVinculoAluno, seqsTiposVinculoAluno);
            var specConfigVinculos = new SMCOrSpecification<InstituicaoNivelTipoVinculoAluno>(specConfigNivelEnsino, specConfigVinculo);
            return SearchBySpecification(specConfigVinculos, IncludesInstituicaoNivelTipoVinculoAluno.InstituicaoNivel
                                                           | IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio_TipoTermoIntercambio)
                .ToList();
        }

        /// <summary>
        /// Busca a configuração do vínculo do aluno com as dependências necessárias para a validação da regra RN_ALN_031
        /// </summary>
        /// <param name="seqNivelEnsino">Nível de ensino do aluno</param>
        /// <param name="seqTipoVinculoAluno">Tipo de vínculo do aluno</param>
        /// <returns>Configurações do vínculo que do aluno</returns>
        public InstituicaoNivelTipoVinculoAluno BuscarConfiguracaoVinculo(long seqNivelEnsino, long seqTipoVinculoAluno)
        {
            return SearchByKey(new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
            {
                SeqNivelEnsino = seqNivelEnsino,
                SeqTipoVinculoAluno = seqTipoVinculoAluno
            }, IncludesInstituicaoNivelTipoVinculoAluno.InstituicaoNivel
             | IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio_TipoTermoIntercambio
             | IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao);
        }

        /// <summary>
        /// Busca todos os tipo de termo de intercambio de um nível de ensino.
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino.</param>
        /// <param name="seqParceriaIntercambio">Sequencial da parceria de intercambio</param>
        /// <returns>Tipos de termo de intercâmbio.</returns>
        public List<SMCDatasourceItem> BuscarTiposTermoIntercambioPorNivelEnsino(long seqNivelEnsino, long seqParceriaIntercambio)
        {
            var filtro = new InstituicaoNivelTipoVinculoAlunoFilterSpecification() { SeqNivelEnsino = seqNivelEnsino };
            var filtroParceria = new InstituicaoNivelTipoVinculoAlunoTipoTermoIntercambioFilterSpecification { SeqParceriaIntercambio = seqParceriaIntercambio };
            var andSpec = new SMCAndSpecification<InstituicaoNivelTipoVinculoAluno>(filtro, filtroParceria);

            var result = this.SearchProjectionByKey(andSpec, x => x.TiposTermoIntercambio.Select(t => new SMCDatasourceItem
            {
                Seq = t.TipoTermoIntercambio.Seq,
                Descricao = t.TipoTermoIntercambio.Descricao,
            })).SMCDistinct(x => x.Seq).OrderBy(t => t.Descricao).ToList();

            return result;
        }
    }
}