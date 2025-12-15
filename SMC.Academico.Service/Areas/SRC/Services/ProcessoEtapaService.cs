using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class ProcessoEtapaService : SMCServiceBase, IProcessoEtapaService
    {
        #region [ DomainService ]

        private ProcessoEtapaDomainService ProcessoEtapaDomainService
        {
            get { return this.Create<ProcessoEtapaDomainService>(); }
        }

        #endregion [ DomainService ]

        #region [ Services ]

        private IEtapaService EtapaService
        {
            get { return this.Create<IEtapaService>(); }
        }

        #endregion [ Services ]

        public ProcessoEtapaCabecalhoData BuscarCabecalhoProcessoEtapa(long seqProcessoEtapa)
        {
            var result = this.ProcessoEtapaDomainService.BuscarCabecalhoProcessoEtapa(seqProcessoEtapa);

            result.DescricaoEtapaSgf = EtapaService.BuscarEtapa(result.SeqEtapaSgf).Descricao;

            return result.Transform<ProcessoEtapaCabecalhoData>();
        }

        public ProcessoEtapaData BuscarProcessoEtapa(long seqProcessoEtapa)
        {
            var result = this.ProcessoEtapaDomainService.BuscarProcessoEtapa(seqProcessoEtapa);

            //VALIDAÇÃO PARA AS SEÇÕES NÃO OBRIGATÓRIAS NÃO CHEGAREM COMO LISTAS VAZIAS E SEREM SALVAS COMO OBJETOS VAZIOS
            result.SituacoesItemMatricula = result.SituacoesItemMatricula.Any() ? result.SituacoesItemMatricula : null;
            result.FiltrosDados = result.FiltrosDados.Any() ? result.FiltrosDados : null;

            var retorno = result.Transform<ProcessoEtapaData>();
            retorno.DescricaoEtapaSgf = EtapaService.BuscarEtapa(result.SeqEtapaSgf).Descricao;
            retorno.CamposReadyOnly = retorno.SituacaoEtapa == SituacaoEtapa.Encerrada || retorno.SituacaoEtapa == SituacaoEtapa.Liberada;

            if (retorno.ExigeEscalonamento)
            {
                retorno.TipoPrazoEtapa = TipoPrazoEtapa.Escalonamento;
            }

            List<string> listaTokensTipoServicoValidar = new List<string>() { TOKEN_TIPO_SERVICO.MATRICULA_INGRESSANTE, TOKEN_TIPO_SERVICO.MATRICULA_REABERTURA, TOKEN_TIPO_SERVICO.RENOVACAO_MATRICULA, TOKEN_TIPO_SERVICO.PLANO_ESTUDO };
            retorno.ExibeSecaoTokenMatricula = listaTokensTipoServicoValidar.Contains(retorno.TokenTipoServico);

            return retorno;
        }

        /// <summary>
        /// Busca os dados para serem exibidos na mensagem de confirmação de encerramento de uma etapa
        /// </summary>
        /// <param name="seqProcessoEtapa">Sequencial do processo Etapa a ser encerrado</param>
        /// <returns>Dados</returns>
        public ProcessoEtapaData BuscarDadosEncerrarProcessoEtapa(long seqProcessoEtapa)
        {
            var result = this.ProcessoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ProcessoEtapa>(seqProcessoEtapa), x => new ProcessoEtapaData
            {
                TipoPrazoEtapa = x.TipoPrazoEtapa,
                Escalonamentos = x.Escalonamentos.Where(e => e.DataFim <= DateTime.Now && !e.DataEncerramento.HasValue).Select(e => new EscalonamentoData
                {
                    DataInicio = e.DataInicio,
                    DataFim = e.DataFim,
                    DataEncerramento = e.DataEncerramento,
                    DescricaoGruposEscalonamento = e.GruposEscalonamento.Select(g => g.GrupoEscalonamento.Descricao).ToList()
                }).ToList()
            });

            return result;
        }

        public List<SMCDatasourceItem> BuscarSituacoesEtapaPorProcessoEtapaSelect(long seqProcessoEtapa)
        {
            var result = this.ProcessoEtapaDomainService.BuscarSituacoesEtapaPorProcessoEtapaSelect(seqProcessoEtapa);

            return result;
        }

        public List<SMCDatasourceItem> BuscarTiposPrazoAtendimentoEtapa(long seqProcessoEtapa)
        {
            var result = this.ProcessoEtapaDomainService.BuscarTiposPrazoAtendimentoEtapa(seqProcessoEtapa);

            return result;
        }

        public long SalvarProcessoEtapa(ProcessoEtapaData modelo)
        {
            return this.ProcessoEtapaDomainService.SalvarProcessoEtapa(modelo.Transform<ProcessoEtapa>());
        }

        public void ValidarModeloSalvar(ProcessoEtapaData modelo)
        {
            this.ProcessoEtapaDomainService.ValidarModeloSalvar(modelo.Transform<ProcessoEtapa>());
        }

        public bool ValidarAssertSalvar(ProcessoEtapaData modelo)
        {
            return this.ProcessoEtapaDomainService.ValidarAssertSalvar(modelo.Transform<ProcessoEtapa>());
        }

        public bool ValidarAssertEscalonamentoBloqueiosEncerrarEtapa(long seqProcessoEtapa)
        {
            return this.ProcessoEtapaDomainService.ValidarAssertEscalonamentoBloqueiosEncerrarEtapa(seqProcessoEtapa);
        }

        /// <summary>
        /// Salva as etapas do SGF selecionadas na inclusão de um processo
        /// </summary>
        /// <param name="seqProcesso">Sequencial do processo</param>
        /// <param name="seqsEtapasSGF">Lista com os sequenciais das etapas do SGF selecionadas</param>
        public void SalvarProcessoEtapaOrigemSGF(int seqProcesso, List<long> seqsEtapasSGF)
        {
            this.ProcessoEtapaDomainService.SalvarProcessoEtapaOrigemSGF(seqProcesso, seqsEtapasSGF);
        }

        /// <summary>
        /// Recupera o token do processo etapa para realiza validações na seleção de turma e de atividade
        /// </summary>
        /// <param name="seqProcessoEtapa"></param>
        /// <returns>token do processo etapa</returns>
        public string BuscarTokenProcessoEtapa(long seqProcessoEtapa)
        {
            return ProcessoEtapaDomainService.BuscarTokenProcessoEtapa(seqProcessoEtapa);
        }

        /// <summary>
        /// Colocar Processo Etapa em manutencao
        /// </summary>
        /// <param name="seqProcessoEtapa">Sequencial do Processo Etapa</param>
        /// <returns>Modelo processo etapa</returns>
        public ProcessoEtapaData ColocarProcessoEtapaManutencao(long seqProcessoEtapa)
        {
            return this.ProcessoEtapaDomainService.ColocarProcessoEtapaManutencao(seqProcessoEtapa).Transform<ProcessoEtapaData>();
        }

        /// <summary>
        /// Liberar Processo Etapa
        /// </summary>
        /// <param name="seqProcessoEtapa">Sequencial do Processo Etapa</param>
        /// <returns>Modelo processo etapa</returns>
        public ProcessoEtapaData LiberarProcessoEtapa(long seqProcessoEtapa)
        {
            return this.ProcessoEtapaDomainService.LiberarProcessoEtapa(seqProcessoEtapa).Transform<ProcessoEtapaData>();
        }

        /// <summary>
        /// Buscar processo etapa por processo
        /// </summary>
        /// <param name="seqProcesso">Sequencial do processo</param>
        /// <returns>Lista dos processo etapas ordenado pelo campo ordem</returns>
        public List<SMCDatasourceItem> BuscarProcessoEtapaPorProcessoSelect(long seqProcesso)
        {
            return this.ProcessoEtapaDomainService.BuscarProcessoEtapaPorProcessoSelect(seqProcesso);
        }

        /// <summary>
        /// Buscar processo etapa por serviço
        /// </summary>
        /// <param name="seqServico">Sequencial do serviço</param>
        /// <returns>Lista dos processo etapas ordenado pelo campo ordem</returns>
        public List<SMCDatasourceItem> BuscarProcessoEtapaPorServicoSelect(long? seqServico)
        {
            return this.ProcessoEtapaDomainService.BuscarProcessoEtapaPorServicoSelect(seqServico);
        }

        public void EncerrarEtapa(long seqEtapaProcesso)
        {
            ProcessoEtapaDomainService.EncerrarEtapa(seqEtapaProcesso);
        }

        public bool ValidarDataEscalonamentoFinalProcesso(long seqProcessoEtapa)
        {
            return ProcessoEtapaDomainService.ValidarDataEscalonamentoFinalProcesso(seqProcessoEtapa);
        }
    }
}