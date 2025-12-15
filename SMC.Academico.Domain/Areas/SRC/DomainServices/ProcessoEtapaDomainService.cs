using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.Shared.Constants;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Resources;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.Domain.Helpers;
using SMC.Financeiro.ServiceContract.Areas.GRA.Data;
using SMC.Financeiro.ServiceContract.BLT;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Formularios.Common.Areas.TMP.Includes;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Formularios.ServiceContract.TMP.Data;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using SMC.Inscricoes.ServiceContract.Areas.INS.Data;
using SMC.Inscricoes.ServiceContract.Areas.INS.Interfaces;
using SMC.Seguranca.ServiceContract.Areas.APL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class ProcessoEtapaDomainService : AcademicoContextDomain<ProcessoEtapa>
    {
        #region [ Serviços Externos ]

        private IInscricaoOfertaHistoricoSituacaoService InscricaoOfertaHistoricoSituacaoService => Create<IInscricaoOfertaHistoricoSituacaoService>();

        #endregion [ Serviços Externos] 

        #region [ DomainServices ]

        #region [ Serviços de outros domínios ]

        private IAplicacaoService AplicacaoService => Create<IAplicacaoService>();

        private IEtapaService EtapaService => Create<IEtapaService>();

        private ISituacaoService SituacaoService => Create<ISituacaoService>();

        private IIntegracaoFinanceiroService IntegracaoFinanceiroService => Create<IIntegracaoFinanceiroService>();

        private IInscricaoService InscricaoService => Create<IInscricaoService>();

        private IIntegracaoService IntegracaoService => Create<IIntegracaoService>();

        private INotificacaoService NotificacaoService { get => Create<INotificacaoService>(); }

        #endregion [ Serviços outros domínios ]

        private PessoaAtuacaoBloqueioDomainService PessoaAtuacaoBloqueioDomainService => Create<PessoaAtuacaoBloqueioDomainService>();

        private ProcessoDomainService ProcessoDomainService => Create<ProcessoDomainService>();

        private EscalonamentoDomainService EscalonamentoDomainService => Create<EscalonamentoDomainService>();

        private GrupoEscalonamentoDomainService GrupoEscalonamentoDomainService => Create<GrupoEscalonamentoDomainService>();

        private ConfiguracaoEtapaDomainService ConfiguracaoEtapaDomainService => Create<ConfiguracaoEtapaDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private SolicitacaoHistoricoSituacaoDomainService SolicitacaoHistoricoSituacaoDomainService => Create<SolicitacaoHistoricoSituacaoDomainService>();

        private SolicitacaoMatriculaDomainService SolicitacaoMatriculaDomainService => Create<SolicitacaoMatriculaDomainService>();

        private SolicitacaoMatriculaItemHistoricoSituacaoDomainService SolicitacaoMatriculaItemHistoricoSituacaoDomainService => Create<SolicitacaoMatriculaItemHistoricoSituacaoDomainService>();

        private SolicitacaoMatriculaItemDomainService SolicitacaoMatriculaItemDomainService => Create<SolicitacaoMatriculaItemDomainService>();

        private IngressanteHistoricoSituacaoDomainService IngressanteHistoricoSituacaoDomainService => Create<IngressanteHistoricoSituacaoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();

        private SolicitacaoServicoEtapaDomainService SolicitacaoServicoEtapaDomainService => Create<SolicitacaoServicoEtapaDomainService>();

        private ServicoDomainService ServicoDomainService => Create<ServicoDomainService>();

        private DocumentoRequeridoDomainService DocumentoRequeridoDomainService => Create<DocumentoRequeridoDomainService>();

        private GrupoDocumentoRequeridoDomainService GrupoDocumentoRequeridoDomainService => Create<GrupoDocumentoRequeridoDomainService>();

        private ConfiguracaoEtapaPaginaDomainService ConfiguracaoEtapaPaginaDomainService => Create<ConfiguracaoEtapaPaginaDomainService>();

        private ConfiguracaoEtapaBloqueioDomainService ConfiguracaoEtapaBloqueioDomainService => Create<ConfiguracaoEtapaBloqueioDomainService>();

        private ServicoTaxaDomainService ServicoTaxaDomainService => Create<ServicoTaxaDomainService>();

        private ServicoTipoNotificacaoDomainService ServicoTipoNotificacaoDomainService => Create<ServicoTipoNotificacaoDomainService>();

        private ProcessoEtapaConfiguracaoNotificacaoDomainService ProcessoEtapaConfiguracaoNotificacaoDomainService => Create<ProcessoEtapaConfiguracaoNotificacaoDomainService>();

        #endregion [ DomainServices ]

        public ProcessoEtapaCabecalhoVO BuscarCabecalhoProcessoEtapa(long seqProcessoEtapa)
        {
            return this.SearchProjectionByKey(new SMCSeqSpecification<ProcessoEtapa>(seqProcessoEtapa), p => new ProcessoEtapaCabecalhoVO()
            {
                Seq = p.Seq,
                SeqProcesso = p.SeqProcesso,
                DescricaoProcesso = p.Processo.Descricao,
                SeqCicloLetivo = p.Processo.SeqCicloLetivo,
                DescricaoCicloLetivo = p.Processo.CicloLetivo.Descricao,
                DataInicio = p.Processo.DataInicio,
                DataFim = p.Processo.DataFim,
                DataEncerramento = p.Processo.DataEncerramento,
                SeqEtapaSgf = p.SeqEtapaSgf,
                SituacaoEtapa = p.SituacaoEtapa
            });
        }

        public List<SMCDatasourceItem> BuscarSituacoesEtapasSgfSelect(long? seqProcessoEtapaSgf, long? seqServico)
        {
            var retorno = new List<SMCDatasourceItem>();

            if (seqProcessoEtapaSgf.HasValue)
            {
                var spec = new SMCSeqSpecification<Servico>(seqServico.GetValueOrDefault());

                var seqTemplateProcessoSgf = ServicoDomainService.SearchProjectionByKey(spec, s => s.SeqTemplateProcessoSgf);

                retorno = SGFHelper.BuscarEtapasSGFCache(seqTemplateProcessoSgf)
                                   .FirstOrDefault(e => e.Seq == seqProcessoEtapaSgf)
                                   .Situacoes.OrderBy(o => o.CategoriaSituacao).ThenBy(o => o.Descricao)
                                   .Select(s => new SMCDatasourceItem()
                                   {
                                       Seq = s.Seq,
                                       Descricao = $"{SMCEnumHelper.GetDescription(s.CategoriaSituacao)} - {s.Descricao}"
                                   }).ToList();
            }

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarSituacoesEtapasComCategoriaSelect(long? seqProcessoEtapa, List<long> seqsProcessos)
        {
            var retorno = new List<SMCDatasourceItem>();
            var listaOrdenada = new List<CategoriaSituacaoVO>();

            if (seqProcessoEtapa.HasValue)
            {
                var result = this.SearchProjectionByKey(new SMCSeqSpecification<ProcessoEtapa>(seqProcessoEtapa.Value), p => new
                {
                    p.SeqEtapaSgf,
                    p.Processo.Servico.SeqTemplateProcessoSgf,
                });

                listaOrdenada = SGFHelper.BuscarEtapasSGFCache(result.SeqTemplateProcessoSgf).FirstOrDefault(e => e.Seq == result.SeqEtapaSgf).Situacoes.Select(s => new CategoriaSituacaoVO()
                {
                    Seq = s.Seq,
                    Descricao = $"{SMCEnumHelper.GetDescription(s.CategoriaSituacao)} - {s.Descricao}",
                    Ordem = (long)s.CategoriaSituacao
                }).ToList();
            }
            //Não foi possivel colocar long? porque o Dynamic não reconheceu como tipo primitivo e por isso passava parâmetro errado
            else if (seqsProcessos.Count > 0)
            {
                var processos = ProcessoDomainService.SearchBySpecification(new SMCContainsSpecification<Processo, long>(p => p.Seq, seqsProcessos.ToArray()), IncludesProcesso.Etapas);

                foreach (var processoEtapa in processos.Select(p => p.Etapas))
                {
                    var result = this.SearchProjectionByKey(new SMCSeqSpecification<ProcessoEtapa>(seqProcessoEtapa.Value), p => new
                    {
                        p.SeqEtapaSgf,
                        p.Processo.Servico.SeqTemplateProcessoSgf
                    });

                    var situacoesDaEtapa = SGFHelper.BuscarEtapasSGFCache(result.SeqTemplateProcessoSgf).Where(e => e.Seq == result.SeqEtapaSgf).SelectMany(e => e.Situacoes.Select(s => new CategoriaSituacaoVO()
                    {
                        Seq = s.Seq,
                        Descricao = $"{SMCEnumHelper.GetDescription(s.CategoriaSituacao)} - {s.Descricao}",
                        Ordem = (long)s.CategoriaSituacao
                    })).ToList();

                    listaOrdenada.AddRange(situacoesDaEtapa.Except(listaOrdenada));
                }
            }

            retorno = listaOrdenada.OrderBy(o => o.Ordem).ThenBy(t => t.Seq).Select(s => new SMCDatasourceItem()
            {
                Seq = s.Seq,
                Descricao = s.Descricao
            }).ToList();

            return retorno;
        }

        public ProcessoEtapa BuscarProcessoEtapa(long seqProcessoEtapa)
        {
            return this.SearchByKey(new SMCSeqSpecification<ProcessoEtapa>(seqProcessoEtapa), IncludesProcessoEtapa.Processo_Servico_TipoServico | IncludesProcessoEtapa.Escalonamentos | IncludesProcessoEtapa.SituacoesItemMatricula | IncludesProcessoEtapa.FiltrosDados);
        }

        public List<SMCDatasourceItem> BuscarSituacoesEtapaPorProcessoEtapaSelect(long seqProcessoEtapa)
        {
            //Cria a lista para retorno retorno
            var retorno = new List<SMCDatasourceItem>();

            //Recupera o processo Etapa
            var result = this.SearchByKey(new SMCSeqSpecification<ProcessoEtapa>(seqProcessoEtapa));

            if (result != null)
            {
                //Verifica se o Processo etapa está no periodo de vigência
                var EtapaNoPeriodoVigencia = result.DataInicio.HasValue && DateTime.Now >= result.DataInicio.Value && (!result.DataFim.HasValue || DateTime.Now <= result.DataFim.Value);

                //Adiciona a situação LIBERADA a lista de retorno
                //A situação "Liberada" deverá ser exibida para seleção em qualquer situação: "Aguardando Liberação", "Em manutenção" ou "Liberada".
                retorno.Add(new SMCDatasourceItem() { Seq = (long)SituacaoEtapa.Liberada, Descricao = SMCEnumHelper.GetDescription(SituacaoEtapa.Liberada) });

                //A situação "Em manutenção" deverá ser exibida para seleção somente quando a situação for "Liberada" E somente se a etapa estiver no período de vigência.
                //A situação "Aguardando Liberação" deverá ser exibida para seleção somente quando a situação for "Liberada" E somente se a etapa ainda não entrar no período de vigência.
                if (result.SituacaoEtapa == SituacaoEtapa.Liberada && EtapaNoPeriodoVigencia)
                    retorno.Add(new SMCDatasourceItem() { Seq = (long)SituacaoEtapa.EmManutencao, Descricao = SMCEnumHelper.GetDescription(SituacaoEtapa.EmManutencao) });
                else
                    retorno.Add(new SMCDatasourceItem() { Seq = (long)SituacaoEtapa.AguardandoLiberacao, Descricao = SMCEnumHelper.GetDescription(SituacaoEtapa.AguardandoLiberacao) });
            }

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarTiposPrazoAtendimentoEtapa(long seqProcessoEtapa)
        {
            var retorno = new List<SMCDatasourceItem>();

            var result = this.SearchByKey(new SMCSeqSpecification<ProcessoEtapa>(seqProcessoEtapa), IncludesProcessoEtapa.Processo_Servico_TipoServico);

            if (result.Processo.Servico.TipoServico.ExigeEscalonamento)
                retorno.Add(new SMCDatasourceItem() { Seq = (long)TipoPrazoEtapa.Escalonamento, Descricao = SMCEnumHelper.GetDescription(TipoPrazoEtapa.Escalonamento) });
            else
            {
                foreach (var item in Enum.GetValues(typeof(TipoPrazoEtapa)).Cast<TipoPrazoEtapa>())
                {
                    if (item != TipoPrazoEtapa.Escalonamento && (long)item != 0)
                    {
                        retorno.Add(new SMCDatasourceItem() { Seq = (long)item, Descricao = SMCEnumHelper.GetDescription(item) });
                    }
                }
            }

            return retorno;
        }

        public long SalvarProcessoEtapa(ProcessoEtapa modelo)
        {
            ValidarModeloSalvar(modelo);

            //VALIDAÇÃO PARA AS SEÇÕES NÃO OBRIGATÓRIAS QUE NÃO ESTIVEREM PREENCHIDAS FICAREM COMO LISTAS VAZIAS PARA OS RELACIONAMENTOS (FILHOS) SEREM REMOVIDOS
            modelo.SituacoesItemMatricula = modelo.SituacoesItemMatricula != null ? modelo.SituacoesItemMatricula : new List<SituacaoItemMatricula>();
            modelo.FiltrosDados = modelo.FiltrosDados != null ? modelo.FiltrosDados : new List<ProcessoEtapaFiltroDado>();
            modelo.OrientacaoAtendimento = modelo.CentralAtendimento ? null : modelo.OrientacaoAtendimento;

            this.SaveEntity(modelo);

            return modelo.Seq;
        }

        public void ValidarModeloSalvar(ProcessoEtapa modelo)
        {
            var processo = this.ProcessoDomainService.SearchByKey(new SMCSeqSpecification<Processo>(modelo.SeqProcesso), IncludesProcesso.Etapas);
            var processoEtapaOld = this.SearchByKey(new SMCSeqSpecification<ProcessoEtapa>(modelo.Seq), IncludesProcessoEtapa.Escalonamentos_GruposEscalonamento);
            var processoEtapaPosterior = BuscarProcessoEtapaPosterior(modelo.Seq);
            var processoEtapaAnterior = BuscarProcessoEtapaAnterior(modelo.Seq);

            if (modelo.TipoPrazoEtapa == TipoPrazoEtapa.Escalonamento)
            {
                //SE O TIPO DE PRAZO DAS DEMAIS ETAPAS FOR DIFERENTE, ABORTAR
                if (processo.Etapas.Any() && processo.Etapas.Where(a => a.Seq != modelo.Seq && a.TipoPrazoEtapa.HasValue && a.TipoPrazoEtapa != TipoPrazoEtapa.Nenhum).Any(a => a.TipoPrazoEtapa != TipoPrazoEtapa.Escalonamento))
                    throw new ProcessoEtapaTipoPrazoEscalonamentoException();
            }
            else if (modelo.TipoPrazoEtapa == TipoPrazoEtapa.PeriodoVigencia)
            {
                /*ESTE TRECHO FOI INSERIDO PORQUE POR ALGUM MOTIVO AS DATAS ESTAVAM SENDO SALVAS COM SEGUNDOS NO BANCO, E AÍ 
                //AO VALIDAR CONSIDERA O SEGUNDO, E MESMO QUE HORA E MINUTO DO PROCESSO E DA ETAPA SEJAM IGUAIS, DÁ EXCEÇÃO 
                //SE TIVER COM SEGUNDO PREENCHIDO, E NAS TELAS OS CAMPOS DE HORA SÓ VEM COM HORA E MINUTO.. POR ISSO DESCONSIDERA O SEGUNDO E VALIDA
                INSERI EM NOVO DATETIME PORQUE O ADDMINUTES, ADDHOURS, ADDSECONDS NÃO FUNCIONA COM A HORA JÁ PREENCHIDA*/

                var dataInicioHoraMinuto = modelo.DataInicio.HasValue ? new DateTime(modelo.DataInicio.Value.Year, modelo.DataInicio.Value.Month, modelo.DataInicio.Value.Day, modelo.DataInicio.Value.Hour, modelo.DataInicio.Value.Minute, 0) : modelo.DataInicio;
                var dataFimHoraMinuto = modelo.DataFim.HasValue ? new DateTime(modelo.DataFim.Value.Year, modelo.DataFim.Value.Month, modelo.DataFim.Value.Day, modelo.DataFim.Value.Hour, modelo.DataFim.Value.Minute, 0) : modelo.DataFim;
                var dataInicioProcessoHoraMinuto = new DateTime(processo.DataInicio.Year, processo.DataInicio.Month, processo.DataInicio.Day, processo.DataInicio.Hour, processo.DataInicio.Minute, 0);
                var dataFimProcessoHoraMinuto = processo.DataFim.HasValue ? new DateTime(processo.DataFim.Value.Year, processo.DataFim.Value.Month, processo.DataFim.Value.Day, processo.DataFim.Value.Hour, processo.DataFim.Value.Minute, 0) : processo.DataFim;

                if (modelo.DataInicio.HasValue && modelo.DataFim.HasValue)
                {
                    //DATA FIM NÃO PODE SER MENOR QUE A DATA DE INÍCIO
                    if (modelo.DataFim < modelo.DataInicio)
                        throw new ProcessoDataFimMenorDataInicioException();

                    //DATA FIM NÃO PODE SER MENOR QUE A DATA ATUAL
                    if (modelo.DataFim < DateTime.Now)
                        throw new ProcessoEtapaDataFimMenorDataAtualException();
                }

                //SE O TIPO DE PRAZO DAS DEMAIS ETAPAS FOR DIFERENTE, ABORTAR
                if (processo.Etapas.Any() && processo.Etapas.Where(a => a.Seq != modelo.Seq && a.TipoPrazoEtapa.HasValue && a.TipoPrazoEtapa != TipoPrazoEtapa.Nenhum).Any(a => a.TipoPrazoEtapa != TipoPrazoEtapa.PeriodoVigencia))
                    throw new ProcessoEtapaTipoPrazoPeriodoVigenciaException();

                //SE O PERÍODO DA ETAPA NÃO ESTIVER DENTRO DO PERÍODO DO PROCESSO, ABORTAR
                if (dataInicioHoraMinuto < dataInicioProcessoHoraMinuto || (modelo.DataFim.HasValue && processo.DataFim.HasValue && dataFimHoraMinuto.Value > dataFimProcessoHoraMinuto.Value))
                    throw new ProcessoEtapaDataInicioFimEtapaForaPeriodoProcessoException();

                //SE HOUVER ETAPA POSTERIOR E, O PARÂMETRO DA ETAPA POSTERIOR IND_FINALIZACAO_ETAPA_ANTERIOR FOR SIM, NÃO PERMITIR QUE A DATA FIM DA ETAPA EM QUESTÃO SEJA MAIOR QUE A DATA INÍCIO DA ETAPA POSTERIOR
                if (processoEtapaPosterior != null && processoEtapaPosterior.FinalizacaoEtapaAnterior && modelo.DataFim.HasValue && processoEtapaPosterior.DataInicio.HasValue && modelo.DataFim > processoEtapaPosterior.DataInicio)
                    throw new ProcessoEtapaDataFimMaiorEtapaPosteriorException();

                //SE HOUVER ETAPA ANTERIOR E, O PARÂMETRO DA ETAPA EM QUESTÃO IND_FINALIZACAO_ETAPA_ANTERIOR FOR SIM, NÃO PERMITIR QUE A DATA INÍCIO DA ETAPA EM QUESTÃO SEJA MENOR QUE A DATA FIM DA ETAPA ANTERIOR
                if (processoEtapaAnterior != null && modelo.FinalizacaoEtapaAnterior && modelo.DataInicio.HasValue && processoEtapaAnterior.DataFim.HasValue && modelo.DataInicio < processoEtapaAnterior.DataFim)
                    throw new ProcessoEtapaDataInicioMenorEtapaAnteriorException();
            }
            else if (modelo.TipoPrazoEtapa == TipoPrazoEtapa.SemPrazoEspecifico || modelo.TipoPrazoEtapa == TipoPrazoEtapa.DiasCorridos || modelo.TipoPrazoEtapa == TipoPrazoEtapa.DiasUteis)
            {
                //SE O TIPO DE PRAZO DAS DEMAIS ETAPAS FOR DIFERENTE, ABORTAR
                if (processo.Etapas.Any() && processo.Etapas.Where(a => a.Seq != modelo.Seq && a.TipoPrazoEtapa.HasValue && a.TipoPrazoEtapa != TipoPrazoEtapa.Nenhum).Any(a => a.TipoPrazoEtapa != TipoPrazoEtapa.SemPrazoEspecifico && a.TipoPrazoEtapa != TipoPrazoEtapa.DiasCorridos && a.TipoPrazoEtapa != TipoPrazoEtapa.DiasUteis))
                    throw new ProcessoEtapaTipoPrazoSemPrazoDiasCorridosUteisException();
            }
        }

        public bool ValidarAssertSalvar(ProcessoEtapa modelo)
        {
            //IRÁ EXIBIR O ASSERT AO SALVAR UMA ETAPA SE O TIPO DE PRAZO DA ETAPA FOR IGUAL A "ESCALONAMENTO" 
            //E HÁ ASSOCIADO NA ETAPA ALGUM ESCALONAMENTO NÃO ENCERRADO, QUE ESTEJA ASSOCIADA A ALGUM GRUPO E, 
            //O PARÂMETRO DA ETAPA IND_FINALIZACAO_ETAPA_ANTERIOR FOI ALTERADO DE NÃO PARA SIM 

            var processoEtapaOld = this.SearchByKey(new SMCSeqSpecification<ProcessoEtapa>(modelo.Seq), IncludesProcessoEtapa.Escalonamentos_GruposEscalonamento);

            if (modelo.TipoPrazoEtapa == TipoPrazoEtapa.Escalonamento)
            {
                if (processoEtapaOld.Escalonamentos.Any() && processoEtapaOld.Escalonamentos.Any(a => !a.DataEncerramento.HasValue && a.GruposEscalonamento != null && a.GruposEscalonamento.Any()))
                {
                    if (!processoEtapaOld.FinalizacaoEtapaAnterior && modelo.FinalizacaoEtapaAnterior)
                        return true;
                }
            }

            return false;
        }

        public bool ValidarAssertEscalonamentoBloqueiosEncerrarEtapa(long seqProcessoEtapa)
        {
            var processoEtapa = this.SearchByKey(new SMCSeqSpecification<ProcessoEtapa>(seqProcessoEtapa), x => x.Escalonamentos);
            var processo = this.ProcessoDomainService.SearchByKey(new SMCSeqSpecification<Processo>(processoEtapa.SeqProcesso), x => x.Servico);

            /*
                Se o tipo de prazo da etapa for igual a Escalonamento e estão parametrizados para a etapa os bloqueios PARCELA_PRE_MATRICULA_PENDENTE, PARCELA_MATRICULA_PENDENTE e/ou PARCELA_SERVICO_ADICIONAL_PENDENTE. 
                Deverá ser avaliado se a data atual (hoje) é maior que a [data de referência]* que permite o encerramento da etapa. Em caso negativo, ao clicar no comando, exibir a seguinte mensagem informativa:
                "O prazo para receber do banco a baixa de pagamento da parcela de pré-matrícula e/ou matrícula ainda está vigente. Tem certeza que deseja encerrar a etapa?"

                [Data de referência]* = será considerado como data de referência.
                - Se o serviço do processo for para Ingressante, considera mais 5 dias da data de vencimento da parcela.
                - Se o serviço do processo for para Aluno, considera mais 10 dias da data de vencimento da parcela
             */

            ConfiguracaoEtapaBloqueioFilterSpecification spec = new ConfiguracaoEtapaBloqueioFilterSpecification()
            {
                TokensMotivoBloqueio = new List<string>() { TOKEN_MOTIVO_BLOQUEIO.PARCELA_PRE_MATRICULA_PENDENTE, TOKEN_MOTIVO_BLOQUEIO.PARCELA_MATRICULA_PENDENTE, TOKEN_MOTIVO_BLOQUEIO.PARCELA_SERVICO_ADICIONAL_PENDENTE },
                SeqProcessoEtapa = seqProcessoEtapa
            };

            if (ConfiguracaoEtapaBloqueioDomainService.Count(spec) > 0)
            {
                Escalonamento e = processoEtapa.Escalonamentos.FirstOrDefault(a => !a.DataEncerramento.HasValue && a.DataFim < DateTime.Now);
                List<GrupoEscalonamento> gruposEscalonamento = GrupoEscalonamentoDomainService.SearchBySpecification(new GrupoEscalonamentoFilterSpecification() { SeqEscalonamento = e.Seq }, a => a.Itens[0].Parcelas).ToList();
                DateTime dataVencimentoParcela = default(DateTime);

                foreach (var grupoEscalonamento in gruposEscalonamento)
                {
                    foreach (var grupoEscalonamentoItem in grupoEscalonamento.Itens)
                    {
                        foreach (var parcela in grupoEscalonamentoItem.Parcelas)
                        {
                            dataVencimentoParcela = (parcela.DataVencimentoParcela > dataVencimentoParcela) ? parcela.DataVencimentoParcela : dataVencimentoParcela;
                        }
                    }
                }

                if (processo.Servico.TipoAtuacao == TipoAtuacao.Ingressante)
                {
                    // Adicionando 5 dias úteis para validar o vencimento.
                    dataVencimentoParcela = SMCDateTimeHelper.AddBusinessDays(dataVencimentoParcela, 5, null);
                }
                else if (processo.Servico.TipoAtuacao == TipoAtuacao.Aluno)
                {
                    // Adicionando 10 dias úteis para validar o vencimento.
                    dataVencimentoParcela = SMCDateTimeHelper.AddBusinessDays(dataVencimentoParcela, 10, null);
                }

                if (DateTime.Now.Date > dataVencimentoParcela.Date)
                    return false;
                else
                    return true;
            }

            return false;
        }

        public void SalvarProcessoEtapaOrigemSGF(long seqProcesso, List<long> seqsEtapasSGF)
        {
            var processo = this.ProcessoDomainService.SearchByKey(new SMCSeqSpecification<Processo>(seqProcesso), x => x.Servico.TipoServico);
            var aplicacaoAdministrativo = AplicacaoService.BuscarAplicacaoPelaSigla(SIGLA_APLICACAO.SGA_ADMINISTRATIVO);

            foreach (long seqEtapa in seqsEtapasSGF)
            {
                var etapaSgf = SGFHelper.BuscarEtapaSGFPorSeqEtapaCache(seqEtapa);

                ProcessoEtapa modelo = new ProcessoEtapa()
                {
                    SeqProcesso = seqProcesso,
                    SeqEtapaSgf = seqEtapa,
                    Ordem = (Int16)etapaSgf.Ordem,
                    DescricaoEtapa = etapaSgf.Descricao,
                    Token = etapaSgf.Token,
                    SituacaoEtapa = SituacaoEtapa.AguardandoLiberacao,
                    DataInicio = null,
                    DataFim = null,
                    DataEncerramento = null,
                    TipoPrazoEtapa = null,
                    NumeroDiasPrazoEtapa = null,
                    CentralAtendimento = false,
                    FinalizacaoEtapaAnterior = false,
                    SolicitacaoEtapaAnteriorAtendida = false,
                    ExibeItemMatriculaSolicitante = false,
                    ExibeItemAposTerminoEtapa = false,
                    EtapaCompartilhada = false,
                    ControleVaga = false,
                    Escalonamentos = new List<Escalonamento>()
                };

                if (processo.Servico.TipoServico.ExigeEscalonamento)
                    modelo.TipoPrazoEtapa = TipoPrazoEtapa.Escalonamento;

                if (etapaSgf.SeqAplicacaoSAS == aplicacaoAdministrativo.Seq)
                    modelo.CentralAtendimento = true;

                this.SaveEntity(modelo);
            }
        }

        //MÉTODO QUE ESTAVA IMPLEMENTADO ANTERIORMENTE PARA SALVAR O PROCESSO ETAPA
        private void ValidarModelo(ProcessoEtapa modelo)
        {
            //Recupera o processo
            var processo = this.ProcessoDomainService.SearchByKey(new SMCSeqSpecification<Processo>(modelo.SeqProcesso), IncludesProcesso.Servico_TipoServico | IncludesProcesso.Etapas_Escalonamentos | IncludesProcesso.Configuracoes | IncludesProcesso.GruposEscalonamento);

            //Caso o tipo do serviço do processo esteja configurado para NÃO EXIGIR ESCALONAMENTO
            if (!processo.Servico.TipoServico.ExigeEscalonamento)
            {
                //Se o valor selecionado no campo “Permite iniciar a vigência da etapa somente após o fim da vigência
                //da etapa anterior?” for “Sim”, e a quantidade de etapar so processo for maior que 1 e o tipo
                //de prazo da etapa for igual a "PERIODO DE VIGÊNCIA", verificar se a
                //Data /Hora Início da etapa em questão é maior do que a Data/Hora Fim da etapa anterior a esta.
                //Caso NÃO ocorra, abortar operação e exibir mensagem de erro.
                if (modelo.FinalizacaoEtapaAnterior &&
                    processo.Etapas.Count > 1 &&
                    modelo.TipoPrazoEtapa == TipoPrazoEtapa.PeriodoVigencia)
                {
                    //Recupera as etapas do processo no SGF, ordenadas pelo numero de ordem
                    var etapasSgf = this.EtapaService.BuscarEtapas(processo.Etapas.Select(e => e.SeqEtapaSgf).ToArray());

                    //Recupera o numero de ordem da etapa que está sendo gravada
                    var numeroOrdemSgf = etapasSgf.First(e => e.Seq == modelo.SeqEtapaSgf).Ordem;

                    //Recupera a etapa anterior a que está sendo gravada
                    var etapaAnteriorSgf = etapasSgf.Where(e => e.Ordem < numeroOrdemSgf).OrderByDescending(e => e.Ordem).FirstOrDefault();

                    //Caso exista etapa anterior
                    if (etapaAnteriorSgf != null)
                    {
                        //Recupera a data fim da etapa anterior e a data inicio da etapa que está sendo gravada
                        var dataFimAnterior = processo.Etapas.First(e => e.SeqEtapaSgf == etapaAnteriorSgf.Seq).DataFim;

                        //Caso a data inicio da etapa que está sendo gravada seja menor que a data fim da etapa
                        //anterior, retorna erro.
                        if (modelo.DataInicio < dataFimAnterior)
                            throw new ProcessoEtapaDataFimAnteriorMenorDataInicioException();
                    }
                }

                //Caso o tipo de prazo da etapa for configurado como "PARÍODO DE VIGÊNCIA"
                //A "Data/Hora Início" e a "Data/Hora Fim" da ETAPA deverão estar compreendidas entre
                //a "Data/Hora Início" e a "Data/Hora Fim" do PROCESSO.
                //Caso NÃO ocorra, abortar a operação e exibir mensagem de erro.
                if (modelo.TipoPrazoEtapa == TipoPrazoEtapa.PeriodoVigencia)
                {
                    if (modelo.DataInicio < processo.DataInicio || (modelo.DataFim.HasValue && processo.DataFim.HasValue && modelo.DataFim.Value > processo.DataFim.Value))
                        throw new ProcessoEtapaDataInicioFimEtapaForaPeriodoProcessoException();
                }
            }
            //Caso o tipo do serviço do processo esteja configurado para EXIGIR ESCALONAMENTO
            else
            {
                //Para cada escalonamento, verificar se existem parcelas cadastradas de acordo com os
                //grupos de escalonamento. Caso ocorra, verificar se a “Data / Hora fim” do escalonamento
                //é menor ou igual à data de vencimento de todas as parcelas existentes.
                //Caso NÃO seja. Abortar a operação e exibir mensagem de erro.
                if (modelo.Escalonamentos.Count > 0)
                {
                    //Cria o spec para recuperar os escalonamentos configurados para a Etapa
                    var spec = new SMCContainsSpecification<Escalonamento, long>(e => e.Seq, modelo.Escalonamentos.Select(e => e.Seq).ToArray());

                    //Recupera os escalonamentos da etapa
                    var escalonamentos = this.EscalonamentoDomainService.SearchBySpecification(spec, IncludesEscalonamento.GruposEscalonamento_Parcelas);

                    //Para cada escalonamento...
                    foreach (var escalonamento in escalonamentos)
                    {
                        //Caso a data de vencimento da parcela for menor que a data fim do escalonamento, exibe mensagem de erro
                        if (escalonamento.GruposEscalonamento.Any(g => g.Parcelas.Any(p => p.DataVencimentoParcela < escalonamento.DataFim)))
                            throw new ProcessoEtapaDataVencimentoParcelaMenorDataFimEscalonamentoException();
                    }
                }

                //Se o valor selecionado no campo “Permite iniciar a vigência da etapa somente após o fim da
                //vigência da etapa anterior?” for “Sim”, para cada escalonamento, verificar se a Data/Hora Início
                //é maior do que a MAIOR Data/Hora Fim dos escalonamentos da etapa anterior a esta.
                //Caso NÃO ocorra, abortar operação e exibir mensagem de erro.
                if (modelo.FinalizacaoEtapaAnterior)
                {
                    //Recupera as etapas do processo no SGF, ordenadas pelo numero de ordem
                    var etapasSgf = this.EtapaService.BuscarEtapas(processo.Etapas.Select(e => e.SeqEtapaSgf).ToArray());

                    //Recupera o numero de ordem da etapa do SGF que está sendo gravada
                    var numeroOrdemSgf = etapasSgf.First(e => e.Seq == modelo.SeqEtapaSgf).Ordem;

                    //Recupera a etapa anterior do SGF a que está sendo gravada
                    var etapaAnteriorSgf = etapasSgf.Where(e => e.Ordem < numeroOrdemSgf).OrderByDescending(e => e.Ordem).FirstOrDefault();

                    //Caso exista etapa anterior do SGF
                    if (etapaAnteriorSgf != null)
                    {
                        //Recupera a maior data fim de todos os escalonamentos da etapa anterior
                        var maiorDataFimEscalonamentoEtapaAnterior = processo.Etapas.First(e => e.SeqEtapaSgf == etapaAnteriorSgf.Seq).Escalonamentos.Max(e => e.DataFim);

                        //Caso a data inicio de algum dos escalonamentos da etapa que está sendo gravada seja menor que
                        //a maior data dos escalonamentos da etapa anterior, retorna erro.
                        if (modelo.Escalonamentos.Any(e => e.DataInicio < maiorDataFimEscalonamentoEtapaAnterior))
                            throw new ProcessoEtapaDataInicioEscalonamentoMenorDataFimEscalonamentoAnteriorException();
                    }
                }

                //A "Data/Hora Início" e a "Data/Hora Fim" do ESCALONAMENTO deverão estar compreendidas entre
                //a "Data/Hora Início" e a "Data/Hora Fim" do PROCESSO.
                //Caso não ocorra, abortar a operação e exibir mensagem de erro
                if (modelo.Escalonamentos.Any(e => e.DataInicio < processo.DataInicio || (processo.DataFim.HasValue && e.DataFim > processo.DataFim.Value)))
                    throw new ProcessoEtapaDataInicioFimEscalonamentoForaPeriodoProcessoException();
            }

            //Se o valor selecionado no campo “Situação” for “Liberada”, verificar as seguintes regras:
            if (modelo.SituacaoEtapa == SituacaoEtapa.Liberada)
            {
                //Verificar se existe pelo menos uma configuração do processo cadastra para o processo
                //em questão. Caso NÃO ocorra, abortar a operação e exibir mensagem de erro
                if (processo.Configuracoes.Count == 0)
                    throw new ProcessoEtapaProcessoSemNenhumaConfiguracaoException();

                //Verificar se existe pelo menos uma configuração de etapa cadastrada para a etapa em questão.
                //Caso NÃO ocorra, abortar a operação e exibir mensagem de erro
                if (this.ConfiguracaoEtapaDomainService.Count(new ConfiguracaoEtapaFilterSpecification() { SeqProcessoEtapa = modelo.Seq }) == 0)
                    throw new ProcessoEtapaEtapaSemNenhumaConfiguracaoException();

                //Se o tipo de serviço do serviço correspondente ao processo em questão tiver sido configurado
                //para EXIGIR ESCALONAMENTO,
                if (processo.Servico.TipoServico.ExigeEscalonamento)
                {
                    //Verificar se existe pelo menos um escalonamento para a etapa em questão.
                    //Caso NÃO ocorra, abortar a operação e exibir mensagem de erro
                    if (modelo.Escalonamentos.Count == 0)
                        throw new ProcessoEtapaEtapaSemNenhumEscalonamentoException();

                    //Verificar se existe pelo menos um grupo de escalonamento para a processo em questão.
                    //Caso NÃO ocorra, abortar a operação e exibir mensagem de erro
                    if (processo.GruposEscalonamento.Count == 0)
                        throw new ProcessoEtapaProcessoSemNenhumGrupoEscalonamentoException();
                }

                //Para cada configuração da etapa, se existir pelo menos um documento requerido que PERMITE
                //upload de arquivo, verificar se existe a página com o Token UPLOAD_DOCUMENTOS no fluxo de
                //páginas dessa configuração.
                //Caso NÃO ocorra, abortar a operação e exibir mensagem de erro
                var etapa = this.SearchByKey(new SMCSeqSpecification<ProcessoEtapa>(modelo.Seq),
                    IncludesProcessoEtapa.ConfiguracoesEtapa
                    | IncludesProcessoEtapa.ConfiguracoesEtapa_DocumentosRequeridos
                    | IncludesProcessoEtapa.ConfiguracoesEtapa_ConfiguracoesPagina);

                //Caso tenha documento requerido para permitir upload e não tenha página configurada
                if (etapa.ConfiguracoesEtapa.Any(ce => ce.DocumentosRequeridos.Any(dr => dr.PermiteUploadArquivo) && !ce.ConfiguracoesPagina.Any(cp => cp.TokenPagina == TOKEN_CONFIGURACAO_ETAPA_PAGINA.UPLOAD_DOCUMENTOS)))
                    throw new ProcessoEtapaConfiguracaoEtapaSemTokenUploadException();

                //Caso não tenha documento requerido para permitir upload e tenha página configurada
                if (etapa.ConfiguracoesEtapa.Any(ce => !ce.DocumentosRequeridos.Any(dr => dr.PermiteUploadArquivo) && ce.ConfiguracoesPagina.Any(cp => cp.TokenPagina == TOKEN_CONFIGURACAO_ETAPA_PAGINA.UPLOAD_DOCUMENTOS)))
                    throw new ProcessoEtapaConfiguracaoEtapaSemTokenUploadException();
            }
        }

        /// <summary>
        /// Recupera o token do processo etapa para realiza validações na seleção de turma e de atividade
        /// </summary>
        /// <param name="seqProcessoEtapa"></param>
        /// <returns>token do processo etapa</returns>
        public string BuscarTokenProcessoEtapa(long seqProcessoEtapa)
        {
            return this.SearchProjectionByKey(seqProcessoEtapa, x => x.Token);
        }

        /// <summary>
        /// Colocar Processo Etapa em manutencao
        /// </summary>
        /// <param name="seqProcessoEtapa">Sequencial do Processo Etapa</param>
        /// <returns>Modelo processo etapa</returns>
        public ProcessoEtapa ColocarProcessoEtapaManutencao(long seqProcessoEtapa)
        {
            var result = this.SearchByKey(new SMCSeqSpecification<ProcessoEtapa>(seqProcessoEtapa));

            result.SituacaoEtapa = SituacaoEtapa.EmManutencao;

            this.SaveEntity(result);

            return result;
        }

        /// <summary>
        /// Liberar Processo Etapa
        /// </summary>
        /// <param name="seqProcessoEtapa">Sequencial do Processo Etapa</param>
        /// <returns>Modelo processo etapa</returns>
        public ProcessoEtapa LiberarProcessoEtapa(long seqProcessoEtapa)
        {
            ValidarLiberacaoProcessoEtapa(seqProcessoEtapa);

            var result = this.SearchByKey(new SMCSeqSpecification<ProcessoEtapa>(seqProcessoEtapa));

            result.SituacaoEtapa = SituacaoEtapa.Liberada;

            this.SaveEntity(result);

            return result;
        }

        private void ValidarLiberacaoProcessoEtapa(long seqProcessoEtapa)
        {
            var processoEtapa = this.SearchByKey(new SMCSeqSpecification<ProcessoEtapa>(seqProcessoEtapa), IncludesProcessoEtapa.Processo_UnidadesResponsaveis);
            var servicoTaxas = this.ServicoTaxaDomainService.SearchBySpecification(new ServicoTaxaFilterSpecification() { SeqServico = processoEtapa.Processo.SeqServico }).ToList();
            var etapaSGF = EtapaService.BuscarEtapa(processoEtapa.SeqEtapaSgf);
            var aplicacaoAluno = AplicacaoService.BuscarAplicacaoPelaSigla(SIGLA_APLICACAO.SGA_ALUNO);
            var configuracoesEtapa = this.ConfiguracaoEtapaDomainService.SearchBySpecification(new ConfiguracaoEtapaFilterSpecification() { SeqProcessoEtapa = seqProcessoEtapa }).ToList();
            var servico = ServicoDomainService.BuscarServico(processoEtapa.Processo.SeqServico);

            //Se o tipo de prazo da etapa for NULO, abortar a operação
            if (processoEtapa.TipoPrazoEtapa == null)
                throw new ConfiguracaoEtapaEtapaSemPrazoException();

            //Senão, se não houver nenhuma configuração de etapa, abortar a operação
            if (!configuracoesEtapa.Any())
                throw new ConfiguracaoEtapaLiberarEtapaSemConfiguracaoException();

            foreach (var configuracaoEtapa in configuracoesEtapa)
            {
                var documentosPermitemUploadConfiguracaoEtapa = this.DocumentoRequeridoDomainService.SearchBySpecification(new DocumentoRequeridoFilterSpecification() { SeqConfiguracaoEtapa = configuracaoEtapa.Seq, PermiteUploadArquivo = true }).ToList();
                var documentosConfiguracaoEtapa = this.DocumentoRequeridoDomainService.SearchBySpecification(new DocumentoRequeridoFilterSpecification() { SeqConfiguracaoEtapa = configuracaoEtapa.Seq }).ToList();
                //var paginasUploadConfiguracaoEtapa = this.ConfiguracaoEtapaPaginaDomainService.SearchBySpecification(new ConfiguracaoEtapaPaginaFilterSpecification() { SeqConfiguracaoEtapa = configuracaoEtapa.Seq, Token = TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_PADRAO_SOLICITACAO_UPLOAD_DOCUMENTO }).ToList();
                //var paginasRegistroDocumentoConfiguracaoEtapa = this.ConfiguracaoEtapaPaginaDomainService.SearchBySpecification(new ConfiguracaoEtapaPaginaFilterSpecification() { SeqConfiguracaoEtapa = configuracaoEtapa.Seq, Token = TOKEN_SOLICITACAO_SERVICO.REGISTRO_DOCUMENTO_ENTREGUE }).ToList();var paginasUploadConfiguracaoEtapa = this.ConfiguracaoEtapaPaginaDomainService.SearchBySpecification(new ConfiguracaoEtapaPaginaFilterSpecification() { SeqConfiguracaoEtapa = configuracaoEtapa.Seq, Token = TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_PADRAO_SOLICITACAO_UPLOAD_DOCUMENTO }).ToList();

                var paginasUploadConfiguracaoEtapa = this.ConfiguracaoEtapaPaginaDomainService.SearchBySpecification(new ConfiguracaoEtapaPaginaFilterSpecification() { SeqConfiguracaoEtapa = configuracaoEtapa.Seq, ConfiguracaoDocumento = ConfiguracaoDocumento.UploadDocumento }).ToList();
                var paginasRegistroDocumentoConfiguracaoEtapa = this.ConfiguracaoEtapaPaginaDomainService.SearchBySpecification(new ConfiguracaoEtapaPaginaFilterSpecification() { SeqConfiguracaoEtapa = configuracaoEtapa.Seq, ConfiguracaoDocumento = ConfiguracaoDocumento.RegistroDocumento }).ToList();

                // Se foi configurado documentos que permitem o upload para a etapa em questão e, não foi associada a página de upload (SOLICITACAO_UPLOAD_DOCUMENTO), abortar a operação
                //if (documentosPermitemUploadConfiguracaoEtapa.Any() && !paginasUploadConfiguracaoEtapa.Any())
                //    throw new ProcessoEtapaLiberarEtapaDocumentosSemPaginaUploadException();

                // Se foi configurado documentos para a etapa em questão e, não foi associada a página que teve o parâmetro "Configuração de documento" informado como "Upload de documento" ou "Registro de documento", abortar a operação
                if (documentosConfiguracaoEtapa.Any() && !paginasUploadConfiguracaoEtapa.Any() && !paginasRegistroDocumentoConfiguracaoEtapa.Any())
                    throw new ProcessoEtapaLiberarEtapaDocumentosSemPaginaUploadRegistroException();

                // Se foi associado no fluxo de páginas a página de upload ou registro (SOLICITACAO_UPLOAD_DOCUMENTO ou REGISTRO_DOCUMENTO_ENTREGUE), 
                // mas não foi configurado os documentos para a etapa em questão, abortar a operação
                if (paginasUploadConfiguracaoEtapa.SMCAny() && !documentosConfiguracaoEtapa.Any())
                {
                    throw new ProcessoEtapaLiberarEtapaPaginaSemConfiguracaoDeDocumentosException();
                }
                else if (paginasRegistroDocumentoConfiguracaoEtapa.SMCAny() && !documentosConfiguracaoEtapa.Any()) //Já verificando de uma vez se a etapa a ser liberada não possui documentos
                {
                    bool existeDocumentoConfiguradoEmOutraEtapa = false;
                    ProcessoEtapaFilterSpecification spec = new ProcessoEtapaFilterSpecification() { SeqProcesso = processoEtapa.SeqProcesso };

                    // Pega os processo etapa diferente do que está sendo liberado no momento
                    var listaOutrosProcessoEtapas = this.SearchBySpecification(spec, a => a.Escalonamentos[0].GruposEscalonamento).Where(c => c.Seq != configuracaoEtapa.SeqProcessoEtapa).ToList();

                    foreach (var etapaDiferenteDaAtual in listaOutrosProcessoEtapas)
                    {
                        var configsOutrasEtapas = this.ConfiguracaoEtapaDomainService.SearchBySpecification(new ConfiguracaoEtapaFilterSpecification() { SeqProcessoEtapa = etapaDiferenteDaAtual.Seq }).Where(c => c.SeqConfiguracaoProcesso == configuracaoEtapa.SeqConfiguracaoProcesso).ToList();

                        if (configsOutrasEtapas.Count > 0)
                        {
                            foreach (var outraConfigEtapa in configsOutrasEtapas)
                            {
                                var docsOutrasEtapas = this.DocumentoRequeridoDomainService.SearchBySpecification(new DocumentoRequeridoFilterSpecification() { SeqConfiguracaoEtapa = outraConfigEtapa.Seq }).ToList();

                                if (docsOutrasEtapas.SMCAny())
                                {
                                    existeDocumentoConfiguradoEmOutraEtapa = true;

                                    continue;
                                }
                            }
                        }
                    }

                    if (!existeDocumentoConfiguradoEmOutraEtapa)
                    {
                        throw new ProcessoEtapaLiberarEtapaSemNenhumaEtapaComConfiguracaoDocumentosException();
                    }
                }

                // Se a etapa possuir tipo de notificação parametrizado como obrigatório, para o serviço em questão, verificar se esses tipos estão associados 
                // na etapa que está sendo liberada. Caso não esteja, abortar a operação e exibir mensagem impeditiva
                var tiposNotificacaoObrigatorios = servico.TiposNotificacao?.Where(c => c.SeqEtapaSgf == etapaSGF.Seq && c.Obrigatorio == true).ToList();
                if (tiposNotificacaoObrigatorios != null && tiposNotificacaoObrigatorios?.Count() > 0)
                {
                    var specTipoNotificavaoServico = new ProcessoEtapaConfiguracaoNotificacaoSpecification() { SeqProcessoEtapa = processoEtapa.Seq };
                    var notificacoesNaEtapa = ProcessoEtapaConfiguracaoNotificacaoDomainService.SearchBySpecification(specTipoNotificavaoServico, IncludesProcessoEtapaConfiguracaoNotificacao.TipoNotificacao).ToList();

                    if (tiposNotificacaoObrigatorios.Count() > 0)
                    {
                        string tiposNotificacaoFaltando = "";                      
                        List<long> seqsTipoNotificacaoNaEtapa = new List<long>();
                        if(notificacoesNaEtapa.Count() > 0)
                            seqsTipoNotificacaoNaEtapa = notificacoesNaEtapa.Select(c => c.SeqTipoNotificacao).ToList();
                        
                        var seqTiposNotificacaoServicoObrigatorios = tiposNotificacaoObrigatorios.Select(c => c.SeqTipoNotificacao).ToList();

                        bool existsCheck = seqTiposNotificacaoServicoObrigatorios.All(x => seqsTipoNotificacaoNaEtapa.Any(y => x == y));

                        if (!existsCheck)
                        {
                            var seqsTipoNotificacaoFaltantes = seqTiposNotificacaoServicoObrigatorios.Where(c => seqsTipoNotificacaoNaEtapa.All(d => d != c));
                            var tiposNotificacao = this.NotificacaoService.BuscarTiposNotificacao(seqsTipoNotificacaoFaltantes.ToArray());

                            foreach (var obrigatorio in seqsTipoNotificacaoFaltantes)
                            {
                                tiposNotificacaoFaltando += $"</br>- {tiposNotificacao.FirstOrDefault(c => c.Seq == obrigatorio).Descricao}";
                            }

                            throw new ProcessoEtapaLiberarEtapaTipoNotificacaoObrigatorioFaltanteException(tiposNotificacaoFaltando);
                        }
                    }
                }

                var paginasFormularioConfiguracaoEtapa = this.ConfiguracaoEtapaPaginaDomainService.SearchBySpecification(new ConfiguracaoEtapaPaginaFilterSpecification() { SeqConfiguracaoEtapa = configuracaoEtapa.Seq, Token = TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_PADRAO_SOLICITACAO_FORMULARIO }).ToList();

                //Se foi associado no fluxo de páginas a página de formulário (SOLICITACAO_FORMULARIO), mas não foi associado o formulário que deverá ser exibido, abortar a operação
                if (paginasFormularioConfiguracaoEtapa.Any() && paginasFormularioConfiguracaoEtapa.Any(a => !a.SeqFormulario.HasValue || !a.SeqVisaoFormulario.HasValue))
                    throw new ProcessoEtapaLiberarEtapaPaginaSemFormularioException();

                var paginasCobrancaConfiguracaoEtapa = this.ConfiguracaoEtapaPaginaDomainService.SearchBySpecification(new ConfiguracaoEtapaPaginaFilterSpecification() { SeqConfiguracaoEtapa = configuracaoEtapa.Seq, Token = TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_COBRANCA_TAXA }).ToList();

                //Se o respectivo serviço do processo possui parametrização de cobrança de taxa e a etapa refere-se a aplicação aluno, mas não foi associada a página de cobrança (SOLICITACAO_COBRANCA_TAXA), abortar a operação
                if (servicoTaxas.Any() && etapaSGF != null && aplicacaoAluno != null && etapaSGF.SeqAplicacaoSAS == aplicacaoAluno.Seq && !paginasCobrancaConfiguracaoEtapa.Any())
                    throw new ProcessoEtapaLiberarEtapaSemPaginaCobrancaException();

                //Se a etapa refere-se a aplicação aluno, foi associada a página de cobrança (SOLICITACAO_COBRANCA_TAXA) e o respectivo serviço não possui parametrização de taxa, abortar a operação
                if (etapaSGF != null && aplicacaoAluno != null && etapaSGF.SeqAplicacaoSAS == aplicacaoAluno.Seq && paginasCobrancaConfiguracaoEtapa.Any() && !servicoTaxas.Any())
                    throw new ProcessoEtapaLiberarEtapaSemParametrizacaoTaxaException();

                var specgrupoDocumentos = new GrupoDocumentoRequeridoFilterSpecification() { SeqConfiguracaoEtapa = configuracaoEtapa.Seq };

                // FIX: Carol - Foi solicitado retirar o campo ExibeTermoResponsabilidadeEntrega do GrupoDocumentoRequerido
                var grupoDocumentosConfiguracaoEtapa = this.GrupoDocumentoRequeridoDomainService.SearchProjectionBySpecification(specgrupoDocumentos, p => new
                {
                    p.Seq,
                    p.SeqConfiguracaoEtapa
                    //p.ExibeTermoResponsabilidadeEntrega
                }).ToList();


                // [ RN_SRC_095 - Etapa Processo - Alteração situação etapa ]
                // Senão, se a aplicação SAS da etapa SGF for SGA.Aluno:
                if (etapaSGF != null && aplicacaoAluno != null && etapaSGF.SeqAplicacaoSAS == aplicacaoAluno.Seq)
                {
                    bool existeDocRequeridoEntregaPosterior = documentosConfiguracaoEtapa.SMCAny(c => c.PermiteEntregaPosterior == true);

                    // Se existir a configuração de etapa possui documento requerido marcado para ser entregue posteriormente 
                    // e a descrição do termo de responsabilidade de entrega da documentação não estiver preenchida na respectiva configuração de etapa
                    if ((documentosConfiguracaoEtapa.Count > 0 && existeDocRequeridoEntregaPosterior) && string.IsNullOrEmpty(configuracaoEtapa.DescricaoTermoEntregaDocumentacao?.Trim()))
                    {
                        // Abortar a operação e emitir a mensagem impeditiva: 
                        // "Não é possível liberar a etapa. Existe documento requerido configurado para permitir entrega posterior, 
                        // porém a descrição do termo de responsabilidade de entrega não foi informada."

                        throw new ProcessoEtapaLiberarEtapaDescricaoTermoNaoInformadaException();
                    }

                    // Se existir a configuração de etapa estiver com a descrição do termo de responsabilidade de entrega da documentação preenchida, 
                    // mas não existir nenhum documento requerido marcado para ser entregue posteriormente
                    if (!string.IsNullOrEmpty(configuracaoEtapa.DescricaoTermoEntregaDocumentacao?.Trim()) && (documentosConfiguracaoEtapa.Count > 0 && !existeDocRequeridoEntregaPosterior))
                    {
                        // Abortar a operação e emitir a mensagem impeditiva: 
                        // "Não é possível liberar a etapa. A descrição do termo de responsabilidade de entrega da documentação foi preenchida, 
                        // porém não existe documento requerido configurado para permitir entrega posterior."

                        throw new ProcessoEtapaLiberarEtapaSemDocumentoRequeridoComEntregaPosteriorException();
                    }
                }
            }

            // Se a etapa está parametrizada para ser compartilhada, mas não foi associada nenhuma entidade responsável do tipo compartilhada, abortar a operação
            if (processoEtapa.EtapaCompartilhada && !processoEtapa.Processo.UnidadesResponsaveis.Any(a => a.TipoUnidadeResponsavel == TipoUnidadeResponsavel.EntidadeCompartilhada))
                throw new ProcessoEtapaLiberarEtapaPaginaSemEntidadeCompartilhadaException();
        }

        /// <summary>
        /// Buscar processo etapa por processo
        /// </summary>
        /// <param name="seqProcesso">Sequencial do processo</param>
        /// <returns>Lista dos processo etapas ordenado pelo campo ordem</returns>
        public List<SMCDatasourceItem> BuscarProcessoEtapaPorProcessoSelect(long seqProcesso)
        {
            var spec = new ProcessoEtapaFilterSpecification() { SeqProcesso = seqProcesso };

            spec.SetOrderBy(o => o.Ordem);

            var retorno = this.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
            {
                Seq = p.Seq,
                Descricao = p.DescricaoEtapa
            }).ToList();

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarProcessoEtapaPorServicoSelect(long? seqServico)
        {
            var servico = ServicoDomainService.SearchByKey(new SMCSeqSpecification<Servico>(seqServico.GetValueOrDefault()));
            var etapasSGF = SGFHelper.BuscarEtapasSGFCache(servico.SeqTemplateProcessoSgf).OrderBy(a => a.Ordem);

            var retorno = etapasSGF.Select(x => new SMCDatasourceItem()
            {
                Seq = x.Seq,
                Descricao = x.Descricao

            }).ToList();

            return retorno;
        }

        public void EncerrarEtapa(long seqEtapaProcesso)
        {
            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                //Recuperando o processo etapa...
                ProcessoEtapa processoEtapa = SearchByKey(new SMCSeqSpecification<ProcessoEtapa>(seqEtapaProcesso), a => a.Escalonamentos[0].GruposEscalonamento,
                                                                                                                    a => a.ConfiguracoesEtapa[0].ConfiguracoesBloqueio[0].MotivoBloqueio,
                                                                                                                    a => a.Processo.Servico,
                                                                                                                    a => a.Processo.CicloLetivo,
                                                                                                                    a => a.Processo,
                                                                                                                    a => a.SituacoesItemMatricula);

                //Verificar quais os escalonamentos da etapa (src.processo_etapa) que queremos encerrar estão expirados
                //Verificar em quais grupos de escalonamento os escalonamentos expirados estão associados
                var seqsGruposEscalonamento = processoEtapa.Escalonamentos.Where(a => a.DataFim <= DateTime.Now && !a.DataEncerramento.HasValue)
                                                                                .SelectMany(b => b.GruposEscalonamento)
                                                                                .Select(c => c.SeqGrupoEscalonamento)
                                                                                .Distinct()
                                                                                .ToList();

                //Recuperando todas as solicitações de serviço relacionados a este processo etapa...
                SolicitacaoServicoFilterSpecification spec = new SolicitacaoServicoFilterSpecification()
                {
                    SeqProcessoEtapa = seqEtapaProcesso,
                    TipoFiltroCentralSolicitacao = TipoFiltroCentralSolicitacao.SituacaoEtapaSelecionada
                };
                var solicitacoesServicos = SolicitacaoServicoDomainService.SearchBySpecification(spec, a => a.Etapas[0].HistoricosSituacao[0],
                                                                                                       a => a.PessoaAtuacao.DadosPessoais,
                                                                                                       a => a.ConfiguracaoProcesso.Processo).ToList();
                foreach (var solicitacaoServico in solicitacoesServicos)
                {
                    //Verificar qual a última situação da solicitação.
                    var solicitacaoHistoricoSituacao = solicitacaoServico.Etapas.SelectMany(a => a.HistoricosSituacao.Where(b => !b.DataExclusao.HasValue)).LastOrDefault();
                    var solicitacaoServicoEtapa = SolicitacaoServicoEtapaDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoServicoEtapa>(solicitacaoHistoricoSituacao.SeqSolicitacaoServicoEtapa), a => a.ConfiguracaoEtapa);

                    //Verificar se a última situação está associada a este processo etapa.
                    if (solicitacaoServicoEtapa.ConfiguracaoEtapa.SeqProcessoEtapa == seqEtapaProcesso)
                    {
                        //Verificar se a solicitação não está com a situação parametrizada com a final de etapa
                        if (!EtapaService.BuscarSituacaoEtapa(solicitacaoHistoricoSituacao.SeqSituacaoEtapaSgf).SituacaoFinalEtapa)
                        {
                            if (
                                (processoEtapa.TipoPrazoEtapa == TipoPrazoEtapa.PeriodoVigencia) ||
                                //Verificar se a solicitação está associada a algum dos grupos de escalonamento
                                ((processoEtapa.TipoPrazoEtapa == TipoPrazoEtapa.Escalonamento) && (seqsGruposEscalonamento.Contains(solicitacaoServico.SeqGrupoEscalonamento.GetValueOrDefault())))
                               )
                            {
                                // Efetua o passo 1 e 2 do processo de encerramento de etapa
                                EncerrarSolicitacaoEtapa(processoEtapa, solicitacaoServico, solicitacaoHistoricoSituacao.SeqSolicitacaoServicoEtapa);
                            }
                        }
                    }
                }

                // Efetua o passo 3 do processo de encerramento de etapa
                EncerrarEtapa(processoEtapa);

                transacao.Commit();
            }
        }

        private void EncerrarSolicitacaoEtapa(ProcessoEtapa processoEtapa, SolicitacaoServico solicitacaoServico, long seqSolicitacaoServicoEtapa)
        {
            bool executarPasso2 = true;

            //PASSO 1 – ATUALIZAÇÃO DA SOLICITAÇÃO
            if (solicitacaoServico != null)
            {
                // RN_SRC_062
                var situacoesItensCancelar = new List<ClassificacaoSituacaoFinal?> { ClassificacaoSituacaoFinal.Cancelado, ClassificacaoSituacaoFinal.FinalizadoSemSucesso, ClassificacaoSituacaoFinal.NaoAlterado };

                //Se houver parametrização de bloqueios para a etapa em questão que geram o cancelamento da solicitação.
                var dadosBloqueios = processoEtapa.ConfiguracoesEtapa.SelectMany(a => a.ConfiguracoesBloqueio)
                    .Where(e => e.GeraCancelamentoSolicitacao).Distinct().ToList();

                var bloqueios = PessoaAtuacaoBloqueioDomainService.BuscarPessoaAtuacaoBloqueios(solicitacaoServico.SeqPessoaAtuacao, dadosBloqueios, false);

                if (bloqueios != null && bloqueios.Count > 0)
                {
                    //A solicitação deverá receber a situação parametrizada para a etapa em questão, como a final da etapa, final do processo e a classificação igual à Cancelada
                    EtapaData etapaSGF = EtapaService.BuscarEtapa(processoEtapa.SeqEtapaSgf, IncludesEtapa.Situacoes);
                    if (etapaSGF != null)
                    {
                        SituacaoEtapaData situacaoEtapaSGF = etapaSGF.Situacoes.FirstOrDefault(a => a.SituacaoFinalEtapa && a.SituacaoFinalProcesso && a.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado && !a.SituacaoSolicitante);
                        SolicitacaoHistoricoSituacaoDomainService.AtualizarHistoricoSituacao(seqSolicitacaoServicoEtapa, situacaoEtapaSGF.Seq, MessagesResource.MSG_Solicitacao_Cancelada_Automaticamente);
                    }

                    //Se for uma solicitação de matrícula os [itens de matrícula deverão ser cancelados]* e, o motivo da situação do item deverá receber a situação
                    //"Existência de bloqueio".

                    // Estava considerando todos os itens da solicitação, incluindo os já cancelados. Modifiquei para buscar apenas os itens que não estão cancelados
                    //var sm = SolicitacaoMatriculaDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoMatricula>(solicitacaoServico.Seq), a => a.Itens);
                    var seqsItensSolicitacao = SolicitacaoMatriculaDomainService.SearchProjectionByKey(solicitacaoServico.Seq, x => x.Itens.Where(i => !situacoesItensCancelar.Contains(i.HistoricosSituacao.OrderByDescending(a => a.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal)).Select(w => w.Seq));

                    if (seqsItensSolicitacao != null)
                    {
                        var situacaoItemMatricula = processoEtapa.SituacoesItemMatricula.FirstOrDefault(a => a.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado);

                        foreach (var seqItemSolicitacao in seqsItensSolicitacao)
                        {
                            SolicitacaoMatriculaItemDomainService.LiberarVagaTurmaAtividadeIngressante(seqItemSolicitacao);
                            SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(seqItemSolicitacao, situacaoItemMatricula.Seq, MotivoSituacaoMatricula.ExistenciaBloqueio);
                        }
                    }
                }
                else if (ServicoEtapaAtualizarSolicitacao(processoEtapa))
                {
                    executarPasso2 = false;

                    // Busca a configuração da etapa atual
                    var configuracaoEtapa = SolicitacaoServicoEtapaDomainService.SearchProjectionByKey(seqSolicitacaoServicoEtapa, x => x.SeqConfiguracaoEtapa);

                    // Finaliza a etapa atual da solicitação
                    SolicitacaoServicoDomainService.ProcedimentosFinalizarEtapa(solicitacaoServico.Seq, configuracaoEtapa, ClassificacaoSituacaoFinal.FinalizadoComSucesso, MessagesResource.MSG_Solicitacao_Chancela_Automatica);
                }
                else
                {
                    //A solicitação deverá receber a situação parametrizada para a etapa em questão, como a final da etapa, final do processo e classificação igual a Finalizada sem sucesso
                    EtapaData etapaSGF = EtapaService.BuscarEtapa(processoEtapa.SeqEtapaSgf, IncludesEtapa.Situacoes);
                    if (etapaSGF != null)
                    {
                        SituacaoEtapaData situacaoEtapaSGF = etapaSGF.Situacoes.FirstOrDefault(a => a.SituacaoFinalEtapa && a.SituacaoFinalProcesso && a.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso);
                        if (situacaoEtapaSGF == null)
                            throw new EtapaSemSituacaoFinalException(etapaSGF.Descricao);
                        SolicitacaoHistoricoSituacaoDomainService.AtualizarHistoricoSituacao(seqSolicitacaoServicoEtapa, situacaoEtapaSGF.Seq, MessagesResource.MSG_Solicitacao_FinalizadaSemSucesso_Automaticamente);
                    }

                    //Se for uma solicitação de matrícula, os [itens de matrícula deverão ser cancelados]* e, o motivo da situação do item deverá receber a situação "Etapa não finalizada".
                    // Estava considerando todos os itens da solicitação, incluindo os já cancelados. Modifiquei para buscar apenas os itens que não estão cancelados
                    //var sm = SolicitacaoMatriculaDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoMatricula>(solicitacaoServico.Seq), a => a.Itens);
                    var seqsItensSolicitacao = SolicitacaoMatriculaDomainService.SearchProjectionByKey(solicitacaoServico.Seq, x => x.Itens.Where(i => !situacoesItensCancelar.Contains(i.HistoricosSituacao.OrderByDescending(a => a.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal)).Select(w => w.Seq));

                    if (seqsItensSolicitacao != null)
                    {
                        var situacaoItemMatricula = processoEtapa.SituacoesItemMatricula.FirstOrDefault(a => a.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado);

                        if (situacaoItemMatricula == null)
                            throw new SMCApplicationException(MessagesResource.MSG_SituacaoFinalItemMatriculaNaoEncontrada);

                        foreach (var seqItemSolicitacao in seqsItensSolicitacao)
                        {
                            SolicitacaoMatriculaItemDomainService.LiberarVagaTurmaAtividadeIngressante(seqItemSolicitacao);
                            SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(seqItemSolicitacao, situacaoItemMatricula.Seq, MotivoSituacaoMatricula.EtapaNaoFinalizada);
                        }
                    }
                }

                //PASSO 2 – ATUALIZAÇÃO DO SOLICITANTE
                if (executarPasso2)
                    AtualizaDadosIngressante(processoEtapa, solicitacaoServico, SGFConstants.CONVOCADO_DESISTENTE, SGFConstants.CONVOCADO_DESISTENTE_MATRICULA_NAO_EFETIVADA, "Situação da oferta de inscrição atualizada automaticamente durante o processo de encerramento de etapas no Sistema Acadêmico.");
            }
        }

        private void EncerrarEtapa(ProcessoEtapa processoEtapa)
        {
            //PASSO 3 – REGISTRO DE ENCERRAMENTO DA ETAPA
            if (processoEtapa.TipoPrazoEtapa == TipoPrazoEtapa.Escalonamento)
            {
                //Se o tipo de prazo da etapa for igual a Escalonamento, então o campo data de encerramento do escalonamento deverá ser preenchido com a data corrente (hoje).
                List<Escalonamento> escalonamentosExpirados = processoEtapa.Escalonamentos.Where(a => a.DataFim <= DateTime.Now && !a.DataEncerramento.HasValue).ToList();
                foreach (var escalonamento in escalonamentosExpirados)
                {
                    escalonamento.DataEncerramento = DateTime.Now;
                    EscalonamentoDomainService.SaveEntity(escalonamento);
                }
            }
            else if (processoEtapa.TipoPrazoEtapa == TipoPrazoEtapa.PeriodoVigencia)
            {
                //Se o tipo de prazo da etapa for igual a Prazo de vigência, então o campo data de encerramento da etapa deverá ser preenchido com a data corrente (hoje)
                //e, a situação deverá ser preenchida a com a situação "Encerrada".
                processoEtapa.DataEncerramento = DateTime.Now;
                processoEtapa.SituacaoEtapa = SituacaoEtapa.Encerrada;
                SaveEntity(processoEtapa);
            }
        }

        /// <summary>
        /// RN_SRC_076 - Rotina cancelamento Atualização do solicitante/ingressante
        /// 1. Criar um registro no histórico de situação do Ingressante com o valor "Desistente";
        /// 2. Alterar a situação do aluno no financeiro, referente ao Ingressante em questão, quando ele existir, para "Calouro desistente"
        /// 3. Se o solicitante for um Ingressante que possui oferta de inscrição com a situação atual igual a CONVOCADO
        ///     3.1. Inserir novo histórico na oferta de inscrição a situação "CONVOCADO_DESISTENTE" com o motivo "motivo parâmetro",
        ///          conforme a RN_SEL_011 - Gravação da situação da inscrição oferta na convocação
        /// </summary>
        /// <param name="processoEtapa">Dados do processo etapa</param>
        /// <param name="solicitacaoServico">Dados da solicitação da serviço</param>
        /// <param name="tokenSituacaoHistoricoGpi">Token que será usado pelo GPI para criação do histórico da situação</param>
        /// <param name="tokenMotivoSituacaoGpi">Token que será usado pelo GPI para criação do motivo da situação</param>
        public void AtualizaDadosIngressante(long seqProcessoEtapa, long seqSolicitacaoServico, string tokenSituacaoHistoricoGpi, string tokenMotivoSituacaoGpi, string justificativa)
        {
            var processoEtapa = SearchByKey(new SMCSeqSpecification<ProcessoEtapa>(seqProcessoEtapa), a => a.Processo.CicloLetivo);

            var solicitacaoServico = SolicitacaoServicoDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), a => a.PessoaAtuacao);

            AtualizaDadosIngressante(processoEtapa, solicitacaoServico, tokenSituacaoHistoricoGpi, tokenMotivoSituacaoGpi, justificativa);
        }

        /// <summary>
        /// RN_SRC_076 - Rotina cancelamento Atualização do solicitante/ingressante
        /// 1. Criar um registro no histórico de situação do Ingressante com o valor "Desistente";
        /// 2. Alterar a situação do aluno no financeiro, referente ao Ingressante em questão, quando ele existir, para "Calouro desistente"
        /// 3. Se o solicitante for um Ingressante que possui oferta de inscrição com a situação atual igual a CONVOCADO
        ///     3.1. Inserir novo histórico na oferta de inscrição a situação "CONVOCADO_DESISTENTE" com o motivo "motivo parâmetro",
        ///          conforme a RN_SEL_011 - Gravação da situação da inscrição oferta na convocação
        /// </summary>
        /// <param name="processoEtapa">Dados do processo etapa</param>
        /// <param name="solicitacaoServico">Dados da solicitação da serviço</param>
        /// <param name="tokenSituacaoHistoricoGpi">Token que será usado pelo GPI para criação do histórico da situação</param>
        /// <param name="tokenMotivoSituacaoGpi">Token que será usado pelo GPI para criação do motivo da situação</param>
        public void AtualizaDadosIngressante(ProcessoEtapa processoEtapa, SolicitacaoServico solicitacaoServico, string tokenSituacaoHistoricoGpi, string tokenMotivoSituacaoGpi, string justificativa)
        {
            if (solicitacaoServico.PessoaAtuacao.TipoAtuacao == TipoAtuacao.Ingressante)
            {
                //Criar um registro no histórico de situação do Ingressante com o valor "Desistente"
                IngressanteHistoricoSituacaoDomainService.SaveEntity(new IngressanteHistoricoSituacao()
                {
                    SeqIngressante = solicitacaoServico.SeqPessoaAtuacao,
                    SituacaoIngressante = SituacaoIngressante.Desistente
                });

                //Alterar a situação do aluno no financeiro, referente ao Ingressante em questão, quando ele existir, para "Calouro desistente"
                if (processoEtapa.Token != TOKENS_ETAPA_SGF.SOLICITACAO_MATRICULA)
                {
                    PessoaAtuacaoDadosOrigemVO dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(solicitacaoServico.SeqPessoaAtuacao);
                    if (dadosOrigem != null)
                    {
                        string erroGRA = IntegracaoFinanceiroService.AlterarSituacaoMatriculaAcademico(new AlterarMatriculaAcademicoData()
                        {
                            SeqPessoaAtuacao = solicitacaoServico.SeqPessoaAtuacao,
                            SeqOrigem = (int)dadosOrigem.SeqOrigem,
                            CodigoServicoOrigem = dadosOrigem.CodigoServicoOrigem,
                            AnoCicloLetivo = processoEtapa.Processo.CicloLetivo.Ano,
                            NumeroCicloLetivo = processoEtapa.Processo.CicloLetivo.Numero,
                            CodigoTipoTransacao = 47
                        });
                    }
                }

                //Se o solicitante for um Ingressante que possui oferta de inscrição com a situação atual igual a CONVOCADO:
                //  Inserir novo histórico na oferta de inscrição com a situação igual a "TOKEN_SITUACAO_INFORMADO",
                //  conforme a RN_SEL_011 - Gravação da situação da inscrição oferta na convocação
                var ingressante = IngressanteDomainService.SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(solicitacaoServico.SeqPessoaAtuacao),
                p => new
                {
                    SeqInscricaoGpi = (long?)p.Convocado.SeqInscricaoGpi,
                    SeqsInscricaoOfertaGpi = p.Ofertas.Select(a => a.SeqInscricaoOfertaGpi).ToList(),
                });

                if (ingressante.SeqInscricaoGpi.HasValue)
                {
                    // Busca os dados do processo do GPI
                    var dadosProcesso = InscricaoService.BuscarDadosProcessoInscricao(ingressante.SeqInscricaoGpi.Value);

                    // Para cada inscrição oferta informada, altera a situação
                    foreach (var seqInscricaoOfertaGpi in ingressante.SeqsInscricaoOfertaGpi)
                    {
                        if (seqInscricaoOfertaGpi.HasValue)
                        {
                            var situacao = IntegracaoService.BuscarHistoricosSituacaoAtual(seqInscricaoOfertaGpi.Value);
                            if (situacao.TokenSituacao == SGFConstants.CONVOCADO)
                            {
                                var seqSituacao = SituacaoService.BuscarSeqSituacaoPorToken(tokenSituacaoHistoricoGpi, dadosProcesso.SeqTipoTemplateProcessoSGF);
                                if (seqSituacao.HasValue)
                                {
                                    var seqMotivo = SituacaoService.BuscarSeqMotivoSituacaoPorToken(seqSituacao.Value, tokenMotivoSituacaoGpi);

                                    // Atualiza o histórico da oferta de inscrição
                                    var historico = new AlterarHistoricoSituacaoData
                                    {
                                        Justificativa = justificativa,
                                        SeqInscricoesOferta = new List<long> { seqInscricaoOfertaGpi.Value },
                                        SeqMotivoSGF = seqMotivo,
                                        SeqProcesso = dadosProcesso.SeqProcesso,
                                        TokenSituacaoDestino = tokenSituacaoHistoricoGpi
                                    };
                                    InscricaoOfertaHistoricoSituacaoService.AlterarHistoricoSituacao(historico);

                                    //AlteracaoSituacaoData AlteracaoSituacao = new AlteracaoSituacaoData
                                    //{
                                    //    SeqSituacaoDestino = seqSituacao.Value,
                                    //    SeqMotivoSGF = seqMotivo,
                                    //    SeqInscricoes = new List<long> { seqInscricao }
                                    //};
                                    //InscricaoService.AlterarSituacaoInscricoes(AlteracaoSituacao);
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool ServicoEtapaAtualizarSolicitacao(ProcessoEtapa processoEtapa)
        {
            //Verifica se a etapa em questão é referente ao serviço SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU e etapa CHANCELA_PLANO_ESTUD
            //ou, serviço RENOVACAO_MATRICULA e etapa CHANCELA_PLANO_ESTUDO
            EtapaData etapaSGF = EtapaService.BuscarEtapa(processoEtapa.SeqEtapaSgf);
            string tokenServico = processoEtapa.Processo.Servico.Token;
            /*return (etapaSGF.Token.ToUpper().Equals(TOKENS_ETAPA_SGF.CHANCELA_PLANO_ESTUDO) &&
					(tokenServico.Equals(TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU) || tokenServico.Equals(TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU)));*/

            // Task 30813
            return (tokenServico.Equals(TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU) && etapaSGF.Token.ToUpper().Equals(TOKENS_ETAPA_SGF.CHANCELA_PLANO_ESTUDO)) ||
                    (tokenServico.Equals(TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU) && etapaSGF.Token.ToUpper().Equals(TOKENS_ETAPA_SGF.CHANCELA_RENOVACAO_MATRICULA));
        }

        private ProcessoEtapa BuscarProcessoEtapaPosterior(long seqProcessoEtapaAtual)
        {
            var processoEtapaAtual = SearchByKey(new SMCSeqSpecification<ProcessoEtapa>(seqProcessoEtapaAtual));
            ProcessoEtapaFilterSpecification spec = new ProcessoEtapaFilterSpecification() { SeqProcesso = processoEtapaAtual.SeqProcesso, Ordem = processoEtapaAtual.Ordem + 1 };
            return SearchBySpecification(spec, a => a.Escalonamentos[0].GruposEscalonamento).FirstOrDefault();
        }

        private ProcessoEtapa BuscarProcessoEtapaAnterior(long seqProcessoEtapaAtual)
        {
            var processoEtapaAtual = SearchByKey(new SMCSeqSpecification<ProcessoEtapa>(seqProcessoEtapaAtual));
            ProcessoEtapaFilterSpecification spec = new ProcessoEtapaFilterSpecification() { SeqProcesso = processoEtapaAtual.SeqProcesso, Ordem = processoEtapaAtual.Ordem - 1 };
            return SearchBySpecification(spec, a => a.Escalonamentos[0].GruposEscalonamento).FirstOrDefault();
        }

        /// <summary>
        /// Buscar etapas posteriores a etapa atual
        /// </summary>
        /// <param name="seqProcessoEtapaAtual">Sequencial da etapa atual</param>
        /// <returns>Lista das etapas posteriores</returns>
        public List<ProcessoEtapa> BuscarProcessoEtapasPosteriores(long seqProcessoEtapaAtual)
        {
            var processoEtapaAtual = SearchByKey(new SMCSeqSpecification<ProcessoEtapa>(seqProcessoEtapaAtual));
            ProcessoEtapaFilterSpecification spec = new ProcessoEtapaFilterSpecification() { SeqProcesso = processoEtapaAtual.SeqProcesso };
            var listaEtapasPosteriores = this.SearchBySpecification(spec, a => a.Escalonamentos[0].GruposEscalonamento).Where(w => w.Ordem > processoEtapaAtual.Ordem).ToList();
            return listaEtapasPosteriores;
        }

        public bool ValidarDataEscalonamentoFinalProcesso(long seqProcessoEtapa)
        {
            var processoEtapa = BuscarProcessoEtapa(seqProcessoEtapa);

            if (!processoEtapa.CentralAtendimento)
                return false;

            if (processoEtapa.Escalonamentos == null || !processoEtapa.Escalonamentos.Any(a => DateTime.Now >= a.DataInicio && DateTime.Now <= processoEtapa.Processo.DataFim))
                return false;

            var seqAplicacaoSAS = AplicacaoService.BuscarAplicacaoPelaSigla(SMCContext.ApplicationId).Sigla;
            if (seqAplicacaoSAS != SIGLA_APLICACAO.SGA_ADMINISTRATIVO)
                return false;

            return true;
            ////var etapasSGF = SGFHelper.BuscarEtapasSGFCache(seqTemplateProcessoSgf);
            ////var etapaAtualSGF = etapasSGF.FirstOrDefault(e => e.Seq == seqEtapaSgf);

            //var dataEscalonamentoVigente = processoEtapa.Escalonamentos.Any(a => DateTime.Now >= a.DataInicio && DateTime.Now <= processoEtapa.Processo.DataFim);

            //var validarFinalProcesso = etapaAtualSGF.SeqAplicacaoSAS == seqAplicacaoSAS && dataEscalonamentoVigente;

            //return validarFinalProcesso;
        }
    }
}