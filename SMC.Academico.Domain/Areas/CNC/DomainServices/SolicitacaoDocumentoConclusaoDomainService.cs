using Newtonsoft.Json;
using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.CNC.Exceptions;
using SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao;
using SMC.Academico.Common.Areas.CNC.Models;
using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.Shared.Constants;
using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Exceptions.SolicitacaoServico;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Resources;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.Specifications.AtoNormativoEntidade;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.DomainServices;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.DadosMestres.ServiceContract.Areas.GED.Interfaces;
using SMC.EstruturaOrganizacional.ServiceContract.Areas.ESO.Interfaces;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Formularios.ServiceContract.Areas.FRM.Interfaces;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Repository;
using SMC.Framework.Rest;
using SMC.Framework.Security;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Data;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Interfaces;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using SMC.PDF;
using SMC.Pessoas.ServiceContract.Areas.PES.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class SolicitacaoDocumentoConclusaoDomainService : AcademicoContextDomain<SolicitacaoDocumentoConclusao>
    {
        #region Querys

        private string _inserirSolicitacaoDocumentoConclusaoPorSolicitacaoServico =
                        @" INSERT INTO CNC.solicitacao_documento_conclusao (seq_solicitacao_servico, seq_tipo_documento_academico) VALUES ({0}, {1})";

        private string QUERY_BUSCAR_CODIGO_UNIDADE_SEO_ALUNO = @"select e.cod_unidade_seo
                                                                from src.solicitacao_servico ss
                                                                inner join aln.aluno_historico ah on ah.seq_aluno_historico = ss.seq_aluno_historico
                                                                inner join cso.curso_oferta_localidade_turno colt on colt.seq_curso_oferta_localidade_turno = ah.seq_curso_oferta_localidade_turno
                                                                inner join cso.curso_oferta_localidade col on col.seq_entidade = colt.seq_entidade_curso_oferta_localidade
                                                                inner join org.hierarquia_entidade_item hi on hi.seq_entidade = col.seq_entidade
                                                                inner join org.hierarquia_entidade h on h.seq_hierarquia_entidade = hi.seq_hierarquia_entidade
                                                                inner join org.tipo_hierarquia_entidade th on th.seq_tipo_hierarquia_entidade = h.seq_tipo_hierarquia_entidade and th.idt_dom_tipo_visao = 3 --3.Visão de Localidade
                                                                inner join org.hierarquia_entidade_item hi_superior on hi_superior.seq_hierarquia_entidade_item = hi.seq_hierarquia_entidade_item_superior
                                                                inner join org.entidade e on e.seq_entidade = hi_superior.seq_entidade
                                                                inner join org.tipo_entidade te on te.seq_tipo_entidade = e.seq_tipo_entidade
                                                                where ss.seq_solicitacao_servico = @SEQ_SOLICITACAO_SERVICO";

        private string _updateDocumentoConclusao =
                        @" UPDATE CNC.documento_academico set
                                  seq_documento_gad = @seqDocumentoGad,
                                  usu_alteracao = @usuAlteracao,
                                  dat_alteracao = @datAlteracao
                           WHERE seq_documento_academico = @seqDocumentoAcademico";

        private string _updateDocumentoConclusaoHistorico =
                        @" UPDATE CNC.documento_academico set
                                  seq_documento_gad = @seqDocumentoGad,
                                  usu_alteracao = @usuAlteracao,
                                  dat_alteracao = @datAlteracao
                           WHERE seq_documento_academico = @seqDocumentoAcademico";

        private string _updateDocumentoConclusaoHistoricoNumRegistro =
                        @" UPDATE CNC.documento_conclusao set
                                  num_registro = @numRegistro
                           WHERE seq_documento_academico = @seqDocumentoAcademico";

        #endregion Querys

        #region DomainServices

        private DocumentoConclusaoFormacaoDomainService DocumentoConclusaoFormacaoDomainService => Create<DocumentoConclusaoFormacaoDomainService>();
        private ArquivoAnexadoDomainService ArquivoAnexadoDomainService => Create<ArquivoAnexadoDomainService>();
        private SolicitacaoServicoEnvioNotificacaoDomainService SolicitacaoServicoEnvioNotificacaoDomainService => Create<SolicitacaoServicoEnvioNotificacaoDomainService>();
        private NivelEnsinoDomainService NivelEnsinoDomainService => Create<NivelEnsinoDomainService>();
        private DocumentoAcademicoHistoricoSituacaoDomainService DocumentoAcademicoHistoricoSituacaoDomainService => Create<DocumentoAcademicoHistoricoSituacaoDomainService>();
        private TipoDocumentoAcademicoDomainService TipoDocumentoAcademicoDomainService => Create<TipoDocumentoAcademicoDomainService>();
        private GrauAcademicoDomainService GrauAcademicoDomainService => Create<GrauAcademicoDomainService>();
        private TipoEntidadeDomainService TipoEntidadeDomainService => Create<TipoEntidadeDomainService>();
        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();
        private HierarquiaEntidadeDomainService HierarquiaEntidadeDomainService => Create<HierarquiaEntidadeDomainService>();
        private SituacaoDocumentoAcademicoDomainService SituacaoDocumentoAcademicoDomainService => Create<SituacaoDocumentoAcademicoDomainService>();
        private TrabalhoAcademicoDomainService TrabalhoAcademicoDomainService => Create<TrabalhoAcademicoDomainService>();
        private AlunoFormacaoDomainService AlunoFormacaoDomainService => Create<AlunoFormacaoDomainService>();
        private AtoNormativoEntidadeDomainService AtoNormativoEntidadeDomainService => Create<AtoNormativoEntidadeDomainService>();
        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();
        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService => Create<FormacaoEspecificaDomainService>();
        private DocumentoConclusaoDomainService DocumentoConclusaoDomainService => Create<DocumentoConclusaoDomainService>();
        private AtoNormativoDomainService AtoNormativoDomainService => Create<AtoNormativoDomainService>();
        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService => Create<CursoOfertaLocalidadeDomainService>();
        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();
        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();
        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();
        private InstituicaoNivelDomainService InstituicaoNivelDomainService => Create<InstituicaoNivelDomainService>();
        private MotivoBloqueioDomainService MotivoBloqueioDomainService => Create<MotivoBloqueioDomainService>();
        private InstituicaoNivelTipoDocumentoAcademicoDomainService InstituicaoNivelTipoDocumentoAcademicoDomainService => Create<InstituicaoNivelTipoDocumentoAcademicoDomainService>();
        private PessoaAtuacaoBloqueioDomainService PessoaAtuacaoBloqueioDomainService => Create<PessoaAtuacaoBloqueioDomainService>();
        private TipoDocumentoAcademicoServicoDomainService TipoDocumentoAcademicoServicoDomainService => Create<TipoDocumentoAcademicoServicoDomainService>();
        private TitulacaoDomainService TitulacaoDomainService => Create<TitulacaoDomainService>();
        private CursoFormacaoEspecificaDomainService CursoFormacaoEspecificaDomainService => Create<CursoFormacaoEspecificaDomainService>();
        private CicloLetivoDomainService CicloLetivoDomainService => Create<CicloLetivoDomainService>();
        private AlunoHistoricoDomainService AlunoHistoricoDomainService => Create<AlunoHistoricoDomainService>();
        private OrgaoRegistroDomainService OrgaoRegistroDomainService => Create<OrgaoRegistroDomainService>();
        private GrupoRegistroDomainService GrupoRegistroDomainService => Create<GrupoRegistroDomainService>();
        private ModalidadeDomainService ModalidadeDomainService => Create<ModalidadeDomainService>();
        private InstituicaoEnsinoDomainService InstituicaoEnsinoDomainService => Create<InstituicaoEnsinoDomainService>();
        private MantenedoraDomainService MantenedoraDomainService => Create<MantenedoraDomainService>();
        private PessoaDomainService PessoaDomainService => Create<PessoaDomainService>();
        private TipoMensagemDomainService TipoMensagemDomainService => Create<TipoMensagemDomainService>();
        private InstituicaoNivelTipoMensagemDomainService InstituicaoNivelTipoMensagemDomainService => Create<InstituicaoNivelTipoMensagemDomainService>();
        private MensagemDomainService MensagemDomainService => Create<MensagemDomainService>();
        private DocumentoConclusaoMensagemDomainService DocumentoConclusaoMensagemDomainService => Create<DocumentoConclusaoMensagemDomainService>();
        private MensagemPessoaAtuacaoDomainService MensagemPessoaAtuacaoDomainService => Create<MensagemPessoaAtuacaoDomainService>();
        private SolicitacaoDocumentoRequeridoDomainService SolicitacaoDocumentoRequeridoDomainService => Create<SolicitacaoDocumentoRequeridoDomainService>();
        private DocumentoRequeridoDomainService DocumentoRequeridoDomainService => Create<DocumentoRequeridoDomainService>();
        private FuncionarioVinculoDomainService FuncionarioVinculoDomainService => Create<FuncionarioVinculoDomainService>();
        private TipoFuncionarioDomainService TipoFuncionarioDomainService => Create<TipoFuncionarioDomainService>();
        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();

        #endregion DomainServices

        #region Services

        private ILocalidadeService LocalidadeService => this.Create<ILocalidadeService>();
        private ITipoTemplateProcessoService TipoTemplateProcessoService => Create<ITipoTemplateProcessoService>();
        private IFormularioService FormularioService => Create<IFormularioService>();
        private ISituacaoService SituacaoService => Create<ISituacaoService>();
        private IIntegracaoAcademicoService IntegracaoAcademicoService => Create<IIntegracaoAcademicoService>();
        public IEstruturaOrganizacionalService EstruturaOrganizacionalService => Create<IEstruturaOrganizacionalService>();
        private IPessoaService PessoaService => Create<IPessoaService>();
        private ITipoDocumentoService TipoDocumentoService => Create<ITipoDocumentoService>();

        #endregion Services

        #region APIS

        public SMCApiClient APIDiplomaGAD => SMCApiClient.Create("DiplomaGAD");

        public SMCApiClient APIHistoricoGAD => SMCApiClient.Create("HistoricoGAD");

        #endregion APIS

        public void CriarBloqueioSolicitacaoDiplomaDanificado(long seqSolicitacaoServico)
        {
            /*  2.2. Se o token do tipo de serviço for igual a EMISSAO_DOCUMENTO_CONCLUSAO, consistir a regra:
                RN_SRC_006 - Solicitação - Condições ao criar solicitação de documento de conclusão*/

            /*  Se o tipo do documento solicitado está parametrizada para habilitar o registro de acordo com a
                parametrização por Instituição x Nível de Ensino e, a justificativa selecionada for igual a Danificado.
                Criar para o solicitante o seguinte bloqueio:
                 Pessoa-Atuação = identificação do aluno logado
                 Motivo-Bloqueio = 'ENTREGA_DIPLOMA_DANIFICADO'
                 Descrição-Bloqueio = 'Entrega do diploma danificado'
                 Situação-Bloqueio = Bloqueado
                 Descricao-Referencia-Atuacao = descrição da pessoa-atuação em questão
                 Cadastro-Integração = Não
                 Solicitação-Serviço = identificação da solicitação de serviço
                 Demais campos = nulo*/

            // Recupera o seq do tipo de documento de conclusão
            var dadosSolicitacaoDocumentoConclusao = this.SearchProjectionByKey(seqSolicitacaoServico, x => new
            {
                x.SeqTipoDocumentoAcademico,
                x.SeqPessoaAtuacao,
                DescricaoPessoaAtuacao = x.PessoaAtuacao.Descricao,
                TokenJustificativaSolicitacaoServico = x.JustificativaSolicitacaoServico.Token
            });

            if (dadosSolicitacaoDocumentoConclusao.TokenJustificativaSolicitacaoServico == TOKEN_JUSTIFICATIVA_SOLICITACAO_SERVICO.DOCUMENTO_CONCLUSAO_DANIFICADO)
            {
                // Recupera os dados de origem
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosSolicitacaoDocumentoConclusao.SeqPessoaAtuacao);

                // Recupera o SeqInstituicaoNivel do aluno
                var seqInstituicaoNivel = InstituicaoNivelDomainService.SearchProjectionByKey(new InstituicaoNivelFilterSpecification
                {
                    SeqInstituicaoEnsino = dadosOrigem.SeqInstituicaoEnsino,
                    SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
                    SeqCicloLetivo = dadosOrigem.SeqCicloLetivo
                }, x => x.Seq);

                var habilitarRegistroDocumento = InstituicaoNivelTipoDocumentoAcademicoDomainService.SearchProjectionByKey(new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification
                {
                    SeqInstituicaoNivel = seqInstituicaoNivel,
                    SeqTipoDocumentoAcademico = dadosSolicitacaoDocumentoConclusao.SeqTipoDocumentoAcademico,
                }, x => x.HabilitaRegistroDocumento);

                if (habilitarRegistroDocumento)
                {
                    var seqMotivoBloqueio = MotivoBloqueioDomainService.SearchProjectionByKey(new MotivoBloqueioFilterSpecification { Token = TOKEN_MOTIVO_BLOQUEIO.ENTREGA_DIPLOMA_DANIFICADO }, x => x.Seq);

                    //Cria o bloqueio
                    var novoBloqueio = new PessoaAtuacaoBloqueio()
                    {
                        SeqSolicitacaoServico = seqSolicitacaoServico,
                        SeqPessoaAtuacao = dadosSolicitacaoDocumentoConclusao.SeqPessoaAtuacao,
                        SeqMotivoBloqueio = seqMotivoBloqueio,
                        Descricao = "Entrega do diploma danificado",
                        SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                        DescricaoReferenciaAtuacao = dadosSolicitacaoDocumentoConclusao.DescricaoPessoaAtuacao,
                        CadastroIntegracao = false,
                        DataBloqueio = DateTime.Now
                    };

                    //Salva o bloqueio
                    PessoaAtuacaoBloqueioDomainService.SaveEntity(novoBloqueio);
                }
            }
        }

        /// <summary>
        /// Converte a solicitação de serviço padrão em em uma solicitação do tipo documento de conclusão
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço atual</param>
        public void CriarSolicitacaoDocumentoConclusaoPorSolicitacaoServico(long seqSolicitacaoServico, long seqServico)
        {
            var registro = this.SearchByKey(new SMCSeqSpecification<SolicitacaoDocumentoConclusao>(seqSolicitacaoServico));

            if (registro == null)
            {
                // Recupera os dados do TipoDocumentoAcademico de acordo com o serviço
                var seqTipoDocumentoAcademico = TipoDocumentoAcademicoServicoDomainService.SearchProjectionByKey(new TipoDocumentoAcademicoServicoFilterSpecification
                {
                    SeqServico = seqServico
                }, x => x.SeqTipoDocumentoAcademico);

                ExecuteSqlCommand(string.Format(_inserirSolicitacaoDocumentoConclusaoPorSolicitacaoServico, seqSolicitacaoServico, seqTipoDocumentoAcademico));
            }
        }

        public SolicitacaoAnaliseEmissaoDocumentoConclusaoVO BuscarDadosSolicitacaoDocumentoConclusao(long seqSolicitacaoServico)
        {
            // Busca todos os dados necessários para confirmação de uma emissão de documento de conclusão
            var dadosRet = this.SearchProjectionByKey(seqSolicitacaoServico, x => new SolicitacaoAnaliseEmissaoDocumentoConclusaoVO
            {
                NomeAluno = x.PessoaAtuacao.DadosPessoais.Nome,
                NomeSocial = x.PessoaAtuacao.DadosPessoais.NomeSocial,
                DataInclusaoDadosPessoais = x.PessoaAtuacao.DadosPessoais.DataInclusao,
                UsuarioInclusaoDadosPessoais = x.PessoaAtuacao.DadosPessoais.UsuarioInclusao,
                Sexo = x.PessoaAtuacao.DadosPessoais.Sexo,
                Cpf = x.PessoaAtuacao.Pessoa.Cpf,
                DataNascimento = x.PessoaAtuacao.Pessoa.DataNascimento,
                CodigoNacionalidade = x.PessoaAtuacao.Pessoa.CodigoNacionalidade,
                CodigoPaisNacionalidade = x.PessoaAtuacao.Pessoa.CodigoPaisNacionalidade,
                TipoNacionalidade = x.PessoaAtuacao.Pessoa.TipoNacionalidade,
                EstadoCivil = x.PessoaAtuacao.DadosPessoais.EstadoCivil,
                ExisteDocumentoConclusao = x.DocumentosAcademicos.Any(),
                Filiacao = x.PessoaAtuacao.Pessoa.Filiacao.Select(f => new PES.ValueObjects.PessoaFiliacaoVO
                {
                    Nome = f.Nome,
                    TipoParentesco = f.TipoParentesco
                }).ToList(),
                NumeroIdentidade = x.PessoaAtuacao.DadosPessoais.NumeroIdentidade,
                OrgaoEmissorIdentidade = x.PessoaAtuacao.DadosPessoais.OrgaoEmissorIdentidade,
                UfIdentidade = x.PessoaAtuacao.DadosPessoais.UfIdentidade,
                DataExpedicaoIdentidade = x.PessoaAtuacao.DadosPessoais.DataExpedicaoIdentidade,
                NumeroPassaporte = x.PessoaAtuacao.Pessoa.NumeroPassaporte,
                CodigoPaisEmissaoPassaporte = x.PessoaAtuacao.Pessoa.CodigoPaisEmissaoPassaporte,
                DataValidadePassaporte = x.PessoaAtuacao.Pessoa.DataValidadePassaporte,
                NumeroIdentidadeEstrangeira = x.PessoaAtuacao.DadosPessoais.NumeroIdentidadeEstrangeira,
                NumeroRegistroCnh = x.PessoaAtuacao.DadosPessoais.NumeroRegistroCnh,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                SeqPessoaAtuacaoAuxiliar = x.SeqPessoaAtuacao,
                SeqPessoa = x.PessoaAtuacao.SeqPessoa,
                SeqPessoaDadosPessoais = x.PessoaAtuacao.SeqPessoaDadosPessoais,
                SeqSolicitacaoServico = x.Seq,
                NumeroRA = x.AlunoHistorico.Aluno.NumeroRegistroAcademico,
                UfNaturalidade = x.PessoaAtuacao.DadosPessoais.UfNaturalidade,
                CodigoCidadeNaturalidade = x.PessoaAtuacao.DadosPessoais.CodigoCidadeNaturalidade ?? 0,
                DescricaoNaturalidadeEstrangeira = x.PessoaAtuacao.DadosPessoais.DescricaoNaturalidadeEstrangeira,
                SeqTipoDocumentoAcademicoAuxiliar = x.SeqTipoDocumentoAcademico,
                DescricaoTipoDocumentoAcademicoAuxiliar = x.TipoDocumentoAcademico.Descricao,
                SeqTemplateProcessoSGF = x.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token,
                TermosCotutela = x.PessoaAtuacao.TermosIntercambio.Where(t => t.TermoIntercambio.ParceriaIntercambioInstituicaoExterna != null).Select(t => new SolicitacaoAnaliseEmissaoDocumentoConclusaoTermoCotutelaVO
                {
                    SeqTermoIntercambio = t.SeqTermoIntercambio,
                    TipoParceriaIntercambio = t.TermoIntercambio.ParceriaIntercambioInstituicaoExterna.ParceriaIntercambio.TipoParceriaIntercambio,
                    SeqInstituicaoEnsino = t.TermoIntercambio.ParceriaIntercambioInstituicaoExterna.SeqInstituicaoExterna,
                    NomeInstituicaoEnsino = t.TermoIntercambio.ParceriaIntercambioInstituicaoExterna.InstituicaoExterna.Nome,
                    CodigoPaisInstituicaoExterna = t.TermoIntercambio.ParceriaIntercambioInstituicaoExterna.InstituicaoExterna.CodigoPais,
                }).Where(t => t.TipoParceriaIntercambio == Common.Areas.ALN.Enums.TipoParceriaIntercambio.Convenio).ToList(),
                CursosFormacaoEspecificaGrauAcademico = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica.Select(g => new CursoFormacaoEspecificaGrauAcademicoVO()
                {
                    SeqFormacaoEspecifica = g.SeqFormacaoEspecifica,
                    SeqGrauAcademico = g.SeqGrauAcademico,
                    DescricaoGrauAcademico = g.GrauAcademico.Descricao
                }).ToList(),
                SeqTitulacao = x.AlunoHistorico.Formacoes.OrderBy(o => o.DataInicio).FirstOrDefault().SeqTitulacao,
                DescricaoCursoDocumento = x.AlunoHistorico.Formacoes.OrderBy(o => o.DataInicio).FirstOrDefault().DescricaoDocumentoConclusao,
                DataConclusao = x.AlunoHistorico.Formacoes.OrderBy(o => o.DataInicio).FirstOrDefault(a => !a.DataFim.HasValue).DataConclusao,
                SeqCursoOfertaLocalidadeHistoricoAtual = x.AlunoHistorico.Aluno.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade,
                SeqCursoOfertaLocalidade = x.AlunoHistorico.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade,
                CodigoCursoOfertaLocalidade = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Codigo,
                DescricaoCursoOfertaLocalidade = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome,
                SeqNivelEnsino = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.SeqNivelEnsino,
                DescricaoNivelEnsino = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.NivelEnsino.Descricao,
                TokenNivelEnsino = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.NivelEnsino.Token,
                SeqCicloLetivoFormado = x.AlunoHistorico.HistoricosCicloLetivo.FirstOrDefault(a => a.AlunoHistoricoSituacao.Any(b => b.SituacaoMatricula.Token == TOKENS_SITUACAO_MATRICULA.FORMADO)).SeqCicloLetivo,
                SeqFormacaoEspecifica = x.AlunoHistorico.Formacoes.OrderBy(o => o.DataInicio).FirstOrDefault().SeqFormacaoEspecifica,
                SeqCurso = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso,
                SeqCursoOferta = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Seq,
                DescricaoCurso = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.Nome,
                DescricaoCursoOferta = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                DescricaoModalidade = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Modalidade.Descricao,
                DescricaoFormaIngresso = x.AlunoHistorico.Aluno.Historicos.FirstOrDefault(f => f.Atual).FormaIngresso.Descricao,
                TokenFormaIngresso = x.AlunoHistorico.Aluno.Historicos.FirstOrDefault(f => f.Atual).FormaIngresso.Token,
                DescricaoXSDFormaIngresso = x.AlunoHistorico.Aluno.Historicos.FirstOrDefault(f => f.Atual).FormaIngresso.DescricaoXSD,
                DataAdmissao = x.AlunoHistorico.Aluno.Historicos.FirstOrDefault(f => f.Atual).DataAdmissao,
                TipoIdentidade = x.TipoIdentidade,
                FormatoHistoricoEscolar = x.FormatoHistoricoEscolar,
                CargaHorariaCurso = x.CargaHorariaCurso,
                CargaHorariaIntegralizada = x.CargaHorariaIntegralizada,
                DataEmissaoHistorico = x.DataEmissaoHistorico,
                DataEnade = x.DataEnade,
                SituacaoEnade = x.SituacaoEnade,
                ReutilizarDados = x.ReutilizarDadosRegistro,
                DescricaoCursoFormacaoEmissao = x.DescricaoCursoFormacao,
                SeqGrauAcademicoEmissao = x.SeqGrauAcademico,
                SeqTitulacaoEmissao = x.SeqTitulacao
            });

            if (dadosRet == null)
                throw new SolicitacaoDocumentoConclusaoSemDadosException();

            dadosRet.EmissaoDiplomaDigital1Via = dadosRet.TokenServico == TOKEN_SERVICO.EMISSAO_DIPLOMA_DIGITAL_1_VIA;

            //RN_CNC_074 -Documento Conclusão -Forma e Data de Ingresso
            var retorno = BuscarFormaEDataIngressoComMenorDataAdmissao(dadosRet.SeqPessoaAtuacao, dadosRet.SeqCursoOferta);
            dadosRet.DataAdmissao = retorno.DataAdmissao;
            dadosRet.TokenFormaIngresso = retorno.Token;
            dadosRet.DescricaoFormaIngresso = retorno.Descricao;
            dadosRet.DescricaoXSDFormaIngresso = retorno.DescricaoXSD;

            dadosRet.HabilitarBotaoNovaMensagem = !dadosRet.ExisteDocumentoConclusao;

            // Recupera o código de pessoa do CAD
            dadosRet.CodigoPessoaCAD = PessoaDomainService.BuscarCodigoDePessoaNosDadosMestres(dadosRet.SeqPessoa, TipoPessoa.Fisica, null);
            dadosRet.ExibirNomeSocial = PessoaService.AutorizarNomeSocial(dadosRet.CodigoPessoaCAD);

            // Recupera os dados do aluno
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosRet.SeqPessoaAtuacao);
            dadosRet.CodigoAlunoMigracao = dadosOrigem.CodigoAlunoMigracao;
            dadosRet.SeqInstituicaoEnsino = dadosOrigem.SeqInstituicaoEnsino;

            var dadosSituacaoMatricula = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(dadosRet.SeqPessoaAtuacao);
            dadosRet.DescricaoSituacaoAtualMatricula = dadosSituacaoMatricula.Descricao;
            dadosRet.DescricaoXSDSituacaoAtualMatricula = dadosSituacaoMatricula.DescricaoXSD;
            dadosRet.TokenSituacaoAtualMatricula = dadosSituacaoMatricula.TokenSituacaoMatricula;

            var tiposHistoricoEscolar = BuscarTiposHistoricoEscolarSelect();

            var specDocumentoConclusaoAluno = new DocumentoConclusaoFilterSpecification() { SeqPessoaAtuacao = dadosRet.SeqPessoaAtuacao, GrupoDocumentoAcademico = GrupoDocumentoAcademico.Diploma };
            var documentosConclusaoAluno = this.DocumentoConclusaoDomainService.SearchBySpecification(specDocumentoConclusaoAluno, x => x.TipoDocumentoAcademico).ToList();

            if (dadosRet.TokenSituacaoAtualMatricula != TOKENS_SITUACAO_MATRICULA.FORMADO)
            {
                var tipoHistorico = tiposHistoricoEscolar.FirstOrDefault(a => a.DataAttributes.FirstOrDefault(b => b.Key == "token").Value == TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_PARCIAL);
                dadosRet.SeqTipoDocumentoSolicitado = tipoHistorico.Seq;
                dadosRet.DescricaoTipoDocumentoSolicitado = tipoHistorico.Descricao;
            }
            else if (dadosRet.TokenSituacaoAtualMatricula == TOKENS_SITUACAO_MATRICULA.FORMADO && documentosConclusaoAluno.Any())
            {
                if (documentosConclusaoAluno.Any(x => x.TipoDocumentoAcademico.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA) ||
                    documentosConclusaoAluno.Any(x => x.TipoDocumentoAcademico.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL && x.NumeroViaDocumento > 1))
                {
                    var tipoHistorico = tiposHistoricoEscolar.FirstOrDefault(a => a.DataAttributes.FirstOrDefault(b => b.Key == "token").Value == TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_2VIA);
                    dadosRet.SeqTipoDocumentoSolicitado = tipoHistorico.Seq;
                    dadosRet.DescricaoTipoDocumentoSolicitado = tipoHistorico.Descricao;
                }
                else if (documentosConclusaoAluno.Any(x => x.TipoDocumentoAcademico.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL && x.NumeroViaDocumento == 1))
                {
                    var tipoHistorico = tiposHistoricoEscolar.FirstOrDefault(a => a.DataAttributes.FirstOrDefault(b => b.Key == "token").Value == TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_FINAL);
                    dadosRet.SeqTipoDocumentoSolicitado = tipoHistorico.Seq;
                    dadosRet.DescricaoTipoDocumentoSolicitado = tipoHistorico.Descricao;
                }
            }
            else if (dadosRet.TokenSituacaoAtualMatricula == TOKENS_SITUACAO_MATRICULA.FORMADO && !documentosConclusaoAluno.Any())
            {
                var tipoHistorico = tiposHistoricoEscolar.FirstOrDefault(a => a.DataAttributes.FirstOrDefault(b => b.Key == "token").Value == TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_FINAL);
                dadosRet.SeqTipoDocumentoSolicitado = tipoHistorico.Seq;
                dadosRet.DescricaoTipoDocumentoSolicitado = tipoHistorico.Descricao;
            }

            // Recupera o seq instituicao nivel
            var seqInstituicaoNivel = InstituicaoNivelDomainService.SearchProjectionByKey(new InstituicaoNivelFilterSpecification
            {
                SeqInstituicaoEnsino = dadosOrigem.SeqInstituicaoEnsino,
                SeqNivelEnsino = dadosRet.SeqNivelEnsino,
            }, x => x.Seq);

            dadosRet.SeqInstituicaoNivel = seqInstituicaoNivel;

            //Recuperando código unidade SEO a partir da localidade
            var codigoUnidadeSeo = this.RawQuery<int?>(QUERY_BUSCAR_CODIGO_UNIDADE_SEO_ALUNO,
                                                       new SMCFuncParameter("SEQ_SOLICITACAO_SERVICO", seqSolicitacaoServico));

            if (codigoUnidadeSeo.Any(a => a != null))
                dadosRet.CodigoUnidadeSeo = codigoUnidadeSeo.FirstOrDefault();

            if (!dadosRet.SeqTipoDocumentoAcademicoAuxiliar.HasValue)
            {
                //A criação de histórico escolar não popula o tipo de documento da solicitação 
                dadosRet.SeqTipoDocumentoAcademico = dadosRet.SeqTipoDocumentoSolicitado.Value;
                dadosRet.DescricaoTipoDocumentoAcademico = dadosRet.DescricaoTipoDocumentoSolicitado;
            }
            else
            {
                dadosRet.SeqTipoDocumentoAcademico = dadosRet.SeqTipoDocumentoAcademicoAuxiliar.Value;
                dadosRet.DescricaoTipoDocumentoAcademico = dadosRet.DescricaoTipoDocumentoAcademicoAuxiliar;
            }

            var tipoDocumentoAcademico = TipoDocumentoAcademicoDomainService.SearchByKey(new SMCSeqSpecification<TipoDocumentoAcademico>(dadosRet.SeqTipoDocumentoAcademico));
            var listaTokensDiploma = new List<string> { TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA, TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL };
            var listaTokensHistorico = new List<string> { TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_PARCIAL, TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_FINAL, TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_2VIA };

            dadosRet.TokenTipoDocumentoAcademico = tipoDocumentoAcademico.Token;

            // Preenche o nome do país da emissão do passaporte
            if (dadosRet.CodigoPaisEmissaoPassaporte.GetValueOrDefault() > 0)
            {
                var dadosPaisPassaporte = LocalidadeService.BuscarPais(dadosRet.CodigoPaisEmissaoPassaporte.Value);
                dadosRet.NomePaisEmissaoPassaporte = dadosPaisPassaporte.Nome;
            }

            /* 
                NV06:
                1. Se não houver a [identificação da formação solicitada]*, então deverá ser analisada as formações que serão
                consideradas durante a emissão. Nesse caso deverão ser listadas as formações que:

                    1.1. Estão associadas ao aluno-histórico que seja o atual e o campo data-fim seja nulo e,

                    1.2. Estão associadas a outros alunos-históricos anteriores ou a outros alunos da mesma pessoa da solicitação 
                    (independente do aluno-histórico) e o campo data-fim seja nulo, mas que atenda também os seguintes critérios:

                        1.2.1. Se o parâmetro do respectivo tipo da formação "Exige grau acadêmico = Sim", então o [curso + grau + 
                        descrição-documento + titulação] devem ser iguais ao [curso + grau + descrição-documento + titulação] das formações 
                        associadas na respectiva solicitação, conforme o item 1.1.
                        Senão, o [curso + descrição-documento + titulação] da formação seja igual ao [curso +
                        descrição-documento + titulação] das formações associadas a solicitação, conforme o item 1.1.

                        1.2.2. Se o parâmetro do respectivo tipo da formação "Gera carimbo no verso do documento = Sim", 
                        então o tipo da formação também deve permitir a emissão do mesmo tipo de documento solicitado. 
                        Para considerar que o tipo é o mesmo solicitado, avaliar se a parametrização de agrupamento do tipo de documento conclusão é o mesmo.

                        1.3. O tipo da formação esteja parametrizado permitindo a emissão de documento de conclusão e do mesmo tipo de documento solicitado.
                        Para considerar que o tipo é o mesmo solicitado, avaliar se a parametrização de agrupamento do tipo de documento conclusão é o mesmo.

                2. Senão, deverão ser consideradas as formações com base na [identificação da formação solicitada]*, conforme os
                seguintes critérios:

                    2.1. Estão associadas ao respectivo aluno da solicitação independente do aluno-histórico e o campo data-fim
                    seja nulo e,

                    2.2. Estão associadas a outros alunos da mesma pessoa da solicitação independente do aluno-histórico, o
                    campo data-fim seja nulo e que o curso é o mesmo da respectiva solicitação e,

                    2.3. Se houver a identificação da descrição-curso-formação na solicitação, então a descrição-curso-oferta-documento-conclusão 
                    do aluno-formação seja igual da solicitação e,

                    2.4. Se houver a identificação do grau-acadêmico na solicitação, então o grau-acadêmico da formação seja igual da 
                    solicitação e,

                    2.5. Se houver a identificação da titulação na solicitação, então a titulação do aluno-formação seja igual da
                    solicitação e,

                    2.6. O parâmetro "Permite gerar documento conclusão" do tipo da formação está parametrizado igual a Sim e,

                    2.7. O tipo da formação esteja parametrizado permitindo a emissão do mesmo tipo de documento solicitado.
                    Para considerar que o tipo é o mesmo solicitado, avaliar se a parametrização de agrupamento do tipo de documento conclusão é o mesmo.

                [Identificação da formação solicitada]* = A identificação é feita através do preenchimento dos campos
                descrição-curso-formação, grau-acadêmico e/ou titulação na respectiva solicitação-documento-conclusão.
            */

            dadosRet.FormacoesEspecificasConcluidas = new List<SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaVO>();
            if (string.IsNullOrEmpty(dadosRet.DescricaoCursoFormacaoEmissao) && !dadosRet.SeqGrauAcademicoEmissao.HasValue && !dadosRet.SeqTitulacaoEmissao.HasValue)
            {
                //FORMAÇÕES ALUNO HISTÓRICO ATUAL
                var seqAlunoHistoricoAtual = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(dadosRet.SeqPessoaAtuacao), x => x.Historicos.Where(b => b.Atual).FirstOrDefault().Seq);
                var alunoHistoricoAtual = AlunoHistoricoDomainService.SearchByKey(new SMCSeqSpecification<AlunoHistorico>(seqAlunoHistoricoAtual),
                    a => a.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.NivelEnsino,
                    b => b.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Modalidade,
                    c => c.Formacoes[0].ApuracoesFormacao,
                    d => d.Formacoes[0].FormacaoEspecifica.TipoFormacaoEspecifica,
                    e => e.Formacoes[0].Titulacao);

                var seqCursoAlunoHistoricoAtual = alunoHistoricoAtual.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso;

                var nivelEnsinoHistoricoAtual = alunoHistoricoAtual.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.NivelEnsino;

                var formacaoAlunoHistoricoAtual = alunoHistoricoAtual.Formacoes.OrderBy(o => o.DataInicio).FirstOrDefault(a => !a.DataFim.HasValue);
                var seqFormacaoEspecificaAlunoHistoricoAtual = formacaoAlunoHistoricoAtual?.SeqFormacaoEspecifica;
                var specCursoFormacaoEspecificaAlunoHistoricoAtual = new CursoFormacaoEspecificaFilterSpecification() { SeqCurso = seqCursoAlunoHistoricoAtual, SeqFormacaoEspecifica = seqFormacaoEspecificaAlunoHistoricoAtual };
                var cursoFormacaoAlunoHistoricoAtual = this.CursoFormacaoEspecificaDomainService.SearchByKey(specCursoFormacaoEspecificaAlunoHistoricoAtual, x => x.GrauAcademico);
                var seqGrauAcademicoAlunoHistoricoAtual = cursoFormacaoAlunoHistoricoAtual?.SeqGrauAcademico;

                var descricaoDocumentoConclusaoAlunoHistoricoAtual = formacaoAlunoHistoricoAtual?.DescricaoDocumentoConclusao;
                var seqTitulacaoAlunoHistoricoAtual = formacaoAlunoHistoricoAtual?.SeqTitulacao;

                //Preenchendo os dados da solicitação 
                dadosRet.SeqCurso = seqCursoAlunoHistoricoAtual;
                dadosRet.DescricaoCurso = alunoHistoricoAtual.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.Nome;

                dadosRet.SeqNivelEnsino = nivelEnsinoHistoricoAtual.Seq;
                dadosRet.DescricaoNivelEnsino = nivelEnsinoHistoricoAtual.Descricao;
                dadosRet.TokenNivelEnsino = nivelEnsinoHistoricoAtual.Token;

                dadosRet.DescricaoModalidade = alunoHistoricoAtual.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Modalidade.Descricao;

                dadosRet.SeqGrauAcademicoSelecionado = seqGrauAcademicoAlunoHistoricoAtual;
                if (dadosRet.SeqGrauAcademicoSelecionado.HasValue)
                {
                    dadosRet.DescricaoGrauAcademicoSelecionado = cursoFormacaoAlunoHistoricoAtual?.GrauAcademico?.Descricao;

                    dadosRet.TiposGrauAcademico = new List<SMCDatasourceItem>();
                    var dados = new SMCDatasourceItem { Seq = seqGrauAcademicoAlunoHistoricoAtual.Value, Descricao = cursoFormacaoAlunoHistoricoAtual?.GrauAcademico?.Descricao };
                    dadosRet.TiposGrauAcademico.Add(dados);
                }

                dadosRet.DescricaoCursoDocumento = descricaoDocumentoConclusaoAlunoHistoricoAtual?.Trim();

                if (seqTitulacaoAlunoHistoricoAtual.GetValueOrDefault() > 0)
                {
                    var titulacao = TitulacaoDomainService.BuscarTitulacao(seqTitulacaoAlunoHistoricoAtual.Value);
                    var titulacaoSexoAluno = dadosRet.Sexo == Sexo.Masculino ? titulacao.DescricaoMasculino : titulacao.DescricaoFeminino;

                    dadosRet.SeqTitulacao = seqTitulacaoAlunoHistoricoAtual.Value;
                    dadosRet.DescricaoTitulacao = !string.IsNullOrEmpty(titulacao.DescricaoXSD) ? titulacao.DescricaoXSD : titulacaoSexoAluno;
                }

                var formacoesAlunoHistoricoAtualParaVerificar = alunoHistoricoAtual.Formacoes.Where(a => !a.DataFim.HasValue).ToList();
                foreach (var alunoFormacao in formacoesAlunoHistoricoAtualParaVerificar)
                {
                    var apuracaoFormacao = alunoFormacao.ApuracoesFormacao.OrderByDescending(a => a.Seq).FirstOrDefault(a => !a.DataExclusao.HasValue && a.DataInicio.Date >= alunoFormacao.DataInicio.Date);
                    var situacaoAlunoFormacao = (SituacaoAlunoFormacao?)apuracaoFormacao.SituacaoAlunoFormacao;
                    var dataFormacao = (DateTime?)apuracaoFormacao.DataInicio;

                    var tipoFormacaoEspecifica = alunoFormacao.FormacaoEspecifica.TipoFormacaoEspecifica;

                    var specInstituicaoNivelTipoDocumentoAcademico = new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification()
                    {
                        SeqInstituicaoNivel = seqInstituicaoNivel,
                        SeqsTipoFormacaoEspecifica = new List<long>() { tipoFormacaoEspecifica.Seq }
                    };

                    bool tipoFormacaoPermiteTipoDocumentoSolicitado = PermitirTipoDocumentoSolicitado(specInstituicaoNivelTipoDocumentoAcademico, tipoDocumentoAcademico.Seq, tipoDocumentoAcademico.GrupoDocumentoAcademico);

                    if (tipoFormacaoEspecifica.PermiteEmitirDocumentoConclusao && tipoFormacaoPermiteTipoDocumentoSolicitado)
                    {
                        dadosRet.FormacoesEspecificasConcluidas.Add(new SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaVO
                        {
                            Data = alunoFormacao.DataInicio,
                            SituacaoAlunoFormacao = situacaoAlunoFormacao,
                            SeqFormacaoEspecifica = alunoFormacao.SeqFormacaoEspecifica,
                            DescricaoDocumentoConclusao = alunoFormacao.DescricaoDocumentoConclusao,
                            SeqTitulacao = alunoFormacao.SeqTitulacao,
                            GeraCarimbo = tipoFormacaoEspecifica.GeraCarimbo,
                            SeqEntidadeResponsavel = alunoFormacao.FormacaoEspecifica.SeqEntidadeResponsavel,
                            DataInicio = alunoFormacao.DataInicio,
                            DataFim = alunoFormacao.DataFim,
                            ExigeGrau = tipoFormacaoEspecifica.ExigeGrau,
                            SeqTipoFormacaoEspecifica = alunoFormacao.FormacaoEspecifica.SeqTipoFormacaoEspecifica,
                            PermiteEmitirDocumentoConclusao = tipoFormacaoEspecifica.PermiteEmitirDocumentoConclusao,
                            SeqAlunoFormacao = alunoFormacao.Seq,
                            TokenTipoFormacaoEspecifica = tipoFormacaoEspecifica.Token,
                            SeqFormacaoEspecificaSuperior = alunoFormacao.FormacaoEspecifica.SeqFormacaoEspecificaSuperior,
                            DescricaoTipoFormacaoEspecifica = tipoFormacaoEspecifica.Descricao,
                            DataFormacao = dataFormacao,
                            NumeroRA = dadosRet.NumeroRA,
                            DescricaoTitulacao = dadosRet.Sexo == Sexo.Masculino ? alunoFormacao.Titulacao.DescricaoMasculino : alunoFormacao.Titulacao.DescricaoFeminino,
                            DataColacaoGrau = alunoFormacao.DataColacaoGrau,
                            DataConclusao = alunoFormacao.DataConclusao
                        });
                    }
                }

                if (formacaoAlunoHistoricoAtual != null)
                {
                    //FORMAÇÕES ASSOCIADAS A OUTROS ALUNOS HISTÓRICOS ANTERIORES
                    if (alunoHistoricoAtual.SeqAlunoHistoricoAnterior.HasValue)
                    {
                        dadosRet.FormacoesEspecificasConcluidas.AddRange(RetornaFormacoesAlunoHistoricosAnteriores(alunoHistoricoAtual.SeqAlunoHistoricoAnterior.Value, seqCursoAlunoHistoricoAtual, seqGrauAcademicoAlunoHistoricoAtual, descricaoDocumentoConclusaoAlunoHistoricoAtual, seqTitulacaoAlunoHistoricoAtual, seqInstituicaoNivel, listaTokensDiploma, tipoDocumentoAcademico, dadosRet.NumeroRA, dadosRet.Sexo, dadosRet.DataConclusao));
                    }

                    //FORMAÇÕES DE OUTROS ALUNOS DA MESMA PESSOA DA SOLICITAÇÃO (INDEPENDENTE DO ALUNO HISTÓRICO)
                    var pessoasAtuacao = PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(dadosRet.SeqPessoaAtuacao),
                       x => x.Pessoa.Atuacoes);

                    var seqsDemaisPessoasAtuacao = pessoasAtuacao.Where(a => a.Seq != dadosRet.SeqPessoaAtuacao).Select(a => a.Seq).ToList();

                    var alunosHistoricoDemaisPessoasAtuacao = new List<AlunoHistorico>();

                    if (seqsDemaisPessoasAtuacao.Any())
                    {
                        alunosHistoricoDemaisPessoasAtuacao = AlunoHistoricoDomainService.SearchBySpecification(new AlunoHistoricoFilterSpecification() { SeqsPessoaAtuacao = seqsDemaisPessoasAtuacao },
                            w => w.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso,
                            x => x.Formacoes[0].ApuracoesFormacao,
                            y => y.Formacoes[0].FormacaoEspecifica.TipoFormacaoEspecifica,
                            z => z.Formacoes[0].Titulacao).ToList();
                    }

                    foreach (var alunoHistorico in alunosHistoricoDemaisPessoasAtuacao)
                    {
                        var formacoesAlunoHistoricoDemaisPessoasAtuacaoParaVerificar = alunoHistorico.Formacoes.Where(a => !a.DataFim.HasValue).ToList();
                        foreach (var alunoFormacao in formacoesAlunoHistoricoDemaisPessoasAtuacaoParaVerificar)
                        {
                            var passouCondicoesInclusaoFormacao = true;

                            //1.2.1
                            if (alunoFormacao.FormacaoEspecifica.TipoFormacaoEspecifica.ExigeGrau)
                            {
                                var seqCursoAlunoFormacao = alunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso;

                                var specCursoFormacaoEspecificaAlunoFormacao = new CursoFormacaoEspecificaFilterSpecification() { SeqCurso = seqCursoAlunoFormacao, SeqFormacaoEspecifica = alunoFormacao.SeqFormacaoEspecifica };
                                var seqGrauAcademicoAlunoFormacao = this.CursoFormacaoEspecificaDomainService.SearchProjectionByKey(specCursoFormacaoEspecificaAlunoFormacao, x => x.SeqGrauAcademico);

                                if (seqCursoAlunoHistoricoAtual != seqCursoAlunoFormacao)
                                    passouCondicoesInclusaoFormacao = false;
                                else if (seqGrauAcademicoAlunoHistoricoAtual != seqGrauAcademicoAlunoFormacao)
                                    passouCondicoesInclusaoFormacao = false;
                                else if (descricaoDocumentoConclusaoAlunoHistoricoAtual.ToLower().Trim() != alunoFormacao.DescricaoDocumentoConclusao.ToLower().Trim())
                                    passouCondicoesInclusaoFormacao = false;
                                else if (seqTitulacaoAlunoHistoricoAtual != alunoFormacao.SeqTitulacao)
                                    passouCondicoesInclusaoFormacao = false;
                            }
                            else
                            {
                                var seqCursoAlunoFormacao = alunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso;

                                if (seqCursoAlunoHistoricoAtual != seqCursoAlunoFormacao)
                                    passouCondicoesInclusaoFormacao = false;
                                else if (descricaoDocumentoConclusaoAlunoHistoricoAtual.ToLower().Trim() != alunoFormacao.DescricaoDocumentoConclusao.ToLower().Trim())
                                    passouCondicoesInclusaoFormacao = false;
                                else if (seqTitulacaoAlunoHistoricoAtual != alunoFormacao.SeqTitulacao)
                                    passouCondicoesInclusaoFormacao = false;
                            }

                            //1.2.2
                            if (alunoFormacao.FormacaoEspecifica.TipoFormacaoEspecifica.GeraCarimbo.GetValueOrDefault())
                            {
                                var specInstituicaoNivelTipoDocumentoAcademico = new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification()
                                {
                                    SeqInstituicaoNivel = seqInstituicaoNivel,
                                    SeqsTipoFormacaoEspecifica = new List<long>() { alunoFormacao.FormacaoEspecifica.SeqTipoFormacaoEspecifica }
                                };

                                bool tipoFormacaoPermiteTipoDocumentoSolicitado = PermitirTipoDocumentoSolicitado(specInstituicaoNivelTipoDocumentoAcademico, tipoDocumentoAcademico.Seq, tipoDocumentoAcademico.GrupoDocumentoAcademico);

                                if (!tipoFormacaoPermiteTipoDocumentoSolicitado)
                                    passouCondicoesInclusaoFormacao = false;
                            }

                            if (passouCondicoesInclusaoFormacao)
                            {
                                var apuracaoFormacao = alunoFormacao.ApuracoesFormacao.OrderByDescending(a => a.DataInicio).FirstOrDefault(a => !a.DataExclusao.HasValue && a.DataInicio >= alunoFormacao.DataInicio);
                                var situacaoAlunoFormacao = (SituacaoAlunoFormacao?)apuracaoFormacao.SituacaoAlunoFormacao;
                                var dataFormacao = (DateTime?)apuracaoFormacao.DataInicio;
                                var tipoFormacaoEspecifica = alunoFormacao.FormacaoEspecifica.TipoFormacaoEspecifica;

                                dadosRet.FormacoesEspecificasConcluidas.Add(new SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaVO
                                {
                                    Data = alunoFormacao.DataInicio,
                                    SituacaoAlunoFormacao = situacaoAlunoFormacao,
                                    SeqFormacaoEspecifica = alunoFormacao.SeqFormacaoEspecifica,
                                    DescricaoDocumentoConclusao = alunoFormacao.DescricaoDocumentoConclusao,
                                    SeqTitulacao = alunoFormacao.SeqTitulacao,
                                    GeraCarimbo = tipoFormacaoEspecifica.GeraCarimbo,
                                    SeqEntidadeResponsavel = alunoFormacao.FormacaoEspecifica.SeqEntidadeResponsavel,
                                    DataInicio = alunoFormacao.DataInicio,
                                    DataFim = alunoFormacao.DataFim,
                                    ExigeGrau = tipoFormacaoEspecifica.ExigeGrau,
                                    SeqTipoFormacaoEspecifica = alunoFormacao.FormacaoEspecifica.SeqTipoFormacaoEspecifica,
                                    PermiteEmitirDocumentoConclusao = tipoFormacaoEspecifica.PermiteEmitirDocumentoConclusao,
                                    SeqAlunoFormacao = alunoFormacao.Seq,
                                    TokenTipoFormacaoEspecifica = tipoFormacaoEspecifica.Token,
                                    SeqFormacaoEspecificaSuperior = alunoFormacao.FormacaoEspecifica.SeqFormacaoEspecificaSuperior,
                                    DescricaoTipoFormacaoEspecifica = tipoFormacaoEspecifica.Descricao,
                                    DataFormacao = dataFormacao,
                                    NumeroRA = dadosRet.NumeroRA,
                                    DescricaoTitulacao = dadosRet.Sexo == Sexo.Masculino ? alunoFormacao.Titulacao.DescricaoMasculino : alunoFormacao.Titulacao.DescricaoFeminino,
                                    DataColacaoGrau = alunoFormacao.DataColacaoGrau,
                                    DataConclusao = alunoFormacao.DataConclusao
                                });
                            }
                        }
                    }
                }
            }
            else
            {
                var alunosHistoricoPessoaAtuacaoSolicitacao = AlunoHistoricoDomainService.SearchBySpecification(new AlunoHistoricoFilterSpecification() { SeqsPessoaAtuacao = new List<long>() { dadosRet.SeqPessoaAtuacao } },
                    a => a.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.NivelEnsino,
                    b => b.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Modalidade,
                    c => c.Formacoes[0].ApuracoesFormacao,
                    d => d.Formacoes[0].FormacaoEspecifica.TipoFormacaoEspecifica,
                    e => e.Formacoes[0].Titulacao).ToList();

                var pessoasAtuacao = PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(dadosRet.SeqPessoaAtuacao),
                     x => x.Pessoa.Atuacoes).ToList();

                var seqsDemaisPessoasAtuacao = pessoasAtuacao.Where(a => a.Seq != dadosRet.SeqPessoaAtuacao).Select(a => a.Seq).ToList();

                var alunosHistoricoDemaisPessoasAtuacao = new List<AlunoHistorico>();

                if (seqsDemaisPessoasAtuacao.Any())
                {
                    alunosHistoricoDemaisPessoasAtuacao = AlunoHistoricoDomainService.SearchBySpecification(new AlunoHistoricoFilterSpecification() { SeqsPessoaAtuacao = seqsDemaisPessoasAtuacao, SeqCurso = dadosRet.SeqCurso },
                       v => v.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.NivelEnsino,
                       w => w.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Modalidade,
                       x => x.Formacoes[0].ApuracoesFormacao,
                       y => y.Formacoes[0].FormacaoEspecifica.TipoFormacaoEspecifica,
                       z => z.Formacoes[0].Titulacao).ToList();
                }

                List<AlunoHistorico> alunosHistoricoParaVerificar = alunosHistoricoPessoaAtuacaoSolicitacao;
                alunosHistoricoParaVerificar.AddRange(alunosHistoricoDemaisPessoasAtuacao);

                foreach (var alunoHistorico in alunosHistoricoParaVerificar)
                {
                    var formacoesParaVerificar = alunoHistorico.Formacoes.Where(a => !a.DataFim.HasValue).ToList();
                    foreach (var alunoFormacao in formacoesParaVerificar)
                    {
                        var passouCondicoesInclusaoFormacao = true;
                        if (!string.IsNullOrEmpty(dadosRet.DescricaoCursoFormacaoEmissao))
                        {
                            if (alunoFormacao.DescricaoDocumentoConclusao.ToLower().Trim() != dadosRet.DescricaoCursoFormacaoEmissao.ToLower().Trim())
                                passouCondicoesInclusaoFormacao = false;
                        }

                        var seqCurso = alunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso;

                        var seqFormacaoEspecifica = alunoFormacao.SeqFormacaoEspecifica;
                        var specCursoFormacaoEspecifica = new CursoFormacaoEspecificaFilterSpecification() { SeqCurso = seqCurso, SeqFormacaoEspecifica = seqFormacaoEspecifica };
                        var cursoFormacaoEspecifica = this.CursoFormacaoEspecificaDomainService.SearchByKey(specCursoFormacaoEspecifica, x => x.GrauAcademico);
                        var seqGrauAcademico = cursoFormacaoEspecifica?.SeqGrauAcademico;

                        if (dadosRet.SeqGrauAcademicoEmissao.HasValue)
                        {
                            if (seqGrauAcademico != dadosRet.SeqGrauAcademicoEmissao.Value)
                                passouCondicoesInclusaoFormacao = false;
                        }

                        if (dadosRet.SeqTitulacaoEmissao.HasValue)
                        {
                            if (alunoFormacao.SeqTitulacao != dadosRet.SeqTitulacaoEmissao.Value)
                                passouCondicoesInclusaoFormacao = false;
                        }

                        var tipoFormacaoEspecifica = alunoFormacao.FormacaoEspecifica.TipoFormacaoEspecifica;

                        if (!tipoFormacaoEspecifica.PermiteEmitirDocumentoConclusao)
                            passouCondicoesInclusaoFormacao = false;

                        var specInstituicaoNivelTipoDocumentoAcademico = new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification()
                        {
                            SeqInstituicaoNivel = seqInstituicaoNivel,
                            SeqsTipoFormacaoEspecifica = new List<long>() { alunoFormacao.FormacaoEspecifica.SeqTipoFormacaoEspecifica }
                        };

                        bool tipoFormacaoPermiteTipoDocumentoSolicitado = PermitirTipoDocumentoSolicitado(specInstituicaoNivelTipoDocumentoAcademico, tipoDocumentoAcademico.Seq, tipoDocumentoAcademico.GrupoDocumentoAcademico);

                        //Comentando a ordenação pelo SeqApuracaoFormacao porque pode ter retroativo, por exemplo:
                        //seq_apuracao_formacao	/ seq_aluno_formacao / idt_dom_situacao_aluno_formacao / dat_inicio	
                        //18400	12984 / 1 / 2017-02-06 18:51:00.000	
                        //18401	12984 / 4 / 2021-10-14 00:00:00.000		
                        //18402	12984 / 6 / 2021-07-16 21:07:00.000
                        //var apuracaoFormacao = alunoFormacao.ApuracoesFormacao.OrderByDescending(a => a.Seq).FirstOrDefault(a => !a.DataExclusao.HasValue && a.DataInicio.Date >= alunoFormacao.DataInicio.Date && (!alunoFormacao.DataFim.HasValue || a.DataInicio.Date <= alunoFormacao.DataFim.Value.Date));

                        var apuracaoFormacao = alunoFormacao.ApuracoesFormacao.OrderByDescending(a => a.DataInicio).FirstOrDefault(a => !a.DataExclusao.HasValue && a.DataInicio.Date >= alunoFormacao.DataInicio.Date);
                        var situacaoAlunoFormacao = (SituacaoAlunoFormacao?)apuracaoFormacao.SituacaoAlunoFormacao;
                        var dataFormacao = (DateTime?)apuracaoFormacao.DataInicio;

                        if (passouCondicoesInclusaoFormacao && tipoFormacaoEspecifica.PermiteEmitirDocumentoConclusao && tipoFormacaoPermiteTipoDocumentoSolicitado)
                        {
                            dadosRet.FormacoesEspecificasConcluidas.Add(new SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaVO
                            {
                                Data = alunoFormacao.DataInicio,
                                SituacaoAlunoFormacao = situacaoAlunoFormacao,
                                SeqFormacaoEspecifica = alunoFormacao.SeqFormacaoEspecifica,
                                DescricaoDocumentoConclusao = alunoFormacao.DescricaoDocumentoConclusao,
                                SeqTitulacao = alunoFormacao.SeqTitulacao,
                                GeraCarimbo = tipoFormacaoEspecifica.GeraCarimbo,
                                SeqEntidadeResponsavel = alunoFormacao.FormacaoEspecifica.SeqEntidadeResponsavel,
                                DataInicio = alunoFormacao.DataInicio,
                                DataFim = alunoFormacao.DataFim,
                                ExigeGrau = tipoFormacaoEspecifica.ExigeGrau,
                                SeqTipoFormacaoEspecifica = alunoFormacao.FormacaoEspecifica.SeqTipoFormacaoEspecifica,
                                PermiteEmitirDocumentoConclusao = tipoFormacaoEspecifica.PermiteEmitirDocumentoConclusao,
                                SeqAlunoFormacao = alunoFormacao.Seq,
                                TokenTipoFormacaoEspecifica = tipoFormacaoEspecifica.Token,
                                SeqFormacaoEspecificaSuperior = alunoFormacao.FormacaoEspecifica.SeqFormacaoEspecificaSuperior,
                                DescricaoTipoFormacaoEspecifica = tipoFormacaoEspecifica.Descricao,
                                DataFormacao = dataFormacao,
                                NumeroRA = dadosRet.NumeroRA,
                                DescricaoTitulacao = dadosRet.Sexo == Sexo.Masculino ? alunoFormacao.Titulacao.DescricaoMasculino : alunoFormacao.Titulacao.DescricaoFeminino,
                                DataColacaoGrau = alunoFormacao.DataColacaoGrau,
                                DataConclusao = alunoFormacao.DataConclusao
                            });

                            //Preenchendo os dados da solicitação 
                            var nivelEnsino = alunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.NivelEnsino;

                            dadosRet.SeqCurso = seqCurso;
                            dadosRet.DescricaoCurso = alunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.Nome;

                            dadosRet.SeqNivelEnsino = nivelEnsino.Seq;
                            dadosRet.DescricaoNivelEnsino = nivelEnsino.Descricao;
                            dadosRet.TokenNivelEnsino = nivelEnsino.Token;

                            dadosRet.DescricaoModalidade = alunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Modalidade.Descricao;

                            dadosRet.SeqGrauAcademicoSelecionado = seqGrauAcademico;
                            if (dadosRet.SeqGrauAcademicoSelecionado.HasValue)
                            {
                                dadosRet.DescricaoGrauAcademicoSelecionado = cursoFormacaoEspecifica?.GrauAcademico.Descricao;

                                dadosRet.TiposGrauAcademico = new List<SMCDatasourceItem>();
                                var dados = new SMCDatasourceItem { Seq = seqGrauAcademico.Value, Descricao = cursoFormacaoEspecifica?.GrauAcademico.Descricao };
                                dadosRet.TiposGrauAcademico.Add(dados);
                            }

                            dadosRet.DescricaoCursoDocumento = alunoFormacao.DescricaoDocumentoConclusao?.Trim();

                            if (alunoFormacao.SeqTitulacao.GetValueOrDefault() > 0)
                            {
                                var titulacao = TitulacaoDomainService.BuscarTitulacao(alunoFormacao.SeqTitulacao.Value);
                                var titulacaoSexoAluno = dadosRet.Sexo == Sexo.Masculino ? titulacao.DescricaoMasculino : titulacao.DescricaoFeminino;

                                dadosRet.SeqTitulacao = alunoFormacao.SeqTitulacao.Value;
                                dadosRet.DescricaoTitulacao = !string.IsNullOrEmpty(titulacao.DescricaoXSD) ? titulacao.DescricaoXSD : titulacaoSexoAluno;
                            }
                        }
                    }
                }
            }

            var seqsAreaConcentracaoFormacoesDocumento = new List<long>();

            if (dadosRet.FormacoesEspecificasConcluidas != null && dadosRet.FormacoesEspecificasConcluidas.Any())
            {
                dadosRet.ExisteFormacaoAssociada = true;

                // Formata a descrição das formações específicas do documento e adiciona os seqs das formações do tipo área de concentração
                foreach (var item in dadosRet.FormacoesEspecificasConcluidas)
                {
                    var descricaoFormacao = FormacaoEspecificaDomainService.BuscarDescricaoFormacaoEspecifica(item.SeqFormacaoEspecifica, item.SeqEntidadeResponsavel, true);

                    if (descricaoFormacao != null && descricaoFormacao.Any())
                    {
                        item.HierarquiaFormacao = descricaoFormacao;
                        item.DescricaoFormacaoEspecifica = descricaoFormacao.FirstOrDefault(d => d.SeqFormacaoEspecifica == item.SeqFormacaoEspecifica).DescricaoFormacaoEspecifica;
                        item.DescricoesFormacaoEspecifica = descricaoFormacao.Select(d => d.DescricaoFormacaoEspecifica).ToList();

                        // Adiciona os seqs da area de concentração das formações do documento para filtrar os documentos anteriores
                        if (descricaoFormacao.Any(d => d.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_CONCENTRACAO))
                            seqsAreaConcentracaoFormacoesDocumento.Add(descricaoFormacao.FirstOrDefault(d => d.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_CONCENTRACAO).SeqFormacaoEspecifica);
                    }
                    else
                    {
                        var descricaoFormacaoEspecifica = FormacaoEspecificaDomainService.BuscarFormacoesEspecificasHierarquia(new long[] { item.SeqFormacaoEspecifica });
                        var hierarquiasFormacao = descricaoFormacaoEspecifica.SelectMany(a => a.Hierarquia).ToList();
                        item.DescricoesFormacaoEspecifica = hierarquiasFormacao.Select(a => $"[{a.DescricaoTipoFormacaoEspecifica}] {a.Descricao}").ToList();

                        // Adiciona os seqs da area de concentração das formações do documento para filtrar os documentos anteriores
                        if (hierarquiasFormacao.Any(d => d.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_CONCENTRACAO))
                            seqsAreaConcentracaoFormacoesDocumento.Add(hierarquiasFormacao.FirstOrDefault(d => d.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_CONCENTRACAO).Seq);
                    }
                }
            }
            else
            {
                dadosRet.ExisteFormacaoAssociada = false;

                var dadosAluno = this.SearchProjectionByKey(seqSolicitacaoServico, x => new
                {
                    DescricaoModalidade = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Modalidade.Descricao,
                    DescricaoCursoDocumento = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.DescricaoDocumentoConclusao,
                    DescricoesGrauAcademico = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.FormacoesEspecificas.SelectMany(sm => sm.FormacaoEspecifica.Cursos).Select(s => new { Descricao = s.GrauAcademico.Descricao, Seq = (long?)s.GrauAcademico.Seq }),
                    NomeCurso = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.Nome,
                    DescricaoNivelEnsino = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.NivelEnsino.Descricao
                });

                dadosRet.DescricaoModalidade = dadosAluno.DescricaoModalidade;
                dadosRet.DescricaoCursoDocumento = dadosAluno.DescricaoCursoDocumento;

                var listaGrauAcademico = dadosAluno.DescricoesGrauAcademico.Where(g => g.Seq.HasValue).Distinct().ToList();
                if (listaGrauAcademico != null && listaGrauAcademico.Any())
                {
                    dadosRet.TiposGrauAcademico = new List<SMCDatasourceItem>();
                    foreach (var grauAcademico in listaGrauAcademico)
                    {
                        var dados = new SMCDatasourceItem { Seq = grauAcademico.Seq.Value, Descricao = grauAcademico.Descricao };
                        dadosRet.TiposGrauAcademico.Add(dados);
                    }

                    dadosRet.SeqGrauAcademicoSelecionado = dadosRet.TiposGrauAcademico.Count() == 1 ? dadosRet.TiposGrauAcademico.FirstOrDefault().Seq : 0;
                    dadosRet.DescricaoGrauAcademicoSelecionado = dadosRet.TiposGrauAcademico.Count() == 1 ? dadosRet.TiposGrauAcademico.FirstOrDefault().Descricao : string.Empty;
                }

                dadosRet.DescricaoCurso = dadosAluno.NomeCurso;
                dadosRet.DescricaoNivelEnsino = dadosAluno.DescricaoNivelEnsino;
            }

            // Formações que permitem emitir documento
            var formacoesFormadasPermitemEmitirDocumento = dadosRet.FormacoesEspecificasConcluidas.Where(f => f.PermiteEmitirDocumentoConclusao).ToList();

            if (!dadosRet.ExisteDocumentoConclusao)
            {
                //Se for inserção
                dadosRet.CamposReadOnly = false;
                //O valor default do campo reutilizar é Não, independente se exibir ou não a seção de dados do registro (ExibirSecaoDadosRegistro)
                dadosRet.ReutilizarDados = false;

                //Se for nacionalidade brasileira desabilita e seta o valor RG
                //Se não for nacionalidade brasileira não desabilita e deixa o usuário selecionar
                if (dadosRet.TipoNacionalidade == TipoNacionalidade.Brasileira)
                {
                    dadosRet.DesabilitarTipoIdentidade = true;
                    dadosRet.TipoIdentidade = TipoIdentidade.RG;
                }
                else
                {
                    dadosRet.DesabilitarTipoIdentidade = false;
                    dadosRet.TipoIdentidade = null;
                }
            }
            else
            {
                //Se for edição
                dadosRet.CamposReadOnly = true;

                //Se for nacionalidade brasileira desabilita e seta o valor RG
                //Se não for nacionalidade brasileira não desabilita e seta o valor que está no banco
                if (dadosRet.TipoNacionalidade == TipoNacionalidade.Brasileira)
                {
                    dadosRet.DesabilitarTipoIdentidade = true;
                    dadosRet.TipoIdentidade = TipoIdentidade.RG;
                }
                else
                {
                    dadosRet.DesabilitarTipoIdentidade = false;
                    dadosRet.TipoIdentidade = dadosRet.TipoIdentidade;
                }

                //BUSCANDO OS VALORES DO BANCO PARA CARREGAR NA TELA 
                //Tipo identidade é montado acima.
                //Dados da documentação acadêmica já vem na consulta e montagem do objeto dadosRet.
                //Documentação comprobatória é recuperada para inserção e edição. 
                //Dados do registro já vem na consulta e montagem do objeto dadosRet. 
                //Informações adicionais (mensagens) é recuperada no método BuscarMensagens
            }

            var specSolicitacaoDocumentoRequerido = new SolicitacaoDocumentoRequeridoFilterSpecification() { SeqSolicitacaoServico = seqSolicitacaoServico };
            var solicitacaoDocumentosRequeridos = this.SolicitacaoDocumentoRequeridoDomainService.SearchBySpecification(specSolicitacaoDocumentoRequerido, x => x.DocumentoRequerido, x => x.ArquivoAnexado).ToList();

            dadosRet.DocumentacaoComprobatoria = new List<SolicitacaoAnaliseEmissaoDocumentoConclusaoDocumentacaoVO>();

            foreach (var solicitacaoDocumentoRequerido in solicitacaoDocumentosRequeridos)
            {
                var descricaoTipoDocumento = this.TipoDocumentoService.BuscarTipoDocumento(solicitacaoDocumentoRequerido.DocumentoRequerido.SeqTipoDocumento).Descricao;

                dadosRet.DocumentacaoComprobatoria.Add(new SolicitacaoAnaliseEmissaoDocumentoConclusaoDocumentacaoVO()
                {
                    Seq = solicitacaoDocumentoRequerido.Seq,
                    SeqDocumentoRequerido = solicitacaoDocumentoRequerido.SeqDocumentoRequerido,
                    SituacaoEntregaDocumento = solicitacaoDocumentoRequerido.SituacaoEntregaDocumento,
                    FormaEntregaDocumento = solicitacaoDocumentoRequerido.FormaEntregaDocumento,
                    VersaoDocumento = solicitacaoDocumentoRequerido.VersaoDocumento,
                    DataEntrega = solicitacaoDocumentoRequerido.DataEntrega,
                    SeqTipoDocumento = solicitacaoDocumentoRequerido.DocumentoRequerido.SeqTipoDocumento,
                    DescricaoTipoDocumento = descricaoTipoDocumento,
                    Observacao = solicitacaoDocumentoRequerido.Observacao,
                    SeqArquivoAnexado = solicitacaoDocumentoRequerido.SeqArquivoAnexado,
                    ArquivoAnexado = solicitacaoDocumentoRequerido.SeqArquivoAnexado.HasValue ? new SMCUploadFile
                    {
                        GuidFile = solicitacaoDocumentoRequerido.ArquivoAnexado.UidArquivo.ToString(),
                        Name = solicitacaoDocumentoRequerido.ArquivoAnexado.Nome,
                        Size = solicitacaoDocumentoRequerido.ArquivoAnexado.Tamanho,
                        Type = solicitacaoDocumentoRequerido.ArquivoAnexado.Tipo
                    } : null,
                    SeqSolicitacaoServico = solicitacaoDocumentoRequerido.SeqSolicitacaoServico,
                    DataPrazoEntrega = solicitacaoDocumentoRequerido.DataPrazoEntrega,
                    DescricaoInconformidade = solicitacaoDocumentoRequerido.DescricaoInconformidade,
                    EntregaPosterior = solicitacaoDocumentoRequerido.EntregaPosterior,
                    ObservacaoSecretaria = solicitacaoDocumentoRequerido.ObservacaoSecretaria
                });
            }

            // Preenche a nacionalidade  
            if (dadosRet.CodigoNacionalidade.GetValueOrDefault() > 0)
            {
                var nacionalidade = this.PessoaService.BuscarNacionalidade(Convert.ToByte(dadosRet.CodigoNacionalidade.Value));

                if (nacionalidade != null)
                {
                    dadosRet.DescricaoNacionalidade = nacionalidade.Descricao;
                }
            }

            // Preenche o nome do país e a naturalidade
            if (dadosRet.CodigoPaisNacionalidade > 0)
            {
                var pais = this.LocalidadeService.BuscarPais((int)dadosRet.CodigoPaisNacionalidade);

                if (pais != null)
                {
                    dadosRet.NomePais = pais.Nome.Trim();

                    if (pais.Nome.Trim().ToUpper() == NOME_PAIS.BRASIL)
                    {
                        if (dadosRet.CodigoCidadeNaturalidade > 0 && !string.IsNullOrEmpty(dadosRet.UfNaturalidade))
                        {
                            var cidade = this.LocalidadeService.BuscarCidade(dadosRet.CodigoCidadeNaturalidade, dadosRet.UfNaturalidade);
                            dadosRet.Naturalidade = $"{cidade?.Nome.Trim()} - {dadosRet.UfNaturalidade.Trim()}";
                        }
                    }
                    else
                    {
                        dadosRet.Naturalidade = dadosRet.DescricaoNaturalidadeEstrangeira;
                    }
                }
            }

            if (!string.IsNullOrEmpty(dadosRet.NumeroIdentidadeEstrangeira))
            {
                if (dadosRet.CodigoPaisNacionalidade > 0)
                {
                    var pais = this.LocalidadeService.BuscarPais((int)dadosRet.CodigoPaisNacionalidade);
                    dadosRet.NumeroIdentidadeEstrangeira += $" - {pais?.Nome.Trim()}";
                }
            }

            // Inicindo os valores
            dadosRet.EmissaoPermitida = true;
            dadosRet.ExibirReconhecimentoCurso = false;

            // Recupera os dados do tipo de documento
            var dadosTipoDocumento = InstituicaoNivelTipoDocumentoAcademicoDomainService.SearchProjectionByKey(new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification
            {
                SeqInstituicaoEnsino = dadosOrigem.SeqInstituicaoEnsino,
                SeqTipoDocumentoAcademico = dadosRet.SeqTipoDocumentoAcademico,
                SeqInstituicaoNivel = seqInstituicaoNivel
            }, x => new
            {
                x.HabilitaRegistroDocumento,
                x.UsoSistemaOrigem,
                x.SeqOrgaoRegistro
            });

            if (dadosTipoDocumento == null)
                throw new SolicitacaoDocumentoConclusaoParametrizacaoTipoDocumentoNaoEncontradaException();

            /*  
                NV02:
                O campo "Emissão permitida?" deverá ser preenchido com a valor igual a "NÃO", quando:
                · O aluno não possui nenhuma formação específica que o tipo esteja parametrizado permitindo a emissão de
                documento de conclusão e, a [situação atual] da apuração da formação seja igual a Formado.
                    · Nesse caso o campo Motivo também deverá ser preenchido com o motivo:
                      ALUNO_NAO_APTO_EMISSAO_DOCUMENTO_CONCLUSAO.
            */

            if (dadosRet.TokenServico == TOKEN_SERVICO.EMISSAO_DIPLOMA_DIGITAL_1_VIA && (formacoesFormadasPermitemEmitirDocumento == null || !formacoesFormadasPermitemEmitirDocumento.Any()))
            {
                dadosRet.EmissaoPermitida = false;
                dadosRet.TokenMotivoEmissaoNaoPermitida = TOKEN_MOTIVO_SITUACAO_CANCELAMENTO.ALUNO_NAO_APTO_EMISSAO_DOCUMENTO_CONCLUSAO;
            }

            // Caso tenha emissão não permitida, busca o token
            if (!dadosRet.EmissaoPermitida)
            {
                // TODO: melhorar este processo de recuperar a descrição do token de motivo de situação.. Muito moroso.
                var templateProcesso = TipoTemplateProcessoService.BuscarTemplateProcesso(dadosRet.SeqTemplateProcessoSGF).FirstOrDefault();
                var seqSituacao = SituacaoService.BuscarSeqSituacaoPorToken(TOKEN_SITUACAO_SGF.SOLICITACAO_CANCELADA, templateProcesso.SeqTipoTemplateProcesso);
                var seqMotivoSituacao = SituacaoService.BuscarSeqMotivoSituacaoPorToken(seqSituacao.Value, dadosRet.TokenMotivoEmissaoNaoPermitida);
                var descricaoMotivoSituacao = SituacaoService.BuscarDescricaoMotivos(new long[] { seqMotivoSituacao.Value }).FirstOrDefault().Descricao;

                dadosRet.SeqMotivoEmissaoNaoPermitida = seqMotivoSituacao.Value;
                dadosRet.MotivoEmissaoNaoPermitida = descricaoMotivoSituacao;
            }

            /* 
              NV04:
              Pesquisar se há outro documento de conclusão que:
                . A pessoa do respectivo aluno do documento, seja a mesma pessoa da solicitação.
                • O agrupamento do tipo do documento seja igual ao agrupamento do tipo do documento solicitado e,
                · O tipo do documento seja igual ao tipo do documento solicitado e,
                    · Deverá ser considerando que DIPLOMA e DIPLOMA_DIGITAL são o mesmo tipo.

                · A situação atual do documento seja igual:
                    · ENTREGUE ou,
                    · AGUARDANDO_ENTREGA ou,
                    · AGUARDANDO_ASSINATURAS ou,
                    · VALIDO ou,
                    · INVALIDO e o motivo da invalidade igual a:
                        · Danificado
                        · Extraviado
                        · Descartado
                        · Indeferido pelo aluno
            */

            var filterDocumentoConclusao = new DocumentoConclusaoFilterSpecification
            {
                TokensSituacaoAtual = new List<string>
                {
                    TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.ENTREGUE,
                    TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.AGUARDANDO_ENTREGA,
                    TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.AGUARDANDO_ASSINATURAS,
                    TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.VALIDO,
                    TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.INVALIDO
                },
                SeqPessoaAtuacao = dadosRet.SeqPessoaAtuacao,
                GrupoDocumentoAcademico = tipoDocumentoAcademico.GrupoDocumentoAcademico
            };

            /*
                E complementar a pesquisa de acordo com os seguintes critérios:
                · "Exige grau acadêmico?":
                · Sim = O [curso + grau + descrição-documento + titulação] do documento, sejam iguais ao [curso +
                grau + descrição-documento + titulação] da formação identificada durante a analise de emissão
                (NV06)
                E se, o nível de ensino for igual a [Mestrado ou Doutorado]* a área de concentração do documento
                seja igual a área de concentração da formação identificada durante a analise de emissão (NV06).
                · Não = O [curso + descrição-documento + titulação] da formação do documento, sejam iguais ao
                [curso + descrição-documento + titulação] da formação identificada durante a analise de emissão
                (NV06)
                · "A formação gera carimbo no verso do documento?":
                · Sim = O tipo da formação específica do documento também esteja parametrizado para gerar carimbo
                e, o tipo da formação também permite a emissão do mesmo tipo de documento solicitado.
                · Deverá ser considerando que DIPLOMA e DIPLOMA_DIGITAL são o mesmo tipo.
                · Não = A formação específica da formação do documento, seja igual a formação específica
                identificada durante a analise de emissão.
            */

            var documentosConclusaoAnterioresAuxiliar = DocumentoConclusaoDomainService.SearchBySpecification(filterDocumentoConclusao,
                u => u.TipoDocumentoAcademico, v => v.OrgaoRegistro,
                w => w.FormacoesEspecificas[0].AlunoFormacao.FormacaoEspecifica.TipoFormacaoEspecifica,
                x => x.FormacoesEspecificas[0].AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica,
                y => y.Aluno.Historicos[0].CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica,
                z => z.SituacaoAtual.SituacaoDocumentoAcademico).ToList();

            var documentosConclusaoAnteriores = new List<DocumentoConclusao>();

            if (documentosConclusaoAnterioresAuxiliar.Any())
            {
                foreach (var documentoAnterior in documentosConclusaoAnterioresAuxiliar)
                {
                    if (documentoAnterior.SituacaoAtual.SituacaoDocumentoAcademico.Token == TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.INVALIDO)
                    {
                        if (documentoAnterior.SituacaoAtual.MotivoInvalidadeDocumento.HasValue)
                        {
                            //Se for inválido, tem que validar o motivo de invalidade (Danificado, Extraviado, Descartado, Indeferido)
                            if (documentoAnterior.SituacaoAtual.MotivoInvalidadeDocumento == MotivoInvalidadeDocumento.Danificado ||
                                documentoAnterior.SituacaoAtual.MotivoInvalidadeDocumento == MotivoInvalidadeDocumento.Extraviado ||
                                documentoAnterior.SituacaoAtual.MotivoInvalidadeDocumento == MotivoInvalidadeDocumento.Descartado)
                            {
                                documentosConclusaoAnteriores.Add(documentoAnterior);
                            }
                        }
                    }
                    else
                    {
                        documentosConclusaoAnteriores.Add(documentoAnterior);
                    }
                }
            }

            if (documentosConclusaoAnteriores.Any())
            {
                if (dadosRet.FormacoesEspecificasConcluidas != null && dadosRet.FormacoesEspecificasConcluidas.Any())
                {
                    if (dadosRet.FormacoesEspecificasConcluidas.Any(f => f.ExigeGrau))
                    {
                        if (dadosRet.SeqCurso.HasValue)
                        {
                            documentosConclusaoAnteriores = documentosConclusaoAnteriores.Where(x => x.Aluno.Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso == dadosRet.SeqCurso).ToList();
                        }

                        if (dadosRet.SeqGrauAcademicoSelecionado.HasValue)
                        {
                            /*Adicionando validação para verificar se existe formações específicas associadas ao documento de conclusão
                            pois precisa validar o grau acadêmico, então se não tiver formação já nem considera esse documento, 
                            e se colocasse x.FormacoesEspecificas?.. mesmo que não tivesse formação para verificar o grau acadêmico 
                            poderia retornar aquele documento*/
                            documentosConclusaoAnteriores = documentosConclusaoAnteriores.Where(x => x.FormacoesEspecificas.Any() && x.FormacoesEspecificas.OrderBy(o => o.DataInclusao).FirstOrDefault().AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica.FirstOrDefault(f => f.SeqFormacaoEspecifica == x.FormacoesEspecificas.OrderBy(o => o.DataInclusao).FirstOrDefault().AlunoFormacao.SeqFormacaoEspecifica).SeqGrauAcademico == dadosRet.SeqGrauAcademicoSelecionado).ToList();
                        }

                        var descricoesDocumento = dadosRet.FormacoesEspecificasConcluidas.Select(a => a.DescricaoDocumentoConclusao.ToLower().Trim()).ToList();
                        if (descricoesDocumento != null && descricoesDocumento.Any())
                        {
                            //Verificando se as descrições das formações do documento são exatamente iguais às descrições das formações do aluno  
                            documentosConclusaoAnteriores = documentosConclusaoAnteriores.Where(x => descricoesDocumento.All(x.FormacoesEspecificas.Select(a => a.AlunoFormacao.DescricaoDocumentoConclusao.ToLower().Trim()).Contains)).ToList();
                        }

                        var seqsTitulacoes = dadosRet.FormacoesEspecificasConcluidas.Select(a => a.SeqTitulacao).ToList();
                        if (seqsTitulacoes != null && seqsTitulacoes.Any())
                        {
                            documentosConclusaoAnteriores = documentosConclusaoAnteriores.Where(x => seqsTitulacoes.Contains(x.FormacoesEspecificas.OrderBy(o => o.DataInclusao).FirstOrDefault().AlunoFormacao.SeqTitulacao)).ToList();
                        }

                        //Aplica o filtro da area de concentração e verifica se é mestrado ou doutorado
                        if (dadosRet.TokenNivelEnsino == TOKEN_NIVEL_ENSINO.MESTRADO_ACADEMICO ||
                            dadosRet.TokenNivelEnsino == TOKEN_NIVEL_ENSINO.MESTRADO_PROFISSIONAL ||
                            dadosRet.TokenNivelEnsino == TOKEN_NIVEL_ENSINO.DOUTORADO_ACADEMICO ||
                            dadosRet.TokenNivelEnsino == TOKEN_NIVEL_ENSINO.DOUTORADO_PROFISSIONAL)
                        {
                            //OK. É mestrado ou doutorado. Filtra a área de concentração de acordo com a area de concentração das formações do documento
                            List<long> seqsDocumentosAnterioresRemover = new List<long>();
                            foreach (var item in documentosConclusaoAnteriores)
                            {
                                bool mesmaAreaConcentracao = false;
                                foreach (var dadosFormacao in item.FormacoesEspecificas)
                                {
                                    //Recupera a hierarquia de formações específicas
                                    var descricaoFormacoes = FormacaoEspecificaDomainService.BuscarDescricaoFormacaoEspecifica(dadosFormacao.AlunoFormacao.SeqFormacaoEspecifica, dadosFormacao.AlunoFormacao.FormacaoEspecifica.SeqEntidadeResponsavel, true);
                                    if (descricaoFormacoes != null && descricaoFormacoes.Any(f => f.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_CONCENTRACAO && seqsAreaConcentracaoFormacoesDocumento.Contains(f.SeqFormacaoEspecifica)))
                                    {
                                        mesmaAreaConcentracao = true;
                                        break;
                                    }
                                }

                                if (!mesmaAreaConcentracao)
                                    seqsDocumentosAnterioresRemover.Add(item.Seq);
                            }
                            documentosConclusaoAnteriores = documentosConclusaoAnteriores.Where(d => !seqsDocumentosAnterioresRemover.Contains(d.Seq)).ToList();
                        }
                    }
                    else
                    {
                        if (dadosRet.SeqCurso.HasValue)
                        {
                            documentosConclusaoAnteriores = documentosConclusaoAnteriores.Where(x => x.Aluno.Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso == dadosRet.SeqCurso).ToList();
                        }

                        var descricoesDocumento = dadosRet.FormacoesEspecificasConcluidas.Select(a => a.DescricaoDocumentoConclusao.ToLower().Trim()).ToList();
                        if (descricoesDocumento != null && descricoesDocumento.Any())
                        {
                            //Verificando se as descrições das formações do documento são exatamente iguais às descrições das formações do aluno  
                            documentosConclusaoAnteriores = documentosConclusaoAnteriores.Where(x => descricoesDocumento.All(x.FormacoesEspecificas.Select(a => a.AlunoFormacao.DescricaoDocumentoConclusao.ToLower().Trim()).Contains) && descricoesDocumento.Count == x.FormacoesEspecificas.Select(a => a.AlunoFormacao.DescricaoDocumentoConclusao).ToList().Count).ToList();
                        }

                        var seqsTitulacoes = dadosRet.FormacoesEspecificasConcluidas.Select(a => a.SeqTitulacao).ToList();
                        if (seqsTitulacoes != null && seqsTitulacoes.Any())
                        {
                            documentosConclusaoAnteriores = documentosConclusaoAnteriores.Where(x => seqsTitulacoes.Contains(x.FormacoesEspecificas.OrderBy(o => o.DataInclusao).FirstOrDefault().AlunoFormacao.SeqTitulacao)).ToList();
                        }
                    }

                    if (dadosRet.FormacoesEspecificasConcluidas.Any(f => f.GeraCarimbo.GetValueOrDefault()))
                    {
                        List<long> seqsDocumentosConclusaoRemover = new List<long>();

                        foreach (var documentoConclusao in documentosConclusaoAnteriores)
                        {
                            bool removerDocumento = true;

                            //Se o documento tem tipos de formação que não geram carimbo ou não permitem emissão de documento, já faz a exclusão desse documento
                            var formacoesNaoGeramCarimboPermitemEmissao = documentoConclusao.FormacoesEspecificas.Where(a => !a.AlunoFormacao.FormacaoEspecifica.TipoFormacaoEspecifica.GeraCarimbo.GetValueOrDefault() || !a.AlunoFormacao.FormacaoEspecifica.TipoFormacaoEspecifica.PermiteEmitirDocumentoConclusao).ToList();

                            if (!(formacoesNaoGeramCarimboPermitemEmissao != null && formacoesNaoGeramCarimboPermitemEmissao.Any()))
                            {
                                //Se entrou aqui é porque todos os tipos de formação do documento geram carimbo e permitem a emissão de documento
                                foreach (var formacaoEspecifica in documentoConclusao.FormacoesEspecificas)
                                {
                                    var specInstituicaoNivelTipoDocumentoAcademico = new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification()
                                    {
                                        SeqInstituicaoNivel = seqInstituicaoNivel,
                                        SeqsTipoFormacaoEspecifica = new List<long>() { formacaoEspecifica.AlunoFormacao.FormacaoEspecifica.SeqTipoFormacaoEspecifica }
                                    };

                                    var instituicoesNivelTipoDocumentoAcademico = InstituicaoNivelTipoDocumentoAcademicoDomainService.SearchBySpecification(specInstituicaoNivelTipoDocumentoAcademico).ToList();

                                    //Então é validado se o tipo de formação permite o tipo de documento de conclusão solicitado 
                                    foreach (var instituicaoNivelTipoDocumentoAcademico in instituicoesNivelTipoDocumentoAcademico)
                                    {
                                        var tipoDocumentoAcademicoParametrizado = TipoDocumentoAcademicoDomainService.SearchByKey(new SMCSeqSpecification<TipoDocumentoAcademico>(instituicaoNivelTipoDocumentoAcademico.SeqTipoDocumentoAcademico));

                                        if (listaTokensDiploma.Contains(tipoDocumentoAcademico.Token))
                                        {
                                            if (listaTokensDiploma.Contains(tipoDocumentoAcademicoParametrizado.Token))
                                            {
                                                removerDocumento = false;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (tipoDocumentoAcademico.Seq == tipoDocumentoAcademicoParametrizado.Seq)
                                            {
                                                removerDocumento = false;
                                                break;
                                            }
                                        }
                                    }

                                    //Verificando se todas as formações específicas do documento permitem a emissão do tipo de documento solicitado
                                    if (removerDocumento)
                                        break;
                                }
                            }

                            if (removerDocumento)
                                seqsDocumentosConclusaoRemover.Add(documentoConclusao.Seq);
                        }
                        documentosConclusaoAnteriores = documentosConclusaoAnteriores.Where(d => !seqsDocumentosConclusaoRemover.Contains(d.Seq)).ToList();
                    }
                    else
                    {
                        var sequenciaisFormacoesEspecificas = dadosRet.FormacoesEspecificasConcluidas.Select(a => a.SeqFormacaoEspecifica).ToList();
                        if (sequenciaisFormacoesEspecificas != null && sequenciaisFormacoesEspecificas.Any())
                        {
                            //Verificando se as formações do documento são exatamente iguais às formações do aluno  
                            documentosConclusaoAnteriores = documentosConclusaoAnteriores.Where(x => sequenciaisFormacoesEspecificas.All(x.FormacoesEspecificas.Select(a => a.AlunoFormacao.SeqFormacaoEspecifica).Contains) && sequenciaisFormacoesEspecificas.Count == x.FormacoesEspecificas.Select(a => a.AlunoFormacao.SeqFormacaoEspecifica).ToList().Count).ToList();
                        }
                    }
                }
            }

            /*
                Se foi identificado outros documentos, conforme os critérios citados acima:
                · Número da Via = número da via do [documento mais recente]* + 1
                · Identificação da via anterior = ID do [documento mais recente]*

                Senão,
                · Número da Via = 1
                · Identificação da via anterior = nulo
            */

            if (documentosConclusaoAnteriores != null && documentosConclusaoAnteriores.Any())
            {
                var viaAnterior = documentosConclusaoAnteriores.OrderByDescending(x => x.NumeroViaDocumento).FirstOrDefault();
                dadosRet.NumeroVia = viaAnterior.NumeroViaDocumento + 1;
                dadosRet.NumeroViaAnterior = viaAnterior.NumeroViaDocumento;
                dadosRet.SeqViaAnterior = viaAnterior.Seq;
                dadosRet.TokenTipoDocumentoAcademicoAnterior = viaAnterior.TipoDocumentoAcademico.Token;
                dadosRet.DescricaoTipoDocumentoAcademicoAnterior = viaAnterior.TipoDocumentoAcademico.Descricao;
                dadosRet.SeqDocumentoDiplomaGADAnterior = viaAnterior.SeqDocumentoGAD;

                var listaTokensTiposHistorico = new List<string>() { TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_FINAL, TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_2VIA, TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_PARCIAL };
                if (listaTokensTiposHistorico.Contains(tipoDocumentoAcademico.Token))
                {
                    if (viaAnterior.SeqDocumentoGAD.HasValue)
                    {
                        var retornoCodigoVerificacao = DocumentoConclusaoDomainService.BuscarCodigoVerificacaoResponseVO(viaAnterior.SeqDocumentoGAD.Value);

                        // Recupera a instituição de ensino logada
                        var seqInstituicaoEnsinoLogada = GetDataFilter(FILTER.INSTITUICAO_ENSINO).FirstOrDefault();
                        var sigla = EntidadeDomainService.SearchProjectionByKey(new EntidadeFilterSpecification { Seq = seqInstituicaoEnsinoLogada }, x => x.Sigla);

                        dadosRet.DescricaoViaAnteriorMensagem = $"emitido na {sigla} sob o código de validação: {retornoCodigoVerificacao.CodigoVerificacao} em {viaAnterior.DataInclusao.SMCDataAbreviada()}.";
                    }
                }
                else
                {
                    var auxViaAnterior = DocumentoConclusaoDomainService.SearchProjectionByKey(dadosRet.SeqViaAnterior.Value, x => new
                    {
                        DescricaoOrgaoRegistro = x.OrgaoRegistro.Descricao,
                        SiglaOrgaoRegistro = x.OrgaoRegistro.Sigla,
                        x.NumeroRegistro,
                        x.DataRegistro,
                        x.Livro,
                        x.Folha
                    });

                    dadosRet.InformacaoViaAnterior = $"A presente via invalida a anteriormente registrada: {dadosRet.NumeroViaAnterior}º Via, registrada no(a) {auxViaAnterior.SiglaOrgaoRegistro} sob o registro {auxViaAnterior.NumeroRegistro}";

                    if (!string.IsNullOrEmpty(auxViaAnterior.Livro))
                        dadosRet.InformacaoViaAnterior += $" Livro: {auxViaAnterior.Livro} e folha: {auxViaAnterior.Folha}";

                    var siglaOrgaoRegistro = !string.IsNullOrEmpty(auxViaAnterior.SiglaOrgaoRegistro) ? auxViaAnterior.SiglaOrgaoRegistro : "-";
                    var numeroRegistro = !string.IsNullOrEmpty(auxViaAnterior.NumeroRegistro) ? auxViaAnterior.NumeroRegistro : "-";
                    var anoRegistro = auxViaAnterior.DataRegistro.HasValue ? auxViaAnterior.DataRegistro.Value.Year.ToString() : "-";

                    dadosRet.DescricaoViaAnteriorMensagem = $"{dadosRet.NumeroViaAnterior}° Via, registrada na {siglaOrgaoRegistro} sob o registro n° {numeroRegistro}/{anoRegistro}";

                    if (!string.IsNullOrEmpty(auxViaAnterior.Livro))
                    {
                        var auxLivro = auxViaAnterior.Livro;
                        dadosRet.DescricaoViaAnteriorMensagem += auxLivro.Trim().ToLower().Contains("livro:") ? $" {auxViaAnterior.Livro.Trim()}" : $" Livro: {auxViaAnterior.Livro.Trim()}";
                    }

                    if (!string.IsNullOrEmpty(auxViaAnterior.Folha))
                    {
                        var auxFolha = auxViaAnterior.Folha;
                        dadosRet.DescricaoViaAnteriorMensagem += auxFolha.Trim().ToLower().Contains("folha:") ? $" {auxViaAnterior.Folha.Trim()}" : $" Folha: {auxViaAnterior.Folha.Trim()}";
                    }
                }
            }
            else
            {
                dadosRet.NumeroVia = 1;
                dadosRet.NumeroViaAnterior = null;
                dadosRet.SeqViaAnterior = null;
                dadosRet.SeqDocumentoDiplomaGADAnterior = null;
            }

            var tokenTipoDocumentoAcademico = new List<string>() { TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL };
            var temDocumentoConclusao = DocumentoConclusaoDomainService.VerificarDocumentoConclusao(dadosRet.SeqPessoaAtuacao, dadosRet.DescricaoCursoDocumento, dadosRet.SeqGrauAcademicoSelecionado, dadosRet.SeqTitulacao, tokenTipoDocumentoAcademico);
            dadosRet.ExibirMensagemHistoricoEscolarNumeroViaMaiorUm = tipoDocumentoAcademico.GrupoDocumentoAcademico == GrupoDocumentoAcademico.HistoricoEscolar && !temDocumentoConclusao && dadosRet.NumeroVia > 1;
            dadosRet.ExibirMensagemDiplomaPrimeiraViaComHistorico = dadosRet.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL && dadosRet.NumeroVia == 1;

            dadosRet.ExibirMensagemDiplomaDigitalBachareladoLicenciatura = dadosRet.TokenNivelEnsino == TOKEN_NIVEL_ENSINO.GRADUACAO && dadosRet.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL && dadosRet.DescricaoGrauAcademicoSelecionado == GRAU_ACADEMICO.BachareladoLicenciatura;
            dadosRet.ExibirMensagemAutorizacaoNomeSocial = !string.IsNullOrEmpty(dadosRet.NomeSocial) && dadosRet.ExibirNomeSocial;
            dadosRet.ExibirComandoDocumentacaoAcademica = dadosRet.TokenNivelEnsino == TOKEN_NIVEL_ENSINO.GRADUACAO || dadosRet.TokenNivelEnsino == TOKEN_NIVEL_ENSINO.SEQUENCIAL_FORMACAO_ESPECIFICA;
            dadosRet.ExibirComandoRVDD = dadosRet.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL;
            dadosRet.ExibirDadosDiploma = tipoDocumentoAcademico.GrupoDocumentoAcademico == GrupoDocumentoAcademico.Diploma;
            dadosRet.ExibirDadosHistorico = (tipoDocumentoAcademico.GrupoDocumentoAcademico == GrupoDocumentoAcademico.Diploma && dadosRet.NumeroVia == 1) || tipoDocumentoAcademico.GrupoDocumentoAcademico == GrupoDocumentoAcademico.HistoricoEscolar;
            dadosRet.ExibirSecaoDocumentacaoAcademica = true;

            if (dadosRet.FormacoesEspecificasConcluidas != null && dadosRet.FormacoesEspecificasConcluidas.Any())
            {
                dadosRet.FormacoesEspecificasConcluidas.ForEach(x => { x.NumeroVia = dadosRet.NumeroVia; });
                dadosRet.FormacoesEspecificasConcluidas = dadosRet.FormacoesEspecificasConcluidas.OrderBy(d => d.DataFormacao).ToList();
            }

            if (dadosRet.FormacoesEspecificasViasAnteriores != null && dadosRet.FormacoesEspecificasViasAnteriores.Any())
            {
                dadosRet.FormacoesEspecificasViasAnteriores.ForEach(x => { x.NumeroVia = dadosRet.NumeroVia; });
                dadosRet.FormacoesEspecificasViasAnteriores = dadosRet.FormacoesEspecificasViasAnteriores.OrderBy(d => d.DataFormacao).ToList();

                // Formata a descrição das formações específicas anteriores  
                foreach (var item in dadosRet.FormacoesEspecificasViasAnteriores)
                {
                    var descricaoFormacao = FormacaoEspecificaDomainService.BuscarDescricaoFormacaoEspecifica(item.SeqFormacaoEspecifica, item.SeqEntidadeResponsavel, true);

                    if (descricaoFormacao != null && descricaoFormacao.Any())
                    {
                        item.HierarquiaFormacao = descricaoFormacao;
                        item.DescricaoFormacaoEspecifica = descricaoFormacao.FirstOrDefault(d => d.SeqFormacaoEspecifica == item.SeqFormacaoEspecifica).DescricaoFormacaoEspecifica;
                        item.DescricoesFormacaoEspecifica = descricaoFormacao.Select(d => d.DescricaoFormacaoEspecifica).ToList();
                    }
                    else
                    {
                        var descricaoFormacaoEspecifica = FormacaoEspecificaDomainService.BuscarFormacoesEspecificasHierarquia(new long[] { item.SeqFormacaoEspecifica });
                        var hierarquiasFormacao = descricaoFormacaoEspecifica.SelectMany(a => a.Hierarquia).ToList();
                        item.DescricoesFormacaoEspecifica = hierarquiasFormacao.Select(a => $"[{a.DescricaoTipoFormacaoEspecifica}] {a.Descricao}").ToList();
                    }
                }
            }

            if (dadosRet.NumeroVia > 1)
            {
                if (dadosRet.SeqViaAnterior.HasValue)
                {
                    var dadosDocumentoConclusaoAnterior = DocumentoConclusaoDomainService.SearchProjectionByKey(dadosRet.SeqViaAnterior.Value, x => new
                    {
                        TokenTipoDocumento = x.TipoDocumentoAcademico.Token,
                        DescricaoTipoDocumento = x.TipoDocumentoAcademico.Descricao,
                        x.NumeroRegistro,
                        x.DataRegistro,
                        x.NumeroPublicacaoDOU,
                        x.DataPublicacaoDOU
                    });

                    if (dadosDocumentoConclusaoAnterior.TokenTipoDocumento == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL)
                    {
                        dadosRet.FormatoHistoricoEscolar = FormatoHistoricoEscolar.InformarComMatrizCurricular;

                        var numeroRegistro = !string.IsNullOrEmpty(dadosDocumentoConclusaoAnterior.NumeroRegistro) ? dadosDocumentoConclusaoAnterior.NumeroRegistro : "-";
                        var dataRegistro = dadosDocumentoConclusaoAnterior.DataRegistro.HasValue ? dadosDocumentoConclusaoAnterior.DataRegistro.Value.ToString("dd/MM/yyyy") : "-";
                        var numeroPublicacao = dadosDocumentoConclusaoAnterior.NumeroPublicacaoDOU.HasValue ? dadosDocumentoConclusaoAnterior.NumeroPublicacaoDOU.ToString() : "-";
                        var dataPublicacao = dadosDocumentoConclusaoAnterior.DataPublicacaoDOU.HasValue ? dadosDocumentoConclusaoAnterior.DataPublicacaoDOU.Value.ToString("dd/MM/yyyy") : "-";

                        dadosRet.DescricaoViaAnterior = $"{dadosDocumentoConclusaoAnterior.DescricaoTipoDocumento} - Registro n° {numeroRegistro} de {dataRegistro}, Publicação DOU n° {numeroPublicacao} de {dataPublicacao}";
                    }
                }
            }

            dadosRet.ExibirSecaoDocumentacaoAcademica = dadosRet.ExibirSecaoDocumentacaoAcademica && dadosRet.EmissaoDiplomaDigital1Via;
            dadosRet.SeqOrgaoRegistro = dadosTipoDocumento.SeqOrgaoRegistro;

            if (dadosTipoDocumento.HabilitaRegistroDocumento && !dadosRet.SeqOrgaoRegistro.HasValue)
                throw new SolicitacaoDocumentoConclusaoParametrizacaoOrgaoRegistroNaoEncontradaException();

            if (dadosRet.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL && dadosRet.DescricaoGrauAcademicoSelecionado == GRAU_ACADEMICO.BachareladoLicenciatura)
            {
                var orgaoRegistroDocumentoAtual = OrgaoRegistroDomainService.SearchByKey(new SMCSeqSpecification<OrgaoRegistro>(dadosRet.SeqOrgaoRegistro.Value));
                dadosRet.DescricaoViaAtual = $"{dadosRet.NumeroVia}° Via, registrada na {orgaoRegistroDocumentoAtual.Sigla} sob o número do respectivo registro que será gerado ao confirmar a emissão";
            }

            dadosRet.ExibirSecaoDadosRegistro = false;
            if (dadosRet.NumeroVia > 1 && dadosRet.SeqViaAnterior.HasValue && dadosTipoDocumento.HabilitaRegistroDocumento && dadosTipoDocumento.UsoSistemaOrigem == UsoSistemaOrigem.ArquivoPDF)
            {
                var seqOrgaoRegistroDocumentoConclusaoAnterior = DocumentoConclusaoDomainService.SearchProjectionByKey(dadosRet.SeqViaAnterior.Value, x => x.SeqOrgaoRegistro);
                var orgaoRegistroDocumentoAtual = OrgaoRegistroDomainService.SearchByKey(new SMCSeqSpecification<OrgaoRegistro>(dadosRet.SeqOrgaoRegistro.Value));

                if (orgaoRegistroDocumentoAtual.Seq == seqOrgaoRegistroDocumentoConclusaoAnterior)
                    dadosRet.ExibirSecaoDadosRegistro = true;
                else
                    dadosRet.ExibirSecaoDadosRegistro = false;
            }

            // Se for emissão de 1a via, verifica o bloqueio de matarial de biblioteca
            if (dadosRet.NumeroVia == 1 && (tipoDocumentoAcademico.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA || tipoDocumentoAcademico.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL))
            {
                PessoaAtuacaoBloqueioDomainService.VerificaBloqueioPendenciaBiblioteca(dadosRet.SeqPessoaAtuacao);
            }

            // Campo Cotutela
            /*NV12: Exibir texto de acordo com RN_CNC_020 - Documento Conclusão - Carimbo de Co-Tutela. Se retornar NULL exibir o texto "Não se aplica".*/
            /*Se a pessoa-atuação da solicitação estiver associada em um termo de intercâmbio e, o tipo da parceria
            é igual a Co-tutela, retornar o seguinte texto: 
            · Convênio co-tutela entre a Pontifícia Universidade Católica de Minas Gerais (Brasil) e [Descrição
            da Instituição Externa] (País da Instituição Externa)
            Senão, retornar null.*/

            if (dadosRet.TermosCotutela != null && dadosRet.TermosCotutela.Any())
            {
                foreach (var item in dadosRet.TermosCotutela)
                {
                    var dadosPais = LocalidadeService.BuscarPais(item.CodigoPaisInstituicaoExterna);
                    dadosRet.Cotutela += $"Convênio co-tutela entre a Pontifícia Universidade Católica de Minas Gerais (Brasil) e {item.NomeInstituicaoEnsino} ({dadosPais.Nome.Trim()})<br />";
                }
            }
            else
                dadosRet.Cotutela = "Não se aplica";

            // Recupera a data de defesa do trabalho
            var specTrabalho = new TrabalhoAcademicoFilterSpecification { SeqAluno = dadosRet.SeqPessoaAtuacao };
            dadosRet.DataDefesa = TrabalhoAcademicoDomainService.SearchProjectionByKey(specTrabalho,
                t => (DateTime?)t.DivisoesComponente.SelectMany(sd => sd.OrigemAvaliacao.AplicacoesAvaliacao)
                            .OrderByDescending(o => o.Seq)
                            .FirstOrDefault(w => w.DataCancelamento == null && w.Avaliacao.TipoAvaliacao == TipoAvaliacao.Banca).DataInicioAplicacaoAvaliacao);

            if (!string.IsNullOrEmpty(dadosRet.NumeroPassaporte))
            {
                dadosRet.TipoDocumento = "Passaporte";

                if (dadosRet.CodigoPaisEmissaoPassaporte.HasValue)
                {
                    var dadosPaisPassaporte = LocalidadeService.BuscarPais(dadosRet.CodigoPaisEmissaoPassaporte.Value);
                    dadosRet.NumeroDocumento = $"{dadosRet.NumeroPassaporte.ToUpper().Trim()} - País de emissão: {dadosPaisPassaporte.Nome.Trim()}";
                }
                else
                {
                    dadosRet.NumeroDocumento = $"{dadosRet.NumeroPassaporte.ToUpper().Trim()} - País de emissão: -";
                }
            }
            else if (!string.IsNullOrEmpty(dadosRet.NumeroRegistroCnh))
            {
                dadosRet.TipoDocumento = "Carteira Motorista";
                dadosRet.NumeroDocumento = dadosRet.NumeroRegistroCnh.Trim();
            }

            if (!string.IsNullOrEmpty(dadosRet.Cpf))
                dadosRet.Cpf = SMCMask.ApplyMaskCPF(dadosRet.Cpf.Trim());

            dadosRet.HabilitarCampoGrauAcademico = dadosRet.SeqGrauAcademicoSelecionado.HasValue && dadosRet.SeqGrauAcademicoSelecionado > 0;

            return dadosRet;
        }

        private bool PermitirTipoDocumentoSolicitado(InstituicaoNivelTipoDocumentoAcademicoFilterSpecification specInstituicaoNivelTipoDocumentoAcademico, long seqTipoDocumentoAcademico, GrupoDocumentoAcademico grupoDocumentoAcademico)
        {
            var instituicoesNivelTipoDocumentoAcademico = InstituicaoNivelTipoDocumentoAcademicoDomainService.SearchBySpecification(specInstituicaoNivelTipoDocumentoAcademico).ToList();
            var tipoFormacaoPermiteTipoDocumentoSolicitado = false;

            // Validando se o tipo de formação permite o tipo de documento de conclusão solicitado 
            foreach (var instituicaoNivelTipoDocumentoAcademico in instituicoesNivelTipoDocumentoAcademico)
            {
                var tipoDocumentoAcademicoParametrizado = TipoDocumentoAcademicoDomainService.SearchByKey(new SMCSeqSpecification<TipoDocumentoAcademico>(instituicaoNivelTipoDocumentoAcademico.SeqTipoDocumentoAcademico));

                if (grupoDocumentoAcademico == tipoDocumentoAcademicoParametrizado.GrupoDocumentoAcademico)
                {
                    tipoFormacaoPermiteTipoDocumentoSolicitado = true;
                    break;
                }
                else
                {
                    if (seqTipoDocumentoAcademico == tipoDocumentoAcademicoParametrizado.Seq)
                    {
                        tipoFormacaoPermiteTipoDocumentoSolicitado = true;
                        break;
                    }
                }
            }
            return tipoFormacaoPermiteTipoDocumentoSolicitado;
        }

        public (DateTime DataAdmissao, string Token, string Descricao, string DescricaoXSD) BuscarFormaEDataIngressoComMenorDataAdmissao(long seqPessoaAtuacao, long seqCursoOferta)
        {
            var specAlunoHistorico = new AlunoHistoricoFilterSpecification() { SeqAluno = seqPessoaAtuacao, SeqCursoOferta = seqCursoOferta };
            var alunoHistoricos = AlunoHistoricoDomainService.SearchBySpecification(specAlunoHistorico, s => s.FormaIngresso).OrderBy(o => o.DataAdmissao).ToList();

            var auxAlunoHistoricoAtual = alunoHistoricos.FirstOrDefault();

            return (auxAlunoHistoricoAtual.DataAdmissao, auxAlunoHistoricoAtual.FormaIngresso.Token,
                    auxAlunoHistoricoAtual.FormaIngresso.Descricao, auxAlunoHistoricoAtual.FormaIngresso.DescricaoXSD);
        }

        public List<SMCDatasourceItem> BuscarTiposHistoricoEscolarSelect()
        {
            var listaRetorno = new List<SMCDatasourceItem>();

            var specTiposHistoricoEscolar = new TipoDocumentoAcademicoFilterSpecification()
            {
                Tokens = new List<string>()
                {
                    TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_FINAL,
                    TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_PARCIAL,
                    TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_2VIA
                }
            };
            var tiposHistoricoEscolar = TipoDocumentoAcademicoDomainService.SearchBySpecification(specTiposHistoricoEscolar).ToList();

            foreach (var itemLista in tiposHistoricoEscolar)
            {
                var itemRetorno = new SMCDatasourceItem
                {
                    Seq = itemLista.Seq,
                    Descricao = itemLista.Descricao,
                    DataAttributes = new List<SMCKeyValuePair>() { new SMCKeyValuePair { Key = "token", Value = itemLista.Token }, }
                };

                listaRetorno.Add(itemRetorno);
            }

            return listaRetorno;
        }

        private List<SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaVO> RetornaFormacoesAlunoHistoricosAnteriores(long seqAlunoHistorico, long seqCursoAlunoHistoricoAtual, long? seqGrauAcademicoAlunoHistoricoAtual, string descricaoDocumentoConclusaoAlunoHistoricoAtual, long? seqTitulacaoAlunoHistoricoAtual, long seqInstituicaoNivel, List<string> listaTokensDiploma, TipoDocumentoAcademico tipoDocumentoAcademico, long numeroRA, Sexo sexoAluno, DateTime? dataConclusao)
        {
            /* Busca as formações anteriores de maneira recursiva, até ter mais anteriores que correspondam as clausulas, 
            se for diferente já desconsidera mesmo que tenha historicos anteriores */

            List<SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaVO> lista = new List<SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaVO>();

            //Retorna o aluno histórico 
            var alunoHistorico = AlunoHistoricoDomainService.SearchByKey(new SMCSeqSpecification<AlunoHistorico>(seqAlunoHistorico),
               w => w.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso,
               x => x.Formacoes[0].ApuracoesFormacao,
               y => y.Formacoes[0].FormacaoEspecifica.TipoFormacaoEspecifica,
               z => z.Formacoes[0].Titulacao);

            var validarHistoricoAnterior = false;

            var formacoesAlunoHistoricoAnteriorParaVerificar = alunoHistorico.Formacoes.Where(a => !a.DataFim.HasValue).ToList();
            foreach (var alunoFormacao in formacoesAlunoHistoricoAnteriorParaVerificar)
            {
                var passouCondicoesInclusaoFormacao = true;

                //1.2.1
                if (alunoFormacao.FormacaoEspecifica.TipoFormacaoEspecifica.ExigeGrau)
                {
                    var seqCursoAlunoFormacao = alunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso;

                    var specCursoFormacaoEspecificaAlunoFormacao = new CursoFormacaoEspecificaFilterSpecification() { SeqCurso = seqCursoAlunoFormacao, SeqFormacaoEspecifica = alunoFormacao.SeqFormacaoEspecifica };
                    var seqGrauAcademicoAlunoFormacao = this.CursoFormacaoEspecificaDomainService.SearchProjectionByKey(specCursoFormacaoEspecificaAlunoFormacao, x => x.SeqGrauAcademico);

                    if (seqCursoAlunoHistoricoAtual != seqCursoAlunoFormacao)
                        passouCondicoesInclusaoFormacao = false;
                    else if (seqGrauAcademicoAlunoHistoricoAtual != seqGrauAcademicoAlunoFormacao)
                        passouCondicoesInclusaoFormacao = false;
                    else if (descricaoDocumentoConclusaoAlunoHistoricoAtual.ToLower().Trim() != alunoFormacao.DescricaoDocumentoConclusao.ToLower().Trim())
                        passouCondicoesInclusaoFormacao = false;
                    else if (seqTitulacaoAlunoHistoricoAtual != alunoFormacao.SeqTitulacao)
                        passouCondicoesInclusaoFormacao = false;
                }
                else
                {
                    var seqCursoAlunoFormacao = alunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso;

                    if (seqCursoAlunoHistoricoAtual != seqCursoAlunoFormacao)
                        passouCondicoesInclusaoFormacao = false;
                    else if (descricaoDocumentoConclusaoAlunoHistoricoAtual.ToLower().Trim() != alunoFormacao.DescricaoDocumentoConclusao.ToLower().Trim())
                        passouCondicoesInclusaoFormacao = false;
                    else if (seqTitulacaoAlunoHistoricoAtual != alunoFormacao.SeqTitulacao)
                        passouCondicoesInclusaoFormacao = false;
                }

                //1.2.2
                if (alunoFormacao.FormacaoEspecifica.TipoFormacaoEspecifica.GeraCarimbo.GetValueOrDefault())
                {
                    var specInstituicaoNivelTipoDocumentoAcademico = new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification()
                    {
                        SeqInstituicaoNivel = seqInstituicaoNivel,
                        SeqsTipoFormacaoEspecifica = new List<long>() { alunoFormacao.FormacaoEspecifica.SeqTipoFormacaoEspecifica }
                    };

                    var instituicoesNivelTipoDocumentoAcademico = InstituicaoNivelTipoDocumentoAcademicoDomainService.SearchBySpecification(specInstituicaoNivelTipoDocumentoAcademico).ToList();
                    var tipoFormacaoPermiteTipoDocumentoSolicitado = false;

                    // Validando se o tipo de formação permite o tipo de documento de conclusão solicitado 
                    foreach (var instituicaoNivelTipoDocumentoAcademico in instituicoesNivelTipoDocumentoAcademico)
                    {
                        var tipoDocumentoAcademicoParametrizado = TipoDocumentoAcademicoDomainService.SearchByKey(new SMCSeqSpecification<TipoDocumentoAcademico>(instituicaoNivelTipoDocumentoAcademico.SeqTipoDocumentoAcademico));

                        if (listaTokensDiploma.Contains(tipoDocumentoAcademico.Token))
                        {
                            if (listaTokensDiploma.Contains(tipoDocumentoAcademicoParametrizado.Token))
                            {
                                tipoFormacaoPermiteTipoDocumentoSolicitado = true;
                                break;
                            }
                        }
                        else
                        {
                            if (tipoDocumentoAcademico.Seq == tipoDocumentoAcademicoParametrizado.Seq)
                            {
                                tipoFormacaoPermiteTipoDocumentoSolicitado = true;
                                break;
                            }
                        }
                    }

                    if (!tipoFormacaoPermiteTipoDocumentoSolicitado)
                        passouCondicoesInclusaoFormacao = false;
                }

                if (passouCondicoesInclusaoFormacao)
                {
                    validarHistoricoAnterior = true;

                    var apuracaoFormacao = alunoFormacao.ApuracoesFormacao.OrderByDescending(a => a.DataInicio).FirstOrDefault(a => !a.DataExclusao.HasValue && a.DataInicio >= alunoFormacao.DataInicio);
                    var situacaoAlunoFormacao = (SituacaoAlunoFormacao?)apuracaoFormacao.SituacaoAlunoFormacao;
                    var dataFormacao = (DateTime?)apuracaoFormacao.DataInicio;
                    var tipoFormacaoEspecifica = alunoFormacao.FormacaoEspecifica.TipoFormacaoEspecifica;

                    lista.Add(new SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaVO
                    {
                        Data = alunoFormacao.DataInicio,
                        SituacaoAlunoFormacao = situacaoAlunoFormacao,
                        SeqFormacaoEspecifica = alunoFormacao.SeqFormacaoEspecifica,
                        DescricaoDocumentoConclusao = alunoFormacao.DescricaoDocumentoConclusao,
                        SeqTitulacao = alunoFormacao.SeqTitulacao,
                        GeraCarimbo = tipoFormacaoEspecifica.GeraCarimbo,
                        SeqEntidadeResponsavel = alunoFormacao.FormacaoEspecifica.SeqEntidadeResponsavel,
                        DataInicio = alunoFormacao.DataInicio,
                        DataFim = alunoFormacao.DataFim,
                        ExigeGrau = tipoFormacaoEspecifica.ExigeGrau,
                        SeqTipoFormacaoEspecifica = alunoFormacao.FormacaoEspecifica.SeqTipoFormacaoEspecifica,
                        PermiteEmitirDocumentoConclusao = tipoFormacaoEspecifica.PermiteEmitirDocumentoConclusao,
                        SeqAlunoFormacao = alunoFormacao.Seq,
                        TokenTipoFormacaoEspecifica = tipoFormacaoEspecifica.Token,
                        SeqFormacaoEspecificaSuperior = alunoFormacao.FormacaoEspecifica.SeqFormacaoEspecificaSuperior,
                        DescricaoTipoFormacaoEspecifica = tipoFormacaoEspecifica.Descricao,
                        DataFormacao = dataFormacao,
                        NumeroRA = numeroRA,
                        DescricaoTitulacao = sexoAluno == Sexo.Masculino ? alunoFormacao.Titulacao.DescricaoMasculino : alunoFormacao.Titulacao.DescricaoFeminino,
                        DataColacaoGrau = alunoFormacao.DataColacaoGrau,
                        DataConclusao = alunoFormacao.DataConclusao  //dataConclusao
                    });
                }
            }

            if (validarHistoricoAnterior && alunoHistorico.SeqAlunoHistoricoAnterior.HasValue)
                lista.AddRange(RetornaFormacoesAlunoHistoricosAnteriores(alunoHistorico.SeqAlunoHistoricoAnterior.Value, seqCursoAlunoHistoricoAtual, seqGrauAcademicoAlunoHistoricoAtual, descricaoDocumentoConclusaoAlunoHistoricoAtual, seqTitulacaoAlunoHistoricoAtual, seqInstituicaoNivel, listaTokensDiploma, tipoDocumentoAcademico, numeroRA, sexoAluno, dataConclusao));

            return lista;
        }

        public string ConsultarInformacoesRVDD(long seqSolicitacaoServico, TipoIdentidade? tipoIdentidade)
        {
            if (tipoIdentidade == null)
                throw new SolicitacaoDocumentoConclusaoTipoIdentidadeNaoInformadaException();
            string mensagemModal = string.Empty;
            var dadosEmissao = BuscarDadosSolicitacaoDocumentoConclusao(seqSolicitacaoServico);

            switch (dadosEmissao.TokenNivelEnsino)
            {
                case TOKEN_NIVEL_ENSINO.GRADUACAO:

                    var descricaoCursoDocumento = !string.IsNullOrEmpty(dadosEmissao.DescricaoCursoDocumento) ? dadosEmissao.DescricaoCursoDocumento.Trim() : "-";

                    var formacoesConcatenadas = new List<SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaVO>();
                    if (dadosEmissao.FormacoesEspecificasConcluidas != null)
                        formacoesConcatenadas.AddRange(dadosEmissao.FormacoesEspecificasConcluidas);

                    if (dadosEmissao.FormacoesEspecificasViasAnteriores != null)
                        formacoesConcatenadas.AddRange(dadosEmissao.FormacoesEspecificasViasAnteriores);

                    var auxDataConclusao = formacoesConcatenadas.Select(a => a.DataConclusao).Min();
                    var dataConclusao = auxDataConclusao.HasValue ? auxDataConclusao.Value.SMCDataPorExtenso() : "-";

                    var auxDataColacao = formacoesConcatenadas.Select(a => a.DataColacaoGrau).Min();
                    var dataColacao = auxDataColacao.HasValue ? auxDataColacao.Value.SMCDataPorExtenso() : "-";

                    var descricaoTitulacao = !string.IsNullOrEmpty(dadosEmissao.DescricaoTitulacao) ? dadosEmissao.DescricaoTitulacao.Trim() : "-";
                    var nomeAluno = dadosEmissao.ExibirNomeSocial && !string.IsNullOrEmpty(dadosEmissao.NomeSocial) ? dadosEmissao.NomeSocial.Trim() : dadosEmissao.NomeAluno.Trim();
                    var descricaoNacionalidade = !string.IsNullOrEmpty(dadosEmissao.DescricaoNacionalidade) ? dadosEmissao.DescricaoNacionalidade.Trim() : "-";
                    var naturalidade = !string.IsNullOrEmpty(dadosEmissao.Naturalidade) ? dadosEmissao.Naturalidade.Trim() : "-";
                    var dataNascimento = dadosEmissao.DataNascimento.SMCDataPorExtenso();

                    var cedulaIdentidade = string.Empty;
                    if (dadosEmissao.TipoNacionalidade == TipoNacionalidade.Brasileira)
                    {
                        var cpf = dadosEmissao.Cpf.SMCRemoveNonDigits();
                        var cpfFormatado = SMCMask.ApplyMaskCPF(cpf.Trim());
                        cedulaIdentidade = $"CPF {cpfFormatado}";
                    }
                    else
                    {
                        if (tipoIdentidade.Value == TipoIdentidade.RG)
                        {
                            cedulaIdentidade = $"{dadosEmissao.NumeroIdentidade} - {dadosEmissao.OrgaoEmissorIdentidade} - {dadosEmissao.UfIdentidade}";
                        }
                        else if (tipoIdentidade.Value == TipoIdentidade.Passaporte)
                        {
                            var pais = this.LocalidadeService.BuscarPais((int)dadosEmissao.CodigoPaisEmissaoPassaporte);
                            cedulaIdentidade = $"passaporte {dadosEmissao.NumeroPassaporte?.Trim()} - {pais?.Nome.Trim()}";
                        }
                        else if (tipoIdentidade.Value == TipoIdentidade.IdentidadeEstrangeira)
                        {
                            var pais = this.LocalidadeService.BuscarPais((int)dadosEmissao.CodigoPaisNacionalidade);
                            cedulaIdentidade = $"identidade estrangeira {dadosEmissao.NumeroIdentidadeEstrangeira?.Trim()}";
                        }
                    }

                    mensagemModal = string.Format(MessagesResource.MSG_ModalInformacoesRVDDGraduacao,
                                                         descricaoCursoDocumento,
                                                         dataConclusao,
                                                         dataColacao,
                                                         descricaoTitulacao,
                                                         nomeAluno,
                                                         descricaoNacionalidade,
                                                         naturalidade,
                                                         dataNascimento,
                                                         cedulaIdentidade);

                    break;
            }


            return mensagemModal;
        }

        /// <summary>
        /// Busca dados instituição - Ato Normativo (Autorização / Reconhecimento / Renovação reconhecimento) - RN_CNC_051
        /// </summary>
        public SolicitacaoAnaliseEmissaoDocumentoConclusaoAtosCursoVO BuscarAtosNormativosCurso(long seqCursoOfertaLocalidade, long? seqGrauAcademico, DateTime? dataConclusao, bool exibirErro = false)
        {
            if (!seqGrauAcademico.HasValue || (seqGrauAcademico.HasValue && seqGrauAcademico.Value == 0))
                throw new SolicitacaoDocumentoConclusaoSemGrauAcademicoException();

            var retorno = new SolicitacaoAnaliseEmissaoDocumentoConclusaoAtosCursoVO()
            {
                SeqCursoOfertaLocalidade = seqCursoOfertaLocalidade,
                DataConclusao = dataConclusao,
                Autorizacao = new DadosAtoNormativoVO(),
                Reconhecimento = new DadosAtoNormativoVO(),
                RenovacaoReconhecimento = new DadosAtoNormativoVO(),
            };

            var cursoOfertaLocalidade = CursoOfertaLocalidadeDomainService.SearchByKey(new SMCSeqSpecification<CursoOfertaLocalidade>(seqCursoOfertaLocalidade));
            retorno.CodigoCursoOfertaLocalidade = cursoOfertaLocalidade.Codigo;
            retorno.CodigoOrgaoRegulador = cursoOfertaLocalidade.CodigoOrgaoRegulador;

            var specGrauAcademico = new GrauAcademicoFilterSpecification() { Seq = seqGrauAcademico };
            var grauAcademico = this.GrauAcademicoDomainService.SearchByKey(specGrauAcademico);
            retorno.DescricaoGrauAcademico = grauAcademico.Descricao;

            var atosNormativosEntidades = AtoNormativoEntidadeDomainService.SearchBySpecification(new AtoNormativoEntidadeFilterSpecification
            {
                HabilitaEmissaoDocumentoConclusao = true,
                SeqEntidade = seqCursoOfertaLocalidade,
                ConsiderarApenasAtosVigente = false,
                SeqGrauAcademico = seqGrauAcademico
            }, x => x.AtoNormativo.AssuntoNormativo, y => y.AtoNormativo.TipoAtoNormativo);

            if (!dataConclusao.HasValue)
                dataConclusao = DateTime.Now;

            atosNormativosEntidades = atosNormativosEntidades.Where(a => a.AtoNormativo.DataDocumento <= dataConclusao).ToList();

            var atosNormativos = atosNormativosEntidades.Select(a => a.AtoNormativo).ToList();

            AtoNormativoEntidadeDomainService.PreencherDadosAtoNormativo(atosNormativos, TOKEN_ASSUNTO_NORMATIVO.AUTORIZACAO, retorno.Autorizacao, exibirErro);
            AtoNormativoEntidadeDomainService.PreencherDadosAtoNormativo(atosNormativos, TOKEN_ASSUNTO_NORMATIVO.RECONHECIMENTO, retorno.Reconhecimento, exibirErro);
            AtoNormativoEntidadeDomainService.PreencherDadosAtoNormativo(atosNormativos, TOKEN_ASSUNTO_NORMATIVO.RENOVACAO_RECONHECIMENTO, retorno.RenovacaoReconhecimento, exibirErro);

            retorno.AtoAutorizacaoCurso = retorno.Autorizacao.Descricao;
            retorno.AtoReconhecimentoCurso = retorno.Reconhecimento.Descricao;
            retorno.AtoRenovacaoReconhecimentoCurso = retorno.RenovacaoReconhecimento.Descricao;

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarTiposDocumentoDocumentacaoComprobatoriaSelect()
        {
            var listaDocumentacaoComprobatoria = BuscarTiposDocumentoDocumentacaoComprobatoria();
            var retorno = listaDocumentacaoComprobatoria.Select(a => new SMCDatasourceItem()
            {
                Seq = a.SeqTipoDocumento,
                Descricao = a.DescricaoTipoDocumento
            }).ToList();

            retorno = retorno.OrderBy(o => o.Descricao).ToList();

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarTiposDocumentoRequeridoPorEtapaSelect(long seqSolicitacaoServico)
        {
            var seqConfiguracaoEtapa = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoDocumentoConclusao>(seqSolicitacaoServico),
                                                                  x => x.SituacaoAtual.SolicitacaoServicoEtapa.SeqConfiguracaoEtapa);

            var specDocumentoRequerido = new DocumentoRequeridoFilterSpecification() { SeqConfiguracaoEtapa = seqConfiguracaoEtapa };

            var documentosRequeridos = this.DocumentoRequeridoDomainService.SearchProjectionBySpecification(specDocumentoRequerido, x => new SMCDatasourceItem()
            {
                Seq = x.SeqTipoDocumento
            }).ToList();

            documentosRequeridos.ForEach(a => a.Descricao = this.TipoDocumentoService.BuscarTipoDocumento(a.Seq).Descricao);
            documentosRequeridos = documentosRequeridos.OrderBy(o => o.Descricao).ToList();

            return documentosRequeridos;
        }

        public List<DocumentacaoComprobatoriaTipoDocumentoVO> BuscarTiposDocumentoDocumentacaoComprobatoria()
        {
            var listaDocumentacaoComprobatoriaRetorno = new List<DocumentacaoComprobatoriaTipoDocumentoVO>();
            var listaDocumentacaoComprobatoriaAuxiliar = new List<DocumentacaoComprobatoriaTipoDocumentoVO>()
            {
                 new DocumentacaoComprobatoriaTipoDocumentoVO()
                 {
                    DescricaoTipoDocumento = "Ato naturalização",
                    TokenTipoDocumento =  TOKEN_TIPO_DOCUMENTO_EMISSAO_DIPLOMA.ATO_NATURALIZACAO,
                    ValorEnumLacuna = "AtoNaturalizacao"
                 },
                 new DocumentacaoComprobatoriaTipoDocumentoVO()
                 {
                    DescricaoTipoDocumento = "Certidão casamento",
                    TokenTipoDocumento =  TOKEN_TIPO_DOCUMENTO_EMISSAO_DIPLOMA.CERTIDAO_DE_CASAMENTO,
                    ValorEnumLacuna = "CertidaoCasamento"
                 },
                 new DocumentacaoComprobatoriaTipoDocumentoVO()
                 {
                    DescricaoTipoDocumento = "Certidão nascimento",
                    TokenTipoDocumento =  TOKEN_TIPO_DOCUMENTO_EMISSAO_DIPLOMA.CERTIDAO_DE_NASCIMENTO,
                    ValorEnumLacuna = "CertidaoNascimento"
                 },
                 new DocumentacaoComprobatoriaTipoDocumentoVO()
                 {
                    DescricaoTipoDocumento = "Certificado de conclusão do Ensino Médio (frente e verso)",
                    TokenTipoDocumento =  TOKEN_TIPO_DOCUMENTO_EMISSAO_DIPLOMA.CERTIFICADO_DE_CONCLUSAO_DO_ENSINO_MEDIO_FRENTE_E_VERSO,
                    ValorEnumLacuna = "ProvaConclusaoEnsinoMedio"
                 },
                 new DocumentacaoComprobatoriaTipoDocumentoVO()
                 {
                    DescricaoTipoDocumento = "Comprovante de estágio curricular",
                    TokenTipoDocumento =  TOKEN_TIPO_DOCUMENTO_EMISSAO_DIPLOMA.COMPROVANTE_ESTAGIO_CURRICULAR,
                    ValorEnumLacuna = "ComprovacaoEstagioCurricular"
                 },
                 new DocumentacaoComprobatoriaTipoDocumentoVO()
                 {
                    DescricaoTipoDocumento = "Documentação comprobatória diploma digital",
                    TokenTipoDocumento =  TOKEN_TIPO_DOCUMENTO_EMISSAO_DIPLOMA.DOCUMENTACAO_COMPROBATORIA_DIPLOMA_DIGITAL,
                    ValorEnumLacuna = "Outros"
                 },
                 new DocumentacaoComprobatoriaTipoDocumentoVO()
                 {
                    DescricaoTipoDocumento = "Documento de identidade (frente e verso)",
                    TokenTipoDocumento =  TOKEN_TIPO_DOCUMENTO_EMISSAO_DIPLOMA.DOCUMENTO_DE_IDENTIDADE_FRENTE_E_VERSO,
                    ValorEnumLacuna = "DocumentoIdentidadeDoAluno"
                 },
                 new DocumentacaoComprobatoriaTipoDocumentoVO()
                 {
                    DescricaoTipoDocumento = "Histórico escolar",
                    TokenTipoDocumento =  TOKEN_TIPO_DOCUMENTO_EMISSAO_DIPLOMA.HISTORICO_ESCOLAR,
                    ValorEnumLacuna = "HistoricoEscolar"
                 },
                 new DocumentacaoComprobatoriaTipoDocumentoVO()
                 {
                    DescricaoTipoDocumento = "Prova de colação de grau",
                    TokenTipoDocumento =  TOKEN_TIPO_DOCUMENTO_EMISSAO_DIPLOMA.PROVA_COLACAO_GRAU,
                    ValorEnumLacuna = "ProvaColacao"
                 },
                 new DocumentacaoComprobatoriaTipoDocumentoVO()
                 {
                    DescricaoTipoDocumento = "Termo de responsabilidade",
                    TokenTipoDocumento =  TOKEN_TIPO_DOCUMENTO_EMISSAO_DIPLOMA.TERMO_RESPONSABILIDADE,
                    ValorEnumLacuna = "TermoResponsabilidade"
                 },
                 new DocumentacaoComprobatoriaTipoDocumentoVO()
                 {
                    DescricaoTipoDocumento = "Título de eleitor (frente e verso)",
                    TokenTipoDocumento =  TOKEN_TIPO_DOCUMENTO_EMISSAO_DIPLOMA.TITULO_DE_ELEITOR_FRENTE_E_VERSO,
                    ValorEnumLacuna = "TituloEleitor"
                 }
            };

            var tiposDocumentos = TipoDocumentoService.BuscarTiposDocumentos().ToList();

            foreach (var itemDocumentacaoAuxiliar in listaDocumentacaoComprobatoriaAuxiliar)
            {
                var tipoDocumento = tiposDocumentos.FirstOrDefault(a => a.Token == itemDocumentacaoAuxiliar.TokenTipoDocumento);

                if (tipoDocumento != null)
                {
                    var itemDocumentacaoRetorno = itemDocumentacaoAuxiliar.Transform<DocumentacaoComprobatoriaTipoDocumentoVO>();
                    itemDocumentacaoRetorno.SeqTipoDocumento = tipoDocumento.Seq;
                    listaDocumentacaoComprobatoriaRetorno.Add(itemDocumentacaoRetorno);
                }
            }

            return listaDocumentacaoComprobatoriaRetorno;
        }

        public List<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO> BuscarMensagens(SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemFiltroVO filtro)
        {
            var keySessionMensagemEmissao = string.Format(KEY_SESSION_MENSAGEM.KEY_SESSION_MENSAGEM_EMISSAO, filtro.SeqSolicitacaoServico);
            var listaMensagensSession = HttpContext.Current.Session[keySessionMensagemEmissao] as List<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO>;

            switch (filtro.TokenNivelEnsino)
            {
                case TOKEN_NIVEL_ENSINO.GRADUACAO:

                    if (listaMensagensSession == null)
                    {
                        //Se a session estiver nula: 
                        //Ou entrou na tela a primeira vez
                        //Ou é inserção e não passou nas condições para inserir registros   
                        //Ou é edição e não tinha nenhuma mensagem no banco
                        //Ou não foi adicionado nenhum registro

                        listaMensagensSession = new List<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO>();

                        if (!filtro.ExisteDocumentoConclusao)
                        {
                            var listaTokensTiposHistorico = new List<string>() { TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_FINAL, TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_2VIA, TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_PARCIAL };
                            if (filtro.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL)
                            {
                                //Inserindo mensagem de Nova Via Diploma nesse ponto porque se entrar na tela e corresponder as condições, já insere a mensagem
                                if (filtro.NumeroVia > 1 && !filtro.ReutilizarDados.GetValueOrDefault())
                                {
                                    var specTipoMensagemNovaVia = new TipoMensagemFilterSpecification() { Token = TOKEN_TIPO_MENSAGEM.DIPLOMA_NOVA_VIA };
                                    var tipoMensagemNovaVia = this.TipoMensagemDomainService.SearchByKey(specTipoMensagemNovaVia);

                                    if (tipoMensagemNovaVia != null)
                                    {
                                        var specInstituicaoNivelTipoMensagem = new InstituicaoNivelTipoMensagemFilterSpecification()
                                        {
                                            SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                            SeqTipoMensagem = tipoMensagemNovaVia.Seq,
                                            SeqInstituicaoNivel = filtro.SeqInstituicaoNivel
                                        };

                                        var dadosInstituicaoNivelTipoMensagem = InstituicaoNivelTipoMensagemDomainService.SearchByKey(specInstituicaoNivelTipoMensagem);
                                        if (dadosInstituicaoNivelTipoMensagem != null)
                                        {
                                            var dadosMerge = new Dictionary<string, string> { { "{{DESCRICAO_VIA_ANTERIOR}}", filtro.DescricaoViaAnterior } };

                                            string mensagemRetorno = dadosInstituicaoNivelTipoMensagem.MensagemPadrao;

                                            foreach (var key in dadosMerge.Keys)
                                                mensagemRetorno = mensagemRetorno.Replace(key, dadosMerge[key]);

                                            var sequencialMensagem = listaMensagensSession.Any() ? listaMensagensSession.Max(a => a.Seq) + 1 : 1;
                                            var item = new SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO()
                                            {
                                                Seq = sequencialMensagem, //Sequencial auxiliar para edição/exclusão como os valores ainda não estão salvos em banco
                                                SeqTipoMensagem = tipoMensagemNovaVia.Seq,
                                                DescricaoTipoMensagem = tipoMensagemNovaVia.Descricao,
                                                Descricao = mensagemRetorno,
                                                DataInicioVigencia = DateTime.Now,
                                                SeqSolicitacaoServico = filtro.SeqSolicitacaoServico,
                                                ExisteDocumentoConclusao = filtro.ExisteDocumentoConclusao,
                                                TokenTipoDocumentoAcademico = filtro.TokenTipoDocumentoAcademico,
                                                SeqTipoDocumentoSolicitado = filtro.SeqTipoDocumentoSolicitado,
                                                NumeroVia = filtro.NumeroVia,
                                                SeqPessoaAtuacao = filtro.SeqPessoaAtuacao,
                                                SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                                SeqInstituicaoNivel = filtro.SeqInstituicaoNivel,
                                                ReutilizarDados = filtro.ReutilizarDados,
                                                NomePais = filtro.NomePais,
                                                DescricaoViaAnterior = filtro.DescricaoViaAnterior,
                                                DescricaoViaAtual = filtro.DescricaoViaAtual,
                                                CodigoUnidadeSeo = filtro.CodigoUnidadeSeo,
                                                DescricaoGrauAcademico = filtro.DescricaoGrauAcademico,
                                                NomeAluno = filtro.NomeAluno,
                                                DocumentoAcademico = TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL
                                            };
                                            listaMensagensSession.Add(item);
                                        }
                                    }
                                }

                                //Inserindo mensagem de PEC G nesse ponto porque se entrar na tela e for PEC G, já insere a mensagem
                                var tokenFormaIngresso = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(filtro.SeqPessoaAtuacao), x => x.Historicos.FirstOrDefault(f => f.Atual).FormaIngresso.Token);
                                if (tokenFormaIngresso == TOKEN_FORMA_INGRESSO.PEC_PG || tokenFormaIngresso == TOKEN_FORMA_INGRESSO.CONVENIO_PEC_G)
                                {
                                    var specTipoMensagemConvenio = new TipoMensagemFilterSpecification() { Token = TOKEN_TIPO_MENSAGEM.DIPLOMA_CONVENIO_PEC };
                                    var tipoMensagemConvenio = this.TipoMensagemDomainService.SearchByKey(specTipoMensagemConvenio);

                                    if (tipoMensagemConvenio != null)
                                    {
                                        var specInstituicaoNivelTipoMensagem = new InstituicaoNivelTipoMensagemFilterSpecification()
                                        {
                                            SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                            SeqTipoMensagem = tipoMensagemConvenio.Seq,
                                            SeqInstituicaoNivel = filtro.SeqInstituicaoNivel
                                        };

                                        var dadosInstituicaoNivelTipoMensagem = InstituicaoNivelTipoMensagemDomainService.SearchByKey(specInstituicaoNivelTipoMensagem);
                                        if (dadosInstituicaoNivelTipoMensagem != null)
                                        {
                                            var dadosMerge = new Dictionary<string, string> { { "{{PAIS}}", filtro.NomePais } };

                                            string mensagemRetorno = dadosInstituicaoNivelTipoMensagem.MensagemPadrao;

                                            foreach (var key in dadosMerge.Keys)
                                                mensagemRetorno = mensagemRetorno.Replace(key, dadosMerge[key]);

                                            var sequencialMensagem = listaMensagensSession.Any() ? listaMensagensSession.Max(a => a.Seq) + 1 : 1;
                                            var item = new SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO()
                                            {
                                                Seq = sequencialMensagem, //Sequencial auxiliar para edição/exclusão como os valores ainda não estão salvos em banco
                                                SeqTipoMensagem = tipoMensagemConvenio.Seq,
                                                DescricaoTipoMensagem = tipoMensagemConvenio.Descricao,
                                                Descricao = mensagemRetorno,
                                                DataInicioVigencia = DateTime.Now,
                                                SeqSolicitacaoServico = filtro.SeqSolicitacaoServico,
                                                ExisteDocumentoConclusao = filtro.ExisteDocumentoConclusao,
                                                TokenTipoDocumentoAcademico = filtro.TokenTipoDocumentoAcademico,
                                                SeqTipoDocumentoSolicitado = filtro.SeqTipoDocumentoSolicitado,
                                                NumeroVia = filtro.NumeroVia,
                                                SeqPessoaAtuacao = filtro.SeqPessoaAtuacao,
                                                SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                                SeqInstituicaoNivel = filtro.SeqInstituicaoNivel,
                                                ReutilizarDados = filtro.ReutilizarDados,
                                                NomePais = filtro.NomePais,
                                                DescricaoViaAnterior = filtro.DescricaoViaAnterior,
                                                DescricaoViaAtual = filtro.DescricaoViaAtual,
                                                CodigoUnidadeSeo = filtro.CodigoUnidadeSeo,
                                                DescricaoGrauAcademico = filtro.DescricaoGrauAcademico,
                                                NomeAluno = filtro.NomeAluno,
                                                DocumentoAcademico = TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL
                                            };
                                            listaMensagensSession.Add(item);
                                        }
                                    }
                                }

                                //Inserindo mensagem de PUC Uberlandia nesse ponto porque se entrar na tela e for de Uberlandía, já insere a mensagem
                                if (filtro.CodigoUnidadeSeo.GetValueOrDefault() == UNIDADE_SEO.CODIGO_PUC_MINAS_UBERLANDIA)
                                {
                                    var specTipoMensagemPucUberlandia = new TipoMensagemFilterSpecification() { Token = TOKEN_TIPO_MENSAGEM.DIPLOMA_PUC_UBERLANDIA };
                                    var tipoMensagemPucUberlandia = this.TipoMensagemDomainService.SearchByKey(specTipoMensagemPucUberlandia);

                                    if (tipoMensagemPucUberlandia != null)
                                    {
                                        var specInstituicaoNivelTipoMensagem = new InstituicaoNivelTipoMensagemFilterSpecification()
                                        {
                                            SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                            SeqTipoMensagem = tipoMensagemPucUberlandia.Seq,
                                            SeqInstituicaoNivel = filtro.SeqInstituicaoNivel
                                        };

                                        var dadosInstituicaoNivelTipoMensagem = InstituicaoNivelTipoMensagemDomainService.SearchByKey(specInstituicaoNivelTipoMensagem);
                                        if (dadosInstituicaoNivelTipoMensagem != null)
                                        {
                                            string mensagemRetorno = dadosInstituicaoNivelTipoMensagem.MensagemPadrao;

                                            var sequencialMensagem = listaMensagensSession.Any() ? listaMensagensSession.Max(a => a.Seq) + 1 : 1;

                                            var item = new SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO()
                                            {
                                                Seq = sequencialMensagem, //Sequencial auxiliar para edição/exclusão como os valores ainda não estão salvos em banco
                                                SeqTipoMensagem = tipoMensagemPucUberlandia.Seq,
                                                DescricaoTipoMensagem = tipoMensagemPucUberlandia.Descricao,
                                                Descricao = mensagemRetorno,
                                                DataInicioVigencia = DateTime.Now,
                                                SeqSolicitacaoServico = filtro.SeqSolicitacaoServico,
                                                ExisteDocumentoConclusao = filtro.ExisteDocumentoConclusao,
                                                TokenTipoDocumentoAcademico = filtro.TokenTipoDocumentoAcademico,
                                                SeqTipoDocumentoSolicitado = filtro.SeqTipoDocumentoSolicitado,
                                                NumeroVia = filtro.NumeroVia,
                                                SeqPessoaAtuacao = filtro.SeqPessoaAtuacao,
                                                SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                                SeqInstituicaoNivel = filtro.SeqInstituicaoNivel,
                                                ReutilizarDados = filtro.ReutilizarDados,
                                                NomePais = filtro.NomePais,
                                                DescricaoViaAnterior = filtro.DescricaoViaAnterior,
                                                DescricaoViaAtual = filtro.DescricaoViaAtual,
                                                CodigoUnidadeSeo = filtro.CodigoUnidadeSeo,
                                                DescricaoGrauAcademico = filtro.DescricaoGrauAcademico,
                                                NomeAluno = filtro.NomeAluno,
                                                DocumentoAcademico = TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL
                                            };
                                            listaMensagensSession.Add(item);
                                        }
                                    }
                                }

                                //Inserindo mensagem de Bacharelado Licenciatura nesse ponto porque se entrar na tela e for desse grau acadêmico, já insere a mensagem
                                if (filtro.DescricaoGrauAcademico == GRAU_ACADEMICO.BachareladoLicenciatura)
                                {
                                    var specTipoMensagemBachareladoLicenciatura = new TipoMensagemFilterSpecification() { Token = TOKEN_TIPO_MENSAGEM.DIPLOMA_BACHARELADO_LICENCIATURA };
                                    var tipoMensagemBachareladoLicenciatura = this.TipoMensagemDomainService.SearchByKey(specTipoMensagemBachareladoLicenciatura);

                                    if (tipoMensagemBachareladoLicenciatura != null)
                                    {
                                        var specInstituicaoNivelTipoMensagem = new InstituicaoNivelTipoMensagemFilterSpecification()
                                        {
                                            SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                            SeqTipoMensagem = tipoMensagemBachareladoLicenciatura.Seq,
                                            SeqInstituicaoNivel = filtro.SeqInstituicaoNivel
                                        };

                                        var dadosInstituicaoNivelTipoMensagem = InstituicaoNivelTipoMensagemDomainService.SearchByKey(specInstituicaoNivelTipoMensagem);
                                        if (dadosInstituicaoNivelTipoMensagem != null)
                                        {
                                            var dadosMerge = new Dictionary<string, string> { { "{{DESCRICAO_VIA_RELACIONADA}}", filtro.DescricaoViaAtual } };

                                            string mensagemRetorno = dadosInstituicaoNivelTipoMensagem.MensagemPadrao;

                                            foreach (var key in dadosMerge.Keys)
                                                mensagemRetorno = mensagemRetorno.Replace(key, dadosMerge[key]);

                                            var sequencialMensagem = listaMensagensSession.Any() ? listaMensagensSession.Max(a => a.Seq) + 1 : 1;

                                            var item = new SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO()
                                            {
                                                Seq = sequencialMensagem, //Sequencial auxiliar para edição/exclusão como os valores ainda não estão salvos em banco
                                                SeqTipoMensagem = tipoMensagemBachareladoLicenciatura.Seq,
                                                DescricaoTipoMensagem = tipoMensagemBachareladoLicenciatura.Descricao,
                                                Descricao = mensagemRetorno,
                                                DataInicioVigencia = DateTime.Now,
                                                SeqSolicitacaoServico = filtro.SeqSolicitacaoServico,
                                                ExisteDocumentoConclusao = filtro.ExisteDocumentoConclusao,
                                                TokenTipoDocumentoAcademico = filtro.TokenTipoDocumentoAcademico,
                                                SeqTipoDocumentoSolicitado = filtro.SeqTipoDocumentoSolicitado,
                                                NumeroVia = filtro.NumeroVia,
                                                SeqPessoaAtuacao = filtro.SeqPessoaAtuacao,
                                                SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                                SeqInstituicaoNivel = filtro.SeqInstituicaoNivel,
                                                ReutilizarDados = filtro.ReutilizarDados,
                                                NomePais = filtro.NomePais,
                                                DescricaoViaAnterior = filtro.DescricaoViaAnterior,
                                                DescricaoViaAtual = filtro.DescricaoViaAtual,
                                                CodigoUnidadeSeo = filtro.CodigoUnidadeSeo,
                                                DescricaoGrauAcademico = filtro.DescricaoGrauAcademico,
                                                NomeAluno = filtro.NomeAluno,
                                                DocumentoAcademico = TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL
                                            };
                                            listaMensagensSession.Add(item);
                                        }
                                    }
                                }
                            }
                            else if (listaTokensTiposHistorico.Contains(filtro.TokenTipoDocumentoAcademico))
                            {
                                if (filtro.NumeroVia > 1 && !filtro.ReutilizarDados.GetValueOrDefault())
                                {
                                    var specTipoMensagemNovaVia = new TipoMensagemFilterSpecification() { Token = TOKEN_TIPO_MENSAGEM.HISTORICO_ESCOLAR_NOVA_VIA };
                                    var tipoMensagemNovaVia = this.TipoMensagemDomainService.SearchByKey(specTipoMensagemNovaVia);

                                    if (tipoMensagemNovaVia != null)
                                    {
                                        var specInstituicaoNivelTipoMensagem = new InstituicaoNivelTipoMensagemFilterSpecification()
                                        {
                                            SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                            SeqTipoMensagem = tipoMensagemNovaVia.Seq,
                                            SeqInstituicaoNivel = filtro.SeqInstituicaoNivel
                                        };

                                        var dadosInstituicaoNivelTipoMensagem = InstituicaoNivelTipoMensagemDomainService.SearchByKey(specInstituicaoNivelTipoMensagem);

                                        if (dadosInstituicaoNivelTipoMensagem != null)
                                        {
                                            var dadosMerge = new Dictionary<string, string> { { "{{DESCRICAO_VIA_ANTERIOR}}", filtro.DescricaoViaAnterior } };

                                            string mensagemRetorno = dadosInstituicaoNivelTipoMensagem.MensagemPadrao;

                                            foreach (var key in dadosMerge.Keys)
                                                mensagemRetorno = mensagemRetorno.Replace(key, dadosMerge[key]);

                                            var sequencialMensagem = listaMensagensSession.Any() ? listaMensagensSession.Max(a => a.Seq) + 1 : 1;

                                            var item = new SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO()
                                            {
                                                Seq = sequencialMensagem, //Sequencial auxiliar para edição/exclusão como os valores ainda não estão salvos em banco
                                                SeqTipoMensagem = tipoMensagemNovaVia.Seq,
                                                DescricaoTipoMensagem = tipoMensagemNovaVia.Descricao,
                                                Descricao = mensagemRetorno,
                                                DataInicioVigencia = DateTime.Now,
                                                SeqSolicitacaoServico = filtro.SeqSolicitacaoServico,
                                                ExisteDocumentoConclusao = filtro.ExisteDocumentoConclusao,
                                                TokenTipoDocumentoAcademico = filtro.TokenTipoDocumentoAcademico,
                                                SeqTipoDocumentoSolicitado = filtro.SeqTipoDocumentoSolicitado,
                                                NumeroVia = filtro.NumeroVia,
                                                SeqPessoaAtuacao = filtro.SeqPessoaAtuacao,
                                                SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                                SeqInstituicaoNivel = filtro.SeqInstituicaoNivel,
                                                ReutilizarDados = filtro.ReutilizarDados,
                                                NomePais = filtro.NomePais,
                                                DescricaoViaAnterior = filtro.DescricaoViaAnterior,
                                                DescricaoViaAtual = filtro.DescricaoViaAtual,
                                                CodigoUnidadeSeo = filtro.CodigoUnidadeSeo,
                                                DescricaoGrauAcademico = filtro.DescricaoGrauAcademico,
                                                NomeAluno = filtro.NomeAluno,
                                                DocumentoAcademico = TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR
                                            };
                                            listaMensagensSession.Add(item);
                                        }
                                    }
                                }
                            }

                            if (filtro.ExibirNomeSocial)
                            {
                                var specTipoMensagemNomeSocial = new TipoMensagemFilterSpecification() { Token = TOKEN_TIPO_MENSAGEM.NOME_SOCIAL_AUTORIZADO_CAD };
                                var tipoMensagemNomeSocial = this.TipoMensagemDomainService.SearchByKey(specTipoMensagemNomeSocial);

                                if (tipoMensagemNomeSocial != null)
                                {
                                    var specInstituicaoNivelTipoMensagem = new InstituicaoNivelTipoMensagemFilterSpecification()
                                    {
                                        SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                        SeqTipoMensagem = tipoMensagemNomeSocial.Seq,
                                        SeqInstituicaoNivel = filtro.SeqInstituicaoNivel
                                    };
                                    var dadosInstituicaoNivelTipoMensagem = InstituicaoNivelTipoMensagemDomainService.SearchByKey(specInstituicaoNivelTipoMensagem);

                                    if (dadosInstituicaoNivelTipoMensagem != null)
                                    {
                                        var dadosMerge = new Dictionary<string, string> { { "{{NOME_COMPLETO}}", filtro.NomeAluno } };

                                        string mensagemRetorno = dadosInstituicaoNivelTipoMensagem.MensagemPadrao;
                                        foreach (var key in dadosMerge.Keys)
                                            mensagemRetorno = mensagemRetorno.Replace(key, dadosMerge[key]);

                                        var sequencialMensagem = listaMensagensSession.Any() ? listaMensagensSession.Max(a => a.Seq) + 1 : 1;
                                        var item = new SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO()
                                        {
                                            Seq = sequencialMensagem, //Sequencial auxiliar para edição/exclusão como os valores ainda não estão salvos em banco
                                            SeqTipoMensagem = tipoMensagemNomeSocial.Seq,
                                            DescricaoTipoMensagem = tipoMensagemNomeSocial.Descricao,
                                            Descricao = mensagemRetorno,
                                            DataInicioVigencia = DateTime.Now,
                                            SeqSolicitacaoServico = filtro.SeqSolicitacaoServico,
                                            ExisteDocumentoConclusao = filtro.ExisteDocumentoConclusao,
                                            TokenTipoDocumentoAcademico = filtro.TokenTipoDocumentoAcademico,
                                            SeqTipoDocumentoSolicitado = filtro.SeqTipoDocumentoSolicitado,
                                            NumeroVia = filtro.NumeroVia,
                                            SeqPessoaAtuacao = filtro.SeqPessoaAtuacao,
                                            SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                            SeqInstituicaoNivel = filtro.SeqInstituicaoNivel,
                                            ReutilizarDados = filtro.ReutilizarDados,
                                            NomePais = filtro.NomePais,
                                            DescricaoViaAnterior = filtro.DescricaoViaAnterior,
                                            DescricaoViaAtual = filtro.DescricaoViaAtual,
                                            CodigoUnidadeSeo = filtro.CodigoUnidadeSeo,
                                            DescricaoGrauAcademico = filtro.DescricaoGrauAcademico,
                                            NomeAluno = filtro.NomeAluno,
                                            DocumentoAcademico = filtro.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL ? TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL : TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR
                                        };
                                        listaMensagensSession.Add(item);

                                        if (filtro.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL && filtro.NumeroVia == 1)
                                        {
                                            var itemHistorico = new SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO()
                                            {
                                                Seq = sequencialMensagem, //Sequencial auxiliar para edição/exclusão como os valores ainda não estão salvos em banco
                                                SeqTipoMensagem = tipoMensagemNomeSocial.Seq,
                                                DescricaoTipoMensagem = tipoMensagemNomeSocial.Descricao,
                                                Descricao = mensagemRetorno,
                                                DataInicioVigencia = DateTime.Now,
                                                SeqSolicitacaoServico = filtro.SeqSolicitacaoServico,
                                                ExisteDocumentoConclusao = filtro.ExisteDocumentoConclusao,
                                                TokenTipoDocumentoAcademico = filtro.TokenTipoDocumentoAcademico,
                                                SeqTipoDocumentoSolicitado = filtro.SeqTipoDocumentoSolicitado,
                                                NumeroVia = filtro.NumeroVia,
                                                SeqPessoaAtuacao = filtro.SeqPessoaAtuacao,
                                                SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                                SeqInstituicaoNivel = filtro.SeqInstituicaoNivel,
                                                ReutilizarDados = filtro.ReutilizarDados,
                                                NomePais = filtro.NomePais,
                                                DescricaoViaAnterior = filtro.DescricaoViaAnterior,
                                                DescricaoViaAtual = filtro.DescricaoViaAtual,
                                                CodigoUnidadeSeo = filtro.CodigoUnidadeSeo,
                                                DescricaoGrauAcademico = filtro.DescricaoGrauAcademico,
                                                NomeAluno = filtro.NomeAluno,
                                                DocumentoAcademico = TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR
                                            };
                                            listaMensagensSession.Add(itemHistorico);
                                        }
                                    }
                                }
                            }

                            if (filtro.CodigoAlunoMigracao.HasValue)
                            {
                                var specTipoMensagemLegado = new TipoMensagemFilterSpecification() { Token = TOKEN_TIPO_MENSAGEM.DIPLOMA_HISTORICO_OBSERVACAO_LEGADO };
                                var tipoMensagemLegado = this.TipoMensagemDomainService.SearchByKey(specTipoMensagemLegado);

                                if (tipoMensagemLegado != null)
                                {
                                    var mensagensLegado = IntegracaoAcademicoService.BuscarMensagens(filtro.CodigoAlunoMigracao.Value);

                                    foreach (var mensagemLegado in mensagensLegado)
                                    {
                                        var sequencialMensagem = listaMensagensSession.Any() ? listaMensagensSession.Max(a => a.Seq) + 1 : 1;
                                        var item = new SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO()
                                        {
                                            Seq = sequencialMensagem, //Sequencial auxiliar para edição/exclusão como os valores ainda não estão salvos em banco
                                            SeqTipoMensagem = tipoMensagemLegado.Seq,
                                            DescricaoTipoMensagem = tipoMensagemLegado.Descricao,
                                            Descricao = mensagemLegado.Mensagem,
                                            DataInicioVigencia = DateTime.Now,
                                            SeqSolicitacaoServico = filtro.SeqSolicitacaoServico,
                                            ExisteDocumentoConclusao = filtro.ExisteDocumentoConclusao,
                                            TokenTipoDocumentoAcademico = mensagemLegado.Tipo == "DD" ? TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL : TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR,
                                            SeqTipoDocumentoSolicitado = filtro.SeqTipoDocumentoSolicitado,
                                            NumeroVia = filtro.NumeroVia,
                                            SeqPessoaAtuacao = filtro.SeqPessoaAtuacao,
                                            SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                            SeqInstituicaoNivel = filtro.SeqInstituicaoNivel,
                                            ReutilizarDados = filtro.ReutilizarDados,
                                            NomePais = filtro.NomePais,
                                            DescricaoViaAnterior = filtro.DescricaoViaAnterior,
                                            DescricaoViaAtual = filtro.DescricaoViaAtual,
                                            CodigoUnidadeSeo = filtro.CodigoUnidadeSeo,
                                            DescricaoGrauAcademico = filtro.DescricaoGrauAcademico,
                                            NomeAluno = filtro.NomeAluno,
                                            DocumentoAcademico = mensagemLegado.Tipo == "DD" ? TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL : TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR
                                        };

                                        listaMensagensSession.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var documentoConclusao = this.SearchProjectionByKey(filtro.SeqSolicitacaoServico, x => x.DocumentosAcademicos.FirstOrDefault());

                            if (documentoConclusao != null)
                            {
                                var spec = new DocumentoConclusaoMensagemFilterSpecification() { SeqDocumentoConclusao = documentoConclusao.Seq };
                                var mensagensDocumento = this.DocumentoConclusaoMensagemDomainService.SearchBySpecification(spec, x => x.Mensagem.TipoMensagem.TiposUso).ToList();

                                foreach (var mensagemDocumento in mensagensDocumento)
                                {
                                    if (mensagemDocumento.Mensagem.TipoMensagem.CategoriaMensagem == CategoriaMensagem.Documento && mensagemDocumento.Mensagem.TipoMensagem.TiposUso != null && mensagemDocumento.Mensagem.TipoMensagem.TiposUso.Any(a => a.TipoUsoMensagem == TipoUsoMensagem.Diploma))
                                    {
                                        var item = new SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO()
                                        {
                                            Seq = mensagemDocumento.Mensagem.Seq,
                                            SeqTipoMensagem = mensagemDocumento.Mensagem.SeqTipoMensagem,
                                            DescricaoTipoMensagem = mensagemDocumento.Mensagem.TipoMensagem.Descricao,
                                            Descricao = mensagemDocumento.Mensagem.Descricao,
                                            DataInicioVigencia = DateTime.Now,
                                            SeqSolicitacaoServico = filtro.SeqSolicitacaoServico,
                                            ExisteDocumentoConclusao = filtro.ExisteDocumentoConclusao,
                                            TokenTipoDocumentoAcademico = filtro.TokenTipoDocumentoAcademico,
                                            SeqTipoDocumentoSolicitado = filtro.SeqTipoDocumentoSolicitado,
                                            NumeroVia = filtro.NumeroVia,
                                            SeqPessoaAtuacao = filtro.SeqPessoaAtuacao,
                                            SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                            SeqInstituicaoNivel = filtro.SeqInstituicaoNivel,
                                            ReutilizarDados = filtro.ReutilizarDados,
                                            NomePais = filtro.NomePais,
                                            DescricaoViaAnterior = filtro.DescricaoViaAnterior,
                                            DescricaoViaAtual = filtro.DescricaoViaAtual,
                                            CodigoUnidadeSeo = filtro.CodigoUnidadeSeo,
                                            DescricaoGrauAcademico = filtro.DescricaoGrauAcademico,
                                            NomeAluno = filtro.NomeAluno
                                        };
                                        listaMensagensSession.Add(item);
                                    }
                                }
                            }
                        }
                    }

                    if (!filtro.ExisteDocumentoConclusao)
                    {
                        if (filtro.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL)
                        {
                            var specTipoMensagemNovaVia = new TipoMensagemFilterSpecification() { Token = TOKEN_TIPO_MENSAGEM.DIPLOMA_NOVA_VIA };
                            var tipoMensagemNovaVia = this.TipoMensagemDomainService.SearchByKey(specTipoMensagemNovaVia);

                            if (tipoMensagemNovaVia != null)
                            {
                                var mensagemNovaVia = listaMensagensSession.FirstOrDefault(a => a.SeqTipoMensagem == tipoMensagemNovaVia.Seq);

                                //Inserindo mensagem de Nova Via Diploma Digital nesse ponto porque depende do que o usuário selecionar na tela no campo 'Reutilizar Dados'
                                //Se corresponder as condições para incluir mensagem de nova via, verifica se já tem na lista para incluir
                                if (filtro.NumeroVia > 1 && filtro.ReutilizarDados.HasValue && !filtro.ReutilizarDados.GetValueOrDefault())
                                {
                                    if (mensagemNovaVia == null)
                                    {
                                        var specInstituicaoNivelTipoMensagem = new InstituicaoNivelTipoMensagemFilterSpecification()
                                        {
                                            SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                            SeqTipoMensagem = tipoMensagemNovaVia.Seq,
                                            SeqInstituicaoNivel = filtro.SeqInstituicaoNivel
                                        };

                                        var dadosInstituicaoNivelTipoMensagem = InstituicaoNivelTipoMensagemDomainService.SearchByKey(specInstituicaoNivelTipoMensagem);

                                        if (dadosInstituicaoNivelTipoMensagem != null)
                                        {
                                            var dadosMerge = new Dictionary<string, string> { { "{{DESCRICAO_VIA_ANTERIOR}}", filtro.DescricaoViaAnterior } };

                                            string mensagemRetorno = dadosInstituicaoNivelTipoMensagem.MensagemPadrao;

                                            foreach (var key in dadosMerge.Keys)
                                                mensagemRetorno = mensagemRetorno.Replace(key, dadosMerge[key]);

                                            var sequencialMensagem = listaMensagensSession.Any() ? listaMensagensSession.Max(a => a.Seq) + 1 : 1;

                                            var item = new SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO()
                                            {
                                                Seq = sequencialMensagem, //Sequencial auxiliar para edição/exclusão como os valores ainda não estão salvos em banco
                                                SeqTipoMensagem = tipoMensagemNovaVia.Seq,
                                                DescricaoTipoMensagem = tipoMensagemNovaVia.Descricao,
                                                Descricao = mensagemRetorno,
                                                DataInicioVigencia = DateTime.Now,
                                                SeqSolicitacaoServico = filtro.SeqSolicitacaoServico,
                                                ExisteDocumentoConclusao = filtro.ExisteDocumentoConclusao,
                                                TokenTipoDocumentoAcademico = filtro.TokenTipoDocumentoAcademico,
                                                SeqTipoDocumentoSolicitado = filtro.SeqTipoDocumentoSolicitado,
                                                NumeroVia = filtro.NumeroVia,
                                                SeqPessoaAtuacao = filtro.SeqPessoaAtuacao,
                                                SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                                SeqInstituicaoNivel = filtro.SeqInstituicaoNivel,
                                                ReutilizarDados = filtro.ReutilizarDados,
                                                NomePais = filtro.NomePais,
                                                DescricaoViaAnterior = filtro.DescricaoViaAnterior,
                                                DescricaoViaAtual = filtro.DescricaoViaAtual,
                                                CodigoUnidadeSeo = filtro.CodigoUnidadeSeo,
                                                DescricaoGrauAcademico = filtro.DescricaoGrauAcademico,
                                                NomeAluno = filtro.NomeAluno
                                            };

                                            listaMensagensSession.Add(item);
                                        }
                                    }
                                }
                                //Se não corresponder as condições para incluir mensagem de nova via, tem que retirar se já tiver na lista
                                else
                                {
                                    if (mensagemNovaVia != null)
                                        listaMensagensSession.Remove(mensagemNovaVia);
                                }
                            }
                        }

                        if (filtro.ExibirNomeSocial)
                        {
                            var specTipoMensagemNomeSocial = new TipoMensagemFilterSpecification() { Token = TOKEN_TIPO_MENSAGEM.NOME_SOCIAL_AUTORIZADO_CAD };
                            var tipoMensagemNomeSocial = this.TipoMensagemDomainService.SearchByKey(specTipoMensagemNomeSocial);

                            if (tipoMensagemNomeSocial != null)
                            {
                                var mensagemNomeSocial = listaMensagensSession.FirstOrDefault(a => a.SeqTipoMensagem == tipoMensagemNomeSocial.Seq);

                                if (mensagemNomeSocial == null)
                                {
                                    var specInstituicaoNivelTipoMensagem = new InstituicaoNivelTipoMensagemFilterSpecification()
                                    {
                                        SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                        SeqTipoMensagem = tipoMensagemNomeSocial.Seq,
                                        SeqInstituicaoNivel = filtro.SeqInstituicaoNivel
                                    };
                                    var dadosInstituicaoNivelTipoMensagem = InstituicaoNivelTipoMensagemDomainService.SearchByKey(specInstituicaoNivelTipoMensagem);

                                    if (dadosInstituicaoNivelTipoMensagem != null)
                                    {
                                        var dadosMerge = new Dictionary<string, string> { { "{{NOME_COMPLETO}}", filtro.NomeAluno } };

                                        string mensagemRetorno = dadosInstituicaoNivelTipoMensagem.MensagemPadrao;
                                        foreach (var key in dadosMerge.Keys)
                                            mensagemRetorno = mensagemRetorno.Replace(key, dadosMerge[key]);

                                        var sequencialMensagem = listaMensagensSession.Any() ? listaMensagensSession.Max(a => a.Seq) + 1 : 1;
                                        var item = new SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO()
                                        {
                                            Seq = sequencialMensagem, //Sequencial auxiliar para edição/exclusão como os valores ainda não estão salvos em banco
                                            SeqTipoMensagem = tipoMensagemNomeSocial.Seq,
                                            DescricaoTipoMensagem = tipoMensagemNomeSocial.Descricao,
                                            Descricao = mensagemRetorno,
                                            DataInicioVigencia = DateTime.Now,
                                            SeqSolicitacaoServico = filtro.SeqSolicitacaoServico,
                                            ExisteDocumentoConclusao = filtro.ExisteDocumentoConclusao,
                                            TokenTipoDocumentoAcademico = filtro.TokenTipoDocumentoAcademico,
                                            SeqTipoDocumentoSolicitado = filtro.SeqTipoDocumentoSolicitado,
                                            NumeroVia = filtro.NumeroVia,
                                            SeqPessoaAtuacao = filtro.SeqPessoaAtuacao,
                                            SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                            SeqInstituicaoNivel = filtro.SeqInstituicaoNivel,
                                            ReutilizarDados = filtro.ReutilizarDados,
                                            NomePais = filtro.NomePais,
                                            DescricaoViaAnterior = filtro.DescricaoViaAnterior,
                                            DescricaoViaAtual = filtro.DescricaoViaAtual,
                                            CodigoUnidadeSeo = filtro.CodigoUnidadeSeo,
                                            DescricaoGrauAcademico = filtro.DescricaoGrauAcademico,
                                            NomeAluno = filtro.NomeAluno,
                                            DocumentoAcademico = filtro.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL ? TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL : TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR
                                        };
                                        listaMensagensSession.Add(item);

                                        if (filtro.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL && filtro.NumeroVia == 1)
                                        {
                                            var itemHistorico = new SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO()
                                            {
                                                Seq = sequencialMensagem, //Sequencial auxiliar para edição/exclusão como os valores ainda não estão salvos em banco
                                                SeqTipoMensagem = tipoMensagemNomeSocial.Seq,
                                                DescricaoTipoMensagem = tipoMensagemNomeSocial.Descricao,
                                                Descricao = mensagemRetorno,
                                                DataInicioVigencia = DateTime.Now,
                                                SeqSolicitacaoServico = filtro.SeqSolicitacaoServico,
                                                ExisteDocumentoConclusao = filtro.ExisteDocumentoConclusao,
                                                TokenTipoDocumentoAcademico = filtro.TokenTipoDocumentoAcademico,
                                                SeqTipoDocumentoSolicitado = filtro.SeqTipoDocumentoSolicitado,
                                                NumeroVia = filtro.NumeroVia,
                                                SeqPessoaAtuacao = filtro.SeqPessoaAtuacao,
                                                SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                                SeqInstituicaoNivel = filtro.SeqInstituicaoNivel,
                                                ReutilizarDados = filtro.ReutilizarDados,
                                                NomePais = filtro.NomePais,
                                                DescricaoViaAnterior = filtro.DescricaoViaAnterior,
                                                DescricaoViaAtual = filtro.DescricaoViaAtual,
                                                CodigoUnidadeSeo = filtro.CodigoUnidadeSeo,
                                                DescricaoGrauAcademico = filtro.DescricaoGrauAcademico,
                                                NomeAluno = filtro.NomeAluno,
                                                DocumentoAcademico = TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR
                                            };
                                            listaMensagensSession.Add(itemHistorico);
                                        }
                                    }
                                }
                            }
                        }

                        if (filtro.CodigoAlunoMigracao.HasValue)
                        {
                            var specTipoMensagemLegado = new TipoMensagemFilterSpecification() { Token = TOKEN_TIPO_MENSAGEM.DIPLOMA_HISTORICO_OBSERVACAO_LEGADO };
                            var tipoMensagemLegado = this.TipoMensagemDomainService.SearchByKey(specTipoMensagemLegado);

                            if (tipoMensagemLegado != null)
                            {
                                var mensagens = listaMensagensSession.Where(a => a.SeqTipoMensagem == tipoMensagemLegado.Seq).ToList();

                                if (mensagens != null)
                                {
                                    foreach (var mensagem in mensagens)
                                        listaMensagensSession.Remove(mensagem);
                                }

                                var mensagensLegado = IntegracaoAcademicoService.BuscarMensagens(filtro.CodigoAlunoMigracao.Value);

                                foreach (var mensagemLegado in mensagensLegado)
                                {
                                    var sequencialMensagem = listaMensagensSession.Any() ? listaMensagensSession.Max(a => a.Seq) + 1 : 1;
                                    var item = new SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO()
                                    {
                                        Seq = sequencialMensagem, //Sequencial auxiliar para edição/exclusão como os valores ainda não estão salvos em banco
                                        SeqTipoMensagem = tipoMensagemLegado.Seq,
                                        DescricaoTipoMensagem = tipoMensagemLegado.Descricao,
                                        Descricao = mensagemLegado.Mensagem,
                                        DataInicioVigencia = DateTime.Now,
                                        SeqSolicitacaoServico = filtro.SeqSolicitacaoServico,
                                        ExisteDocumentoConclusao = filtro.ExisteDocumentoConclusao,
                                        TokenTipoDocumentoAcademico = mensagemLegado.Tipo == "DD" ? TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL : TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR,
                                        SeqTipoDocumentoSolicitado = filtro.SeqTipoDocumentoSolicitado,
                                        NumeroVia = filtro.NumeroVia,
                                        SeqPessoaAtuacao = filtro.SeqPessoaAtuacao,
                                        SeqInstituicaoEnsino = filtro.SeqInstituicaoEnsino,
                                        SeqInstituicaoNivel = filtro.SeqInstituicaoNivel,
                                        ReutilizarDados = filtro.ReutilizarDados,
                                        NomePais = filtro.NomePais,
                                        DescricaoViaAnterior = filtro.DescricaoViaAnterior,
                                        DescricaoViaAtual = filtro.DescricaoViaAtual,
                                        CodigoUnidadeSeo = filtro.CodigoUnidadeSeo,
                                        DescricaoGrauAcademico = filtro.DescricaoGrauAcademico,
                                        NomeAluno = filtro.NomeAluno,
                                        DocumentoAcademico = mensagemLegado.Tipo == "DD" ? TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL : TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR
                                    };

                                    listaMensagensSession.Add(item);
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            foreach (var mensagem in listaMensagensSession)
            {
                mensagem.ReutilizarDados = filtro.ReutilizarDados;

                var htmlDescricao = HttpUtility.HtmlDecode(mensagem.Descricao);
                mensagem.DescricaoDecode = Regex.Replace(htmlDescricao, "<.*?>", string.Empty);

                var tipoMensagem = this.TipoMensagemDomainService.SearchByKey(new SMCSeqSpecification<TipoMensagem>(mensagem.SeqTipoMensagem));

                mensagem.HabilitarBotaoEditar = !filtro.ExisteDocumentoConclusao && tipoMensagem.PermiteCadastroManual;
                mensagem.HabilitarBotaoExcluir = !filtro.ExisteDocumentoConclusao && tipoMensagem.PermiteCadastroManual;
            }

            HttpContext.Current.Session[keySessionMensagemEmissao] = listaMensagensSession;

            return listaMensagensSession;
        }

        public SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemVO BuscarMensagem(long seq, long seqPessoaAtuacao, long seqSolicitacaoServico)
        {
            var keySessionMensagemEmissao = string.Format(KEY_SESSION_MENSAGEM.KEY_SESSION_MENSAGEM_EMISSAO, seqSolicitacaoServico);
            var listaMensagensSession = HttpContext.Current.Session[keySessionMensagemEmissao] as List<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO>;

            var mensagem = listaMensagensSession.FirstOrDefault(a => a.Seq == seq);

            var retorno = new SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemVO()
            {
                Seq = mensagem.Seq,
                SeqTipoMensagem = mensagem.SeqTipoMensagem,
                Descricao = mensagem.Descricao,
                DataInicioVigencia = mensagem.DataInicioVigencia,
                SeqSolicitacaoServico = mensagem.SeqSolicitacaoServico,
                ExisteDocumentoConclusao = mensagem.ExisteDocumentoConclusao,
                TokenTipoDocumentoAcademico = mensagem.TokenTipoDocumentoAcademico,
                SeqTipoDocumentoSolicitado = mensagem.SeqTipoDocumentoSolicitado,
                NumeroVia = mensagem.NumeroVia,
                SeqPessoaAtuacao = mensagem.SeqPessoaAtuacao,
                SeqInstituicaoEnsino = mensagem.SeqInstituicaoEnsino,
                SeqInstituicaoNivel = mensagem.SeqInstituicaoNivel,
                ReutilizarDados = mensagem.ReutilizarDados,
                NomePais = mensagem.NomePais,
                DescricaoViaAnterior = mensagem.DescricaoViaAnterior,
                DescricaoViaAtual = mensagem.DescricaoViaAtual,
                CodigoUnidadeSeo = mensagem.CodigoUnidadeSeo,
                DescricaoGrauAcademico = mensagem.DescricaoGrauAcademico,
                DocumentoAcademico = mensagem.DocumentoAcademico
            };

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarTiposMensagemPorInstituicaoNivel(string tokenTipoDocumentoAcademico, long? seqTipoDocumentoSolicitado, long seqInstituicaoEnsino, long seqInstituicaoNivel, bool primeiraVia, string documentoAcademico)
        {
            var retorno = new List<SMCDatasourceItem>();

            var specInstituicaoNivelTipoMensagem = new InstituicaoNivelTipoMensagemFilterSpecification()
            {
                SeqInstituicaoEnsino = seqInstituicaoEnsino,
                SeqInstituicaoNivel = seqInstituicaoNivel
            };

            var dadosInstituicaoNivelTipoMensagem = InstituicaoNivelTipoMensagemDomainService.SearchBySpecification(specInstituicaoNivelTipoMensagem, x => x.TipoMensagem.TiposUso).ToList();

            var listaTokensTiposHistorico = new List<string>() { TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_FINAL, TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_PARCIAL, TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_2VIA };
            var tiposHistoricoEscolar = BuscarTiposHistoricoEscolarSelect();
            var tokenTipoHistorico = string.Empty;

            if (seqTipoDocumentoSolicitado.HasValue)
                tokenTipoHistorico = tiposHistoricoEscolar.FirstOrDefault(a => a.Seq == seqTipoDocumentoSolicitado.Value).DataAttributes.FirstOrDefault(b => b.Key == "token").Value;

            if (tokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL)
            {
                if (documentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL)
                {
                    var dados = dadosInstituicaoNivelTipoMensagem.Where(w => w.TipoMensagem.TiposUso.Any(a => a.TipoUsoMensagem == TipoUsoMensagem.Diploma)).ToList();
                    foreach (var item in dados)
                    {
                        if (item.TipoMensagem.PermiteCadastroManual && item.TipoMensagem.TiposUso != null)
                        {
                            retorno.Add(new SMCDatasourceItem() { Seq = item.TipoMensagem.Seq, Descricao = item.TipoMensagem.Descricao });
                        }
                    }
                }
                else if (primeiraVia && documentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR)
                {
                    var dados = dadosInstituicaoNivelTipoMensagem.Where(w => w.TipoMensagem.TiposUso.Any(a => a.TipoUsoMensagem == TipoUsoMensagem.HistoricoEscolar)).ToList();
                    foreach (var item in dados)
                    {
                        if (item.TipoMensagem.PermiteCadastroManual && item.TipoMensagem.TiposUso != null)
                        {
                            retorno.Add(new SMCDatasourceItem() { Seq = item.TipoMensagem.Seq, Descricao = item.TipoMensagem.Descricao });
                        }
                    }
                }
            }
            else if (listaTokensTiposHistorico.Contains(tokenTipoHistorico))
            {
                foreach (var item in dadosInstituicaoNivelTipoMensagem)
                {
                    if (item.TipoMensagem.PermiteCadastroManual && item.TipoMensagem.TiposUso != null && item.TipoMensagem.TiposUso.Any(a => a.TipoUsoMensagem == TipoUsoMensagem.HistoricoEscolar))
                    {
                        retorno.Add(new SMCDatasourceItem() { Seq = item.TipoMensagem.Seq, Descricao = item.TipoMensagem.Descricao });
                    }
                }
            }

            return retorno;
        }

        public void SalvarMensagem(SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemVO model)
        {
            var keySessionMensagemEmissao = string.Format(KEY_SESSION_MENSAGEM.KEY_SESSION_MENSAGEM_EMISSAO, model.SeqSolicitacaoServico);
            var listaMensagensSession = HttpContext.Current.Session[keySessionMensagemEmissao] as List<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO>;

            if (listaMensagensSession == null)
                listaMensagensSession = new List<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO>();

            var tipoMensagem = this.TipoMensagemDomainService.SearchByKey(new SMCSeqSpecification<TipoMensagem>(model.SeqTipoMensagem));

            if (model.Seq == 0)
            {
                var sequencialMensagem = listaMensagensSession.Any() ? listaMensagensSession.Max(a => a.Seq) + 1 : 1;

                var mensagem = new SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO()
                {
                    Seq = sequencialMensagem, //Sequencial auxiliar para edição/exclusão como os valores ainda não estão salvos em banco
                    SeqTipoMensagem = tipoMensagem.Seq,
                    DescricaoTipoMensagem = tipoMensagem.Descricao,
                    Descricao = model.Descricao,
                    DataInicioVigencia = model.DataInicioVigencia,
                    SeqSolicitacaoServico = model.SeqSolicitacaoServico,
                    ExisteDocumentoConclusao = model.ExisteDocumentoConclusao,
                    TokenTipoDocumentoAcademico = model.TokenTipoDocumentoAcademico,
                    SeqTipoDocumentoSolicitado = model.SeqTipoDocumentoSolicitado,
                    NumeroVia = model.NumeroVia,
                    SeqPessoaAtuacao = model.SeqPessoaAtuacao,
                    SeqInstituicaoEnsino = model.SeqInstituicaoEnsino,
                    SeqInstituicaoNivel = model.SeqInstituicaoNivel,
                    ReutilizarDados = model.ReutilizarDados,
                    NomePais = model.NomePais,
                    DescricaoViaAnterior = model.DescricaoViaAnterior,
                    DescricaoViaAtual = model.DescricaoViaAtual,
                    CodigoUnidadeSeo = model.CodigoUnidadeSeo,
                    DescricaoGrauAcademico = model.DescricaoGrauAcademico,
                    DocumentoAcademico = model.DocumentoAcademico
                };

                listaMensagensSession.Add(mensagem);
            }
            else
            {
                var mensagem = listaMensagensSession.FirstOrDefault(a => a.Seq == model.Seq);

                if (mensagem != null)
                {
                    mensagem.SeqTipoMensagem = tipoMensagem.Seq;
                    mensagem.DescricaoTipoMensagem = tipoMensagem.Descricao;
                    mensagem.Descricao = model.Descricao;
                }
            }

            HttpContext.Current.Session[keySessionMensagemEmissao] = listaMensagensSession;
        }

        public void ExcluirMensagem(long seq, long seqPessoaAtuacao, long seqSolicitacaoServico)
        {
            var keySessionMensagemEmissao = string.Format(KEY_SESSION_MENSAGEM.KEY_SESSION_MENSAGEM_EMISSAO, seqSolicitacaoServico);
            var listaMensagensSession = HttpContext.Current.Session[keySessionMensagemEmissao] as List<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO>;

            var mensagemRemover = listaMensagensSession.FirstOrDefault(a => a.Seq == seq);
            listaMensagensSession.Remove(mensagemRemover);

            HttpContext.Current.Session[keySessionMensagemEmissao] = listaMensagensSession;
        }

        public void SalvarAtendimentoEmissaoDocumentoConclusao(SolicitacaoAnaliseEmissaoDocumentoConclusaoVO model)
        {
            ValidarModeloAtendimentoEmissaoDocumentoConclusao(model);

            using (var transacao = SMCUnitOfWork.Begin())
            {
                var documentoConclusao = new DocumentoConclusao();

                // Recupera os dados do documento 
                var dadosEmissao = BuscarDadosSolicitacaoDocumentoConclusao(model.SeqSolicitacaoServicoAuxiliar);

                if (string.IsNullOrEmpty(dadosEmissao.DescricaoGrauAcademicoSelecionado))
                    dadosEmissao.DescricaoGrauAcademicoSelecionado = dadosEmissao.TiposGrauAcademico.Where(w => w.Seq == model.SeqGrauAcademicoSelecionado).FirstOrDefault().Descricao;

                if (!dadosEmissao.SeqGrauAcademicoSelecionado.HasValue ||
                    (dadosEmissao.SeqGrauAcademicoSelecionado.HasValue && dadosEmissao.SeqGrauAcademicoSelecionado == 0))
                {
                    dadosEmissao.SeqGrauAcademicoSelecionado = model.SeqGrauAcademicoSelecionado;
                }

                if (dadosEmissao.EmissaoPermitida)
                {
                    // Recupera o tipo de documento solicitado
                    var tipoDocumentoAcademico = TipoDocumentoAcademicoDomainService.SearchByKey(new SMCSeqSpecification<TipoDocumentoAcademico>(dadosEmissao.SeqTipoDocumentoAcademico));

                    if (tipoDocumentoAcademico.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL &&
                        dadosEmissao.NumeroVia == 1 && (model.DocumentacaoComprobatoria == null || !model.DocumentacaoComprobatoria.Any()))
                    {
                        throw new SolicitacaoDocumentoConclusaoDocumentacaoComprobatoriaObrigatoriaException();
                    }

                    // Recupera as formações específicas
                    var formacoesConcatenadas = new List<SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaVO>();
                    if (dadosEmissao.FormacoesEspecificasConcluidas != null)
                        formacoesConcatenadas.AddRange(dadosEmissao.FormacoesEspecificasConcluidas);

                    if (dadosEmissao.FormacoesEspecificasViasAnteriores != null)
                        formacoesConcatenadas.AddRange(dadosEmissao.FormacoesEspecificasViasAnteriores);

                    if (!dadosEmissao.ExisteDocumentoConclusao)
                    {
                        // Recupera os dados do aluno
                        var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosEmissao.SeqPessoaAtuacao);

                        // Recupera o seq instituicao nivel
                        var seqInstituicaoNivel = InstituicaoNivelDomainService.SearchProjectionByKey(new InstituicaoNivelFilterSpecification
                        {
                            SeqInstituicaoEnsino = dadosOrigem.SeqInstituicaoEnsino,
                            SeqNivelEnsino = dadosEmissao.SeqNivelEnsino,
                            //SeqCicloLetivo = dadosOrigem.SeqCicloLetivo
                        }, x => x.Seq);

                        // Recupera o seq dos tipos campus e unidade educacional
                        var seqsTiposEntidades = TipoEntidadeDomainService.SearchProjectionBySpecification(new TipoEntidadeFilterSpecification
                        {
                            Tokens = new List<string> { TOKEN_TIPO_ENTIDADE.UNIDADE_EDUCACIONAL, TOKEN_TIPO_ENTIDADE.CAMPUS }
                        }, x => x.Seq).ToArray();

                        // Recupera os ids da hierarquia por localidade
                        var hierarquia = HierarquiaEntidadeDomainService.BuscarEntidadesSuperioresSelect(new List<long> { dadosEmissao.SeqCursoOfertaLocalidade }, TipoVisao.VisaoLocalidades);
                        var seqsEntidades = hierarquia.Select(h => h.Seq).ToList();

                        // Recupera a primeira entidade dentre as retornadas na hierarquia que seja do token unidade educacional ou campus
                        var seqEntidadeProcesso = EntidadeDomainService.SearchProjectionByKey(new EntidadeFilterSpecification
                        {
                            Seqs = seqsEntidades.ToArray(),
                            SeqsTipoEntidade = seqsTiposEntidades
                        }, x => x.Seq);

                        documentoConclusao = new DocumentoConclusao
                        {
                            SeqAtuacaoAluno = dadosEmissao.SeqPessoaAtuacao,
                            LancamentoHistorico = false,
                            NumeroViaDocumento = dadosEmissao.NumeroVia.HasValue ? (short)dadosEmissao.NumeroVia : (short)0,
                            NumeroProcesso = $"{seqEntidadeProcesso}/{dadosEmissao.NumeroRA}/{DateTime.Today.Year}{DateTime.Today.Month}",
                            SeqPessoaDadosPessoais = dadosEmissao.SeqPessoaDadosPessoais,
                            SeqSolicitacaoServico = model.SeqSolicitacaoServicoAuxiliar,
                            SeqDocumentoAcademicoViaAnterior = dadosEmissao.SeqViaAnterior
                        };

                        //var listaTokensTiposHistorico = new List<string>() { TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_FINAL, TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_PARCIAL, TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_2VIA };
                        var tiposHistoricoEscolar = BuscarTiposHistoricoEscolarSelect();
                        var tokenTipoHistorico = string.Empty;

                        if (dadosEmissao.SeqTipoDocumentoSolicitado.HasValue)
                        {
                            tokenTipoHistorico = tiposHistoricoEscolar.FirstOrDefault(a => a.Seq == dadosEmissao.SeqTipoDocumentoSolicitado.Value).DataAttributes.FirstOrDefault(b => b.Key == "token").Value;
                        }

                        var dataColacao = formacoesConcatenadas.Min(a => a.DataColacaoGrau);
                        var dataConclusao = formacoesConcatenadas.Min(a => a.DataConclusao);

                        var dadosElementoHistorico = RetornarDadosElementoHistorico(dadosEmissao.CodigoAlunoMigracao.Value, dadosEmissao.NumeroVia, false, dadosEmissao.CodigoCursoOfertaLocalidade);

                        List<SolicitacaoAnaliseEmissaoDadosCriacaoDocumentoVO> listaDadosCriacaoDocumento = new List<SolicitacaoAnaliseEmissaoDadosCriacaoDocumentoVO>();

                        if (tipoDocumentoAcademico.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL)
                        {
                            if (dadosEmissao.NumeroVia > 1)
                            {
                                var classeSituacaoAtualViaAnterior = DocumentoConclusaoDomainService.SearchProjectionByKey(dadosEmissao.SeqViaAnterior.Value, x => x.SituacaoAtual.SituacaoDocumentoAcademico.ClasseSituacaoDocumento);

                                if (classeSituacaoAtualViaAnterior != ClasseSituacaoDocumento.Invalido)
                                    throw new SolicitacaoDocumentoConclusaoNovaViaException();
                            }

                            if (dataColacao.HasValue && dataConclusao.HasValue && dataColacao.Value.Date < dataConclusao.Value.Date)
                                throw new SolicitacaoDocumentoConclusaoDataColacaoMenorException();

                            if (dataColacao.HasValue && dataColacao.Value.Date < dadosEmissao.DataAdmissao.Date)
                                throw new SolicitacaoDocumentoConclusaoDataColacaoMenorException();

                            // Recupera os dados do tipo de documento x instituição nivel (tipo documento diploma digital)
                            var dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital = InstituicaoNivelTipoDocumentoAcademicoDomainService.BuscarConfiguracaoInstituicaoNivelTipoDocumentoAcademico(dadosOrigem.SeqInstituicaoEnsino, dadosEmissao.SeqTipoDocumentoAcademico, seqInstituicaoNivel);

                            if (dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital == null)
                                throw new SolicitacaoDocumentoConclusaoParametrizacaoTipoDocumentoNaoEncontradaException();

                            var specTipoDocumentoHistoricoFinal = new TipoDocumentoAcademicoFilterSpecification()
                            {
                                Token = TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_FINAL
                            };
                            var tipoDocumentoHistoricoFinal = TipoDocumentoAcademicoDomainService.SearchByKey(specTipoDocumentoHistoricoFinal);

                            if (tipoDocumentoHistoricoFinal == null)
                                throw new SolicitacaoDocumentoConclusaoTipoDocumentoNaoEncontradoException(TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_FINAL);

                            // Recupera os dados do tipo de documento x instituição nivel (tipo documento histórico escolar final)
                            var dadosInstituicaoNivelTipoDocumentoAcademicoHistoricoFinal = InstituicaoNivelTipoDocumentoAcademicoDomainService.BuscarConfiguracaoInstituicaoNivelTipoDocumentoAcademico(dadosOrigem.SeqInstituicaoEnsino, tipoDocumentoHistoricoFinal.Seq, seqInstituicaoNivel);

                            if (dadosInstituicaoNivelTipoDocumentoAcademicoHistoricoFinal == null)
                                throw new SolicitacaoDocumentoConclusaoParametrizacaoTipoDocumentoNaoEncontradaException();

                            if (dadosEmissao.TokenNivelEnsino == TOKEN_NIVEL_ENSINO.GRADUACAO)
                            {
                                if (tipoDocumentoAcademico.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL && !dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.SeqConfiguracaoGad.HasValue)
                                    throw new SolicitacaoDocumentoConclusaoConfiguracaoGADNaoEncontradaException();
                            }

                            if (dadosEmissao.DescricaoGrauAcademicoSelecionado != GRAU_ACADEMICO.BachareladoLicenciatura && dadosEmissao.NumeroVia == 1)
                            {
                                var dadosFormacao = formacoesConcatenadas.OrderBy(o => o.Data).FirstOrDefault();

                                var specCursoFormacaoEspecifica = new CursoFormacaoEspecificaFilterSpecification() { SeqCurso = dadosEmissao.SeqCurso, SeqFormacaoEspecifica = dadosFormacao.SeqFormacaoEspecifica };
                                var grauAcademicoFormacao = this.CursoFormacaoEspecificaDomainService.SearchProjectionByKey(specCursoFormacaoEspecifica, x => x.GrauAcademico);

                                if (grauAcademicoFormacao != null)
                                {
                                    var dadosRegistroDiploma = BuscarDadosRegistro(model.ReutilizarDados, dadosEmissao.SeqViaAnterior, dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.HabilitaRegistroDocumento, dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.SeqGrupoRegistro, listaDadosCriacaoDocumento);

                                    var dadoCriacaoDiploma = new SolicitacaoAnaliseEmissaoDadosCriacaoDocumentoVO()
                                    {
                                        EmissaoDiplomaHistorico = true,
                                        SeqTipoDocumentoAcademico = dadosEmissao.SeqTipoDocumentoAcademico,
                                        SeqGrupoRegistro = !dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.HabilitaRegistroDocumento ? (long?)null : dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.SeqGrupoRegistro,
                                        TipoRegistroDocumento = !dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.HabilitaRegistroDocumento ? (TipoRegistroDocumento?)null : TipoRegistroDocumento.Interno,
                                        SeqOrgaoRegistro = !dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.HabilitaRegistroDocumento ? (long?)null : dadosEmissao.SeqOrgaoRegistro,
                                        SeqConfiguracaoGad = dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.SeqConfiguracaoGad.Value,
                                        SeqConfiguracaoHistoricoGAD = dadosInstituicaoNivelTipoDocumentoAcademicoHistoricoFinal.SeqConfiguracaoGad.Value,
                                        SeqGrauAcademico = grauAcademicoFormacao.Seq,
                                        DescricaoGrauAcademico = grauAcademicoFormacao.Descricao,
                                        NumeroRegistro = dadosRegistroDiploma.NumeroRegistro,
                                        DataRegistro = dadosRegistroDiploma.DataRegistro,
                                        UsuarioRegistro = dadosRegistroDiploma.UsuarioRegistro,
                                        NumeroPublicacaoDOU = dadosRegistroDiploma.NumeroPublicacaoDOU,
                                        NumeroSecaoDOU = dadosRegistroDiploma.NumeroSecaoDOU,
                                        NumeroPaginaDOU = dadosRegistroDiploma.NumeroPaginaDOU,
                                        DataPublicacaoDOU = dadosRegistroDiploma.DataPublicacaoDOU,
                                        DataEnvioPublicacaoDOU = dadosRegistroDiploma.DataEnvioPublicacaoDOU
                                    };
                                    listaDadosCriacaoDocumento.Add(dadoCriacaoDiploma);

                                    var dadoCriacaoHistorico = new SolicitacaoAnaliseEmissaoDadosCriacaoDocumentoVO()
                                    {
                                        SeqTipoDocumentoAcademico = tipoDocumentoHistoricoFinal.Seq,
                                        SeqGrupoRegistro = !dadosInstituicaoNivelTipoDocumentoAcademicoHistoricoFinal.HabilitaRegistroDocumento ? (long?)null : dadosInstituicaoNivelTipoDocumentoAcademicoHistoricoFinal.SeqGrupoRegistro,
                                        TipoRegistroDocumento = !dadosInstituicaoNivelTipoDocumentoAcademicoHistoricoFinal.HabilitaRegistroDocumento ? (TipoRegistroDocumento?)null : TipoRegistroDocumento.Interno,
                                        SeqOrgaoRegistro = !dadosInstituicaoNivelTipoDocumentoAcademicoHistoricoFinal.HabilitaRegistroDocumento ? (long?)null : dadosEmissao.SeqOrgaoRegistro,
                                        NumeroRegistro = string.Format("TEMP{0}", grauAcademicoFormacao.Descricao)
                                    };
                                    listaDadosCriacaoDocumento.Add(dadoCriacaoHistorico);

                                    //Linkando no documento de conclusão do diploma qual é o numero de registro do histórico para 
                                    //depois atualizar o sequencial do documento criado no GAD
                                    var itemDadoCriacaoDiploma = listaDadosCriacaoDocumento.FirstOrDefault(a => a.NumeroRegistro == dadosRegistroDiploma.NumeroRegistro);
                                    itemDadoCriacaoDiploma.IdentificadorNumeroRegistroAtualizacaoHistorico = dadoCriacaoHistorico.NumeroRegistro;
                                }

                                DocumentoConclusaoDomainService.InvalidarDocumentoConclusaoHistorico(dadosEmissao.SeqPessoaAtuacao, dadosEmissao.DescricaoCursoDocumento, dadosEmissao.SeqCurso, dadosEmissao.SeqGrauAcademicoSelecionado, dadosEmissao.SeqTitulacao, model.Protocolo);
                            }
                            else if (dadosEmissao.DescricaoGrauAcademicoSelecionado != GRAU_ACADEMICO.BachareladoLicenciatura && dadosEmissao.NumeroVia > 1)
                            {
                                var dadosFormacao = formacoesConcatenadas.OrderBy(o => o.Data).FirstOrDefault();

                                var specCursoFormacaoEspecifica = new CursoFormacaoEspecificaFilterSpecification() { SeqCurso = dadosEmissao.SeqCurso, SeqFormacaoEspecifica = dadosFormacao.SeqFormacaoEspecifica };
                                var grauAcademicoFormacao = this.CursoFormacaoEspecificaDomainService.SearchProjectionByKey(specCursoFormacaoEspecifica, x => x.GrauAcademico);

                                if (grauAcademicoFormacao != null)
                                {
                                    var dadosRegistroDiploma = BuscarDadosRegistro(model.ReutilizarDados, dadosEmissao.SeqViaAnterior, dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.HabilitaRegistroDocumento, dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.SeqGrupoRegistro, listaDadosCriacaoDocumento);

                                    var dadoCriacaoDiploma = new SolicitacaoAnaliseEmissaoDadosCriacaoDocumentoVO()
                                    {
                                        EmissaoDiploma = true,
                                        SeqTipoDocumentoAcademico = dadosEmissao.SeqTipoDocumentoAcademico,
                                        SeqGrupoRegistro = !dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.HabilitaRegistroDocumento ? (long?)null : dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.SeqGrupoRegistro,
                                        TipoRegistroDocumento = !dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.HabilitaRegistroDocumento ? (TipoRegistroDocumento?)null : TipoRegistroDocumento.Interno,
                                        SeqOrgaoRegistro = !dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.HabilitaRegistroDocumento ? (long?)null : dadosEmissao.SeqOrgaoRegistro,
                                        SeqConfiguracaoGad = dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.SeqConfiguracaoGad.Value,
                                        SeqConfiguracaoHistoricoGAD = dadosInstituicaoNivelTipoDocumentoAcademicoHistoricoFinal.SeqConfiguracaoGad.Value,
                                        SeqGrauAcademico = grauAcademicoFormacao.Seq,
                                        DescricaoGrauAcademico = grauAcademicoFormacao.Descricao,
                                        NumeroRegistro = dadosRegistroDiploma.NumeroRegistro,
                                        DataRegistro = dadosRegistroDiploma.DataRegistro,
                                        UsuarioRegistro = dadosRegistroDiploma.UsuarioRegistro,
                                        NumeroPublicacaoDOU = dadosRegistroDiploma.NumeroPublicacaoDOU,
                                        NumeroSecaoDOU = dadosRegistroDiploma.NumeroSecaoDOU,
                                        NumeroPaginaDOU = dadosRegistroDiploma.NumeroPaginaDOU,
                                        DataPublicacaoDOU = dadosRegistroDiploma.DataPublicacaoDOU,
                                        DataEnvioPublicacaoDOU = dadosRegistroDiploma.DataEnvioPublicacaoDOU
                                    };
                                    listaDadosCriacaoDocumento.Add(dadoCriacaoDiploma);
                                }
                            }
                            else if (dadosEmissao.DescricaoGrauAcademicoSelecionado == GRAU_ACADEMICO.BachareladoLicenciatura && dadosEmissao.NumeroVia == 1)
                            {
                                //Para o grau Bacharelado/Licenciatura, será criado um documento para cada grau (Bacharelado e Licenciatura)
                                //Por isso, agora é iterado na lista para criação dos documentos

                                var descricoesGrauBachareladoLicenciatura = new List<string>()
                                {
                                    GRAU_ACADEMICO.Bacharelado,
                                    GRAU_ACADEMICO.Licenciatura
                                };

                                foreach (var descricaoGrau in descricoesGrauBachareladoLicenciatura)
                                {
                                    var dadosRegistroDiploma = BuscarDadosRegistro(model.ReutilizarDados, dadosEmissao.SeqViaAnterior, dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.HabilitaRegistroDocumento, dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.SeqGrupoRegistro, listaDadosCriacaoDocumento);

                                    var dadoCriacaoDiploma = new SolicitacaoAnaliseEmissaoDadosCriacaoDocumentoVO()
                                    {
                                        EmissaoDiplomaHistorico = true,
                                        SeqTipoDocumentoAcademico = dadosEmissao.SeqTipoDocumentoAcademico,
                                        SeqGrupoRegistro = !dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.HabilitaRegistroDocumento ? (long?)null : dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.SeqGrupoRegistro,
                                        TipoRegistroDocumento = !dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.HabilitaRegistroDocumento ? (TipoRegistroDocumento?)null : TipoRegistroDocumento.Interno,
                                        SeqOrgaoRegistro = !dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.HabilitaRegistroDocumento ? (long?)null : dadosEmissao.SeqOrgaoRegistro,
                                        SeqConfiguracaoGad = dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.SeqConfiguracaoGad.Value,
                                        SeqConfiguracaoHistoricoGAD = dadosInstituicaoNivelTipoDocumentoAcademicoHistoricoFinal.SeqConfiguracaoGad.Value,
                                        DescricaoGrauAcademico = descricaoGrau,
                                        NumeroRegistro = dadosRegistroDiploma.NumeroRegistro,
                                        DataRegistro = dadosRegistroDiploma.DataRegistro,
                                        UsuarioRegistro = dadosRegistroDiploma.UsuarioRegistro,
                                        NumeroPublicacaoDOU = dadosRegistroDiploma.NumeroPublicacaoDOU,
                                        NumeroSecaoDOU = dadosRegistroDiploma.NumeroSecaoDOU,
                                        NumeroPaginaDOU = dadosRegistroDiploma.NumeroPaginaDOU,
                                        DataPublicacaoDOU = dadosRegistroDiploma.DataPublicacaoDOU,
                                        DataEnvioPublicacaoDOU = dadosRegistroDiploma.DataEnvioPublicacaoDOU
                                    };

                                    var specGrauAcademico = new GrauAcademicoFilterSpecification() { Descricao = descricaoGrau };
                                    var grauAcademico = this.GrauAcademicoDomainService.SearchByKey(specGrauAcademico);
                                    dadoCriacaoDiploma.SeqGrauAcademico = grauAcademico.Seq;

                                    if (descricaoGrau == GRAU_ACADEMICO.Licenciatura)
                                        dadoCriacaoDiploma.DescricaoTitulacao = dadosEmissao.Sexo == Sexo.Masculino ? TITULACAO.Licenciado : TITULACAO.Licenciada;
                                    else if (descricaoGrau == GRAU_ACADEMICO.Bacharelado)
                                        dadoCriacaoDiploma.DescricaoTitulacao = TITULACAO.Bacharel;

                                    listaDadosCriacaoDocumento.Add(dadoCriacaoDiploma);

                                    var dadoCriacaoHistorico = new SolicitacaoAnaliseEmissaoDadosCriacaoDocumentoVO()
                                    {
                                        SeqTipoDocumentoAcademico = tipoDocumentoHistoricoFinal.Seq,
                                        SeqGrupoRegistro = !dadosInstituicaoNivelTipoDocumentoAcademicoHistoricoFinal.HabilitaRegistroDocumento ? (long?)null : dadosInstituicaoNivelTipoDocumentoAcademicoHistoricoFinal.SeqGrupoRegistro,
                                        TipoRegistroDocumento = !dadosInstituicaoNivelTipoDocumentoAcademicoHistoricoFinal.HabilitaRegistroDocumento ? (TipoRegistroDocumento?)null : TipoRegistroDocumento.Interno,
                                        SeqOrgaoRegistro = !dadosInstituicaoNivelTipoDocumentoAcademicoHistoricoFinal.HabilitaRegistroDocumento ? (long?)null : dadosEmissao.SeqOrgaoRegistro,
                                        NumeroRegistro = string.Format("TEMP{0}", descricaoGrau),
                                        SeqGrauAcademico = grauAcademico.Seq
                                    };

                                    if (descricaoGrau == GRAU_ACADEMICO.Licenciatura)
                                        dadoCriacaoHistorico.DescricaoTitulacao = dadosEmissao.Sexo == Sexo.Masculino ? TITULACAO.Licenciado : TITULACAO.Licenciada;
                                    else if (descricaoGrau == GRAU_ACADEMICO.Bacharelado)
                                        dadoCriacaoHistorico.DescricaoTitulacao = TITULACAO.Bacharel;

                                    listaDadosCriacaoDocumento.Add(dadoCriacaoHistorico);

                                    //Linkando no documento de conclusão do diploma qual é o numero de registro do histórico para 
                                    //depois atualizar o sequencial do documento criado no GAD
                                    var itemDadoCriacaoDiploma = listaDadosCriacaoDocumento.FirstOrDefault(a => a.NumeroRegistro == dadosRegistroDiploma.NumeroRegistro);
                                    itemDadoCriacaoDiploma.IdentificadorNumeroRegistroAtualizacaoHistorico = dadoCriacaoHistorico.NumeroRegistro;
                                }

                                DocumentoConclusaoDomainService.InvalidarDocumentoConclusaoHistorico(dadosEmissao.SeqPessoaAtuacao, dadosEmissao.DescricaoCursoDocumento, dadosEmissao.SeqCurso, dadosEmissao.SeqGrauAcademicoSelecionado, dadosEmissao.SeqTitulacao, model.Protocolo);
                            }
                            else if (dadosEmissao.DescricaoGrauAcademicoSelecionado == GRAU_ACADEMICO.BachareladoLicenciatura && dadosEmissao.NumeroVia > 1)
                            {
                                //Para o grau Bacharelado/Licenciatura, será criado um documento para cada grau (Bacharelado e Licenciatura)
                                //Por isso, agora é iterado na lista para criação dos documentos

                                var descricoesGrauBachareladoLicenciatura = new List<string>()
                                {
                                    GRAU_ACADEMICO.Bacharelado,
                                    GRAU_ACADEMICO.Licenciatura
                                };

                                foreach (var descricaoGrau in descricoesGrauBachareladoLicenciatura)
                                {
                                    var dadosRegistroDiploma = BuscarDadosRegistro(model.ReutilizarDados, dadosEmissao.SeqViaAnterior, dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.HabilitaRegistroDocumento, dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.SeqGrupoRegistro, listaDadosCriacaoDocumento);

                                    var dadoCriacaoDiploma = new SolicitacaoAnaliseEmissaoDadosCriacaoDocumentoVO()
                                    {
                                        EmissaoDiploma = true,
                                        SeqTipoDocumentoAcademico = dadosEmissao.SeqTipoDocumentoAcademico,
                                        SeqGrupoRegistro = !dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.HabilitaRegistroDocumento ? (long?)null : dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.SeqGrupoRegistro,
                                        TipoRegistroDocumento = !dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.HabilitaRegistroDocumento ? (TipoRegistroDocumento?)null : TipoRegistroDocumento.Interno,
                                        SeqOrgaoRegistro = !dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.HabilitaRegistroDocumento ? (long?)null : dadosEmissao.SeqOrgaoRegistro,
                                        SeqConfiguracaoGad = dadosInstituicaoNivelTipoDocumentoAcademicoDiplomaDigital.SeqConfiguracaoGad.Value,
                                        SeqConfiguracaoHistoricoGAD = dadosInstituicaoNivelTipoDocumentoAcademicoHistoricoFinal.SeqConfiguracaoGad.Value,
                                        DescricaoGrauAcademico = descricaoGrau,
                                        NumeroRegistro = dadosRegistroDiploma.NumeroRegistro,
                                        DataRegistro = dadosRegistroDiploma.DataRegistro,
                                        UsuarioRegistro = dadosRegistroDiploma.UsuarioRegistro,
                                        NumeroPublicacaoDOU = dadosRegistroDiploma.NumeroPublicacaoDOU,
                                        NumeroSecaoDOU = dadosRegistroDiploma.NumeroSecaoDOU,
                                        NumeroPaginaDOU = dadosRegistroDiploma.NumeroPaginaDOU,
                                        DataPublicacaoDOU = dadosRegistroDiploma.DataPublicacaoDOU,
                                        DataEnvioPublicacaoDOU = dadosRegistroDiploma.DataEnvioPublicacaoDOU
                                    };

                                    var specGrauAcademico = new GrauAcademicoFilterSpecification() { Descricao = descricaoGrau };
                                    var grauAcademico = this.GrauAcademicoDomainService.SearchByKey(specGrauAcademico);
                                    dadoCriacaoDiploma.SeqGrauAcademico = grauAcademico.Seq;

                                    if (descricaoGrau == GRAU_ACADEMICO.Licenciatura)
                                    {
                                        dadoCriacaoDiploma.DescricaoTitulacao = dadosEmissao.Sexo == Sexo.Masculino ? TITULACAO.Licenciado : TITULACAO.Licenciada;
                                    }
                                    else if (descricaoGrau == GRAU_ACADEMICO.Bacharelado)
                                    {
                                        dadoCriacaoDiploma.DescricaoTitulacao = TITULACAO.Bacharel;
                                    }

                                    listaDadosCriacaoDocumento.Add(dadoCriacaoDiploma);
                                }
                            }
                        }
                        else if (tipoDocumentoAcademico.GrupoDocumentoAcademico == GrupoDocumentoAcademico.HistoricoEscolar)
                        {
                            var tokenTipoDocumentoAcademicoDiploma = new List<string>() { TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL };

                            if (tokenTipoHistorico == TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_FINAL || tokenTipoHistorico == TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_2VIA)
                            {
                                /* Essa regra virou um Assert
                                if (dadosElementoHistorico.CargaHorariaCursoIntegralizada > 0 && dadosElementoHistorico.CargaHorariaCurso > 0 && dadosElementoHistorico.CargaHorariaCursoIntegralizada < dadosElementoHistorico.CargaHorariaCurso)
                                    throw new SolicitacaoDocumentoConclusaoCargaHorariaMenorException();
                                */

                                if (dataColacao.HasValue && dataConclusao.HasValue && dataColacao.Value.Date < dataConclusao.Value.Date)
                                    throw new SolicitacaoDocumentoConclusaoDataColacaoMenorException();

                                if (dataColacao.HasValue && dataColacao.Value.Date < dadosEmissao.DataAdmissao.Date)
                                    throw new SolicitacaoDocumentoConclusaoDataColacaoMenorException();

                                var dataExpedicaoDiploma = DocumentoConclusaoDomainService.BuscarDataExpedicaoDiploma(dadosEmissao.SeqPessoaAtuacao, dadosEmissao.DescricaoCursoDocumento, dadosEmissao.SeqGrauAcademicoSelecionado, dadosEmissao.SeqTitulacao);
                                if (dataExpedicaoDiploma.HasValue)
                                    dadosEmissao.DataExpedicaoDiploma = dataExpedicaoDiploma.Value;
                                else
                                    throw new SolicitacaoDocumentoConclusaoSemDataExpedicaoDiplomaException();

                                //if (DocumentoConclusaoDomainService.VerificarDocumentoConclusaoDadosPessoais(dadosEmissao.SeqPessoaAtuacao, dadosEmissao.DescricaoCursoDocumento, dadosEmissao.SeqGrauAcademicoSelecionado, dadosEmissao.SeqTitulacao, dadosEmissao.SeqPessoaDadosPessoais, tokenTipoDocumentoAcademicoDiploma))
                                //    throw new SolicitacaoDocumentoConclusaoDadosPessoaisDiferente2Exception();
                            }

                            if (dadosEmissao.NumeroVia > 1)
                            {
                                var tokensTipoDocumentoAcademico = new List<string>() { TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL, TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_FINAL, TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_2VIA };
                                if (DocumentoConclusaoDomainService.VerificarDocumentoConclusao(dadosEmissao.SeqPessoaAtuacao, dadosEmissao.DescricaoCursoDocumento, dadosEmissao.SeqGrauAcademicoSelecionado, dadosEmissao.SeqTitulacao, tokensTipoDocumentoAcademico))
                                    throw new SolicitacaoDocumentoConclusaoEmissaoHistoricoInvalidoException();

                                //if (DocumentoConclusaoDomainService.VerificarDocumentoConclusaoDadosPessoais(dadosEmissao.SeqPessoaAtuacao, dadosEmissao.DescricaoCursoDocumento, dadosEmissao.SeqGrauAcademicoSelecionado, dadosEmissao.SeqTitulacao, dadosEmissao.SeqPessoaDadosPessoais, tokenTipoDocumentoAcademicoDiploma))
                                //    throw new SolicitacaoDocumentoConclusaoDadosPessoaisDiferenteException();

                                DocumentoConclusaoDomainService.InvalidarDocumentoConclusaoHistorico(dadosEmissao.SeqPessoaAtuacao, dadosEmissao.DescricaoCursoDocumento, dadosEmissao.SeqCurso, dadosEmissao.SeqGrauAcademicoSelecionado, dadosEmissao.SeqTitulacao, model.Protocolo);
                            }

                            var specTipoDocumentoHistorico = new TipoDocumentoAcademicoFilterSpecification()
                            {
                                Token = tokenTipoHistorico
                            };
                            var tipoDocumentoHistorico = TipoDocumentoAcademicoDomainService.SearchByKey(specTipoDocumentoHistorico);

                            if (tipoDocumentoHistorico == null)
                                throw new SolicitacaoDocumentoConclusaoTipoDocumentoNaoEncontradoException(tokenTipoHistorico);

                            // Recupera os dados do tipo de documento x instituição nivel (tipo documento histórico escolar)
                            var dadosInstituicaoNivelTipoDocumentoAcademicoHistorico = InstituicaoNivelTipoDocumentoAcademicoDomainService.BuscarConfiguracaoInstituicaoNivelTipoDocumentoAcademico(dadosOrigem.SeqInstituicaoEnsino, tipoDocumentoHistorico.Seq, seqInstituicaoNivel);

                            if (dadosInstituicaoNivelTipoDocumentoAcademicoHistorico == null)
                                throw new SolicitacaoDocumentoConclusaoParametrizacaoTipoDocumentoNaoEncontradaException();

                            SolicitacaoAnaliseEmissaoDadosCriacaoDocumentoVO dadoCriacaoHistorico = new SolicitacaoAnaliseEmissaoDadosCriacaoDocumentoVO()
                            {
                                EmissaoHistorico = true,
                                SeqTipoDocumentoAcademico = tipoDocumentoHistorico.Seq,
                                SeqGrupoRegistro = !dadosInstituicaoNivelTipoDocumentoAcademicoHistorico.HabilitaRegistroDocumento ? (long?)null : dadosInstituicaoNivelTipoDocumentoAcademicoHistorico.SeqGrupoRegistro,
                                TipoRegistroDocumento = !dadosInstituicaoNivelTipoDocumentoAcademicoHistorico.HabilitaRegistroDocumento ? (TipoRegistroDocumento?)null : TipoRegistroDocumento.Interno,
                                SeqOrgaoRegistro = !dadosInstituicaoNivelTipoDocumentoAcademicoHistorico.HabilitaRegistroDocumento ? (long?)null : dadosEmissao.SeqOrgaoRegistro,
                                SeqConfiguracaoHistoricoGAD = dadosInstituicaoNivelTipoDocumentoAcademicoHistorico.SeqConfiguracaoGad.Value,
                            };

                            if (dadosInstituicaoNivelTipoDocumentoAcademicoHistorico.HabilitaRegistroDocumento)
                            {
                                var dadosRegistroHistorico = BuscarDadosRegistro(model.ReutilizarDados, dadosEmissao.SeqViaAnterior, dadosInstituicaoNivelTipoDocumentoAcademicoHistorico.HabilitaRegistroDocumento, dadosInstituicaoNivelTipoDocumentoAcademicoHistorico.SeqGrupoRegistro, listaDadosCriacaoDocumento);

                                dadoCriacaoHistorico.NumeroRegistro = dadosRegistroHistorico.NumeroRegistro;
                                dadoCriacaoHistorico.DataRegistro = dadosRegistroHistorico.DataRegistro;
                                dadoCriacaoHistorico.UsuarioRegistro = dadosRegistroHistorico.UsuarioRegistro;
                                dadoCriacaoHistorico.NumeroPublicacaoDOU = dadosRegistroHistorico.NumeroPublicacaoDOU;
                                dadoCriacaoHistorico.NumeroSecaoDOU = dadosRegistroHistorico.NumeroSecaoDOU;
                                dadoCriacaoHistorico.NumeroPaginaDOU = dadosRegistroHistorico.NumeroPaginaDOU;
                                dadoCriacaoHistorico.DataPublicacaoDOU = dadosRegistroHistorico.DataPublicacaoDOU;
                                dadoCriacaoHistorico.DataEnvioPublicacaoDOU = dadosRegistroHistorico.DataEnvioPublicacaoDOU;
                            }

                            listaDadosCriacaoDocumento.Add(dadoCriacaoHistorico);
                        }

                        //Dicionário auxiliar para atualização dos documentos de conclusão do tipo histórico escolar
                        //São criados junto com a emissão do diploma, então depois precisa atualizar no documento de conclusão
                        //qual é o sequencial do GAD
                        //Numero registro do histórico, sequencial do documento no GAD
                        Dictionary<string, long> atualizacaoHistoricos = new Dictionary<string, long>();

                        //Criando os documentos de conclusão e emitindo o diploma/histórico
                        foreach (var dadoCriacaoDocumento in listaDadosCriacaoDocumento)
                        {
                            documentoConclusao.Seq = 0;
                            documentoConclusao.SeqDocumentoGAD = null;

                            documentoConclusao.SeqTipoDocumentoAcademico = dadoCriacaoDocumento.SeqTipoDocumentoAcademico.GetValueOrDefault();

                            documentoConclusao.SeqGrupoRegistro = dadoCriacaoDocumento.SeqGrupoRegistro;
                            documentoConclusao.TipoRegistroDocumento = dadoCriacaoDocumento.TipoRegistroDocumento;
                            documentoConclusao.SeqOrgaoRegistro = dadoCriacaoDocumento.SeqOrgaoRegistro;

                            documentoConclusao.NumeroRegistro = dadoCriacaoDocumento.NumeroRegistro;
                            documentoConclusao.DataRegistro = dadoCriacaoDocumento.DataRegistro;
                            documentoConclusao.UsuarioRegistro = dadoCriacaoDocumento.UsuarioRegistro;

                            documentoConclusao.DataEnvioPublicacaoDOU = dadoCriacaoDocumento.DataEnvioPublicacaoDOU;
                            documentoConclusao.DataPublicacaoDOU = dadoCriacaoDocumento.DataPublicacaoDOU;
                            documentoConclusao.NumeroPublicacaoDOU = dadoCriacaoDocumento.NumeroPublicacaoDOU;
                            documentoConclusao.NumeroSecaoDOU = dadoCriacaoDocumento.NumeroSecaoDOU;
                            documentoConclusao.NumeroPaginaDOU = dadoCriacaoDocumento.NumeroPaginaDOU;

                            string numeroRegistroMensagemBachareladoLicenciatura = string.Empty;
                            DateTime? dataRegistroMensagemBachareladoLicenciatura = (DateTime?)null;

                            if (dadosEmissao.DescricaoGrauAcademicoSelecionado == GRAU_ACADEMICO.BachareladoLicenciatura)
                            {
                                //Se o grau for Bacharelado/Licenciatura, o diploma de bacharelado vai referenciar o número
                                //de registro do diploma de licenciatura, e vice-versa

                                if (dadoCriacaoDocumento.DescricaoGrauAcademico == GRAU_ACADEMICO.Bacharelado)
                                {
                                    dadosEmissao.DataEmissaoHistoricoEmissaoDiploma = DateTime.Now;

                                    var dadosGrauLicenciatura = listaDadosCriacaoDocumento.FirstOrDefault(a => a.DescricaoGrauAcademico == GRAU_ACADEMICO.Licenciatura);

                                    if (dadosGrauLicenciatura != null)
                                    {
                                        numeroRegistroMensagemBachareladoLicenciatura = dadosGrauLicenciatura.NumeroRegistro;
                                        dataRegistroMensagemBachareladoLicenciatura = dadosGrauLicenciatura.DataRegistro;
                                    }
                                }
                                else if (dadoCriacaoDocumento.DescricaoGrauAcademico == GRAU_ACADEMICO.Licenciatura)
                                {
                                    dadosEmissao.DataEmissaoHistoricoEmissaoDiploma = DateTime.Now.AddMinutes(1);

                                    var dadosGrauBacharelado = listaDadosCriacaoDocumento.FirstOrDefault(a => a.DescricaoGrauAcademico == GRAU_ACADEMICO.Bacharelado);

                                    if (dadosGrauBacharelado != null)
                                    {
                                        numeroRegistroMensagemBachareladoLicenciatura = dadosGrauBacharelado.NumeroRegistro;
                                        dataRegistroMensagemBachareladoLicenciatura = dadosGrauBacharelado.DataRegistro;
                                    }
                                }
                            }

                            DocumentoConclusaoDomainService.SaveEntity(documentoConclusao);

                            // Recupera o sequencial da situação do documento de conclusao
                            var situacaoAguardandoAssinaturas = SituacaoDocumentoAcademicoDomainService.SearchByKey(new SituacaoDocumentoAcademicoFilterSpecification { Token = TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.AGUARDANDO_ASSINATURAS });

                            if (situacaoAguardandoAssinaturas != null)
                            {
                                var seqSituacaoAguardandoAssinaturas = situacaoAguardandoAssinaturas.Seq;

                                var situacaoAtual = new DocumentoAcademicoHistoricoSituacao
                                {
                                    SeqSituacaoDocumentoAcademico = seqSituacaoAguardandoAssinaturas,
                                    SeqDocumentoAcademico = documentoConclusao.Seq
                                };

                                DocumentoAcademicoHistoricoSituacaoDomainService.SaveEntity(situacaoAtual);
                            }

                            foreach (var formacao in formacoesConcatenadas)
                            {
                                var documentoConclusaoFormacao = new DocumentoConclusaoFormacao
                                {
                                    SeqDocumentoConclusao = documentoConclusao.Seq,
                                    SeqAlunoFormacao = formacao.SeqAlunoFormacao.Value
                                };

                                if (dadoCriacaoDocumento.SeqGrauAcademico.HasValue && dadosEmissao.DescricaoGrauAcademicoSelecionado == GRAU_ACADEMICO.BachareladoLicenciatura)
                                {
                                    var descricaoGrauAcademico = string.Empty;
                                    var grauAcademico = GrauAcademicoDomainService.SearchByKey(new SMCSeqSpecification<GrauAcademico>(dadoCriacaoDocumento.SeqGrauAcademico.Value));

                                    if (grauAcademico != null)
                                    {
                                        if (!string.IsNullOrEmpty(grauAcademico.DescricaoXSD))
                                        {
                                            descricaoGrauAcademico = grauAcademico.DescricaoXSD;
                                        }
                                    }

                                    documentoConclusaoFormacao.ObservacaoFormacao = $"Grau acadêmico: {descricaoGrauAcademico} e Titulação: {dadoCriacaoDocumento.DescricaoTitulacao}";
                                }

                                DocumentoConclusaoFormacaoDomainService.SaveEntity(documentoConclusaoFormacao);
                            }

                            var tipoDocumentoCriacao = TipoDocumentoAcademicoDomainService.SearchByKey(new SMCSeqSpecification<TipoDocumentoAcademico>(dadoCriacaoDocumento.SeqTipoDocumentoAcademico.GetValueOrDefault()));

                            if (tipoDocumentoCriacao.Token != TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA)
                            {
                                var specTipoMensagemBachareladoLicenciatura = new TipoMensagemFilterSpecification() { Token = TOKEN_TIPO_MENSAGEM.DIPLOMA_BACHARELADO_LICENCIATURA };
                                var tipoMensagemBachareladoLicenciatura = this.TipoMensagemDomainService.SearchByKey(specTipoMensagemBachareladoLicenciatura);

                                var keySessionMensagemEmissao = string.Format(KEY_SESSION_MENSAGEM.KEY_SESSION_MENSAGEM_EMISSAO, dadosEmissao.SeqSolicitacaoServico);
                                var listaMensagensSession = HttpContext.Current.Session[keySessionMensagemEmissao] as List<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO>;

                                var mensagensFiltrada = new List<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO>();
                                if (tipoDocumentoCriacao.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL)
                                    mensagensFiltrada = listaMensagensSession.Where(w => w.DocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL).ToList();
                                else
                                    mensagensFiltrada = listaMensagensSession.Where(w => w.DocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR).ToList();

                                if (mensagensFiltrada != null && mensagensFiltrada.Any())
                                {
                                    foreach (var mensagemTela in mensagensFiltrada)
                                    {
                                        if (dadosEmissao.DescricaoGrauAcademicoSelecionado == GRAU_ACADEMICO.BachareladoLicenciatura && tipoMensagemBachareladoLicenciatura != null && tipoMensagemBachareladoLicenciatura.Seq == mensagemTela.SeqTipoMensagem)
                                        {
                                            var specInstituicaoNivelTipoMensagem = new InstituicaoNivelTipoMensagemFilterSpecification()
                                            {
                                                SeqInstituicaoEnsino = dadosEmissao.SeqInstituicaoEnsino,
                                                SeqTipoMensagem = tipoMensagemBachareladoLicenciatura.Seq,
                                                SeqInstituicaoNivel = dadosEmissao.SeqInstituicaoNivel
                                            };

                                            var dadosInstituicaoNivelTipoMensagem = InstituicaoNivelTipoMensagemDomainService.SearchByKey(specInstituicaoNivelTipoMensagem);

                                            if (dadosInstituicaoNivelTipoMensagem != null)
                                            {
                                                if (!dadosEmissao.SeqOrgaoRegistro.HasValue)
                                                    throw new SolicitacaoDocumentoConclusaoParametrizacaoOrgaoRegistroNaoEncontradaException();

                                                var orgaoRegistroDocumento = OrgaoRegistroDomainService.SearchByKey(new SMCSeqSpecification<OrgaoRegistro>(dadosEmissao.SeqOrgaoRegistro.Value));
                                                var descricaoVia = $"{dadosEmissao.NumeroVia}° Via, registrada na {orgaoRegistroDocumento.Sigla} sob o registro n° {numeroRegistroMensagemBachareladoLicenciatura}/{dataRegistroMensagemBachareladoLicenciatura?.Year}";

                                                Dictionary<string, string> dadosMerge = new Dictionary<string, string>
                                                {
                                                    { "{{DESCRICAO_VIA_RELACIONADA}}", descricaoVia }
                                                };

                                                string mensagemRetorno = dadosInstituicaoNivelTipoMensagem.MensagemPadrao;

                                                foreach (var key in dadosMerge.Keys)
                                                {
                                                    mensagemRetorno = mensagemRetorno.Replace(key, dadosMerge[key]);
                                                }

                                                mensagemTela.Descricao = mensagemRetorno;

                                                var htmlDescricao = HttpUtility.HtmlDecode(mensagemTela.Descricao);
                                                mensagemTela.DescricaoDecode = Regex.Replace(htmlDescricao, "<.*?>", string.Empty);
                                            }
                                        }

                                        Mensagem mensagem = new Mensagem()
                                        {
                                            SeqTipoMensagem = mensagemTela.SeqTipoMensagem,
                                            Descricao = HttpUtility.HtmlDecode(mensagemTela.Descricao),
                                            DataInicioVigencia = mensagemTela.DataInicioVigencia
                                        };

                                        this.MensagemDomainService.SaveEntity(mensagem);

                                        MensagemPessoaAtuacao mensagemPessoaAtuacao = new MensagemPessoaAtuacao()
                                        {
                                            SeqMensagem = mensagem.Seq,
                                            SeqPessoaAtuacao = dadosEmissao.SeqPessoaAtuacao
                                        };

                                        this.MensagemPessoaAtuacaoDomainService.SaveEntity(mensagemPessoaAtuacao);

                                        DocumentoConclusaoMensagem documentoConclusaoMensagem = new DocumentoConclusaoMensagem()
                                        {
                                            SeqDocumentoConclusao = documentoConclusao.Seq,
                                            SeqMensagem = mensagem.Seq
                                        };

                                        this.DocumentoConclusaoMensagemDomainService.SaveEntity(documentoConclusaoMensagem);
                                    }

                                    HttpContext.Current.Session[keySessionMensagemEmissao] = listaMensagensSession;
                                }
                            }

                            if (dadoCriacaoDocumento.EmissaoDiploma.GetValueOrDefault() && dadosEmissao.TokenNivelEnsino == TOKEN_NIVEL_ENSINO.GRADUACAO)
                            {
                                var documentoDiplomaGAD = GerarDiplomaGad(dadosEmissao, model, formacoesConcatenadas, documentoConclusao, dadoCriacaoDocumento.SeqConfiguracaoGad.GetValueOrDefault(), dadoCriacaoDocumento.SeqConfiguracaoHistoricoGAD.GetValueOrDefault(), dadoCriacaoDocumento.SeqGrauAcademico, dadoCriacaoDocumento.DescricaoTitulacao);

                                var paramDocumentoConclusao = new List<SMCFuncParameter>()
                                {
                                    new SMCFuncParameter("seqDocumentoGad", documentoDiplomaGAD.SeqDocumentoDiploma),
                                    new SMCFuncParameter("usuAlteracao", SMCContext.User.SMCGetSequencialUsuario()),
                                    new SMCFuncParameter("datAlteracao", DateTime.Now),
                                    new SMCFuncParameter("seqDocumentoAcademico", documentoConclusao.Seq)
                                };

                                ExecuteSqlCommand(_updateDocumentoConclusao, paramDocumentoConclusao);

                                //DEIXANDO DE UTILIZAR UpdateFields DO FRAMEWORK E FAZENDO NA MÃO PORQUE PERDE OS DADOS DO
                                //OBJETO DOCUMENTO CONCLUSÃO, E ESSES DADOS SÃO NECESSÁRIOS POIS SE UTILIZA DOS MESMOS DADOS
                                //PARA CRIAÇÃO DE CADA GRAU ACADÊMICO 
                                //SENÃO TERIA QUE CRIAR UM documentoConclusaoAuxiliar DEPOIS DA CRIAÇÃO DO documentoConclusao,
                                //E ASSOCIÁ-LO AO documentoConclusao AO ENTRAR NO FOREACH
                                //OU USAR DocumentoConclusaoDomainService.SaveEntity(documentoConclusao), BUSCAR O
                                //SeqDocumentoAcademicoHistoricoSituacaoAtual ANTES NA BASE, ASSOCIAR NO OBJETO E SALVAR
                                //documentoConclusao.SeqDocumentoDiplomaGAD = documentoDiplomaGAD.SeqDocumentoDiploma;
                                //DocumentoConclusaoDomainService.UpdateFields(documentoConclusao, x => x.SeqDocumentoDiplomaGAD);
                            }
                            if (dadoCriacaoDocumento.EmissaoDiplomaHistorico.GetValueOrDefault() && dadosEmissao.TokenNivelEnsino == TOKEN_NIVEL_ENSINO.GRADUACAO)
                            {
                                var documentoDiplomaGAD = GerarDiplomaGad(dadosEmissao, model, formacoesConcatenadas, documentoConclusao, dadoCriacaoDocumento.SeqConfiguracaoGad.GetValueOrDefault(), dadoCriacaoDocumento.SeqConfiguracaoHistoricoGAD.GetValueOrDefault(), dadoCriacaoDocumento.SeqGrauAcademico, dadoCriacaoDocumento.DescricaoTitulacao);

                                var paramDocumentoConclusao = new List<SMCFuncParameter>()
                                {
                                    new SMCFuncParameter("seqDocumentoGad", documentoDiplomaGAD.SeqDocumentoDiploma),
                                    new SMCFuncParameter("usuAlteracao", SMCContext.User.SMCGetSequencialUsuario()),
                                    new SMCFuncParameter("datAlteracao", DateTime.Now),
                                    new SMCFuncParameter("seqDocumentoAcademico", documentoConclusao.Seq)
                                };

                                ExecuteSqlCommand(_updateDocumentoConclusao, paramDocumentoConclusao);

                                atualizacaoHistoricos.Add(dadoCriacaoDocumento.IdentificadorNumeroRegistroAtualizacaoHistorico, documentoDiplomaGAD.SeqDocumentoHistorico.Value);
                            }
                            else if (dadoCriacaoDocumento.EmissaoHistorico.GetValueOrDefault() && dadosEmissao.TokenNivelEnsino == TOKEN_NIVEL_ENSINO.GRADUACAO)
                            {
                                var documentoHistoricoGAD = GerarHistoricoGad(dadosEmissao, model, formacoesConcatenadas, dadoCriacaoDocumento.SeqConfiguracaoHistoricoGAD.GetValueOrDefault(), tokenTipoHistorico);

                                var paramDocumentoConclusao = new List<SMCFuncParameter>()
                                {
                                    new SMCFuncParameter("seqDocumentoGad", documentoHistoricoGAD.SeqDocumentoHistorico),
                                    new SMCFuncParameter("usuAlteracao", SMCContext.User.SMCGetSequencialUsuario()),
                                    new SMCFuncParameter("datAlteracao", DateTime.Now),
                                    new SMCFuncParameter("seqDocumentoAcademico", documentoConclusao.Seq)
                                };

                                ExecuteSqlCommand(_updateDocumentoConclusao, paramDocumentoConclusao);
                            }
                        }

                        foreach (var itemHistorico in atualizacaoHistoricos)
                        {
                            //Numero de registro é unique na base, mas recupera o documento de conclusão que ainda não tem o sequencial
                            //de documento GAD preenchido só pra garantir, pode ser que volte a reutilizar o numero de registro 
                            //aí vai retornar mais de um documento de conclusao
                            var specDocumentoHistorico = new DocumentoConclusaoFilterSpecification() { NumeroRegistro = itemHistorico.Key };
                            var documentosConclusaoHistorico = this.DocumentoConclusaoDomainService.SearchBySpecification(specDocumentoHistorico).ToList();
                            var documentoConclusaoHistorico = documentosConclusaoHistorico.FirstOrDefault(a => !a.SeqDocumentoGAD.HasValue);

                            if (documentoConclusaoHistorico != null)
                            {
                                var paramDocumentoAcademico = new List<SMCFuncParameter>()
                                {
                                    new SMCFuncParameter("seqDocumentoGad", itemHistorico.Value),
                                    new SMCFuncParameter("usuAlteracao", SMCContext.User.SMCGetSequencialUsuario()),
                                    new SMCFuncParameter("datAlteracao", DateTime.Now),
                                    new SMCFuncParameter("seqDocumentoAcademico", documentoConclusaoHistorico.Seq)
                                };

                                ExecuteSqlCommand(_updateDocumentoConclusaoHistorico, paramDocumentoAcademico);

                                var paramDocumentoConclusao = new List<SMCFuncParameter>()
                                {
                                    new SMCFuncParameter("numRegistro", null),
                                    new SMCFuncParameter("seqDocumentoAcademico", documentoConclusaoHistorico.Seq)
                                };

                                ExecuteSqlCommand(_updateDocumentoConclusaoHistoricoNumRegistro, paramDocumentoConclusao);
                            }
                        }

                        //Atualizando a solicitação de documento conclusão
                        var specSolicitacaoDocumentoConclusao = new SMCSeqSpecification<SolicitacaoDocumentoConclusao>(model.SeqSolicitacaoServicoAuxiliar);
                        var solicitacaoDocumentoConclusao = this.SearchByKey(specSolicitacaoDocumentoConclusao);
                        solicitacaoDocumentoConclusao.TipoIdentidade = model.TipoIdentidade;

                        var listaFiliacao = dadosEmissao.Filiacao?.Select(f => $"{SMCEnumHelper.GetDescription(f.TipoParentesco)} - {f.Nome}").ToList() ?? new List<string>();
                        solicitacaoDocumentoConclusao.DescricaoFiliacao = string.Join(", ", listaFiliacao);

                        solicitacaoDocumentoConclusao.FormatoHistoricoEscolar = FormatoHistoricoEscolar.InformarComMatrizCurricular;
                        solicitacaoDocumentoConclusao.DataEnade = null;
                        solicitacaoDocumentoConclusao.ReutilizarDadosRegistro = false;
                        solicitacaoDocumentoConclusao.SeqTipoDocumentoAcademico = dadosEmissao.SeqTipoDocumentoAcademico;

                        this.SaveEntity(solicitacaoDocumentoConclusao);

                        //Atualizando os documentos requeridos da solicitação
                        if (model.DocumentacaoComprobatoria != null && model.DocumentacaoComprobatoria.Any())
                        {
                            var specSolicitacaoDocumentoRequerido = new SolicitacaoDocumentoRequeridoFilterSpecification() { SeqSolicitacaoServico = model.SeqSolicitacaoServicoAuxiliar };
                            var solicitacaoDocumentosRequeridos = this.SolicitacaoDocumentoRequeridoDomainService.SearchBySpecification(specSolicitacaoDocumentoRequerido).ToList();

                            //Validando quais documentos foram excluídos para alterar sua situação
                            var sequenciaisDocumentosTela = model.DocumentacaoComprobatoria.Select(a => a.Seq).Where(a => a != 0).ToList();
                            var documentosExcluidos = solicitacaoDocumentosRequeridos.Where(a => !sequenciaisDocumentosTela.Contains(a.Seq)).ToList();

                            foreach (var documentoExcluido in documentosExcluidos)
                            {
                                documentoExcluido.SituacaoEntregaDocumento = SituacaoEntregaDocumento.Indeferido;

                                this.SolicitacaoDocumentoRequeridoDomainService.SaveEntity(documentoExcluido);
                            }

                            solicitacaoDocumentoConclusao = this.SearchByKey(new SMCSeqSpecification<SolicitacaoDocumentoConclusao>(model.SeqSolicitacaoServicoAuxiliar), x => x.SituacaoAtual.SolicitacaoServicoEtapa);
                            var seqConfiguracaoEtapa = solicitacaoDocumentoConclusao.SituacaoAtual.SolicitacaoServicoEtapa.SeqConfiguracaoEtapa;

                            foreach (var documento in model.DocumentacaoComprobatoria)
                            {
                                var specDocumentoRequerido = new DocumentoRequeridoFilterSpecification()
                                {
                                    SeqConfiguracaoEtapa = seqConfiguracaoEtapa,
                                    SeqTipoDocumento = documento.SeqTipoDocumento
                                };
                                var documentoRequerido = this.DocumentoRequeridoDomainService.SearchByKey(specDocumentoRequerido);

                                //if (documentoRequerido == null)
                                //    throw new DocumentoConclusaoEntregaInformacaoNaoEncontradaException("documento requerido");

                                if (documento.Seq == 0)
                                {
                                    var dominio = documento.Transform<SolicitacaoDocumentoRequerido>();
                                    dominio.SeqSolicitacaoServico = model.SeqSolicitacaoServicoAuxiliar;
                                    dominio.SeqDocumentoRequerido = documentoRequerido.Seq;
                                    dominio.DataEntrega = DateTime.Now;
                                    dominio.FormaEntregaDocumento = FormaEntregaDocumento.Upload;
                                    dominio.VersaoDocumento = VersaoDocumento.CopiaSimples;
                                    dominio.SituacaoEntregaDocumento = SituacaoEntregaDocumento.Deferido;

                                    this.SolicitacaoDocumentoRequeridoDomainService.SaveEntity(dominio);
                                }
                                else
                                {
                                    var dominio = documento.Transform<SolicitacaoDocumentoRequerido>();

                                    if (documentoRequerido != null)
                                    {
                                        //Se não achou registro de documento requerido para um documento que está sendo alterado então mantém o sequencial que estava, senão 
                                        //associa o documento selecionado
                                        dominio.SeqDocumentoRequerido = documentoRequerido.Seq;
                                    }

                                    dominio.SituacaoEntregaDocumento = SituacaoEntregaDocumento.Deferido;

                                    if (dominio.ArquivoAnexado.Conteudo == null)
                                    {
                                        //Quando só carrega e não altera o arquivo nao vem o Conteudo preenchido
                                        //O conteúdo é preenchido se for um novo registro ou se alterou o arquivo de um registro existente
                                        this.EnsureFileIntegrity(dominio, x => x.SeqArquivoAnexado, x => x.ArquivoAnexado);
                                    }

                                    this.SolicitacaoDocumentoRequeridoDomainService.SaveEntity(dominio);
                                }
                            }
                        }

                        //Atualizando a solicitação de serviço
                        var specSolicitacaoServico = new SMCSeqSpecification<SolicitacaoServico>(model.SeqSolicitacaoServicoAuxiliar);
                        var solicitacaoServico = this.SolicitacaoServicoDomainService.SearchByKey(specSolicitacaoServico);
                        solicitacaoServico.SituacaoDocumentacao = SituacaoDocumentacao.Entregue;
                        this.SolicitacaoServicoDomainService.SaveEntity(solicitacaoServico);

                        // Procedimentos de finalização da etapa...
                        SolicitacaoServicoDomainService.ProcedimentosFinalizarEtapa(model.SeqSolicitacaoServicoAuxiliar, model.SeqConfiguracaoEtapaAuxiliar, ClassificacaoSituacaoFinal.FinalizadoComSucesso, null);
                    }
                    else
                    {
                        /*  
                            Senão, exibir a seguinte mensagem de confirmação: 
                            "Confirma a atualização do documento de conclusão? Ao confirmar o cadastro do documento será atualizado.".
                            · Ao confirmar executar os seguintes passos, conforme RN_CNC_041 - Documento Conclusão - Atualização
                            · Senão, retornar para a página de Emissão.
                        */
                        // FIX: CAROL - Não existe mais tramite de assinatura manual, código foi comentado.
                        /*
                        if (tipoDocumentoAcademico.TramiteAssinaturas == TramiteAssinaturas.AssinaturaManual)
                        {
                            var specDocumentoConclusaoExistentes = new DocumentoConclusaoFilterSpecification() { SeqSolicitacaoServico = model.SeqSolicitacaoServicoAuxiliar };
                            var documentosConclusaoExistentes = this.DocumentoConclusaoDomainService.SearchBySpecification(specDocumentoConclusaoExistentes, x => x.FormacoesEspecificas[0].AlunoFormacao.FormacaoEspecifica).ToList();


                            // Dados Pessoais = atualizar a identificação dos dados pessoais, se os dados pessoais persistidos
                            // no documento-conclusão for diferente dos dados-pessoais identificado durante a analise para a emissão

                            var documentosConclusaoDadosPessoaisDiferente = documentosConclusaoExistentes.Where(a => a.SeqPessoaDadosPessoais != dadosEmissao.SeqPessoaDadosPessoais).ToList();
                            if (documentosConclusaoDadosPessoaisDiferente.Any())
                            {
                                foreach (var documentoConclusaoBanco in documentosConclusaoDadosPessoaisDiferente)
                                {
                                    documentoConclusaoBanco.SeqPessoaDadosPessoais = dadosEmissao.SeqPessoaDadosPessoais;
                                    DocumentoConclusaoDomainService.UpdateFields(documentoConclusaoBanco, x => x.SeqPessoaDadosPessoais);
                                }
                            }

                            // Formações do Documento = associar apenas as formações que ainda não estão associadas ao documento e 
                            // foram identificadas durante a analise para emissão. Se houver formações no documento que não consta 
                            // na analise para emissão, a mesma deverá ser desassociada do documento-conclusão.

                            var listaFormacoesIncluir = new List<DocumentoConclusaoFormacao>();
                            var listaFormacoesRemover = new List<DocumentoConclusaoFormacao>();

                            foreach (var documentoConclusaoBanco in documentosConclusaoExistentes)
                            {
                                foreach (var formacaoTela in formacoesConcatenadas)
                                {
                                    if (!documentoConclusaoBanco.FormacoesEspecificas.Any(a => a.SeqAlunoFormacao == formacaoTela.SeqAlunoFormacao))
                                    {
                                        listaFormacoesIncluir.Add(new DocumentoConclusaoFormacao
                                        {
                                            SeqDocumentoConclusao = documentoConclusaoBanco.Seq,
                                            SeqAlunoFormacao = formacaoTela.SeqAlunoFormacao.Value
                                        });
                                    }
                                }

                                foreach (var formacaoBanco in documentoConclusaoBanco.FormacoesEspecificas)
                                {
                                    if (!formacoesConcatenadas.Any(a => a.SeqAlunoFormacao == formacaoBanco.SeqAlunoFormacao))
                                    {
                                        listaFormacoesRemover.Add(formacaoBanco);
                                    }
                                }
                            }

                            if (listaFormacoesIncluir.Any())
                            {
                                foreach (var formacaoIncluir in listaFormacoesIncluir)
                                {
                                    DocumentoConclusaoFormacaoDomainService.SaveEntity(formacaoIncluir);
                                }
                            }

                            if (listaFormacoesRemover.Any())
                            {
                                foreach (var formacaoRemover in listaFormacoesRemover)
                                {
                                    DocumentoConclusaoFormacaoDomainService.DeleteEntity(formacaoRemover);
                                }
                            }
                        }
                        */

                        // Procedimentos de finalização da etapa...
                        SolicitacaoServicoDomainService.ProcedimentosFinalizarEtapa(model.SeqSolicitacaoServicoAuxiliar, model.SeqConfiguracaoEtapaAuxiliar, ClassificacaoSituacaoFinal.FinalizadoComSucesso, null);
                    }
                }
                else
                {
                    // Cancela a solicitação
                    SolicitacaoServicoDomainService.SalvarCancelamentoSolicitacao(new SRC.ValueObjects.CancelamentoSolicitacaoVO
                    {
                        SeqSolicitacaoServico = model.SeqSolicitacaoServicoAuxiliar,
                        Observacao = model.ObservacoesEmissaoNaoPermitida,
                        TokenMotivoCancelamento = dadosEmissao.TokenMotivoEmissaoNaoPermitida,
                        SeqMotivoCancelamento = dadosEmissao.SeqMotivoEmissaoNaoPermitida
                    });
                }

                transacao.Commit();
            }
        }

        public SolicitacaoAnaliseEmissaoDadosRegistroVO BuscarDadosRegistro(bool? reutilizarDados, long? seqViaAnterior, bool habilitaRegistroDocumento, long? seqGrupoRegistro, List<SolicitacaoAnaliseEmissaoDadosCriacaoDocumentoVO> listaDadosCriacaoDocumento)
        {
            SolicitacaoAnaliseEmissaoDadosRegistroVO retorno = new SolicitacaoAnaliseEmissaoDadosRegistroVO();

            //Populando os dados de registro do documento de conclusão 
            //Para assinatura digital, não vai mais poder reutilizar dados, então não tem risco de quando for 
            //bacharelado/licenciatura não calcular o número de registro e reutilizar o mesmo (é unique na base)
            if (reutilizarDados.GetValueOrDefault())
            {
                var viaAnterior = DocumentoConclusaoDomainService.SearchProjectionByKey(seqViaAnterior.Value, x => new
                {
                    x.NumeroRegistro,
                    x.DataRegistro,
                    x.UsuarioRegistro,
                    x.DataEnvioPublicacaoDOU,
                    x.DataPublicacaoDOU,
                    x.NumeroPublicacaoDOU,
                    x.NumeroSecaoDOU,
                    x.NumeroPaginaDOU
                });

                retorno.NumeroRegistro = !habilitaRegistroDocumento ? null : viaAnterior.NumeroRegistro;
                retorno.DataRegistro = !habilitaRegistroDocumento ? (DateTime?)null : viaAnterior.DataRegistro;
                retorno.UsuarioRegistro = !habilitaRegistroDocumento ? null : viaAnterior.UsuarioRegistro;

                retorno.DataEnvioPublicacaoDOU = viaAnterior.DataEnvioPublicacaoDOU;
                retorno.DataPublicacaoDOU = viaAnterior.DataPublicacaoDOU;
                retorno.NumeroPublicacaoDOU = viaAnterior.NumeroPublicacaoDOU;
                retorno.NumeroSecaoDOU = viaAnterior.NumeroSecaoDOU;
                retorno.NumeroPaginaDOU = viaAnterior.NumeroPaginaDOU;
            }
            else
            {
                if (seqGrupoRegistro.HasValue)
                {
                    //Busca um número de registro que não exista, para isso adiciona +1 até encontrar o último número registro
                    //cadastrado para o documento de conclusão para tal grupo de registro. 

                    var grupoRegistro = GrupoRegistroDomainService.SearchByKey(new SMCSeqSpecification<GrupoRegistro>(seqGrupoRegistro.Value));
                    var numeroSequencial = grupoRegistro.NumeroUltimoRegistro;
                    var numeroRegistro = string.Empty;

                    if (!string.IsNullOrEmpty(grupoRegistro.Prefixo))
                    {
                        var achouNumeroRegistroNaoExiste = false;

                        do
                        {
                            numeroSequencial += 1;
                            numeroRegistro = $"{grupoRegistro.Prefixo}{numeroSequencial}";

                            var specDocumentoNumeroRegistro = new DocumentoConclusaoFilterSpecification() { SeqGrupoRegistro = grupoRegistro.Seq, NumeroRegistro = numeroRegistro };
                            var documentoConclusaoNumeroRegistro = this.DocumentoConclusaoDomainService.SearchBySpecification(specDocumentoNumeroRegistro).ToList();

                            if (!documentoConclusaoNumeroRegistro.Any() && !listaDadosCriacaoDocumento.Any(a => a.NumeroRegistro == numeroRegistro))
                            {
                                achouNumeroRegistroNaoExiste = true;
                            }

                        } while (!achouNumeroRegistroNaoExiste);
                    }
                    else
                    {
                        var achouNumeroRegistroNaoExiste = false;

                        do
                        {
                            numeroSequencial += 1;
                            numeroRegistro = numeroSequencial.ToString();

                            var specDocumentoNumeroRegistro = new DocumentoConclusaoFilterSpecification() { SeqGrupoRegistro = grupoRegistro.Seq, NumeroRegistro = numeroRegistro };
                            var documentoConclusaoNumeroRegistro = this.DocumentoConclusaoDomainService.SearchBySpecification(specDocumentoNumeroRegistro).ToList();

                            if (!documentoConclusaoNumeroRegistro.Any() && !listaDadosCriacaoDocumento.Any(a => a.NumeroRegistro == numeroRegistro))
                            {
                                achouNumeroRegistroNaoExiste = true;
                            }

                        } while (!achouNumeroRegistroNaoExiste);
                    }

                    retorno.NumeroRegistro = !habilitaRegistroDocumento ? null : numeroRegistro;
                }
                else
                {
                    throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("grupo de registro");
                }

                retorno.DataRegistro = !habilitaRegistroDocumento ? (DateTime?)null : DateTime.Now;

                var sequencialUsuario = SMCContext.User?.SMCGetSequencialUsuario();
                var nomeReduzido = SMCContext.User?.SMCGetNomeReduzido();

                if (sequencialUsuario != null && nomeReduzido != null)
                {
                    retorno.UsuarioRegistro = !habilitaRegistroDocumento ? null : $"{sequencialUsuario}/{nomeReduzido.ToUpper()}";
                }
                else
                {
                    retorno.UsuarioRegistro = !habilitaRegistroDocumento ? null : SMCContext.User?.Identity?.Name;
                }
            }

            return retorno;
        }

        public void ValidarModeloAtendimentoEmissaoDocumentoConclusao(SolicitacaoAnaliseEmissaoDocumentoConclusaoVO modelo)
        {
            if (modelo.DocumentacaoComprobatoria != null && modelo.DocumentacaoComprobatoria.Count() > 0)
            {
                foreach (var documento in modelo.DocumentacaoComprobatoria)
                {
                    string nomeArquivo = documento.ArquivoAnexado.Name;
                    string extensao = Path.GetExtension(documento.ArquivoAnexado.Name);

                    //Não deverá ser permitido carregar arquivos maiores que 25MB.
                    if (documento.ArquivoAnexado != null && documento.ArquivoAnexado.Size > VALIDACAO_ARQUIVO_ANEXADO.TAMANHO_MAXIMO_ARQUIVO_ANEXADO)
                    {
                        throw new TamanhoArquivoExcedidoException();
                    }

                    //Permitir carregar somente arquivos com extensão: pdf/a
                    if (!string.IsNullOrEmpty(extensao) && extensao == ".pdf")
                    {
                        bool isPDFA = false;

                        var dominio = documento.Transform<SolicitacaoDocumentoRequerido>();

                        if (dominio.ArquivoAnexado.Conteudo == null)
                        {
                            //Quando só carrega e não altera o arquivo nao vem o Conteudo preenchido
                            //O conteúdo é preenchido se for um novo registro ou se alterou o arquivo de um registro existente
                            this.EnsureFileIntegrity(dominio, x => x.SeqArquivoAnexado, x => x.ArquivoAnexado);
                        }

                        isPDFA = SMCPDFHelper.ValidatorPDFA(dominio.ArquivoAnexado.Conteudo);

                        if (!isPDFA)
                            throw new SolicitacaoDocumentoConclusaoExtensaoNaoPermitidaException(nomeArquivo);
                    }
                    else
                    {
                        throw new SolicitacaoDocumentoConclusaoExtensaoNaoPermitidaException(nomeArquivo);
                    }
                }
            }

            //Usando o SeqPessoaAtuacaoAuxiliar para pegar o valor que está na classe SolicitacaoAnaliseEmissaoDocumentoConclusaoViewModel, e não na classe base SolicitacaoServicoPaginaViewModelBase
            var keySessionMensagemEmissao = string.Format(KEY_SESSION_MENSAGEM.KEY_SESSION_MENSAGEM_EMISSAO, modelo.SeqSolicitacaoServico);
            var listaMensagensSession = HttpContext.Current.Session[keySessionMensagemEmissao] as List<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO>;

            if (!modelo.ExisteDocumentoConclusao)
            {
                /* Se for a criação do documento, então as mensagens ainda não estão salvas no banco, e para isso foi 
                inserido um sequencial auxiliar para a edição/exclusão das mensagens, que tem que ser limpo para 
                inserir os registros no banco */

                if (listaMensagensSession != null && listaMensagensSession.Any())
                {
                    foreach (var mensagem in listaMensagensSession)
                    {
                        mensagem.Seq = 0;
                    }
                }
            }
        }

        #region Diploma Gad

        public RetornoCriarDiplomaVO GerarDiplomaGad(SolicitacaoAnaliseEmissaoDocumentoConclusaoVO dadosEmissao, SolicitacaoAnaliseEmissaoDocumentoConclusaoVO modeloTela, List<SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaVO> formacoes, DocumentoConclusao documentoConclusao, long seqConfiguracaoGad, long seqConfiguracaoHistoricoGAD, long? seqGrauAcademicoConferido = null, string tituloConferido = null)
        {
            var usuarioInclusao = UsuarioLogado.RetornaUsuarioLogado();
            var modeloDiploma = new CriarDiplomaVO()
            {
                SeqConfiguracaoDiploma = seqConfiguracaoGad,
                SeqConfiguracaoHistorico = seqConfiguracaoHistoricoGAD,
                UsuarioInclusao = usuarioInclusao
            };

            modeloDiploma.Degree.DegreeType = TIPO_DEGREE_LACUNA.DEFAULT;
            modeloDiploma.Degree.Diploma.DadosDiploma.DeclaracoesAcercaProcesso = null;
            modeloDiploma.Degree.Diploma.DadosDiploma.DadosIesOriginalCursoPTA = null;

            var dadosFormacao = formacoes.OrderBy(o => o.Data).FirstOrDefault();
            var dataConclusao = formacoes.Min(a => a.DataConclusao);
            var dataColacao = formacoes.Min(a => a.DataColacaoGrau);

            modeloDiploma.Degree.Diploma.DadosDiploma.Diplomado = PreencherDadosAlunoDiploma(dadosEmissao, modeloTela);

            #region Dados da Formação do Aluno e do Curso

            if (dataConclusao.HasValue)
                modeloDiploma.Degree.Diploma.DadosDiploma.DataConclusao = dataConclusao.Value.Date;

            var cursoOfertaLocalidade = CursoOfertaLocalidadeDomainService.BuscarIdentificacaoEmecComEnderecoParaDocumentoAcademico(dadosEmissao.SeqCursoOfertaLocalidade);
            var modalidade = ModalidadeDomainService.SearchByKey(new SMCSeqSpecification<Modalidade>(cursoOfertaLocalidade.SeqModalidade));

            modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.NomeCurso = FormatarString.Truncate(dadosFormacao.DescricaoDocumentoConclusao, 255);

            if (modalidade != null)
            {
                if (!string.IsNullOrEmpty(modalidade.DescricaoXSD))
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Modalidade = FormatarString.Truncate(modalidade.DescricaoXSD, 255);
                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.ModalidadeNSF = FormatarString.Truncate(modalidade.DescricaoXSD, 255);
                }
                else
                {
                    throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("descrição XSD da modalidade do curso oferta localidade");
                }
            }
            else
            {
                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("modalidade do curso oferta localidade");
            }

            modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Habilitacoes = new List<HabilitacaoVO>();
            foreach (var formacao in formacoes)
            {
                var habilitacao = new HabilitacaoVO() { DataHabilitacao = formacao.DataConclusao };

                var formacaoEspecifica = FormacaoEspecificaDomainService.SearchByKey(new SMCSeqSpecification<FormacaoEspecifica>(formacao.SeqFormacaoEspecifica));
                var nomeHabilitacao = string.Empty;

                if (formacao.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.APROFUNDAMENTO ||
                    formacao.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.ENFASE ||
                    formacao.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.HABILITACAO ||
                    formacao.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.LINHA_FORMACAO)
                {
                    var auxNomeHabilitacao = $"{formacao.DescricaoTipoFormacaoEspecifica.Trim()} em {formacaoEspecifica.Descricao.Trim()}";
                    auxNomeHabilitacao = auxNomeHabilitacao.Trim();
                    nomeHabilitacao = FormatarString.Truncate(auxNomeHabilitacao, 255);
                }
                else if (formacao.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.GRAU)
                {
                    var auxNomeHabilitacao = $"{formacaoEspecifica.Descricao.Trim()} em {formacao.DescricaoDocumentoConclusao.Trim()}";
                    auxNomeHabilitacao = auxNomeHabilitacao.Trim();
                    nomeHabilitacao = FormatarString.Truncate(auxNomeHabilitacao, 255);
                }
                else
                {
                    var auxNomeHabilitacao = formacaoEspecifica.Descricao.Trim();
                    nomeHabilitacao = FormatarString.Truncate(auxNomeHabilitacao, 255);
                }

                habilitacao.NomeHabilitacao = nomeHabilitacao;

                modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Habilitacoes.Add(habilitacao);
            }

            modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Enfases = null;

            List<string> listaTokensTituloConferidoMasculino = new List<string>()
            {
                TOKEN_TITULO_CONFERIDO.LICENCIADO,
                TOKEN_TITULO_CONFERIDO.TECNOLOGO,
                TOKEN_TITULO_CONFERIDO.BACHAREL,
                TOKEN_TITULO_CONFERIDO.MEDICO
            };

            List<string> listaTokensTituloConferidoFeminino = new List<string>()
            {
                TOKEN_TITULO_CONFERIDO.BACHAREL
            };

            if (!string.IsNullOrEmpty(tituloConferido))
            {
                if (dadosEmissao.Sexo == Sexo.Masculino)
                {
                    if (listaTokensTituloConferidoMasculino.Any(titulo => titulo == tituloConferido))
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.TituloConferido.Titulo = FormatarString.Truncate(tituloConferido, 255);
                    }
                    else
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.TituloConferido.OutroTitulo = FormatarString.Truncate(tituloConferido, 255);
                    }
                }
                else
                {
                    if (listaTokensTituloConferidoFeminino.Any(titulo => titulo == tituloConferido))
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.TituloConferido.Titulo = FormatarString.Truncate(tituloConferido, 255);
                    }
                    else
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.TituloConferido.OutroTitulo = FormatarString.Truncate(tituloConferido, 255);
                    }
                }
            }
            else
            {
                if (dadosFormacao.SeqTitulacao.HasValue)
                {
                    var titulacao = TitulacaoDomainService.SearchByKey(new SMCSeqSpecification<Titulacao>(dadosFormacao.SeqTitulacao.Value));

                    if (titulacao != null)
                    {
                        if (!string.IsNullOrEmpty(titulacao.DescricaoXSD))
                        {
                            modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.TituloConferido.Titulo = FormatarString.Truncate(titulacao.DescricaoXSD, 255);
                        }
                        else
                        {
                            if (dadosEmissao.Sexo == Sexo.Masculino)
                            {
                                if (listaTokensTituloConferidoMasculino.Any(titulo => titulo == titulacao.DescricaoMasculino))
                                {
                                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.TituloConferido.Titulo = FormatarString.Truncate(titulacao.DescricaoMasculino, 255);
                                }
                                else
                                {
                                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.TituloConferido.OutroTitulo = FormatarString.Truncate(titulacao.DescricaoMasculino, 255);
                                }
                            }
                            else
                            {
                                if (listaTokensTituloConferidoFeminino.Any(titulo => titulo == titulacao.DescricaoFeminino))
                                {
                                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.TituloConferido.Titulo = FormatarString.Truncate(titulacao.DescricaoFeminino, 255);
                                }
                                else
                                {
                                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.TituloConferido.OutroTitulo = FormatarString.Truncate(titulacao.DescricaoFeminino, 255);
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("titulação");
                    }
                }
                else
                {
                    throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("sequencial da titulação");
                }
            }

            if (seqGrauAcademicoConferido.HasValue)
            {
                var grauAcademico = GrauAcademicoDomainService.SearchByKey(new SMCSeqSpecification<GrauAcademico>(seqGrauAcademicoConferido.Value));

                if (grauAcademico != null)
                {
                    if (!string.IsNullOrEmpty(grauAcademico.DescricaoXSD))
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.GrauConferido = FormatarString.Truncate(grauAcademico.DescricaoXSD, 255);
                    }
                    else
                    {
                        throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("descrição XSD do grau acadêmico");
                    }
                }
                else
                {
                    throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("grau acadêmico");
                }
            }
            else
            {
                var specCursoFormacaoEspecifica = new CursoFormacaoEspecificaFilterSpecification() { SeqCurso = dadosEmissao.SeqCurso, SeqFormacaoEspecifica = dadosFormacao.SeqFormacaoEspecifica };
                var seqGrauAcademicoFormacao = this.CursoFormacaoEspecificaDomainService.SearchProjectionByKey(specCursoFormacaoEspecifica, x => x.SeqGrauAcademico);

                if (seqGrauAcademicoFormacao.HasValue)
                {
                    var grauAcademico = GrauAcademicoDomainService.SearchByKey(new SMCSeqSpecification<GrauAcademico>(seqGrauAcademicoFormacao.Value));

                    if (grauAcademico != null)
                    {
                        if (!string.IsNullOrEmpty(grauAcademico.DescricaoXSD))
                        {
                            modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.GrauConferido = FormatarString.Truncate(grauAcademico.DescricaoXSD, 255);
                        }
                        else
                        {
                            throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("descrição XSD do grau acadêmico");
                        }
                    }
                    else
                    {
                        throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("grau acadêmico");
                    }
                }
                else
                {
                    throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("sequencial do grau acadêmico");
                }
            }

            modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.CodigoCursoEMEC = cursoOfertaLocalidade.CodigoOrgaoRegulador.Value;
            modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.EnderecoCurso.Logradouro = cursoOfertaLocalidade.Endereco.Logradouro;

            if (!string.IsNullOrEmpty(cursoOfertaLocalidade.Endereco.Numero))
                modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.EnderecoCurso.Numero = cursoOfertaLocalidade.Endereco.Numero;

            if (!string.IsNullOrEmpty(cursoOfertaLocalidade.Endereco.Complemento))
                modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.EnderecoCurso.Complemento = cursoOfertaLocalidade.Endereco.Complemento;

            modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.EnderecoCurso.Bairro = cursoOfertaLocalidade.Endereco.Bairro;
            modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.EnderecoCurso.Cep = cursoOfertaLocalidade.Endereco.Cep;
            modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.EnderecoCurso.Uf = cursoOfertaLocalidade.Endereco.Uf;
            modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.EnderecoCurso.NomeMunicipio = cursoOfertaLocalidade.Endereco.NomeMunicipio;
            modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.EnderecoCurso.CodigoMunicipio = cursoOfertaLocalidade.Endereco.CodigoMunicipio;

            var atosNormativosCurso = BuscarAtosNormativosCurso(dadosEmissao.SeqCursoOfertaLocalidade, dadosEmissao.SeqGrauAcademicoSelecionado, dataConclusao, true);

            if (atosNormativosCurso.Autorizacao != null)
            {
                modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Autorizacao.Numero = atosNormativosCurso.Autorizacao.Numero;
                modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Autorizacao.Data = atosNormativosCurso.Autorizacao.Data;
                modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Autorizacao.Tipo = atosNormativosCurso.Autorizacao.Tipo;

                if (!string.IsNullOrEmpty(atosNormativosCurso.Autorizacao.VeiculoPublicacao))
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Autorizacao.VeiculoPublicacao = atosNormativosCurso.Autorizacao.VeiculoPublicacao;
                }

                if (atosNormativosCurso.Autorizacao.DataPublicacao.HasValue)
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Autorizacao.DataPublicacao = atosNormativosCurso.Autorizacao.DataPublicacao.Value.Date;
                }

                if (atosNormativosCurso.Autorizacao.SecaoPublicacao.HasValue)
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Autorizacao.SecaoPublicacao = atosNormativosCurso.Autorizacao.SecaoPublicacao;
                }

                if (atosNormativosCurso.Autorizacao.PaginaPublicacao.HasValue)
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Autorizacao.PaginaPublicacao = atosNormativosCurso.Autorizacao.PaginaPublicacao;
                }

                if (atosNormativosCurso.Autorizacao.NumeroDOU.HasValue)
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Autorizacao.NumeroDOU = atosNormativosCurso.Autorizacao.NumeroDOU;
                }
            }
            if (atosNormativosCurso.Reconhecimento != null)
            {
                modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Reconhecimento.Numero = atosNormativosCurso.Reconhecimento.Numero;
                modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Reconhecimento.Data = atosNormativosCurso.Reconhecimento.Data;
                modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Reconhecimento.Tipo = atosNormativosCurso.Reconhecimento.Tipo;

                if (!string.IsNullOrEmpty(atosNormativosCurso.Reconhecimento.VeiculoPublicacao))
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Reconhecimento.VeiculoPublicacao = atosNormativosCurso.Reconhecimento.VeiculoPublicacao;
                }

                if (atosNormativosCurso.Reconhecimento.DataPublicacao.HasValue)
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Reconhecimento.DataPublicacao = atosNormativosCurso.Reconhecimento.DataPublicacao.Value.Date;
                }

                if (atosNormativosCurso.Reconhecimento.SecaoPublicacao.HasValue)
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Reconhecimento.SecaoPublicacao = atosNormativosCurso.Reconhecimento.SecaoPublicacao;
                }

                if (atosNormativosCurso.Reconhecimento.PaginaPublicacao.HasValue)
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Reconhecimento.PaginaPublicacao = atosNormativosCurso.Reconhecimento.PaginaPublicacao;
                }

                if (atosNormativosCurso.Reconhecimento.NumeroDOU.HasValue)
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.Reconhecimento.NumeroDOU = atosNormativosCurso.Reconhecimento.NumeroDOU;
                }
            }
            if (atosNormativosCurso.RenovacaoReconhecimento != null)
            {
                modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.RenovacaoReconhecimento.Numero = atosNormativosCurso.RenovacaoReconhecimento.Numero;
                modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.RenovacaoReconhecimento.Data = atosNormativosCurso.RenovacaoReconhecimento.Data;
                modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.RenovacaoReconhecimento.Tipo = atosNormativosCurso.RenovacaoReconhecimento.Tipo;

                if (!string.IsNullOrEmpty(atosNormativosCurso.RenovacaoReconhecimento.VeiculoPublicacao))
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.RenovacaoReconhecimento.VeiculoPublicacao = atosNormativosCurso.RenovacaoReconhecimento.VeiculoPublicacao;
                }

                if (atosNormativosCurso.RenovacaoReconhecimento.DataPublicacao.HasValue)
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.RenovacaoReconhecimento.DataPublicacao = atosNormativosCurso.RenovacaoReconhecimento.DataPublicacao.Value.Date;
                }

                if (atosNormativosCurso.RenovacaoReconhecimento.SecaoPublicacao.HasValue)
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.RenovacaoReconhecimento.SecaoPublicacao = atosNormativosCurso.RenovacaoReconhecimento.SecaoPublicacao;
                }

                if (atosNormativosCurso.RenovacaoReconhecimento.PaginaPublicacao.HasValue)
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.RenovacaoReconhecimento.PaginaPublicacao = atosNormativosCurso.RenovacaoReconhecimento.PaginaPublicacao;
                }

                if (atosNormativosCurso.RenovacaoReconhecimento.NumeroDOU.HasValue)
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.DadosCurso.RenovacaoReconhecimento.NumeroDOU = atosNormativosCurso.RenovacaoReconhecimento.NumeroDOU;
                }
            }

            #endregion

            #region Dados da Instituição Emissora e Registradora

            if (dadosEmissao.SeqInstituicaoEnsino.HasValue)
            {
                var instituicaoEnsino = InstituicaoEnsinoDomainService.BuscarDadosInstituicaoEnsinoParaDocumentoAcademico(dadosEmissao.SeqInstituicaoEnsino.Value);

                modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Nome = instituicaoEnsino.Nome;
                modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.CodigoMEC = instituicaoEnsino.CodigoMEC;

                modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Nome = instituicaoEnsino.Nome;
                modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.CodigoMEC = instituicaoEnsino.CodigoMEC;

                modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.AutorizacaoRegistro = null;
                modeloDiploma.Degree.Diploma.DadosRegistro.InformacoesProcessoJudicial = null;

                modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Cnpj = instituicaoEnsino.Cnpj;
                modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Cnpj = instituicaoEnsino.Cnpj;

                modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Endereco.Logradouro = instituicaoEnsino.Endereco.Logradouro;
                modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Endereco.Logradouro = instituicaoEnsino.Endereco.Logradouro;

                if (!string.IsNullOrEmpty(instituicaoEnsino.Endereco.Numero))
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Endereco.Numero = instituicaoEnsino.Endereco.Numero;
                    modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Endereco.Numero = instituicaoEnsino.Endereco.Numero;
                }

                if (!string.IsNullOrEmpty(instituicaoEnsino.Endereco.Complemento))
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Endereco.Complemento = instituicaoEnsino.Endereco.Complemento;
                    modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Endereco.Complemento = instituicaoEnsino.Endereco.Complemento;
                }

                modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Endereco.Bairro = instituicaoEnsino.Endereco.Bairro;
                modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Endereco.Bairro = instituicaoEnsino.Endereco.Bairro;

                modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Endereco.Cep = instituicaoEnsino.Endereco.Cep;
                modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Endereco.Cep = instituicaoEnsino.Endereco.Cep;


                modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Endereco.Uf = instituicaoEnsino.Endereco.Uf;
                modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Endereco.Uf = instituicaoEnsino.Endereco.Uf;

                modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Endereco.NomeMunicipio = instituicaoEnsino.Endereco.NomeMunicipio;
                modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Endereco.NomeMunicipio = instituicaoEnsino.Endereco.NomeMunicipio;

                modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Endereco.CodigoMunicipio = instituicaoEnsino.Endereco.CodigoMunicipio;
                modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Endereco.CodigoMunicipio = instituicaoEnsino.Endereco.CodigoMunicipio;

                var atosNormativosInstituicao = AtoNormativoEntidadeDomainService.BuscarAtoNormativoEntidadeInstituicao(dadosEmissao.SeqInstituicaoEnsino.Value, dataConclusao);

                if (atosNormativosInstituicao.Credenciamento != null)
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Credenciamento.Numero = atosNormativosInstituicao.Credenciamento.Numero;
                    modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Credenciamento.Data = atosNormativosInstituicao.Credenciamento.Data;

                    modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Credenciamento.Numero = atosNormativosInstituicao.Credenciamento.Numero;
                    modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Credenciamento.Data = atosNormativosInstituicao.Credenciamento.Data;

                    modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Credenciamento.Tipo = atosNormativosInstituicao.Credenciamento.Tipo;
                    modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Credenciamento.Tipo = atosNormativosInstituicao.Credenciamento.Tipo;

                    if (!string.IsNullOrEmpty(atosNormativosInstituicao.Credenciamento.VeiculoPublicacao))
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Credenciamento.VeiculoPublicacao = atosNormativosInstituicao.Credenciamento.VeiculoPublicacao;
                        modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Credenciamento.VeiculoPublicacao = atosNormativosInstituicao.Credenciamento.VeiculoPublicacao;
                    }

                    if (atosNormativosInstituicao.Credenciamento.DataPublicacao.HasValue)
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Credenciamento.DataPublicacao = atosNormativosInstituicao.Credenciamento.DataPublicacao.Value.Date;
                        modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Credenciamento.DataPublicacao = atosNormativosInstituicao.Credenciamento.DataPublicacao.Value.Date;
                    }

                    if (atosNormativosInstituicao.Credenciamento.SecaoPublicacao.HasValue)
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Credenciamento.SecaoPublicacao = atosNormativosInstituicao.Credenciamento.SecaoPublicacao;
                        modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Credenciamento.SecaoPublicacao = atosNormativosInstituicao.Credenciamento.SecaoPublicacao;
                    }

                    if (atosNormativosInstituicao.Credenciamento.PaginaPublicacao.HasValue)
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Credenciamento.PaginaPublicacao = atosNormativosInstituicao.Credenciamento.PaginaPublicacao;
                        modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Credenciamento.PaginaPublicacao = atosNormativosInstituicao.Credenciamento.PaginaPublicacao;
                    }

                    if (atosNormativosInstituicao.Credenciamento.NumeroDOU.HasValue)
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Credenciamento.NumeroDOU = atosNormativosInstituicao.Credenciamento.NumeroDOU;
                        modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Credenciamento.NumeroDOU = atosNormativosInstituicao.Credenciamento.NumeroDOU;
                    }
                }
                if (atosNormativosInstituicao.Recredenciamento != null)
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Recredenciamento.Numero = atosNormativosInstituicao.Recredenciamento.Numero;
                    modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Recredenciamento.Data = atosNormativosInstituicao.Recredenciamento.Data;

                    modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Recredenciamento.Numero = atosNormativosInstituicao.Recredenciamento.Numero;
                    modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Recredenciamento.Data = atosNormativosInstituicao.Recredenciamento.Data;

                    modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Recredenciamento.Tipo = atosNormativosInstituicao.Recredenciamento.Tipo;
                    modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Recredenciamento.Tipo = atosNormativosInstituicao.Recredenciamento.Tipo;

                    if (!string.IsNullOrEmpty(atosNormativosInstituicao.Recredenciamento.VeiculoPublicacao))
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Recredenciamento.VeiculoPublicacao = atosNormativosInstituicao.Recredenciamento.VeiculoPublicacao;
                        modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Recredenciamento.VeiculoPublicacao = atosNormativosInstituicao.Recredenciamento.VeiculoPublicacao;
                    }

                    if (atosNormativosInstituicao.Recredenciamento.DataPublicacao.HasValue)
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Recredenciamento.DataPublicacao = atosNormativosInstituicao.Recredenciamento.DataPublicacao.Value.Date;
                        modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Recredenciamento.DataPublicacao = atosNormativosInstituicao.Recredenciamento.DataPublicacao.Value.Date;
                    }

                    if (atosNormativosInstituicao.Recredenciamento.SecaoPublicacao.HasValue)
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Recredenciamento.SecaoPublicacao = atosNormativosInstituicao.Recredenciamento.SecaoPublicacao;
                        modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Recredenciamento.SecaoPublicacao = atosNormativosInstituicao.Recredenciamento.SecaoPublicacao;
                    }

                    if (atosNormativosInstituicao.Recredenciamento.PaginaPublicacao.HasValue)
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Recredenciamento.PaginaPublicacao = atosNormativosInstituicao.Recredenciamento.PaginaPublicacao;
                        modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Recredenciamento.PaginaPublicacao = atosNormativosInstituicao.Recredenciamento.PaginaPublicacao;
                    }

                    if (atosNormativosInstituicao.Recredenciamento.NumeroDOU.HasValue)
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Recredenciamento.NumeroDOU = atosNormativosInstituicao.Recredenciamento.NumeroDOU;
                        modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Recredenciamento.NumeroDOU = atosNormativosInstituicao.Recredenciamento.NumeroDOU;
                    }
                }
                if (atosNormativosInstituicao.RenovacaoDeRecredenciamento != null)
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.RenovacaoDeRecredenciamento.Numero = atosNormativosInstituicao.RenovacaoDeRecredenciamento.Numero;
                    modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.RenovacaoDeRecredenciamento.Data = atosNormativosInstituicao.RenovacaoDeRecredenciamento.Data;

                    modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.RenovacaoDeRecredenciamento.Numero = atosNormativosInstituicao.RenovacaoDeRecredenciamento.Numero;
                    modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.RenovacaoDeRecredenciamento.Data = atosNormativosInstituicao.RenovacaoDeRecredenciamento.Data;

                    modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.RenovacaoDeRecredenciamento.Tipo = atosNormativosInstituicao.RenovacaoDeRecredenciamento.Tipo;
                    modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.RenovacaoDeRecredenciamento.Tipo = atosNormativosInstituicao.RenovacaoDeRecredenciamento.Tipo;

                    if (!string.IsNullOrEmpty(atosNormativosInstituicao.RenovacaoDeRecredenciamento.VeiculoPublicacao))
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.RenovacaoDeRecredenciamento.VeiculoPublicacao = atosNormativosInstituicao.RenovacaoDeRecredenciamento.VeiculoPublicacao;
                        modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.RenovacaoDeRecredenciamento.VeiculoPublicacao = atosNormativosInstituicao.RenovacaoDeRecredenciamento.VeiculoPublicacao;
                    }

                    if (atosNormativosInstituicao.RenovacaoDeRecredenciamento.DataPublicacao.HasValue)
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.RenovacaoDeRecredenciamento.DataPublicacao = atosNormativosInstituicao.RenovacaoDeRecredenciamento.DataPublicacao.Value.Date;
                        modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.RenovacaoDeRecredenciamento.DataPublicacao = atosNormativosInstituicao.RenovacaoDeRecredenciamento.DataPublicacao.Value.Date;
                    }

                    if (atosNormativosInstituicao.RenovacaoDeRecredenciamento.SecaoPublicacao.HasValue)
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.RenovacaoDeRecredenciamento.SecaoPublicacao = atosNormativosInstituicao.RenovacaoDeRecredenciamento.SecaoPublicacao;
                        modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.RenovacaoDeRecredenciamento.SecaoPublicacao = atosNormativosInstituicao.RenovacaoDeRecredenciamento.SecaoPublicacao;
                    }

                    if (atosNormativosInstituicao.RenovacaoDeRecredenciamento.PaginaPublicacao.HasValue)
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.RenovacaoDeRecredenciamento.PaginaPublicacao = atosNormativosInstituicao.RenovacaoDeRecredenciamento.PaginaPublicacao;
                        modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.RenovacaoDeRecredenciamento.PaginaPublicacao = atosNormativosInstituicao.RenovacaoDeRecredenciamento.PaginaPublicacao;
                    }

                    if (atosNormativosInstituicao.RenovacaoDeRecredenciamento.NumeroDOU.HasValue)
                    {
                        modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.RenovacaoDeRecredenciamento.NumeroDOU = atosNormativosInstituicao.RenovacaoDeRecredenciamento.NumeroDOU;
                        modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.RenovacaoDeRecredenciamento.NumeroDOU = atosNormativosInstituicao.RenovacaoDeRecredenciamento.NumeroDOU;
                    }
                }

                var mantenedora = MantenedoraDomainService.BuscarMantenedoraParaDocumentoAcademico(instituicaoEnsino.SeqMantenedora);

                modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Mantenedora.RazaoSocial = mantenedora.RazaoSocial;
                modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Mantenedora.RazaoSocial = mantenedora.RazaoSocial;

                modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Mantenedora.Cnpj = mantenedora.Cnpj;
                modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Mantenedora.Cnpj = mantenedora.Cnpj;

                modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Mantenedora.Endereco.Logradouro = mantenedora.Endereco.Logradouro;
                modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Mantenedora.Endereco.Logradouro = mantenedora.Endereco.Logradouro;

                if (!string.IsNullOrEmpty(mantenedora.Endereco.Numero))
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Mantenedora.Endereco.Numero = mantenedora.Endereco.Numero;
                    modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Mantenedora.Endereco.Numero = mantenedora.Endereco.Numero;
                }

                if (!string.IsNullOrEmpty(mantenedora.Endereco.Complemento))
                {
                    modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Mantenedora.Endereco.Complemento = mantenedora.Endereco.Complemento;
                    modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Mantenedora.Endereco.Complemento = mantenedora.Endereco.Complemento;
                }

                modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Mantenedora.Endereco.Bairro = mantenedora.Endereco.Bairro;
                modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Mantenedora.Endereco.Bairro = mantenedora.Endereco.Bairro;

                modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Mantenedora.Endereco.Cep = mantenedora.Endereco.Cep;
                modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Mantenedora.Endereco.Cep = mantenedora.Endereco.Cep;

                modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Mantenedora.Endereco.Uf = mantenedora.Endereco.Uf;
                modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Mantenedora.Endereco.Uf = mantenedora.Endereco.Uf;

                modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Mantenedora.Endereco.NomeMunicipio = mantenedora.Endereco.NomeMunicipio;
                modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Mantenedora.Endereco.NomeMunicipio = mantenedora.Endereco.NomeMunicipio;

                modeloDiploma.Degree.Diploma.DadosDiploma.IesEmissora.Mantenedora.Endereco.CodigoMunicipio = mantenedora.Endereco.CodigoMunicipio;
                modeloDiploma.Degree.Diploma.DadosRegistro.IesRegistradora.Mantenedora.Endereco.CodigoMunicipio = mantenedora.Endereco.CodigoMunicipio;
            }
            else
            {
                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("instituição de ensino do aluno");
            }

            #endregion

            #region Dados dos Assinantes

            var dataAtual = DateTime.Today;

            //Asssinantes diploma
            var tokensTiposFuncionariosAssinantesDiploma = new List<string>() { TOKEN_TIPO_FUNCIONARIO.REITOR, TOKEN_TIPO_FUNCIONARIO.PRO_REITOR };
            var specTiposFuncionariosAssinantesDiploma = new TipoFuncionarioFilterSpecification() { Tokens = tokensTiposFuncionariosAssinantesDiploma };
            var tiposFuncionariosAssinantesDiploma = this.TipoFuncionarioDomainService.SearchBySpecification(specTiposFuncionariosAssinantesDiploma).ToList();

            var specFuncionariosAssinantesDiploma = new FuncionarioVinculoFilterSpecification() { SeqsTipoFuncionario = tiposFuncionariosAssinantesDiploma.Select(a => a.Seq).ToList() };
            var funcionariosAssinantesDiploma = this.FuncionarioVinculoDomainService.SearchBySpecification(specFuncionariosAssinantesDiploma, a => a.Funcionario.Pessoa, a => a.Funcionario.DadosPessoais, a => a.TipoFuncionario).ToList();
            var funcionariosAssinantesDiplomaAtivos = funcionariosAssinantesDiploma.Where(vinculo => vinculo.DataInicio <= dataAtual && (!vinculo.DataFim.HasValue || vinculo.DataFim >= dataAtual)).ToList();

            var listaAssinantesDiploma = new List<InformacaoAssinanteVO>();
            foreach (var funcionarioBanco in funcionariosAssinantesDiplomaAtivos)
            {
                var funcionarioLacuna = new InformacaoAssinanteVO();

                var auxCpf = FormatarString.ObterNumerosComVirgula(funcionarioBanco.Funcionario.Pessoa.Cpf);
                funcionarioLacuna.Cpf = auxCpf;

                if (funcionarioBanco.TipoFuncionario.DescricaoMasculino == DESCRICAO_TIPO_FUNCIONARIO.REITOR)
                    funcionarioLacuna.Cargo = funcionarioBanco.TipoFuncionario.DescricaoMasculino;
                else
                {
                    var cargoSexoFuncionario = funcionarioBanco.Funcionario.DadosPessoais.Sexo == Sexo.Masculino ? funcionarioBanco.TipoFuncionario.DescricaoMasculino : funcionarioBanco.TipoFuncionario.DescricaoFeminino;
                    funcionarioLacuna.OutroCargo = cargoSexoFuncionario;
                }

                listaAssinantesDiploma.Add(funcionarioLacuna);
            }

            modeloDiploma.Degree.Diploma.DadosDiploma.Assinantes = listaAssinantesDiploma;

            //Assinantes registro
            var tokensTiposFuncionariosAssinantesRegistro = new List<string>() { TOKEN_TIPO_FUNCIONARIO.CHEFE_CRA };
            var specTiposFuncionariosAssinantesRegistro = new TipoFuncionarioFilterSpecification() { Tokens = tokensTiposFuncionariosAssinantesRegistro };
            var tiposFuncionariosAssinantesRegistro = this.TipoFuncionarioDomainService.SearchBySpecification(specTiposFuncionariosAssinantesRegistro).ToList();

            var specFuncionariosAssinantesRegistro = new FuncionarioVinculoFilterSpecification() { SeqsTipoFuncionario = tiposFuncionariosAssinantesRegistro.Select(a => a.Seq).ToList() };
            var funcionariosAssinantesRegistro = this.FuncionarioVinculoDomainService.SearchBySpecification(specFuncionariosAssinantesRegistro, a => a.Funcionario.Pessoa, a => a.Funcionario.DadosPessoais, a => a.TipoFuncionario).ToList();
            var funcionariosAssinantesRegistroAtivos = funcionariosAssinantesRegistro.Where(vinculo => vinculo.DataInicio <= dataAtual && (!vinculo.DataFim.HasValue || vinculo.DataFim >= dataAtual)).ToList();

            var listaAssinantesRegistro = new List<InformacaoAssinanteVO>();
            foreach (var funcionarioBanco in funcionariosAssinantesRegistroAtivos)
            {
                var funcionarioLacuna = new InformacaoAssinanteVO();

                var auxCpf = FormatarString.ObterNumerosComVirgula(funcionarioBanco.Funcionario.Pessoa.Cpf);
                funcionarioLacuna.Cpf = auxCpf;

                var cargoSexoFuncionario = funcionarioBanco.Funcionario.DadosPessoais.Sexo == Sexo.Masculino ? funcionarioBanco.TipoFuncionario.DescricaoMasculino : funcionarioBanco.TipoFuncionario.DescricaoFeminino;
                funcionarioLacuna.OutroCargo = cargoSexoFuncionario;

                listaAssinantesRegistro.Add(funcionarioLacuna);
            }

            modeloDiploma.Degree.Diploma.DadosRegistro.Assinantes = listaAssinantesRegistro;

            #endregion

            #region Dados do Registro

            var responsavelRegistro = funcionariosAssinantesRegistroAtivos.FirstOrDefault();
            var auxCpfResponsavel = FormatarString.ObterNumerosComVirgula(responsavelRegistro.Funcionario.Pessoa.Cpf);
            modeloDiploma.Degree.Diploma.DadosRegistro.LivroRegistro.ProcessoDoDiploma = documentoConclusao.NumeroProcesso;
            modeloDiploma.Degree.Diploma.DadosRegistro.LivroRegistro.DataExpedicaoDiploma = DateTime.Now;
            modeloDiploma.Degree.Diploma.DadosRegistro.LivroRegistro.ResponsavelRegistro.Nome = responsavelRegistro.Funcionario.DadosPessoais.Nome;
            modeloDiploma.Degree.Diploma.DadosRegistro.LivroRegistro.ResponsavelRegistro.Cpf = FormatarString.Truncate(auxCpfResponsavel, 11);

            if (documentoConclusao.SeqGrupoRegistro.HasValue)
            {
                var grupoRegistro = GrupoRegistroDomainService.SearchByKey(new SMCSeqSpecification<GrupoRegistro>(documentoConclusao.SeqGrupoRegistro.Value));
                modeloDiploma.Degree.Diploma.DadosRegistro.LivroRegistro.LivroRegistro = $"SGA - {grupoRegistro.Descricao}";
            }
            else
            {
                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("grupo de registro");
            }

            if (!string.IsNullOrEmpty(documentoConclusao.NumeroRegistro))
            {
                modeloDiploma.Degree.Diploma.DadosRegistro.LivroRegistro.NumeroRegistro = documentoConclusao.NumeroRegistro;
            }
            else
            {
                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("número do registro do documento de conclusão");
            }

            if (dataColacao.HasValue)
            {
                modeloDiploma.Degree.Diploma.DadosRegistro.LivroRegistro.DataColacaoGrau = dataColacao.Value.Date;
            }
            else
            {
                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("data da colação de grau");
            }

            if (documentoConclusao.DataRegistro.HasValue)
            {
                modeloDiploma.Degree.Diploma.DadosRegistro.LivroRegistro.DataRegistroDiploma = documentoConclusao.DataRegistro.Value.Date;
            }
            else
            {
                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("data do registro do documento de conclusão");
            }

            var keySessionMensagemEmissao = string.Format(KEY_SESSION_MENSAGEM.KEY_SESSION_MENSAGEM_EMISSAO, dadosEmissao.SeqSolicitacaoServico);
            var listaMensagensSession = HttpContext.Current.Session[keySessionMensagemEmissao] as List<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO>;

            if (listaMensagensSession != null && listaMensagensSession.Any())
            {
                var mensagemTelaDiploma = listaMensagensSession.Where(w => w.DocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL).ToList();
                if (mensagemTelaDiploma != null && mensagemTelaDiploma.Any())
                {
                    var listaInformacoesAdicionaisDiploma = new List<string>();
                    foreach (var mensagemTela in mensagemTelaDiploma)
                    {
                        var formatado = HttpUtility.HtmlDecode(mensagemTela.DescricaoDecode);
                        listaInformacoesAdicionaisDiploma.Add(formatado);
                    }

                    modeloDiploma.Degree.Diploma.DadosRegistro.InformacoesAdicionais = string.Join(" | ", listaInformacoesAdicionaisDiploma);
                }

                var mensagemTelaHistorico = listaMensagensSession.Where(w => w.DocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR).ToList();
                if (mensagemTelaHistorico != null && mensagemTelaHistorico.Any())
                {
                    var listaInformacoesAdicionaisHistorico = new List<string>();
                    foreach (var mensagemTela in mensagemTelaHistorico)
                    {
                        var formatado = HttpUtility.HtmlDecode(mensagemTela.DescricaoDecode);
                        listaInformacoesAdicionaisHistorico.Add(formatado);
                    }

                    modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.InformacoesAdicionais = string.Join(" | ", listaInformacoesAdicionaisHistorico);
                }
            }

            #endregion

            #region Dados Privados

            if (dadosEmissao.NumeroVia > 1)
                modeloDiploma.Degree.DocumentacaoAcademica.Registro.IsSegundaVia = true;
            else
                modeloDiploma.Degree.DocumentacaoAcademica.Registro.IsSegundaVia = false;

            if (dadosEmissao.Filiacao != null && dadosEmissao.Filiacao.Any())
            {
                foreach (var filiacao in dadosEmissao.Filiacao)
                {
                    modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Filiacao.Add(new FiliacaoVO()
                    {
                        Nome = FormatarString.Truncate(filiacao.Nome, 255),
                        Sexo = filiacao.TipoParentesco == TipoParentesco.Pai ? "M" : "F"
                    });
                }
            }
            else
            {
                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("filiação do aluno");
            }

            modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Ingresso.Data = dadosEmissao.DataAdmissao.Date;
            modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.Ingresso.Data = dadosEmissao.DataAdmissao.Date;

            //var descricaoXSDFormaIngresso = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(dadosEmissao.SeqPessoaAtuacao), x => x.Historicos.FirstOrDefault(f => f.Atual).FormaIngresso.DescricaoXSD);

            if (!string.IsNullOrEmpty(dadosEmissao.DescricaoXSDFormaIngresso))
            {
                modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Ingresso.FormaAcesso = FormatarString.Truncate(dadosEmissao.DescricaoXSDFormaIngresso, 255);
                modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.Ingresso.FormaAcesso = FormatarString.Truncate(dadosEmissao.DescricaoXSDFormaIngresso, 255);
            }
            else
            {
                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("descrição XSD da forma de ingresso do aluno");
            }

            if (!dadosEmissao.CodigoAlunoMigracao.HasValue)
                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("código de aluno migração");

            //A procedure por enquanto irá retornar sempre uma linha de situação enade por aluno
            var dadosSituacaoEnade = IntegracaoAcademicoService.BuscarSituacaoEnade(dadosEmissao.CodigoAlunoMigracao.Value, dadosEmissao.CodigoCursoOfertaLocalidade);

            if (dadosEmissao.NumeroVia == 1 && !(dadosSituacaoEnade != null && dadosSituacaoEnade.Any()))
                throw new SolicitacaoDocumentoConclusaoDadosEnadeNaoEncontradoException();

            if (dadosEmissao.NumeroVia == 1 && string.IsNullOrEmpty(dadosSituacaoEnade.First()?.Categoria))
                throw new SolicitacaoDocumentoConclusaoDadosEnadeNaoEncontradoException();

            var listaParticipacoesEnade = new List<ParticipacaoEnadeVO>();
            if (dadosSituacaoEnade != null && dadosSituacaoEnade.Any())
                foreach (var situacaoEnade in dadosSituacaoEnade)
                {
                    if (situacaoEnade != null)
                    {
                        if (situacaoEnade.Categoria.ToLower().Trim() == CATEGORIA_SITUACAO_ENADE.HABILITADO.ToLower().Trim())
                        {
                            var participacaoEnade = new ParticipacaoEnadeVO()
                            {
                                Situacao = "Habilitado",
                                Informacoes = new InformacaoEnadeVO()
                                {
                                    Condicao = situacaoEnade.Condicao?.Trim(),
                                    Edicao = situacaoEnade.AnoEdicao?.ToString().Trim()
                                }
                            };

                            listaParticipacoesEnade.Add(participacaoEnade);
                        }
                        else if (situacaoEnade.Categoria.ToLower().Trim() == CATEGORIA_SITUACAO_ENADE.NAO_HABILITADO.ToLower().Trim())
                        {
                            var participacaoEnade = new ParticipacaoEnadeVO()
                            {
                                Situacao = "NaoHabilitado",
                                NaoHabilitado = new EnadeNaoHabilitadoVO()
                                {
                                    Condicao = situacaoEnade.Condicao?.Trim(),
                                    Edicao = situacaoEnade.AnoEdicao?.ToString().Trim(),
                                    OutroMotivo = situacaoEnade.SituacaoEnadeXsd?.Trim()
                                }
                            };

                            listaParticipacoesEnade.Add(participacaoEnade);
                        }
                    }
                }

            if (listaParticipacoesEnade.Any())
                modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.ParticipacoesEnade = listaParticipacoesEnade;

            modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.MatrizCurricular = null;
            modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.Areas = null;

            var dadosElementoHistorico = RetornarDadosElementoHistorico(dadosEmissao.CodigoAlunoMigracao.Value, dadosEmissao.NumeroVia, codigoCursoOfertaLocalidade: dadosEmissao.CodigoCursoOfertaLocalidade);

            modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.CodigoCurriculo = dadosElementoHistorico.CodigoCurriculo;
            modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.ElementosHistorico = dadosElementoHistorico.ElementosHistorico;

            var atividadeComplementar = modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.ElementosHistorico.Where(w => w.AtividadeComplementar != null).Select(s => s.AtividadeComplementar);
            atividadeComplementar.SMCForEach(f =>
            {
                if (f.Docentes == null || !f.DataInicio.HasValue || !f.DataFim.HasValue)
                    throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("Atividade complementar com informações incompletas");
            });

            modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.CargaHorariaCursoV2.Tipo = TIPO_CARGA_HORARIA_LACUNA.HORA_RELOGIO;
            modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.CargaHorariaCursoV2.HoraRelogio = dadosElementoHistorico.CargaHorariaCurso;
            modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.DataEmissaoHistorico = dadosEmissao.DataEmissaoHistoricoEmissaoDiploma.HasValue ? dadosEmissao.DataEmissaoHistoricoEmissaoDiploma.Value : DateTime.Now;
            modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.HoraEmissaoHistorico = dadosEmissao.DataEmissaoHistoricoEmissaoDiploma.HasValue ? dadosEmissao.DataEmissaoHistoricoEmissaoDiploma.Value.ToLongTimeString() : DateTime.Now.ToLongTimeString();
            modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.CargaHorariaCurso.Tipo = TIPO_CARGA_HORARIA_LACUNA.HORA_RELOGIO;
            modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.CargaHorariaCurso.HoraRelogio = dadosElementoHistorico.CargaHorariaCurso;
            modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.CargaHorariaCursoIntegralizadaV2.Tipo = TIPO_CARGA_HORARIA_LACUNA.HORA_RELOGIO;
            modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.CargaHorariaCursoIntegralizadaV2.HoraRelogio = dadosElementoHistorico.CargaHorariaCursoIntegralizada;

            if (dadosEmissao.DescricaoXSDSituacaoAtualMatricula == DESCRICAO_XSD_MATRICULA.Formado)
            {
                modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.SituacaoAtualDiscente.Tipo = dadosEmissao.DescricaoXSDSituacaoAtualMatricula;
                modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.SituacaoAtualDiscente.Formado.DataExpedicaoDiploma = DateTime.Now;

                if (dataConclusao.HasValue)
                {
                    modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.SituacaoAtualDiscente.Formado.DataConclusaoCurso = dataConclusao.Value.Date;
                }

                if (dadosFormacao.DataColacaoGrau.HasValue)
                {
                    modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.SituacaoAtualDiscente.Formado.DataColacaoGrau = dadosFormacao.DataColacaoGrau.Value.Date;
                }
            }
            else if (dadosEmissao.DescricaoXSDSituacaoAtualMatricula == DESCRICAO_XSD_MATRICULA.Trancamento ||
                     dadosEmissao.DescricaoXSDSituacaoAtualMatricula == DESCRICAO_XSD_MATRICULA.MatriculadoEmDisciplina ||
                     dadosEmissao.DescricaoXSDSituacaoAtualMatricula == DESCRICAO_XSD_MATRICULA.Licenca ||
                     dadosEmissao.DescricaoXSDSituacaoAtualMatricula == DESCRICAO_XSD_MATRICULA.Desistencia ||
                     dadosEmissao.DescricaoXSDSituacaoAtualMatricula == DESCRICAO_XSD_MATRICULA.Abandono ||
                     dadosEmissao.DescricaoXSDSituacaoAtualMatricula == DESCRICAO_XSD_MATRICULA.Jubilado)
            {
                modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.SituacaoAtualDiscente.Tipo = dadosEmissao.DescricaoXSDSituacaoAtualMatricula;
            }
            else if (dadosEmissao.DescricaoXSDSituacaoAtualMatricula == DESCRICAO_XSD_MATRICULA.OutraSituacao)
            {
                modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.SituacaoAtualDiscente.Tipo = dadosEmissao.DescricaoXSDSituacaoAtualMatricula;
                modeloDiploma.Degree.DocumentacaoAcademica.Registro.DadosPrivadosDiplomado.Historico.SituacaoAtualDiscente.OutraSituacao = dadosEmissao.DescricaoSituacaoAtualMatricula;
            }

            if (modeloTela.DocumentacaoComprobatoria != null && modeloTela.DocumentacaoComprobatoria.Any())
            {
                modeloDiploma.Degree.DocumentacaoAcademica.Registro.DocumentacaoComprobatoria = new List<DocumentacaoComprobatoriaVO>();

                var tiposDocumentoComprobatorios = BuscarTiposDocumentoDocumentacaoComprobatoria();

                foreach (var documento in modeloTela.DocumentacaoComprobatoria)
                {
                    var documentacaoComprobatoria = new DocumentacaoComprobatoriaVO()
                    {
                        Observacoes = documento.Observacao?.Length > 255 ? documento.Observacao?.Substring(0, 255) : documento.Observacao
                    };

                    var tipoDocumento = tiposDocumentoComprobatorios.FirstOrDefault(a => a.SeqTipoDocumento == documento.SeqTipoDocumento);

                    if (tipoDocumento == null)
                        throw new SolicitacaoDocumentoConclusaoDocumentacaoComprobatoriaExcedenteException(documento.DescricaoTipoDocumento);

                    documentacaoComprobatoria.Tipo = FormatarString.Truncate(tipoDocumento.ValorEnumLacuna, 255);

                    var dominio = documento.Transform<SolicitacaoDocumentoRequerido>();

                    if (dominio.ArquivoAnexado.Conteudo == null)
                    {
                        //Quando só carrega e não altera o arquivo nao vem o Conteudo preenchido
                        //O conteúdo é preenchido se for um novo registro ou se alterou o arquivo de um registro existente
                        this.EnsureFileIntegrity(dominio, x => x.SeqArquivoAnexado, x => x.ArquivoAnexado);
                    }

                    string pdfABase64 = Convert.ToBase64String(dominio.ArquivoAnexado.Conteudo);
                    documentacaoComprobatoria.PdfA = pdfABase64;

                    modeloDiploma.Degree.DocumentacaoAcademica.Registro.DocumentacaoComprobatoria.Add(documentacaoComprobatoria);
                }
            }
            else
            {
                modeloDiploma.Degree.DocumentacaoAcademica.Registro.DocumentacaoComprobatoria = null;
            }

            var specSolicitacaoDocumentoConclusao = new SMCSeqSpecification<SolicitacaoDocumentoConclusao>(dadosEmissao.SeqSolicitacaoServico);
            var solicitacaoDocumentoConclusao = this.SearchByKey(specSolicitacaoDocumentoConclusao);
            solicitacaoDocumentoConclusao.CargaHorariaCurso = (decimal?)dadosElementoHistorico.CargaHorariaCurso;
            solicitacaoDocumentoConclusao.CargaHorariaIntegralizada = (decimal?)dadosElementoHistorico.CargaHorariaCursoIntegralizada;
            solicitacaoDocumentoConclusao.DataEmissaoHistorico = DateTime.Now;

            var dadoSituacaoEnade = dadosSituacaoEnade?.OrderBy(o => o.AnoEdicao).FirstOrDefault();
            if (dadoSituacaoEnade != null)
            {
                var situacaoEnadeAuxiliar = string.Empty;

                if (dadoSituacaoEnade.Categoria.ToLower().Trim() == CATEGORIA_SITUACAO_ENADE.HABILITADO.ToLower().Trim())
                {
                    situacaoEnadeAuxiliar = $"{CATEGORIA_SITUACAO_ENADE.HABILITADO} - {dadoSituacaoEnade.Condicao?.Trim()} - {dadoSituacaoEnade.AnoEdicao?.ToString().Trim()} - {dadoSituacaoEnade.SituacaoEnadeXsd?.Trim()}";
                }
                else if (dadoSituacaoEnade.Categoria.ToLower().Trim() == CATEGORIA_SITUACAO_ENADE.NAO_HABILITADO.ToLower().Trim())
                {
                    situacaoEnadeAuxiliar = $"{CATEGORIA_SITUACAO_ENADE.NAO_HABILITADO} - {dadoSituacaoEnade.SituacaoEnadeXsd?.Trim()}";
                }

                solicitacaoDocumentoConclusao.SituacaoEnade = situacaoEnadeAuxiliar;
            }

            this.SaveEntity(solicitacaoDocumentoConclusao);

            #endregion

            //string requisicaoJson = JsonConvert.SerializeObject(modeloDiploma, Newtonsoft.Json.Formatting.Indented);

            var retornoGAD = APIDiplomaGAD.Execute<object>("Criar", modeloDiploma);
            var retornoCriarDiploma = JsonConvert.DeserializeObject<RetornoCriarDiplomaVO>(retornoGAD.ToString());

            if (retornoCriarDiploma.SeqDocumentoDiploma == 0)
            {
                throw new Exception(retornoCriarDiploma.ErrorMessage);
            }

            return retornoCriarDiploma;
        }

        private DiplomadoVO PreencherDadosAlunoDiploma(SolicitacaoAnaliseEmissaoDocumentoConclusaoVO dadosEmissao, SolicitacaoAnaliseEmissaoDocumentoConclusaoVO modeloTela)
        {
            var dadosAluno = new DiplomadoVO
            {
                Naturalidade = new NaturalidadeVO(),
                Rg = new RgVO(),
                OutroDocumentoIdentificacao = new OutroDocumentoIdentificacaoVO(),

                Id = dadosEmissao.CodigoAlunoMigracao?.ToString(),
                Nome = FormatarString.Truncate(dadosEmissao.NomeAluno, 255),
                Sexo = dadosEmissao.Sexo == Sexo.Masculino ? "M" : "F"
            };

            if (dadosEmissao.DataNascimento.HasValue)
                dadosAluno.DataNascimento = dadosEmissao.DataNascimento;
            else
                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("data de nascimento");

            if (dadosEmissao.ExibirNomeSocial && !string.IsNullOrEmpty(dadosEmissao.NomeSocial))
                dadosAluno.NomeSocial = FormatarString.Truncate(dadosEmissao.NomeSocial, 255);
            else if (dadosEmissao.ExibirNomeSocial && string.IsNullOrEmpty(dadosEmissao.NomeSocial))
                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("nome social");

            if (!string.IsNullOrEmpty(dadosEmissao.DescricaoNacionalidade))
                dadosAluno.Nacionalidade = FormatarString.Truncate(dadosEmissao.DescricaoNacionalidade, 255);
            else
                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("nacionalidade");

            if (dadosEmissao.CodigoPaisNacionalidade > 0)
            {
                var pais = this.LocalidadeService.BuscarPais((int)dadosEmissao.CodigoPaisNacionalidade);
                if (pais != null)
                {
                    if (pais.Nome.Trim().ToUpper() == NOME_PAIS.BRASIL)
                    {
                        if (!string.IsNullOrEmpty(dadosEmissao.UfNaturalidade))
                        {
                            dadosAluno.Naturalidade.Uf = FormatarString.Truncate(dadosEmissao.UfNaturalidade, 2);

                            if (dadosEmissao.CodigoCidadeNaturalidade > 0)
                            {
                                var cidade = this.LocalidadeService.BuscarCidade(dadosEmissao.CodigoCidadeNaturalidade, dadosEmissao.UfNaturalidade);
                                if (cidade != null)
                                {
                                    dadosAluno.Naturalidade.NomeMunicipio = FormatarString.Truncate(cidade.Nome, 255);

                                    if (cidade.CodigoMunicipioIBGE.HasValue)
                                    {
                                        dadosAluno.Naturalidade.CodigoMunicipio = FormatarString.Truncate(cidade.CodigoMunicipioIBGE.ToString(), 7);
                                    }
                                    else
                                    {
                                        throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("código do município IBGE da naturalidade do aluno");
                                    }
                                }
                                else
                                {
                                    throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("cidade da naturalidade do aluno");
                                }
                            }
                            else
                            {
                                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("código da cidade da naturalidade do aluno");
                            }
                        }
                        else
                        {
                            throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("sigla uf da naturalidade do aluno");
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(dadosEmissao.DescricaoNaturalidadeEstrangeira))
                            throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("nome munícipio estrangeiro");

                        dadosAluno.Naturalidade.NomeMunicipioEstrangeiro = FormatarString.Truncate(dadosEmissao.DescricaoNaturalidadeEstrangeira, 255);
                    }
                }
            }

            if (!string.IsNullOrEmpty(dadosEmissao.Cpf))
            {
                var auxCpf = FormatarString.ObterNumerosComVirgula(dadosEmissao.Cpf);
                dadosAluno.Cpf = FormatarString.Truncate(auxCpf, 11);
            }

            if (modeloTela.TipoIdentidade == TipoIdentidade.RG)
            {
                var auxNumeroRg = FormatarString.ObterNumerosComVirgula(dadosEmissao.NumeroIdentidade);
                if (!string.IsNullOrEmpty(auxNumeroRg))
                    dadosAluno.Rg.Numero = FormatarString.Truncate(auxNumeroRg, 15);
                else
                    throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("número RG");

                if (!string.IsNullOrEmpty(dadosEmissao.OrgaoEmissorIdentidade))
                    dadosAluno.Rg.OrgaoExpedidor = FormatarString.Truncate(dadosEmissao.OrgaoEmissorIdentidade, 255);
                else
                    throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("órgão expedidor");

                if (!string.IsNullOrEmpty(dadosEmissao.UfIdentidade))
                    dadosAluno.Rg.Uf = FormatarString.Truncate(dadosEmissao.UfIdentidade, 2);
                else
                    throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("sigla uf do RG do aluno");
            }
            else if (modeloTela.TipoIdentidade == TipoIdentidade.Passaporte)
            {
                if (string.IsNullOrEmpty(dadosEmissao.NumeroPassaporte) || !dadosEmissao.CodigoPaisEmissaoPassaporte.HasValue)
                    throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("passaporte");

                var pais = this.LocalidadeService.BuscarPais((int)dadosEmissao.CodigoPaisEmissaoPassaporte);
                var identificadorNumeroPassaporte = $"{dadosEmissao.NumeroPassaporte} - {pais?.Nome.Trim()}";

                dadosAluno.OutroDocumentoIdentificacao.TipoDocumento = FormatarString.Truncate(modeloTela.TipoIdentidade.SMCGetDescription(), 255);
                dadosAluno.OutroDocumentoIdentificacao.Identificador = FormatarString.Truncate(identificadorNumeroPassaporte, 255);
            }
            else if (modeloTela.TipoIdentidade == TipoIdentidade.IdentidadeEstrangeira)
            {
                if (string.IsNullOrEmpty(dadosEmissao.NumeroIdentidadeEstrangeira))
                    throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("identidade estrangeira");

                dadosAluno.OutroDocumentoIdentificacao.TipoDocumento = FormatarString.Truncate(modeloTela.TipoIdentidade.SMCGetDescription(), 255);
                dadosAluno.OutroDocumentoIdentificacao.Identificador = FormatarString.Truncate(dadosEmissao.NumeroIdentidadeEstrangeira, 255);
            }

            return dadosAluno;
        }

        #endregion Diploma Gad

        #region Historico Gad

        public RetornoCriarHistoricoVO GerarHistoricoGad(SolicitacaoAnaliseEmissaoDocumentoConclusaoVO dadosEmissao, SolicitacaoAnaliseEmissaoDocumentoConclusaoVO modeloTela, List<SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaVO> formacoes, long seqConfiguracaoHistoricoGAD, string tokenTipoHistorico)
        {
            var usuarioInclusao = UsuarioLogado.RetornaUsuarioLogado();
            var modeloHistorico = new CriarTranscricaoAcademicaVO()
            {
                SeqConfiguracaoHistorico = seqConfiguracaoHistoricoGAD,
                UsuarioInclusao = usuarioInclusao
            };

            var dadosFormacao = formacoes.OrderBy(o => o.Data).FirstOrDefault();
            var dataConclusao = formacoes.Min(a => a.DataConclusao);

            if (tokenTipoHistorico == TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_FINAL)
                modeloHistorico.Type = TIPO_HISTORICO.FINAL;
            else if (tokenTipoHistorico == TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_PARCIAL)
            {
                modeloHistorico.Type = TIPO_HISTORICO.PARTIAL;
                dataConclusao = DateTime.Now;
            }
            else if (tokenTipoHistorico == TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_2VIA)
                modeloHistorico.Type = TIPO_HISTORICO.COPY;

            modeloHistorico.AcademicTranscript.Aluno = PreencherDadosAlunoHistorico(dadosEmissao, modeloTela);

            modeloHistorico.AcademicTranscript.DadosCurso = PreencherDadosCursoHistorico(dadosEmissao, formacoes, dataConclusao, tokenTipoHistorico);

            #region Dados da Instituição Emissora

            if (dadosEmissao.SeqInstituicaoEnsino.HasValue)
            {
                var instituicaoEnsino = InstituicaoEnsinoDomainService.BuscarDadosInstituicaoEnsinoParaDocumentoAcademico(dadosEmissao.SeqInstituicaoEnsino.Value);

                modeloHistorico.AcademicTranscript.IesEmissora.Nome = instituicaoEnsino.Nome;
                modeloHistorico.AcademicTranscript.IesEmissora.CodigoMEC = instituicaoEnsino.CodigoMEC;
                modeloHistorico.AcademicTranscript.IesEmissora.Cnpj = instituicaoEnsino.Cnpj;
                modeloHistorico.AcademicTranscript.IesEmissora.Endereco.Logradouro = instituicaoEnsino.Endereco.Logradouro;

                if (!string.IsNullOrEmpty(instituicaoEnsino.Endereco.Numero))
                {
                    modeloHistorico.AcademicTranscript.IesEmissora.Endereco.Numero = instituicaoEnsino.Endereco.Numero;
                }

                if (!string.IsNullOrEmpty(instituicaoEnsino.Endereco.Complemento))
                {
                    modeloHistorico.AcademicTranscript.IesEmissora.Endereco.Complemento = instituicaoEnsino.Endereco.Complemento;
                }

                modeloHistorico.AcademicTranscript.IesEmissora.Endereco.Bairro = instituicaoEnsino.Endereco.Bairro;
                modeloHistorico.AcademicTranscript.IesEmissora.Endereco.Cep = instituicaoEnsino.Endereco.Cep;
                modeloHistorico.AcademicTranscript.IesEmissora.Endereco.Uf = instituicaoEnsino.Endereco.Uf;
                modeloHistorico.AcademicTranscript.IesEmissora.Endereco.NomeMunicipio = instituicaoEnsino.Endereco.NomeMunicipio;
                modeloHistorico.AcademicTranscript.IesEmissora.Endereco.CodigoMunicipio = instituicaoEnsino.Endereco.CodigoMunicipio;

                var mantenedora = MantenedoraDomainService.BuscarMantenedoraParaDocumentoAcademico(instituicaoEnsino.SeqMantenedora);
                modeloHistorico.AcademicTranscript.IesEmissora.Mantenedora.RazaoSocial = mantenedora.RazaoSocial;
                modeloHistorico.AcademicTranscript.IesEmissora.Mantenedora.Cnpj = mantenedora.Cnpj;
                modeloHistorico.AcademicTranscript.IesEmissora.Mantenedora.Endereco.Logradouro = mantenedora.Endereco.Logradouro;

                if (!string.IsNullOrEmpty(mantenedora.Endereco.Numero))
                {
                    modeloHistorico.AcademicTranscript.IesEmissora.Mantenedora.Endereco.Numero = mantenedora.Endereco.Numero;
                }

                if (!string.IsNullOrEmpty(mantenedora.Endereco.Complemento))
                {
                    modeloHistorico.AcademicTranscript.IesEmissora.Mantenedora.Endereco.Complemento = mantenedora.Endereco.Complemento;
                }

                modeloHistorico.AcademicTranscript.IesEmissora.Mantenedora.Endereco.Bairro = mantenedora.Endereco.Bairro;
                modeloHistorico.AcademicTranscript.IesEmissora.Mantenedora.Endereco.Cep = mantenedora.Endereco.Cep;
                modeloHistorico.AcademicTranscript.IesEmissora.Mantenedora.Endereco.Uf = mantenedora.Endereco.Uf;
                modeloHistorico.AcademicTranscript.IesEmissora.Mantenedora.Endereco.NomeMunicipio = mantenedora.Endereco.NomeMunicipio;
                modeloHistorico.AcademicTranscript.IesEmissora.Mantenedora.Endereco.CodigoMunicipio = mantenedora.Endereco.CodigoMunicipio;

                var atosNormativosInstituicao = AtoNormativoEntidadeDomainService.BuscarAtoNormativoEntidadeInstituicao(dadosEmissao.SeqInstituicaoEnsino.Value, dataConclusao);

                if (atosNormativosInstituicao.Credenciamento != null)
                {
                    modeloHistorico.AcademicTranscript.IesEmissora.Credenciamento.Numero = atosNormativosInstituicao.Credenciamento.Numero;
                    modeloHistorico.AcademicTranscript.IesEmissora.Credenciamento.Data = atosNormativosInstituicao.Credenciamento.Data;
                    modeloHistorico.AcademicTranscript.IesEmissora.Credenciamento.Tipo = atosNormativosInstituicao.Credenciamento.Tipo;

                    if (!string.IsNullOrEmpty(atosNormativosInstituicao.Credenciamento.VeiculoPublicacao))
                    {
                        modeloHistorico.AcademicTranscript.IesEmissora.Credenciamento.VeiculoPublicacao = atosNormativosInstituicao.Credenciamento.VeiculoPublicacao;
                    }

                    if (atosNormativosInstituicao.Credenciamento.DataPublicacao.HasValue)
                    {
                        modeloHistorico.AcademicTranscript.IesEmissora.Credenciamento.DataPublicacao = atosNormativosInstituicao.Credenciamento.DataPublicacao.Value.Date;
                    }

                    if (atosNormativosInstituicao.Credenciamento.SecaoPublicacao.HasValue)
                    {
                        modeloHistorico.AcademicTranscript.IesEmissora.Credenciamento.SecaoPublicacao = atosNormativosInstituicao.Credenciamento.SecaoPublicacao;
                    }

                    if (atosNormativosInstituicao.Credenciamento.PaginaPublicacao.HasValue)
                    {
                        modeloHistorico.AcademicTranscript.IesEmissora.Credenciamento.PaginaPublicacao = atosNormativosInstituicao.Credenciamento.PaginaPublicacao;
                    }

                    if (atosNormativosInstituicao.Credenciamento.NumeroDOU.HasValue)
                    {
                        modeloHistorico.AcademicTranscript.IesEmissora.Credenciamento.NumeroDOU = atosNormativosInstituicao.Credenciamento.NumeroDOU;
                    }
                }
                if (atosNormativosInstituicao.Recredenciamento != null)
                {
                    modeloHistorico.AcademicTranscript.IesEmissora.Recredenciamento.Numero = atosNormativosInstituicao.Recredenciamento.Numero;
                    modeloHistorico.AcademicTranscript.IesEmissora.Recredenciamento.Data = atosNormativosInstituicao.Recredenciamento.Data;
                    modeloHistorico.AcademicTranscript.IesEmissora.Recredenciamento.Tipo = atosNormativosInstituicao.Recredenciamento.Tipo;

                    if (!string.IsNullOrEmpty(atosNormativosInstituicao.Recredenciamento.VeiculoPublicacao))
                    {
                        modeloHistorico.AcademicTranscript.IesEmissora.Recredenciamento.VeiculoPublicacao = atosNormativosInstituicao.Recredenciamento.VeiculoPublicacao;
                    }

                    if (atosNormativosInstituicao.Recredenciamento.DataPublicacao.HasValue)
                    {
                        modeloHistorico.AcademicTranscript.IesEmissora.Recredenciamento.DataPublicacao = atosNormativosInstituicao.Recredenciamento.DataPublicacao.Value.Date;
                    }

                    if (atosNormativosInstituicao.Recredenciamento.SecaoPublicacao.HasValue)
                    {
                        modeloHistorico.AcademicTranscript.IesEmissora.Recredenciamento.SecaoPublicacao = atosNormativosInstituicao.Recredenciamento.SecaoPublicacao;
                    }

                    if (atosNormativosInstituicao.Recredenciamento.PaginaPublicacao.HasValue)
                    {
                        modeloHistorico.AcademicTranscript.IesEmissora.Recredenciamento.PaginaPublicacao = atosNormativosInstituicao.Recredenciamento.PaginaPublicacao;
                    }

                    if (atosNormativosInstituicao.Recredenciamento.NumeroDOU.HasValue)
                    {
                        modeloHistorico.AcademicTranscript.IesEmissora.Recredenciamento.NumeroDOU = atosNormativosInstituicao.Recredenciamento.NumeroDOU;
                    }
                }
                if (atosNormativosInstituicao.RenovacaoDeRecredenciamento != null)
                {
                    modeloHistorico.AcademicTranscript.IesEmissora.RenovacaoDeRecredenciamento.Numero = atosNormativosInstituicao.RenovacaoDeRecredenciamento.Numero;
                    modeloHistorico.AcademicTranscript.IesEmissora.RenovacaoDeRecredenciamento.Data = atosNormativosInstituicao.RenovacaoDeRecredenciamento.Data;
                    modeloHistorico.AcademicTranscript.IesEmissora.RenovacaoDeRecredenciamento.Tipo = atosNormativosInstituicao.RenovacaoDeRecredenciamento.Tipo;

                    if (!string.IsNullOrEmpty(atosNormativosInstituicao.RenovacaoDeRecredenciamento.VeiculoPublicacao))
                    {
                        modeloHistorico.AcademicTranscript.IesEmissora.RenovacaoDeRecredenciamento.VeiculoPublicacao = atosNormativosInstituicao.RenovacaoDeRecredenciamento.VeiculoPublicacao;
                    }

                    if (atosNormativosInstituicao.RenovacaoDeRecredenciamento.DataPublicacao.HasValue)
                    {
                        modeloHistorico.AcademicTranscript.IesEmissora.RenovacaoDeRecredenciamento.DataPublicacao = atosNormativosInstituicao.RenovacaoDeRecredenciamento.DataPublicacao.Value.Date;
                    }

                    if (atosNormativosInstituicao.RenovacaoDeRecredenciamento.SecaoPublicacao.HasValue)
                    {
                        modeloHistorico.AcademicTranscript.IesEmissora.RenovacaoDeRecredenciamento.SecaoPublicacao = atosNormativosInstituicao.RenovacaoDeRecredenciamento.SecaoPublicacao;
                    }

                    if (atosNormativosInstituicao.RenovacaoDeRecredenciamento.PaginaPublicacao.HasValue)
                    {
                        modeloHistorico.AcademicTranscript.IesEmissora.RenovacaoDeRecredenciamento.PaginaPublicacao = atosNormativosInstituicao.RenovacaoDeRecredenciamento.PaginaPublicacao;
                    }

                    if (atosNormativosInstituicao.RenovacaoDeRecredenciamento.NumeroDOU.HasValue)
                    {
                        modeloHistorico.AcademicTranscript.IesEmissora.RenovacaoDeRecredenciamento.NumeroDOU = atosNormativosInstituicao.RenovacaoDeRecredenciamento.NumeroDOU;
                    }
                }
            }
            else
            {
                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("instituição de ensino do aluno");
            }

            #endregion

            modeloHistorico.AcademicTranscript.HistoricoEscolar = PreencherDadosHistoricoEscolarHistorico(dadosEmissao, dadosFormacao, dataConclusao);

            modeloHistorico.AcademicTranscript.InformacoesAdicionais = PreencherInformacoesAdicionaisHistorico(dadosEmissao.SeqSolicitacaoServico);

            //string requisicaoJson = JsonConvert.SerializeObject(modeloHistorico, Newtonsoft.Json.Formatting.Indented);

            var retornoGAD = APIHistoricoGAD.Execute<object>("Criar", modeloHistorico);
            var retornoCriarHistorico = JsonConvert.DeserializeObject<RetornoCriarHistoricoVO>(retornoGAD.ToString());

            if (retornoCriarHistorico.SeqDocumentoHistorico == 0)
                throw new Exception(retornoCriarHistorico.ErrorMessage);

            return retornoCriarHistorico;
        }

        private DiplomadoVO PreencherDadosAlunoHistorico(SolicitacaoAnaliseEmissaoDocumentoConclusaoVO dadosEmissao, SolicitacaoAnaliseEmissaoDocumentoConclusaoVO modeloTela)
        {
            var dadosAluno = new DiplomadoVO
            {
                Naturalidade = new NaturalidadeVO(),
                Rg = new RgVO(),
                OutroDocumentoIdentificacao = new OutroDocumentoIdentificacaoVO(),

                Id = dadosEmissao.CodigoAlunoMigracao?.ToString(),
                Nome = FormatarString.Truncate(dadosEmissao.NomeAluno, 255),
                Sexo = dadosEmissao.Sexo == Sexo.Masculino ? "M" : "F"
            };

            if (dadosEmissao.DataNascimento.HasValue)
                dadosAluno.DataNascimento = dadosEmissao.DataNascimento;
            else
                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("data de nascimento");

            if (dadosEmissao.ExibirNomeSocial && !string.IsNullOrEmpty(dadosEmissao.NomeSocial))
                dadosAluno.NomeSocial = FormatarString.Truncate(dadosEmissao.NomeSocial, 255);
            else if (dadosEmissao.ExibirNomeSocial && string.IsNullOrEmpty(dadosEmissao.NomeSocial))
                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("nome social");


            if (!string.IsNullOrEmpty(dadosEmissao.DescricaoNacionalidade))
                dadosAluno.Nacionalidade = FormatarString.Truncate(dadosEmissao.DescricaoNacionalidade, 255);
            else
                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("nacionalidade");

            if (dadosEmissao.CodigoPaisNacionalidade > 0)
            {
                var pais = this.LocalidadeService.BuscarPais((int)dadosEmissao.CodigoPaisNacionalidade);
                if (pais != null)
                {
                    if (pais.Nome.Trim().ToUpper() == NOME_PAIS.BRASIL)
                    {
                        if (!string.IsNullOrEmpty(dadosEmissao.UfNaturalidade))
                        {
                            dadosAluno.Naturalidade.Uf = FormatarString.Truncate(dadosEmissao.UfNaturalidade, 2);

                            if (dadosEmissao.CodigoCidadeNaturalidade > 0)
                            {
                                var cidade = this.LocalidadeService.BuscarCidade(dadosEmissao.CodigoCidadeNaturalidade, dadosEmissao.UfNaturalidade);
                                if (cidade != null)
                                {
                                    dadosAluno.Naturalidade.NomeMunicipio = FormatarString.Truncate(cidade.Nome, 255);

                                    if (cidade.CodigoMunicipioIBGE.HasValue)
                                    {
                                        dadosAluno.Naturalidade.CodigoMunicipio = FormatarString.Truncate(cidade.CodigoMunicipioIBGE.ToString(), 7);
                                    }
                                    else
                                    {
                                        throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("código do município IBGE da naturalidade do aluno");
                                    }
                                }
                                else
                                {
                                    throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("cidade da naturalidade do aluno");
                                }
                            }
                            else
                            {
                                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("código da cidade da naturalidade do aluno");
                            }
                        }
                        else
                        {
                            throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("sigla uf da naturalidade do aluno");
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(dadosEmissao.DescricaoNaturalidadeEstrangeira))
                            throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("nome munícipio estrangeiro");

                        dadosAluno.Naturalidade.NomeMunicipioEstrangeiro = FormatarString.Truncate(dadosEmissao.DescricaoNaturalidadeEstrangeira, 255);
                    }
                }
            }

            if (!string.IsNullOrEmpty(dadosEmissao.Cpf))
            {
                var auxCpf = FormatarString.ObterNumerosComVirgula(dadosEmissao.Cpf);
                dadosAluno.Cpf = FormatarString.Truncate(auxCpf, 11);
            }

            if (modeloTela.TipoIdentidade == TipoIdentidade.RG)
            {
                var auxNumeroRg = FormatarString.ObterNumerosComVirgula(dadosEmissao.NumeroIdentidade);
                if (!string.IsNullOrEmpty(auxNumeroRg))
                    dadosAluno.Rg.Numero = FormatarString.Truncate(auxNumeroRg, 15);
                else
                    throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("número RG");

                if (!string.IsNullOrEmpty(dadosEmissao.OrgaoEmissorIdentidade))
                    dadosAluno.Rg.OrgaoExpedidor = FormatarString.Truncate(dadosEmissao.OrgaoEmissorIdentidade, 255);
                else
                    throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("órgão expedidor");

                if (!string.IsNullOrEmpty(dadosEmissao.UfIdentidade))
                    dadosAluno.Rg.Uf = FormatarString.Truncate(dadosEmissao.UfIdentidade, 2);
                else
                    throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("sigla uf do RG do aluno");

                dadosAluno.OutroDocumentoIdentificacao = null;
            }
            else if (modeloTela.TipoIdentidade == TipoIdentidade.Passaporte)
            {
                if (string.IsNullOrEmpty(dadosEmissao.NumeroPassaporte) || !dadosEmissao.CodigoPaisEmissaoPassaporte.HasValue)
                    throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("passaporte");

                var pais = this.LocalidadeService.BuscarPais((int)dadosEmissao.CodigoPaisEmissaoPassaporte);
                var identificadorNumeroPassaporte = $"{dadosEmissao.NumeroPassaporte} - {pais?.Nome.Trim()}";

                dadosAluno.OutroDocumentoIdentificacao.TipoDocumento = FormatarString.Truncate(modeloTela.TipoIdentidade.SMCGetDescription(), 255);
                dadosAluno.OutroDocumentoIdentificacao.Identificador = FormatarString.Truncate(identificadorNumeroPassaporte, 255);
                dadosAluno.Rg = null;
            }
            else if (modeloTela.TipoIdentidade == TipoIdentidade.IdentidadeEstrangeira)
            {
                if (string.IsNullOrEmpty(dadosEmissao.NumeroIdentidadeEstrangeira))
                    throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("identidade estrangeira");

                dadosAluno.OutroDocumentoIdentificacao.TipoDocumento = FormatarString.Truncate(modeloTela.TipoIdentidade.SMCGetDescription(), 255);
                dadosAluno.OutroDocumentoIdentificacao.Identificador = FormatarString.Truncate(dadosEmissao.NumeroIdentidadeEstrangeira, 255);
                dadosAluno.Rg = null;
            }

            return dadosAluno;
        }

        private DadosMinimosCursoVO PreencherDadosCursoHistorico(SolicitacaoAnaliseEmissaoDocumentoConclusaoVO dadosEmissao, List<SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaVO> formacoes, DateTime? dataConclusao, string tokenTipoHistorico)
        {
            var dadosCurso = new DadosMinimosCursoVO
            {
                InformacoesTramitacaoEmec = new InformacoesTramitacaoEmecVO(),
                NomesHabilitacao = new List<string>(),
                Habilitacoes = new List<HabilitacaoVO>(),
                Autorizacao = new AtoRegulatorioVO()
                {
                    InformacoesTramitacaoEmec = new InformacoesTramitacaoEmecVO()
                },
                Reconhecimento = new AtoRegulatorioVO()
                {
                    InformacoesTramitacaoEmec = new InformacoesTramitacaoEmecVO()
                },
                RenovacaoReconhecimento = new AtoRegulatorioVO()
                {
                    InformacoesTramitacaoEmec = new InformacoesTramitacaoEmecVO()
                },
                NomeCurso = FormatarString.Truncate(dadosEmissao.DescricaoCursoDocumento, 255)
            };

            foreach (var formacao in formacoes)
            {
                var formacaoEspecifica = FormacaoEspecificaDomainService.SearchByKey(new SMCSeqSpecification<FormacaoEspecifica>(formacao.SeqFormacaoEspecifica));
                var nomeHabilitacao = string.Empty;

                if (formacao.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.APROFUNDAMENTO ||
                    formacao.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.ENFASE ||
                    formacao.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.HABILITACAO ||
                    formacao.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.LINHA_FORMACAO)
                {
                    var auxNomeHabilitacao = $"{formacao.DescricaoTipoFormacaoEspecifica.Trim()} em {formacaoEspecifica.Descricao.Trim()}";
                    auxNomeHabilitacao = auxNomeHabilitacao.Trim();
                    nomeHabilitacao = FormatarString.Truncate(auxNomeHabilitacao, 255);
                }
                else if (formacao.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.GRAU)
                {
                    var auxNomeHabilitacao = $"{formacaoEspecifica.Descricao.Trim()} em {formacao.DescricaoDocumentoConclusao.Trim()}";
                    auxNomeHabilitacao = auxNomeHabilitacao.Trim();
                    nomeHabilitacao = FormatarString.Truncate(auxNomeHabilitacao, 255);
                }
                else
                {
                    var auxNomeHabilitacao = formacaoEspecifica.Descricao.Trim();
                    nomeHabilitacao = FormatarString.Truncate(auxNomeHabilitacao, 255);
                }

                var habilitacao = new HabilitacaoVO() { DataHabilitacao = formacao.DataConclusao };
                habilitacao.NomeHabilitacao = nomeHabilitacao;
                dadosCurso.Habilitacoes.Add(habilitacao);
            }

            var cursoOfertaLocalidade = CursoOfertaLocalidadeDomainService.BuscarIdentificacaoEmecComEnderecoParaDocumentoAcademico(dadosEmissao.SeqCursoOfertaLocalidade);

            dadosCurso.CodigoCursoEMEC = cursoOfertaLocalidade.CodigoOrgaoRegulador.Value;

            if (tokenTipoHistorico == TOKEN_TIPO_HISTORICO_ESCOLAR.HISTORICO_ESCOLAR_PARCIAL)
                dataConclusao = DateTime.Now;

            var atosNormativosCurso = this.BuscarAtosNormativosCurso(dadosEmissao.SeqCursoOfertaLocalidade, dadosEmissao.SeqGrauAcademicoSelecionado, dataConclusao, true);

            if (atosNormativosCurso.Autorizacao != null)
            {
                dadosCurso.Autorizacao.Numero = atosNormativosCurso.Autorizacao.Numero;
                dadosCurso.Autorizacao.Data = atosNormativosCurso.Autorizacao.Data;
                dadosCurso.Autorizacao.Tipo = atosNormativosCurso.Autorizacao.Tipo;

                if (!string.IsNullOrEmpty(atosNormativosCurso.Autorizacao.VeiculoPublicacao))
                {
                    dadosCurso.Autorizacao.VeiculoPublicacao = atosNormativosCurso.Autorizacao.VeiculoPublicacao;
                }

                if (atosNormativosCurso.Autorizacao.DataPublicacao.HasValue)
                {
                    dadosCurso.Autorizacao.DataPublicacao = atosNormativosCurso.Autorizacao.DataPublicacao.Value.Date;
                }

                if (atosNormativosCurso.Autorizacao.SecaoPublicacao.HasValue)
                {
                    dadosCurso.Autorizacao.SecaoPublicacao = atosNormativosCurso.Autorizacao.SecaoPublicacao;
                }

                if (atosNormativosCurso.Autorizacao.PaginaPublicacao.HasValue)
                {
                    dadosCurso.Autorizacao.PaginaPublicacao = atosNormativosCurso.Autorizacao.PaginaPublicacao;
                }

                if (atosNormativosCurso.Autorizacao.NumeroDOU.HasValue)
                {
                    dadosCurso.Autorizacao.NumeroDOU = atosNormativosCurso.Autorizacao.NumeroDOU;
                }
            }

            if (atosNormativosCurso.Reconhecimento != null)
            {
                dadosCurso.Reconhecimento.Numero = atosNormativosCurso.Reconhecimento.Numero;
                dadosCurso.Reconhecimento.Data = atosNormativosCurso.Reconhecimento.Data;
                dadosCurso.Reconhecimento.Tipo = atosNormativosCurso.Reconhecimento.Tipo;

                if (!string.IsNullOrEmpty(atosNormativosCurso.Reconhecimento.VeiculoPublicacao))
                {
                    dadosCurso.Reconhecimento.VeiculoPublicacao = atosNormativosCurso.Reconhecimento.VeiculoPublicacao;
                }

                if (atosNormativosCurso.Reconhecimento.DataPublicacao.HasValue)
                {
                    dadosCurso.Reconhecimento.DataPublicacao = atosNormativosCurso.Reconhecimento.DataPublicacao.Value.Date;
                }

                if (atosNormativosCurso.Reconhecimento.SecaoPublicacao.HasValue)
                {
                    dadosCurso.Reconhecimento.SecaoPublicacao = atosNormativosCurso.Reconhecimento.SecaoPublicacao;
                }

                if (atosNormativosCurso.Reconhecimento.PaginaPublicacao.HasValue)
                {
                    dadosCurso.Reconhecimento.PaginaPublicacao = atosNormativosCurso.Reconhecimento.PaginaPublicacao;
                }

                if (atosNormativosCurso.Reconhecimento.NumeroDOU.HasValue)
                {
                    dadosCurso.Reconhecimento.NumeroDOU = atosNormativosCurso.Reconhecimento.NumeroDOU;
                }
            }

            if (atosNormativosCurso.RenovacaoReconhecimento != null)
            {
                dadosCurso.RenovacaoReconhecimento.Numero = atosNormativosCurso.RenovacaoReconhecimento.Numero;
                dadosCurso.RenovacaoReconhecimento.Data = atosNormativosCurso.RenovacaoReconhecimento.Data;
                dadosCurso.RenovacaoReconhecimento.Tipo = atosNormativosCurso.RenovacaoReconhecimento.Tipo;

                if (!string.IsNullOrEmpty(atosNormativosCurso.RenovacaoReconhecimento.VeiculoPublicacao))
                {
                    dadosCurso.RenovacaoReconhecimento.VeiculoPublicacao = atosNormativosCurso.RenovacaoReconhecimento.VeiculoPublicacao;
                }

                if (atosNormativosCurso.RenovacaoReconhecimento.DataPublicacao.HasValue)
                {
                    dadosCurso.RenovacaoReconhecimento.DataPublicacao = atosNormativosCurso.RenovacaoReconhecimento.DataPublicacao.Value.Date;
                }

                if (atosNormativosCurso.RenovacaoReconhecimento.SecaoPublicacao.HasValue)
                {
                    dadosCurso.RenovacaoReconhecimento.SecaoPublicacao = atosNormativosCurso.RenovacaoReconhecimento.SecaoPublicacao;
                }

                if (atosNormativosCurso.RenovacaoReconhecimento.PaginaPublicacao.HasValue)
                {
                    dadosCurso.RenovacaoReconhecimento.PaginaPublicacao = atosNormativosCurso.RenovacaoReconhecimento.PaginaPublicacao;
                }

                if (atosNormativosCurso.RenovacaoReconhecimento.NumeroDOU.HasValue)
                {
                    dadosCurso.RenovacaoReconhecimento.NumeroDOU = atosNormativosCurso.RenovacaoReconhecimento.NumeroDOU;
                }
            }

            return dadosCurso;
        }

        private HistoricoVO PreencherDadosHistoricoEscolarHistorico(SolicitacaoAnaliseEmissaoDocumentoConclusaoVO dadosEmissao, SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaVO dadosFormacao, DateTime? dataConclusao)
        {
            var dadosHistoricoEscolar = new HistoricoVO
            {
                CargaHorariaCursoIntegralizadaV2 = new CargaHorariaVO(),
                CargaHorariaCurso = new CargaHorariaVO(),
                SituacaoAtualDiscente = new SituacaoDiscenteVO()
                {
                    Intercambio = new SituacaoIntercambioVO(),
                    Formado = new SituacaoFormadoVO()
                },
                SituacaoEnade = new SituacaoAlunoEnadeVO(),
                ParticipacoesEnade = new List<ParticipacaoEnadeVO>()
                    {
                        new ParticipacaoEnadeVO()
                        {
                            Informacoes = new InformacaoEnadeVO(),
                            NaoHabilitado = new EnadeNaoHabilitadoVO()
                        }
                    },
                ElementosHistorico = new List<ElementoHistoricoVO>()
                    {
                        new ElementoHistoricoVO()
                        {
                            Disciplina = new DisciplinaV2VO()
                            {
                                Aprovacao = new AprovacaoDisciplinaVO(),
                                CargaHorariaComEtiqueta = new List<CargaHorariaComEtiquetaVO>(),
                                CargaHorariaV2 = new CargaHorariaVO(),
                                Docentes = new List<DocenteVO>()
                            },
                            AtividadeComplementar = new AtividadeComplementarVO()
                            {
                                CargaHorariaEmHoraRelogioComEtiqueta = new List<HoraRelogioComEtiquetaVO>(),
                                Docentes = new List<DocenteVO>()
                            },
                            Estagio = new EstagioVO()
                            {
                                Concedente = new ConcedenteEstagioVO(),
                                Docentes = new List<DocenteVO>(),
                                CargaHorariaEmHoraRelogioComEtiqueta = new List<HoraRelogioComEtiquetaVO>()
                            },
                            SituacaoDiscente = new SituacaoDiscenteVO()
                            {
                                Intercambio = new SituacaoIntercambioVO(),
                                Formado = new SituacaoFormadoVO()
                            }
                        }
                    },
                Ingresso = new IngressoVO()
                {
                    FormasAcesso = new List<string>()
                },

                MatrizCurricular = null,
                DataEmissaoHistorico = DateTime.Now,
                HoraEmissaoHistorico = DateTime.Now.ToLongTimeString()
            };

            dadosHistoricoEscolar.Ingresso.Data = dadosEmissao.DataAdmissao.Date;

            if (!string.IsNullOrEmpty(dadosEmissao.DescricaoXSDFormaIngresso))
            {
                var descricaoFormaIngresso = FormatarString.Truncate(dadosEmissao.DescricaoXSDFormaIngresso, 255);
                dadosHistoricoEscolar.Ingresso.FormasAcesso.Add(descricaoFormaIngresso);
            }
            else
            {
                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("descrição XSD da forma de ingresso do aluno");
            }

            if (!dadosEmissao.CodigoAlunoMigracao.HasValue)
                throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("código de aluno migração");

            //A procedure por enquanto irá retornar sempre uma linha de situação enade por aluno
            var dadosSituacaoEnade = IntegracaoAcademicoService.BuscarSituacaoEnade(dadosEmissao.CodigoAlunoMigracao.Value, dadosEmissao.CodigoCursoOfertaLocalidade);

            if (dadosEmissao.NumeroVia == 1 && !(dadosSituacaoEnade != null && dadosSituacaoEnade.Any()))
                throw new SolicitacaoDocumentoConclusaoDadosEnadeNaoEncontradoException();

            if (dadosEmissao.NumeroVia == 1 && string.IsNullOrEmpty(dadosSituacaoEnade.First()?.Categoria))
                throw new SolicitacaoDocumentoConclusaoDadosEnadeNaoEncontradoException();

            var listaParticipacoesEnade = new List<ParticipacaoEnadeVO>();
            if (dadosSituacaoEnade != null && dadosSituacaoEnade.Any())
                foreach (var situacaoEnade in dadosSituacaoEnade)
                {
                    if (situacaoEnade != null)
                    {
                        if (situacaoEnade.Categoria.ToLower().Trim() == CATEGORIA_SITUACAO_ENADE.HABILITADO.ToLower().Trim())
                        {
                            var participacaoEnade = new ParticipacaoEnadeVO()
                            {
                                Situacao = "Habilitado",
                                Informacoes = new InformacaoEnadeVO()
                                {
                                    Condicao = situacaoEnade.Condicao?.Trim(),
                                    Edicao = situacaoEnade.AnoEdicao?.ToString().Trim()
                                }
                            };

                            listaParticipacoesEnade.Add(participacaoEnade);
                        }
                        else if (situacaoEnade.Categoria.ToLower().Trim() == CATEGORIA_SITUACAO_ENADE.NAO_HABILITADO.ToLower().Trim())
                        {
                            var participacaoEnade = new ParticipacaoEnadeVO()
                            {
                                Situacao = "NaoHabilitado",
                                NaoHabilitado = new EnadeNaoHabilitadoVO()
                                {
                                    Condicao = situacaoEnade.Condicao?.Trim(),
                                    Edicao = situacaoEnade.AnoEdicao?.ToString().Trim(),
                                    OutroMotivo = situacaoEnade.SituacaoEnadeXsd?.Trim()
                                }
                            };

                            listaParticipacoesEnade.Add(participacaoEnade);
                        }
                    }
                }

            if (listaParticipacoesEnade.Any())
            {
                dadosHistoricoEscolar.ParticipacoesEnade = listaParticipacoesEnade;
            }
            else
            {
                dadosHistoricoEscolar.ParticipacoesEnade = null;
            }

            var dadosElementoHistorico = RetornarDadosElementoHistorico(dadosEmissao.CodigoAlunoMigracao.Value, dadosEmissao.NumeroVia, codigoCursoOfertaLocalidade: dadosEmissao.CodigoCursoOfertaLocalidade);

            dadosHistoricoEscolar.CodigoCurriculo = dadosElementoHistorico.CodigoCurriculo;
            dadosHistoricoEscolar.ElementosHistorico = dadosElementoHistorico.ElementosHistorico;
            dadosHistoricoEscolar.CargaHorariaCurso.Tipo = TIPO_CARGA_HORARIA_LACUNA.HORA_RELOGIO;
            dadosHistoricoEscolar.CargaHorariaCurso.HoraRelogio = dadosElementoHistorico.CargaHorariaCurso;
            dadosHistoricoEscolar.CargaHorariaCursoIntegralizadaV2.Tipo = TIPO_CARGA_HORARIA_LACUNA.HORA_RELOGIO;
            dadosHistoricoEscolar.CargaHorariaCursoIntegralizadaV2.HoraRelogio = dadosElementoHistorico.CargaHorariaCursoIntegralizada;

            if (dadosEmissao.DescricaoXSDSituacaoAtualMatricula == DESCRICAO_XSD_MATRICULA.Formado)
            {
                dadosHistoricoEscolar.SituacaoAtualDiscente.Tipo = dadosEmissao.DescricaoXSDSituacaoAtualMatricula;
                dadosHistoricoEscolar.SituacaoAtualDiscente.Formado.DataExpedicaoDiploma = dadosEmissao.DataExpedicaoDiploma.Date;

                if (dataConclusao.HasValue)
                    dadosHistoricoEscolar.SituacaoAtualDiscente.Formado.DataConclusaoCurso = dataConclusao.Value.Date;

                if (dadosFormacao != null && dadosFormacao.DataColacaoGrau.HasValue)
                    dadosHistoricoEscolar.SituacaoAtualDiscente.Formado.DataColacaoGrau = dadosFormacao.DataColacaoGrau.Value.Date;
            }
            else if (dadosEmissao.DescricaoXSDSituacaoAtualMatricula == DESCRICAO_XSD_MATRICULA.Trancamento ||
                     dadosEmissao.DescricaoXSDSituacaoAtualMatricula == DESCRICAO_XSD_MATRICULA.MatriculadoEmDisciplina ||
                     dadosEmissao.DescricaoXSDSituacaoAtualMatricula == DESCRICAO_XSD_MATRICULA.Licenca ||
                     dadosEmissao.DescricaoXSDSituacaoAtualMatricula == DESCRICAO_XSD_MATRICULA.Desistencia ||
                     dadosEmissao.DescricaoXSDSituacaoAtualMatricula == DESCRICAO_XSD_MATRICULA.Abandono ||
                     dadosEmissao.DescricaoXSDSituacaoAtualMatricula == DESCRICAO_XSD_MATRICULA.Jubilado)
            {
                dadosHistoricoEscolar.SituacaoAtualDiscente.Tipo = dadosEmissao.DescricaoXSDSituacaoAtualMatricula;
            }
            else if (dadosEmissao.DescricaoXSDSituacaoAtualMatricula == DESCRICAO_XSD_MATRICULA.OutraSituacao)
            {
                dadosHistoricoEscolar.SituacaoAtualDiscente.Tipo = dadosEmissao.DescricaoXSDSituacaoAtualMatricula;
                dadosHistoricoEscolar.SituacaoAtualDiscente.OutraSituacao = dadosEmissao.DescricaoSituacaoAtualMatricula;
            }

            var specSolicitacaoDocumentoConclusao = new SMCSeqSpecification<SolicitacaoDocumentoConclusao>(dadosEmissao.SeqSolicitacaoServico);
            var solicitacaoDocumentoConclusao = this.SearchByKey(specSolicitacaoDocumentoConclusao);
            solicitacaoDocumentoConclusao.CargaHorariaCurso = (decimal?)dadosElementoHistorico.CargaHorariaCurso;
            solicitacaoDocumentoConclusao.CargaHorariaIntegralizada = (decimal?)dadosElementoHistorico.CargaHorariaCursoIntegralizada;
            solicitacaoDocumentoConclusao.DataEmissaoHistorico = DateTime.Now;

            var dadoSituacaoEnade = dadosSituacaoEnade?.OrderBy(o => o.AnoEdicao).FirstOrDefault();
            if (dadoSituacaoEnade != null)
            {
                var situacaoEnadeAuxiliar = string.Empty;

                if (dadoSituacaoEnade.Categoria.ToLower().Trim() == CATEGORIA_SITUACAO_ENADE.HABILITADO.ToLower().Trim())
                    situacaoEnadeAuxiliar = $"{CATEGORIA_SITUACAO_ENADE.HABILITADO} - {dadoSituacaoEnade.Condicao?.Trim()} - {dadoSituacaoEnade.AnoEdicao?.ToString().Trim()} - {dadoSituacaoEnade.SituacaoEnadeXsd?.Trim()}";
                else if (dadoSituacaoEnade.Categoria.ToLower().Trim() == CATEGORIA_SITUACAO_ENADE.NAO_HABILITADO.ToLower().Trim())
                    situacaoEnadeAuxiliar = $"{CATEGORIA_SITUACAO_ENADE.NAO_HABILITADO} - {dadoSituacaoEnade.SituacaoEnadeXsd?.Trim()}";
                solicitacaoDocumentoConclusao.SituacaoEnade = situacaoEnadeAuxiliar;
            }
            this.SaveEntity(solicitacaoDocumentoConclusao);

            return dadosHistoricoEscolar;
        }

        private string PreencherInformacoesAdicionaisHistorico(long seqSolicitacaoServico)
        {
            var mensagens = string.Empty;
            var keySessionMensagemEmissao = string.Format(KEY_SESSION_MENSAGEM.KEY_SESSION_MENSAGEM_EMISSAO, seqSolicitacaoServico);
            var listaMensagensSession = HttpContext.Current.Session[keySessionMensagemEmissao] as List<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO>;

            if (listaMensagensSession != null && listaMensagensSession.Any())
            {
                var mensagemTelaHistorico = listaMensagensSession.Where(w => w.DocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR).ToList();
                if (mensagemTelaHistorico != null && mensagemTelaHistorico.Any())
                {
                    var listaInformacoesAdicionais = new List<string>();
                    foreach (var mensagemTela in mensagemTelaHistorico)
                    {
                        var formatado = HttpUtility.HtmlDecode(mensagemTela.DescricaoDecode);
                        listaInformacoesAdicionais.Add(formatado);
                    }
                    mensagens = string.Join(" | ", listaInformacoesAdicionais);
                }
            }

            return mensagens;
        }

        #endregion Historico Gad

        public DadosElementoHistoricoVO RetornarDadosElementoHistorico(long codigoAlunoMigracao, long? numeroVia, bool validarDados = true, int? codigoCursoOfertaLocalidade = null)
        {
            var retorno = new DadosElementoHistoricoVO { ElementosHistorico = new List<ElementoHistoricoVO>() };

            var listaDisciplinas = new List<ElementoHistoricoVO>();
            var listaAtividadesComplementares = new List<ElementoHistoricoVO>();
            var listaEstagios = new List<ElementoHistoricoVO>();

            #region Disciplinas

            double cargaHorariaCursoHoraRelogio = 0;
            double cargaHorariaCursoIntegralizadaHoraRelogio = 0;

            List<HistoricoEscolarData> listaRetornoHistoricoEscolar = IntegracaoAcademicoService.BuscarHistoricoEscolarAluno(codigoAlunoMigracao);

            if (!(listaRetornoHistoricoEscolar != null && listaRetornoHistoricoEscolar.Any()))
                throw new SolicitacaoDocumentoConclusaoHistoricoEscolarNaoEncontradoException();

            var codigoCurriculo = listaRetornoHistoricoEscolar.FirstOrDefault().CodigoCurriculo;
            var auxCargaHorariaCurso = listaRetornoHistoricoEscolar.FirstOrDefault().CargaHorariaCurriculoHR;

            if (!(auxCargaHorariaCurso > 0))
            {
                if (validarDados)
                {
                    throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("carga horária do curso");
                }
            }
            else
            {
                //Transformando a carga horária do curso de hora-aula para hora-relógio
                //[Hora-Relógio]* = (carga horária hora-aula * 50) / 60
                //A procedure retorna a carga horária no formato hora-aula
                //cargaHorariaCursoHoraRelogio = (cargaHorariaCursoHoraAula * 50.0) / 60.0;
                cargaHorariaCursoHoraRelogio = Convert.ToDouble(auxCargaHorariaCurso);
                //cargaHorariaCursoHoraRelogio = Math.Round(cargaHorariaCursoHoraRelogio, 2);
            }

            var auxCargaHorariaCursoIntegralizada = listaRetornoHistoricoEscolar.FirstOrDefault().CargaHorariaIntegralizada;
            if (auxCargaHorariaCursoIntegralizada > 0)
            {
                cargaHorariaCursoIntegralizadaHoraRelogio = Convert.ToDouble(auxCargaHorariaCursoIntegralizada);
                //cargaHorariaCursoIntegralizadaHoraRelogio = Math.Round(cargaHorariaCursoIntegralizadaHoraRelogio, 2);
            }

            foreach (var historico in listaRetornoHistoricoEscolar)
            {
                if (validarDados)
                {
                    if (string.IsNullOrEmpty(historico.NomeDisciplina))
                        throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("descrição da disciplina");

                    if (!(historico.Ano > 0 && historico.Semestre > 0))
                        throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("período que o aluno cursou a disciplina");

                    if (!(historico.CargaHorariaDisciplinaHR >= 0))
                        throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("carga horária da disciplina");

                    if (string.IsNullOrEmpty(historico.Nota))
                        throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("nota do aluno");

                    if (numeroVia.GetValueOrDefault() == 1)
                    {
                        if (string.IsNullOrEmpty(historico.NomeProfessorXSD))
                            throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("nome do docente");

                        if (string.IsNullOrEmpty(historico.DescricaoTitulacaoXSD))
                            throw new SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException("descrição XSD da titulação do docente");
                    }
                }

                var auxPeriodo = $"{historico.Ano}/{historico.Semestre}";

                //var cargaHorariaDisciplinaHoraAula = historico.CargaHorariaDisciplina;
                //double cargaHorariaDisciplinaHoraRelogio = 0;
                ////Transformando a carga horária da disciplina de hora-aula para hora-relógio
                ////[Hora-Relógio]* = (carga horária hora-aula * 50) / 60
                ////A procedure retorna a carga horária no formato hora-aula
                //cargaHorariaDisciplinaHoraRelogio = (cargaHorariaDisciplinaHoraAula * 50.0) / 60.0;
                //cargaHorariaDisciplinaHoraRelogio = Math.Round(cargaHorariaDisciplinaHoraRelogio, 2);

                string descricaoTitulacaoXSD = null;

                if (!string.IsNullOrEmpty(historico.DescricaoTitulacaoXSD))
                {
                    var normalizedString = historico.DescricaoTitulacaoXSD.Normalize(NormalizationForm.FormD);
                    var stringBuilder = new StringBuilder();

                    foreach (var c in normalizedString)
                    {
                        var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                        if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                            stringBuilder.Append(c);
                    }

                    descricaoTitulacaoXSD = stringBuilder.ToString().Normalize(NormalizationForm.FormC);
                }

                if (!listaDisciplinas.Any(a => a.Disciplina.CodigoDisciplina == historico.CodigoDisciplina && a.Disciplina.Disciplina == historico.NomeDisciplina))
                {
                    var elementoDisciplina = new ElementoHistoricoVO()
                    {
                        Tipo = "Disciplina",
                        Disciplina = new DisciplinaV2VO
                        {
                            CodigoDisciplina = historico.CodigoDisciplina,
                            Disciplina = historico.NomeDisciplina?.Length > 255 ? historico.NomeDisciplina?.Substring(0, 255) : historico.NomeDisciplina,
                            Periodo = auxPeriodo.Length > 255 ? auxPeriodo.Substring(0, 255) : auxPeriodo,
                            CargaHorariaComEtiqueta = new List<CargaHorariaComEtiquetaVO>()
                            {
                                new CargaHorariaComEtiquetaVO()
                                {
                                    Tipo =TIPO_CARGA_HORARIA_LACUNA.HORA_RELOGIO,
                                    Etiqueta = historico.TipoEtiquetaDisciplina,
                                    HoraRelogio =Convert.ToDouble(historico.CargaHorariaDisciplinaHR)
                                }
                            },
                            Situacao = "Aprovado",
                            Aprovacao = new AprovacaoDisciplinaVO()
                        }
                    };

                    if (FORMA_INTEGRALIZACAO.APROVEITADO.Equals(historico.FormaIntegralizacao, StringComparison.OrdinalIgnoreCase) || FORMA_INTEGRALIZACAO.CURSADO.Equals(historico.FormaIntegralizacao, StringComparison.OrdinalIgnoreCase))
                    {
                        elementoDisciplina.Disciplina.Aprovacao.FormaIntegralizacao = historico.FormaIntegralizacao;
                    }
                    else
                    {
                        elementoDisciplina.Disciplina.Aprovacao.OutraFormaIntegralizacao = historico.FormaIntegralizacao;
                    }

                    if (!string.IsNullOrEmpty(historico.NomeProfessorXSD) || !string.IsNullOrEmpty(descricaoTitulacaoXSD))
                    {
                        elementoDisciplina.Disciplina.Docentes = new List<DocenteVO>() { new DocenteVO() { Nome = historico.NomeProfessorXSD, Titulacao = descricaoTitulacaoXSD } };
                    }

                    if (!string.IsNullOrEmpty(historico.Nota))
                    {
                        double auxNota;
                        var converteuNota = double.TryParse(historico.Nota, out auxNota);

                        if (converteuNota)
                        {
                            elementoDisciplina.Disciplina.TipoAvaliacao = TIPO_AVALIACAO_LACUNA.NOTA_ATE_CEM;
                            elementoDisciplina.Disciplina.NotaAteCem = auxNota;
                        }
                        else
                        {
                            // TSK 55783
                            // Regra fala que: "Se a nota aprovação for do tipo caractere (ex: A) enviar como conceito."
                            // Os conceitos permitidos são A, A+, A-... até a letra F. Será verificado com uma expressão regular
                            Regex conceito = new Regex(@"^[ABCDEF][\+-]?$");
                            if (conceito.IsMatch(historico.Nota.Trim()))
                            {
                                elementoDisciplina.Disciplina.TipoAvaliacao = TIPO_AVALIACAO_LACUNA.CONCEITO;
                                elementoDisciplina.Disciplina.Conceito = historico.Nota.Trim();
                            }
                            else
                            {
                                elementoDisciplina.Disciplina.TipoAvaliacao = TIPO_AVALIACAO_LACUNA.CONCEITO_ESPECIFICO_CURSO;
                                elementoDisciplina.Disciplina.ConceitoEspecificoDoCurso = historico.Nota.Trim();
                            }
                        }
                    }

                    listaDisciplinas.Add(elementoDisciplina);
                }
                else
                {
                    var docente = new DocenteVO() { Nome = historico.NomeProfessorXSD, Titulacao = descricaoTitulacaoXSD };
                    var cargaHorariaComEtiqueta = new CargaHorariaComEtiquetaVO()
                    {
                        Tipo = TIPO_CARGA_HORARIA_LACUNA.HORA_RELOGIO,
                        Etiqueta = historico.TipoEtiquetaDisciplina,
                        HoraRelogio = Convert.ToDouble(historico.CargaHorariaDisciplinaHR)
                    };

                    listaDisciplinas.SMCForEach(f =>
                    {
                        if (f.Disciplina.CodigoDisciplina == historico.CodigoDisciplina && f.Disciplina.Disciplina == historico.NomeDisciplina)
                        {
                            if (historico.NomeProfessorXSD != null && !f.Disciplina.Docentes.Any(a => a.Nome == historico.NomeProfessorXSD))
                                f.Disciplina.Docentes.Add(docente);

                            if (historico.TipoEtiquetaDisciplina != null && !f.Disciplina.CargaHorariaComEtiqueta.Any(a => a.Etiqueta == historico.TipoEtiquetaDisciplina))
                                f.Disciplina.CargaHorariaComEtiqueta.Add(cargaHorariaComEtiqueta);
                        }
                    });
                }
            }

            #endregion

            #region Atividades complementares

            List<AtividadeComplementarData> listaRetornoAtividadesComplementares = IntegracaoAcademicoService.BuscarAtividadesComplementaresAluno(codigoAlunoMigracao, codigoCursoOfertaLocalidade);

            if (listaRetornoAtividadesComplementares != null && listaRetornoAtividadesComplementares.Any())
            {
                var listaDocentes = new List<DocenteVO>();

                foreach (var itemAtividadeComplementar in listaRetornoAtividadesComplementares)
                {
                    if (!string.IsNullOrEmpty(itemAtividadeComplementar.Docente))
                    {
                        listaDocentes.Add(new DocenteVO()
                        {
                            Nome = itemAtividadeComplementar.Docente,
                            Titulacao = itemAtividadeComplementar.Titulacao
                        });
                    }
                }

                var primeiroRegistroAtividadesComplementares = listaRetornoAtividadesComplementares.First();

                double cargaHorariaAtividadeHoraRelogio = 0;

                if (!string.IsNullOrEmpty(primeiroRegistroAtividadesComplementares.CargaHorariaTotal))
                {
                    var auxCargaHorariaAtividadeHoraRelogio = primeiroRegistroAtividadesComplementares.CargaHorariaTotal;
                    auxCargaHorariaAtividadeHoraRelogio = string.Format("{0:n2}", auxCargaHorariaAtividadeHoraRelogio.Replace(':', ','));
                    cargaHorariaAtividadeHoraRelogio = Convert.ToDouble(auxCargaHorariaAtividadeHoraRelogio);
                }

                var elementoAtividadeComplementar = new ElementoHistoricoVO()
                {
                    Tipo = "AtividadeComplementar",
                    AtividadeComplementar = new AtividadeComplementarVO
                    {
                        DataRegistro = null,
                        Codigo = primeiroRegistroAtividadesComplementares.Codigo,
                        Tipo = primeiroRegistroAtividadesComplementares.Tipo,
                        DataInicio = primeiroRegistroAtividadesComplementares.DataInicio.HasValue ? primeiroRegistroAtividadesComplementares.DataInicio.Value.Date : (DateTime?)null,
                        DataFim = primeiroRegistroAtividadesComplementares.DataFim.HasValue ? primeiroRegistroAtividadesComplementares.DataFim.Value.Date : (DateTime?)null,
                        Descricao = primeiroRegistroAtividadesComplementares.Descricao,
                        Docentes = listaDocentes.Any() ? listaDocentes : null,
                        CargaHorariaEmHoraRelogioComEtiqueta = new List<HoraRelogioComEtiquetaVO>()
                        {
                          new HoraRelogioComEtiquetaVO()
                          {
                              HoraRelogio = cargaHorariaAtividadeHoraRelogio,
                              Etiqueta = primeiroRegistroAtividadesComplementares.TipoEtiquetaDisciplina
                          }
                        }
                    }
                };

                listaAtividadesComplementares.Add(elementoAtividadeComplementar);
            }

            #endregion

            retorno.ElementosHistorico.AddRange(listaDisciplinas);
            retorno.ElementosHistorico.AddRange(listaAtividadesComplementares);
            retorno.ElementosHistorico.AddRange(listaEstagios);

            retorno.CargaHorariaCurso = cargaHorariaCursoHoraRelogio;
            retorno.CargaHorariaCursoIntegralizada = cargaHorariaCursoIntegralizadaHoraRelogio;
            retorno.CodigoCurriculo = codigoCurriculo.ToString();

            return retorno;
        }

        public SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoVO BuscarDadosConsultaHistorico(long seqSolicitacaoServico)
        {
            var retorno = new SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoVO();

            var dadosEmissao = BuscarDadosSolicitacaoDocumentoConclusao(seqSolicitacaoServico);

            var formacoesConcatenadas = new List<SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaVO>();
            if (dadosEmissao.FormacoesEspecificasConcluidas != null)
                formacoesConcatenadas.AddRange(dadosEmissao.FormacoesEspecificasConcluidas);

            if (dadosEmissao.FormacoesEspecificasViasAnteriores != null)
                formacoesConcatenadas.AddRange(dadosEmissao.FormacoesEspecificasViasAnteriores);

            var dataColacao = formacoesConcatenadas.Min(a => a.DataColacaoGrau);
            var dataConclusao = formacoesConcatenadas.Min(a => a.DataConclusao);

            var dadosElementoHistorico = RetornarDadosElementoHistorico(dadosEmissao.CodigoAlunoMigracao.Value, dadosEmissao.NumeroVia, false, dadosEmissao.CodigoCursoOfertaLocalidade);

            //Dados Cabeçalho
            retorno.EmissaoDiplomaDigital1Via = dadosEmissao.EmissaoDiplomaDigital1Via;
            retorno.DescricaoSituacaoAtualMatricula = dadosEmissao.DescricaoSituacaoAtualMatricula;
            retorno.DescricaoFormaIngresso = dadosEmissao.DescricaoFormaIngresso;
            retorno.DescricaoTipoHistorico = dadosEmissao.TokenSituacaoAtualMatricula == TOKENS_SITUACAO_MATRICULA.FORMADO ? "Final" : "Parcial";
            retorno.DataIngresso = dadosEmissao.DataAdmissao;
            retorno.DataColacao = dataColacao;
            retorno.DataConclusao = dataConclusao;
            retorno.CargaHorariaCurso = dadosElementoHistorico.CargaHorariaCurso;
            retorno.CargaHorariaCursoIntegralizada = dadosElementoHistorico.CargaHorariaCursoIntegralizada;
            retorno.CodigoCurriculo = dadosElementoHistorico.CodigoCurriculo;
            retorno.CodigoCursoOfertaLocalidade = dadosEmissao.CodigoCursoOfertaLocalidade;

            //Dados Filtro
            retorno.SeqSolicitacaoServico = seqSolicitacaoServico;
            retorno.NumeroVia = dadosEmissao.NumeroVia;

            retorno.Enade = RetornarDadosEnade(dadosEmissao.CodigoAlunoMigracao.Value, dadosEmissao.CodigoCursoOfertaLocalidade);

            //Dados Mensagem Informativa
            List<string> mensagensInformativas = new List<string>();
            bool exibirMensagemOrdemCronologica = false;

            if (retorno.DataConclusao.HasValue && retorno.DataIngresso.HasValue && retorno.DataConclusao.Value.Date < retorno.DataIngresso.Value.Date)
                exibirMensagemOrdemCronologica = true;

            if (retorno.DataColacao.HasValue && retorno.DataIngresso.HasValue && retorno.DataColacao.Value.Date < retorno.DataIngresso.Value.Date)
                exibirMensagemOrdemCronologica = true;

            if (retorno.DataColacao.HasValue && retorno.DataConclusao.HasValue && retorno.DataColacao.Value.Date < retorno.DataConclusao.Value.Date)
                exibirMensagemOrdemCronologica = true;

            if (exibirMensagemOrdemCronologica)
                mensagensInformativas.Add(MessagesResource.MSG_Ordem_Cronologica_Datas_Historico);

            if (retorno.CargaHorariaCursoIntegralizada > 0 && retorno.CargaHorariaCurso > 0 && retorno.CargaHorariaCursoIntegralizada < retorno.CargaHorariaCurso)
                mensagensInformativas.Add(MessagesResource.MSG_Carga_Horaria_Datas_Historico);

            retorno.MensagemInformativa = mensagensInformativas.Any() ? string.Join("<br>", mensagensInformativas) : string.Empty;

            return retorno;
        }

        private List<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarEnadeVO> RetornarDadosEnade(long codigoAlunoMigracao, int? codigoCursoOfertaLocalidade)
        {
            var retorno = new List<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarEnadeVO>();
            var dadosSituacaoEnade = IntegracaoAcademicoService.BuscarSituacaoEnade(codigoAlunoMigracao, codigoCursoOfertaLocalidade);

            if (dadosSituacaoEnade != null && dadosSituacaoEnade.Any())
                foreach (var situacaoEnade in dadosSituacaoEnade)
                {
                    if (situacaoEnade != null)
                    {
                        var enade = new SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarEnadeVO()
                        {
                            Categoria = situacaoEnade.Categoria.Trim(),
                            Ano = situacaoEnade.AnoEdicao,
                            Condicao = situacaoEnade.Condicao.Trim(),
                        };

                        if (situacaoEnade.Categoria.ToLower().Trim() == CATEGORIA_SITUACAO_ENADE.HABILITADO.ToLower().Trim())
                        {
                            enade.Descricao = $"{CATEGORIA_SITUACAO_ENADE.HABILITADO} - {situacaoEnade.Condicao?.Trim()} - {situacaoEnade.AnoEdicao?.ToString().Trim()} - {situacaoEnade.SituacaoEnadeXsd?.Trim()}";
                        }
                        else if (situacaoEnade.Categoria.ToLower().Trim() == CATEGORIA_SITUACAO_ENADE.NAO_HABILITADO.ToLower().Trim())
                        {
                            enade.Descricao = $"{CATEGORIA_SITUACAO_ENADE.NAO_HABILITADO} - {situacaoEnade.SituacaoEnadeXsd?.Trim()}";
                        }

                        retorno.Add(enade);
                    }
                }

            return retorno;
        }

        public SMCPagerData<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoListarVO> BuscarHistoricoEscolar(SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoVO filtro)
        {
            var seqPessoaAtuacao = this.SearchProjectionByKey(filtro.SeqSolicitacaoServico, x => x.SeqPessoaAtuacao);
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);
            var dadosElementoHistorico = RetornarDadosElementoHistorico(dadosOrigem.CodigoAlunoMigracao.Value, filtro.NumeroVia, false, filtro.CodigoCursoOfertaLocalidade);

            var listaHistorico = new List<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoListarVO>();
            var elementosDisciplinas = dadosElementoHistorico.ElementosHistorico.Where(a => a.Tipo == "Disciplina").ToList();

            foreach (var elementoDisciplina in elementosDisciplinas)
            {
                var itemHistorico = new SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoListarVO()
                {
                    Codigo = elementoDisciplina.Disciplina.CodigoDisciplina,
                    Periodo = elementoDisciplina.Disciplina.Periodo,
                    Disciplina = elementoDisciplina.Disciplina.Disciplina,
                    CargaHoraria = string.Format("{0:n2}", elementoDisciplina.Disciplina.CargaHorariaComEtiqueta.FirstOrDefault().HoraRelogio),
                    FormaIntegralizacao = string.IsNullOrEmpty(elementoDisciplina.Disciplina.Aprovacao.OutraFormaIntegralizacao) ? elementoDisciplina.Disciplina.Aprovacao.FormaIntegralizacao : elementoDisciplina.Disciplina.Aprovacao.OutraFormaIntegralizacao,
                };

                if (elementoDisciplina.Disciplina.CargaHorariaComEtiqueta != null && elementoDisciplina.Disciplina.CargaHorariaComEtiqueta.Any())
                {
                    elementoDisciplina.Disciplina.CargaHorariaComEtiqueta.SMCForEach(cargaHorariaComEtiqueta =>
                    {
                        itemHistorico.Etiqueta += string.Format("{0} / ", cargaHorariaComEtiqueta.Etiqueta);
                    });

                    itemHistorico.Etiqueta = itemHistorico.Etiqueta.TrimEnd().EndsWith("/") ? itemHistorico.Etiqueta.Trim().Remove(itemHistorico.Etiqueta.Length - 2) : itemHistorico.Etiqueta;
                }

                if (!string.IsNullOrEmpty(elementoDisciplina.Disciplina.TipoAvaliacao))
                {
                    if (elementoDisciplina.Disciplina.TipoAvaliacao == TIPO_AVALIACAO_LACUNA.NOTA_ATE_CEM)
                        itemHistorico.Nota = string.Format("{0:n0}", elementoDisciplina.Disciplina.NotaAteCem);
                    else if (elementoDisciplina.Disciplina.TipoAvaliacao == TIPO_AVALIACAO_LACUNA.CONCEITO_ESPECIFICO_CURSO)
                        itemHistorico.Nota = elementoDisciplina.Disciplina.ConceitoEspecificoDoCurso;
                    else
                        itemHistorico.Nota = elementoDisciplina.Disciplina.Conceito;

                }

                if (elementoDisciplina.Disciplina.Docentes != null && elementoDisciplina.Disciplina.Docentes.Any())
                {
                    elementoDisciplina.Disciplina.Docentes.SMCForEach(docente =>
                    {
                        itemHistorico.NomeDocente += string.Format("{0} / ", docente.Nome);
                        itemHistorico.TitulacaoDocente += string.Format("{0} / ", docente.Titulacao);
                    });

                    itemHistorico.NomeDocente = itemHistorico.NomeDocente.TrimEnd().EndsWith("/") ? itemHistorico.NomeDocente.Trim().Remove(itemHistorico.NomeDocente.Length - 2) : itemHistorico.NomeDocente;
                    itemHistorico.TitulacaoDocente = itemHistorico.TitulacaoDocente.TrimEnd().EndsWith("/") ? itemHistorico.TitulacaoDocente.Trim().Remove(itemHistorico.TitulacaoDocente.Length - 2) : itemHistorico.TitulacaoDocente;
                }

                listaHistorico.Add(itemHistorico);
            }

            var total = 0;

            return new SMCPagerData<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoListarVO>(listaHistorico, total);
        }

        public SMCPagerData<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarAtividadeComplementarListarVO> BuscarHistoricoEscolarAtividadeComplementar(SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoVO filtro)
        {
            var seqPessoaAtuacao = this.SearchProjectionByKey(filtro.SeqSolicitacaoServico, x => x.SeqPessoaAtuacao);
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);
            var dadosElementoHistorico = RetornarDadosElementoHistorico(dadosOrigem.CodigoAlunoMigracao.Value, filtro.NumeroVia, false, filtro.CodigoCursoOfertaLocalidade);

            var listaAtividadeComplementar = new List<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarAtividadeComplementarListarVO>();
            var elementosAtividadeComplementar = dadosElementoHistorico.ElementosHistorico.Where(a => a.Tipo == "AtividadeComplementar").ToList();

            foreach (var elementoAtividadeComplementar in elementosAtividadeComplementar)
            {
                var itemAtividadeComplementar = new SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarAtividadeComplementarListarVO()
                {
                    Codigo = elementoAtividadeComplementar.AtividadeComplementar.Codigo,
                    DataInicio = elementoAtividadeComplementar.AtividadeComplementar.DataInicio.HasValue ? elementoAtividadeComplementar.AtividadeComplementar.DataInicio.Value.Date : (DateTime?)null,
                    DataFim = elementoAtividadeComplementar.AtividadeComplementar.DataFim.HasValue ? elementoAtividadeComplementar.AtividadeComplementar.DataFim.Value.Date : (DateTime?)null,
                    Descricao = elementoAtividadeComplementar.AtividadeComplementar.Descricao,
                    CargaHoraria = string.Format("{0:n2}", elementoAtividadeComplementar.AtividadeComplementar.CargaHorariaEmHoraRelogioComEtiqueta.FirstOrDefault().HoraRelogio),
                    Etiqueta = elementoAtividadeComplementar.AtividadeComplementar.CargaHorariaEmHoraRelogioComEtiqueta.FirstOrDefault().Etiqueta
                };

                if (elementoAtividadeComplementar.AtividadeComplementar.Docentes != null && elementoAtividadeComplementar.AtividadeComplementar.Docentes.Any())
                {
                    var docente = elementoAtividadeComplementar.AtividadeComplementar.Docentes.FirstOrDefault();

                    itemAtividadeComplementar.NomeDocente = docente.Nome;
                    itemAtividadeComplementar.TitulacaoDocente = docente.Titulacao;
                }

                listaAtividadeComplementar.Add(itemAtividadeComplementar);
            }

            var total = 0;

            return new SMCPagerData<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarAtividadeComplementarListarVO>(listaAtividadeComplementar, total);
        }

        public void FinalizarSolicitacaoDiplomaDigital(long SeqSolicitacaoDocumentoConclusao)
        {
            var retorno = this.SearchProjectionByKey(SeqSolicitacaoDocumentoConclusao, x => new
            {
                TokenTipoDocumentoAcademico = x.TipoDocumentoAcademico.Token,
                DocumentacaoConclusaoSolicitacao = x.DocumentosAcademicos.Select(d => new
                {
                    SeqDocumentoConclusao = d.Seq,
                    d.SituacaoAtual.SituacaoDocumentoAcademico.Token,
                    DescricaoSituacaoDocumentoAcademicoAtual = d.SituacaoAtual.SituacaoDocumentoAcademico.Descricao
                }).ToList()
            });

            var documentacaoConclusaoSolicitacaoConcluidos = retorno.DocumentacaoConclusaoSolicitacao.Where(w => w.Token != TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.AGUARDANDO_ENTREGA).Any();
            if (!documentacaoConclusaoSolicitacaoConcluidos)
                SolicitacaoServicoDomainService.FinalizarSolicitacaoDiplomaDigital(SeqSolicitacaoDocumentoConclusao, retorno.TokenTipoDocumentoAcademico);
        }

        public void VerificarDocumentoConclusaoAtivo(long seqSolicitacaoServico)
        {
            var dadosDocumentoConclusaoSolicitacao = this.SearchProjectionByKey(seqSolicitacaoServico, s => new DocumentoConclusaoAtivoVO
            {
                GrupoDocumentoAcademico = (GrupoDocumentoAcademico?)s.TipoDocumentoAcademico.GrupoDocumentoAcademico,
                SeqPessoaAtuacao = s.SeqPessoaAtuacao,
                DocumentosConclusao = s.DocumentosAcademicos.OfType<DocumentoConclusao>().Select(d => new DocumentosConclusaoVO
                {
                    FormacoesEspecificas = d.FormacoesEspecificas.Select(x => new DocumentoConclusaoFormacaoVO
                    {
                        DescricaoDocumentoConclusao = x.AlunoFormacao.DescricaoDocumentoConclusao,
                        SeqGrauAcademico = (long?)x.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica.FirstOrDefault(f => f.SeqFormacaoEspecifica == x.AlunoFormacao.SeqFormacaoEspecifica).GrauAcademico.Seq,
                        SeqTitulacao = (long?)x.AlunoFormacao.Titulacao.Seq,
                    }).ToList()
                }).ToList()
            });

            if (dadosDocumentoConclusaoSolicitacao.GrupoDocumentoAcademico.HasValue && dadosDocumentoConclusaoSolicitacao.DocumentosConclusao.Any())
            {
                var tokensTipoDocumentoAcademico = new List<string>() { TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL };
                if (dadosDocumentoConclusaoSolicitacao.GrupoDocumentoAcademico == GrupoDocumentoAcademico.HistoricoEscolar)
                    tokensTipoDocumentoAcademico = new List<string>() { TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_PARCIAL, TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_FINAL, TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_2VIA };

                var documentoConclusao = dadosDocumentoConclusaoSolicitacao.DocumentosConclusao.SelectMany(s => s.FormacoesEspecificas);

                var temDocumento = DocumentoConclusaoDomainService.VerificarDocumentoConclusao(dadosDocumentoConclusaoSolicitacao.SeqPessoaAtuacao, documentoConclusao.Select(s => s.DescricaoDocumentoConclusao).FirstOrDefault(), documentoConclusao.Select(s => s.SeqGrauAcademico).FirstOrDefault(), documentoConclusao.Select(s => s.SeqTitulacao).FirstOrDefault(), tokensTipoDocumentoAcademico);
                if (temDocumento)
                    throw new SolicitacaoDocumentoConclusaoReabeturaNaoPermitidaException();
            }
        }

        /// <summary>
        /// Reabertura de uma solicitação de serviço de emissão de documento de conclusão
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação</param>
        /// <param name="observacao">Observação da reabertura</param>
        public void ReabrirSolicitacao(long seqSolicitacaoServico, string observacao)
        {
            // Recupera os dados da solicitação
            var specSol = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);
            var dadosSolicitacao = SolicitacaoServicoDomainService.SearchProjectionByKey(specSol, s => new
            {
                NumeroProtocolo = s.NumeroProtocolo,
                SeqPessoaAtuacao = s.SeqPessoaAtuacao,
                TokenEtapa = s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.Token,
                DocumentosConclusao = s.DocumentosAcademicos.Select(a => new
                {
                    a.Seq,
                    a.SeqDocumentoGAD,
                    a.TipoDocumentoAcademico.Token
                })
            });

            if (dadosSolicitacao.TokenEtapa == TOKEN_ETAPA_SOLICITACAO.ASSINATURA_DOCUMENTO_DIGITAL)
                throw new SolicitacaoServicoReabeturaNaoPermitidaException();

            //Se o nível - ensino do aluno for igual a GRADUACAO, deverá ser acionada a rotina do SGA 
            //st_altera_situacao_servico_diploma_digital para reabrir o serviço(tramite - serviço)
            //correspondente à solicitação - serviço do Acadêmico.
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosSolicitacao.SeqPessoaAtuacao);
            var tokenNivelEnsino = NivelEnsinoDomainService.SearchProjectionByKey(new SMCSeqSpecification<NivelEnsino>(dadosOrigem.SeqNivelEnsino), x => x.Token);

            if (tokenNivelEnsino == TOKEN_NIVEL_ENSINO.GRADUACAO)
            {
                var usuarioOperacao = UsuarioLogado.RetornaUsuarioLogado();
                IntegracaoAcademicoService.ReabrirSolicitacaoDiplomaDigital(dadosSolicitacao.NumeroProtocolo, usuarioOperacao);
            }

            if (dadosSolicitacao.DocumentosConclusao != null && dadosSolicitacao.DocumentosConclusao.Any())
            {
                //Se houver documento - conclusão associado a solicitação - serviço, inserir no histórico de situação do documento - conclusão,
                //a situação anterior à situação atual. Usar como referência a data de inclusão.
                //No campo "Observação" inserir o texto no seguinte formato: "Reabertura da solicitação" - [Observação informada na reabertura]
                foreach (var documentoConclusao in dadosSolicitacao.DocumentosConclusao)
                {
                    var specDocumentoAcademicoHistoricoSituacao = new DocumentoAcademicoHistoricoSituacaoFilterSpecification { SeqDocumentoAcademico = documentoConclusao.Seq };
                    var seqSituacaoDocumentoAcademico = DocumentoAcademicoHistoricoSituacaoDomainService.SearchBySpecification(specDocumentoAcademicoHistoricoSituacao)
                                                                                                        .OrderByDescending(a => a.Seq).Take(2).LastOrDefault().SeqSituacaoDocumentoAcademico;

                    var novoDocumentoAcademicoHistoricoSituacao = new DocumentoAcademicoHistoricoSituacao()
                    {
                        SeqDocumentoAcademico = documentoConclusao.Seq,
                        SeqSituacaoDocumentoAcademico = seqSituacaoDocumentoAcademico,
                        Observacao = $"Reabertura da solicitação - {observacao}"
                    };

                    DocumentoAcademicoHistoricoSituacaoDomainService.SaveEntity(novoDocumentoAcademicoHistoricoSituacao);

                    //Se houver documento - gad associado ao respectivo documento-conclusão da solicitação-serviço, 
                    //o mesmo deverá ser restaurado, conforme RN_DDG_003 -Restaurar diploma, 
                    //passando como parâmetro a observação informada no seguinte formato: "Reabertura da solicitação" - [Observação informada na reabertura].
                    if (documentoConclusao.SeqDocumentoGAD.HasValue)
                    {
                        var usuarioInclusao = UsuarioLogado.RetornaUsuarioLogado();
                        var modeloDocumentoAcademico = new
                        {
                            SeqDocumentoAcademico = documentoConclusao.SeqDocumentoGAD.Value,
                            Observacao = $"Reabertura da solicitação - {observacao}",
                            UsuarioInclusao = usuarioInclusao
                        };

                        if (documentoConclusao.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL)
                        {
                            var retornoGAD = APIDiplomaGAD.Execute<object>("Restaurar", modeloDocumentoAcademico);
                            var retornoRestaurarDiploma = JsonConvert.DeserializeObject<MensagemHttp>(retornoGAD.ToString());

                            if (!string.IsNullOrEmpty(retornoRestaurarDiploma.ErrorMessage))
                                throw new Exception(retornoRestaurarDiploma.ErrorMessage);
                        }
                        else if (documentoConclusao.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_2VIA ||
                                 documentoConclusao.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_FINAL ||
                                 documentoConclusao.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_PARCIAL)
                        {
                            var retornoGAD = APIHistoricoGAD.Execute<object>("Restaurar", modeloDocumentoAcademico);
                            var retornoRestaurarHistorico = JsonConvert.DeserializeObject<MensagemHttp>(retornoGAD.ToString());

                            if (!string.IsNullOrEmpty(retornoRestaurarHistorico.ErrorMessage))
                                throw new Exception(retornoRestaurarHistorico.ErrorMessage);
                        }
                    }
                }
            }
        }
    }
}