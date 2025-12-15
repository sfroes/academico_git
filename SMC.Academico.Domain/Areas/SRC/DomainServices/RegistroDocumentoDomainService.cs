using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.Domain.DomainServices;
using SMC.Academico.Domain.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.DadosMestres.ServiceContract.Areas.GED.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security.Util;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Seguranca.ServiceContract.Areas.APL.Interfaces;
using SMC.Academico.Common.Constants;
using SMC.Framework.Domain;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using iTextSharp.text;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class RegistroDocumentoDomainService : AcademicoContextDomain
    {
        #region DomainServices

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService
        {
            get { return this.Create<SolicitacaoServicoDomainService>(); }
        }

        private SolicitacaoDocumentoRequeridoDomainService SolicitacaoDocumentoRequeridoDomainService
        {
            get { return this.Create<SolicitacaoDocumentoRequeridoDomainService>(); }
        }

        private DocumentoRequeridoDomainService DocumentoRequeridoDomainService
        {
            get { return this.Create<DocumentoRequeridoDomainService>(); }
        }

        private AlunoDomainService AlunoDomainService
        {
            get { return this.Create<AlunoDomainService>(); }
        }

        private ConfiguracaoEtapaDomainService ConfiguracaoEtapaDomainService
        {
            get { return this.Create<ConfiguracaoEtapaDomainService>(); }
        }

        private GrupoDocumentoRequeridoItemDomainService GrupoDocumentoRequeridoItemDomainService
        {
            get { return this.Create<GrupoDocumentoRequeridoItemDomainService>(); }
        }

        private PessoaAtuacaoBloqueioDomainService PessoaAtuacaoBloqueioDomainService
        {
            get { return this.Create<PessoaAtuacaoBloqueioDomainService>(); }
        }

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService
        {
            get { return this.Create<PessoaAtuacaoDomainService>(); }
        }

        private MotivoBloqueioDomainService MotivoBloqueioDomainService
        {
            get { return this.Create<MotivoBloqueioDomainService>(); }
        }

        private ArquivoAnexadoDomainService ArquivoAnexadoDomainService
        {
            get { return this.Create<ArquivoAnexadoDomainService>(); }
        }

        private SolicitacaoServicoEnvioNotificacaoDomainService SolicitacaoServicoEnvioNotificacaoDomainService => Create<SolicitacaoServicoEnvioNotificacaoDomainService>();

        private PessoaAtuacaoDocumentoDomainService PessoaAtuacaoDocumentoDomainService => Create<PessoaAtuacaoDocumentoDomainService>();
        private SolicitacaoHistoricoSituacaoDomainService SolicitacaoHistoricoSituacaoDomainService => Create<SolicitacaoHistoricoSituacaoDomainService>();
        private ProcessoEtapaConfiguracaoNotificacaoDomainService ProcessoEtapaConfiguracaoNotificacaoDomainService => Create<ProcessoEtapaConfiguracaoNotificacaoDomainService>();
        private MensagemDomainService MensagemDomainService => Create<MensagemDomainService>();
        private ProcessoEtapaDomainService ProcessoEtapaDomainService => Create<ProcessoEtapaDomainService>();
        private SolicitacaoServicoEtapaDomainService SolicitacaoServicoEtapaDomainService => Create<SolicitacaoServicoEtapaDomainService>();
        private ConfiguracaoProcessoDomainService ConfiguracaoProcessoDomainService => Create<ConfiguracaoProcessoDomainService>();
        private ProcessoDomainService ProcessoDomainService => Create<ProcessoDomainService>();
        private ServicoDomainService ServicoDomainService => Create<ServicoDomainService>();


        #endregion DomainServices

        #region Services

        private ITipoDocumentoService TipoDocumentoService
        {
            get { return this.Create<ITipoDocumentoService>(); }
        }

        private IAplicacaoService AplicacaoService { get => Create<IAplicacaoService>(); }

        private IEtapaService EtapaService
        {
            get { return Create<IEtapaService>(); }
        }

        #endregion Services

        public RegistroDocumentoCabecalhoVO BuscarCabecalhoRegistroDocumentos(long seqSolicitacaoServico)
        {
            return this.SolicitacaoServicoDomainService.SearchProjectionByKey(
                new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico),
                x => new RegistroDocumentoCabecalhoVO()
                {
                    Nome = x.PessoaAtuacao.DadosPessoais.Nome,
                    NomeSocial = x.PessoaAtuacao.DadosPessoais.NomeSocial,
                    Cpf = x.PessoaAtuacao.Pessoa.Cpf,
                    NumeroPassaporte = x.PessoaAtuacao.Pessoa.NumeroPassaporte,
                    DescricaoProcesso = x.GrupoEscalonamento.Processo.Descricao
                });
        }

        public List<DocumentoVO> BuscarDocumentosRegistro(long seqSolicitacaoServico, long? seqConfiguracaoEtapa = null, bool? permiteUploadArquivo = null, bool exibirDocumentoPermiteUpload = true, bool exibirDocumentoNaoPermiteUpload = false, bool? atendimento = null)
        {
            var tiposDocumentos = TipoDocumentoService.BuscarTiposDocumentos();

            var specSolicitacaoServico = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);

            var solicitacaoServico = this.SolicitacaoServicoDomainService.SearchProjectionByKey(specSolicitacaoServico, s => new
            {
                SeqsConfiguracoesEtapas = s.ConfiguracaoProcesso.ConfiguracoesEtapa.Select(a => a.Seq),
                SexoPessoaAtuacao = s.PessoaAtuacao.DadosPessoais.Sexo,
                SeqPessoaAtuacao = s.SeqPessoaAtuacao,
                TokenServico = s.ConfiguracaoProcesso.Processo.Servico.Token,
                DocumentosEnviados = s.DocumentosRequeridos.Select(d => new DocumentoItemVO
                {
                    Seq = d.Seq,
                    SeqSolicitacaoServico = d.SeqSolicitacaoServico,
                    SeqDocumentoRequerido = d.SeqDocumentoRequerido,
                    SeqTipoDocumento = d.DocumentoRequerido.SeqTipoDocumento,
                    SeqArquivoAnexado = d.SeqArquivoAnexado,
                    ArquivoAnexado = d.SeqArquivoAnexado.HasValue ? new SMCUploadFile
                    {
                        FileData = d.ArquivoAnexado.Conteudo,
                        GuidFile = d.ArquivoAnexado.UidArquivo.ToString(),
                        Name = d.ArquivoAnexado.Nome,
                        Size = d.ArquivoAnexado.Tamanho,
                        Type = d.ArquivoAnexado.Tipo
                    } : null,
                    DataEntrega = d.DataEntrega,
                    FormaEntregaDocumento = d.FormaEntregaDocumento,
                    Observacao = d.Observacao,
                    ObservacaoSecretaria = d.ObservacaoSecretaria,
                    DescricaoInconformidade = d.DescricaoInconformidade,
                    VersaoDocumento = d.VersaoDocumento,
                    SituacaoEntregaDocumento = d.SituacaoEntregaDocumento,
                    SituacaoEntregaDocumentoInicial = d.SituacaoEntregaDocumento,
                    VersaoExigida = d.DocumentoRequerido.VersaoDocumento,
                    EntregaPosterior = d.EntregaPosterior,
                    DataPrazoEntrega = d.DataPrazoEntrega,
                    EntregueAnteriormente = d.EntregueAnteriormente,
                }).ToList()
            });


            // Filtra as configurações etapa.
            var configuracoesEtapas = (seqConfiguracaoEtapa.HasValue) ?
                                        solicitacaoServico.SeqsConfiguracoesEtapas.Where(f => f == seqConfiguracaoEtapa.Value).ToArray() :
                                        solicitacaoServico.SeqsConfiguracoesEtapas.ToArray();

            var specConfiguracoesEtapa = new SMCContainsSpecification<DocumentoRequerido, long>(c => c.SeqConfiguracaoEtapa, configuracoesEtapas);
            var specFiltroDocumentoRequerido = new DocumentoRequeridoFilterSpecification { Sexo = solicitacaoServico.SexoPessoaAtuacao };
            var specDocumento = new SMCAndSpecification<DocumentoRequerido>(specConfiguracoesEtapa, specFiltroDocumentoRequerido);

            var documentosRequeridos = this.DocumentoRequeridoDomainService.SearchProjectionBySpecification(
                specDocumento,
                d => new DocumentoVO()
                {
                    SeqDocumentoRequerido = d.Seq,
                    SeqTipoDocumento = d.SeqTipoDocumento,
                    SeqConfiguracaoEtapa = d.SeqConfiguracaoEtapa,
                    PermiteVarios = d.PermiteVarios,
                    Obrigatorio = d.Obrigatorio,
                    PermiteUploadArquivo = d.PermiteUploadArquivo,
                    ObrigatorioUpload = d.ObrigatorioUpload,
                    PermiteEntregaPosterior = d.PermiteEntregaPosterior,
                    Sexo = d.Sexo ?? Sexo.Nenhum,
                    ValidacaoOutroSetor = d.ValidacaoOutroSetor,
                    DataLimiteEntrega = d.DataLimiteEntrega,
                    EntregueAnteriormente = d.SolicitacoesDocumentoRequerido.Where(c => c.SeqSolicitacaoServico == seqSolicitacaoServico).FirstOrDefault() != null
                                                                                      ? d.SolicitacoesDocumentoRequerido.Where(c => c.SeqSolicitacaoServico == seqSolicitacaoServico).FirstOrDefault().EntregueAnteriormente
                                                                                      : false
                }).ToList();

            var specGrupoDocumentoRequerido = new SMCContainsSpecification<GrupoDocumentoRequeridoItem, long>(c => c.GrupoDocumentoRequerido.SeqConfiguracaoEtapa, configuracoesEtapas);

            var gruposDocumentosRequeridos = this.GrupoDocumentoRequeridoItemDomainService.SearchProjectionBySpecification(specGrupoDocumentoRequerido,
                d => new
                {
                    SeqGrupoDocumentoRequerido = d.SeqGrupoDocumentoRequerido,
                    SeqDocumentoRequerido = d.SeqDocumentoRequerido,
                    DescricaoGrupoDocumentoRequerido = d.GrupoDocumentoRequerido.Descricao,
                    NumeroMinimoDocumentosRequerido = d.GrupoDocumentoRequerido.MinimoObrigatorio
                });

            List<DocumentoVO> documentosSeremExibidos = new List<DocumentoVO>();
            List<DocumentoVO> retornoCasoAtendimento = new List<DocumentoVO>();

            if (exibirDocumentoPermiteUpload)
                documentosSeremExibidos.AddRange(documentosRequeridos.Where(w => w.PermiteUploadArquivo).ToList());

            if (exibirDocumentoNaoPermiteUpload)
                documentosSeremExibidos.AddRange(documentosRequeridos.Where(w => !w.PermiteUploadArquivo).ToList());

            documentosRequeridos = documentosSeremExibidos;

            documentosRequeridos.ForEach(d =>
            {
                d.Grupos = gruposDocumentosRequeridos.Where(e => e.SeqDocumentoRequerido == d.SeqDocumentoRequerido).Select(g => new GrupoDocumentoVO
                {
                    Seq = g.SeqGrupoDocumentoRequerido,
                    Descricao = g.DescricaoGrupoDocumentoRequerido,
                    NumeroMinimoDocumentosRequerido = g.NumeroMinimoDocumentosRequerido
                }).ToList();

                d.Documentos = solicitacaoServico.DocumentosEnviados.Where(e => e.SeqDocumentoRequerido == d.SeqDocumentoRequerido || (e.SeqTipoDocumento == d.SeqTipoDocumento && e.EntregueAnteriormente)).ToList() ?? new List<DocumentoItemVO> { new DocumentoItemVO() };
                d.DescricaoTipoDocumento = tiposDocumentos.FirstOrDefault(t => t.Seq == d.SeqTipoDocumento)?.Descricao;

                if (d.PermiteVarios && (d.Documentos == null || !d.Documentos.Any()))
                    d.Documentos.Add(new DocumentoItemVO());

                if (atendimento.HasValue && atendimento.Value)
                {
                    var documentosAguardandoValidacao = solicitacaoServico.DocumentosEnviados.Where(e => e.SeqDocumentoRequerido == d.SeqDocumentoRequerido && e.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoValidacao).ToList() ?? new List<DocumentoItemVO> { new DocumentoItemVO() };
                    if (documentosAguardandoValidacao.Any())
                    {
                        d.Documentos = documentosAguardandoValidacao;
                        retornoCasoAtendimento.Add(d);
                    }
                }

                d.SolicitacoesEntregaDocumento = BuscarSituacoesEntregaDocumento(d);
            });

            if (atendimento.HasValue && atendimento.Value && solicitacaoServico.TokenServico != TOKEN_SERVICO.ATUALIZACAO_DOCUMENTACAO_EMISSAO_DIPLOMA)
                return retornoCasoAtendimento.OrderBy(d => d.DescricaoTipoDocumento).ToList();

            return documentosRequeridos.OrderBy(d => d.DescricaoTipoDocumento).ToList();
        }

        public long SalvarRegistroDocumentos(RegistroDocumentoVO modelo)
        {
            using (var unityOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    // Instanciando caso Documentos esteja null para evitar erros
                    if (modelo.Documentos == null)
                    {
                        modelo.Documentos = new List<DocumentoVO>();
                    }

                    bool enviarNoificacao = false;

                    // Coloca o seq do documento requerido nos documentos com seq zerado
                    modelo?.Documentos?.ForEach(d =>
                    {
                        d?.Documentos?.ForEach(doc =>
                        {
                            if (doc.SeqDocumentoRequerido == 0)
                                doc.SeqDocumentoRequerido = d.SeqDocumentoRequerido;
                        });
                    });

                    ValidarRegistroDocumentos(modelo);

                    enviarNoificacao = ValidarEnvioNotificacao(modelo);

                    InserirBloqueiosRegistroDocumento(modelo.SeqPessoaAtuacao, modelo.SeqSolicitacaoServico, modelo.Documentos.SelectMany(dr => dr.Documentos).ToList());

                    InserirDesbloqueiosRegistroDocumento(modelo.SeqPessoaAtuacao, modelo.SeqSolicitacaoServico, modelo.Documentos.SelectMany(dr => dr.Documentos).ToList());

                    var solicitacaoServico = this.SolicitacaoServicoDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoServico>(modelo.SeqSolicitacaoServico));

                    solicitacaoServico.DocumentosRequeridos = modelo.Documentos.SelectMany(dr => dr.Documentos).Select(d => d.Transform<SolicitacaoDocumentoRequerido>()).ToList();

                    solicitacaoServico.DocumentosRequeridos.SMCForEach(d =>
                    {
                        EnsureFileIntegrity(d, x => x.SeqArquivoAnexado, x => x.ArquivoAnexado);
                    });

                    this.SolicitacaoServicoDomainService.SaveEntity(solicitacaoServico);

                    if (enviarNoificacao)
                    {
                        var parametros = EnviarNotificacaoSolicitacao(modelo.SeqSolicitacaoServico);

                        if (parametros.SeqSolicitacaoServico != 0)
                            SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);
                    }

                    unityOfWork.Commit();

                    return solicitacaoServico.Seq;
                }
                catch (Exception)
                {
                    unityOfWork.Rollback();
                    throw;
                }
            }
        }

        private bool ValidarEnvioNotificacao(RegistroDocumentoVO modelo)
        {
            bool retorno = false;

            var documentosPendentesOuIndeferidosModelo = modelo.Documentos.SelectMany(sm => sm.Documentos)
                                                                            .Where(w => w.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Indeferido ||
                                                                                                w.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente).ToList();

            var documentosInDB = BuscarDocumentosRegistro(modelo.SeqSolicitacaoServico, exibirDocumentoNaoPermiteUpload: true)
                                                                    .SelectMany(sm => sm.Documentos);

            var documentosAValidar = documentosInDB.Where(w => documentosPendentesOuIndeferidosModelo.Select(s => s.Seq).Contains(w.Seq)).ToList();

            foreach (var documento in documentosAValidar)
            {
                var documentoModelo = documentosPendentesOuIndeferidosModelo.Where(w => w.Seq == documento.Seq).FirstOrDefault();
                if (documento.SituacaoEntregaDocumento != documentoModelo.SituacaoEntregaDocumento)
                {
                    retorno = true;
                }
            }

            return retorno;
        }

        private EnvioNotificacaoSolicitacaoServicoVO EnviarNotificacaoSolicitacao(long seqSolicitacaoServico)
        {

            long seqProecessoEtapa = this.SolicitacaoServicoDomainService.SearchProjectionByKey(seqSolicitacaoServico, p => p.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.SeqProcessoEtapa);
            // Busca os dados da solicitação

            EnvioNotificacaoSolicitacaoServicoVO parametros = new EnvioNotificacaoSolicitacaoServicoVO();
            var dadosSolicitacao = this.SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
            {
                DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao,
                Token = x.ConfiguracaoProcesso.Processo.Servico.Token,
                NumeroProtocolo = x.NumeroProtocolo,
                NomeSocialSolicitante = x.PessoaAtuacao.DadosPessoais.NomeSocial,
                NomeSolicitante = x.PessoaAtuacao.DadosPessoais.Nome,
                x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(f => f.SeqProcessoEtapa == seqProecessoEtapa).ProcessoEtapa.ConfiguracoesNotificacao.FirstOrDefault(f => f.SeqProcessoEtapa == seqProecessoEtapa).EnvioAutomatico
            });

            //Enviar notificação somente quando a solicitação for de um serviço que possui um dos tokens
            //SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA ou SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU e 
            //Uma notificação só poderá ser enviada se o  parâmetro "Envio de notificação automático?" for igual a SIM na configuração
            //da notificação para etapa processo em questão
            if ((dadosSolicitacao.Token == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU ||
                 dadosSolicitacao.Token == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA ||
                 dadosSolicitacao.Token == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU ||
                 dadosSolicitacao.Token == TOKEN_SERVICO.MATRICULA_REABERTURA) && dadosSolicitacao.EnvioAutomatico)
            {
                // Monta os dados para merge de envio de notificações
                Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
                dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, string.IsNullOrEmpty(dadosSolicitacao.NomeSocialSolicitante) ? dadosSolicitacao.NomeSolicitante : string.Format("{0} ({1})", dadosSolicitacao.NomeSocialSolicitante, dadosSolicitacao.NomeSolicitante));
                dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.DSC_PROCESSO, dadosSolicitacao.DescricaoProcesso);
                dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.NUM_PROTOCOLO, dadosSolicitacao.NumeroProtocolo);

                // Envia a notificação de CRIAÇÃO DA SOLICITAÇÃO NO PORTAL
                parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                {
                    SeqSolicitacaoServico = seqSolicitacaoServico,
                    TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.DOCUMENTO_INDEFERIDO_PENDENTE,
                    DadosMerge = dadosMerge,
                    EnvioSolicitante = true,
                    ConfiguracaoPrimeiraEtapa = false
                };

            }

            return parametros;
        }

        public void InserirBloqueiosRegistroDocumento(long seqPessoaAtuacao, long seqSolicitacaoServico, List<DocumentoItemVO> documentosRequeridos)
        {
            //UC_SRC_004_02_01 - NV13

            //Recupera os documentos com situacao pendente
            var documentosPendentes = documentosRequeridos
                                            .Where(w => w.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente)
                                            .ToList();

            //Caso existe documentos com situação pendente
            if (documentosPendentes.Count > 0)
            {
                //Recupera o seq do motivo de bloqueio pelo token
                var seqMotivoBloqueio = this.MotivoBloqueioDomainService.BuscarSeqMotivoBloqueioPorToken(TOKEN_MOTIVO_BLOQUEIO.ENTREGA_DOCUMENTACAO);

                //Caso não exista motivo de bloqueio cadastrado para gravar os bloqueios, dispara mensagem mde erro.
                if (seqMotivoBloqueio == null || seqMotivoBloqueio == 0)
                    throw new RegistroDocumentoMotivoBloqueioNaoCadastradoException();

                //Cria o spec para recuperar os bloqueios da pessoa atuação
                var spec = new PessoaAtuacaoBloqueioFilterSpecification()
                {
                    ///SeqSolicitacaoServico = seqSolicitacaoServico,
                    SeqPessoaAtuacao = seqPessoaAtuacao,
                    SeqMotivoBloqueio = new List<long>() { seqMotivoBloqueio },
                };

                //Recupera os bloqueios da pessoa atuação
                var pessoaAtuacaoBloqueios = this.PessoaAtuacaoBloqueioDomainService.SearchBySpecification(spec, IncludesPessoaAtuacaoBloqueio.Itens).ToList();

                //Apos recuperar os dados da pessoa atuação, descobre qual é o ingressante do histórico mais antigo do aluno referente a pessoa atuação
                //e unifica os dados, caso existam bloqueios do o ingressante encontrado

                //Cria o spec do aluno, usando o seq da pessoa atuação
                var specAluno = new SMCSeqSpecification<Aluno>(seqPessoaAtuacao);

                //Recupera o seq do ingressante do historico mais antigo do aluno, da pessao atuação
                var seqIngressante = this.AlunoDomainService.SearchProjectionByKey(specAluno, p => p.Historicos.OrderBy(h => h.DataInclusao).FirstOrDefault().SeqIngressante);

                //Se encontrado ingressante
                if (seqIngressante.HasValue)
                {
                    //Atualiza o spec passando o ingressante encontrado
                    spec = new PessoaAtuacaoBloqueioFilterSpecification()
                    {
                        //SeqSolicitacaoServico = seqSolicitacaoServico,
                        SeqPessoaAtuacao = seqIngressante.Value,
                        SeqMotivoBloqueio = new List<long>() { seqMotivoBloqueio },
                    };

                    //Recupera os bloqueios do ingressante
                    var ingressanteBloqueios = this.PessoaAtuacaoBloqueioDomainService.SearchBySpecification(spec, IncludesPessoaAtuacaoBloqueio.Itens);

                    //Caso encontre algum registro, adiciona os mesmos ao resultado
                    if (ingressanteBloqueios.Any())
                        pessoaAtuacaoBloqueios.AddRange(ingressanteBloqueios);
                }

                //Para cada documento pendente (do modelo)
                foreach (var documentoPendente in documentosPendentes)
                {
                    //Verificar se pelo menos um bloqueio associado possui o tipo de documento em questão como item do bloqueio
                    var bloqueiosDoDocumento = pessoaAtuacaoBloqueios.Where(i => i.Itens.Any(ii => ii.CodigoIntegracaoSistemaOrigem == documentoPendente.SeqTipoDocumento.ToString())).ToList();

                    // Regra 1
                    var documentosBloqueadosAtual = bloqueiosDoDocumento.Where(i =>
                                (i.SituacaoBloqueio == SituacaoBloqueio.Bloqueado)).ToList();

                    // Regra 2
                    var documentosDesbloqueadoEfetivo = bloqueiosDoDocumento.Where(i =>
                                (i.SituacaoBloqueio == SituacaoBloqueio.Desbloqueado)).ToList();

                    // Criar o bloqueio para o caso de so existir bloqueio desbloqueado efetivamente ou não existir bloqueio
                    if ((documentosBloqueadosAtual == null || !documentosBloqueadosAtual.Any()) ||
                        (documentosDesbloqueadoEfetivo.Any() && (documentosBloqueadosAtual == null || !documentosBloqueadosAtual.Any())))
                    {
                        //Recupera o tipo de documento pendente
                        var tipoDocumento = this.TipoDocumentoService.BuscarTipoDocumento(documentoPendente.SeqTipoDocumento);

                        //Recupera a pessoa atuação
                        var pessoaAtuacao = this.PessoaAtuacaoDomainService.SearchProjectionByKey(
                            new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao),
                            p => new
                            {
                                SeqPessoaAtuacao = p.Seq,
                                Descricao = p.Descricao
                            });

                        //Cria o bloqueio
                        var novaPessoaAtuacaoBloqueio = new PessoaAtuacaoBloqueio()
                        {
                            SeqSolicitacaoServico = seqSolicitacaoServico,
                            SeqPessoaAtuacao = pessoaAtuacao.SeqPessoaAtuacao,
                            SeqMotivoBloqueio = seqMotivoBloqueio,
                            Descricao = "Pendência na entrega da documentação.",
                            SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                            DescricaoReferenciaAtuacao = pessoaAtuacao.Descricao,
                            CadastroIntegracao = true,
                            DataBloqueio = documentoPendente.DataPrazoEntrega.Value,
                            Itens = new List<PessoaAtuacaoBloqueioItem>()
                            {
                                 new PessoaAtuacaoBloqueioItem()
                                 {
                                    Descricao = tipoDocumento.Descricao,
                                    SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                                    CodigoIntegracaoSistemaOrigem = documentoPendente.SeqTipoDocumento.ToString()
                                 }
                            },
                        };

                        //Salva o bloqueio
                        this.PessoaAtuacaoBloqueioDomainService.SaveEntity(novaPessoaAtuacaoBloqueio);
                    }
                    else
                    {
                        //recupera o bloqueio atual
                        var documentoBloqueadosAtual = documentosBloqueadosAtual.FirstOrDefault();

                        //Se a data foi alterada pelo usuário, realiza a atualização
                        if (documentoPendente.DataPrazoEntrega.HasValue)
                        {
                            if (documentoBloqueadosAtual != null && (documentoBloqueadosAtual.DataBloqueio != documentoPendente.DataPrazoEntrega.Value))
                            {
                                documentoBloqueadosAtual.DataBloqueio = documentoPendente.DataPrazoEntrega.Value;

                                //Salva o bloqueio atual
                                this.PessoaAtuacaoBloqueioDomainService.UpdateFields(documentoBloqueadosAtual, x => x.DataBloqueio);
                            }
                        }
                    }
                }
            }
        }

        public void InserirDesbloqueiosRegistroDocumento(long seqPessoaAtuacao, long seqSolicitacaoServico, List<DocumentoItemVO> documentosRequeridos)
        {
            //Recupera os documentos com situacao pendente
            var documentosDesbloqueio = documentosRequeridos
                                        .Where(w => w.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido || w.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel)
                                        .ToList();

            //Recupera o seq do motivo de bloqueio pelo token
            var seqMotivoBloqueio = this.MotivoBloqueioDomainService.BuscarSeqMotivoBloqueioPorToken(TOKEN_MOTIVO_BLOQUEIO.ENTREGA_DOCUMENTACAO);

            //Cria o spec para recuperar os bloqueios da pessoa atuação
            var spec = new PessoaAtuacaoBloqueioFilterSpecification()
            {
                //SeqSolicitacaoServico = seqSolicitacaoServico,
                SeqPessoaAtuacao = seqPessoaAtuacao,
                SeqMotivoBloqueio = new List<long>() { seqMotivoBloqueio },
            };

            //Recupera os bloqueios da pessoa atuação
            var pessoaAtuacaoBloqueios = this.PessoaAtuacaoBloqueioDomainService.SearchBySpecification(spec, IncludesPessoaAtuacaoBloqueio.Itens).ToList();

            //Apos recuperar os dados da pessoa atuação, descobre qual é o ingressante do histórico mais antigo do aluno referente a pessoa atuação
            //e unifica os dados, caso existam bloqueios do o ingressante encontrado

            //Cria o spec do aluno, usando o seq da pessoa atuação
            var specAluno = new SMCSeqSpecification<Aluno>(seqPessoaAtuacao);

            //Recupera o seq do ingressante do historico mais antigo do aluno, da pessao atuação
            var seqIngressante = this.AlunoDomainService.SearchProjectionByKey(specAluno, p => p.Historicos.OrderBy(h => h.DataInclusao).FirstOrDefault().SeqIngressante);

            //Se encontrado ingressante
            if (seqIngressante.HasValue)
            {
                //Atualiza o spec passando o ingressante encontrado
                spec = new PessoaAtuacaoBloqueioFilterSpecification()
                {
                    //SeqSolicitacaoServico = seqSolicitacaoServico,
                    SeqPessoaAtuacao = seqIngressante.Value,
                    SeqMotivoBloqueio = new List<long>() { seqMotivoBloqueio },
                };

                //Recupera os bloqueios do ingressante
                var ingressanteBloqueios = this.PessoaAtuacaoBloqueioDomainService.SearchBySpecification(spec, IncludesPessoaAtuacaoBloqueio.Itens);

                //Caso encontre algum registro, adiciona os mesmos ao resultado
                if (ingressanteBloqueios.Any())
                    pessoaAtuacaoBloqueios.AddRange(ingressanteBloqueios);
            }

            foreach (var documentoDesbloqueio in documentosDesbloqueio)
            {
                //Verificar se pelo menos um bloqueio associado possui o tipo de documento em questão como item do bloqueio
                var bloqueiosDoDocumento = pessoaAtuacaoBloqueios.Where(i => i.Itens.Any(ii => ii.CodigoIntegracaoSistemaOrigem == documentoDesbloqueio.SeqTipoDocumento.ToString())).ToList();

                // Regra 1
                var documentosBloqueadosAtual = bloqueiosDoDocumento.Where(i =>
                            (i.SituacaoBloqueio == SituacaoBloqueio.Bloqueado)).ToList();

                //Para cada bloqueio
                foreach (var documentoBloqueado in documentosBloqueadosAtual)
                {
                    //Para cada item de bloqueio, proceder com o desbloqueio
                    foreach (var itemBloqueio in documentoBloqueado.Itens)
                    {
                        itemBloqueio.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                        itemBloqueio.UsuarioDesbloqueio = SMCContext.User.Identity.Name;
                        itemBloqueio.DataDesbloqueio = DateTime.Now;
                    }
                    //Caso todos os itens de um bloqueio estejam desbloqueados, realizar o desbloqueio
                    if (documentoBloqueado.Itens.All(i => i.SituacaoBloqueio == SituacaoBloqueio.Desbloqueado))
                    {
                        documentoBloqueado.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                        documentoBloqueado.TipoDesbloqueio = TipoDesbloqueio.Efetivo;
                        documentoBloqueado.UsuarioDesbloqueioEfetivo = SMCContext.User.Identity.Name;
                        documentoBloqueado.DataDesbloqueioEfetivo = DateTime.Now;
                        documentoBloqueado.JustificativaDesbloqueio = "Entrega de documentação.";
                    }

                    //Salvar o desbloqueio
                    this.PessoaAtuacaoBloqueioDomainService.SaveEntity(documentoBloqueado);
                }
            }
        }

        private void ValidarRegistroDocumentos(RegistroDocumentoVO modelo)
        {
            ValidarSituacaoDocumentos(modelo);
            ValidarDataPrazoEntrega(modelo);
            ValidarAnexoObrigatorio(modelo);
            ValidarItensNaoInformados(modelo);
            ValidarExtensaoArquivo(modelo);
            ValidarDocumentoObrigatorio(modelo);
        }

        /// <summary>
        /// Ao salvar:
        /// Caso algum tipo de documento esteja, ao mesmo tempo, com a situação "Pendente" e "Deferido" 
        /// ou "Pendente" e "Aguardando análise do setor responsável", abortar a operação e emitir a mensagem de erro.
        /// </summary>
        private void ValidarSituacaoDocumentos(RegistroDocumentoVO modelo)
        {
            List<string> documentosDescricao = new List<string>();

            var documentosInDB = BuscarDocumentosRegistro(modelo.SeqSolicitacaoServico, exibirDocumentoNaoPermiteUpload: true)
                                                    .SelectMany(sm => sm.Documentos).ToList();

            foreach (var doc in modelo.Documentos)
            {
                if (doc.Documentos.Any(c => c.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente))
                {
                    if (doc.Documentos.Any(d => d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido ||
                                                d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel))
                    {
                        documentosDescricao.Add(doc.DescricaoTipoDocumento.Trim());
                    }
                }

                var documentosDiferentesDePendente = doc.Documentos.Where(c => c.SituacaoEntregaDocumento != SituacaoEntregaDocumento.Pendente).ToList();

                var documentosPendente = doc.Documentos.Where(c => c.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente).ToList();

                foreach (var docDifetente in documentosDiferentesDePendente)
                {
                    var docReferencia = documentosInDB.FirstOrDefault(f => f.Seq == docDifetente.Seq);
                    if (docReferencia?.SituacaoEntregaDocumento != docDifetente.SituacaoEntregaDocumento)
                    {
                        docDifetente.EntregaPosterior = false;
                    }
                    else
                    {
                        docDifetente.EntregaPosterior = docReferencia.EntregaPosterior;
                    }
                }

                foreach (var docpendente in documentosPendente)
                {
                    var docReferencia = documentosInDB.FirstOrDefault(f => f.Seq == docpendente.Seq);

                    if (docReferencia != null)
                    {
                        docpendente.EntregaPosterior = docReferencia.EntregaPosterior;
                    }
                    else
                    {
                        docpendente.EntregaPosterior = false;
                    }
                }
            }

            if (documentosDescricao.Any())
            {
                string docsFormatados = "";
                documentosDescricao.Sort();

                documentosDescricao.ForEach(d =>
                {
                    docsFormatados += $"- {d} <br/>";
                });

                throw new RegistroDocumentoSituacaoPendenteDeferidoAguardandoException(docsFormatados);
            }
        }

        private void ValidarAnexoObrigatorio(RegistroDocumentoVO modelo)
        {
            //Racupera todos os documentos cuja forma de entrega seja Upload ou E-mail e que não tenha arquivo anexado
            var qtdDocumentosInvalidos = modelo.Documentos
                                               .SelectMany(d => d.Documentos)
                                               .Where(doc => (doc.FormaEntregaDocumento == FormaEntregaDocumento.Upload ||
                                                              doc.FormaEntregaDocumento == FormaEntregaDocumento.Email) &&
                                                              doc.ArquivoAnexado == null).Count();

            //Caso encontre algum documento nas condições acima, exibir mensagem de erro
            if (qtdDocumentosInvalidos > 0)
                throw new RegistroDocumentoArquivoAnexoObrigatorioException();
        }

        private void ValidarItensNaoInformados(RegistroDocumentoVO modelo)
        {
            //Para os tipos de documentos que permitem vários itens, validar se os itens existem
            var qtdItensNaoInformados = modelo.Documentos
                                              .Count(d => d.PermiteVarios && (d.Documentos == null || !d.Documentos.Any()));

            //Caso encontre algum item faltante, exibir mensagem de erro
            if (qtdItensNaoInformados > 0)
                throw new RegistroDocumentoItemObrigatorioException();
        }

        private void ValidarDataPrazoEntrega(RegistroDocumentoVO modelo)
        {
            var seqsSolicitacaoDocumentoRequerido = modelo.Documentos.SelectMany(d => d.Documentos).Select(doc => doc.Seq).ToArray();

            var specSolicitacaoDocumentoRequerido = new SMCContainsSpecification<SolicitacaoDocumentoRequerido, long>(c => c.Seq, seqsSolicitacaoDocumentoRequerido);

            var solicitacoesDocumentosRequeridos = this.SolicitacaoDocumentoRequeridoDomainService.SearchBySpecification(specSolicitacaoDocumentoRequerido);

            foreach (var documento in modelo.Documentos.SelectMany(d => d.Documentos).Select(doc => doc))
            {
                if (documento.DataPrazoEntrega.HasValue)
                {
                    var solicitacaoDocumentoRequerido = solicitacoesDocumentosRequeridos.FirstOrDefault(d => d.Seq == documento.Seq);

                    if (solicitacaoDocumentoRequerido != null)
                    {
                        if (solicitacaoDocumentoRequerido.DataPrazoEntrega != documento.DataPrazoEntrega)
                        {
                            if (documento.DataPrazoEntrega < DateTime.Now.Date)
                            {
                                throw new RegistroDocumentoDataPrazoEntregaInvalidaException();
                            }
                        }
                    }
                    else
                    {
                        if (documento.DataPrazoEntrega < DateTime.Now.Date)
                        {
                            throw new RegistroDocumentoDataPrazoEntregaInvalidaException();
                        }
                    }
                }
            }

            //Caso existam datas de prazo de entregas diferentes para o mesmo documento, abortar a operação e exibir a seguinte mensagem de erro:
            var documentosComMaisDeUmArquivo = modelo.Documentos.Where(w => w.Documentos.Count() > 1).ToList();
            foreach (var item in documentosComMaisDeUmArquivo)
            {
                DocumentoItemVO primeiroDocumento = item.Documentos.FirstOrDefault();

                bool todosTemAMesData = item.Documentos.All(al => al.DataPrazoEntrega == primeiroDocumento.DataPrazoEntrega);

                if (!todosTemAMesData)
                {
                    throw new RegistroDocumentoDataPrazoEntregaDiferentesException();
                }
            }
        }

        private void ValidarExtensaoArquivo(RegistroDocumentoVO modelo)
        {
            //RN_PES_011 - Consistência arquivos

            //Aceitar apenas arquivos com estas extensões: doc, docx, xls, xlsx, jpg, jpeg, png, pdf, rar, zip, ps
            var extensoesPermitidas = new string[] { ".doc", ".docx", ".xls", ".xlsx", ".jpg", ".jpeg", ".png", ".pdf", ".rar", ".zip", ".ps", ".xml" };

            foreach (var nomeArquivo in modelo.Documentos.SelectMany(d => d.Documentos).Where(doc => doc.ArquivoAnexado != null).Select(s => s.ArquivoAnexado.Name))
            {
                var extensaoArquivo = new FileInfo(nomeArquivo).Extension.ToLower();

                if (!extensoesPermitidas.Contains(extensaoArquivo))
                    throw new RegistroDocumentoExtensaoArquivoNaoPermitidaException(nomeArquivo);
            }
        }

        public List<DocumentoVO> ValidarDocumentoObrigatorio(long seqSolicitacaoServico)
        {
            var dadosSolicitacao = new RegistroDocumentoVO
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
            };

            dadosSolicitacao.Documentos = BuscarDocumentosRegistro(seqSolicitacaoServico);

            ValidarDocumentoObrigatorio(dadosSolicitacao);

            return dadosSolicitacao.Documentos;
        }

        private List<(bool Pendente, SituacaoDocumentacao Situacao)> SituacoesDocumentos(IEnumerable<DocumentoVO> documentos)
        {
            List<(bool Pendente, SituacaoDocumentacao Situacao)> situacoesDocumentos = new List<(bool Pendente, SituacaoDocumentacao Situacao)>();

            documentos.ToList().ForEach(d =>
            {
                /* Alterar a regra do comando “Salvar” do UC_SRC_004_02_01 - Registrar Documentos:

                    * Verificar se todos os documentos obrigatórios estão em uma das situações: "Deferido" ou "Aguardando análise do setor responsável".
                        Se estiverem, salvar a situação da documentação da solicitação com o valor "Entregue";

                    * Se não estiver, verificar se estão com as situações "Deferido", "Aguardando análise do setor responsável" e "Pendente"
                        Se estiverem, salvar a situação da documentação da solicitação com valor "Entregue com pendência"

                    * Se não estiverem, verificar se pelo menos um está com a situação "Aguardando Validação"
                        Se estiver, salvar a situação da documentação da solicitação com valor "Aguardando validação"

                    * Se não estiverem, verificar se pelo menos um está com a situação "Aguardando entrega" ou "Indeferido";
                        Se estiver, salvar a situação da documentação da solicitação com valor "Aguardando (nova) entrega".

                    * Caso não exista lista de documentos requeridos para a solicitação, salvar a situação da documentação com o valor "Não requerida".
                */

                var pendente = false;
                var situacao = SituacaoDocumentacao.Nenhum;

                if (d.Documentos?.All(dd => dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido ||
                                            dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel) ?? false)
                {
                    pendente = false;
                    situacao = SituacaoDocumentacao.Entregue;
                }
                else if (d.Documentos?.All(dd => dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido ||
                                                 dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel ||
                                                 dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente) ?? false)
                {
                    pendente = false;
                    situacao = SituacaoDocumentacao.EntregueComPendencia;
                }
                else if (d.Documentos?.Any(dd => dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoValidacao) ?? false)
                {
                    pendente = true;
                    situacao = SituacaoDocumentacao.AguardandoValidacao;
                }
                else if (d.Documentos?.Any(dd => dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega ||
                                                 dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Indeferido) ?? false)
                {
                    pendente = true;
                    situacao = SituacaoDocumentacao.AguardandoEntrega;
                }
                else
                {
                    pendente = false;
                    situacao = SituacaoDocumentacao.NaoRequerida;
                }

                situacoesDocumentos.Add((pendente, situacao));
            });

            return situacoesDocumentos;
        }

        public List<(bool Pendente, SituacaoDocumentacao Situacao)> ValidarSituacoesDocumentosObrigatorios(IEnumerable<DocumentoVO> documentos)
        {
            // Recupera as situações dos documentos obrigatórios que não possuem grupos
            var situacoesObrigatoriosSemGrupo = SituacoesDocumentos(documentos);

            return situacoesObrigatoriosSemGrupo;
        }

        private void ValidarDocumentoObrigatorio(RegistroDocumentoVO modelo)
        {
            List<DocumentoVO> documentosRequeridos = BuscarDadosDocumentosRequeridos(modelo);

            var situacaoGrupos = ValidarSituacoesGruposDocumentos(documentosRequeridos);

            // Depois de verificar os grupos remover os docs deles e os documentos que não são obrigatórios para não serem verificados novamente
            var situacoesDocumentos = ValidarSituacoesDocumentosObrigatorios(documentosRequeridos.SMCRemove(c => c.Obrigatorio == false));

            SituacaoDocumentacao situacaoDocumentacao = ConfirmarSituacaoDocumento(situacoesDocumentos, situacaoGrupos);

            ////Recupera a solicitação do servico
            var solicitacaoServico = this.SolicitacaoServicoDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoServico>(modelo.SeqSolicitacaoServico));

            solicitacaoServico.SituacaoDocumentacao = situacaoDocumentacao;

            //Salva a solicitaçção de servido com as situações alteradas
            this.SolicitacaoServicoDomainService.SaveEntity(solicitacaoServico);
        }

        /// <summary>
        /// Parte de Grupos de Documentação da regra
        /// RN_SRC_003 - Registro Documento - Situação documentação solicitação serviço
        /// Regra atualizada na task 48817
        /// /// </summary>        
        public static List<(bool Pendente, SituacaoDocumentacao Situacao)> ValidarSituacoesGruposDocumentos(List<DocumentoVO> documentosRequeridos)
        {
            var situacaoGrupos = new List<(bool Pendente, SituacaoDocumentacao Situacao)>();
            var docsComGrupos = documentosRequeridos.Where(c => c.Grupos.SMCAny()).ToList();
            var grupos = docsComGrupos.SelectMany(c => c.Grupos).Distinct().ToList();

            foreach (var grupo in grupos)
            {
                var documentosGrupo = documentosRequeridos.Where(c => c.Grupos.SMCAny(d => d.Seq == grupo.Seq)).ToList();

                if (documentosGrupo.SMCAny())
                {
                    var situacoesValidadas = documentosGrupo.Where(c => c.Documentos.All(d =>
                                                                        d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido ||
                                                                        d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel)).ToList();

                    // 1 - Verificar se os grupos de documentos obrigatórios estão com o número mínimo de documentos em uma das situações: "Deferido" ou "Aguardando análise do setor responsável".
                    if (situacoesValidadas.Count >= grupo.NumeroMinimoDocumentosRequerido)
                    {
                        // 1.1 - Se estiverem, salvar a situação da documentação da solicitação com o valor "Entregue"
                        situacaoGrupos.Add((false, SituacaoDocumentacao.Entregue));
                    }
                    else
                    {
                        var docsDeferidoAnalisePendente = documentosGrupo.Where(c => c.Documentos.All(d =>
                                                                                     d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido ||
                                                                                     d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel ||
                                                                                     d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente)).ToList();

                        // 1.2 - Se não estiver, verificar se estão com as situações "Deferido", "Aguardando análise do setor responsável" e "Pendente"
                        if (docsDeferidoAnalisePendente.SMCAny())
                        {
                            // 1.2.1 - Se estiverem, salvar a situação da documentação da solicitação com valor "Entregue com pendência"
                            situacaoGrupos.Add((true, SituacaoDocumentacao.EntregueComPendencia));
                        }
                        else
                        {
                            // 1.2.2 - Se não estiverem, verificar se pelo menos um está com a situação "Aguardando Validação"
                            var situacoesAguardandoValidacao = documentosGrupo.Where(c => c.Documentos.All(d => d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoValidacao)).ToList();

                            if (situacoesAguardandoValidacao.SMCAny())
                            {
                                // 1.2.2.1 - Se estiver, salvar a situação da documentação da solicitação com valor "Aguardando validação"
                                situacaoGrupos.Add((false, SituacaoDocumentacao.AguardandoValidacao));
                            }
                            else
                            {
                                var situacoesAguardandoEntregaIndeferido = documentosGrupo.Where(c => c.Documentos.All(d => d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega ||
                                                                                                                            d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Indeferido)).ToList();

                                // 1.2.2.2 - Se não estiverem, verificar se pelo menos um está com a situação "Aguardando entrega" ou "Indeferido";
                                if (situacoesAguardandoEntregaIndeferido.SMCAny())
                                {
                                    // 1.2.2.2.1 - Se estiver, salvar a situação da documentação da solicitação com valor "Aguardando (nova) entrega".
                                    situacaoGrupos.Add((true, SituacaoDocumentacao.AguardandoEntrega));
                                }
                            }
                        }
                    }
                }
                else
                {
                    // 1.2.2.2.2 - Caso não exista lista de documentos requeridos para a solicitação, salvar a situação da documentação com o valor "Não requerida".
                    situacaoGrupos.Add((false, SituacaoDocumentacao.NaoRequerida));
                }
            }

            return situacaoGrupos;
        }

        /// <summary>
        /// Retorna a Situação do Documento baseado em documentos e Grupo de documentos que já estão validados separadamente.        
        /// </summary>
        public SituacaoDocumentacao ConfirmarSituacaoDocumento(List<(bool Pendente, SituacaoDocumentacao Situacao)> SituacaoDocumentos, List<(bool Pendente, SituacaoDocumentacao Situacao)> SituacaoGrupos)
        {
            /** Regra atualizada (48817):
             1 - Verificar se todos os documentos obrigatórios e se os grupos de documentos obrigatórios estão com o número mínimo de documentos em uma das situações: "Deferido" ou "Aguardando análise do setor responsável".
             1.1 - Se estiverem, salvar a situação da documentação da solicitação com o valor "Entregue";
             1.2 - Se não estiver, verificar se estão com as situações "Deferido", "Aguardando análise do setor responsável" e "Pendente"
             1.2.1 -Se estiverem, salvar a situação da documentação da solicitação com valor "Entregue com pendência"
             1.2.2 - Se não estiverem, verificar se pelo menos um está com a situação "Aguardando Validação"
             1.2.2.1 - Se estiver, salvar a situação da documentação da solicitação com valor "Aguardando validação"
             1.2.2.2 - Se não estiverem, verificar se pelo menos um está com a situação "Aguardando entrega" ou "Indeferido";
             1.2.2.2.1 - Se estiver, salvar a situação da documentação da solicitação com valor "Aguardando (nova) entrega".
             1.2.2.2.2 - Caso não exista lista de documentos requeridos para a solicitação, salvar a situação da documentação com o valor "Não requerida".
            **/

            // Se não existir uma lista de documentos e uma lista de grupos de documentos, então é uma solicitação não requerida
            bool existemDocumentosParaValidar = SituacaoDocumentos.Count > 0 || SituacaoGrupos.Count > 0;

            if (existemDocumentosParaValidar)
            {
                // Se Todos os documentos Obrigatórios e todos os grupos foram validados individualmente como Entregue
                if (SituacaoDocumentos.All(c => c.Situacao == SituacaoDocumentacao.Entregue) && SituacaoGrupos.All(c => c.Situacao == SituacaoDocumentacao.Entregue))
                {
                    return SituacaoDocumentacao.Entregue;
                }
                // Se Todos os documentos Obrigatórios e todos os grupos foram validados individualmente como Entregue com pendência, tirando os que já estão marcados como entregue
                else if (SituacaoDocumentos.Where(c => c.Situacao != SituacaoDocumentacao.Entregue).All(c => c.Situacao == SituacaoDocumentacao.EntregueComPendencia)
                          && SituacaoGrupos.Where(c => c.Situacao != SituacaoDocumentacao.Entregue).All(c => c.Situacao == SituacaoDocumentacao.EntregueComPendencia))
                {
                    return SituacaoDocumentacao.EntregueComPendencia;
                }
                // Se pelo menos um documento ou grupo de documentos estiver com a situação Aguardando validação
                else if (SituacaoDocumentos.Any(c => c.Situacao == SituacaoDocumentacao.AguardandoValidacao) || SituacaoGrupos.Any(c => c.Situacao == SituacaoDocumentacao.AguardandoValidacao))
                {
                    return SituacaoDocumentacao.AguardandoValidacao;
                }
                // Se pelo menos um documento ou grupo de documentos estiver marcado como aguardando entrega
                else if (SituacaoDocumentos.Any(c => c.Situacao == SituacaoDocumentacao.AguardandoEntrega) || SituacaoGrupos.Any(c => c.Situacao == SituacaoDocumentacao.AguardandoEntrega))
                {
                    return SituacaoDocumentacao.AguardandoEntrega;
                }
                else
                {
                    return SituacaoDocumentacao.NaoRequerida;
                }
            }
            else
            {
                return SituacaoDocumentacao.NaoRequerida;
            }
        }

        public List<DocumentoVO> BuscarDadosDocumentosRequeridos(RegistroDocumentoVO modelo)
        {
            // Busca as configurações dos documentos requeridos com seus respectivos grupos
            var dadosSolicitacaoServico = this.SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(modelo.SeqSolicitacaoServico), s => new
            {
                SeqsConfiguracoesEtapas = s.ConfiguracaoProcesso.ConfiguracoesEtapa.Select(a => a.Seq),
                SexoPessoaAtuacao = s.PessoaAtuacao.DadosPessoais.Sexo,
                s.DocumentosRequeridos
            });

            // Filtra as configurações etapa.
            var configuracoesEtapas = (modelo.SeqConfiguracaoEtapa.HasValue) ?
                                        dadosSolicitacaoServico.SeqsConfiguracoesEtapas.Where(f => f == modelo.SeqConfiguracaoEtapa.Value).ToArray() :
                                        dadosSolicitacaoServico.SeqsConfiguracoesEtapas.ToArray();

            var specConfiguracoesEtapa = new SMCContainsSpecification<DocumentoRequerido, long>(c => c.SeqConfiguracaoEtapa, configuracoesEtapas);
            var specFiltroDocumentoRequerido = new DocumentoRequeridoFilterSpecification { Sexo = dadosSolicitacaoServico.SexoPessoaAtuacao, PermiteUploadArquivo = modelo.PermiteUploadArquivo };
            var specDocumento = new SMCAndSpecification<DocumentoRequerido>(specConfiguracoesEtapa, specFiltroDocumentoRequerido);

            var documentosRequeridos = this.DocumentoRequeridoDomainService.SearchProjectionBySpecification(
                specDocumento,
                d => new DocumentoVO()
                {
                    SeqDocumentoRequerido = d.Seq,
                    SeqConfiguracaoEtapa = d.SeqConfiguracaoEtapa,
                    SeqTipoDocumento = d.SeqTipoDocumento,
                    PermiteVarios = d.PermiteVarios,
                    Obrigatorio = d.Obrigatorio,
                    ObrigatorioUpload = d.ObrigatorioUpload,
                    PermiteEntregaPosterior = d.PermiteEntregaPosterior,
                    Sexo = d.Sexo ?? Sexo.Nenhum,
                    ValidacaoOutroSetor = d.ValidacaoOutroSetor
                }).ToList();

            var specGrupoDocumentoRequerido = new SMCContainsSpecification<GrupoDocumentoRequeridoItem, long>(c => c.GrupoDocumentoRequerido.SeqConfiguracaoEtapa, configuracoesEtapas);

            var gruposDocumentosRequeridos = this.GrupoDocumentoRequeridoItemDomainService.SearchProjectionBySpecification(specGrupoDocumentoRequerido,
                d => new
                {
                    d.SeqGrupoDocumentoRequerido,
                    d.SeqDocumentoRequerido,
                    DescricaoGrupoDocumentoRequerido = d.GrupoDocumentoRequerido.Descricao,
                    NumeroMinimoDocumentosRequerido = d.GrupoDocumentoRequerido.MinimoObrigatorio
                });

            documentosRequeridos.ForEach(d =>
            {
                d.Grupos = gruposDocumentosRequeridos.Where(e => e.SeqDocumentoRequerido == d.SeqDocumentoRequerido).Select(g => new GrupoDocumentoVO
                {
                    Seq = g.SeqGrupoDocumentoRequerido,
                    Descricao = g.DescricaoGrupoDocumentoRequerido,
                    NumeroMinimoDocumentosRequerido = g.NumeroMinimoDocumentosRequerido
                }).ToList();

                var documentos = modelo.Documentos?.FirstOrDefault(dc => dc.SeqDocumentoRequerido == d.SeqDocumentoRequerido);

                if (documentos != null)
                {
                    d.Documentos = documentos.Documentos;
                    d.SolicitacoesEntregaDocumento = BuscarSituacoesEntregaDocumento(d);
                }
                else
                {
                    // Se o documento não estiver na model então procura na solicitação. Nem sempre a model vai ter todos os documentos, e não estiver na model não precisa recuperar
                    // os itens para um select
                    var documentosNaSolicitacao = dadosSolicitacaoServico.DocumentosRequeridos?.Where(dc => dc.SeqDocumentoRequerido == d.SeqDocumentoRequerido).ToList();

                    if (documentosNaSolicitacao != null)
                    {
                        var listaDocs = documentosNaSolicitacao.TransformList<DocumentoItemVO>();
                        d.Documentos = listaDocs;
                    }
                }
            });

            return documentosRequeridos;
        }

        private List<SMCDatasourceItem> BuscarSituacoesEntregaDocumento(DocumentoVO documentoRequerido)
        {
            var lista = new List<SMCDatasourceItem>();

            if (!SMCSecurityHelper.Authorize(UC_SRC_004_02_01.VALIDACAO_CRA) && !SMCSecurityHelper.Authorize(UC_SRC_004_02_01.VALIDACAO_SECRETARIA))
                return lista;

            /*  Validação CRA;
                Validação secretaria;
                Se o usuário possuir os dois tokens, exibir;
                    Aguardando análise do setor responsável;
                    Aguardando entrega;
                    Deferido;
                    Indeferido;
                Verificar se o documento pode ser entregue posteriormente, em TODAS as solicitações selecionadas na interface.
                Se sim, exibir também a situação: Pendente.
                Se não possuir um dos tokens, não exibir valor.*/
            if (SMCSecurityHelper.Authorize(UC_SRC_004_02_01.VALIDACAO_CRA) && SMCSecurityHelper.Authorize(UC_SRC_004_02_01.VALIDACAO_SECRETARIA))
            {
                lista.Add(new SMCDatasourceItem { Seq = (long)SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel, Descricao = SMCEnumHelper.GetDescription(SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel) });
                lista.Add(new SMCDatasourceItem { Seq = (long)SituacaoEntregaDocumento.AguardandoEntrega, Descricao = SMCEnumHelper.GetDescription(SituacaoEntregaDocumento.AguardandoEntrega) });
                lista.Add(new SMCDatasourceItem { Seq = (long)SituacaoEntregaDocumento.Deferido, Descricao = SMCEnumHelper.GetDescription(SituacaoEntregaDocumento.Deferido) });
                lista.Add(new SMCDatasourceItem { Seq = (long)SituacaoEntregaDocumento.Indeferido, Descricao = SMCEnumHelper.GetDescription(SituacaoEntregaDocumento.Indeferido) });
                if (documentoRequerido.PermiteEntregaPosterior)
                    lista.Add(new SMCDatasourceItem { Seq = (long)SituacaoEntregaDocumento.Pendente, Descricao = SMCEnumHelper.GetDescription(SituacaoEntregaDocumento.Pendente) });
            }

            /*  Token Validação CRA:
                Verificar se o documento requerido em questão foi parametrizado para ser validado por outro setor.
                1. Se foi, exibir as situações:
                    - Aguardando entrega
                    - Deferido;
                    - Indeferido;
                - Verificar se o documento pode ser entregue posteriormente. Se sim, exibir também a situação: Pendente.
                2. Se não foi, todos os campos deverão ser exibidos desabilitados e preenchidos com os valores atuais da situação do documento em questão.*/
            else if (SMCSecurityHelper.Authorize(UC_SRC_004_02_01.VALIDACAO_CRA))
            {
                if (documentoRequerido.ValidacaoOutroSetor)
                {
                    lista.Add(new SMCDatasourceItem { Seq = (long)SituacaoEntregaDocumento.AguardandoEntrega, Descricao = SMCEnumHelper.GetDescription(SituacaoEntregaDocumento.AguardandoEntrega) });
                    lista.Add(new SMCDatasourceItem { Seq = (long)SituacaoEntregaDocumento.Deferido, Descricao = SMCEnumHelper.GetDescription(SituacaoEntregaDocumento.Deferido) });
                    lista.Add(new SMCDatasourceItem { Seq = (long)SituacaoEntregaDocumento.Indeferido, Descricao = SMCEnumHelper.GetDescription(SituacaoEntregaDocumento.Indeferido) });
                    if (documentoRequerido.PermiteEntregaPosterior)
                        lista.Add(new SMCDatasourceItem { Seq = (long)SituacaoEntregaDocumento.Pendente, Descricao = SMCEnumHelper.GetDescription(SituacaoEntregaDocumento.Pendente) });
                }
                else
                {
                    documentoRequerido.Documentos.ForEach(d => d.BloquearTodosOsCampos = true);
                }
            }

            /*  Token Validação Secretaria:
                Verificar se o documento requerido em questão foi parametrizado para ser validado por outro setor.
                1. Se foi, verificar se a situação atual do documento é Deferido ou Indeferido
                - Se sim, exibir todos os campos referentes ao documento em questão desabilitados e preenchidos com os valores atuais da situação do documento;
                - Se não, exibir as situações:
                    - (Task 52522) Indeferido
                    - Aguardando entrega;
                    - Aguardando análise do setor responsável;
                    - Verificar se o documento pode ser entregue posteriormente. 
                               Se sim, exibir também a situação: Pendente.

                2. Se não foi, exibir as situações:
                    - Aguardando entrega;
                    - Deferido;
                    - Indeferido;
                    - Verificar se o documento pode ser entregue posteriormente. 
                                Se sim, exibir também a situação: Pendente.
            */
            else if (SMCSecurityHelper.Authorize(UC_SRC_004_02_01.VALIDACAO_SECRETARIA))
            {
                if (documentoRequerido.ValidacaoOutroSetor)
                {
                    bool exibirDeferido = false;

                    documentoRequerido.Documentos?.ForEach(d =>
                    {
                        if (d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido ||
                            d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Indeferido)
                        {
                            d.BloquearTodosOsCampos = true;
                            exibirDeferido = true;
                        }
                    });

                    if (exibirDeferido)
                    {
                        lista.Add(new SMCDatasourceItem { Seq = (long)SituacaoEntregaDocumento.Deferido, Descricao = SMCEnumHelper.GetDescription(SituacaoEntregaDocumento.Deferido) });
                        lista.Add(new SMCDatasourceItem { Seq = (long)SituacaoEntregaDocumento.Indeferido, Descricao = SMCEnumHelper.GetDescription(SituacaoEntregaDocumento.Indeferido) });
                    }
                    else
                    {
                        lista.Add(new SMCDatasourceItem { Seq = (long)SituacaoEntregaDocumento.Indeferido, Descricao = SMCEnumHelper.GetDescription(SituacaoEntregaDocumento.Indeferido) });
                        lista.Add(new SMCDatasourceItem { Seq = (long)SituacaoEntregaDocumento.AguardandoEntrega, Descricao = SMCEnumHelper.GetDescription(SituacaoEntregaDocumento.AguardandoEntrega) });
                        lista.Add(new SMCDatasourceItem { Seq = (long)SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel, Descricao = SMCEnumHelper.GetDescription(SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel) });
                        if (documentoRequerido.PermiteEntregaPosterior)
                            lista.Add(new SMCDatasourceItem { Seq = (long)SituacaoEntregaDocumento.Pendente, Descricao = SMCEnumHelper.GetDescription(SituacaoEntregaDocumento.Pendente) });
                    }
                }
                else
                {
                    lista.Add(new SMCDatasourceItem { Seq = (long)SituacaoEntregaDocumento.AguardandoEntrega, Descricao = SMCEnumHelper.GetDescription(SituacaoEntregaDocumento.AguardandoEntrega) });
                    lista.Add(new SMCDatasourceItem { Seq = (long)SituacaoEntregaDocumento.Deferido, Descricao = SMCEnumHelper.GetDescription(SituacaoEntregaDocumento.Deferido) });
                    lista.Add(new SMCDatasourceItem { Seq = (long)SituacaoEntregaDocumento.Indeferido, Descricao = SMCEnumHelper.GetDescription(SituacaoEntregaDocumento.Indeferido) });
                    if (documentoRequerido.PermiteEntregaPosterior)
                        lista.Add(new SMCDatasourceItem { Seq = (long)SituacaoEntregaDocumento.Pendente, Descricao = SMCEnumHelper.GetDescription(SituacaoEntregaDocumento.Pendente) });
                }
            }

            return lista;
        }

        public void RegistrarDocumentosSolicitacao(RegistroDocumentoVO modelo)
        {
            using (var unityOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    var solicitacaoServico = this.SolicitacaoServicoDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoServico>(modelo.SeqSolicitacaoServico),
                                                                                    IncludesSolicitacaoServico.DocumentosRequeridos);

                    var seqEtapaAtualSgf = this.SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(modelo.SeqSolicitacaoServico), s => s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf);
                    var etapaSGF = EtapaService.BuscarEtapa(seqEtapaAtualSgf);
                    var aplicacaoAluno = AplicacaoService.BuscarAplicacaoPelaSigla(SIGLA_APLICACAO.SGA_ALUNO);

                    List<long> arquivosExclusao = new List<long>();

                    // Para cada tipo de documento...
                    foreach (var documento in modelo.Documentos)
                    {
                        // Para cada arquivo
                        foreach (var arquivo in documento.Documentos)
                        {
                            // Se é um novo documento
                            if (arquivo.Seq == 0)
                            {
                                var novoDoc = new SolicitacaoDocumentoRequerido()
                                {
                                    SeqSolicitacaoServico = modelo.SeqSolicitacaoServico,
                                    SeqDocumentoRequerido = documento.SeqDocumentoRequerido,
                                    FormaEntregaDocumento = FormaEntregaDocumento.Upload,
                                    DataEntrega = DateTime.Now,
                                    Observacao = arquivo.Observacao,
                                    ObservacaoSecretaria = arquivo.ObservacaoSecretaria,
                                    ArquivoAnexado = arquivo.ArquivoAnexado.Transform<ArquivoAnexado>()
                                };

                                if (etapaSGF.SeqAplicacaoSAS == aplicacaoAluno.Seq)
                                {
                                    novoDoc.SituacaoEntregaDocumento = SituacaoEntregaDocumento.AguardandoValidacao;
                                    novoDoc.VersaoDocumento = VersaoDocumento.CopiaSimples;
                                }
                                else
                                {
                                    novoDoc.SituacaoEntregaDocumento = SituacaoEntregaDocumento.Deferido;
                                    novoDoc.VersaoDocumento = VersaoDocumento.Original;
                                }

                                EnsureFileIntegrity(novoDoc, x => x.SeqArquivoAnexado, x => x.ArquivoAnexado);
                                solicitacaoServico.DocumentosRequeridos.Add(novoDoc);
                            }
                            else
                            {
                                // Busca o documento referente na solicitação de serviço
                                var docSol = solicitacaoServico.DocumentosRequeridos.FirstOrDefault(d => d.Seq == arquivo.Seq);
                                if (docSol != null)
                                {
                                    // Atualiza o documento se entregue
                                    if (arquivo.ArquivoAnexado != null)
                                    {
                                        docSol.FormaEntregaDocumento = FormaEntregaDocumento.Upload;
                                        docSol.DataEntrega = DateTime.Now;
                                        docSol.Observacao = arquivo.Observacao;
                                        docSol.ObservacaoSecretaria = arquivo.ObservacaoSecretaria;
                                        docSol.ArquivoAnexado = arquivo.ArquivoAnexado.Transform<ArquivoAnexado>();

                                        if (etapaSGF.SeqAplicacaoSAS == aplicacaoAluno.Seq)
                                        {
                                            docSol.SituacaoEntregaDocumento = SituacaoEntregaDocumento.AguardandoValidacao;
                                            docSol.VersaoDocumento = VersaoDocumento.CopiaSimples;
                                        }
                                        else
                                        {
                                            docSol.SituacaoEntregaDocumento = SituacaoEntregaDocumento.Deferido;
                                            docSol.VersaoDocumento = VersaoDocumento.Original;
                                        }

                                        EnsureFileIntegrity(docSol, x => x.SeqArquivoAnexado, x => x.ArquivoAnexado);
                                    }
                                    else // Limpa as informações de entrega se necessário
                                    {
                                        if (docSol.DataEntrega.HasValue)
                                        {
                                            docSol.DataEntrega = null;
                                            docSol.SituacaoEntregaDocumento = SituacaoEntregaDocumento.AguardandoEntrega;
                                            docSol.FormaEntregaDocumento = null;
                                            docSol.VersaoDocumento = null;
                                            docSol.Observacao = null;
                                            if (docSol.SeqArquivoAnexado.HasValue)
                                            {
                                                arquivosExclusao.Add(docSol.SeqArquivoAnexado.Value);
                                                docSol.SeqArquivoAnexado = null;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        // Se o documento aceita mais do que um, verifica se teve algum excluído
                        if (documento.PermiteVarios)
                        {
                            solicitacaoServico.DocumentosRequeridos = solicitacaoServico.DocumentosRequeridos.Where(d => d.SeqDocumentoRequerido != documento.SeqDocumentoRequerido ||
                                                                                                                        (d.SeqDocumentoRequerido == documento.SeqDocumentoRequerido &&
                                                                                                                        documento.Documentos.Select(a => a.Seq).Contains(d.Seq))).ToList();
                        }
                    }

                    this.SolicitacaoServicoDomainService.SaveEntity(solicitacaoServico);

                    foreach (var arquivo in arquivosExclusao)
                    {
                        ArquivoAnexadoDomainService.DeleteEntity(new ArquivoAnexado() { Seq = arquivo });
                    }

                    unityOfWork.Commit();
                }
                catch (Exception)
                {
                    unityOfWork.Rollback();
                    throw;
                }
            }
        }

        public void AnexarDocumentosSolicitacao(RegistroDocumentoVO modelo, bool enviarNotificacao = false, bool ehPrimeiraEtapa = true)
        {
            using (var unityOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    var solicitacaoServico = this.SolicitacaoServicoDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoServico>(modelo.SeqSolicitacaoServico),
                                                                                              IncludesSolicitacaoServico.DocumentosRequeridos);

                    var seqEtapaAtualSgf = this.SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(modelo.SeqSolicitacaoServico),
                                                                                                      s => s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf);
                    var etapaSGF = EtapaService.BuscarEtapa(seqEtapaAtualSgf);
                    var aplicacaoAluno = AplicacaoService.BuscarAplicacaoPelaSigla(SIGLA_APLICACAO.SGA_ALUNO);
                    var arquivosExclusao = new List<long>();

                    //Buscando token servico
                    var dadosServico = this.SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(modelo.SeqSolicitacaoServico), x => new
                    {
                        TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token,
                        TokenTipoServico = x.ConfiguracaoProcesso.Processo.Servico.TipoServico.Token
                    });


                    // Para cada tipo de documento
                    foreach (var documento in modelo.Documentos)
                    {
                        // Para cada arquivo anexado ao documento
                        foreach (var arquivo in documento.Documentos)
                        {
                            // Se é um novo documento
                            if (arquivo.Seq == 0)
                            {
                                var novoDoc = new SolicitacaoDocumentoRequerido()
                                {
                                    SeqSolicitacaoServico = modelo.SeqSolicitacaoServico,
                                    SeqDocumentoRequerido = documento.SeqDocumentoRequerido,
                                    DataEntrega = arquivo.ArquivoAnexado != null ? (DateTime?)DateTime.Now : null, // O mesmo para Aluno e Administrador (1.1.4 e 2.1.4) 
                                    ArquivoAnexado = arquivo.ArquivoAnexado.Transform<ArquivoAnexado>(), // O mesmo para Aluno e Administrador (1.1.4 e 2.1.4) 
                                    DescricaoInconformidade = null // O mesmo para Aluno e Administrador (1.1.4 e 2.1.4)                                     
                                };

                                #region [ 1.1.4 - Inclusão de novo arquivo ]
                                if (SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ALUNO)
                                {
                                    if (arquivo.EntregaPosterior.HasValue && arquivo.EntregaPosterior.Value == true)
                                    {
                                        novoDoc.SituacaoEntregaDocumento = SituacaoEntregaDocumento.Pendente;
                                    }
                                    else if (arquivo.ArquivoAnexado != null)
                                    {
                                        novoDoc.SituacaoEntregaDocumento = SituacaoEntregaDocumento.AguardandoValidacao;
                                    }
                                    else if (arquivo.ArquivoAnexado == null)
                                    {
                                        novoDoc.SituacaoEntregaDocumento = SituacaoEntregaDocumento.AguardandoEntrega;
                                    }

                                    novoDoc.FormaEntregaDocumento = arquivo.ArquivoAnexado != null ? (FormaEntregaDocumento?)FormaEntregaDocumento.Upload : null;
                                    novoDoc.VersaoDocumento = arquivo.ArquivoAnexado != null ? (VersaoDocumento?)VersaoDocumento.CopiaSimples : null;
                                    novoDoc.Observacao = arquivo.Observacao;
                                    novoDoc.ObservacaoSecretaria = null;
                                    novoDoc.DataPrazoEntrega = arquivo.EntregaPosterior.HasValue && arquivo.EntregaPosterior.Value && arquivo.DataLimiteEntrega.HasValue ? arquivo.DataLimiteEntrega : null;
                                    novoDoc.EntregaPosterior = arquivo.EntregaPosterior;
                                }
                                #endregion
                                #region [ 2.1.4 - Inclusão de novo arquivo ]
                                else
                                {
                                    novoDoc.SituacaoEntregaDocumento = SituacaoEntregaDocumento.Deferido;
                                    novoDoc.FormaEntregaDocumento = FormaEntregaDocumento.Upload;
                                    novoDoc.VersaoDocumento = VersaoDocumento.CopiaSimples;
                                    novoDoc.Observacao = arquivo.Observacao;
                                    novoDoc.ObservacaoSecretaria = arquivo.ObservacaoSecretaria;
                                    novoDoc.DataPrazoEntrega = null;
                                    novoDoc.EntregaPosterior = false;
                                }
                                #endregion

                                EnsureFileIntegrity(novoDoc, x => x.SeqArquivoAnexado, x => x.ArquivoAnexado);
                                solicitacaoServico.DocumentosRequeridos.Add(novoDoc);
                            }
                            else
                            {
                                //Na modal de upload de documentos, acessada a partir da Home, ao anexar somente um documento 
                                //indeferido e clicar em salvar, o sistema limpa a forma de entrega, a versão, o prazo e a data da entrega 
                                //dos demais documentos que não foram entregues.Os campos dos registros dos documentos que não tiverem arquivos 
                                //postados não devem ser alterados.Nenhum campo. Segunda Etapa
                                if (!ehPrimeiraEtapa && arquivo.ArquivoAnexado == null)
                                {
                                    continue;
                                }

                                // Busca o documento referente na solicitação de serviço
                                var docSol = solicitacaoServico.DocumentosRequeridos.FirstOrDefault(d => d.Seq == arquivo.Seq);

                                if (docSol != null)
                                {
                                    // Atualiza o documento se entregue
                                    if (arquivo.ArquivoAnexado != null && arquivo.ArquivoAnexado.State == SMCUploadFileState.Changed)
                                    {
                                        var arquivoAnexado = new ArquivoAnexado();

                                        arquivoAnexado = arquivo.ArquivoAnexado.Transform<ArquivoAnexado>();

                                        ArquivoAnexadoDomainService.SaveEntity(arquivoAnexado);

                                        docSol.DataEntrega = DateTime.Now.Date;
                                        //docSol.ArquivoAnexado = arquivo.ArquivoAnexado.Transform<ArquivoAnexado>();
                                        docSol.DescricaoInconformidade = null;

                                        #region [ 1.1.4 - Edição de arquivo ]
                                        if (SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ALUNO)
                                        {
                                            // Ao salvar no portal aluno, caso o documento já esteja deferido esta parte está marcando ele como aguardando validação novamente
                                            if (docSol.SituacaoEntregaDocumento != SituacaoEntregaDocumento.Deferido && docSol.SituacaoEntregaDocumento != SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel)
                                            {
                                                if (arquivo.EntregaPosterior.HasValue && arquivo.EntregaPosterior.Value == true)
                                                {
                                                    docSol.SituacaoEntregaDocumento = SituacaoEntregaDocumento.Pendente;
                                                }
                                                else if (arquivo.ArquivoAnexado != null)
                                                {
                                                    docSol.SituacaoEntregaDocumento = SituacaoEntregaDocumento.AguardandoValidacao;
                                                }
                                                else if (arquivo.ArquivoAnexado == null && !arquivo.TemAnexoAnterior)
                                                {
                                                    docSol.SituacaoEntregaDocumento = SituacaoEntregaDocumento.AguardandoEntrega;
                                                }
                                            }


                                            if (dadosServico.TokenTipoServico == TOKEN_TIPO_SERVICO.ENTREGA_DOCUMENTACAO)
                                            {
                                                if (arquivo.EntregaPosterior.HasValue && arquivo.EntregaPosterior.Value == true)
                                                {
                                                    docSol.SituacaoEntregaDocumento = SituacaoEntregaDocumento.Pendente;
                                                }
                                                else if (arquivo.ArquivoAnexado != null)
                                                {
                                                    docSol.SituacaoEntregaDocumento = SituacaoEntregaDocumento.AguardandoValidacao;
                                                }
                                                else if (arquivo.ArquivoAnexado == null && !arquivo.TemAnexoAnterior)
                                                {
                                                    docSol.SituacaoEntregaDocumento = SituacaoEntregaDocumento.AguardandoEntrega;
                                                }
                                            }

                                            docSol.FormaEntregaDocumento = FormaEntregaDocumento.Upload;
                                            docSol.VersaoDocumento = VersaoDocumento.CopiaSimples;
                                            docSol.Observacao = arquivo.Observacao;
                                            docSol.ObservacaoSecretaria = null;
                                            docSol.DataPrazoEntrega = arquivo.EntregaPosterior.HasValue && arquivo.EntregaPosterior.Value && arquivo.DataLimiteEntrega.HasValue ? arquivo.DataLimiteEntrega : null;
                                            docSol.EntregaPosterior = false;
                                        }
                                        #endregion
                                        #region [ 2.1.4 - Edição de arquivo ]
                                        else
                                        {
                                            docSol.SituacaoEntregaDocumento = SituacaoEntregaDocumento.Deferido;
                                            docSol.FormaEntregaDocumento = FormaEntregaDocumento.Upload;
                                            docSol.VersaoDocumento = VersaoDocumento.CopiaSimples;
                                            docSol.Observacao = arquivo.Observacao;
                                            docSol.ObservacaoSecretaria = arquivo.ObservacaoSecretaria;
                                            docSol.DataPrazoEntrega = arquivo.EntregaPosterior.HasValue && arquivo.EntregaPosterior.Value && arquivo.DataLimiteEntrega.HasValue ? arquivo.DataLimiteEntrega : null;
                                            docSol.EntregaPosterior = false;
                                        }
                                        #endregion

                                        EnsureFileIntegrity(docSol, x => x.SeqArquivoAnexado, x => x.ArquivoAnexado);

                                        docSol.SeqArquivoAnexado = arquivoAnexado.Seq;

                                        this.SolicitacaoDocumentoRequeridoDomainService.SaveEntity(docSol);


                                    }
                                    else  // Limpa as informações de entrega se necessário
                                    {
                                        if ((bool)arquivo.EntregaPosterior)
                                        {
                                            docSol.SituacaoEntregaDocumento = SituacaoEntregaDocumento.Pendente;
                                            docSol.DataEntrega = null;
                                            docSol.FormaEntregaDocumento = null;
                                            docSol.VersaoDocumento = null;
                                            docSol.Observacao = null;
                                            docSol.ObservacaoSecretaria = null;
                                            docSol.EntregaPosterior = arquivo.EntregaPosterior;
                                            docSol.DataPrazoEntrega = arquivo.EntregaPosterior.HasValue && arquivo.EntregaPosterior.Value && arquivo.DataLimiteEntrega.HasValue ? arquivo.DataLimiteEntrega : null;
                                        }
                                        else if (arquivo.ArquivoAnexado == null && !arquivo.TemAnexoAnterior && docSol.SituacaoEntregaDocumento != SituacaoEntregaDocumento.AguardandoEntrega)
                                        {
                                            docSol.SituacaoEntregaDocumento = SituacaoEntregaDocumento.AguardandoEntrega;
                                            docSol.DataEntrega = null;
                                            docSol.FormaEntregaDocumento = null;
                                            docSol.VersaoDocumento = null;
                                            docSol.Observacao = null;
                                            docSol.ObservacaoSecretaria = null;
                                            docSol.EntregaPosterior = arquivo.EntregaPosterior;
                                            docSol.DataPrazoEntrega = arquivo.EntregaPosterior.HasValue && arquivo.EntregaPosterior.Value && arquivo.DataLimiteEntrega.HasValue ? arquivo.DataLimiteEntrega : null;
                                        }

                                        //TemAnexoAnterior é utilizado pela modal de entrega de documentos acionada através do botão da Home. Valor padrão é false
                                        if (arquivo.ArquivoAnexado == null && docSol.SeqArquivoAnexado.HasValue && !arquivo.TemAnexoAnterior)
                                        {
                                            if (docSol.SeqArquivoAnexado.HasValue)
                                            {
                                                //arquivosExclusao.Add(docSol.SeqArquivoAnexado.Value);
                                                docSol.SeqArquivoAnexado = null;
                                            }
                                        }
                                    }
                                }
                            }


                        }

                        // Se o documento aceita mais do que um, verifica se teve algum excluído
                        if (documento.PermiteVarios)
                        {
                            solicitacaoServico.DocumentosRequeridos = solicitacaoServico.DocumentosRequeridos.Where(d => d.SeqDocumentoRequerido != documento.SeqDocumentoRequerido ||
                                                                                                                   (d.SeqDocumentoRequerido == documento.SeqDocumentoRequerido &&
                                                                                                                    documento.Documentos.Select(a => a.Seq).Contains(d.Seq))).ToList();
                        }
                    }

                    modelo.Documentos = AtualizarStatusDocumentosModel(modelo.Documentos, solicitacaoServico.DocumentosRequeridos);

                    this.SolicitacaoServicoDomainService.SaveEntity(solicitacaoServico);

                    //Combinado com a Carol/Jéssica que será feita uma manutenção de tempo em tempo para excluir os arquivos que estão sem referência
                    //foreach (var arquivo in arquivosExclusao)
                    //    ArquivoAnexadoDomainService.DeleteEntity(new ArquivoAnexado() { Seq = arquivo });

                    ValidarDocumentoObrigatorio(modelo);

                    if (enviarNotificacao)
                    {
                        EnviarNotificacaoUploadAluno(modelo.SeqSolicitacaoServico);
                    }

                    unityOfWork.Commit();
                }
                catch (Exception)
                {
                    unityOfWork.Rollback();
                    throw;
                }
            }
        }

        public List<DocumentoVO> BuscarDocumentosRegistroPorStatus(long seqSolicitacaoServico, long? seqConfiguracaoEtapa = null, List<SituacaoEntregaDocumento> situacoes = null, bool? permiteUploadArquivo = null)
        {
            var tiposDocumentos = TipoDocumentoService.BuscarTiposDocumentos();

            var specSolicitacaoServico = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);
            var solicitacaoServico = this.SolicitacaoServicoDomainService.SearchProjectionByKey(specSolicitacaoServico, s => new
            {
                SeqsConfiguracoesEtapas = s.ConfiguracaoProcesso.ConfiguracoesEtapa.Select(a => a.Seq),
                SexoPessoaAtuacao = s.PessoaAtuacao.DadosPessoais.Sexo,
                SeqPessoaAtuacao = s.SeqPessoaAtuacao,
                DocumentosEnviados = s.DocumentosRequeridos.Where(w => situacoes.Contains(w.SituacaoEntregaDocumento)).Select(d => new DocumentoItemVO
                {
                    Seq = d.Seq,
                    SeqSolicitacaoServico = d.SeqSolicitacaoServico,
                    SeqDocumentoRequerido = d.SeqDocumentoRequerido,
                    SeqTipoDocumento = d.DocumentoRequerido.SeqTipoDocumento,
                    SeqArquivoAnexado = d.SeqArquivoAnexado,
                    ArquivoAnexado = d.SeqArquivoAnexado.HasValue ? new SMCUploadFile
                    {
                        FileData = d.ArquivoAnexado.Conteudo,
                        GuidFile = d.ArquivoAnexado.UidArquivo.ToString(),
                        Name = d.ArquivoAnexado.Nome,
                        Size = d.ArquivoAnexado.Tamanho,
                        Type = d.ArquivoAnexado.Tipo
                    } : null,
                    DataEntrega = d.DataEntrega,
                    FormaEntregaDocumento = d.FormaEntregaDocumento,
                    Observacao = d.Observacao,
                    ObservacaoSecretaria = d.ObservacaoSecretaria,
                    DescricaoInconformidade = d.DescricaoInconformidade,
                    VersaoDocumento = d.VersaoDocumento,
                    SituacaoEntregaDocumento = d.SituacaoEntregaDocumento,
                    SituacaoEntregaDocumentoInicial = d.SituacaoEntregaDocumento,
                    VersaoExigida = d.DocumentoRequerido.VersaoDocumento,
                    EntregaPosterior = d.EntregaPosterior,
                    DataPrazoEntrega = d.DataPrazoEntrega,
                }).ToList()
            });

            // Filtra as configurações etapa.
            var configuracoesEtapas = (seqConfiguracaoEtapa.HasValue) ?
                                        solicitacaoServico.SeqsConfiguracoesEtapas.Where(f => f == seqConfiguracaoEtapa.Value).ToArray() :
                                        solicitacaoServico.SeqsConfiguracoesEtapas.ToArray();

            var specConfiguracoesEtapa = new SMCContainsSpecification<DocumentoRequerido, long>(c => c.SeqConfiguracaoEtapa, configuracoesEtapas);
            var specFiltroDocumentoRequerido = new DocumentoRequeridoFilterSpecification { Sexo = solicitacaoServico.SexoPessoaAtuacao, PermiteUploadArquivo = permiteUploadArquivo };
            var specDocumento = new SMCAndSpecification<DocumentoRequerido>(specConfiguracoesEtapa, specFiltroDocumentoRequerido);

            var documentosRequeridos = this.DocumentoRequeridoDomainService.SearchProjectionBySpecification(
                specDocumento,
                d => new DocumentoVO()
                {
                    SeqDocumentoRequerido = d.Seq,
                    SeqTipoDocumento = d.SeqTipoDocumento,
                    SeqConfiguracaoEtapa = d.SeqConfiguracaoEtapa,
                    PermiteVarios = d.PermiteVarios,
                    Obrigatorio = d.Obrigatorio,
                    PermiteUploadArquivo = d.PermiteUploadArquivo,
                    ObrigatorioUpload = d.ObrigatorioUpload,
                    PermiteEntregaPosterior = d.PermiteEntregaPosterior,
                    Sexo = d.Sexo ?? Sexo.Nenhum,
                    ValidacaoOutroSetor = d.ValidacaoOutroSetor,
                    DataLimiteEntrega = d.DataLimiteEntrega,
                }).ToList();

            var specGrupoDocumentoRequerido = new SMCContainsSpecification<GrupoDocumentoRequeridoItem, long>(c => c.GrupoDocumentoRequerido.SeqConfiguracaoEtapa, configuracoesEtapas);

            var gruposDocumentosRequeridos = this.GrupoDocumentoRequeridoItemDomainService.SearchProjectionBySpecification(specGrupoDocumentoRequerido,
                d => new
                {
                    SeqGrupoDocumentoRequerido = d.SeqGrupoDocumentoRequerido,
                    SeqDocumentoRequerido = d.SeqDocumentoRequerido,
                    DescricaoGrupoDocumentoRequerido = d.GrupoDocumentoRequerido.Descricao,
                    NumeroMinimoDocumentosRequerido = d.GrupoDocumentoRequerido.MinimoObrigatorio
                });

            documentosRequeridos.ForEach(d =>
            {
                d.Grupos = gruposDocumentosRequeridos.Where(e => e.SeqDocumentoRequerido == d.SeqDocumentoRequerido).Select(g => new GrupoDocumentoVO
                {
                    Seq = g.SeqGrupoDocumentoRequerido,
                    Descricao = g.DescricaoGrupoDocumentoRequerido,
                    NumeroMinimoDocumentosRequerido = g.NumeroMinimoDocumentosRequerido
                }).ToList();

                d.Documentos = solicitacaoServico.DocumentosEnviados.Where(e => e.SeqDocumentoRequerido == d.SeqDocumentoRequerido).ToList() ?? new List<DocumentoItemVO>();
                d.DescricaoTipoDocumento = tiposDocumentos.FirstOrDefault(t => t.Seq == d.SeqTipoDocumento)?.Descricao;

                if (d.PermiteVarios && !d.Documentos.SMCAny(w => situacoes.Contains(w.SituacaoEntregaDocumento)))
                    d.Documentos.Add(new DocumentoItemVO());

                d.SolicitacoesEntregaDocumento = BuscarSituacoesEntregaDocumento(d);
            });

            return documentosRequeridos.Where(w => w.Documentos.SMCAny(s => situacoes.Contains(s.SituacaoEntregaDocumento))).OrderBy(d => d.DescricaoTipoDocumento).ToList();
        }

        /// <summary>
        /// Atualiza os documentos da model de acordo com as atualizações feitas nos documentos da solicitação
        /// </summary>
        private List<DocumentoVO> AtualizarStatusDocumentosModel(List<DocumentoVO> DocumentosModel, IList<SolicitacaoDocumentoRequerido> DocumentosRequeridosSolicitacao)
        {
            foreach (var documento in DocumentosModel)
            {
                foreach (var anexo in documento.Documentos)
                {
                    var anexoSolicitacao = DocumentosRequeridosSolicitacao.Where(c => c.Seq == anexo.Seq).FirstOrDefault();

                    if (anexoSolicitacao != null)
                    {
                        anexo.SituacaoEntregaDocumento = anexoSolicitacao.SituacaoEntregaDocumento;
                    }
                }
            }

            return DocumentosModel;
        }

        /// <summary>
        /// Envia notificação para nova entrega de documentação
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de servico</param>
        public void EnviarNotificacaoUploadAluno(long seqSolicitacaoServico)
        {
            using (var unityOfWork = SMCUnitOfWork.Begin())
            {
                long seqProecessoEtapa = this.SolicitacaoServicoDomainService.SearchProjectionByKey(seqSolicitacaoServico, p => p.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.SeqProcessoEtapa);
                // Busca os dados da solicitação
                var dadosSolicitacao = this.SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
                {
                    DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao,
                    NumeroProtocolo = x.NumeroProtocolo,
                    NomeSocialSolicitante = x.PessoaAtuacao.DadosPessoais.NomeSocial,
                    NomeSolicitante = x.PessoaAtuacao.DadosPessoais.Nome,
                    x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(f => f.SeqProcessoEtapa == seqProecessoEtapa).ProcessoEtapa.ConfiguracoesNotificacao.FirstOrDefault(f => f.SeqProcessoEtapa == seqProecessoEtapa).EnvioAutomatico,
                    TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token
                });

                //Enviar notificação somente quando a página for acionada pelo SGA.Aluno e e a etapa atual for diferente da primeira, de acordo com:
                if (dadosSolicitacao.EnvioAutomatico)
                {
                    // Monta os dados para merge de envio de notificações
                    Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
                    dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, string.IsNullOrEmpty(dadosSolicitacao.NomeSocialSolicitante) ? dadosSolicitacao.NomeSolicitante : string.Format("{0} ({1})", dadosSolicitacao.NomeSocialSolicitante, dadosSolicitacao.NomeSolicitante));
                    dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.DSC_PROCESSO, dadosSolicitacao.DescricaoProcesso);
                    dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.NUM_PROTOCOLO, dadosSolicitacao.NumeroProtocolo);

                    // Envia a notificação de CRIAÇÃO DA SOLICITAÇÃO NO PORTAL
                    var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                    {
                        SeqSolicitacaoServico = seqSolicitacaoServico,
                        TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.NOVA_ENTREGA_DOCUMENTACAO,
                        DadosMerge = dadosMerge,
                        EnvioSolicitante = false,
                        ConfiguracaoPrimeiraEtapa = false
                    };

                    if (dadosSolicitacao.TokenServico != TOKEN_SERVICO.ENTREGA_DOCUMENTACAO)
                        SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);
                }

                unityOfWork.Commit();
            }
        }

        //RN_SRC_134 - Finalização solicitação entrega documento
        public void SalvarRegistrarEntregaDocumentacaoAtendimento(RegistroDocumentoAtendimentoVO model, long seqSolicitacaoServico)
        {
            var solicitacaoServico = this.SolicitacaoServicoDomainService.SearchByKey(seqSolicitacaoServico, IncludesSolicitacaoServico.DocumentosRequeridos);

            var specHistoricoSituacaoAtual = new SolicitacaoHistoricoSituacaoFilterSpecification { Seq = solicitacaoServico.SeqSolicitacaoHistoricoSituacaoAtual };
            var situacaoAtual = this.SolicitacaoHistoricoSituacaoDomainService.SearchByKey(specHistoricoSituacaoAtual);

            var specSolicitacaoServicoEtapa = new SolicitacaoServicoEtapaFilterSpecification { Seq = situacaoAtual.SeqSolicitacaoServicoEtapa };
            var solicitacaoServicoEtapa = this.SolicitacaoServicoEtapaDomainService.SearchByKey(specSolicitacaoServicoEtapa);

            var specConfiguracaoEtapa = new ConfiguracaoEtapaFilterSpecification { Seq = solicitacaoServicoEtapa.SeqConfiguracaoEtapa };
            var configuracaoEtapa = this.ConfiguracaoEtapaDomainService.SearchByKey(specConfiguracaoEtapa);

            var seqMotivoBloqueio = this.MotivoBloqueioDomainService.BuscarSeqMotivoBloqueioPorToken(TOKEN_MOTIVO_BLOQUEIO.ENTREGA_DOCUMENTACAO);
            if (seqMotivoBloqueio == 0)
                throw new RegistroDocumentoMotivoBloqueioNaoCadastradoException();

            var specBloqueio = new PessoaAtuacaoBloqueioFilterSpecification()
            {
                SeqPessoaAtuacao = solicitacaoServico.SeqPessoaAtuacao,
                SeqMotivoBloqueio = new List<long>() { seqMotivoBloqueio },
                SituacaoBloqueio = SituacaoBloqueio.Bloqueado
            };

            model.SeqSolicitacaoServico = seqSolicitacaoServico;

            var documentosSolicitacao = this.BuscarDocumentosRegistro(seqSolicitacaoServico).TransformList<DocumentoAtendimentoVO>();

            var pessoaAtuacaoBloqueioEntregaDocumentacao = this.PessoaAtuacaoBloqueioDomainService.SearchBySpecification(specBloqueio, IncludesPessoaAtuacaoBloqueio.Itens).ToList();

            var qtdDocumentosInvalidos = model.Documentos
                                              .SelectMany(d => d.Documentos)
                                              .Where(doc => (doc.FormaEntregaDocumento == FormaEntregaDocumento.Upload ||
                                                             doc.FormaEntregaDocumento == FormaEntregaDocumento.Email) &&
                                                             doc.ArquivoAnexado == null).Count();

            //Caso encontre algum documento nas condições acima, exibir mensagem de erro
            if (qtdDocumentosInvalidos > 0)
                throw new RegistroDocumentoArquivoAnexoObrigatorioException();

            var descricaoAtualizada = "Documentos validados: ";
            if (solicitacaoServico != null)
            {
                #region Busca do Serviço
                var configuracaoProcessoSpec = new ConfiguracaoProcessoFilterSpecification() { Seq = solicitacaoServico.SeqConfiguracaoProcesso };
                var configuracaoProcesso = ConfiguracaoProcessoDomainService.SearchByKey(configuracaoProcessoSpec);
                var processoSpec = new ProcessoFilterSpecification() { Seq = configuracaoProcesso.SeqProcesso };
                var processo = ProcessoDomainService.SearchByKey(processoSpec);
                var servicoSpec = new ServicoFilterSpecification() { Seq = processo.SeqServico };
                var servico = ServicoDomainService.SearchByKey(servicoSpec);
                #endregion Busca do Serviço

                var documentos = solicitacaoServico.DocumentosRequeridos;
                using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
                {
                    foreach (var documento in model.Documentos)
                    {
                        foreach (var item in documento.Documentos)
                        {

                            var descricaoTipoDocumento = TipoDocumentoService.BuscarDescricaoTipoDocumento(item.SeqTipoDocumento);
                            var seqsDocumentoRequerido = documentos.Select(x => x.SeqDocumentoRequerido).ToArray();

                            var documentoRequeridoSpec = new DocumentoRequeridoFilterSpecification { Seqs = seqsDocumentoRequerido, SeqTipoDocumento = item.SeqTipoDocumento };
                            var documentoRequerido = DocumentoRequeridoDomainService.SearchByKey(documentoRequeridoSpec);

                            var solicitacaoDocumentoRequerido = solicitacaoServico.DocumentosRequeridos.SingleOrDefault(x => x.SeqDocumentoRequerido == documentoRequerido.Seq);
                            if (servico.Token == TOKEN_SERVICO.ATUALIZACAO_DOCUMENTACAO_EMISSAO_DIPLOMA)
                            {
                                var documentoAtual = documentosSolicitacao.SingleOrDefault(x => x.SeqTipoDocumento == item.SeqTipoDocumento);

                                if (documentoAtual != null && documentoAtual.Obrigatorio && !item.EntregueAnteriormente && item.ArquivoAnexado == null)
                                    throw new RegistroDocumentoArquivosObrigatoriosException();

                                if (documentoAtual.Obrigatorio && documentoAtual.Grupos != null && documentoAtual.Grupos.Count() > 0)
                                {
                                    foreach (var grupo in documentoAtual.Grupos)
                                    {
                                        var documentosGrupo = documentoAtual.Grupos.Where(d => d.Seq == grupo.Seq).ToList();

                                        var situacoesValidadas = model.Documentos.Where(x => x.Documentos.Any(y => y.SeqTipoDocumento == documentoAtual.SeqTipoDocumento
                                        && y.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido
                                        || y.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel)).ToList();

                                        if (situacoesValidadas.Count < grupo.NumeroMinimoDocumentosRequerido)
                                            throw new RegistroDocumentoArquivosObrigatoriosException();
                                    }
                                }
                            }
                            //ATUALIZAR DESCRIÇÃO DA SOLICITAÇÃO
                            //Salvar no campo "Descrição da solicitação atualizada", em formato html:
                            //Documentos validados:
                            //- <Descrição dos tipos de documento que estão exibidos na interface> +: + <Descrição da situação do respectivo documento>
                            //OBS.: Separar cada documento com quebra de linha.
                            if (descricaoTipoDocumento != null)
                                descricaoAtualizada += "<br/>" + "- " + descricaoTipoDocumento + ": " + item.SituacaoEntregaDocumento.SMCGetDescription();

                            if (solicitacaoDocumentoRequerido != null)
                            {

                                //Caso não exista o arquivo, será adicionado em caso de atualização de emissão de diploma
                                if (servico.Token == TOKEN_SERVICO.ATUALIZACAO_DOCUMENTACAO_EMISSAO_DIPLOMA)
                                {

                                    if (item.ArquivoAnexado != null && item.SeqArquivoAnexado == null)
                                    {
                                        var solicitacaoDocumentoAtualizar = solicitacaoServico.DocumentosRequeridos.SingleOrDefault(x => x.SeqDocumentoRequerido == solicitacaoDocumentoRequerido.SeqDocumentoRequerido);

                                        var arquivoAnexado = new ArquivoAnexado();

                                        if (solicitacaoDocumentoAtualizar.SeqArquivoAnexado == null)
                                        {
                                            arquivoAnexado = item.ArquivoAnexado.Transform<ArquivoAnexado>();

                                            ArquivoAnexadoDomainService.SaveEntity(arquivoAnexado);

                                            solicitacaoDocumentoAtualizar.DataEntrega = DateTime.Now.Date;
                                            //solicitacaoDocumentoAtualizar.ArquivoAnexado = arquivoAnexado;
                                            solicitacaoDocumentoAtualizar.DescricaoInconformidade = null;

                                            EnsureFileIntegrity(solicitacaoDocumentoAtualizar, x => x.SeqArquivoAnexado, x => x.ArquivoAnexado);

                                            solicitacaoDocumentoAtualizar.SeqArquivoAnexado = arquivoAnexado.Seq;

                                            item.SeqArquivoAnexado = arquivoAnexado.Seq;

                                            this.SolicitacaoDocumentoRequeridoDomainService.SaveEntity(solicitacaoDocumentoAtualizar);
                                            this.SolicitacaoServicoDomainService.SaveEntity(solicitacaoServico);
                                        }
                                    }
                                }


                                //Verificar se existe algum documento exibido na interface foi entregue anteriormente e está sendo deferido(situação Deferido ou Aguardando análise do setor responsável).
                                //Realizar atualização do registro do documento na solicitação, de acordo com os seguintes dados:
                                if (item.EntregueAnteriormente && (item.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido || item.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel))
                                {

                                    solicitacaoDocumentoRequerido.SituacaoEntregaDocumento = item.SituacaoEntregaDocumento;
                                    solicitacaoDocumentoRequerido.FormaEntregaDocumento = item.FormaEntregaDocumento;
                                    solicitacaoDocumentoRequerido.VersaoDocumento = item.VersaoDocumento;
                                    solicitacaoDocumentoRequerido.DataEntrega = item.DataEntrega;
                                    solicitacaoDocumentoRequerido.Observacao = item.Observacao;
                                    solicitacaoDocumentoRequerido.ObservacaoSecretaria = item.ObservacaoSecretaria;
                                    solicitacaoDocumentoRequerido.SeqArquivoAnexado = item.SeqArquivoAnexado;
                                    solicitacaoDocumentoRequerido.DataPrazoEntrega = null;

                                    //1.2.2.Realizar atualização do documento da pessoa - atuação, de acordo com o sequencial associado
                                    //ao registro do documento da solicitação:
                                    var specPessoaAtuacao = new PessoaAtuacaoDocumentoFilterSpecification { SeqPessoaAtuacao = solicitacaoServico.SeqPessoaAtuacao, SeqTipoDocumento = item.SeqTipoDocumento };
                                    var pessoaAtuacaoDocumento = this.PessoaAtuacaoDocumentoDomainService.SearchByKey(specPessoaAtuacao);

                                    pessoaAtuacaoDocumento.SolicitacaoDocumentoRequerido = solicitacaoDocumentoRequerido;
                                    pessoaAtuacaoDocumento.DataEntrega = item.DataEntrega.HasValue ? item.DataEntrega.Value : DateTime.Now;
                                    pessoaAtuacaoDocumento.Observacao = item.ObservacaoSecretaria;
                                    pessoaAtuacaoDocumento.SeqArquivoAnexado = item.SeqArquivoAnexado;
                                    pessoaAtuacaoDocumento.SeqSolicitacaoDocumentoRequerido = solicitacaoDocumentoRequerido.Seq;

                                    this.PessoaAtuacaoDocumentoDomainService.SaveEntity(pessoaAtuacaoDocumento);
                                }



                                //Caso o documento não tenha sido entregue anteriormente e está sendo deferido, realizar atualização do registro do documento na solicitação, de acordo com os seguintes dados:
                                if (!item.EntregueAnteriormente && (item.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido || item.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel))
                                {

                                    solicitacaoDocumentoRequerido.SituacaoEntregaDocumento = item.SituacaoEntregaDocumento;
                                    solicitacaoDocumentoRequerido.FormaEntregaDocumento = item.FormaEntregaDocumento;
                                    solicitacaoDocumentoRequerido.VersaoDocumento = item.VersaoDocumento;
                                    solicitacaoDocumentoRequerido.DataEntrega = item.DataEntrega;
                                    solicitacaoDocumentoRequerido.Observacao = item.Observacao;
                                    solicitacaoDocumentoRequerido.ObservacaoSecretaria = item.ObservacaoSecretaria;
                                    solicitacaoDocumentoRequerido.SeqArquivoAnexado = item.SeqArquivoAnexado;
                                    solicitacaoDocumentoRequerido.DataPrazoEntrega = null;
                                    solicitacaoDocumentoRequerido.DescricaoInconformidade = item.DescricaoInconformidade;

                                    //2.1.Salvar o documento na tabela de documentos da pessoa-atuação, conforme:
                                    var novoDocumentoPessoaAtuacao = new PessoaAtuacaoDocumento
                                    {
                                        SeqPessoaAtuacao = solicitacaoServico.SeqPessoaAtuacao,
                                        SeqTipoDocumento = item.SeqTipoDocumento,
                                        SeqSolicitacaoDocumentoRequerido = solicitacaoDocumentoRequerido.Seq,
                                        Observacao = item.ObservacaoSecretaria,
                                        SeqArquivoAnexado = item.SeqArquivoAnexado,
                                        DataEntrega = item.DataEntrega.HasValue ? item.DataEntrega.Value : DateTime.Now
                                    };
                                    this.PessoaAtuacaoDocumentoDomainService.SaveEntity(novoDocumentoPessoaAtuacao.Transform<PessoaAtuacaoDocumento>());
                                }

                                //Para documentos que estão sendo indeferidos, realizar atualização do registro do documento na solicitação, de acordo com os seguintes dados:
                                if (item.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Indeferido)
                                {
                                    solicitacaoDocumentoRequerido.SituacaoEntregaDocumento = item.SituacaoEntregaDocumento;
                                    solicitacaoDocumentoRequerido.FormaEntregaDocumento = item.FormaEntregaDocumento;
                                    solicitacaoDocumentoRequerido.VersaoDocumento = item.VersaoDocumento;
                                    solicitacaoDocumentoRequerido.DataEntrega = item.DataEntrega;
                                    solicitacaoDocumentoRequerido.Observacao = item.Observacao;
                                    solicitacaoDocumentoRequerido.ObservacaoSecretaria = item.ObservacaoSecretaria;
                                    solicitacaoDocumentoRequerido.SeqArquivoAnexado = item.SeqArquivoAnexado;
                                    solicitacaoDocumentoRequerido.DataPrazoEntrega = null;
                                    solicitacaoDocumentoRequerido.DescricaoInconformidade = item.DescricaoInconformidade;
                                }


                                this.SolicitacaoDocumentoRequeridoDomainService.SaveEntity(solicitacaoDocumentoRequerido);

                                //DESBLOQUEAR BLOQUEIO DE DOCUMENTAÇÃO PENDENTE - RN_PES_047 - Desbloqueio documentação pendente
                                if (pessoaAtuacaoBloqueioEntregaDocumentacao != null && (item.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido || item.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel))
                                {
                                    foreach (var bloqueio in pessoaAtuacaoBloqueioEntregaDocumentacao)
                                    {

                                        var bloqueioItem = bloqueio.Itens.FirstOrDefault(x => x.CodigoIntegracaoSistemaOrigem == item.SeqTipoDocumento.ToString());
                                        if (bloqueioItem != null)
                                        {
                                            //Atualiza pessoa atuação bloqueio conforme regra RN_PES_047
                                            bloqueio.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                                            bloqueio.TipoDesbloqueio = TipoDesbloqueio.Efetivo;
                                            bloqueio.UsuarioDesbloqueioEfetivo = SMCContext.User.Identity.Name;
                                            bloqueio.DataDesbloqueioEfetivo = DateTime.Now;
                                            bloqueio.JustificativaDesbloqueio = "Entrega de documentação";

                                            //Atualiza pessoa atuação bloqueio ITEM conforme regra RN_PES_047
                                            bloqueioItem.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                                            bloqueioItem.UsuarioDesbloqueio = SMCContext.User.Identity.Name;
                                            bloqueioItem.DataDesbloqueio = DateTime.Now;

                                            this.PessoaAtuacaoBloqueioDomainService.UpdateEntity(bloqueio, x => x.Itens);
                                            this.PessoaAtuacaoBloqueioDomainService.SaveEntity(bloqueio);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //ATUALIZAR DATA SOLUÇÃO DA SOLICITAÇÃO SERVIÇO
                    solicitacaoServico.DataSolucao = DateTime.Now;

                    var existeDocumentoIndeferido = model.Documentos.Any(x => x.Documentos.Any(y => y.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Indeferido));

                    //Valida situação documentação da solicitação
                    var situacaoDocumento = ValidaSituacaoDocumentoAtendimento(model.Documentos);

                    solicitacaoServico.SituacaoDocumentacao = situacaoDocumento;

                    //RN_SRC_132 - Realizar atendimento - Notificação encerramento da entrega de documento
                    //Uma notificação só poderá ser enviada se o parâmetro "Envio de notificação automático?" for igual a
                    //SIM na configuração da notificação para etapa processo em questão.

                    //carrega parametros para envio de notificação
                    var parametros = EnviarNotificacaoSolicitacaoAtendimento(seqSolicitacaoServico, model);

                    //envia notificação efetivamente
                    if (parametros != null && servico.Token == TOKEN_SERVICO.ENTREGA_DOCUMENTACAO)
                    {
                        parametros.TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.FINALIZACAO_SOLICITACAO_ENTREGA_DOCUMENTO;
                        SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);
                        SolicitacaoServicoDomainService.ProcedimentosFinalizarEtapa(seqSolicitacaoServico, configuracaoEtapa.Seq, Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.FinalizadoComSucesso, null);
                    }

                    if (parametros != null && servico.Token == TOKEN_SERVICO.ATUALIZACAO_DOCUMENTACAO_EMISSAO_DIPLOMA && existeDocumentoIndeferido)
                    {
                        parametros.TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.FINALIZACAO_SOLICITACAO_ENTREGA_DOCUMENTO;
                        parametros.EnvioSolicitante = true;
                        SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);
                        SolicitacaoServicoDomainService.ProcedimentosFinalizarEtapa(seqSolicitacaoServico, configuracaoEtapa.Seq, Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.FinalizadoSemSucesso, null);
                    }

                    if (parametros != null && servico.Token == TOKEN_SERVICO.ATUALIZACAO_DOCUMENTACAO_EMISSAO_DIPLOMA && !existeDocumentoIndeferido)
                    {
                        parametros.TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.FINALIZACAO_SOLICITACAO_ATUALIZACAO_DOCUMENTO_CRA;
                        parametros.EnvioSolicitante = false;
                        SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);
                        SolicitacaoServicoDomainService.ProcedimentosFinalizarEtapa(seqSolicitacaoServico, configuracaoEtapa.Seq, Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.NaoAlterado, null);
                    }

                    solicitacaoServico.DescricaoAtualizada = descricaoAtualizada;

                    var specFinalizacaoSituacaoHistorico = new SolicitacaoHistoricoSituacaoFilterSpecification { SeqSolicitacaoServico = seqSolicitacaoServico, CategoriaSituacao = Formularios.Common.Areas.TMP.Enums.CategoriaSituacao.Encerrado };
                    var seqSituacaoHistoricoFinalizada = SolicitacaoHistoricoSituacaoDomainService.SearchBySpecification(specFinalizacaoSituacaoHistorico).OrderByDescending(x => x.Seq).FirstOrDefault();

                    solicitacaoServico.SeqSolicitacaoHistoricoSituacaoAtual = seqSituacaoHistoricoFinalizada.Seq;

                    if (servico.Token == TOKEN_SERVICO.ENTREGA_DOCUMENTACAO)
                        MensagemDomainService.EnviarMensagemLinhaDoTempoEncerramentoSolicitacao(seqSolicitacaoServico, TOKEN_TIPO_MENSAGEM.ENCERRAMENTO_SOLICITACAO_SERVICO);

                    this.SolicitacaoServicoDomainService.SaveEntity(solicitacaoServico);

                    transacao.Commit();
                }
            }
        }

        private EnvioNotificacaoSolicitacaoServicoVO EnviarNotificacaoSolicitacaoAtendimento(long seqSolicitacaoServico, RegistroDocumentoAtendimentoVO model)
        {
            long seqProecessoEtapa = this.SolicitacaoServicoDomainService.SearchProjectionByKey(seqSolicitacaoServico, p => p.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.SeqProcessoEtapa);

            EnvioNotificacaoSolicitacaoServicoVO parametros = new EnvioNotificacaoSolicitacaoServicoVO();

            // Busca os dados da solicitação
            var dadosSolicitacao = this.SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
            {
                DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao,
                Token = x.ConfiguracaoProcesso.Processo.Servico.Token,
                NumeroProtocolo = x.NumeroProtocolo,
                NomeSocialSolicitante = x.PessoaAtuacao.DadosPessoais.NomeSocial,
                NomeSolicitante = x.PessoaAtuacao.DadosPessoais.Nome,
                x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(f => f.SeqProcessoEtapa == seqProecessoEtapa).ProcessoEtapa.ConfiguracoesNotificacao.FirstOrDefault(f => f.SeqProcessoEtapa == seqProecessoEtapa).EnvioAutomatico,
            });

            // Monta os dados para merge de envio de notificações
            Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
            dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, string.IsNullOrEmpty(dadosSolicitacao.NomeSocialSolicitante) ? dadosSolicitacao.NomeSolicitante : string.Format("{0} ({1})", dadosSolicitacao.NomeSocialSolicitante, dadosSolicitacao.NomeSolicitante));
            dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.DSC_PROCESSO, dadosSolicitacao.DescricaoProcesso);
            dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.NUM_PROTOCOLO, dadosSolicitacao.NumeroProtocolo);

            var documentosAtualizados = string.Empty;

            if (model.Documentos != null && model.Documentos.Count() > 0)
            {
                foreach (var documentos in model.Documentos)
                {
                    foreach (var documento in documentos.Documentos)
                    {
                        var descricaoTipoDocumento = TipoDocumentoService.BuscarDescricaoTipoDocumento(documento.SeqTipoDocumento);

                        documentosAtualizados += descricaoTipoDocumento + ": " + documento.SituacaoEntregaDocumento.SMCGetDescription() + "<br /> ";
                    }
                }
            }

            dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.LISTA_DOCUMENTOS, documentosAtualizados);

            // Envia a notificação de CRIAÇÃO DA SOLICITAÇÃO NO PORTAL
            parametros = new EnvioNotificacaoSolicitacaoServicoVO()
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
                DadosMerge = dadosMerge,
                EnvioSolicitante = true,
                ConfiguracaoPrimeiraEtapa = false
            };

            return parametros;
        }

        private SituacaoDocumentacao ValidaSituacaoDocumentoAtendimento(List<RegistroDocumentoAtendimentoDocumentoVO> documentos)
        {
            var situacao = SituacaoDocumentacao.Nenhum;
            /* Alterar a regra do comando “Salvar” do UC_SRC_004_02_01 - Registrar Documentos:

                * Verificar se todos os documentos obrigatórios estão em uma das situações: "Deferido" ou "Aguardando análise do setor responsável".
                    Se estiverem, salvar a situação da documentação da solicitação com o valor "Entregue";

                * Se não estiver, verificar se estão com as situações "Deferido", "Aguardando análise do setor responsável" e "Pendente"
                    Se estiverem, salvar a situação da documentação da solicitação com valor "Entregue com pendência"

                * Se não estiverem, verificar se pelo menos um está com a situação "Aguardando Validação"
                    Se estiver, salvar a situação da documentação da solicitação com valor "Aguardando validação"

                * Se não estiverem, verificar se pelo menos um está com a situação "Aguardando entrega" ou "Indeferido";
                    Se estiver, salvar a situação da documentação da solicitação com valor "Aguardando (nova) entrega".

                * Caso não exista lista de documentos requeridos para a solicitação, salvar a situação da documentação com o valor "Não requerida".
            */

            if (documentos.All(x => x.Documentos.All(dd => dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido ||
            dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel)))
            {
                situacao = SituacaoDocumentacao.Entregue;
            }
            else if (documentos.All(x => x.Documentos.All(dd => dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido ||
                                             dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel ||
                                             dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente)))
            {
                situacao = SituacaoDocumentacao.EntregueComPendencia;
            }
            else if (documentos.Any(x => x.Documentos.Any(dd => dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoValidacao)))
            {
                situacao = SituacaoDocumentacao.AguardandoValidacao;
            }
            else if (documentos.Any(x => x.Documentos.Any(dd => dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega ||
                                             dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Indeferido)))
            {
                situacao = SituacaoDocumentacao.AguardandoEntrega;
            }
            else
            {
                situacao = SituacaoDocumentacao.NaoRequerida;
            }

            return situacao;
        }

        public void FinalizarSolicitacaoCRA(long seqSolicitacaoServico)
        {
            var solicitacaoServico = this.SolicitacaoServicoDomainService.SearchByKey(seqSolicitacaoServico, IncludesSolicitacaoServico.DocumentosRequeridos);

            var specHistoricoSituacaoAtual = new SolicitacaoHistoricoSituacaoFilterSpecification { Seq = solicitacaoServico.SeqSolicitacaoHistoricoSituacaoAtual };
            var situacaoAtual = this.SolicitacaoHistoricoSituacaoDomainService.SearchByKey(specHistoricoSituacaoAtual);

            var specSolicitacaoServicoEtapa = new SolicitacaoServicoEtapaFilterSpecification { Seq = situacaoAtual.SeqSolicitacaoServicoEtapa };
            var solicitacaoServicoEtapa = this.SolicitacaoServicoEtapaDomainService.SearchByKey(specSolicitacaoServicoEtapa);

            var specConfiguracaoEtapa = new ConfiguracaoEtapaFilterSpecification { Seq = solicitacaoServicoEtapa.SeqConfiguracaoEtapa };
            var configuracaoEtapa = this.ConfiguracaoEtapaDomainService.SearchByKey(specConfiguracaoEtapa);

            SolicitacaoServicoDomainService.ProcedimentosFinalizarEtapa(seqSolicitacaoServico, configuracaoEtapa.Seq, Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.FinalizadoComSucesso, null);

        }
    }
}