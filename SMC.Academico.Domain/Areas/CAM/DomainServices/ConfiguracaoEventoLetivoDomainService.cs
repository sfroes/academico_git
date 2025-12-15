using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Calendarios.ServiceContract.Areas.CLD.Interfaces;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CAM.DomainServices
{
    public class ConfiguracaoEventoLetivoDomainService : AcademicoContextDomain<ConfiguracaoEventoLetivo>
    {
        #region Services

        private ITipoEventoService TipoEventoService
        {
            get { return this.Create<ITipoEventoService>(); }
        }

        #endregion Services

        #region [ DomainServices ]

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private CicloLetivoDomainService CicloLetivoDomainService => Create<CicloLetivoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private ConfiguracaoEventoLetivoItemDomainService ConfiguracaoEventoLetivoItemDomainService => Create<ConfiguracaoEventoLetivoItemDomainService>();

        #endregion [ DomainServices ]

        /// <summary>
        /// A partir de uma data de referencia, um curso-oferta-localidade-turno, um tipo de aluno e um
        /// tipo de evento, retorna o sequencial do ciclo letivo ativo no evento e data de referencia
        /// </summary>
        /// <param name="dataReferencia">Data de referencia</param>
        /// <param name="seqCursoOfertaLocalidadeTurno">Sequencial do curso-oferta-localidade-turno</param>
        /// <param name="tipoAluno">Tipo do aluno a ser considerado</param>
        /// <param name="tokenTipoEvento">Token do tipo de evento a ser considerado</param>
        /// <returns>Sequencial do ciclo letivo ativo na data de referencia</returns>
        public long BuscarSeqCicloLetivo(DateTime dataReferencia, long seqCursoOfertaLocalidadeTurno, TipoAluno? tipoAluno, string tokenTipoEvento)
        {
            var spec = new ConfiguracaoEventoLetivoItemFilterSpecification()
            {
                TokenTipoEvento = tokenTipoEvento,
                SeqCursoOfertaLocalidadeTurno = seqCursoOfertaLocalidadeTurno,
                TipoAluno = tipoAluno,
                DataReferencia = dataReferencia
            };
            return ConfiguracaoEventoLetivoItemDomainService.SearchProjectionByKey(spec, x => x.ConfiguracaoEventoLetivo.SeqCicloLetivo);
        }

        /// <summary>
        /// A partir de uma data de referencia, um curso-oferta-localidade-turno, um tipo de aluno e um
        /// tipo de evento, retorna o sequencial do ciclo letivo ativo no evento e data de referencia
        /// </summary>
        /// <param name="dataReferencia">Data de referencia</param>
        /// <param name="seqCursoOfertaLocalidadeTurno">Sequencial do curso-oferta-localidade-turno</param>
        /// <param name="tipoAluno">Tipo do aluno a ser considerado</param>
        /// <param name="tokenTipoEvento">Token do tipo de evento a ser considerado</param>
        /// <returns>Sequencial do ciclo letivo ativo na data de referencia</returns>
        public DatasEventoLetivoVO BuscarCicloLetivo(DateTime dataReferencia, long seqCursoOfertaLocalidadeTurno, TipoAluno? tipoAluno, string tokenTipoEvento)
        {
            var spec = new ConfiguracaoEventoLetivoItemFilterSpecification()
            {
                TokenTipoEvento = tokenTipoEvento,
                SeqCursoOfertaLocalidadeTurno = seqCursoOfertaLocalidadeTurno,
                TipoAluno = tipoAluno,
                DataReferencia = dataReferencia.Date
            };
            return ConfiguracaoEventoLetivoItemDomainService.SearchProjectionByKey(spec, x => new DatasEventoLetivoVO
            {
                SeqCicloLetivo = x.ConfiguracaoEventoLetivo.SeqCicloLetivo,
                DescricaoCicloLetivo = x.ConfiguracaoEventoLetivo.CicloLetivo.Descricao,
                Ano = x.ConfiguracaoEventoLetivo.CicloLetivo.Ano,
                Numero = x.ConfiguracaoEventoLetivo.CicloLetivo.Numero,
                DataInicio = x.EventoLetivo.DataInicio,
                DataFim = x.EventoLetivo.DataFim,
                AnoNumeroCicloLetivo = x.ConfiguracaoEventoLetivo.CicloLetivo.AnoNumeroCicloLetivo
            });
        }

        /// <summary>
        /// Busca a data de inicio e fim de um evento letivo
        /// </summary>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo a ser considerado</param>
        /// <param name="seqCursoOfertaLocalidadeTurno">Sequencial do curso-oferta-localidade-turno a ser considerado</param>
        /// <param name="tipoAluno">Tipo do aluno a ser considerado</param>
        /// <param name="tokenTipoEvento">Token do tipo de evento a ser considerado</param>
        /// <returns></returns>
        public DatasEventoLetivoVO BuscarDatasEventoLetivo(long seqCicloLetivo, long seqCursoOfertaLocalidadeTurno, TipoAluno? tipoAluno, string tokenTipoEvento)
        {
            var spec = new ConfiguracaoEventoLetivoItemFilterSpecification()
            {
                SeqCicloLetivo = seqCicloLetivo,
                SeqCursoOfertaLocalidadeTurno = seqCursoOfertaLocalidadeTurno,
                TipoAluno = tipoAluno,
                TokenTipoEvento = tokenTipoEvento
            };

            var ciclo = ConfiguracaoEventoLetivoItemDomainService.SearchProjectionByKey(spec, x => new DatasEventoLetivoVO
            {
                SeqCicloLetivo = x.ConfiguracaoEventoLetivo.SeqCicloLetivo,
                DescricaoCicloLetivo = x.ConfiguracaoEventoLetivo.CicloLetivo.Descricao,
                DataInicio = x.EventoLetivo.DataInicio,
                DataFim = x.EventoLetivo.DataFim,
                Ano = x.ConfiguracaoEventoLetivo.CicloLetivo.Ano,
                Numero = x.ConfiguracaoEventoLetivo.CicloLetivo.Numero,
                TipoAluno = x.ConfiguracaoEventoLetivo.TipoAluno,
                SeqTipoEventoAgd = x.EventoLetivo.CicloLetivoTipoEvento.InstituicaoTipoEvento.SeqTipoEventoAgd
            });

            if (ciclo == null)
                throw new SMCApplicationException("Não foi encontrado nenhum evento letivo com os parâmetros informados.");

            if (ciclo.TipoAluno.HasValue && !tipoAluno.HasValue)
            {
                var descricaoTipoEvento = ciclo.SeqTipoEventoAgd.GetValueOrDefault() != 0 ? this.TipoEventoService.BuscarTipoEvento(ciclo.SeqTipoEventoAgd.Value).Descricao : tokenTipoEvento;
                throw new SMCApplicationException($"O tipo de aluno é obrigatório para busca do período do evento letivo '{descricaoTipoEvento}' no ciclo '{ciclo.DescricaoCicloLetivo}'");
            }

            //SMCApplicationException($"Parâmetros de evento letivo inválidos ciclo [{seqCicloLetivo}] curso oferta localidade turno [{seqCursoOfertaLocalidadeTurno}] tipo aluno [{tipoAluno}] tipo evento [{tokenTipoEvento}]");

            return ciclo;
        }

        public DatasEventoLetivoVO BuscarDatasEventoLetivo(long seqCicloLetivo, long seqAluno, string tokenTipoEvento)
        {
            // Busca os dados de origem da pessoa atuação
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno);

            return BuscarDatasEventoLetivo(seqCicloLetivo, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, tokenTipoEvento);
        }

        /// <summary>
        /// Busca as datas e dados dos ciclos letivos por um período de referência.
        /// </summary>
        /// <param name="dataReferenciaInicio">Data de referência inicial</param>
        /// <param name="dataReferenciaFim">Data de referência final</param>
        /// <param name="seqCursoOfertaLocalidadeTurno">Sequencial do Curso Oferta Localidade Turno</param>
        /// <param name="tipoAluno">Tipo de aluno</param>
        /// <returns>Lista com as datas e dados dos ciclos letivos existentes no período informado</returns>
        public List<DatasEventoLetivoVO> BuscarDatasEventosLetivoPorPeriodo(DateTime dataReferenciaInicio, DateTime? dataReferenciaFim, long seqCursoOfertaLocalidadeTurno, TipoAluno tipoAluno)
        {
            // Recebe o ciclo letivo e retorna todas as configurações de data para o mesmo
            Func<long, DatasEventoLetivoVO> buscarDadosEventoLetivo = (seqCiclo) =>
            {
                // Achou. Busca a data e os dados do ciclo letivo encontrado
                var dataCicloLetivoInicioBeneficio = BuscarDatasEventoLetivo(seqCiclo, seqCursoOfertaLocalidadeTurno, tipoAluno, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
                var cicloLetivo = CicloLetivoDomainService.SearchProjectionByKey(seqCiclo, x => new { Ano = x.Ano, Numero = x.Numero });

                return new DatasEventoLetivoVO
                {
                    Ano = cicloLetivo.Ano,
                    Numero = cicloLetivo.Numero,
                    DataInicio = dataCicloLetivoInicioBeneficio.DataInicio,
                    DataFim = dataCicloLetivoInicioBeneficio.DataFim,
                    SeqCicloLetivo = seqCiclo
                };
            };

            // Retorno
            List<DatasEventoLetivoVO> ret = new List<DatasEventoLetivoVO>();

            // Busca o ciclo letivo da data de referência inicial
            var seqCicloLetivoInicioBeneficio = BuscarSeqCicloLetivo(dataReferenciaInicio, seqCursoOfertaLocalidadeTurno, tipoAluno, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

            // Caso não tenha achado, retorna a lista em branco
            if (seqCicloLetivoInicioBeneficio == 0)
                return ret;

            // Busca os dados do ciclo letivo inicial
            var dadosCicloInicioBeneficio = buscarDadosEventoLetivo(seqCicloLetivoInicioBeneficio);
            ret.Add(dadosCicloInicioBeneficio);

            // Controle do loop
            bool encontrouTodos = false;

            // Caso a data fim do benefício esteja dentro do período do ciclo letivo, significa que é apenas 1 e já encontrou o mesmo
            if (dataReferenciaFim.HasValue && dadosCicloInicioBeneficio.DataFim >= dataReferenciaFim)
                encontrouTodos = true;

            // Armazena o último ciclo letivo buscado
            long? seqCicloLetivoAtual = dadosCicloInicioBeneficio.SeqCicloLetivo;

            while (!encontrouTodos)
            {
                // Busca o próximo ciclo letivo
                seqCicloLetivoAtual = CicloLetivoDomainService.BuscarProximoCicloLetivo(seqCicloLetivoAtual.Value);

                // Caso tenha, busca os dados do mesmo
                if (seqCicloLetivoAtual.GetValueOrDefault() > 0)
                {
                    dadosCicloInicioBeneficio = buscarDadosEventoLetivo(seqCicloLetivoAtual.Value);
                    ret.Add(dadosCicloInicioBeneficio);

                    // Caso a data fim do benefício esteja dentro do período do ciclo letivo, significa que é apenas 1 e já encontrou o mesmo
                    if (dataReferenciaFim.HasValue && dadosCicloInicioBeneficio.DataFim >= dataReferenciaFim)
                        encontrouTodos = true;
                }
                else
                    encontrouTodos = true;
            }

            return ret;
        }

        public DatasEventoLetivoVO BuscarEventoLetivoAtual(long seqAluno)
        {
            // Busca os dados de origem da pessoa atuação
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno);

            // Recupera o ciclo letivo corrente (cujo periodo do evento letivo é corrente) para o curso-oferta-turno do aluno
            var ciclo = BuscarCicloLetivo(DateTime.Today, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

            // Se não encontou, busca o ultimo ciclo letivo do aluno (ciclo de maior ano/numero)
            if (ciclo == null)
            {
                var seqCiclo = AlunoDomainService.BuscarUltimoCicloLetivoAlunoHistorico(seqAluno);
                ciclo = BuscarDatasEventoLetivo(seqCiclo, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
            }
            else
            {
                // Verifica se o aluno possui o ciclo letivo atual
                var possuiCicloAtual = AlunoDomainService.SearchProjectionByKey(seqAluno, x => x.Historicos.FirstOrDefault(h => h.Atual)
                                                                       .HistoricosCicloLetivo.Any(a => a.SeqCicloLetivo == ciclo.SeqCicloLetivo && !a.DataExclusao.HasValue));
                if (!possuiCicloAtual)
                {
                    var seqCiclo = AlunoDomainService.BuscarUltimoCicloLetivoAlunoHistorico(seqAluno);
                    ciclo = BuscarDatasEventoLetivo(seqCiclo, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
                }
            }

            return ciclo;
        }
    }
}