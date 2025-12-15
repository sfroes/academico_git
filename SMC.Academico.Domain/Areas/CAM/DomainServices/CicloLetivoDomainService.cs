using SMC.Academico.Common.Areas.CAM.Exceptions;
using SMC.Academico.Common.Areas.CAM.Includes;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CAM.Validators;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Domain.Exceptions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CAM.DomainServices
{
    public class CicloLetivoDomainService : AcademicoContextDomain<CicloLetivo>
    {
        #region [ Serviços ]

        private AlunoDomainService AlunoDomainService
        {
            get { return this.Create<AlunoDomainService>(); }
        }

        private TurmaDomainService TurmaDomainService
        {
            get { return this.Create<TurmaDomainService>(); }
        }

        private RegimeLetivoDomainService RegimeLetivoDomainService
        {
            get { return this.Create<RegimeLetivoDomainService>(); }
        }

        private CampanhaDomainService CampanhaDomainService
        {
            get { return this.Create<CampanhaDomainService>(); }
        }

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService { get => Create<PessoaAtuacaoDomainService>(); }

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService { get => Create<ConfiguracaoEventoLetivoDomainService>(); }

        #endregion [ Serviços ]

        /// <summary>
        /// Grava um CicloLetivo aplicando suas validações e preenchendo o campo descrição
        /// </summary>
        /// <param name="cicloLetivo">Ciclo letivo a ser gravado</param>
        /// <returns>Sequencial do ciclo letivo gravado</returns>
        public long SalvarCicloLetivo(CicloLetivo cicloLetivo)
        {
            // Recupera dependência
            cicloLetivo.RegimeLetivo = cicloLetivo.RegimeLetivo ?? this.RegimeLetivoDomainService
                .SearchByKey(new SMCSeqSpecification<RegimeLetivo>(cicloLetivo.SeqRegimeLetivo));

            this.Validar(cicloLetivo);

            // Regra de descrição
            string descricaoNivel = cicloLetivo.Numero > 0 ? cicloLetivo.Numero + "/" : "";
            cicloLetivo.Descricao = $"{descricaoNivel}{cicloLetivo.Ano}";

            this.SaveEntity(cicloLetivo);

            return cicloLetivo.Seq;
        }

        /// <summary>
        /// Busca os ciclos letivos para cópia segundo o filtro informado
        /// </summary>
        /// <param name="filtro">Filtros dos ciclos letivos para cópia</param>
        /// <param name="anoDestino">Ano que será usuado no lugar do ano de destino</param>
        /// <param name="numeroDestino">Número que pode ser usado no lugar do número dos ciclos originais</param>
        /// <returns>Lista páginada dos Ciclos que atendam ao filtro com o ano trocado pelo valor de destino e o número caso seja informado</returns>
        public SMCPagerData<CicloLetivo> BuscarCiclosLetivosCopia(CicloLetivoFilterSpecification filtro, short anoDestino)
        {
            var ordenacao = new List<SMCSortInfo>();
            ordenacao.Add(new SMCSortInfo("Numero", SMCSortDirection.Ascending));
            ordenacao.Add(new SMCSortInfo("RegimeLetivo.Descricao", SMCSortDirection.Ascending));
            filtro.SetOrderBy(ordenacao);

            var ciclosLetivos = this.SearchBySpecification(filtro, IncludesCicloLetivo.NiveisEnsino | IncludesCicloLetivo.RegimeLetivo).ToList();

            if (ciclosLetivos.GroupBy(x => x.Ano).Count() > 1)
                throw new CicloLetivoCopiaAnosDiferentesException();

            ciclosLetivos.SMCForEach(f =>
            {
                f.Ano = anoDestino;
            });

            return new SMCPagerData<CicloLetivo>(ciclosLetivos, ciclosLetivos.Count());
        }

        /// <summary>
        /// Copia os ciclos letivos informados em ciclosLetivos trocando o Número pelo informado na lista e o ano pelo anoDestino.
        /// </summary>
        /// <param name="filtro">Filtro para recuperar os ciclos originais</param>
        /// <param name="anoDestino">Ano que será utilizado nos Ciclos copiados</param>
        /// <param name="ciclosLetivos">Ciclos letivos com os números de Ciclos letivos que serão copiados</param>
        public void CopiarCiclosLetivos(CicloLetivoFilterSpecification filtro, short anoDestino)
        {
            if (filtro.SeqsCiclosLetivos.SMCCount() == 0)
                throw new CicloLetivoCopiaNenhumSelecionadoException();

            // Spec com os sequenciais de ciclos letivos marcados para cópia
            var specCiclosCopiar = new SMCContainsSpecification<CicloLetivo, long>(p => p.Seq, filtro.SeqsCiclosLetivos.ToArray());

            var specAnd = new SMCAndSpecification<CicloLetivo>(filtro, specCiclosCopiar);
            var includes = IncludesCicloLetivo.NiveisEnsino
                         | IncludesCicloLetivo.RegimeLetivo;
            var ciclosLetivosCopia = this.SearchBySpecification(specAnd, includes)
                .ToList();

            var regimeLetivoDomainService = this.RegimeLetivoDomainService;

            // Atualiza e valida todos os cilos a serem copiados
            ciclosLetivosCopia.ForEach(f =>
            {
                f.Ano = anoDestino;
                // Reseta o Sequencial para ser considerado como um novo registro
                f.Seq = 0;

                this.Validar(f);
            });

            ciclosLetivosCopia.ForEach(f => this.SalvarCicloLetivo(f));
        }

        public List<SMCDatasourceItem> BuscarCiclosLetivosPorInstituicaoENivelSelect(long seqInstituicaoEnsino, long seqNivelEnsino)
        {
            var spec = new CicloLetivoFilterSpecification { SeqInstituicaoEnsino = seqInstituicaoEnsino, SeqNivelEnsino = seqNivelEnsino };
            spec.SetOrderByDescending(x => x.AnoNumeroCicloLetivo);

            return SearchProjectionBySpecification(spec, x => new SMCDatasourceItem
            {
                Seq = x.Seq,
                Descricao = x.Descricao,
            }).ToList();
        }

        /// <summary>
        /// Busca os Ciclos Letivos com Níveis de Ensino para o lookup
        /// </summary>
        /// <param name="filtro">Filtro dos Ciclos Letivos</param>
        /// <returns>Lista páginada dos ciclos letivos com Níveis de Ensino</returns>
        public SMCPagerData<CicloLetivo> BuscarCiclosLetivosLookup(SMCSpecification<CicloLetivo> filtro)
        {
            //TODO: Passar ordenação para model do lookup
            var ordenacao = new List<SMCSortInfo>();
            ordenacao.Add(new SMCSortInfo("Ano", SMCSortDirection.Descending));
            ordenacao.Add(new SMCSortInfo("Numero", SMCSortDirection.Descending));
            ordenacao.Add(new SMCSortInfo("RegimeLetivo.Descricao", SMCSortDirection.Ascending));
            filtro.SetOrderBy(ordenacao);

            int total;
            var includes = IncludesCicloLetivo.NiveisEnsino | IncludesCicloLetivo.RegimeLetivo;
            var ciclosLetivos = this.SearchBySpecification(filtro, out total, includes);

            return new SMCPagerData<CicloLetivo>(ciclosLetivos, total);
        }

        /// <summary>
        /// Busca todos os ciclos letivos por campanha e nível de ensino
        /// </summary>
        /// <param name="seqCampanha">Sequencial da campanha</param>
        /// <param name="seqNivelEnsino">Sequencial do nivel ensino</param>
        /// <returns>Dados dos ciclos letivos ordenados por descrição</returns>
        public List<SMCDatasourceItem> BuscarCiclosLetivosPorCampanhaNivelSelect(long seqCampanha, long seqNivelEnsino)
        {
            var specCampanha = new CampanhaFilterSpecification() { Seq = seqCampanha, SeqNivelEnsino = seqNivelEnsino };
            specCampanha.SetOrderBy(o => o.CiclosLetivos.FirstOrDefault().CicloLetivo.Descricao);

            return this.CampanhaDomainService.SearchProjectionByKey(specCampanha, p =>
                    p.CiclosLetivos.Select(s => new SMCDatasourceItem()
                    {
                        Seq = s.SeqCicloLetivo,
                        Descricao = s.CicloLetivo.Descricao
                    })
                )?.ToList() ?? new List<SMCDatasourceItem>();
        }

        /// <summary>
        /// Busca todos os ciclos letivos por campanha
        /// </summary>
        /// <param name="seqCampanha">Sequencial da campanha</param>
        /// <returns>Dados dos ciclos letivos ordenados por descrição</returns>
        public List<SMCDatasourceItem> BuscarCiclosLetivosPorCampanhaSelect(long seqCampanha)
        {
            var specCampanha = new CampanhaFilterSpecification() { Seq = seqCampanha };
            specCampanha.SetOrderBy(o => o.CiclosLetivos.FirstOrDefault().CicloLetivo.Descricao);

            return this.CampanhaDomainService.SearchProjectionByKey(specCampanha, p =>
                    p.CiclosLetivos.Select(s => new SMCDatasourceItem()
                    {
                        Seq = s.SeqCicloLetivo,
                        Descricao = s.CicloLetivo.Descricao
                    })
                )?.ToList() ?? new List<SMCDatasourceItem>();
        }

        public long BuscarCicloLetivoPorAnoNumero(short ano, short numero, long seqInstituicaoEnsino)
        {
            var specCicloLetivo = new CicloLetivoFilterSpecification() { Ano = ano, Numero = numero, SeqInstituicaoEnsino = seqInstituicaoEnsino };
            specCicloLetivo.SetOrderBy(o => o.Descricao);

            return this.SearchProjectionByKey(specCicloLetivo, p => p.Seq);
        }

        public List<SMCDatasourceItem> BuscarCiclosLetivosPorAluno(long seqAluno)
        {
            // Recupera, em ordem decrescente, todos os ciclos letivos que o aluno esteve ativo, ou seja, onde a última situação no ciclo é diferente de "Apto para Matrícula"
            var tokensNaoExibicao = new string[] { TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA };
            return AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seqAluno), x => x.Historicos.SelectMany(h => h.HistoricosCicloLetivo.Where(hc => !tokensNaoExibicao.Contains(hc.AlunoHistoricoSituacao.OrderByDescending(a => a.DataInicioSituacao).FirstOrDefault(s => !s.DataExclusao.HasValue && s.DataInicioSituacao <= DateTime.Now).SituacaoMatricula.Token))).Select(h => h.CicloLetivo).Distinct().OrderByDescending(cl => cl.Ano).ThenByDescending(cl => cl.Numero).Select(cl => new SMCDatasourceItem
            {
                Seq = cl.Seq,
                Descricao = cl.Descricao
            })).ToList();
        }

        private void Validar(CicloLetivo cicloLetivo)
        {
            var validator = new CicloLetivoValidator();
            var results = validator.Validate(cicloLetivo);
            if (!results.IsValid)
            {
                var errorList = new List<SMCValidationResults>();
                errorList.Add(results);
                throw new SMCInvalidEntityException(errorList);
            }
        }

        /// <summary>
        /// Recupera todos os ciclos letivos do aluno independente da situação
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Ciclos letivos vinculados ao aluno</returns>
        public List<SMCDatasourceItem> BuscarCiclosLetivosPorHistoricoAluno(long seqAluno)
        {
            return AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seqAluno), p =>
                p.Historicos
                 .SelectMany(s => s.HistoricosCicloLetivo).Where(w => w.PlanosEstudo.Any(f => f.Atual))
                 .Select(s => s.CicloLetivo).Distinct().OrderByDescending(o => o.AnoNumeroCicloLetivo).Select(s =>
                      new SMCDatasourceItem()
                      {
                          Seq = s.Seq,
                          Descricao = s.Descricao
                      })).ToList();
        }

        /// <summary>
        /// Busca o próximo ciclo letivo de um ciclo de referencia.
        /// </summary>
        /// <param name="seqCicloReferencia">Sequencial do ciclo letivo para buscar o próximo</param>
        /// <returns>Sequencial do próximo ciclo letivo</returns>
        public long? BuscarProximoCicloLetivo(long seqCicloReferencia)
        {
            // Busca as informações do ciclo letivo de referencia
            var cicloReferencia = this.SearchByKey(seqCicloReferencia, IncludesCicloLetivo.RegimeLetivo);

            // Monta o spec para o proximo ciclo
            var spec = new CicloLetivoFilterSpecification()
            {
                SeqInstituicaoEnsino = cicloReferencia.SeqInstituicaoEnsino,
                SeqRegimeLetivo = cicloReferencia.SeqRegimeLetivo,
                Ano = cicloReferencia.Numero == cicloReferencia.RegimeLetivo.NumeroItensAno ? (short)(cicloReferencia.Ano + 1) : cicloReferencia.Ano,
                Numero = cicloReferencia.Numero == cicloReferencia.RegimeLetivo.NumeroItensAno ? (short)1 : (short)(cicloReferencia.Numero + 1)
            };

            long? ret = (long?)this.SearchProjectionByKey(spec, x => x.Seq);
            if (ret == 0) return null;
            return ret;
        }

        /// <summary>
        /// Buscar o ciclo letivo anterior a um ciclo de referencia
        /// </summary>
        /// <param name="seqCicloReferencia">Sequencial do ciclo letivo para buscar o anterior</param>
        /// <returns>Sequencial do ciclo letivo anterior</returns>
        public long? BuscarCicloLetivoAnterior(long seqCicloReferencia)
        {
            // Busca as informações do ciclo letivo de referencia
            var cicloReferencia = this.SearchByKey(seqCicloReferencia, IncludesCicloLetivo.RegimeLetivo);

            // Monta o spec para o proximo ciclo
            var spec = new CicloLetivoFilterSpecification()
            {
                SeqInstituicaoEnsino = cicloReferencia.SeqInstituicaoEnsino,
                SeqRegimeLetivo = cicloReferencia.SeqRegimeLetivo,
                Ano = cicloReferencia.Numero == 1 ? (short)(cicloReferencia.Ano - 1) : cicloReferencia.Ano,
                Numero = cicloReferencia.Numero == 1 ? cicloReferencia.RegimeLetivo.NumeroItensAno : (short)(cicloReferencia.Numero - 1)
            };

            return this.SearchProjectionByKey(spec, x => x.Seq);
        }

        /// <summary>
        /// Busca o ciclo letivo com a formatação {Numero}º/{Ano}
        /// </summary>
        /// <param name="seqCicloLetivo"></param>
        /// <returns>Ciclo letivo formatado {Numero}º/{Ano}</returns>
        public string BuscarDescricaoFormatadaCicloLetivo(long seqCicloLetivo)
        {
            var cicloLetivo = SearchByKey(new SMCSeqSpecification<CicloLetivo>(seqCicloLetivo));

            return $"{cicloLetivo?.Numero}º/{cicloLetivo?.Ano}";
        }

        /// <summary>
        /// Busca uma quantidade de ciclos letivos do aluno, em que o mesmo possui situação no ciclo letivo
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="quantidadeCiclosLetivos">Quantidade de ciclos letivos anteriores ao atual</param>
        /// <returns>Lista de ciclos letivos em que o aluno pussui situação</returns>
        public List<SMCDatasourceItem> BuscarCiclosLetivosAlunoComSituacaoSelect(long seqAluno, int? quantidadeCiclosLetivosAnteriores = null)
        {
            var listaCiclosLetivos = new List<SMCDatasourceItem>();

            var cicloLetivoAtual = ConfiguracaoEventoLetivoDomainService.BuscarEventoLetivoAtual(seqAluno);

            listaCiclosLetivos.Add(new SMCDatasourceItem()
            {
                Seq = cicloLetivoAtual.SeqCicloLetivo,
                Descricao = cicloLetivoAtual.DescricaoCicloLetivo,
                Selected = true
            });

            if (quantidadeCiclosLetivosAnteriores.HasValue && quantidadeCiclosLetivosAnteriores.GetValueOrDefault() > 0)
            {
                var seqCicloLetivoReferencia = cicloLetivoAtual.SeqCicloLetivo;

                var ciclosLetivosAluno = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seqAluno), a =>
                                         a.Historicos.FirstOrDefault(h => h.Atual)
                                          .HistoricosCicloLetivo.Where(w => w.AlunoHistoricoSituacao.Count > 0)
                                          .Select(s => s.CicloLetivo).Distinct().OrderByDescending(o => o.AnoNumeroCicloLetivo).Select(s =>
                                          new SMCDatasourceItem()
                                          {
                                              Seq = s.Seq,
                                              Descricao = s.Descricao
                                          })).ToList();

                if (ciclosLetivosAluno.Count == listaCiclosLetivos.Count)
                    return listaCiclosLetivos;

                while (listaCiclosLetivos.Count < quantidadeCiclosLetivosAnteriores + 1)
                {
                    var seqCicloLetivoAnterior = this.BuscarCicloLetivoAnterior(seqCicloLetivoReferencia);

                    var cicloLetivoAnterior = ciclosLetivosAluno.FirstOrDefault(c => c.Seq == seqCicloLetivoAnterior);

                    if (ciclosLetivosAluno != null)
                        listaCiclosLetivos.Add(cicloLetivoAnterior);

                    seqCicloLetivoReferencia = cicloLetivoAnterior.Seq;
                }
            }
            return listaCiclosLetivos;
        }

        public List<CicloLetivo> BuscarCiclosLetivosPorAlunoLancamentoNota(long seqAluno)
        {
            return AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seqAluno), p =>
                 p.Historicos
                  .SelectMany(s => s.HistoricosCicloLetivo).Where(w => w.PlanosEstudo.Any(f => f.Atual))
                  .Select(s => s.CicloLetivo).Distinct().OrderByDescending(o => o.AnoNumeroCicloLetivo)).ToList();
        }
    }
}