using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Areas.ORT.Exceptions;
using SMC.Academico.Common.Areas.ORT.Exceptions.PublicacaoBdp;
using SMC.Academico.Common.Areas.ORT.Includes;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Common.Enums;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.ORT.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.Domain.Repositories;
using SMC.AvaliacaoPermanente.ServiceContract.Areas.QST.Data;
using SMC.AvaliacaoPermanente.ServiceContract.Areas.QST.Interfaces;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Repository;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Interfaces;
using SMC.Localidades.Common.Areas.LOC.Enums;
using SMC.Notificacoes.Common.Areas.NTF.Enums;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;

namespace SMC.Academico.Domain.Areas.ORT.DomainServices
{
    public class PublicacaoBdpDomainService : AcademicoContextDomain<PublicacaoBdp>
    {
        #region [ Query ]

        public const string JOB_MUDANCA_PUBLICACAO =
@"select	
	ta.seq_trabalho_academico as SeqTrabalhoAcademico,
	p.seq_publicacao_bdp as SeqPublicacaoBdp,
	e.nom_entidade as NomeEntidadeVinculo,
	tau.seq_atuacao_aluno as SeqAtuacaoAluno,
	tau.nom_autor as NomeAutor,
	ta.dsc_titulo as Titulo,
	a.dat_autorizacao as DatAutorizacao, 
	d.dsc_dominio as TipoAutorizacao
from	ORT.trabalho_academico ta
join	ORT.publicacao_bdp p
		on ta.seq_trabalho_academico = p.seq_trabalho_academico
join	ORT.publicacao_bdp_autorizacao a
		on p.seq_publicacao_bdp = a.seq_publicacao_bdp
join	dominio d
		on a.idt_dom_tipo_autorizacao = d.val_dominio
		and d.nom_dominio = 'tipo_autorizacao'
join	ORT.trabalho_academico_autoria tau
		on ta.seq_trabalho_academico = tau.seq_trabalho_academico
join	ALN.aluno al
		on tau.seq_atuacao_aluno = al.seq_pessoa_atuacao
join	ALN.aluno_historico ah
		on al.seq_pessoa_atuacao = ah.seq_atuacao_aluno
		and ah.ind_atual = 1
join	ORG.entidade e
		on ah.seq_entidade_vinculo = e.seq_entidade
where	ta.seq_entidade_instituicao = @SEQ_INSTITUICAO_ENSINO
and		a.dat_autorizacao = @DAT_AUTORIZACAO
-- mudança de publicação
and		exists (select	1
				from	ORT.publicacao_bdp_autorizacao aa
				where	aa.seq_publicacao_bdp = p.seq_publicacao_bdp
				and		aa.dat_autorizacao < a.dat_autorizacao)";

        #endregion [ Query ]

        #region Services

        private IIntegracaoAcademicoService IntegracaoAcademicoService => Create<IIntegracaoAcademicoService>();

        private IAmostraService AmostraService => Create<IAmostraService>();

        private INotificacaoService NotificacaoService => Create<INotificacaoService>();

        #endregion Services

        #region DomainServices

        private CicloLetivoDomainService CicloLetivoDomainService => Create<CicloLetivoDomainService>();

        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private PessoaDomainService PessoaDomainService => Create<PessoaDomainService>();

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        private TrabalhoAcademicoDomainService TrabalhoAcademicoDomainService => this.Create<TrabalhoAcademicoDomainService>();

        private PublicacaoBdpHistoricoSituacaoDomainService PublicacaoBdpHistoricoSituacaoDomainService => this.Create<PublicacaoBdpHistoricoSituacaoDomainService>();

        private IAcademicoRepository AcademicoRepository => this.Create<IAcademicoRepository>();

        private InstituicaoNivelDomainService InstituicaoNivelDomainService => this.Create<InstituicaoNivelDomainService>();

        private AlunoDomainService AlunoDomainService => this.Create<AlunoDomainService>();

        private PlanoEstudoDomainService PlanoEstudoDomainService => this.Create<PlanoEstudoDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => this.Create<SolicitacaoServicoDomainService>();

        private SolicitacaoMatriculaDomainService SolicitacaoMatriculaDomainService => this.Create<SolicitacaoMatriculaDomainService>();

        private EntidadeConfiguracaoNotificacaoDomainService EntidadeConfiguracaoNotificacaoDomainService => this.Create<EntidadeConfiguracaoNotificacaoDomainService>();

        private PessoaAtuacaoAmostraPpaDomainService PessoaAtuacaoAmostraPpaDomainService => this.Create<PessoaAtuacaoAmostraPpaDomainService>();

        private EntidadeDomainService EntidadeDomainService => this.Create<EntidadeDomainService>();

        private OrientacaoPessoaAtuacaoDomainService OrientacaoPessoaAtuacaoDomainService => this.Create<OrientacaoPessoaAtuacaoDomainService>();

        private ConfiguracaoAvaliacaoPpaDomainService ConfiguracaoAvaliacaoPpaDomainService => this.Create<ConfiguracaoAvaliacaoPpaDomainService>();

        private HistoricoEscolarDomainService HistoricoEscolarDomainService => this.Create<HistoricoEscolarDomainService>();

        private PublicacaoBdpAutorizacaoDomainService PublicacaoBdpAutorizacaoDomainService => this.Create<PublicacaoBdpAutorizacaoDomainService>();

        private ProgramaDomainService ProgramaDomainService => Create<ProgramaDomainService>();

        #endregion DomainServices

        /// <summary>
        /// Registrar a publicação do trabalho no BDP com a situação "Aguardando Cadastro pelo Aluno".
        /// O restante dos abributos deverão ser cadastrados com o valor igual a nulo.
        ///
        /// Enviar e-mail para o aluno informando sobre a necessidade de preencher os dados da publicação BDP e liberar
        /// para consulta
        /// </summary>
        /// <param name="seqTrabalhoAcademico">Sequencial do trabalho que está sendo registrado no BDP</param>
        public void CriarPublicacaoBDP(long seqTrabalhoAcademico)
        {
            // Busca os dados do trabalho academico
            var specTrabalho = new SMCSeqSpecification<TrabalhoAcademico>(seqTrabalhoAcademico);
            var dadosTrabalho = TrabalhoAcademicoDomainService.SearchProjectionByKey(specTrabalho, t => new
            {
                PublicacaoBDP = t.PublicacaoBdp.FirstOrDefault(),
                SeqInstituicaoEnsino = t.SeqInstituicaoEnsino,
                DescricaoTipoTrabalho = t.TipoTrabalho.Descricao,
                DataDefesa = t.DivisoesComponente.Select(sd => sd.OrigemAvaliacao.AplicacoesAvaliacao
                                                                 .Where(w => w.DataCancelamento == null && w.Avaliacao.TipoAvaliacao == TipoAvaliacao.Banca)
                                                                 .OrderByDescending(o => o.Seq).Select(se => se.DataInicioAplicacaoAvaliacao).FirstOrDefault()).FirstOrDefault(),
                Autor = t.Autores.Select(a => new
                {
                    Nome = a.NomeAutor,
                    SeqEntidadeVinculo = a.Aluno.Historicos.FirstOrDefault(h => h.Atual).SeqEntidadeVinculo,
                    EmailsAluno = a.Aluno.EnderecosEletronicos.Where(e => e.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email)
                                                              .Select(e => e.EnderecoEletronico.Descricao)
                }).FirstOrDefault()
            });

            // Se já existe uma publicação BDP para o trabalho, erro
            if (dadosTrabalho.PublicacaoBDP != null)
                throw new PublicacaoBDPJaExistenteTrabalhoException();

            // Cria a publicação do Trabalho Acadêmico
            var publicacaoBDP = new PublicacaoBdp()
            {
                SeqTrabalhoAcademico = seqTrabalhoAcademico,
                HistoricoSituacoes = new List<PublicacaoBdpHistoricoSituacao>()
            };

            // Selecionar a configuração da notificação cuja o tipo de notificação tem o token "LIBERACAO_CADASTRO_TRABALHO_BDP"
            // e esteja associada a entidade da instituição de ensino do aluno (ou do trabalho)
            var seqConfigTipoNotificacao = EntidadeConfiguracaoNotificacaoDomainService.BuscarSeqConfiguracaoNotificacaoAtivo(dadosTrabalho.SeqInstituicaoEnsino, TOKEN_TIPO_NOTIFICACAO.LIBERACAO_CADASTRO_TRABALHO_BDP);

            // Se encontrou a configuração, envia o email
            long? seqNotificacaoEmailDestinatario = null;
            if (seqConfigTipoNotificacao > 0)
            {
                // Busca os dados da entidade de vinculo para formatação do e-mail
                var specEntidade = new SMCSeqSpecification<Entidade>(dadosTrabalho.Autor.SeqEntidadeVinculo);

                // RN_ORT_012 - Alteração preenchimento telefone:
                // Buscar telefone da entidade sem categoria associada. Se não existir, buscar categoria 'secretaria'
                var dadosEntidade = EntidadeDomainService.SearchProjectionByKey(specEntidade, e => new
                {
                    Nome = e.Nome,
                    Endereco = e.Enderecos.FirstOrDefault(d => d.TipoEndereco == TipoEndereco.Comercial),
                    Telefones = e.Telefones,
                    Emails = e.EnderecosEletronicos.Where(m => m.TipoEnderecoEletronico == TipoEnderecoEletronico.Email)
                });

                // Formata o endereço da entidade
                string enderecoFormatado = string.Format("{0}, {1}{2} - {3}, {4} - {5}, CEP: {6}",
                    string.IsNullOrEmpty(dadosEntidade.Endereco.Logradouro) ? string.Empty : dadosEntidade.Endereco.Logradouro.Trim(),
                    string.IsNullOrEmpty(dadosEntidade.Endereco.Numero) ? string.Empty : dadosEntidade.Endereco.Numero.Trim(),
                    string.IsNullOrEmpty(dadosEntidade.Endereco.Complemento) ? string.Empty : string.Format(" / {0}", dadosEntidade.Endereco.Complemento.Trim()),
                    string.IsNullOrEmpty(dadosEntidade.Endereco.Bairro) ? string.Empty : dadosEntidade.Endereco.Bairro.Trim(),
                    string.IsNullOrEmpty(dadosEntidade.Endereco.NomeCidade) ? string.Empty : dadosEntidade.Endereco.NomeCidade.Trim(),
                    string.IsNullOrEmpty(dadosEntidade.Endereco.SiglaUf) ? string.Empty : dadosEntidade.Endereco.SiglaUf.Trim(),
                    string.IsNullOrEmpty(dadosEntidade.Endereco.Cep) ? string.Empty : dadosEntidade.Endereco.Cep.Trim());

                // Formata o telefone da entidade
                string telefoneFormatado = string.Empty;
                if (dadosEntidade.Telefones != null)
                {
                    // Buscar telefone comercial sem categoria associada,
                    var telComercialSemCategoria = dadosEntidade.Telefones.FirstOrDefault(t => t.TipoTelefone == TipoTelefone.Comercial && !t.CategoriaTelefone.HasValue);
                    if (telComercialSemCategoria != null)
                    {
                        telefoneFormatado = string.Format("({0}){1}",
                                                            telComercialSemCategoria.CodigoArea.ToString().Trim(),
                                                            telComercialSemCategoria.Numero.ToString().Trim());
                    }
                    // Se não existir, buscar comercial e categoria 'secretaria'
                    else
                    {
                        var telComercialSecretaria = dadosEntidade.Telefones.FirstOrDefault(t => t.TipoTelefone == TipoTelefone.Comercial && t.CategoriaTelefone == CategoriaTelefone.Secretaria);
                        if (telComercialSecretaria != null)
                        {
                            telefoneFormatado = string.Format("({0}){1}",
                                                                telComercialSecretaria.CodigoArea.ToString().Trim(),
                                                                telComercialSecretaria.Numero.ToString().Trim());
                        }
                    }
                }

                // Formata o endereço eletrônico da entidade
                string emailFormatado = string.Empty;
                if (dadosEntidade.Emails != null)
                {
                    // Buscar e-mails sem categoria associada
                    var emailSemCategoria = dadosEntidade.Emails.FirstOrDefault(e => !e.CategoriaEnderecoEletronico.HasValue);
                    if (emailSemCategoria != null)
                    {
                        emailFormatado = emailSemCategoria.Descricao;
                    }
                    // Se não existir, buscar categoria 'secretaria'
                    else
                    {
                        var emailSecretaria = dadosEntidade.Emails.FirstOrDefault(e => e.CategoriaEnderecoEletronico == CategoriaEnderecoEletronico.Secretaria);
                        if (emailSecretaria != null)
                        {
                            emailFormatado = emailSecretaria.Descricao;
                        }
                    }
                }

                // Monta dicionário para merge dos dados
                // Tags para substituição:
                // {{NOM_PESSOA}} -> nome do aluno;
                // {{DSC_TIPO_TRABALHO}} -> descrição do tipo do trabalho;
                // {{DAT_DEFESA}} -> data de início da aplicação da avaliação no formato "dd/mm/yyyy";
                // {{NOM_ENTIDADE}} ->Com o nome da entidade do grupo de programa relacionado ao curso do aluno.
                // {{END_ENTIDADE}} ->Com o endereço comercial da entidade do grupo de programa relacionado ao curso do aluno. (Obs.: Retornar o primeiro endereço cadastrado, caso haja mais de um)
                // {{TEL_ENTIDADE}} ->Com o telefone comercial da entidade do grupo de programa relacionado ao curso do aluno. (Obs.: Retornar o primeiro telefone cadastrado, caso haja mais de um)
                // {{END_ELETRONICO_ENTIDADE}} ->Com o e-mail da entidade do grupo de programa relacionado ao curso do aluno. (Obs.: Retornar o primeiro e-mail cadastrado, caso haja mais de um)
                Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
                dadosMerge.Add("{{NOM_PESSOA}}", dadosTrabalho.Autor.Nome);
                dadosMerge.Add("{{DSC_TIPO_TRABALHO}}", dadosTrabalho.DescricaoTipoTrabalho);
                dadosMerge.Add("{{DAT_DEFESA}}", dadosTrabalho.DataDefesa.ToString("dd/MM/yyyy"));
                dadosMerge.Add("{{NOM_ENTIDADE}}", dadosEntidade.Nome);
                dadosMerge.Add("{{END_ENTIDADE}}", enderecoFormatado);
                dadosMerge.Add("{{TEL_ENTIDADE}}", telefoneFormatado);
                dadosMerge.Add("{{END_ELETRONICO_ENTIDADE}}", emailFormatado);

                // Enviar uma notificação com a configuração selecionada para todos os e-mails cadastrados para o aluno em questão.
                var dataNotificacao = new NotificacaoEmailData()
                {
                    DadosMerge = dadosMerge,
                    SeqConfiguracaoNotificacao = seqConfigTipoNotificacao,
                    DataPrevistaEnvio = DateTime.Now,
                    PrioridadeEnvio = PrioridadeEnvio.QuandoPossivel,
                    Destinatarios = new List<NotificacaoEmailDestinatarioData>()
                    {
                        new NotificacaoEmailDestinatarioData()
                        {
                            EmailDestinatario = string.Join(";", dadosTrabalho.Autor.EmailsAluno)
                        }
                    }
                };

                // Chama o serviço de envio de notificação
                long seqNotificacaoEnviada = NotificacaoService.SalvarNotificacao(dataNotificacao);

                // Busca o sequencial da notificação-email-destinatário enviada
                var envioDestinatario = NotificacaoService.BuscaNotificacaoEmailDestinatario(seqNotificacaoEnviada);
                seqNotificacaoEmailDestinatario = envioDestinatario.FirstOrDefault()?.Seq;
            }

            // Inclui na publicação BDP o histórico da publicação com a situação "AguardandoCadastroAluno" e o
            // Seq do email enviado (caso enviado)
            var publicacaoBDPHistorico = new PublicacaoBdpHistoricoSituacao()
            {
                SituacaoTrabalhoAcademico = SituacaoTrabalhoAcademico.AguardandoCadastroAluno,
                SeqNotificacaoEmailDestinatario = seqNotificacaoEmailDestinatario
            };

            // Vinculo ao histórico a situação do trabalho
            publicacaoBDP.HistoricoSituacoes.Add(publicacaoBDPHistorico);

            // Persisto a entidade na base de dados
            this.SaveEntity(publicacaoBDP);
        }

        /// <summary>
        /// Salva as alterações da publicação BDP pela tela de conferencia e liberação para consulta
        /// </summary>
        /// <param name="liberacaoPublicacaoBdpVO">Informações da publicação BDP</param>
        /// <returns>Sequencial alterado</returns>
        public long SalvarAlteracoesLiberacaoConsultaBdp(LiberacaoPublicacaoBdpVO liberacaoPublicacaoBdpVO)
        {
            #region [Validações]

            // Lança exception se não houver ao menos um arquivo de texto completo anexado
            if (liberacaoPublicacaoBdpVO.Arquivos.Count() == 0 || !liberacaoPublicacaoBdpVO.Arquivos.Any(a => a.TipoAutorizacao == TipoAutorizacao.TextoCompleto))
                throw new PublicacaoBdpTipoAutorizacaoObrigatorioException(TipoAutorizacao.TextoCompleto);

            // Lança exception se houver dois arquivos do mesmo tipo
            if (liberacaoPublicacaoBdpVO.Arquivos.GroupBy(g => g.TipoAutorizacao).Select(s => new { Tipo = s.Key, Count = s.Count() }).Any(w => w.Count > 1))
                throw new PublicacaoBdpArquivoTipoException();

            /// Verifica a RN_ORT_003 - É necessário informar pelo menos uma palavra-chave para cada idioma.
            /// Caso isso não ocorra, exibir mensagem de erro e abortar a operação.
            if (liberacaoPublicacaoBdpVO.Idiomas.Any(f => !f.PalavrasChave.Any()))
                throw new PublicacaoBdpIdiomaSemPalavraChaveException();

            // É necessário que um e somente um dos idiomas esteja marcado como o idioma do trabalho
            if (liberacaoPublicacaoBdpVO?.Idiomas?.Where(x => x.IdiomaTrabalho).ToList().Count() != 1)
                throw new PublicacaoBdpIdiomaTrabalhoException();

            // UC_ORT_003_02  - NV01 - Obrigatorio existir um arquivo para cada tipo de autorização existente para a publicação

            // Separa os arquivos que foram enviados
            var arquivosEnviados = liberacaoPublicacaoBdpVO.Arquivos.Select(x => x.TipoAutorizacao).ToList();

            // Busca as autorizações existentes para essa publicação
            var spec = new PublicacaoBdpAutorizacaoFilterSpecification() { SeqPublicacaoBdp = liberacaoPublicacaoBdpVO.SeqPublicacaoBdp };
            var autorizacoes = PublicacaoBdpAutorizacaoDomainService.SearchProjectionBySpecification(spec, x => x.TipoAutorizacao).ToList();

            // Retorna false se nao possui todos os arquivos para cada tipo de autorizacao realizada pelo aluno
            var possuiAutorizacoesNecessarias = autorizacoes.All(p => arquivosEnviados.Contains(p));

            if (!possuiAutorizacoesNecessarias)
            {
                //Lançar exception informando o tipo que falta
                var tipoFaltando = autorizacoes.Except(arquivosEnviados).FirstOrDefault();
                throw new PublicacaoBdpTipoAutorizacaoObrigatorioException(tipoFaltando);
            }
            #endregion

            // Busca os dados da publicação BDP do banco de dados para alteração
            var publicacaoBdp = SearchByKey(new SMCSeqSpecification<PublicacaoBdp>(liberacaoPublicacaoBdpVO.SeqPublicacaoBdp),
                                                IncludesPublicacaoBdp.InformacoesIdioma_PalavrasChave | IncludesPublicacaoBdp.Arquivos);

            // Busca os dados do trabalho acadêmico do banco de dados para alteração
            var trabalhoAcademico = TrabalhoAcademicoDomainService.SearchByKey(new SMCSeqSpecification<TrabalhoAcademico>(liberacaoPublicacaoBdpVO.Seq),
                                                IncludesTrabalhoAcademico.Autores | IncludesTrabalhoAcademico.TipoTrabalho);

            // Inicia a transação
            using (var unit = SMCUnitOfWork.Begin())
            {
                try
                {
                    // Atualiza as informações da publicação BDP
                    publicacaoBdp.QuantidadeVolumes = liberacaoPublicacaoBdpVO.QuantidadeVolumes;
                    publicacaoBdp.QuantidadePaginas = liberacaoPublicacaoBdpVO.QuantidadePaginas;
                    // FIX: Carol - Acredito que o código abaixo esteja errado. A autorização deve ser feita pelo aluno apenas.
                    //publicacaoBdp.TipoAutorizacao = liberacaoPublicacaoBdpVO.TipoAutorizacao;

                    if (publicacaoBdp.Arquivos != null && publicacaoBdp.Arquivos.Count > 0)
                    {
                        publicacaoBdp.Arquivos = publicacaoBdp.Arquivos.Where(w => liberacaoPublicacaoBdpVO.Arquivos.Any(a => a.Seq == w.Seq)).ToList();
                    }

                    if (liberacaoPublicacaoBdpVO.Arquivos != null && liberacaoPublicacaoBdpVO.Arquivos.Count > 0)
                    {
                        foreach (var item in liberacaoPublicacaoBdpVO.Arquivos)
                        {
                            if (!string.IsNullOrEmpty(item.UrlArquivo) && item.Arquivo.FileData == null)
                            {
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                                using (WebClient client = new WebClient())
                                {
                                    item.Arquivo.FileData = client.DownloadData(item.UrlArquivo);
                                    item.Arquivo.State = SMCUploadFileState.Changed;
                                }
                            }
                        }

                        foreach (var file in liberacaoPublicacaoBdpVO.Arquivos)
                        {
                            // Se enviar arquivo e feito o tratamento
                            if (file.Arquivo != null && file.Arquivo.FileData != null && (file.Seq == 0 || file.Arquivo.State == SMCUploadFileState.Changed))
                            {
                                // Busca os dados o autor
                                var autor = trabalhoAcademico.Autores.Select(x => new
                                {
                                    Nome = x.NomeAutor,
                                    SeqAluno = x.SeqAluno
                                }).FirstOrDefault();

                                // Salva o arquivo no diretório do BDP
                                var arquivoSalvo = SalvarArquivoPublicacaoBDP(file.Arquivo.FileData, trabalhoAcademico.Seq, autor.Nome, autor.SeqAluno, file.TipoAutorizacao);

                                if (publicacaoBdp.Arquivos.Any(a => a.TipoAutorizacao == file.TipoAutorizacao))
                                {
                                    publicacaoBdp.Arquivos.SMCForEach(f =>
                                    {
                                        if (f.TipoAutorizacao == file.TipoAutorizacao)
                                        {
                                            // Atualiza as informações do arquivo salvo
                                            f.Seq = file.Seq;
                                            f.TipoAutorizacao = file.TipoAutorizacao;
                                            f.NomeArquivo = arquivoSalvo.NomeArquivo;
                                            f.TamanhoArquivo = file.Arquivo.Size;
                                            f.UrlArquivo = arquivoSalvo.UrlArquivo;
                                        }
                                    });
                                }
                                else
                                {
                                    publicacaoBdp.Arquivos.Add(new PublicacaoBdpArquivo()
                                    {
                                        TipoAutorizacao = file.TipoAutorizacao,
                                        NomeArquivo = arquivoSalvo.NomeArquivo,
                                        TamanhoArquivo = file.Arquivo.Size,
                                        UrlArquivo = arquivoSalvo.UrlArquivo,
                                    });
                                }
                            }
                        }
                    }

                    // Atualiza as informações por idioma
                    foreach (var idioma in publicacaoBdp.InformacoesIdioma)
                    {
                        var idiomaVO = liberacaoPublicacaoBdpVO.Idiomas.FirstOrDefault(f => f.Seq == idioma.Seq);
                        idioma.Titulo = idiomaVO.Titulo;
                        idioma.IdiomaTrabalho = idiomaVO.IdiomaTrabalho;
                        idioma.Resumo = idiomaVO.Resumo;
                        idioma.PalavrasChave = idiomaVO.PalavrasChave.TransformList<PublicacaoBdpPalavraChave>();
                    }

                    // Salva a publicação BDP
                    SaveEntity(publicacaoBdp);

                    // Atualiza o email dos autores dos trabalhos
                    foreach (var autores in trabalhoAcademico.Autores)
                    {
                        var autorVO = liberacaoPublicacaoBdpVO.Alunos.FirstOrDefault(f => f.SeqTrabalhoAcademicoAutoria == autores.Seq);
                        autores.EmailAutor = autorVO.EmailAutor;
                    }

                    TrabalhoAcademicoDomainService.SaveEntity(trabalhoAcademico);

                    unit.Commit();
                }
                catch (Exception e)
                {
                    unit.Rollback();
                    throw e;
                }
            }

            return liberacaoPublicacaoBdpVO.Seq;
        }

        /// <summary>
        /// Salva o arquivo da publicação BDP no diretório de teses
        /// </summary>
        /// <param name="conteudoArquivo">Conteudo do arquivo a ser salvo</param>
        /// <param name="seqTrabalho">Sequencial do trabalho</param>
        /// <param name="nomeAutor">Nome do autor</param>
        /// <param name="seqAluno">Sequencial do aluno da publicação</param>
        /// <returns>Retorna o nome e a url do arquivo salvo</returns>
        private (string NomeArquivo, string UrlArquivo) SalvarArquivoPublicacaoBDP(byte[] conteudoArquivo, long seqTrabalho, string nomeAutor, long seqAluno, TipoAutorizacao tipoAutorizacao)
        {
            // Busca os parâmetros para upload de arquivos
            var urlDownloadTeses = ConfigurationManager.AppSettings["UrlDownloadTeses"];
            if (string.IsNullOrWhiteSpace(urlDownloadTeses))
                throw new ParametrosUploadArquivoBDPInvalidoException("UrlDownloadTeses");
            var diretorioTeses = ConfigurationManager.AppSettings["DiretorioTeses"];
            if (string.IsNullOrWhiteSpace(diretorioTeses))
                throw new ParametrosUploadArquivoBDPInvalidoException("DiretorioTeses");

            // Busca o curso do aluno
            var specAluno = new SMCSeqSpecification<Aluno>(seqAluno);
            string nomeCurso = AlunoDomainService.SearchProjectionByKey(specAluno, x => x.Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.NomeReduzido);

            /// Monta o nome do arquivo segundo a regra:
            /// NomeDoCurso + _ + NomeDoAluno + _ + SequencialDoTrabalho.
            /// Onde:
            ///  - NomeDoCurso: Nome reduzido da entidade relacionada ao Curso do Aluno(cso.curso.seq_entidade).
            /// Retirando os espaços vazios, acentos e caracteres especiais.
            ///  - NomeDoAluno: Nome do aluno retirando os espaços vazios, acentos e caracteres especiais.
            ///  - SequencialDoTrabalho: ID do trabalho em questão.
            /// Exemplos:
            /// - ZoologiaDeVertebrados_JussaraVieiraESilva_1234
            /// - Direito_CarolinaMariaFranciscoCota_9876
            nomeCurso = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(nomeCurso);
            string nomeCursoFormatado = nomeCurso.Replace(" ", "").SMCRemoveAccents();
            nomeAutor = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(nomeAutor);
            string nomeAutorFormatado = nomeAutor.Replace(" ", "").SMCRemoveAccents();
            string tipo = tipoAutorizacao.SMCGetDescription().Replace(" ", "").SMCRemoveAccents();
            string nomeArquivo = string.Format("{0}_{1}_{2}_{3}.pdf", nomeCursoFormatado, nomeAutorFormatado, seqTrabalho.ToString(), tipo);

            // Monta a url do arquivo
            string urlArquivo = Path.Combine(urlDownloadTeses, nomeArquivo);

            // Salva o arquivo no diretório do BDP
            SMCFileHelper.SaveFile(conteudoArquivo, nomeArquivo, new DirectoryInfo(diretorioTeses));

            return (nomeArquivo, urlArquivo);
        }

        /// <summary>
        /// Altera a situação de uma publicação BDP enviando e-mail quando necessário
        /// </summary>
        /// <param name="seqPublicacaoBdp">Sequencial da publicação BDP a ser alterada</param>
        /// <param name="situacao">Situação para a qual a publicação BDP deve ir</param>
        public void AlterarSituacao(long seqPublicacaoBdp, SituacaoTrabalhoAcademico situacao)
        {
            string tokenNotificacao = null;
            long? seqNotificacaoEnviada = null;

            // De acordo com a situação, verifica se deve ou não enviar email.
            switch (situacao)
            {
                case SituacaoTrabalhoAcademico.AguardandoCadastroAluno:
                    tokenNotificacao = TOKEN_TIPO_NOTIFICACAO.LIBERACAO_CADASTRO_TRABALHO_BDP;
                    break;

                case SituacaoTrabalhoAcademico.AutorizadaLiberadaSecretaria:
                    tokenNotificacao = TOKEN_TIPO_NOTIFICACAO.AUTORIZACAO_PUBLICACAO_BDP;
                    break;

                case SituacaoTrabalhoAcademico.LiberadaBiblioteca:
                    tokenNotificacao = TOKEN_TIPO_NOTIFICACAO.LIBERACAO_PUBLICACAO_BDP;
                    break;

                case SituacaoTrabalhoAcademico.LiberadaConsulta:
                    tokenNotificacao = TOKEN_TIPO_NOTIFICACAO.LIBERACAO_CONSULTA_BDP;
                    break;
            }

            // Se deve enviar a notificação na alteração da situação...
            if (!string.IsNullOrEmpty(tokenNotificacao))
            {
                // Busca os dados da publicação BDP para realizar o merge da notificação
                var dadosPublicacao = this.SearchProjectionByKey(seqPublicacaoBdp, p => new
                {
                    DescricaoTipoTrabalho = p.TrabalhoAcademico.TipoTrabalho.Descricao,
                    SeqInstituicaoEnsino = p.TrabalhoAcademico.SeqInstituicaoEnsino,
                    Aluno = p.TrabalhoAcademico.Autores.Select(a => new
                    {
                        Nome = a.NomeAutor,
                        Emails = a.Aluno.EnderecosEletronicos.Where(e => e.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email).Select(e => e.EnderecoEletronico.Descricao).ToList(),
                        SeqEntidadeVinculo = a.Aluno.Historicos.FirstOrDefault(h => h.Atual).SeqEntidadeVinculo,
                    }).FirstOrDefault(),
                    DataDefesa = p.TrabalhoAcademico.DivisoesComponente.Select(sd => sd.OrigemAvaliacao.AplicacoesAvaliacao
                                                 .Where(w => w.DataCancelamento == null && w.Avaliacao.TipoAvaliacao == TipoAvaliacao.Banca)
                                                 .OrderByDescending(o => o.Seq).Select(se => se.DataInicioAplicacaoAvaliacao).FirstOrDefault()).FirstOrDefault(),

                });

                // Selecionar a configuração da notificação cuja o tipo de notificação tem o token da situação
                // e esteja associada a entidade da instituição de ensino do aluno (ou do trabalho).
                long seqConfigNotificacao = EntidadeConfiguracaoNotificacaoDomainService.BuscarSeqConfiguracaoNotificacaoAtivo(dadosPublicacao.SeqInstituicaoEnsino, tokenNotificacao);

                // Se encontrou a configuração para o envio da notificação
                if (seqConfigNotificacao > 0)
                {
                    // Busca os dados da entidade para merge e/ou envio de notificação
                    var specEntidade = new SMCSeqSpecification<Entidade>(dadosPublicacao.Aluno.SeqEntidadeVinculo);

                    var dadosEntidade = EntidadeDomainService.SearchProjectionByKey(specEntidade, e => new
                    {
                        Nome = e.Nome,
                        Endereco = e.Enderecos.FirstOrDefault(d => d.TipoEndereco == TipoEndereco.Comercial),
                        Telefones = e.Telefones,
                        Emails = e.EnderecosEletronicos.Where(t => t.TipoEnderecoEletronico == TipoEnderecoEletronico.Email)
                    });

                    // Formata o endereço da entidade
                    string enderecoFormatado = string.Format("{0}, {1}{2} - {3}, {4} - {5}, CEP: {6}",
                        string.IsNullOrEmpty(dadosEntidade.Endereco.Logradouro) ? string.Empty : dadosEntidade.Endereco.Logradouro.Trim(),
                        string.IsNullOrEmpty(dadosEntidade.Endereco.Numero) ? string.Empty : dadosEntidade.Endereco.Numero.Trim(),
                        string.IsNullOrEmpty(dadosEntidade.Endereco.Complemento) ? string.Empty : string.Format(" / {0}", dadosEntidade.Endereco.Complemento.Trim()),
                        string.IsNullOrEmpty(dadosEntidade.Endereco.Bairro) ? string.Empty : dadosEntidade.Endereco.Bairro.Trim(),
                        string.IsNullOrEmpty(dadosEntidade.Endereco.NomeCidade) ? string.Empty : dadosEntidade.Endereco.NomeCidade.Trim(),
                        string.IsNullOrEmpty(dadosEntidade.Endereco.SiglaUf) ? string.Empty : dadosEntidade.Endereco.SiglaUf.Trim(),
                        string.IsNullOrEmpty(dadosEntidade.Endereco.Cep) ? string.Empty : dadosEntidade.Endereco.Cep.Trim());

                    // Formata o telefone da entidade
                    string telefoneFormatado = string.Empty;
                    if (dadosEntidade.Telefones != null)
                    {
                        // Buscar telefone comercial sem categoria associada,
                        var telComercialSemCategoria = dadosEntidade.Telefones.FirstOrDefault(t => t.TipoTelefone == TipoTelefone.Comercial && !t.CategoriaTelefone.HasValue);
                        if (telComercialSemCategoria != null)
                        {
                            telefoneFormatado = string.Format("({0}){1}",
                                                                telComercialSemCategoria.CodigoArea.ToString().Trim(),
                                                                telComercialSemCategoria.Numero.ToString().Trim());
                        }
                        // Se não existir, buscar comercial e categoria 'secretaria'
                        else
                        {
                            var telComercialSecretaria = dadosEntidade.Telefones.FirstOrDefault(t => t.TipoTelefone == TipoTelefone.Comercial && t.CategoriaTelefone == CategoriaTelefone.Secretaria);
                            if (telComercialSecretaria != null)
                            {
                                telefoneFormatado = string.Format("({0}){1}",
                                                                    telComercialSecretaria.CodigoArea.ToString().Trim(),
                                                                    telComercialSecretaria.Numero.ToString().Trim());
                            }
                        }
                    }

                    // Formata o endereço eletrônico da entidade
                    string emailFormatado = string.Empty;
                    if (dadosEntidade.Emails != null)
                    {
                        // Buscar e-mails sem categoria associada
                        var emailSemCategoria = dadosEntidade.Emails.FirstOrDefault(e => !e.CategoriaEnderecoEletronico.HasValue);
                        if (emailSemCategoria != null)
                        {
                            emailFormatado = emailSemCategoria.Descricao;
                        }
                        // Se não existir, buscar categoria 'secretaria'
                        else
                        {
                            var emailSecretaria = dadosEntidade.Emails.FirstOrDefault(e => e.CategoriaEnderecoEletronico == CategoriaEnderecoEletronico.Secretaria);
                            if (emailSecretaria != null)
                            {
                                emailFormatado = emailSecretaria.Descricao;
                            }
                        }
                    }

                    // Monta os dados para o merge (Obs.: Algumas notificações não utilizam todos as tags, mas como não tem prejuizo
                    // caso envie o valor para a tag, o mesmo está sendo enviado)
                    // Tags para substituir:
                    // {{NOM_PESSOA}} -> nome do aluno;
                    // {{DSC_TIPO_TRABALHO}} -> descrição do tipo do trabalho;
                    // {{DAT_DEFESA}} -> data de início da aplicação da avaliação no formato "dd/mm/yyyy";
                    // {{NOM_ENTIDADE}} ->Com o nome da entidade do grupo de programa relacionado ao curso do aluno.
                    // {{END_ENTIDADE}} ->Com o endereço comercial da entidade do grupo de programa relacionado ao curso do aluno. (Obs.: Retornar o primeiro endereço cadastrado, caso haja mais de um)
                    // {{TEL_ENTIDADE}} ->Com o telefone comercial da entidade do grupo de programa relacionado ao curso do aluno. (Obs.: Retornar o primeiro telefone cadastrado, caso haja mais de um)
                    // {{END_ELETRONICO_ENTIDADE}} ->Com o e-mail da entidade do grupo de programa relacionado ao curso do aluno. (Obs.: Retornar o primeiro e-mail cadastrado, caso haja mais de um)
                    Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
                    dadosMerge.Add("{{NOM_PESSOA}}", dadosPublicacao.Aluno.Nome);
                    dadosMerge.Add("{{DSC_TIPO_TRABALHO}}", dadosPublicacao.DescricaoTipoTrabalho);
                    dadosMerge.Add("{{DAT_DEFESA}}", dadosPublicacao.DataDefesa.ToString("dd/MM/yyyy"));
                    dadosMerge.Add("{{NOM_ENTIDADE}}", dadosEntidade.Nome);
                    dadosMerge.Add("{{END_ENTIDADE}}", enderecoFormatado);
                    dadosMerge.Add("{{TEL_ENTIDADE}}", telefoneFormatado);
                    dadosMerge.Add("{{END_ELETRONICO_ENTIDADE}}", emailFormatado);

                    // Monta o Data para o serviço de notificação
                    NotificacaoEmailData data = new NotificacaoEmailData()
                    {
                        SeqConfiguracaoNotificacao = seqConfigNotificacao,
                        DadosMerge = dadosMerge,
                        DataPrevistaEnvio = DateTime.Now,
                        PrioridadeEnvio = PrioridadeEnvio.QuandoPossivel
                    };

                    // Define o destinatário conforme a situação para alteração
                    switch (situacao)
                    {
                        case SituacaoTrabalhoAcademico.AguardandoCadastroAluno:
                            // Destinatario = email dos alunos do trabalho
                            data.Destinatarios = new List<NotificacaoEmailDestinatarioData>()
                            {
                                new NotificacaoEmailDestinatarioData() { EmailDestinatario = string.Join(";", dadosPublicacao.Aluno.Emails) }
                            };
                            break;

                        case SituacaoTrabalhoAcademico.AutorizadaLiberadaSecretaria:
                            // Se for o token AUTORIZACAO_PUBLICACAO_BDP 
                            // buscar e-mails sem categoria associada ou categoria 'secretaria'.
                            var destinatarios = dadosEntidade.Emails.Where(c => c.CategoriaEnderecoEletronico == null
                                                                             || c.CategoriaEnderecoEletronico != CategoriaEnderecoEletronico.Secretaria)
                                                                    .Select(d => d.Descricao).ToList();
                            data.Destinatarios = new List<NotificacaoEmailDestinatarioData>()
                            {
                                new NotificacaoEmailDestinatarioData() { EmailDestinatario = string.Join(";", destinatarios) }
                            };
                            break;

                        case SituacaoTrabalhoAcademico.LiberadaBiblioteca:
                            // Não é necessário configurar o destinatário, pois o envio é feito para os emails da
                            // biblioteca que estão configurados na configuração da notificação no GCN (notificação).
                            break;

                        case SituacaoTrabalhoAcademico.LiberadaConsulta:
                            // Destinatario = email dos alunos do trabalho
                            data.Destinatarios = new List<NotificacaoEmailDestinatarioData>()
                            {
                                new NotificacaoEmailDestinatarioData() { EmailDestinatario = string.Join(";", dadosPublicacao.Aluno.Emails) }
                            };
                            break;
                    }

                    // Chama o serviço de envio de notificação
                    var seqNotificacaoSecretaria = this.NotificacaoService.SalvarNotificacao(data);

                    // Buscar a lista de sequencial da notificação-email-destinatário enviada
                    var notificacaoSecretaria = this.NotificacaoService.BuscaNotificacaoEmailDestinatario(seqNotificacaoSecretaria);

                    seqNotificacaoEnviada = notificacaoSecretaria.FirstOrDefault()?.Seq;
                }
            }

            // Valida se já está nessa situação
            ValidarAlteracaoSituacao(seqPublicacaoBdp, situacao);

            // Atualiza o histórico de situação da publicação BDP
            var historico = new PublicacaoBdpHistoricoSituacao()
            {
                SeqPublicacaoBdp = seqPublicacaoBdp,
                SituacaoTrabalhoAcademico = situacao,
                SeqNotificacaoEmailDestinatario = seqNotificacaoEnviada
            };

            PublicacaoBdpHistoricoSituacaoDomainService.SaveEntity(historico);
        }

        /// <summary>
        /// Buscar publicações bdps do aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do Aluno</param>
        /// <returns>Lista das publicações bdps do aluno</returns>
        public List<PublicacaoBdpVO> BuscarPublicacoesBdpsAluno(long seqAluno)
        {
            var spec = new PublicacaoBdpFilterSpecification() { SeqAluno = seqAluno };
            var retorno = this.SearchProjectionBySpecification(spec, p => new PublicacaoBdpVO()
            {
                Seq = p.Seq,
                DescricaoTituloTrabalho = p.TrabalhoAcademico.Titulo,
                DescricaoTipoTrabalho = p.TrabalhoAcademico.TipoTrabalho.Descricao,
                UltimaSituacaoTrabalho = p.HistoricoSituacoes.OrderByDescending(o => o.DataInclusao).Select(s => s.SituacaoTrabalhoAcademico).FirstOrDefault(),
                TrabalhoAcademico = new TrabalhoAcademicoVO()
                {
                    SeqInstituicaoEnsino = p.TrabalhoAcademico.SeqInstituicaoEnsino,
                    SeqNivelEnsino = p.TrabalhoAcademico.SeqNivelEnsino
                },
                InformacoesIdioma = p.InformacoesIdioma.Select(i => new PublicacaoBdpIdiomaVO()
                {
                    Seq = i.Seq,
                    IdiomaTrabalho = i.IdiomaTrabalho,
                    Titulo = i.Titulo,
                }).ToList()
            }).ToList();

            foreach (var item in retorno)
            {
                // Busca as informações da instituição/nível do trabalho
                var instituicaoNivel = this.InstituicaoNivelDomainService.BuscarInstituicaoNivelEnsino(item.TrabalhoAcademico.SeqNivelEnsino.Value, item.TrabalhoAcademico.SeqInstituicaoEnsino);
                item.SeqInstituicaoNivel = instituicaoNivel.Seq;
                item.NomeReduzidaInstituicao = instituicaoNivel.InstituicaoEnsino.NomeReduzido;

                // Verifica se o título do trabalho está em outro idioma
                if (item.InformacoesIdioma.Count() > 0)
                {
                    item.DescricaoTituloTrabalho = item.InformacoesIdioma.Where(i => i.IdiomaTrabalho).FirstOrDefault().Titulo;
                }
            }

            return retorno;
        }

        /// <summary>
        /// Buscar publicação Bdp
        /// </summary>
        /// <param name="seq">Sequencial do publicação bdp</param>
        /// <returns>Publicação Bdp/returns>
        public PublicacaoBdpVO BuscarPublicacaoBdp(long seq)
        {
            // Busca as informações da publicação do BDP
            var spec = new SMCSeqSpecification<PublicacaoBdp>(seq);
            var retorno = this.SearchProjectionByKey(spec, p => new PublicacaoBdpVO()
            {
                // Dados da publicação
                Seq = p.Seq,
                DataPublicacao = p.DataPublicacao,
                QuantidadeVolumes = p.QuantidadeVolumes,
                QuantidadePaginas = p.QuantidadePaginas,
                CodigoAcervo = p.CodigoAcervo,
                DataInclusao = p.DataInclusao,
                Arquivos = p.Arquivos.Select(a => new PublicacaoBdpArquivoVO()
                {
                    // Dados do arquivo
                    Seq = a.Seq,
                    TipoAutorizacao = a.TipoAutorizacao,
                    NomeArquivo = a.NomeArquivo,
                    TamanhoArquivo = a.TamanhoArquivo,
                    UrlArquivo = a.UrlArquivo,
                    Arquivo = new SMCUploadFile
                    {
                        GuidFile = p.SeqTrabalhoAcademico.ToString(),
                        Name = a.NomeArquivo,
                        Size = a.TamanhoArquivo ?? 0
                    }
                }).ToList(),
                // Dados do historico de situação
                HistoricoSituacoes = p.HistoricoSituacoes.Select(hs => new PublicacaoBdpHistoricoSituacaoVO()
                {
                    Seq = hs.Seq,
                    SeqPublicacaoBdp = hs.SeqPublicacaoBdp,
                    SituacaoTrabalhoAcademico = hs.SituacaoTrabalhoAcademico,
                    SeqNotificacaoEmailDestinatario = hs.SeqNotificacaoEmailDestinatario,
                    DataInclusao = hs.DataInclusao
                }).ToList(),
                // Dados do trabalho acadêmico
                SeqTrabalhoAcademico = p.SeqTrabalhoAcademico,
                DescricaoTipoTrabalho = p.TrabalhoAcademico.TipoTrabalho.Descricao,
                TrabalhoAcademico = new TrabalhoAcademicoVO()
                {
                    Seq = p.TrabalhoAcademico.Seq,
                    Titulo = p.TrabalhoAcademico.Titulo,
                    DataDepositoSecretaria = p.TrabalhoAcademico.DataDepositoSecretaria,
                    SeqNivelEnsino = p.TrabalhoAcademico.SeqNivelEnsino,
                    SeqInstituicaoEnsino = p.TrabalhoAcademico.SeqInstituicaoEnsino,
                    NumeroDiasDuracaoAutorizacaoParcial = p.TrabalhoAcademico.NumeroDiasDuracaoAutorizacaoParcial,
                    Autores = p.TrabalhoAcademico.Autores.Select(a => new TrabalhoAcademicoAutoriaVO()
                    {
                        EmailAutor = a.EmailAutor,
                        NomeAutor = a.NomeAutor,
                        NomeAutorFormatado = a.NomeAutorFormatado,
                        SeqAluno = a.SeqAluno,
                        SeqTrabalhoAcademicoAutoria = a.Seq
                    }).ToList(),
                    MembrosBancaExaminadora = p.TrabalhoAcademico.DivisoesComponente.SelectMany(sd => sd.OrigemAvaliacao.AplicacoesAvaliacao
                                                                                   .OrderByDescending(o => o.Seq)
                                                                                   .FirstOrDefault(w => w.DataCancelamento == null && w.Avaliacao.TipoAvaliacao == TipoAvaliacao.Banca)
                                                                                   .MembrosBancaExaminadora.Where(wm => wm.Participou == true).OrderBy(om => om.NomeColaborador).Select(sm => new BancaExaminadoraVO()
                                                                                   {
                                                                                       Nome = sm.SeqColaborador.HasValue ? sm.Colaborador.DadosPessoais.Nome : sm.NomeColaborador,
                                                                                       Instituicao = sm.SeqInstituicaoExterna.HasValue ? sm.InstituicaoExterna.Nome : sm.NomeInstituicaoExterna,
                                                                                       ComplementoInstituicao = sm.ComplementoInstituicao,
                                                                                       TipoMembroBanca = sm.TipoMembroBanca
                                                                                   })).OrderBy(o => o.Nome).ToList()
                },
                // Dados de idioma
                InformacoesIdioma = p.InformacoesIdioma.Select(i => new PublicacaoBdpIdiomaVO()
                {
                    Seq = i.Seq,
                    Idioma = (Linguagem)i.Idioma,
                    Titulo = i.Titulo,
                    Resumo = i.Resumo,
                    IdiomaTrabalho = i.IdiomaTrabalho,
                    PalavrasChave = i.PalavrasChave.Select(pc => new PublicacaoBdpPalavraChaveVO()
                    {
                        Seq = pc.Seq,
                        PalavraChave = pc.PalavraChave
                    }).ToList()
                }).ToList(),
                // Dados da defesa
                DataDefesa = p.TrabalhoAcademico.DivisoesComponente.Select(sd => sd.OrigemAvaliacao.AplicacoesAvaliacao
                                                                    .Where(w => w.DataCancelamento == null && w.Avaliacao.TipoAvaliacao == TipoAvaliacao.Banca)
                                                                    .OrderByDescending(o => o.Seq).Select(se => se.DataInicioAplicacaoAvaliacao).FirstOrDefault()).FirstOrDefault(),
            });

            // Busca os dados da autorização
            var autorização = PublicacaoBdpAutorizacaoDomainService.BuscarDadosAutorizacaoAtiva(seq);
            if (autorização != null)
            {
                retorno.DataAutorizacao = autorização.DataAutorizacao;
                retorno.TipoAutorizacao = autorização.TipoAutorizacao;
                retorno.CodigoAutorizacao = autorização.CodigoAutorizacao;
            }

            // Busca a instituição e nível de ensino da publicação BDP
            retorno.SeqInstituicaoNivel = this.InstituicaoNivelDomainService.BuscarSequencialInstituicaoNivelEnsino(retorno.TrabalhoAcademico.SeqNivelEnsino.Value, retorno.TrabalhoAcademico.SeqInstituicaoEnsino);

            // Seleciona a ultima situação da publicação bdp
            retorno.UltimaSituacaoTrabalho = retorno.HistoricoSituacoes.OrderByDescending(h => h.DataInclusao).Select(s => s.SituacaoTrabalhoAcademico).FirstOrDefault();

            // Faz ajustes necessários nos membros da banca
            foreach (var membro in retorno.TrabalhoAcademico.MembrosBancaExaminadora)
            {
                // Quando o tipo de membro for “Orientador” ou “Coorientador”, concatenar esta informação juntamente com o nome.
                // Conforme exemplo: Eliane Aparecida Notato (Orientador)
                if (membro.TipoMembroBanca == TipoMembroBanca.Orientador || membro.TipoMembroBanca == TipoMembroBanca.Coorientador)
                {
                    membro.Nome = $"{membro.Nome} ({SMCEnumHelper.GetDescription(membro.TipoMembroBanca)})";
                }

                // Se o complemento de instituição tiver registrado: Instituição - Complemento
                if (!string.IsNullOrEmpty(membro.ComplementoInstituicao))
                {
                    membro.Instituicao = $"{membro.Instituicao} - {membro.ComplementoInstituicao}";
                }
            }

            // Monta o arquivo anexado
            //if (!string.IsNullOrEmpty(retorno.NomeArquivo))
            //{
            //    retorno.Arquivo = new SMCUploadFile
            //    {
            //        GuidFile = retorno.SeqTrabalhoAcademico.ToString(),
            //        Name = retorno.NomeArquivo,
            //        Size = retorno.TamanhoArquivo ?? 0
            //    };
            //}

            // Ordena os idiomas para apresentação
            // Primeiro traz o idioma do trabalho, depois ordenação alfabética pelo no nome do idioma
            retorno.InformacoesIdioma = retorno.InformacoesIdioma.OrderByDescending(i => i.IdiomaTrabalho).ThenBy(i => i.Idioma.SMCGetDescription()).ToList();

            return retorno;
        }

        /// <summary>
        /// Retorna os dados da ficha catalografica do aluno
        /// </summary>
        /// <param name="seqPublicacaoBdp">Seq da publicação</param>
        /// <returns>Dados da ficha catalografica encontrada</returns>
        public FichaCatalograficaVO BuscarDadosImpressaoFichaCatalografica(long seqPublicacaoBdp)
        {
            // Busca as informações da publicação do BDP
            var spec = new SMCSeqSpecification<PublicacaoBdp>(seqPublicacaoBdp);
            var dados = this.SearchProjectionByKey(spec, p => new
            {
                // Dados da publicação
                SeqPublicacao = p.Seq,
                DescricaoTipoTrabalho = p.TrabalhoAcademico.TipoTrabalho.Descricao,
                AnoDefesa = p.TrabalhoAcademico.DivisoesComponente.Select(sd => sd.OrigemAvaliacao.AplicacoesAvaliacao
                                                      .Where(w => w.DataCancelamento == null && w.Avaliacao.TipoAvaliacao == TipoAvaliacao.Banca)
                                                      .OrderByDescending(o => o.Seq).Select(se => se.DataInicioAplicacaoAvaliacao).FirstOrDefault()).FirstOrDefault(),
                NumeroPaginas = p.QuantidadePaginas,
                Seq = p.TrabalhoAcademico.Seq,
                SeqNivelEnsino = p.TrabalhoAcademico.SeqNivelEnsino,
                DescricaoNivelEnsino = p.TrabalhoAcademico.NivelEnsino.Descricao,
                SeqInstituicaoEnsino = p.TrabalhoAcademico.SeqInstituicaoEnsino,
                DescricaoInstituicaoEnsino = p.TrabalhoAcademico.InstituicaoEnsino.Nome,
                Autores = p.TrabalhoAcademico.Autores.Select(a => new
                {
                    SeqAluno = a.SeqAluno,
                    NomeAluno = a.NomeAutor,
                    NomeAlunoABNT = a.NomeAutorFormatado,
                    // Nome da entidade de vinculo do aluno autor do trabalho
                    GrupoPrograma = a.Aluno.Historicos.FirstOrDefault(h => h.Atual).EntidadeVinculo.Nome,
                    Cidade = a.Aluno.Historicos
                            .FirstOrDefault(o => o.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Enderecos
                            .FirstOrDefault(b => b.TipoEndereco == TipoEndereco.Comercial).NomeCidade
                }).ToList(),
                MembrosBancaExaminadora = p.TrabalhoAcademico.DivisoesComponente.SelectMany(sd => sd.OrigemAvaliacao.AplicacoesAvaliacao
                                            .OrderByDescending(o => o.Seq)
                                            .FirstOrDefault(w => w.DataCancelamento == null && w.Avaliacao.TipoAvaliacao == TipoAvaliacao.Banca)
                                            .MembrosBancaExaminadora
                                            .Where(wm => wm.Participou == true)
                                            .OrderBy(om => om.NomeColaborador)
                                            .Select(sm => new BancaExaminadoraVO()
                                            {
                                                Nome = sm.SeqColaborador.HasValue ? sm.Colaborador.DadosPessoais.Nome : sm.NomeColaborador,
                                                Instituicao = sm.SeqInstituicaoExterna.HasValue ? sm.InstituicaoExterna.Nome : sm.NomeInstituicaoExterna,
                                                ComplementoInstituicao = sm.ComplementoInstituicao,
                                                TipoMembroBanca = sm.TipoMembroBanca
                                            })).OrderBy(o => o.Nome).ToList()
            });

            var orientador = dados.MembrosBancaExaminadora
                               .Where(x => x.TipoMembroBanca == TipoMembroBanca.Orientador)
                               .Select(y => y.Nome)
                               .FirstOrDefault();

            var coOrientador = dados.MembrosBancaExaminadora
                            .Where(x => x.TipoMembroBanca == TipoMembroBanca.Coorientador)
                            .Select(y => y.Nome)
                            .FirstOrDefault();

            var orientadorABNT = SMCABNTHelper.FormataNomeABNT(orientador);

            FichaCatalograficaVO retorno = new FichaCatalograficaVO()
            {
                NomeAlunoABNT = dados.Autores.FirstOrDefault().NomeAlunoABNT,
                NomeAluno = dados.Autores.FirstOrDefault().NomeAluno,
                Cidade = dados.Autores.FirstOrDefault().Cidade,
                AnoDefesa = dados.AnoDefesa.Year.ToString(),
                NumeroPaginas = dados.NumeroPaginas,
                Orientador = string.IsNullOrEmpty(orientador) ? " " : $"Orientador(a): {orientador}",
                Coorientador = string.IsNullOrEmpty(coOrientador) ? " " : $"Coorientador(a): {coOrientador}",
                TipoTrabalho = dados.DescricaoTipoTrabalho,
                NivelEnsino = dados.DescricaoNivelEnsino.Replace("Acadêmico", string.Empty).Replace("Profissional", string.Empty).Trim(),
                InstituicaoEnsino = dados.DescricaoInstituicaoEnsino,
                GrupoPrograma = dados.Autores.FirstOrDefault().GrupoPrograma,
                NomeOrientadorABNT = orientadorABNT
            };

            return retorno;
        }

        /// <summary>
        /// Salvar a publicacao bdp
        /// </summary>
        /// <param name="model">Dados a serem salvos</param>
        /// <returns>Sequencial da publicação BDP salva</returns>
        public long SalvarPublicacaoBdp(PublicacaoBdpVO model)
        {
            // RN_ORT_004 -Consistir quantidade de informações por Idioma
            // É necessário que as informações do trabalho sejam cadastradas em pelo menos 2 idiomas.
            // Sendo o português obrigatório.
            if (!(model?.InformacoesIdioma?.Any(x => x.Idioma == Linguagem.Portuguese)).GetValueOrDefault())
                throw new PublicacaoBdpSemIdiomaPortuguesException();

            // É necessário que um e somente um dos idiomas esteja marcado como o idioma do trabalho
            if (model?.InformacoesIdioma?.Where(x => x.IdiomaTrabalho).ToList().Count() != 1)
                throw new PublicacaoBdpIdiomaTrabalhoException();

            // Busca os dados do trabalho
            var trabalhoAcademico = this.TrabalhoAcademicoDomainService.SearchByKey(new SMCSeqSpecification<TrabalhoAcademico>(model.SeqTrabalhoAcademico), IncludesTrabalhoAcademico.Autores);

            // Validar tipos de autorizacoes de acordo com o programa do aluno
            VerificarTipoAutorizacaoArquivos(model, trabalhoAcademico);

            // Busca a ultima situação da publicação
            // Se não for "Aguardando Cadastro do Aluno" ou "Cadastrada pelo aluno" não deixa alterar
            var spec = new PublicacaoBdpHistoricoSituacaoFilterSpecification() { SeqPublicacaoBdp = model.Seq };
            spec.SetOrderByDescending(o => o.Seq);
            var situacaoAtual = this.PublicacaoBdpHistoricoSituacaoDomainService.SearchProjectionBySpecification(spec, p => p.SituacaoTrabalhoAcademico).FirstOrDefault();
            if (situacaoAtual != SituacaoTrabalhoAcademico.AguardandoCadastroAluno && situacaoAtual != SituacaoTrabalhoAcademico.CadastradaAluno)
                throw new PublicacaoBdpSituacaoAcademicaInvalidaException();

            // Inicia a transação
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {

                // Atualiza o e-mail dos autores do trabalho
                foreach (var item in trabalhoAcademico.Autores)
                {
                    foreach (var autor in model.TrabalhoAcademico.Autores)
                    {
                        if (autor.SeqAluno == item.SeqAluno)
                        {
                            item.EmailAutor = autor.EmailAutor;
                        }
                    }
                }

                //Ao salvar as informações por idioma, registrar o titulo do trabalho com o mesmo título informado para o idioma “Português”.
                //trabalhoAcademico.Titulo = model.InformacoesIdioma.FirstOrDefault(f => f.Idioma == Linguagem.Portuguese).Titulo;

                this.TrabalhoAcademicoDomainService.SaveEntity(trabalhoAcademico);

                // Prepara a publicação BDP para salvar as alterações
                var publicacao = model.Transform<PublicacaoBdp>();

                // Se está atualizando a publicação, verifica se já tem codigo de acervo e atualiza o modelo sendo salvo
                if (publicacao.Seq > 0)
                {
                    var specPubBD = new SMCSeqSpecification<PublicacaoBdp>(publicacao.Seq);
                    publicacao.CodigoAcervo = this.SearchProjectionByKey(specPubBD, x => x.CodigoAcervo);
                }

                // Se a situação atual da publicação for "Aguardando Cadastro do aluno" altera para "Cadastrada pelo Aluno"
                if (situacaoAtual == SituacaoTrabalhoAcademico.AguardandoCadastroAluno)
                {
                    PublicacaoBdpHistoricoSituacao historico = new PublicacaoBdpHistoricoSituacao()
                    {
                        SeqPublicacaoBdp = model.Seq,
                        SituacaoTrabalhoAcademico = SituacaoTrabalhoAcademico.CadastradaAluno
                    };
                    publicacao.HistoricoSituacoes = publicacao.HistoricoSituacoes ?? new List<PublicacaoBdpHistoricoSituacao>();
                    publicacao.HistoricoSituacoes.Add(historico);
                }

                // Depois de salvar alterações nos autores mantem o estado na publicação
                publicacao.TrabalhoAcademico = null;

                if (model.Arquivos != null && model.Arquivos.Count > 0)
                {
                    foreach (var item in model.Arquivos)
                    {
                        if (!string.IsNullOrEmpty(item.UrlArquivo) && item.Arquivo.FileData == null)
                        {
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                            using (WebClient client = new WebClient())
                            {
                                item.Arquivo.FileData = client.DownloadData(item.UrlArquivo);
                                item.Arquivo.State = SMCUploadFileState.Changed;
                            }
                        }
                    }

                    foreach (var file in model.Arquivos)
                    {
                        // Se enviar arquivo e feito o tratamento
                        if (file.Arquivo != null && file.Arquivo.FileData != null && (file.Seq == 0 || file.Arquivo.State == SMCUploadFileState.Changed))
                        {
                            // Busca os dados o autor
                            var autor = trabalhoAcademico.Autores.Select(x => new
                            {
                                Nome = x.NomeAutor,
                                SeqAluno = x.SeqAluno
                            }).FirstOrDefault();

                            // Salva o arquivo no diretório do BDP
                            var arquivoSalvo = SalvarArquivoPublicacaoBDP(file.Arquivo.FileData, trabalhoAcademico.Seq, autor.Nome, autor.SeqAluno, file.TipoAutorizacao);

                            publicacao.Arquivos.SMCForEach(f =>
                            {
                                if (f.TipoAutorizacao == file.TipoAutorizacao)
                                {
                                    // Atualiza as informações do arquivo salvo
                                    f.Seq = file.Seq;
                                    f.TipoAutorizacao = file.TipoAutorizacao;
                                    f.NomeArquivo = arquivoSalvo.NomeArquivo;
                                    f.TamanhoArquivo = file.Arquivo.Size;
                                    f.UrlArquivo = arquivoSalvo.UrlArquivo;
                                }
                            });
                        }
                    }
                }

                // Salva a publicação
                this.SaveEntity(publicacao);

                unitOfWork.Commit();
            }

            return model.Seq;
        }
        /// <summary>
        /// Obrigar que todos os arquivos sejam anexados conforme os tipos de autorização que estão parametrizados para o programa do curso do aluno
        /// </summary>
        /// <param name="model"></param>
        private void VerificarTipoAutorizacaoArquivos(PublicacaoBdpVO model, TrabalhoAcademico trabalhoAcademico)
        {
            // Validar se possui publicacao texto completo no modelo enviado pra salvar
            if (model.Arquivos.Count() == 0 || !model.Arquivos.Any(a => a.TipoAutorizacao == TipoAutorizacao.TextoCompleto))
                throw new PublicacaoBdpTipoAutorizacaoObrigatorioException(TipoAutorizacao.TextoCompleto);

            // Validar se possui necessidade de anexar arquivo parcial
            var seqAluno = model.TrabalhoAcademico.Autores.Select(x => x.SeqAluno).FirstOrDefault();

            // Obtem lista do tipo de autorização do aluno
            List<TipoAutorizacao> tiposAutorizacaoPrograma = BuscarTiposAutorizacaoPrograma(seqAluno);

            // Verifica se existe tipo de autorizacao parcial informado no programa do aluno
            bool existeTipoAutorizacaoParcialPrograma = tiposAutorizacaoPrograma.Contains(TipoAutorizacao.Parcial);

            // Verifica se existe dias de autorizacao parcial parametrizado
            bool existeDiasAutorizacaoParcial = (trabalhoAcademico.NumeroDiasDuracaoAutorizacaoParcial > 0);

            // Verifica se existe arquivo do tipo parcial anexado no modelo
            bool arquivoTipoParcialAnexado = model.Arquivos.Any(x => x.TipoAutorizacao == TipoAutorizacao.Parcial);

            // Caso o trabalho possua o numero de dias de autorização parcial informado e maior que zero,
            // o arquivo do tipo parcial deve ser obrigatoriamente anexado
            if ((existeDiasAutorizacaoParcial || existeTipoAutorizacaoParcialPrograma) && !arquivoTipoParcialAnexado)
            {
                throw new PublicacaoBdpTipoAutorizacaoObrigatorioException(TipoAutorizacao.Parcial);
            }


        }
        /// <summary>
        /// Retorna lista dos tipos de autorização do programa do aluno
        /// </summary>
        /// <param name="seqAluno">identificado do aluno</param>
        /// <returns>lista dos tipos de autorização</returns>
        public List<TipoAutorizacao> BuscarTiposAutorizacaoPrograma(long seqAluno)
        {
            // Busca o Seq do Programa pelo Seq do Aluno
            var seqPrograma = ProgramaDomainService.BuscarProgramaPorAluno(seqAluno);

            // Busca o programa
            var programa = ProgramaDomainService.BuscarPrograma(seqPrograma);

            // Tipos de autorização que estao parametrizados para o programa do curso do aluno
            var tiposAutorizacaoPrograma = programa.TiposAutorizacaoBdp.Select(x => x.TipoAutorizacao).ToList();
            return tiposAutorizacaoPrograma;
        }

        private void ValidarAlteracaoSituacao(long seq, SituacaoTrabalhoAcademico situacao)
        {
            // Verifica se a situação atual do histórico é a mesma que estou tentando atualizar
            var situacaoAtual = (SituacaoTrabalhoAcademico?)this.SearchProjectionByKey(seq, x => x.HistoricoSituacoes.OrderByDescending(h => h.DataInclusao).FirstOrDefault().SituacaoTrabalhoAcademico);
            if (situacaoAtual == situacao)
            {
                if (situacao == SituacaoTrabalhoAcademico.AutorizadaLiberadaSecretaria)
                    throw new AutorizacaoJaRealizadaException();
                else
                    throw new AlterarSituacaoPublicacaoException(SMCEnumHelper.GetDescription(situacao));
            }
        }

        /// <summary>
        /// Auutorizar publicacao do bdp (RN_ORT_022 - Registrar Autorização de Publicação no BDP)
        /// </summary>
        /// <param name="model">Dados a serem salvos</param>
        public void AutorizarPublicacaoBdp(PublicacaoBdpVO model)
        {
            // Valida se já está nessa situação
            ValidarAlteracaoSituacao(model.Seq, SituacaoTrabalhoAcademico.AutorizadaLiberadaSecretaria);

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    // Recupera a publicação
                    var dadosPublicacao = this.SearchProjectionByKey(model.Seq, p => new
                    {
                        TrabalhoAcademico = p.TrabalhoAcademico,
                        Aluno = p.TrabalhoAcademico.Autores.Select(a => new
                        {
                            SeqPessoa = a.Aluno.SeqPessoa,
                            Emails = a.Aluno.EnderecosEletronicos.Where(e => e.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email).Select(e => e.EnderecoEletronico.Descricao).ToList(),
                            SeqEntidadeVinculo = a.Aluno.Historicos.FirstOrDefault(h => h.Atual).SeqEntidadeVinculo,
                            SeqPessoaAtuacao = a.SeqAluno,
                            CodigoAlunoMigracao = a.Aluno.CodigoAlunoMigracao,
                            SeqNivelEnsino = a.Aluno.Historicos.FirstOrDefault(h => h.Atual).SeqNivelEnsino,
                            DscNivelEnsino = a.Aluno.Historicos.FirstOrDefault(h => h.Atual).NivelEnsino.Descricao,
                            SeqCursoOferta = a.Aluno.Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.SeqCursoOferta
                        }).FirstOrDefault()
                    });

                    // 2. Verificar se o aluno possui algum registro de amostra em questionário do PPA do tipo 
                    // "Concluinte". Caso não seja ainda amostra, criar conforme os passos abaixo:
                    var specAmostra = new PessoaAtuacaoAmostraPpaFilterSpecification()
                    {
                        SeqPessoaAtuacao = dadosPublicacao.Aluno.SeqPessoaAtuacao,
                        TipoAvaliacaoPpa = TipoAvaliacaoPpa.Concluinte
                    };
                    var qtdAmostra = PessoaAtuacaoAmostraPpaDomainService.Count(specAmostra);
                    if (qtdAmostra == 0)
                    {
                        // Busca a configuração de avaliação de instrumento PPA para o aluno
                        var config = ConfiguracaoAvaliacaoPpaDomainService.BuscarConfiguracaoValida(
                                dadosPublicacao.Aluno.SeqEntidadeVinculo,
                                dadosPublicacao.Aluno.SeqNivelEnsino,
                                TipoAvaliacaoPpa.Concluinte);

                        // Se encontrou a configuração do ppa, cria a amostra
                        if (config != null)
                        {
                            // Confirma se a configuração possui CodigoOrigemPpa e CodigoInstrumentoPpa informados
                            if (!config.CodigoOrigemPpa.HasValue || !config.CodigoInstrumentoPpa.HasValue)
                                throw new ConfiguracaoAvaliacaoPPAConcluinteInvalidaException();

                            /* 2.2) Caso encontre configuração de avaliação PPA, chamar a rotina st_gera_amostra do banco PPA
                                com os parâmetros listados abaixo conforme registro encontrado na configuração_avaliação_PPA
                            */
                            var gerarAmostraData = new GerarAmostraData
                            {
                                CodigoOrigem = config.CodigoOrigemPpa.Value,
                                CodigoInstrumento = config.CodigoInstrumentoPpa.Value,
                                CodigoPessoa = PessoaDomainService.BuscarCodigoDePessoaNosDadosMestres(dadosPublicacao.Aluno.SeqPessoa, TipoPessoa.Fisica, null),
                                CodigoAluno = dadosPublicacao.Aluno.CodigoAlunoMigracao.Value,
                                UsuarioLogado = SMCContext.User.Identity.Name,
                                CodigoCurso = (int)dadosPublicacao.Aluno.SeqEntidadeVinculo,
                                CodigoPublicoExterno = dadosPublicacao.Aluno.SeqNivelEnsino.ToString(),
                                NomePublicoExterno = dadosPublicacao.Aluno.DscNivelEnsino,
                                CodigoUnidadeCursoTurma = (int)dadosPublicacao.Aluno.SeqCursoOferta
                            };
                            var codigoAmostraPpa = AmostraService.GerarAmostra(gerarAmostraData);
                            if (codigoAmostraPpa <= 0)
                                throw new CriacaoAmostraPPAException();

                            /* 2.4) Gravar um registro na tabela "PES.pessoa_atuacao_amostra_ppa" conforme descrição dos
                                atributos abaixo:
                                - seq_pessoa_atuacao = Sequencial do aluno;
                                - cod_amostra_ppa = Código de amostra retornado no objeto mencionado · no item 2.2;
                                - seq_configuracao_avaliacao_cpa = sequencial da configuração_avaliacao_ppa encontrado no item 2.1;
                                - dat_preenchimento = null;
                                - seq_notificacao_email_destinatario = Sequencial retornado no envio de notificação realizado no
                                item 2.3, caso enviado. Se não enviou a notificação registrar o valor igual a null;
                            */
                            // Cria a pessoa atuação amostra PPA para salvar (sem a referencia da notificação)
                            var pessoaAtuacaoAmostraPpa = new PessoaAtuacaoAmostraPpa()
                            {
                                SeqPessoaAtuacao = dadosPublicacao.Aluno.SeqPessoaAtuacao,
                                CodigoAmostraPpa = codigoAmostraPpa,
                                SeqConfiguracaoAvaliacaoPpa = config.Seq
                            };

                            /* 2.3) Selecionar a configuração da notificação cuja o tipo de notificação tem o token
                                "CONVITE_AVALIACAO_PROGRAMA" e esteja associada a entidade resposnável do aluno. Se
                                encontrar a configuração, enviar uma notificação para todos os e-mails do aluno em questão com a
                                configuração selecionada;
                            */
                            // Buscar dados da entidade de configuração notificação
                            var seqConfigNotificacaoConvite = EntidadeConfiguracaoNotificacaoDomainService.BuscarSeqConfiguracaoNotificacaoAtivo(
                                dadosPublicacao.Aluno.SeqEntidadeVinculo,
                                TOKEN_TIPO_NOTIFICACAO.CONVITE_AVALIACAO_PROGRAMA);

                            // Caso tenha configuração de notificação, prosseguir com o envio
                            if (seqConfigNotificacaoConvite > 0)
                            {
                                // Monta o Data para o serviço de notificação
                                NotificacaoEmailData data = new NotificacaoEmailData()
                                {
                                    SeqConfiguracaoNotificacao = seqConfigNotificacaoConvite,
                                    DataPrevistaEnvio = DateTime.Now,
                                    PrioridadeEnvio = PrioridadeEnvio.QuandoPossivel,
                                    Destinatarios = new List<NotificacaoEmailDestinatarioData>() {
                                    new NotificacaoEmailDestinatarioData() { EmailDestinatario = string.Join(";", dadosPublicacao.Aluno.Emails) }
                                }
                                };

                                // Chama o serviço de envio de notificação
                                var seqNotificacaoEnviada = this.NotificacaoService.SalvarNotificacao(data);

                                // Buscar a lista de sequencial da notificação-email-destinatário enviada
                                var notificacoesEmailsDestinatarios = this.NotificacaoService.BuscaNotificacaoEmailDestinatario(seqNotificacaoEnviada);

                                // Para cada notificação enviada, salva um registro na tabela
                                foreach (var notificacaoEmailDestinatario in notificacoesEmailsDestinatarios)
                                {
                                    // Atualiza a pessoa atuação amostra PPA com o sequencial da notificação enviada
                                    pessoaAtuacaoAmostraPpa.SeqNotificacaoEmailDestinatario = notificacaoEmailDestinatario.Seq;
                                }
                            }

                            // Salva a pessoa atuação amostra PPA (com ou sem a notificação enviada)
                            PessoaAtuacaoAmostraPpaDomainService.SaveEntity(pessoaAtuacaoAmostraPpa);
                        }
                    }

                    // Atualiza os dados da autorização da publicação BDP
                    var diasAutorizacao = dadosPublicacao.TrabalhoAcademico.NumeroDiasDuracaoAutorizacaoParcial;

                    // UC_ORT_003_01
                    // Quando o trabalho estiver parametrizado para ter autorização de publicação do texto parcial
                    // (num_dias_autorizacao_parcial > 0) significa que este trabalho terá a publicação do texto parcial
                    // e texto completo

                    List<PublicacaoBdpAutorizacao> listaPublicacaoAutorizada = new List<PublicacaoBdpAutorizacao>();

                    // Configura a lista de configuração de publicação, podendo conter só 1 elemento dependendo do caso
                    CriarListaAutorizacaPublicacao(model.Seq, diasAutorizacao, listaPublicacaoAutorizada);

                    // Salva a autorização de publicação
                    foreach (var publicacao in listaPublicacaoAutorizada)
                    {
                        PublicacaoBdpAutorizacaoDomainService.SaveEntity(publicacao);
                    }

                    // Altera a situação da publicação BDP enviando notificação
                    // 1. Inserir a situação "Autorizado e Liberado para a Secretaria" como atual para o trabalho em questão.
                    // 3. Enviar notificação de acordo com o processo descrito pela regra RN_ORT_012 - Enviar Notificação
                    // Processo Publicação BDP cujo token é "AUTORIZACAO_PUBLICACAO_BDP".
                    AlterarSituacao(model.Seq, SituacaoTrabalhoAcademico.AutorizadaLiberadaSecretaria);

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Adiciona  a parametrização da configuração de autorização para a publicação na lista informada
        /// </summary>
        /// <param name="seqPublicacao">Seq da publicação</param>
        /// <param name="diasAutorizacao">Dias de autorização se houver</param>
        /// <param name="lista">Lista para retornar configurada</param>
        private void CriarListaAutorizacaPublicacao(long seqPublicacao, short? diasAutorizacao, List<PublicacaoBdpAutorizacao> lista)
        {
            var codAutorizacao = Guid.NewGuid();
            var dataAtual = DateTime.Now.Date;

            if (diasAutorizacao > 0)
            {

                PublicacaoBdpAutorizacao publicacaoParcial = new PublicacaoBdpAutorizacao()
                {
                    SeqPublicacaoBdp = seqPublicacao,
                    DataAutorizacao = dataAtual,
                    TipoAutorizacao = TipoAutorizacao.Parcial,
                    CodigoAutorizacao = codAutorizacao
                };

                lista.Add(publicacaoParcial);

                PublicacaoBdpAutorizacao publicacaoTextoCompleto = new PublicacaoBdpAutorizacao()
                {
                    SeqPublicacaoBdp = seqPublicacao,
                    DataAutorizacao = dataAtual.AddDays(diasAutorizacao.Value),
                    TipoAutorizacao = TipoAutorizacao.TextoCompleto,
                    CodigoAutorizacao = codAutorizacao
                };

                lista.Add(publicacaoTextoCompleto);
            }
            else
            {
                PublicacaoBdpAutorizacao publicacao = new PublicacaoBdpAutorizacao()
                {
                    SeqPublicacaoBdp = seqPublicacao,
                    DataAutorizacao = dataAtual,
                    TipoAutorizacao = TipoAutorizacao.TextoCompleto,
                    CodigoAutorizacao = codAutorizacao
                };

                lista.Add(publicacao);
            }
        }

        /// <summary>
        /// Dados para exibir autorização da publicação bdp
        /// </summary>
        /// <param name="seq">Sequencial da publicação bdp</param>
        /// <returns>Dados do aluno</returns>
        public PublicacaoBdpAutorizacaoVO DadosAutorizacaoPublicacaoBdp(long seq)
        {
            // Busca os dados da publicação BDP
            var dados = this.SearchProjectionByKey(new SMCSeqSpecification<PublicacaoBdp>(seq), p => new
            {
                Seq = p.Seq,
                TipoTrabalho = p.TrabalhoAcademico.TipoTrabalho.Descricao,
                NivelEnsino = p.TrabalhoAcademico.NivelEnsino.Descricao,
                TituloTrabalho = p.InformacoesIdioma.FirstOrDefault(w => w.IdiomaTrabalho).Titulo,
                NumeroDiasDuracaoAutorizacaoParcial = p.TrabalhoAcademico.NumeroDiasDuracaoAutorizacaoParcial,
                Autor = p.TrabalhoAcademico.Autores.Select(a => new
                {
                    Nome = a.Aluno.DadosPessoais.Nome,
                    NumeroIdentidade = a.Aluno.DadosPessoais.NumeroIdentidade,
                    OrgaoEmissorIdentidade = a.Aluno.DadosPessoais.OrgaoEmissorIdentidade,
                    Cpf = a.Aluno.Pessoa.Cpf,
                    HistoricoAluno = a.Aluno.Historicos.Select(d => new
                    {
                        Atual = d.Atual,
                        EntidadeResponsavel = d.EntidadeVinculo.Nome,
                        Curso = d.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.Nome,
                        CidadeLocalidade = d.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade
                                            .Enderecos.FirstOrDefault(e => e.Correspondencia.HasValue && e.Correspondencia.Value).NomeCidade,
                    }).FirstOrDefault(h => h.Atual),
                }).FirstOrDefault(),
                Endereco = p.TrabalhoAcademico.Autores.FirstOrDefault().Aluno.Enderecos.FirstOrDefault(w => w.EnderecoCorrespondencia == EnderecoCorrespondencia.AcademicaFinanceira ||
                                                                                                       w.EnderecoCorrespondencia == EnderecoCorrespondencia.Academica).PessoaEndereco.Endereco
            });

            // Converte as informações no VO de retorno
            PublicacaoBdpAutorizacaoVO voRetorno = new PublicacaoBdpAutorizacaoVO()
            {
                Seq = dados.Seq,
                NomeAluno = dados.Autor.Nome,
                Logradouro = dados.Endereco.Logradouro,
                Numero = dados.Endereco.Numero,
                Complemento = dados.Endereco.Complemento,
                Bairro = dados.Endereco.Bairro,
                Cidade = dados.Endereco.NomeCidade,
                Estado = dados.Endereco.SiglaUf,
                CEP = Convert.ToUInt64(dados.Endereco.Cep).ToString(@"00\.000\-000"),
                RG = dados.Autor.NumeroIdentidade?.Trim(),
                OrgaoEmissor = dados.Autor.OrgaoEmissorIdentidade?.Trim(),
                CPF = Convert.ToUInt64(dados.Autor.Cpf).ToString(@"000\.000\.000\-00"),
                TipoTrabalho = dados.TipoTrabalho,
                NivelEnsino = dados.NivelEnsino,
                TituloTrabalho = dados.TituloTrabalho,
                CidadeLocalidade = dados.Autor.HistoricoAluno.CidadeLocalidade,
                DataEmissao = DateTime.Now.SMCDataPorExtenso(),
                EntidadeResponsavel = dados.Autor.HistoricoAluno.EntidadeResponsavel,
                Curso = dados.Autor.HistoricoAluno.Curso,
                NumDiasAutorizacaoParcial = dados.NumeroDiasDuracaoAutorizacaoParcial
            };

            // Busca as informações da autorização
            var autorizacao = PublicacaoBdpAutorizacaoDomainService.BuscarDadosAutorizacaoAtiva(seq);
            if (autorizacao != null)
            {
                voRetorno.TipoAutorizacao = autorizacao.TipoAutorizacao.SMCGetDescription();
                voRetorno.DataAutorizacao = autorizacao.DataAutorizacao.SMCDataAbreviada();
                voRetorno.DataHoraAutorizacao = autorizacao.DataAutorizacao.ToString("dd/MM/yyyy HH:mm");
                voRetorno.CodigoAutorizacao = autorizacao.CodigoAutorizacao.HasValue ? autorizacao.CodigoAutorizacao.ToString().ToUpper() : string.Empty;
                voRetorno.PossuiCodigoAutorizacao = autorizacao.CodigoAutorizacao.HasValue ? "TRUE" : "FALSE";
            }

            // Retirar " que possam ter no título do trabalho e colocar em UpperCase
            // Tratamento necessário pois as " interferem no merge do word
            voRetorno.TituloTrabalho = voRetorno.TituloTrabalho.Replace("\"", "'");
            voRetorno.TituloTrabalho = voRetorno.TituloTrabalho.Replace("“", "'");
            voRetorno.TituloTrabalho = voRetorno.TituloTrabalho.Replace("”", "'");
            voRetorno.TituloTrabalho = voRetorno.TituloTrabalho.ToUpper();

            return voRetorno;
        }

        public void LiberarConferenciaBiblioteca(long seqPublicacaoBdp)
        {
            using (var unit = SMCUnitOfWork.Begin())
            {
                /*  Na liberação para conferência da biblioteca:
                    o   Excluir logicamente as situações do aluno cadastradas para os ciclos letivos que são
                        maiores ao ciclo letivo da data de entrega. A exclusão logica ocorrerá preenchendo os
                        campos com os valores abaixo:
                        · dsc_observacao_exclusao = 'Excluido pela inclusão da data de entrega - Data de entrega: ' +  data de entrega
                        · dat_exclusao = getdate()
                        ·          usu_exclusao = usuário logado
                        ·          dat_alteracao = getdate()
                        ·          usu_alteracao = usuário logado
                    o   Realizar a inclusão da situação de matricula 'FORMADO' para o aluno no ciclo letivo
                        correspondente a data de entrega. Caso este ciclo não exista o mesmo deverá ser incluído
                        no histórico ciclo letivo do aluno.
                */

                // busca qual os seqs dos alunos
                var dadosAluno = this.SearchProjectionByKey(seqPublicacaoBdp, x => new
                {
                    SeqsAlunos = x.TrabalhoAcademico.Autores.Select(a => a.SeqAluno),
                });
                long seqAlunoAutor = dadosAluno.SeqsAlunos.FirstOrDefault();

                foreach (var seqAluno in dadosAluno.SeqsAlunos)
                {
                    /*
                     * RN_ORT_019 - Registrar Entrega Final
                       1. Verificar se existe alguma solicitação para o aluno cujo serviço está configurado para "Abortar processo se existir solicitação não finalizada" que ainda
                          não foi finalizada e em caso positivo, emitir a mensagem de erro abaixo e abortar operação.
                          "Este aluno possui solicitação de X, Y e Z ainda não finalizada(s). Para registro da entrega definitiva do trabalho estes tipos de solicitações deverão
                           ser concluídas."
                          Onde: X, Y e Z é o nome do serviço.
                     */

                    // Busca as solicitações para abortar
                    var specSolicitacaoNaoFinalizada = new SolicitacaoServicoFilterSpecification()
                    {
                        SeqPessoaAtuacao = seqAluno,
                        AcaoLiberacaoTrabalho = AcaoLiberacaoTrabalho.AbortarProcessoSeExistirSolicitacaoNaoFinalizada,
                        CategoriasSituacoes = new List<CategoriaSituacao> { CategoriaSituacao.Novo, CategoriaSituacao.EmAndamento },
                    };
                    var solicitacoesAbortar = SolicitacaoServicoDomainService.SearchProjectionBySpecification(specSolicitacaoNaoFinalizada, x => new
                    {
                        x.ConfiguracaoProcesso.Processo.Servico.Descricao,
                        x.Seq,
                        x.NumeroProtocolo
                    });
                    if (solicitacoesAbortar.Any())
                        throw new PublicacaoBdpSolicitacaoAbertaAbortarException(solicitacoesAbortar.Select(x => x.Descricao).ToList());

                    // 2 . Busca os dados de origem da pessoa atuação
                    var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno, true);

                    // Recupera o ciclo letivo corrente (cujo periodo do evento letivo é corrente) para o curso-oferta-turno do aluno
                    long seqCiclo = ConfiguracaoEventoLetivoDomainService.BuscarSeqCicloLetivo(DateTime.Now, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

                    // Recupera o ciclo letivo posterior (se encontrou o ciclo corrente)
                    long? seqCicloPosterior = null;
                    if (seqCiclo > 0)
                        seqCicloPosterior = CicloLetivoDomainService.BuscarProximoCicloLetivo(seqCiclo);

                    // Recupera as situações futuras pela data...
                    List<SituacaoFuturaAlunoVO> situacoesFuturasData = AlunoHistoricoSituacaoDomainService.BuscarSituacoesFuturasAluno(seqAluno, DateTime.Now, null);

                    // Recupera as situações futuras pelo ciclo posterior caso exista...
                    List<SituacaoFuturaAlunoVO> situacoesFuturasCiclo = new List<SituacaoFuturaAlunoVO>();
                    if (seqCicloPosterior.HasValue)
                        situacoesFuturasCiclo = AlunoHistoricoSituacaoDomainService.BuscarSituacoesFuturasAluno(seqAluno, null, seqCicloPosterior);

                    // Junta tudo que for diferente
                    var situacoesFuturas = new List<SituacaoFuturaAlunoVO>();
                    situacoesFuturas.AddRange(situacoesFuturasData);
                    situacoesFuturas.AddRange(situacoesFuturasCiclo.Where(s => !situacoesFuturasData.Any(sd => sd.SeqAlunoHistoricoSituacao == s.SeqAlunoHistoricoSituacao)));

                    // Cancela as situações futuras
                    foreach (var situacaoCancelar in situacoesFuturas)
                        AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacaoCancelar.SeqAlunoHistoricoSituacao, "Excluido pela inclusão da data de entrega - Data de entrega: " + DateTime.Now.ToShortDateString(), null);

                    // 3 . Insere no ciclo atual a situação de formado (se ainda não existir situação de formado para o aluno)
                    var incluirSituacao = AlunoHistoricoSituacaoDomainService.Count(new AlunoHistoricoSituacaoFilterSpecification
                    {
                        SeqPessoaAtuacaoAluno = seqAluno,
                        TokenSituacao = TOKENS_SITUACAO_MATRICULA.FORMADO,
                        Excluido = false,
                    }) == 0;
                    if (incluirSituacao)
                    {
                        // Se deve incluir a situação de FORMADO, mas não existe ciclo letivo atual para o aluno, estoura erro
                        if (seqCiclo == 0)
                            throw new SituacaoAlunoFormadoSemCicloAtualException(DateTime.Now.ToShortDateString());
                            
                        AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(new IncluirAlunoHistoricoSituacaoVO
                        {
                            CriarAlunoHistoricoCicloLetivo = true,
                            DataInicioSituacao = DateTime.Now,
                            SeqAluno = seqAluno,
                            SeqCicloLetivo = seqCiclo,
                            TokenSituacao = TOKENS_SITUACAO_MATRICULA.FORMADO,
                            Observacao = "Entrega final da tese/dissertação"
                        });

                        //
                        /*o	Ao clicar no botão “Liberar para Biblioteca”, além das regras já implementadas, incluir também no SGP o ano/semestre formação do aluno.
                            Para isto, atualizar na tabela aluno do banco SGA onde o cod_aluno = cod_aluno_migracao da tabela aln.aluno do banco ACADEMICO, os campos conforme informações abaixo:
                            o	ind_formando = 1
                            o	ano_formando = ano do ciclo letivo do curso do aluno referente a data atual
                            o	sem_formando = número do ciclo letivo do curso do aluno referente a data atual
                            */
                        var dadosCiclo = CicloLetivoDomainService.SearchProjectionByKey(seqCiclo, x => new
                        {
                            Ano = x.Ano,
                            Numero = x.Numero,
                        });
                        var codAlunoMigracao = AlunoDomainService.SearchProjectionByKey(seqAluno, x => x.CodigoAlunoMigracao);
                        IntegracaoAcademicoService.AtualizarAlunoLiberacaoTrabalhoBiblioteca(codAlunoMigracao.GetValueOrDefault(), dadosCiclo.Ano, dadosCiclo.Numero);
                    }

                    // 4 . Busca as solicitações para encerrar
                    specSolicitacaoNaoFinalizada.AcaoLiberacaoTrabalho = AcaoLiberacaoTrabalho.CancelarSolicitacoesNaoFinalizadas;
                    var solicitacoesEncerrar = SolicitacaoServicoDomainService.SearchProjectionBySpecification(specSolicitacaoNaoFinalizada, x => new
                    {
                        x.ConfiguracaoProcesso.Processo.Servico.Descricao,
                        x.Seq,
                        x.NumeroProtocolo
                    });
                    if (solicitacoesAbortar.Any())
                    {
                        solicitacoesEncerrar.SMCForEach(s =>
                        {
                            SolicitacaoServicoDomainService.SalvarCancelamentoSolicitacao(new CancelamentoSolicitacaoVO()
                            {
                                SeqSolicitacaoServico = s.Seq,
                                TokenMotivoCancelamento = TOKEN_MOTIVO_SITUACAO_CANCELAMENTO.ALUNO_FORMADO,
                                Observacao = "Aluno formado"
                            });
                        });
                    }

                    //Verificar se existem solicitações de renovação de matricula com a categoria de situação "Nova" ou "Em andamento"
                    //para o ciclo letivo posterior ao ciclo letivo que o aluno passou para situação de "Formado".

                    //Caso existe ciclo letivo posterior
                    if (seqCicloPosterior.HasValue)
                    {
                        //Realiza ajustes no plao de estudo
                        PlanoEstudoDomainService.AjustarPlanoEstudo(new AjustePlanoEstudoVO()
                        {
                            SeqPessoaAtuacao = seqAluno,
                            SeqCicloLetivoReferencia = seqCicloPosterior,
                            Observacao = "Aluno formado",
                            Atual = false,
                            DataFimOrientacao = DateTime.Now
                        });
                    }
                }

                // Altera a situação da publicação do bdp para liberada para biblioteca
                AlterarSituacao(seqPublicacaoBdp, SituacaoTrabalhoAcademico.LiberadaBiblioteca);
                EnviarNotificacaoAtualizacaoDocumentacao(seqAlunoAutor);

                unit.Commit();
            }
        }
        public void EnviarNotificacaoAtualizacaoDocumentacao(long seqAluno)
        {
            DestinatarioEnvioNotificacaoVO alunoAutorTrabalho = ObterDadosAlunoAutorParaEnvioNotificacao(seqAluno);

            long seqConfiguracaoNotificacaoAtivo = EntidadeConfiguracaoNotificacaoDomainService
                .BuscarSeqConfiguracaoNotificacaoAtivo(alunoAutorTrabalho.SeqInstituicaoEnsino, TOKEN_TIPO_NOTIFICACAO.CRIACAO_SOLICITACAO_ATUALIZACAO_DOCUMENTACAO);

            if (seqConfiguracaoNotificacaoAtivo > 0)//Se encontrar notificação configurada
            {
                NotificacaoEmailData notificacaoEmailData = MontarNotificacaoEmail(alunoAutorTrabalho, seqConfiguracaoNotificacaoAtivo);
                NotificacaoService.SalvarNotificacao(notificacaoEmailData);
            }
        }

        private NotificacaoEmailData MontarNotificacaoEmail(DestinatarioEnvioNotificacaoVO aluno, long seqConfiguracaoNotificacaoAtivo)
        {
            Dictionary<string, string> mergeMensagemNotificacao = new Dictionary<string, string>();
            mergeMensagemNotificacao["{{NOM_PESSOA}}"] = string.IsNullOrWhiteSpace(aluno.NomeSocial) ? aluno.Nome : aluno.NomeSocial;
            mergeMensagemNotificacao["{{CURSO_OFERTA}}"] = aluno.CursoOferta;
            var destinatarios = new List<NotificacaoEmailDestinatarioData>() {
                                        new NotificacaoEmailDestinatarioData() {EmailDestinatario = string.Join(";", aluno.Emails)}};
            return new NotificacaoEmailData()
            {
                SeqConfiguracaoNotificacao = seqConfiguracaoNotificacaoAtivo,
                DadosMerge = mergeMensagemNotificacao,
                DataPrevistaEnvio = DateTime.Now,
                PrioridadeEnvio = PrioridadeEnvio.QuandoPossivel,
                Destinatarios = destinatarios
            };
        }

        private DestinatarioEnvioNotificacaoVO ObterDadosAlunoAutorParaEnvioNotificacao(long seqAluno)
        {
            return AlunoDomainService.SearchProjectionByKey(seqAluno, a => new DestinatarioEnvioNotificacaoVO
            {
                Nome = a.DadosPessoais.Nome,
                NomeSocial = a.DadosPessoais.NomeSocial,
                CursoOferta = a.Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                Emails = a.EnderecosEletronicos.Where(e => e.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email).Select(e => e.EnderecoEletronico.Descricao).ToList(),
                SeqInstituicaoEnsino = a.Pessoa.SeqInstituicaoEnsino
            });
        }
        public bool ValidarLiberacaoConferenciaBiblioteca(long seqPublicacaoBdp)
        {
            // busca qual os seqs dos alunos
            var dadosAluno = this.SearchProjectionByKey(seqPublicacaoBdp, x => new
            {
                SeqsAlunos = x.TrabalhoAcademico.Autores.Select(a => a.SeqAluno),
                Arquivos = x.Arquivos,
                PossuiOrientador = x.TrabalhoAcademico
                                    .DivisoesComponente.Any(f => f.OrigemAvaliacao.AplicacoesAvaliacao.Where(g => g.Avaliacao.TipoAvaliacao == TipoAvaliacao.Banca && g.DataCancelamento == null)
                                    .OrderByDescending(g => g.Seq).FirstOrDefault().MembrosBancaExaminadora.Any(w => w.TipoMembroBanca == TipoMembroBanca.Orientador))
            });

            // Verificar se existe um membro da banca examinadora com o tipo de participação igual a "Orientador".
            // Caso negativo emitir a mensagem abaixo de erro e abortar operação:
            if (!dadosAluno.PossuiOrientador)
                throw new PublicacaoBdpLiberacaoConsultaSemOrientadorException();

            // Task 31378
            // Ao clicar no botão que executa o comando de liberar para biblioteca, verificar se o arquivo do trabalho foi anexado.
            // Em caso negativo, emitir a mensagem de erro:
            if (dadosAluno.Arquivos.Count() == 0)
                throw new PublicacaoBdpLiberacaoConsultaSemArquivoException();

            foreach (var seqAluno in dadosAluno.SeqsAlunos)
            {
                ValidarAlunoParaPublicacaoBDP(seqAluno);
            }

            return true;
        }

        /// <summary>
        /// Validar e Liberar a consulta para biblioteca de uma publicação BDP
        /// </summary>
        /// <param name="seqPublicacaoBdp">Sequencial da publicação BDP a ser liberado para consulta.</param>
        public void ValidarLiberacaoConsulta(long seqPublicacaoBdp)
        {
            using (var unit = SMCUnitOfWork.Begin())
            {
                // Busca os dados da publicação BDP
                var publicacaoBdp = this.SearchProjectionByKey(seqPublicacaoBdp, x => new
                {
                    Arquivos = x.Arquivos,
                    SeqTrabalhoAcademico = x.SeqTrabalhoAcademico,
                    IntegracaoBiblioteca = (IntegracaoBiblioteca?)x.TrabalhoAcademico.InstituicaoEnsino.IntegracaoBiblioteca,
                    CodigoAcervo = x.CodigoAcervo
                });

                // Verifica se o arquivo da publicação BDP está informado.
                if (publicacaoBdp.Arquivos.Count() == 0)
                    throw new PublicacaoBdpLiberacaoConsultaSemArquivoException();

                // Se a integração com a biblioteca é via PERGAMUM e ainda não existe CodigoAcervo, carregar o trabalho
                if (publicacaoBdp.IntegracaoBiblioteca == IntegracaoBiblioteca.Pergarmun && !publicacaoBdp.CodigoAcervo.HasValue)
                {
                    var codigoAcervo = AcademicoRepository.CarregarTrabalhoPergamun(publicacaoBdp.SeqTrabalhoAcademico);
                    UpdateFields(new PublicacaoBdp { Seq = seqPublicacaoBdp, CodigoAcervo = codigoAcervo }, x => x.CodigoAcervo);
                }

                // Altera a situação da publicação BDP
                AlterarSituacao(seqPublicacaoBdp, SituacaoTrabalhoAcademico.LiberadaConsulta);

                // Finaliza a transação
                unit.Commit();
            }
        }

        /// <summary>
        /// Buscar os dados da publicação BDP para conferência e liberação
        /// </summary>
        /// <param name="spec">Filtros de pesquisa</param>
        /// <returns>Lista paginada das publicações BDP para conferencia</returns>
        public SMCPagerData<ConferenciaPublicacaoBdpVO> BuscarPublicacoesBdp(PublicacaoBdpFilterSpecification spec)
        {
            var lista = this.SearchProjectionBySpecification(spec, x => new ConferenciaPublicacaoBdpVO
            {
                // Obs.: Esse VO está sendo converdido em um TrabalhoAcademicoListaData por isso os Seqs estão invertidos! :(
                Seq = x.TrabalhoAcademico.Seq,
                SeqPublicacaoBdp = x.Seq,
                DataDefesa = x.TrabalhoAcademico.DivisoesComponente.Select(f => f.OrigemAvaliacao.AplicacoesAvaliacao
                                                                    .Where(g => g.Avaliacao.TipoAvaliacao == TipoAvaliacao.Banca && g.DataCancelamento == null)
                                                                    .OrderByDescending(o => o.Seq)
                                                                    .FirstOrDefault().DataInicioAplicacaoAvaliacao).FirstOrDefault(),
                TituloTrabalho = x.TrabalhoAcademico.Titulo,
                TituloIdiomaTrabalho = x.InformacoesIdioma.FirstOrDefault(i => i.IdiomaTrabalho).Titulo,
                TipoTrabalho = x.TrabalhoAcademico.TipoTrabalho.Descricao,
                Situacao = x.HistoricoSituacoes.OrderByDescending(f => f.DataInclusao).FirstOrDefault().SituacaoTrabalhoAcademico,
                Autores = x.TrabalhoAcademico.Autores.Select(f => f.NomeAutor).ToList(),
                EntidadeResponsavel = x.TrabalhoAcademico.Autores.FirstOrDefault().Aluno.Historicos.OrderByDescending(o => o.DataInclusao).FirstOrDefault().EntidadeVinculo.Nome,
                OfertaCursoLocalidadeTurno = x.TrabalhoAcademico.Autores.FirstOrDefault().Aluno.Historicos.OrderByDescending(o => o.DataInclusao).FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome
                                                + " " +
                                                x.TrabalhoAcademico.Autores.FirstOrDefault().Aluno.Historicos.OrderByDescending(o => o.DataInclusao).FirstOrDefault().CursoOfertaLocalidadeTurno.Turno.Descricao,
                SeqInstituicaoEnsino = x.TrabalhoAcademico.SeqInstituicaoEnsino,
                SeqNivelEnsino = x.TrabalhoAcademico.SeqNivelEnsino
            }, out int total).ToList();

            foreach (var item in lista)
            {
                // Busca a instituição e nível de ensino
                var specInstituicaoNivel = new InstituicaoNivelFilterSpecification() { SeqInstituicaoEnsino = item.SeqInstituicaoEnsino, SeqNivelEnsino = item.SeqNivelEnsino };
                item.SeqInstituicaoNivel = InstituicaoNivelDomainService.SearchProjectionByKey(specInstituicaoNivel, i => i.Seq);

                // Verifica qual título do trabalho deve ser exibido
                item.Titulo = !string.IsNullOrEmpty(item.TituloIdiomaTrabalho) ? item.TituloIdiomaTrabalho : item.TituloTrabalho;
            }

            return new SMCPagerData<ConferenciaPublicacaoBdpVO>(lista, total);
        }

        private void ValidarAlunoParaPublicacaoBDP(long seqAluno)
        {
            //1 - Verificar nos planos de estudo atual de cada ciclo letivo  do aluno se existe alguma turma deste aluno que esta sem a nota final no seu  histórico escolar. Caso seja encontrado alguma turma enviar a mensagem de erro abaixo e abortar toda a transação:
            //'O aluno não possui nota final  lançada para as turmas: <ciclo letivo> + ' - '  + <descrição da turma conforme regra
            //RN_TUR_029 - Exibição Descrição Resumida Divisão Turma >
            VerificarPlanoEstudos(seqAluno);

            //2 - Verificar se o aluno concluiu todo o currículo conforme RN_CNC_004 - Apuração - Situação de conclusão currículo.
            //Se o resultado retornado for diferente de 'Concluído' enviar a mensagem de erro e abortar a transação:
            //'Aluno não integralizou todo o seu currículo não podendo desta forma, ter a situação de 'FORMADO' e ter seu trabalho liberado para consulta'.
            VerificarCurriculoConcretizado(seqAluno);
        }

        private void VerificarCurriculoConcretizado(long seqAluno)
        {
            // Recuperar os dados da pessoa atuação identificando aluno e ingressante
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno);

            // Se o aluno possui matriz curricular informada, verifica se concluiu o curso
            if (dadosOrigem.SeqMatrizCurricularOferta > 0)
            {
                var dadosConclusaoCursoAluno = AcademicoRepository.CalcularPercentualConclusaoCursoAluno(dadosOrigem.SeqAlunoHistoricoAtual);
                if (dadosConclusaoCursoAluno.PercentualConclusaoGeral != 100)
                {
                    throw new PublicacaoBdpIdiomaCurriculoNaoConcluidoException();
                }
            }
        }

        private void VerificarPlanoEstudos(long seqAluno)
        {
            //Foi criado desta forma para que busque todos os planos de estudos diferentes 0 ou seja todos
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno);
            var seqCicloLetivo = 0;
            var specPlanoEstudo = new PlanoEstudoFilterSpecification()
            {
                SeqAluno = seqAluno,
                CicloLetivoDiferente = seqCicloLetivo,
                Atual = true
            };

            var planosEstudos = this.PlanoEstudoDomainService.SearchProjectionBySpecification(specPlanoEstudo, p => new
            {
                p.Seq,
                SeqAlunoHistorico = p.AlunoHistoricoCicloLetivo.SeqAlunoHistorico,
                Itens = p.Itens.Select(s => new
                {
                    s.SeqDivisaoTurma,
                    SeqTurma = s.SeqDivisaoTurma == null ? 0 : s.DivisaoTurma.SeqTurma,
                    SeqOrigemAvaliacaoTurma = s.DivisaoTurma.Turma.SeqOrigemAvaliacao == null ? 0 : s.DivisaoTurma.Turma.SeqOrigemAvaliacao,
                    DescricaoConfiguracaoPrincipal = s.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).Descricao,
                    DescricaoConfiguracao = s.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).Descricao,
                }).ToList()
            }).ToList();

            var turmasSemNotas = string.Empty;
            //Percorre todas a turmas anteriores
            foreach (var plano in planosEstudos)
            {
                //Percorre todas as turmas que validando se a mesma tem os mesmo componetes e assunto 
                foreach (var item in plano.Itens)
                {
                    //Valida se por ventura tem divisão para aquele item do plano de estudo
                    if (item.SeqDivisaoTurma == null)
                    {
                        continue;
                    }

                    var specHistorico = new HistoricoEscolarFilterSpecification()
                    {
                        SeqAlunoHistorico = plano.SeqAlunoHistorico,
                        SeqOrigemAvaliacao = item.SeqOrigemAvaliacaoTurma
                    };

                    var nota = this.HistoricoEscolarDomainService.SearchProjectionBySpecification(specHistorico, p => new { p.Nota, p.CicloLetivo.Descricao }).FirstOrDefault();

                    if (nota == null)
                    {
                        // <descrição da turma conforme regra
                        //RN_TUR_029 - Exibição Descrição Resumida Divisão Turma
                        var descricao = !string.IsNullOrEmpty(item.DescricaoConfiguracao) ? item.DescricaoConfiguracao : item.DescricaoConfiguracaoPrincipal;
                        turmasSemNotas += ($"{descricao}<br />");
                    }
                }
            };
            if (!string.IsNullOrEmpty(turmasSemNotas))
            {
                throw new PublicacaoBdpIdiomaPlanoEstudoSemNotaException(turmasSemNotas);
            }
        }

        public void RetornarSituacaoAlunoBdp(long seqPublicacaoBdp)
        {
            // Limpa as autorizações realizadas anterioremente.
            PublicacaoBdpAutorizacaoDomainService.ExcluirAutorizacoes(seqPublicacaoBdp);

            // Retorna com a situação para "Cadastrado pelo aluno"
            this.AlterarSituacao(seqPublicacaoBdp, SituacaoTrabalhoAcademico.CadastradaAluno);
        }

        public void NotificarBibliotecaTrabalhoComMudanca(MudancaTipoTrabalhoAcademicoSATVO filtro)
        {
            var servico = Create<MudancaTipoTrabalhoAcademicoJob>();
            servico.Execute(filtro);
        }

        public List<ListaTrabalhoAcademicoVO> BuscarTrabalhoComMudancaTipoTrabalho(long seqInstituicaoEnsino, string dataProcessamento)
        {
            return this.RawQuery<ListaTrabalhoAcademicoVO>(JOB_MUDANCA_PUBLICACAO,
                                                           new SMCFuncParameter("SEQ_INSTITUICAO_ENSINO", seqInstituicaoEnsino),
                                                           new SMCFuncParameter("DAT_AUTORIZACAO", dataProcessamento));
        }
    }
}