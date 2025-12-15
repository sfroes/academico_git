using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Resources;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.Domain.Helpers;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Formularios.ServiceContract.Areas.TMP;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Formularios.ServiceContract.TMP.Data;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.Security.Util;
using SMC.Framework.Util;
using SMC.Seguranca.ServiceContract.Areas.APL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class ViewCentralSolicitacaoServicoDomainService : AcademicoContextDomain<ViewCentralSolicitacaoServico>
    {
        #region Services

        public IAplicacaoService AplicacaoService { get { return this.Create<IAplicacaoService>(); } }
        public IEtapaService EtapaService { get { return this.Create<IEtapaService>(); } }

        #endregion Services

        #region DomainServices

        public EntidadeDomainService EntidadeDomainService { get { return this.Create<EntidadeDomainService>(); } }

        public PessoaAtuacaoBloqueioDomainService PessoaAtuacaoBloqueioDomainService { get { return this.Create<PessoaAtuacaoBloqueioDomainService>(); } }

        public ConfiguracaoEtapaDomainService ConfiguracaoEtapaDomainService { get { return this.Create<ConfiguracaoEtapaDomainService>(); } }
        #endregion DomainServices

        public SMCPagerData<SolicitacaoServicoListarVO> BuscarSolicitacoesServicoLista(ViewCentralSolicitacaoServicoFilterSpecification filter)
        {
            if (filter.DisponivelParaAtendimento.HasValue)
            {
                var situacaoEtapaFiltroData = new SituacaoEtapaFiltroData();

                if (filter.DisponivelParaAtendimento.HasValue)
                {
                    if (filter.DisponivelParaAtendimento.Value)
                        situacaoEtapaFiltroData.SituacaoFinalProcesso = false;
                    else
                        situacaoEtapaFiltroData.SituacaoFinalProcesso = true;
                }

                filter.SeqsSituacoesEtapasSGF = this.EtapaService.BuscarSeqsSituacoesEtapa(situacaoEtapaFiltroData);
            }

            if (filter.SeqsEntidadesResponsaveis.SMCIsNullOrEmpty())
                filter.SeqsEntidadesResponsaveis = new List<long>();


            // Busca os dados da view considerando os filtros
            var dados = this.SearchBySpecification(filter, out int total).ToList();

            List<SolicitacaoServicoListarVO> dadosRet = null;
            if (dados != null)
            {
                Dictionary<long, EtapaSimplificadaData[]> dadosSGF = new Dictionary<long, EtapaSimplificadaData[]>();

                // Recupera todos os dados do SGF para as solicitações listadas
                foreach (var seqTemplateSGF in dados.Select(x => x.SeqTemplateProcessoSGF).Distinct())
                {
                    var etapa = SGFHelper.BuscarEtapasSGFCache(seqTemplateSGF);
                    dadosSGF.Add(seqTemplateSGF, etapa);
                }

                // Cria a lista de retorno
                dadosRet = new List<SolicitacaoServicoListarVO>();
                foreach (var item in dados)
                {
                    var itemParsed = new SolicitacaoServicoListarVO
                    {
                        Seq = item.SeqSolicitacaoServico,
                        SeqProcesso = item.SeqProcesso,
                        SeqSolicitacaoServicoEtapa = item.SeqSolicitacaoServicoEtapa,
                        SeqProcessoEtapa = item.SeqProcessoEtapa,
                        SeqSolicitacaoMatricula = item.SeqSolicitacaoServico,
                        SeqConfiguracaoEtapa = item.SeqConfiguracaoEtapa,
                        SeqIngressante = item.SeqPessoaAtuacao,
                        Nome = item.NomePessoaSolicitante,
                        NomeSocial = item.NomeSocialSolicitante,
                        CodigoAdesao = item.CodigoAdesao,
                        EtapaAtualIndisponivelCentralAtendimento = !item.CentralAtendimento,
                        SolicitacaoPossuiUsuarioResponsavel = item.SeqUsuarioResponsavelSAS.HasValue,
                        UsuarioNaoPossuiAcessoARealizarAtendimento = !SMCSecurityHelper.Authorize(item.TokenAcessoAtendimento),
                        UsuarioLogadoEResponsavelAtualPelaSolicitacao = item.SeqUsuarioResponsavelSAS == SMCContext.User.SMCGetSequencialUsuario(),
                        SolicitacaoServicoEDeMatricula = item.SolicitacaoMatricula.GetValueOrDefault(),
                        SituacaoDocumentacao = item.SituacaoDocumentacao,
                        NumeroProtocolo = item.NumeroProtocolo,
                        Processo = item.DescricaoProcesso,
                        GrupoEscalonamentoAtual = item.DescricaoGrupoEscalonamento,
                        Solicitante = item.NomeSolicitanteFormatado,
                        DataInclusao = item.DataInclusaoSolicitacao,
                        DataSolicitacao = item.DataSolicitacao,
                        DataPrevistaSolucao = item.DataPrevistaSolucaoSolicitacao,
                        EtapaAtual = $"{item.OrdemEtapa}° Etapa",
                        SituacaodDocumentacaoNaoRequerida = item.SituacaoDocumentacao == SituacaoDocumentacao.NaoRequerida,
                        InstructionsChancela = string.Empty,
                        InstructionsEfetivacaoMatricula = string.Empty,
                        DataInicioProcesso = item.DataInicioProcesso,
                        DescricaoTipoVinculoAluno = item.DescricaoTipoVinculoAluno,
                        TokenEntregaDocumentacao = item.TokenServico == TOKEN_SERVICO.ENTREGA_DOCUMENTACAO
                        || item.TokenServico == TOKEN_SERVICO.ATUALIZACAO_DOCUMENTACAO_EMISSAO_DIPLOMA
                    };

                    // Buscar os dados do sgf desta solicitação
                    var etapaAtualSGF = dadosSGF[item.SeqTemplateProcessoSGF].FirstOrDefault(e => e.Seq == item.SeqEtapaSGF);
                    var situacaoAtualSGF = etapaAtualSGF.Situacoes.FirstOrDefault(s => s.Seq == item.SeqSituacaoEtapaSGF);                    

                    if (itemParsed.SeqConfiguracaoEtapa.HasValue)
                    {
                        var specConfigEtapa = new ConfiguracaoEtapaFilterSpecification() { Seq = itemParsed.SeqConfiguracaoEtapa };
                        var configEtapa = ConfiguracaoEtapaDomainService.SearchProjectionByKey(specConfigEtapa, x => new { 
                            x.Seq,
                            x.SeqConfiguracaoProcesso,
                            x.Descricao,
                            x.ConfiguracoesPagina
                        });

                        bool configEtapaPossuiPaginaUploadDocumento = configEtapa.ConfiguracoesPagina.SMCAny(c => c.ConfiguracaoDocumento == ConfiguracaoDocumento.UploadDocumento);
                        itemParsed.ConfigEtapaPossuiPaginaUploadDocumento = configEtapaPossuiPaginaUploadDocumento;
                    }

                    itemParsed.SituacaoAtual = situacaoAtualSGF != null ? $"{SMCEnumHelper.GetDescription(situacaoAtualSGF.CategoriaSituacao)} - {situacaoAtualSGF.Descricao}" : MessagesResource.MSG_NaoExisteSituacaoEtapaSGF;

                    itemParsed.DescricaoLookupSolicitacao = $"{item.NumeroProtocolo} - {itemParsed.Solicitante} - Grupo Atual: {itemParsed.GrupoEscalonamentoAtual}";

                    //Se a situação atual da solicitação for finalizada com sucesso, deverá ser considerado que não há bloqueios. 
                    //Senão, se há associado pelo menos 1(um) bloqueio vigente ao solicitante(pessoa-atuação), 
                    //conforme a[parametrização de bloqueios]* do processo x etapa atual da solicitação e, com a situação igual a "Bloqueado", considerar que há bloqueios.
                    if (situacaoAtualSGF.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                    {
                        itemParsed.PossuiBloqueio = PossuiBloqueio.Nao;
                        itemParsed.SolicitantePossuiBloqueiosVigentes = false;
                    }
                    else
                    {
                        var bloqueiosQueNaoImpedemFim = PessoaAtuacaoBloqueioDomainService.BuscarPessoaAtuacaoBloqueios(item.SeqPessoaAtuacao, item.SeqConfiguracaoEtapa, false);

                        itemParsed.PossuiBloqueio = item.BloqueioFimEtapa.GetValueOrDefault() || bloqueiosQueNaoImpedemFim.Count > 0 ? PossuiBloqueio.Sim : PossuiBloqueio.Nao;
                        itemParsed.SolicitantePossuiBloqueiosVigentes = item.BloqueioFimEtapa.GetValueOrDefault();
                    }

                    /*A situação atual da solicitação possuir categoria igual a "Encerrado" é o mesmo que a situação atual da 
                    solicitação for situação final processo*/
                    itemParsed.SituacaoAtualSolicitacaoEFimProcesso = situacaoAtualSGF.SituacaoFinalProcesso;

                    // Adiciona no retorno
                    dadosRet.Add(itemParsed);
                }
            }

            // Seleciona
            return new SMCPagerData<SolicitacaoServicoListarVO>(dadosRet, total);
        }
    }
}