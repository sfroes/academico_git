using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Common.Areas.FIN.Exceptions;
using SMC.Academico.Common.Areas.FIN.Includes;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.FIN.Validators;
using SMC.Academico.Domain.Areas.FIN.ValueObjects;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.DomainServices;
using SMC.Academico.Domain.Models;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.FIN.DomainServices
{
    public class ContratoDomainService : AcademicoContextDomain<Contrato>
    {
        #region [ Services ]

        private ISMCReportMergeService SMCReportMergeService
        {
            get { return this.Create<ISMCReportMergeService>(); }
        }

        private IEtapaService EtapaService
        {
            get { return this.Create<IEtapaService>(); }
        }

        #endregion [ Services ]

        #region [ DomainsServices ]

        private SolicitacaoMatriculaItemDomainService SolicitacaoMatriculaItemDomainService => Create<SolicitacaoMatriculaItemDomainService>();
        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private ArquivoAnexadoDomainService ArquivoAnexadoDomainService => Create<ArquivoAnexadoDomainService>();

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();
        private ContratoCursoDomainService ContratoCursoDomainService => Create<ContratoCursoDomainService>();

        private ContratoNivelEnsinoDomainService ContratoNivelEnsinoDomainService => Create<ContratoNivelEnsinoDomainService>();

        private CursoOfertaLocalidadeTurnoDomainService CursoOfertaLocalidadeTurnoDomainService => Create<CursoOfertaLocalidadeTurnoDomainService>();

        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();

        private InstituicaoNivelModeloRelatorioDomainService InstituicaoNivelModeloRelatorioDomainService => Create<InstituicaoNivelModeloRelatorioDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        private PessoaAtuacaoBeneficioDomainService PessoaAtuacaoBeneficioDomainService => Create<PessoaAtuacaoBeneficioDomainService>();

        private PessoaAtuacaoBloqueioDomainService PessoaAtuacaoBloqueioDomainService => Create<PessoaAtuacaoBloqueioDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private SolicitacaoMatriculaDomainService SolicitacaoMatriculaDomainService => Create<SolicitacaoMatriculaDomainService>();

        #endregion [ DomainsServices ]

        /// <summary>
        /// Lock para geração de termo de adesão para não permitir chamadas simultâneas
        /// </summary>
        private static object _sync = new object();

        public SMCUploadFile GerarTermoAdesaoContrato(long seqSolicitacaoMatricula, bool gerarTermo = false)
        {
            // Busca o contrato
            var model = BuscarAdesaoContrato(seqSolicitacaoMatricula, gerarTermo);

            SMCUploadFile arquivo = null;

            if (model.SeqArquivoTermoAdesao.HasValue)
            {
                // Buscar no banco o arquivo que ja foi gerado
                arquivo = ArquivoAnexadoDomainService.SearchByKey(new SMCSeqSpecification<ArquivoAnexado>(model.SeqArquivoTermoAdesao.Value)).Transform<SMCUploadFile>();
            }
            else
            {
                lock (_sync)
                {
                    var seqInstituicaoNivel = PessoaAtuacaoDomainService.BuscarInstituicaoNivelEnsinoESequenciaisPorPessoaAtuacao(model.SeqPessoaAtuacao).SeqInstituicaoNivelEnsino;
                    if (model.CPF == null)
                        model.CPF = model.Passaporte;
                    else
                        model.CPF = SMCMask.ApplyMaskCPF(model.CPF);

                    if (model.TipoAluno == TipoAluno.Calouro)
                    {
                        // Recupera o template do relatório
                        var template = InstituicaoNivelModeloRelatorioDomainService.BuscarTemplateModeloRelatorio(seqInstituicaoNivel, ModeloRelatorio.ModeloTermoAdesaoIngressante);

                        // Transforma o modelo de adesão para o modelo do relatório
                        var modeloRelatorio = model.Transform<ModeloRelatorioTermoAdesaoVO>();

                        // Ajusta os campos
                        //modeloRelatorio.CPF = SMCMask.ApplyMaskCPF(modeloRelatorio.CPF);
                        modeloRelatorio.Inicio = model.Inicio.SMCDataAbreviada();
                        modeloRelatorio.TerminoPrevisto = model.TerminoPrevisto.SMCDataAbreviada();
                        modeloRelatorio.ValorParcela = "R$ " + model.InformacoesFinanceiras.FirstOrDefault()?.ValorParcela.ToString("#,##0.00");
                        modeloRelatorio.ValorTotalContrato = "R$ " + model.InformacoesFinanceiras.FirstOrDefault()?.ValorTotalCurso.ToString("#,##0.00");
                        modeloRelatorio.DataAdesao = model.DataAdesao?.ToString("dd/MM/yyyy HH:mm");
                        modeloRelatorio.CodigoAdesao = model.CodigoAdesao.ToString().ToUpper();
                        modeloRelatorio.PrazoConclusao = $"{model.PrazoConclusao} meses";
                        modeloRelatorio.NumeroParcelas = model.InformacoesFinanceiras.FirstOrDefault()?.QuantidadeParcelas ?? 0;
                        modeloRelatorio.DiaVencimentoParcela = model.InformacoesFinanceiras.FirstOrDefault()?.DiaVencimentoParcelas.ToString();

                        // Chama o serviço para gerar o relatório
                        List<Dictionary<string, object>> modelDic = new List<Dictionary<string, object>>();
                        modelDic.Add(SMCReflectionHelper.ToDictionary(modeloRelatorio));

                        var bytesArquivo = SMCReportMergeService.MailMergeToPdf(template.ArquivoModelo.Conteudo, modelDic.ToArray());

                        // Salvar o arquivo e associar a solicitação de matricula
                        arquivo = SolicitacaoMatriculaDomainService.InserirArquivoTermoAdesao(seqSolicitacaoMatricula, bytesArquivo);
                    }
                    else
                    {
                        var template = InstituicaoNivelModeloRelatorioDomainService.BuscarTemplateModeloRelatorio(seqInstituicaoNivel, ModeloRelatorio.ModeloTermoAdesaoRenovacao);

                        // Cria modelo de relatório de renovação
                        var modeloRelatorio = new ModeloRelatorioTermoAdesaoRenovacaoVO();

                        // Ajusta os campos
                        modeloRelatorio.NomeContrato = model.NomeContrato;
                        modeloRelatorio.RA = model.NumeroRegistroAcademico.ToString();
                        modeloRelatorio.Nome = model.Nome;
                        modeloRelatorio.RG = model.RG;
                        modeloRelatorio.CPF = model.CPF;
                        modeloRelatorio.ResponsavelFinanceiro = model.ResponsavelFinanceiro;
                        modeloRelatorio.EntidadeResponsavel = model.EntidadeResponsavel;
                        modeloRelatorio.OfertaCurso = model.OfertaCurso;
                        modeloRelatorio.Modalidade = model.Modalidade;
                        modeloRelatorio.PrazoConclusao = $"{model.PrazoConclusao} meses";
                        modeloRelatorio.Inicio = model.Inicio.SMCDataAbreviada();
                        modeloRelatorio.TerminoPrevisto = model.TerminoPrevisto.SMCDataAbreviada();

                        // Dados das parcelas
                        var dadosServicoPadrao = model.InformacoesFinanceiras.FirstOrDefault(i => !i.ServicoAdicional);
                        if (dadosServicoPadrao != null)
                        {
                            modeloRelatorio.NumeroParcelas = dadosServicoPadrao.QuantidadeParcelas.ToString();
                            modeloRelatorio.ValorParcela = "R$ " + dadosServicoPadrao.ValorParcela.ToString("#,##0.00");
                            modeloRelatorio.InicioParcela = dadosServicoPadrao.DataInicioParcela.ToString("dd/MM/yyyy");
                            modeloRelatorio.DiaVencimentoParcela = dadosServicoPadrao.DiaVencimentoParcelas.ToString();
                            modeloRelatorio.QuantidadeParcelasRestantes = dadosServicoPadrao.QuantidadeParcelasRestantes;
                        }
                        else
                        {
                            modeloRelatorio.NumeroParcelas = "-";
                            modeloRelatorio.ValorParcela = "-";
                            modeloRelatorio.InicioParcela = "-";
                            modeloRelatorio.DiaVencimentoParcela = "-";
                        }

                        var dadosServicoAdicional = model.InformacoesFinanceiras.FirstOrDefault(i => i.ServicoAdicional);
                        if (dadosServicoAdicional != null)
                        {
                            modeloRelatorio.NumeroParcelasAdicional = dadosServicoAdicional.QuantidadeParcelas.ToString();
                            modeloRelatorio.ValorParcelaAdicional = "R$ " + dadosServicoAdicional.ValorParcela.ToString("#,##0.00");
                            modeloRelatorio.InicioParcelaAdicional = dadosServicoAdicional.DataInicioParcela.ToString("dd/MM/yyyy");
                            modeloRelatorio.DiaVencimentoParcelaAdicional = dadosServicoAdicional.DiaVencimentoParcelas.ToString();

                            modeloRelatorio.InicioServicoAdicional = model.DataInicioServico.ToString("dd/MM/yyyy");
                            modeloRelatorio.FimServicoAdicional = model.DataFimServico.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            modeloRelatorio.NumeroParcelasAdicional = "-";
                            modeloRelatorio.ValorParcelaAdicional = "-";
                            modeloRelatorio.InicioParcelaAdicional = "-";
                            modeloRelatorio.DiaVencimentoParcelaAdicional = "-";

                            modeloRelatorio.InicioServicoAdicional = "-";
                            modeloRelatorio.FimServicoAdicional = "-";
                        }
                        modeloRelatorio.DataAdesao = model.DataAdesao?.ToString("dd/MM/yyyy HH:mm");
                        modeloRelatorio.CodigoAdesao = model.CodigoAdesao.ToString().ToUpper();
                        modeloRelatorio.PrazoConclusao = $"{model.PrazoConclusao} meses";
                        modeloRelatorio.TermoAdesao = model.TermoAdesao;
                        modeloRelatorio.DescricaoCicloLetivo = model.DescricaoCicloLetivo;

                        // Chama o serviço para gerar o relatório
                        List<Dictionary<string, object>> modelDic = new List<Dictionary<string, object>>();
                        modelDic.Add(SMCReflectionHelper.ToDictionary(modeloRelatorio));
                        var bytesArquivo = SMCReportMergeService.MailMergeToPdf(template.ArquivoModelo.Conteudo, modelDic.ToArray());

                        // Salvar o arquivo e associar a solicitação de matricula
                        arquivo = SolicitacaoMatriculaDomainService.InserirArquivoTermoAdesao(seqSolicitacaoMatricula, bytesArquivo);
                    }
                }
            }

            return arquivo;
        }


        public SMCUploadFile GerarTermoAdesaoContratoResidenciaMedica(long seqSolicitacaoMatricula, bool gerarTermo = false)
        {
            // Busca o contrato
            var model = BuscarAdesaoContratoResidenciaMedica(seqSolicitacaoMatricula, gerarTermo);

            SMCUploadFile arquivo = null;

            if (model.SeqArquivoTermoAdesao.HasValue)
            {
                // Buscar no banco o arquivo que ja foi gerado
                arquivo = ArquivoAnexadoDomainService.SearchByKey(new SMCSeqSpecification<ArquivoAnexado>(model.SeqArquivoTermoAdesao.Value)).Transform<SMCUploadFile>();
            }
            else
            {
                lock (_sync)
                {
                    var seqInstituicaoNivel = PessoaAtuacaoDomainService.BuscarInstituicaoNivelEnsinoESequenciaisPorPessoaAtuacao(model.SeqPessoaAtuacao).SeqInstituicaoNivelEnsino;
                    if (model.CPF == null)
                        model.CPF = model.Passaporte;
                    else
                        model.CPF = SMCMask.ApplyMaskCPF(model.CPF);

                    if (model.TipoAluno == TipoAluno.Calouro)
                    {
                        // Recupera o template do relatório
                        var template = InstituicaoNivelModeloRelatorioDomainService.BuscarTemplateModeloRelatorio(seqInstituicaoNivel, ModeloRelatorio.ModeloTermoAdesaoIngressante);

                        // Transforma o modelo de adesão para o modelo do relatório
                        var modeloRelatorio = model.Transform<ModeloRelatorioTermoAdesaoVO>();

                        // Ajusta os campos
                        //modeloRelatorio.CPF = SMCMask.ApplyMaskCPF(modeloRelatorio.CPF);
                        modeloRelatorio.Inicio = model.Inicio.SMCDataAbreviada();
                        modeloRelatorio.TerminoPrevisto = model.TerminoPrevisto.SMCDataAbreviada();
                        modeloRelatorio.DataAdesao = model.DataAdesao?.ToString("dd/MM/yyyy HH:mm");
                        modeloRelatorio.CodigoAdesao = model.CodigoAdesao.ToString().ToUpper();
                        modeloRelatorio.PrazoConclusao = $"{model.PrazoConclusao} meses";

                        // Chama o serviço para gerar o relatório
                        List<Dictionary<string, object>> modelDic = new List<Dictionary<string, object>>();
                        modelDic.Add(SMCReflectionHelper.ToDictionary(modeloRelatorio));

                        var bytesArquivo = SMCReportMergeService.MailMergeToPdf(template.ArquivoModelo.Conteudo, modelDic.ToArray());

                        // Salvar o arquivo e associar a solicitação de matricula
                        arquivo = SolicitacaoMatriculaDomainService.InserirArquivoTermoAdesao(seqSolicitacaoMatricula, bytesArquivo);
                    }
                    else
                    {
                        var template = InstituicaoNivelModeloRelatorioDomainService.BuscarTemplateModeloRelatorio(seqInstituicaoNivel, ModeloRelatorio.ModeloTermoAdesaoRenovacao);

                        // Cria modelo de relatório de renovação
                        var modeloRelatorio = new ModeloRelatorioTermoAdesaoResidenciaMedicaVO();

                        // Ajusta os campos
                        modeloRelatorio.NomeContrato = model.NomeContrato;
                        modeloRelatorio.RA = model.NumeroRegistroAcademico.ToString();
                        modeloRelatorio.Nome = model.Nome;
                        modeloRelatorio.RG = model.RG;
                        modeloRelatorio.CPF = model.CPF;
                        modeloRelatorio.EntidadeResponsavel = model.EntidadeResponsavel;
                        modeloRelatorio.OfertaCurso = model.OfertaCurso;
                        modeloRelatorio.Modalidade = model.Modalidade;
                        modeloRelatorio.PrazoConclusao = $"{model.PrazoConclusao} meses";
                        modeloRelatorio.Inicio = model.Inicio.SMCDataAbreviada();
                        modeloRelatorio.TerminoPrevisto = model.TerminoPrevisto.SMCDataAbreviada();

                        modeloRelatorio.DataAdesao = model.DataAdesao?.ToString("dd/MM/yyyy HH:mm");
                        modeloRelatorio.CodigoAdesao = model.CodigoAdesao.ToString().ToUpper();
                        modeloRelatorio.TermoAdesao = model.TermoAdesao;
                        modeloRelatorio.DescricaoCicloLetivo = model.DescricaoCicloLetivo;

                        // Chama o serviço para gerar o relatório
                        List<Dictionary<string, object>> modelDic = new List<Dictionary<string, object>>();
                        modelDic.Add(SMCReflectionHelper.ToDictionary(modeloRelatorio));
                        var bytesArquivo = SMCReportMergeService.MailMergeToPdf(template.ArquivoModelo.Conteudo, modelDic.ToArray());

                        // Salvar o arquivo e associar a solicitação de matricula
                        arquivo = SolicitacaoMatriculaDomainService.InserirArquivoTermoAdesao(seqSolicitacaoMatricula, bytesArquivo);
                    }
                }
            }

            return arquivo;
        }

        public AdesaoContratoVO AderirContrato(AdesaoContratoDadosVO dados)
        {
            // Busca os dados da solicitação para fazer a adesão de contrato e atualização do histórico dos itens e mudança da etapa.
            var solicitacao = SolicitacaoMatriculaDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoMatricula>(dados.SeqSolicitacaoMatricula));

            // Verifica se tem bloqueio de final de etapa
            var bloqueios = PessoaAtuacaoBloqueioDomainService.BuscarPessoaAtuacaoBloqueios(dados.SeqPessoaAtuacao, dados.SeqConfiguracaoEtapa, true);
            if (bloqueios != null && bloqueios.Any())
                throw new SolicitacaoServicoComBloqueioFimEtapaException("fazer a adesão do contrato");

            //Iniciando a transacao
            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                if (!solicitacao.DataAdesao.HasValue || !solicitacao.CodigoAdesao.HasValue)
                {
                    solicitacao.DataAdesao = DateTime.Now;
                    solicitacao.CodigoAdesao = Guid.NewGuid();

                    // Gera a descrição da solicitação com as turmas e atividades selecionadas
                    solicitacao.DescricaoOriginal = SolicitacaoMatriculaItemDomainService.GerarDescricaoItensSolicitacao(dados.SeqSolicitacaoMatricula, dados.SeqConfiguracaoEtapa, false, false);

                    SolicitacaoMatriculaDomainService.UpdateFields(solicitacao, x => x.DataAdesao, x => x.CodigoAdesao, x => x.DescricaoOriginal);

                    // RN_ALN_039 - Criação do ingressante no sistema financeiro
                    var seqMatricula = IngressanteDomainService.CriaAlunoNoFinanceiro(dados.SeqSolicitacaoMatricula, dados.SeqPessoaAtuacao);

                    // A pedido do Humberto, vamos gerar o termo de adesão após a conclusão da criação do aluno no financeiro
                    GerarTermoAdesaoContrato(dados.SeqSolicitacaoMatricula, true);

                    SolicitacaoServicoDomainService.ProcedimentosFinalizarEtapa(dados.SeqSolicitacaoMatricula, dados.SeqConfiguracaoEtapa, ClassificacaoSituacaoFinal.FinalizadoComSucesso, null);
                }
                //throw new Exception("Roolback");
                transacao.Commit();
            }
            return BuscarAdesaoContrato(dados.SeqSolicitacaoMatricula);
        }

        public AdesaoContratoVO AderirContratoRenovacao(AdesaoContratoDadosVO dados)
        {
            // Busca os dados da solicitação para fazer a adesão de contrato e atualização do histórico dos itens e mudança da etapa.
            var solicitacao = SolicitacaoMatriculaDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoMatricula>(dados.SeqSolicitacaoMatricula));

            // Verifica se tem bloqueio de final de etapa
            var bloqueios = PessoaAtuacaoBloqueioDomainService.BuscarPessoaAtuacaoBloqueios(dados.SeqPessoaAtuacao, dados.SeqConfiguracaoEtapa, true);
            if (bloqueios != null && bloqueios.Any())
                throw new SolicitacaoServicoComBloqueioFimEtapaException("fazer a adesão do contrato");

            //Iniciando a transacao
            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                if (!solicitacao.DataAdesao.HasValue || !solicitacao.CodigoAdesao.HasValue)
                {
                    solicitacao.DataAdesao = DateTime.Now;
                    solicitacao.CodigoAdesao = Guid.NewGuid();

                    // Gera a descrição da solicitação com as turmas e atividades selecionadas
                    solicitacao.DescricaoOriginal = SolicitacaoMatriculaItemDomainService.GerarDescricaoItensSolicitacao(dados.SeqSolicitacaoMatricula, dados.SeqConfiguracaoEtapa, false, false);

                    SolicitacaoMatriculaDomainService.UpdateFields(solicitacao, x => x.DataAdesao, x => x.CodigoAdesao, x => x.DescricaoOriginal);

                    // RN_MAT_013 - Inclusão bloqueio financeiro parcela matrícula em aberto;
                    SolicitacaoMatriculaDomainService.CriaBloqueiosFinanceirosMatricula(dados.SeqSolicitacaoMatricula, dados.SeqPessoaAtuacao, TipoAtuacao.Aluno);

                    // A pedido do Humberto, vamos gerar o termo de adesão após a conclusão da criação do aluno no financeiro
                    GerarTermoAdesaoContrato(dados.SeqSolicitacaoMatricula, true);

                    SolicitacaoServicoDomainService.ProcedimentosFinalizarEtapa(dados.SeqSolicitacaoMatricula, dados.SeqConfiguracaoEtapa, ClassificacaoSituacaoFinal.FinalizadoComSucesso, null);
                }

                transacao.Commit();
            }
            return BuscarAdesaoContrato(dados.SeqSolicitacaoMatricula);
        }

        public AdesaoContratoVO AderirContratoResidenciaMedica(AdesaoContratoDadosVO dados)
        {
            // Busca os dados da solicitação para fazer a adesão de contrato e atualização do histórico dos itens e mudança da etapa.
            var solicitacao = SolicitacaoMatriculaDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoMatricula>(dados.SeqSolicitacaoMatricula));

            // Verifica se tem bloqueio de final de etapa
            var bloqueios = PessoaAtuacaoBloqueioDomainService.BuscarPessoaAtuacaoBloqueios(dados.SeqPessoaAtuacao, dados.SeqConfiguracaoEtapa, true);
            if (bloqueios != null && bloqueios.Any())
                throw new SolicitacaoServicoComBloqueioFimEtapaException("fazer a adesão do contrato");

            //Iniciando a transacao
            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                if (!solicitacao.DataAdesao.HasValue || !solicitacao.CodigoAdesao.HasValue)
                {
                    solicitacao.DataAdesao = DateTime.Now;
                    solicitacao.CodigoAdesao = Guid.NewGuid();

                    // Gera a descrição da solicitação com as turmas e atividades selecionadas
                    solicitacao.DescricaoOriginal = SolicitacaoMatriculaItemDomainService.GerarDescricaoItensSolicitacao(dados.SeqSolicitacaoMatricula, dados.SeqConfiguracaoEtapa, false, false);

                    SolicitacaoMatriculaDomainService.UpdateFields(solicitacao, x => x.DataAdesao, x => x.CodigoAdesao, x => x.DescricaoOriginal);

                    GerarTermoAdesaoContratoResidenciaMedica(dados.SeqSolicitacaoMatricula, true);

                    SolicitacaoServicoDomainService.ProcedimentosFinalizarEtapa(dados.SeqSolicitacaoMatricula, dados.SeqConfiguracaoEtapa, ClassificacaoSituacaoFinal.FinalizadoComSucesso, null);
                }

                transacao.Commit();
            }
            return BuscarAdesaoContratoResidenciaMedica(dados.SeqSolicitacaoMatricula);
        }



        #region Propriedade

        private const string INCLUSAO = "Inclusão";
        private const string ALTERAR = "Alteração";

        #endregion Propriedade

        public AdesaoContratoVO BuscarAdesaoContrato(long seqSolicitacaoMatricula, bool gerarTermo = false)
        {
            // Recupera a solicitação de matrícula
            var solicitacao = SolicitacaoMatriculaDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new
            {
                SeqCicloLetivo = x.ConfiguracaoProcesso.Processo.SeqCicloLetivo,
                TipoAluno = (x.PessoaAtuacao is Ingressante) ? TipoAluno.Calouro : TipoAluno.Veterano,
                CodAluno = (long?)(x.PessoaAtuacao as Ingressante).Seq ?? (x.PessoaAtuacao as Aluno).CodigoAlunoMigracao,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                CodigoAdesao = x.CodigoAdesao,
                NomeEntidadeResponsavel = x.EntidadeResponsavel.Nome,
                DataAdesao = x.DataAdesao,
                DescricaoTermoAdesao = x.TermoAdesao.Descricao,
                SeqArquivoTermoAdesao = x.SeqArquivoTermoAdesao,
                SeqArquivoContrato = x.TermoAdesao.Contrato.SeqArquivoAnexado,
                DescricaoContrato = x.TermoAdesao.Contrato.Descricao,
                NumeroRegistroAcademico = (long?)(x.PessoaAtuacao as Aluno).NumeroRegistroAcademico,
                DescricaoCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Descricao
            });

            // Recupera os dados de origem simulando o curso
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(solicitacao.SeqPessoaAtuacao);

            var dataEvento = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(solicitacao.SeqCicloLetivo.Value, dadosOrigem.SeqCursoOfertaLocalidadeTurno, solicitacao.TipoAluno, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

            var dadosPessoaAtuacao = PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(solicitacao.SeqPessoaAtuacao), x => new
            {
                CPF = x.Pessoa.Cpf,
                Passaporte = x.Pessoa.NumeroPassaporte,
                DescricaoModalidade = (x as Ingressante).Ofertas.FirstOrDefault().CampanhaOfertaItem.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Modalidade.Descricao ?? (x as Aluno).Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Modalidade.Descricao,
                Nome = x.DadosPessoais.Nome,
                NomeSocial = x.DadosPessoais.NomeSocial,
                DescricaoOferta = (x as Ingressante).Ofertas.FirstOrDefault().CampanhaOfertaItem.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao ?? (x as Aluno).Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                DataAdmissao = (DateTime?)(x as Ingressante).DataAdmissao ?? (x as Aluno).Historicos.FirstOrDefault(h => h.Atual).DataAdmissao,
                DataPrevisaoConclusao = (DateTime?)((x as Ingressante).DataPrevisaoConclusao ?? (x as Aluno).Historicos.FirstOrDefault(h => h.Atual).PrevisoesConclusao.OrderByDescending(p => p.Seq).FirstOrDefault().DataPrevisaoConclusao),
                NumeroIdentidade = x.DadosPessoais.NumeroIdentidade,
                ResponsaveisFinanceiros = x.Beneficios.Where(b => b.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataInicioVigencia <= dataEvento.DataInicio && dataEvento.DataInicio <= b.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataFimVigencia && b.HistoricoSituacoes.OrderByDescending(o => o.DataInicioSituacao).FirstOrDefault().SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.Deferido).SelectMany(b => b.ResponsaveisFinanceiro.Select(r => r.PessoaJuridica.NomeFantasia)).ToList(),
            });

            // Busca os dados do ingressante
            var instituicaoNiveltipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(solicitacao.SeqPessoaAtuacao);
            var exigeCurso = instituicaoNiveltipoVinculoAluno?.ExigeCurso.Value ?? true;

            // Dados de pagamento
            var informacoesFinanceiras = SolicitacaoMatriculaDomainService.BuscarInformacoesFinanceirasTermoAdesao(seqSolicitacaoMatricula, true).TransformList<InformacaoFinanceiraTermoAcademicoVO>();

            // Dados de benefícios
            List<PessoaAtuacaoBeneficioMatriculaVO> informacoesBeneficios;
            if (!gerarTermo)
            {
                informacoesBeneficios = PessoaAtuacaoBeneficioDomainService.BuscarPesssoasAtuacoesBeneficiosMatricula(solicitacao.SeqPessoaAtuacao, SituacaoChancelaBeneficio.Deferido, dataEvento.DataInicio);
            }
            else
            {
                // Este método faz exatamente a mesma coisa que o de cima, porém sem pesquisar pela data
                informacoesBeneficios = PessoaAtuacaoBeneficioDomainService.BuscarPesssoasAtuacoesBeneficiosIntegralizacao(solicitacao.SeqPessoaAtuacao, SituacaoChancelaBeneficio.Deferido);
            }

            /*Exibir os benefícios associados a pessoa-atuação. Ordená-los primeiramente pelos que posuem parâmetro por
             insituição e nível, no cadastro de configuração, com a forma de dedução "Percentual de bolsa" e depois alfabeticamente.
             *Na data fim de vigência do benefício, alterar a regra para:
            -Fim de vigência: Se o ano da data fim da vigência da associação do beneficio à pessoa - atuação for maior ou igual a "2075" exibir "-",
              caso contrário, exibir a data fim. 07/06/2019 Task 31365 */

            var listaOrdenadaBeneficio = informacoesBeneficios.OrderBy(o => o.DescricaoBeneficio).GroupBy(g => g.FormaDeducao);
            informacoesBeneficios = new List<PessoaAtuacaoBeneficioMatriculaVO>();

            foreach (var beneficio in listaOrdenadaBeneficio.Select(s => s.Where(w => w.FormaDeducao == FormaDeducao.PercentualBolsa)))
            {
                foreach (var item in beneficio)
                {
                    if (item.DataFimVigencia.Value.Year >= 2075)
                    {
                        item.DataFimVigencia = null;
                    }

                    informacoesBeneficios.Add(item);
                }
            }

            foreach (var beneficio in listaOrdenadaBeneficio.Select(s => s.Where(w => w.FormaDeducao != FormaDeducao.PercentualBolsa)))
            {
                foreach (var item in beneficio)
                {
                    if (item.DataFimVigencia.Value.Year >= 2075)
                    {
                        item.DataFimVigencia = null;
                    }

                    informacoesBeneficios.Add(item);
                }
            }

            #region [ Exibição de valores de parcela e total do curso ]
            /* 
            Task 51525
               Caso a pessoa-atuação possua um benefício associado com o parâmetro "Exibe valores no termo de adesão" igual a NÃO, exibir o valor 0,00. Caso contrário, exibir os valores conforme:
            */
            if (solicitacao.TipoAluno == TipoAluno.Calouro && informacoesBeneficios.Any(c => c.ExibeValoresTermoAdesao == false))
            {
                foreach (var infoFinanceira in informacoesFinanceiras)
                {
                    infoFinanceira.ValorParcela = 0;
                    infoFinanceira.ValorTotalCurso = 0;
                }
            }
            /*
            Task 51803
                Caso a pessoa-atuação possua um benefício associado com o parâmetro "Exibe valores no termo de adesão" igual a NÃO 
                e a integração st_dados_termo_ACADEMICO retornar o campo serviço adicional igual a não, exibir o valor 0,00. Caso contrário, 
                exibir os valores conforme retornado na st_dados_termo_ACADEMICO.             
             */
            else if (solicitacao.TipoAluno == TipoAluno.Veterano && informacoesBeneficios.Any(c => c.ExibeValoresTermoAdesao == false))
            {
                foreach (var infoFinanceira in informacoesFinanceiras)
                {
                    if (infoFinanceira.ServicoAdicional == false)
                    {
                        infoFinanceira.ValorParcela = 0;
                        infoFinanceira.ValorTotalCurso = 0;
                    }
                }
            }

            #endregion

            // Recupera o curso
            var ret = new AdesaoContratoVO
            {
                CodigoAdesao = solicitacao.CodigoAdesao,
                CPF = dadosPessoaAtuacao?.CPF,
                DataAdesao = solicitacao.DataAdesao,
                EntidadeResponsavel = solicitacao.NomeEntidadeResponsavel,
                Modalidade = dadosPessoaAtuacao.DescricaoModalidade,
                Nome = dadosPessoaAtuacao.Nome,
                OfertaCurso = dadosPessoaAtuacao.DescricaoOferta ?? "-",
                Passaporte = dadosPessoaAtuacao.Passaporte,
                Inicio = dadosPessoaAtuacao.DataAdmissao,
                TerminoPrevisto = dadosPessoaAtuacao.DataPrevisaoConclusao.GetValueOrDefault(),
                PrazoConclusao = null,
                RG = dadosPessoaAtuacao.NumeroIdentidade ?? "-",
                TermoAdesao = solicitacao?.DescricaoTermoAdesao,
                SeqArquivoContrato = solicitacao?.SeqArquivoContrato ?? 0,
                SeqArquivoTermoAdesao = solicitacao.SeqArquivoTermoAdesao,
                SeqPessoaAtuacao = solicitacao.SeqPessoaAtuacao,
                NomeContrato = solicitacao.DescricaoContrato,
                InformacoesFinanceiras = informacoesFinanceiras,
                TipoAluno = solicitacao.TipoAluno,
                NumeroRegistroAcademico = solicitacao.NumeroRegistroAcademico,
                DescricaoCicloLetivo = solicitacao.DescricaoCicloLetivo,
                InformacoesBeneficios = informacoesBeneficios,
            };

            // Recupera as informações do serviço adicional
            if (informacoesFinanceiras.Any(i => i.ServicoAdicional))
            {
                //Conversado com a Mariana ficou definido que não utilizaria as datas do evento letivo
                //Será utilizado a data de inicio da parcela e a data final será a data inicio + o numero de parcelas em meses

                // Concatena os serviços
                ret.Servicos = string.Join(", ", informacoesFinanceiras.Where(i => i.ServicoAdicional).Select(i => i.DescricaoServico) ?? new string[] { });
                ret.DataInicioServico = informacoesFinanceiras.FirstOrDefault(i => i.ServicoAdicional).DataInicioParcela;
                ret.DataFimServico = ret.DataInicioServico.AddMonths(informacoesFinanceiras.FirstOrDefault(i => i.ServicoAdicional).QuantidadeParcelas).AddDays(-1);

                //Comentado devido a conversa com a Mariana e implementado o código acima
                //// Busca os dados do evento letivo
                //var dadosEventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(solicitacao.SeqCicloLetivo.Value, solicitacao.SeqCursoOfertaLocalidadeTurno.Value, solicitacao.TipoAluno, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

                //// Preenche o retorno
                //if (dadosPessoaAtuacao.DataPrevisaoConclusao <= dadosEventoLetivo.DataInicio)
                //    ret.DataInicioServico = dadosEventoLetivo.DataInicio;
                //else
                //    ret.DataInicioServico = dadosPessoaAtuacao.DataPrevisaoConclusao.GetValueOrDefault();

                //ret.DataFimServico = dadosEventoLetivo.DataFim;
            }

            // Não exige curso. É disciplina isolada
            if (!exigeCurso)
            {
                // Buscar a modalidade do seqcursoofertalocalidadeturno
                ret.Modalidade = CursoOfertaLocalidadeTurnoDomainService.SearchProjectionByKey(new SMCSeqSpecification<CursoOfertaLocalidadeTurno>(dadosOrigem.SeqCursoOfertaLocalidadeTurno), x => x.CursoOfertaLocalidade.Modalidade.Descricao);
            }

            // Benefícios / Responsável financeiro
            ret.ResponsavelFinanceiro = string.Join(", ", dadosPessoaAtuacao.ResponsaveisFinanceiros);
            if (string.IsNullOrEmpty(ret.ResponsavelFinanceiro))
                ret.ResponsavelFinanceiro = dadosPessoaAtuacao.NomeSocial ?? dadosPessoaAtuacao.Nome;

            if (dadosPessoaAtuacao.DataPrevisaoConclusao.HasValue)
                ret.PrazoConclusao = ((ret.TerminoPrevisto.Year - ret.Inicio.Year) * 12) + (ret.TerminoPrevisto.Month - ret.Inicio.Month) + ((ret.TerminoPrevisto.Day - ret.Inicio.Day) > 0 ? 1 : 0);

            return ret;
        }


        public AdesaoContratoVO BuscarAdesaoContratoResidenciaMedica(long seqSolicitacaoMatricula, bool gerarTermo = false)
        {
            // Recupera a solicitação de matrícula
            var solicitacao = SolicitacaoMatriculaDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new
            {
                SeqCicloLetivo = x.ConfiguracaoProcesso.Processo.SeqCicloLetivo,
                TipoAluno = (x.PessoaAtuacao is Ingressante) ? TipoAluno.Calouro : TipoAluno.Veterano,
                CodAluno = (long?)(x.PessoaAtuacao as Ingressante).Seq ?? (x.PessoaAtuacao as Aluno).CodigoAlunoMigracao,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                CodigoAdesao = x.CodigoAdesao,
                NomeEntidadeResponsavel = x.EntidadeResponsavel.Nome,
                DataAdesao = x.DataAdesao,
                DescricaoTermoAdesao = x.TermoAdesao.Descricao,
                SeqArquivoTermoAdesao = x.SeqArquivoTermoAdesao,
                SeqArquivoContrato = x.TermoAdesao.Contrato.SeqArquivoAnexado,
                DescricaoContrato = x.TermoAdesao.Contrato.Descricao,
                NumeroRegistroAcademico = (long?)(x.PessoaAtuacao as Aluno).NumeroRegistroAcademico,
                DescricaoCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Descricao
            });

            // Recupera os dados de origem simulando o curso
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(solicitacao.SeqPessoaAtuacao);

            var dataEvento = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(solicitacao.SeqCicloLetivo.Value, dadosOrigem.SeqCursoOfertaLocalidadeTurno, solicitacao.TipoAluno, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

            var dadosPessoaAtuacao = PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(solicitacao.SeqPessoaAtuacao), x => new
            {
                CPF = x.Pessoa.Cpf,
                Passaporte = x.Pessoa.NumeroPassaporte,
                DescricaoModalidade = (x as Ingressante).Ofertas.FirstOrDefault().CampanhaOfertaItem.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Modalidade.Descricao ?? (x as Aluno).Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Modalidade.Descricao,
                Nome = x.DadosPessoais.Nome,
                NomeSocial = x.DadosPessoais.NomeSocial,
                DescricaoOferta = (x as Ingressante).Ofertas.FirstOrDefault().CampanhaOfertaItem.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao ?? (x as Aluno).Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                DataAdmissao = (DateTime?)(x as Ingressante).DataAdmissao ?? (x as Aluno).Historicos.FirstOrDefault(h => h.Atual).DataAdmissao,
                DataPrevisaoConclusao = (DateTime?)((x as Ingressante).DataPrevisaoConclusao ?? (x as Aluno).Historicos.FirstOrDefault(h => h.Atual).PrevisoesConclusao.OrderByDescending(p => p.Seq).FirstOrDefault().DataPrevisaoConclusao),
                NumeroIdentidade = x.DadosPessoais.NumeroIdentidade,
                ResponsaveisFinanceiros = x.Beneficios.Where(b => b.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataInicioVigencia <= dataEvento.DataInicio && dataEvento.DataInicio <= b.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataFimVigencia && b.HistoricoSituacoes.OrderByDescending(o => o.DataInicioSituacao).FirstOrDefault().SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.Deferido).SelectMany(b => b.ResponsaveisFinanceiro.Select(r => r.PessoaJuridica.NomeFantasia)).ToList(),
            });

            // Busca os dados do ingressante
            var instituicaoNiveltipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(solicitacao.SeqPessoaAtuacao);
            var exigeCurso = instituicaoNiveltipoVinculoAluno?.ExigeCurso.Value ?? true;

            // Dados de benefícios
            List<PessoaAtuacaoBeneficioMatriculaVO> informacoesBeneficios;
            if (!gerarTermo)
            {
                informacoesBeneficios = PessoaAtuacaoBeneficioDomainService.BuscarPesssoasAtuacoesBeneficiosMatricula(solicitacao.SeqPessoaAtuacao, SituacaoChancelaBeneficio.Deferido, dataEvento.DataInicio);
            }
            else
            {
                // Este método faz exatamente a mesma coisa que o de cima, porém sem pesquisar pela data
                informacoesBeneficios = PessoaAtuacaoBeneficioDomainService.BuscarPesssoasAtuacoesBeneficiosIntegralizacao(solicitacao.SeqPessoaAtuacao, SituacaoChancelaBeneficio.Deferido);
            }

            /*Exibir os benefícios associados a pessoa-atuação. Ordená-los primeiramente pelos que posuem parâmetro por
             insituição e nível, no cadastro de configuração, com a forma de dedução "Percentual de bolsa" e depois alfabeticamente.
             *Na data fim de vigência do benefício, alterar a regra para:
            -Fim de vigência: Se o ano da data fim da vigência da associação do beneficio à pessoa - atuação for maior ou igual a "2075" exibir "-",
              caso contrário, exibir a data fim. 07/06/2019 Task 31365 */

            var listaOrdenadaBeneficio = informacoesBeneficios.OrderBy(o => o.DescricaoBeneficio).GroupBy(g => g.FormaDeducao);
            informacoesBeneficios = new List<PessoaAtuacaoBeneficioMatriculaVO>();

            foreach (var beneficio in listaOrdenadaBeneficio.Select(s => s.Where(w => w.FormaDeducao == FormaDeducao.PercentualBolsa)))
            {
                foreach (var item in beneficio)
                {
                    if (item.DataFimVigencia.Value.Year >= 2075)
                    {
                        item.DataFimVigencia = null;
                    }

                    informacoesBeneficios.Add(item);
                }
            }

            foreach (var beneficio in listaOrdenadaBeneficio.Select(s => s.Where(w => w.FormaDeducao != FormaDeducao.PercentualBolsa)))
            {
                foreach (var item in beneficio)
                {
                    if (item.DataFimVigencia.Value.Year >= 2075)
                    {
                        item.DataFimVigencia = null;
                    }

                    informacoesBeneficios.Add(item);
                }
            }

            // Recupera o curso
            var ret = new AdesaoContratoVO
            {
                CodigoAdesao = solicitacao.CodigoAdesao,
                CPF = dadosPessoaAtuacao?.CPF,
                DataAdesao = solicitacao.DataAdesao,
                EntidadeResponsavel = solicitacao.NomeEntidadeResponsavel,
                Modalidade = dadosPessoaAtuacao.DescricaoModalidade,
                Nome = dadosPessoaAtuacao.Nome,
                OfertaCurso = dadosPessoaAtuacao.DescricaoOferta ?? "-",
                Passaporte = dadosPessoaAtuacao.Passaporte,
                Inicio = dadosPessoaAtuacao.DataAdmissao,
                TerminoPrevisto = dadosPessoaAtuacao.DataPrevisaoConclusao.GetValueOrDefault(),
                PrazoConclusao = null,
                RG = dadosPessoaAtuacao.NumeroIdentidade ?? "-",
                TermoAdesao = solicitacao?.DescricaoTermoAdesao,
                SeqArquivoContrato = solicitacao?.SeqArquivoContrato ?? 0,
                SeqArquivoTermoAdesao = solicitacao.SeqArquivoTermoAdesao,
                SeqPessoaAtuacao = solicitacao.SeqPessoaAtuacao,
                NomeContrato = solicitacao.DescricaoContrato,
                TipoAluno = solicitacao.TipoAluno,
                NumeroRegistroAcademico = solicitacao.NumeroRegistroAcademico,
                DescricaoCicloLetivo = solicitacao.DescricaoCicloLetivo,
            };

            // Não exige curso. É disciplina isolada
            if (!exigeCurso)
            {
                // Buscar a modalidade do seqcursoofertalocalidadeturno
                ret.Modalidade = CursoOfertaLocalidadeTurnoDomainService.SearchProjectionByKey(new SMCSeqSpecification<CursoOfertaLocalidadeTurno>(dadosOrigem.SeqCursoOfertaLocalidadeTurno), x => x.CursoOfertaLocalidade.Modalidade.Descricao);
            }


            if (dadosPessoaAtuacao.DataPrevisaoConclusao.HasValue)
                ret.PrazoConclusao = ((ret.TerminoPrevisto.Year - ret.Inicio.Year) * 12) + (ret.TerminoPrevisto.Month - ret.Inicio.Month) + ((ret.TerminoPrevisto.Day - ret.Inicio.Day) > 0 ? 1 : 0);

            return ret;
        }


        public ContratoVO BuscarContrato(long seq)
        {
            var contrato = this.SearchByKey(new SMCSeqSpecification<Contrato>(seq),
                 IncludesContrato.ArquivoAnexado
               | IncludesContrato.Cursos
               | IncludesContrato.Cursos_Curso
               | IncludesContrato.NiveisEnsino);

            var retorno = contrato.Transform<ContratoVO>();

            if (retorno.ArquivoAnexado != null)
                retorno.ArquivoAnexado.GuidFile = contrato.ArquivoAnexado.UidArquivo.ToString();

            return retorno;
        }

        public long SalvarContrato(Contrato contrato)
        {
            if (contrato.Cursos?.Count == 0 && contrato.NiveisEnsino?.Count == 0)
                throw new ContratoCursosOfertasOuNivelEnsinoNullException();
            else if (contrato.Cursos?.Count > 0 && contrato.NiveisEnsino?.Count > 0)
                throw new ContratoCursosOfertasOuNivelEnsinoException();

            if (contrato.NiveisEnsino?.Count > 0)
            {
                foreach (var item in contrato.NiveisEnsino)
                {
                    //Se um nível de ensino tiver sido informado, verificar se existe algum contrato vigente para os Níveis de Ensino
                    var specNivel = new ContratoNiveisEnsinoVigentesSpecification()
                    {
                        SeqNivelEnsino = item.SeqNivelEnsino,
                        SeqInstituicaoEnsino = contrato.SeqInstituicaoEnsino
                    };

                    var contratosComNiveisEnsino = ContratoNivelEnsinoDomainService.SearchBySpecification(specNivel, IncludesContratoNivelEnsino.Contrato).ToList();

                    if (contratosComNiveisEnsino.Count > 0)
                    {
                        foreach (var contratoNivel in contratosComNiveisEnsino)
                        {
                            // se o contrato vigente for diferente do atual valida as regras abaixo
                            if (contrato.Seq != contratoNivel.Contrato.Seq)
                            {
                                if (!contratoNivel.Contrato.DataFimValidade.HasValue)
                                {
                                    if (contrato.Seq == 0)
                                    {
                                        throw new ContratoNiveisEnsinoVigentesException(INCLUSAO);
                                    }
                                    else
                                    {
                                        throw new ContratoNiveisEnsinoVigentesException(ALTERAR);
                                    }
                                }
                                else if ((contrato.DataInicioValidade >= contratoNivel.Contrato.DataInicioValidade && contrato.DataInicioValidade <= contratoNivel.Contrato.DataFimValidade)
                                         || (contrato.DataFimValidade >= contratoNivel.Contrato.DataInicioValidade && contrato.DataFimValidade <= contratoNivel.Contrato.DataFimValidade)
                                         || (contratoNivel.Contrato.DataInicioValidade >= contrato.DataInicioValidade && contratoNivel.Contrato.DataInicioValidade <= contrato.DataFimValidade)
                                         || (contratoNivel.Contrato.DataFimValidade >= contrato.DataInicioValidade && contratoNivel.Contrato.DataFimValidade <= contrato.DataFimValidade))
                                {
                                    if (contrato.Seq == 0)
                                    {
                                        throw new ContratoNiveisEnsinoVigentesException(INCLUSAO);
                                    }
                                    else
                                    {
                                        throw new ContratoNiveisEnsinoVigentesException(ALTERAR);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (contrato.Cursos?.Count > 0)
            {
                foreach (var item in contrato.Cursos)
                {
                    //Se uma oferta de curso tiver sido informada, verificar se existe algum contrato vigente para as ofertas selecionadas.
                    var specCurso = new ContratoCursosVigentesSpecification()
                    {
                        SeqCurso = item.SeqCurso,
                        SeqInstituicaoEnsino = contrato.SeqInstituicaoEnsino
                    };
                    var contratosComCursosVigentes = ContratoCursoDomainService.SearchBySpecification(specCurso, IncludesContratoCurso.Contrato).ToList();

                    if (contratosComCursosVigentes.Count > 0)
                    {
                        foreach (var contratoCurso in contratosComCursosVigentes)
                        {
                            // se o contrato vigente for diferente do atual valida as regras abaixo
                            if (contrato.Seq != contratoCurso.Contrato.Seq)
                            {
                                if (!contratoCurso.Contrato.DataFimValidade.HasValue)
                                {
                                    if (contrato.Seq == 0)
                                    {
                                        throw new ContratoCursosVigentesException(INCLUSAO);
                                    }
                                    else
                                    {
                                        throw new ContratoCursosVigentesException(ALTERAR);
                                    }
                                }
                                else if ((contrato.DataInicioValidade >= contratoCurso.Contrato.DataInicioValidade && contrato.DataInicioValidade <= contratoCurso.Contrato.DataFimValidade)
                                         || (contrato.DataFimValidade >= contratoCurso.Contrato.DataInicioValidade && contrato.DataFimValidade <= contratoCurso.Contrato.DataFimValidade)
                                         || (contratoCurso.Contrato.DataInicioValidade >= contrato.DataInicioValidade && contratoCurso.Contrato.DataInicioValidade <= contrato.DataFimValidade)
                                         || (contratoCurso.Contrato.DataFimValidade >= contrato.DataInicioValidade && contratoCurso.Contrato.DataFimValidade <= contrato.DataFimValidade))
                                {
                                    if (contrato.Seq == 0)
                                    {
                                        throw new ContratoCursosVigentesException(INCLUSAO);
                                    }
                                    else
                                    {
                                        throw new ContratoCursosVigentesException(ALTERAR);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (contrato.Seq == 0)
                this.SaveEntity<Contrato, ContratoValidator>(contrato);
            else
            {
                var contratoUpdate = contrato.Transform<Contrato>();
                if (contrato.Cursos != null)
                    contratoUpdate.Cursos = contrato.Cursos.Select(c => new ContratoCurso() { SeqContrato = contrato.Seq, SeqCurso = c.SeqCurso }).ToList();

                if (contrato.NiveisEnsino != null)
                    contratoUpdate.NiveisEnsino = contrato.NiveisEnsino.Select(n => new ContratoNivelEnsino() { SeqContrato = contrato.Seq, SeqNivelEnsino = n.SeqNivelEnsino }).ToList();

                this.UpdateEntity(contratoUpdate);
            }

            return contrato.Seq;
        }

        public SMCPagerData<ContratoListarVO> ListarContrato(ContratoFiltroVO filtro)
        {
            var spec = filtro.Transform<ContratoFilterSpecification>(filtro);

            int total = 0;
            var contratos = this.SearchBySpecification(spec, out total,
                  IncludesContrato.ArquivoAnexado
                | IncludesContrato.Cursos
                | IncludesContrato.Cursos_Curso
                | IncludesContrato.NiveisEnsino
                | IncludesContrato.NiveisEnsino_NivelEnsino);

            var lista = new List<ContratoListarVO>();

            foreach (var item in contratos)
            {
                var vo = new ContratoListarVO();
                vo = vo.Transform<ContratoListarVO>(item);
                vo.DataFimValidade = (item.DataFimValidade == null) ? "-" : item.DataFimValidade.SMCDataAbreviada();
                vo.Cursos = item.Cursos.TransformList<ContratoCursosListarVO>();
                vo.NiveisEnsino = item.NiveisEnsino.TransformList<ContratoNiveisEnsinoVO>();
                lista.Add(vo);
            }

            return new SMCPagerData<ContratoListarVO>(lista, total);
        }
    }
}