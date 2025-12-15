using Newtonsoft.Json;
using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.CNC.Exceptions;
using SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao;
using SMC.Academico.Common.Areas.CNC.Models;
using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.Shared.Constants;
using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications.AtoNormativoEntidade;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.Domain.DomainServices;
using SMC.Academico.Domain.Helpers;
using SMC.Academico.Domain.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.DadosMestres.ServiceContract.Areas.GED.Interfaces;
using SMC.EstruturaOrganizacional.ServiceContract.Areas.ESO.Interfaces;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Repository;
using SMC.Framework.Rest;
using SMC.Framework.Security;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Interfaces;
using SMC.Localidades.Common.Areas.LOC.Enums;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using SMC.Notificacoes.Common.Areas.NTF.Enums;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using SMC.Pessoas.ServiceContract.Areas.PES.Interfaces;
using SMC.Seguranca.ServiceContract.Areas.USU.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class DocumentoConclusaoDomainService : AcademicoContextDomain<DocumentoConclusao>
    {
        #region DomainServices

        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService => Create<CursoOfertaLocalidadeDomainService>();

        private NivelEnsinoDomainService NivelEnsinoDomainService => Create<NivelEnsinoDomainService>();

        private SolicitacaoDocumentoConclusaoDomainService SolicitacaoDocumentoConclusaoDomainService => Create<SolicitacaoDocumentoConclusaoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private DocumentoAcademicoHistoricoSituacaoDomainService DocumentoAcademicoHistoricoSituacaoDomainService => Create<DocumentoAcademicoHistoricoSituacaoDomainService>();

        private DocumentoConclusaoFormacaoDomainService DocumentoConclusaoFormacaoDomainService => Create<DocumentoConclusaoFormacaoDomainService>();

        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();

        private PessoaDomainService PessoaDomainService => Create<PessoaDomainService>();

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService => Create<FormacaoEspecificaDomainService>();

        private InstituicaoNivelDomainService InstituicaoNivelDomainService => Create<InstituicaoNivelDomainService>();

        private InstituicaoNivelTipoDocumentoAcademicoDomainService InstituicaoNivelTipoDocumentoAcademicoDomainService => Create<InstituicaoNivelTipoDocumentoAcademicoDomainService>();

        private SituacaoDocumentoAcademicoDomainService SituacaoDocumentoAcademicoDomainService => Create<SituacaoDocumentoAcademicoDomainService>();

        private SolicitacaoDocumentoConclusaoEntregaDigitalDomainService SolicitacaoDocumentoConclusaoEntregaDigitalDomainService => Create<SolicitacaoDocumentoConclusaoEntregaDigitalDomainService>();

        private PessoaDadosPessoaisDomainService PessoaDadosPessoaisDomainService => Create<PessoaDadosPessoaisDomainService>();

        private CursoFormacaoEspecificaDomainService CursoFormacaoEspecificaDomainService => Create<CursoFormacaoEspecificaDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private SolicitacaoDocumentoRequeridoDomainService SolicitacaoDocumentoRequeridoDomainService => Create<SolicitacaoDocumentoRequeridoDomainService>();

        private ArquivoAnexadoDomainService ArquivoAnexadoDomainService => Create<ArquivoAnexadoDomainService>();

        private SolicitacaoHistoricoSituacaoDomainService SolicitacaoHistoricoSituacaoDomainService => Create<SolicitacaoHistoricoSituacaoDomainService>();

        private ProcessoEtapaConfiguracaoNotificacaoDomainService ProcessoEtapaConfiguracaoNotificacaoDomainService => Create<ProcessoEtapaConfiguracaoNotificacaoDomainService>();

        private TipoNotificacaoDomainService TipoNotificacaoDomainService => Create<TipoNotificacaoDomainService>();

        private SolicitacaoServicoEnvioNotificacaoDomainService SolicitacaoServicoEnvioNotificacaoDomainService => Create<SolicitacaoServicoEnvioNotificacaoDomainService>();

        private DocumentoRequeridoDomainService DocumentoRequeridoDomainService => Create<DocumentoRequeridoDomainService>();

        private InstituicaoEnsinoDomainService InstituicaoEnsinoDomainService => Create<InstituicaoEnsinoDomainService>();

        private MantenedoraDomainService MantenedoraDomainService => Create<MantenedoraDomainService>();

        private CicloLetivoDomainService CicloLetivoDomainService => Create<CicloLetivoDomainService>();

        private ModalidadeDomainService ModalidadeDomainService => Create<ModalidadeDomainService>();

        private AtoNormativoEntidadeDomainService AtoNormativoEntidadeDomainService => Create<AtoNormativoEntidadeDomainService>();

        private PessoaAtuacaoBloqueioDomainService PessoaAtuacaoBloqueioDomainService => Create<PessoaAtuacaoBloqueioDomainService>();

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private AlunoHistoricoDomainService AlunoHistoricoDomainService => Create<AlunoHistoricoDomainService>();

        private ClassificacaoInvalidadeDocumentoDomainService ClassificacaoInvalidadeDocumentoDomainService => Create<ClassificacaoInvalidadeDocumentoDomainService>();

        #endregion DomainServices

        #region Services

        private ILocalidadeService LocalidadeService => Create<ILocalidadeService>();

        private ISMCReportMergeService SMCReportMergeService => Create<ISMCReportMergeService>();

        private ITipoDocumentoService TipoDocumentoService => Create<ITipoDocumentoService>();

        private ITipoTemplateProcessoService TipoTemplateProcessoService => Create<ITipoTemplateProcessoService>();

        private ISituacaoService SituacaoService => Create<ISituacaoService>();

        private IIntegracaoAcademicoService IntegracaoAcademicoService => Create<IIntegracaoAcademicoService>();

        private INotificacaoService NotificacaoService { get => Create<INotificacaoService>(); }

        public IEstruturaOrganizacionalService EstruturaOrganizacionalService => Create<IEstruturaOrganizacionalService>();

        private IPessoaService PessoaService => Create<IPessoaService>();

        private IUsuarioService UsuarioService => Create<IUsuarioService>();

        #endregion Services

        #region Querys

        private string QUERY_COUNT_DOCUMENTOS_CONCLUSAO =
            @"select count(dc.seq_documento_academico)
              from	cnc.documento_conclusao dc
			  inner join cnc.documento_academico da on dc.seq_documento_academico = da.seq_documento_academico
              inner join aln.aluno a on a.seq_pessoa_atuacao = dc.seq_atuacao_aluno
              inner join pes.pessoa_atuacao p on p.seq_pessoa_atuacao = a.seq_pessoa_atuacao
              inner join pes.pessoa_dados_pessoais pd on pd.seq_pessoa_dados_pessoais = p.seq_pessoa_dados_pessoais
              inner join aln.aluno_historico ah on ah.seq_atuacao_aluno = a.seq_pessoa_atuacao and ah.ind_atual = 1
              inner join cso.curso_oferta_localidade_turno colt on colt.seq_curso_oferta_localidade_turno = ah.seq_curso_oferta_localidade_turno
              inner join cso.curso_oferta_localidade col on col.seq_entidade = colt.seq_entidade_curso_oferta_localidade
              inner join org.entidade e on e.seq_entidade = col.seq_entidade
              inner join cnc.tipo_documento_academico tdc on tdc.seq_tipo_documento_academico = da.seq_tipo_documento_academico and tdc.idt_dom_grupo_docto_academico in (1,2)
              inner join cnc.documento_academico_historico_situacao dchs on dchs.seq_documento_academico_historico_situacao = da.seq_documento_academico_historico_situacao_atual
              inner join cnc.situacao_documento_academico sdc on sdc.seq_situacao_documento_academico = dchs.seq_situacao_documento_academico
              where ah.seq_aluno_historico in (select max(seq_aluno_historico) 
                                               from aln.aluno_historico 
                                               where seq_atuacao_aluno = a.seq_pessoa_atuacao and ind_atual = 1)";

        private string QUERY_BUSCAR_DOCUMENTOS_CONCLUSAO =
            @"select dc.seq_documento_academico as Seq, 
                     a.num_registro_academico as NumeroRegistroAcademico,
                     CASE WHEN pd.nom_social is not null 
					 THEN pd.nom_social + ' (' + pd.nom_pessoa + ')' 
					 ELSE pd.nom_pessoa 
					 END AS NomeAluno, 
                     pd.idt_dom_sexo as Sexo,
                     colt.seq_entidade_curso_oferta_localidade as SeqEntidadeCursoOfertaLocalidade,
                     e.nom_entidade as NomeEntidadeCursoOfertaLocalidade,
                     da.seq_tipo_documento_academico as SeqTipoDocumentoAcademico,
                     tdc.dsc_tipo_documento_academico as DescricaoTipoDocumentoAcademico,
                     da.num_via_documento as NumeroViaDocumento,
                     sdc.dsc_situacao_documento_academico as DescricaoSituacaoDocumentoAcademicoAtual,
                     tdc.dsc_token as TipoDocumentoToken,
                     sdc.dsc_token as Token,
                     dchs.idt_dom_tipo_invalidade as TipoInvalidade
              from cnc.documento_conclusao dc
			  inner join cnc.documento_academico da on dc.seq_documento_academico = da.seq_documento_academico
              inner join aln.aluno a on a.seq_pessoa_atuacao = dc.seq_atuacao_aluno
              inner join pes.pessoa_atuacao p on p.seq_pessoa_atuacao = a.seq_pessoa_atuacao
              inner join pes.pessoa_dados_pessoais pd on pd.seq_pessoa_dados_pessoais = p.seq_pessoa_dados_pessoais
              inner join aln.aluno_historico ah on ah.seq_atuacao_aluno = a.seq_pessoa_atuacao and ah.ind_atual = 1
              inner join cso.curso_oferta_localidade_turno colt on colt.seq_curso_oferta_localidade_turno = ah.seq_curso_oferta_localidade_turno
              inner join cso.curso_oferta_localidade col on col.seq_entidade = colt.seq_entidade_curso_oferta_localidade
              inner join org.entidade e on e.seq_entidade = col.seq_entidade
              inner join cnc.tipo_documento_academico tdc on tdc.seq_tipo_documento_academico = da.seq_tipo_documento_academico and tdc.idt_dom_grupo_docto_academico in (1,2)
              inner join cnc.documento_academico_historico_situacao dchs on dchs.seq_documento_academico_historico_situacao = da.seq_documento_academico_historico_situacao_atual
              inner join cnc.situacao_documento_academico sdc on sdc.seq_situacao_documento_academico = dchs.seq_situacao_documento_academico
              where ah.seq_aluno_historico in (select max(seq_aluno_historico)
                                               from aln.aluno_historico 
                                               where seq_atuacao_aluno = a.seq_pessoa_atuacao and ind_atual = 1)";

        #endregion Querys

        #region [ Apis ]

        public SMCApiClient APIDiplomaGAD => SMCApiClient.Create("DiplomaGAD");

        public SMCApiClient APIHistoricoGAD => SMCApiClient.Create("HistoricoGAD");

        #endregion [ Apis ]

        public DocumentoConclusaoVO BuscarDocumentoConclusao(long seqDocumentoConclusao)
        {
            var documentoConclusao = this.SearchByKey(new SMCSeqSpecification<DocumentoConclusao>(seqDocumentoConclusao));

            var retorno = documentoConclusao.Transform<DocumentoConclusaoVO>();

            return retorno;
        }

        public DocumentoConclusaoVO BuscarPessoaCursoGrauDocumentoConclusao(long seqDocumentoConclusao)
        {
            var documentoConclusao = this.SearchByKey(new SMCSeqSpecification<DocumentoConclusao>(seqDocumentoConclusao), x => x.Aluno);

            var retorno = documentoConclusao.Transform<DocumentoConclusaoVO>();
            retorno.SeqPessoa = documentoConclusao.Aluno.SeqPessoa;

            var cursoOfertas = this.DocumentoConclusaoFormacaoDomainService.SearchProjectionBySpecification(new DocumentoConclusaoFormacaoFilterSpecification() { SeqDocumentoConclusao = seqDocumentoConclusao }, x => new CursoOfertaVO()
            {
                SeqCurso = x.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso,
                SeqGrauAcademico = x.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica.FirstOrDefault(f => f.SeqFormacaoEspecifica == x.AlunoFormacao.SeqFormacaoEspecifica).SeqGrauAcademico
            }).ToList();

            if (cursoOfertas.Any())
            {
                retorno.SeqCurso = cursoOfertas.First().SeqCurso;
                retorno.SeqGrauAcademico = cursoOfertas.First().SeqGrauAcademico;
            }

            return retorno;
        }

        public DocumentoConclusaoConsultaVO BuscarDocumentoConclusaoConsulta(long seqDocumentoConclusao)
        {
            var spec = new SMCSeqSpecification<DocumentoConclusao>(seqDocumentoConclusao);

            var retorno = this.SearchProjectionByKey(spec, a => new DocumentoConclusaoConsultaVO()
            {
                NumeroRegistroAcademico = a.Aluno.NumeroRegistroAcademico,
                CodigoAlunoMigracao = a.Aluno.CodigoAlunoMigracao,
                Nome = a.SeqPessoaDadosPessoais.HasValue ? a.PessoaDadosPessoais.Nome : a.Aluno.DadosPessoais.Nome,
                NomeSocial = a.SeqPessoaDadosPessoais.HasValue ? a.PessoaDadosPessoais.NomeSocial : a.Aluno.DadosPessoais.NomeSocial,
                Cpf = a.Aluno.Pessoa.Cpf,
                NumeroPassaporte = a.Aluno.Pessoa.NumeroPassaporte,

                Seq = a.Seq,
                SeqPessoaDadosPessoais = a.SeqPessoaDadosPessoais,
                SeqPessoaAtuacao = a.SeqAtuacaoAluno,
                TokenSituacaoDocumentoAcademicoAtual = a.SituacaoAtual.SituacaoDocumentoAcademico.Token,
                TipoInvalidade = a.SituacaoAtual.ClassificacaoInvalidadeDocumento.TipoInvalidade,
                TokenTipoDocumento = a.TipoDocumentoAcademico.Token,
                SeqTipoDocumentoAcademico = a.SeqTipoDocumentoAcademico,
                DescricaoTipoDocumentoAcademico = a.TipoDocumentoAcademico.Descricao,
                LancamentoHistorico = a.LancamentoHistorico,
                SeqSolicitacaoServico = a.SeqSolicitacaoServico,
                CodigoMigracaoDocumento = a.CodigoMigracaoDocumento,
                SeqDocumentoDiplomaGAD = a.SeqDocumentoGAD,
                NumeroProtocolo = a.SolicitacaoServico.NumeroProtocolo,
                PossuiApostilamentos = a.Apostilamentos.Any(),
                NumeroViaDocumento = a.NumeroViaDocumento,
                SeqDocumentoConclusaoViaAnterior = a.SeqDocumentoAcademicoViaAnterior,

                FormacoesEspecificas = a.FormacoesEspecificas.Select(x => new DocumentoConclusaoFormacaoListarVO()
                {
                    Seq = x.Seq,
                    SeqDocumentoConclusao = x.SeqDocumentoConclusao,
                    SeqAlunoFormacao = x.SeqAlunoFormacao,
                    NumeroRegistroAcademico = x.AlunoFormacao.AlunoHistorico.Aluno.NumeroRegistroAcademico,
                    DescricaoTitulacaoMasculino = x.AlunoFormacao.Titulacao.DescricaoMasculino,
                    DescricaoTitulacaoFeminino = x.AlunoFormacao.Titulacao.DescricaoFeminino,

                    DescricaoNivelEnsino = x.AlunoFormacao.AlunoHistorico.NivelEnsino.Descricao,
                    DescricaoCursoOferta = x.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                    DescricaoDocumentoConclusao = x.AlunoFormacao.DescricaoDocumentoConclusao,
                    DescricaoGrauAcademico = x.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica.FirstOrDefault(f => f.SeqFormacaoEspecifica == x.AlunoFormacao.SeqFormacaoEspecifica).GrauAcademico.Descricao,
                    DescricaoTitulacao = a.Aluno.DadosPessoais.Sexo == Sexo.Masculino ? x.AlunoFormacao.Titulacao.DescricaoMasculino : x.AlunoFormacao.Titulacao.DescricaoFeminino,
                    DescricaoTitulacaoXSD = x.AlunoFormacao.Titulacao.DescricaoXSD,

                    ObservacaoFormacao = x.ObservacaoFormacao,
                    SeqEntidadeResponsavel = x.AlunoFormacao.FormacaoEspecifica.SeqEntidadeResponsavel,
                    SeqFormacaoEspecifica = x.AlunoFormacao.SeqFormacaoEspecifica,
                    DescricaoTipoFormacaoEspecifica = x.AlunoFormacao.FormacaoEspecifica.TipoFormacaoEspecifica.Descricao,
                    DescricaoFormacaoEspecifica = x.AlunoFormacao.FormacaoEspecifica.Descricao,
                    DataColacaoGrau = x.AlunoFormacao.DataColacaoGrau,
                    DataConclusao = x.AlunoFormacao.DataConclusao

                }).ToList(),

                UsuarioImpressao = a.UsuarioImpressao,
                DataImpressao = a.DataImpressao,
                NumeroSerie = a.NumeroSerie,

                TipoRegistroDocumento = a.TipoRegistroDocumento,
                NumeroProcesso = a.NumeroProcesso,
                GrupoRegistro = a.GrupoRegistro.Descricao,
                OrgaoDeRegistro = a.OrgaoRegistro.Sigla,
                NumeroRegistro = a.NumeroRegistro,
                DataRegistro = a.DataRegistro,
                UsuarioRegistro = a.UsuarioRegistro,
                Livro = a.Livro,
                Folha = a.Folha,

                ConfirmacaoAluno = (a.SolicitacaoServico as SolicitacaoDocumentoConclusao).ConfirmacaoAluno,

                Situacoes = a.Situacoes.OrderBy(o => o.DataInclusao).Select(x => new DocumentoConclusaoHistoricoListarVO()
                {
                    Seq = x.Seq,
                    SeqDocumentoConclusao = x.SeqDocumentoAcademico,
                    SeqSituacaoDocumentoAcademico = x.SeqSituacaoDocumentoAcademico,
                    DescricaoSituacaoDocumentoAcademico = x.SituacaoDocumentoAcademico.Descricao,
                    Token = x.SituacaoDocumentoAcademico.Token,
                    DataInclusao = x.DataInclusao,
                    UsuarioInclusao = x.UsuarioInclusao,
                    MotivoInvalidadeDocumento = x.MotivoInvalidadeDocumento,
                    Observacao = x.Observacao,
                    TipoInvalidade = x.ClassificacaoInvalidadeDocumento.TipoInvalidade,
                    DescricaoClassificacaoInvalidadeDocumento = x.ClassificacaoInvalidadeDocumento.Descricao
                }).ToList(),

                InformacoesAdicionais = a.Mensagens.OrderBy(o => o.DataInclusao).Select(s => new DocumentoConclusaoMensagemVO
                {
                    Seq = s.Seq,
                    Descricao = s.Mensagem.Descricao
                }).ToList()
            });

            retorno.HabilitarSecoes = retorno.SeqSolicitacaoServico.HasValue;

            if (retorno.SeqDocumentoConclusaoViaAnterior.HasValue)
            {
                var lista = new List<DocumentoConclusaoViaAnteriorListarVO>();
                BuscarDocumentoConclusaoViaAnterior(retorno.SeqDocumentoConclusaoViaAnterior.Value, ref lista);
                retorno.DocumentoConclusaoViaAnterior = lista.OrderByDescending(o => o.Seq).ToList();
            }

            if (retorno.FormacoesEspecificas.Any())
            {
                retorno.DescricaoNivelEnsino = retorno.FormacoesEspecificas.FirstOrDefault().DescricaoNivelEnsino;
                retorno.DescricaoCursoOferta = retorno.FormacoesEspecificas.FirstOrDefault().DescricaoCursoOferta;
                retorno.DescricaoDocumentoConclusao = retorno.FormacoesEspecificas.FirstOrDefault().DescricaoDocumentoConclusao;
                retorno.DescricaoGrauAcademico = retorno.FormacoesEspecificas.FirstOrDefault().DescricaoGrauAcademico;
                retorno.DescricaoTitulacao = retorno.TokenTipoDocumento == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL &&
                                             !string.IsNullOrEmpty(retorno.FormacoesEspecificas.FirstOrDefault().DescricaoTitulacaoXSD) ? retorno.FormacoesEspecificas.FirstOrDefault().DescricaoTitulacaoXSD : retorno.FormacoesEspecificas.FirstOrDefault().DescricaoTitulacao;

                foreach (var formacao in retorno.FormacoesEspecificas)
                {
                    //Busca a hierarquia da formação
                    if (formacao.SeqEntidadeResponsavel.HasValue)
                    {
                        formacao.FormacoesEspecificas = this.FormacaoEspecificaDomainService.BuscarDescricaoFormacaoEspecifica(formacao.SeqFormacaoEspecifica.GetValueOrDefault(), formacao.SeqEntidadeResponsavel).Select(x => x.DescricaoFormacaoEspecifica).ToList();
                    }
                    else
                    {
                        formacao.FormacoesEspecificas = new List<string> { $"[{formacao.DescricaoTipoFormacaoEspecifica}] {formacao.DescricaoFormacaoEspecifica}" };
                    }

                    if (retorno.DescricaoGrauAcademico == GRAU_ACADEMICO.BachareladoLicenciatura && retorno.TokenTipoDocumento == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL)
                    {
                        var listaAuxiliarFormacoesEspecificas = new List<string>();

                        foreach (var formacaoEspecifica in formacao.FormacoesEspecificas)
                        {
                            var auxiliarFormacaoEspecifica = $"{formacaoEspecifica} / [Dados do XML] {formacao.ObservacaoFormacao}";
                            listaAuxiliarFormacoesEspecificas.Add(auxiliarFormacaoEspecifica);
                        }
                        formacao.FormacoesEspecificas = listaAuxiliarFormacoesEspecificas;
                    }
                }
            }
            else if (retorno.SeqSolicitacaoServico.HasValue)
            {
                var dadosSolicitacao = this.SolicitacaoDocumentoConclusaoDomainService.SearchProjectionByKey(retorno.SeqSolicitacaoServico.Value, x => new
                {
                    DescricaoNivelEnsino = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.NivelEnsino.Descricao,
                    DescricaoCursoOferta = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                    DescricaoDocumentoConclusao = x.AlunoHistorico.Formacoes.OrderBy(o => o.DataInicio).FirstOrDefault().DescricaoDocumentoConclusao,
                    SeqFormacaoEspecifica = x.AlunoHistorico.Formacoes.OrderBy(o => o.DataInicio).Any() ? x.AlunoHistorico.Formacoes.OrderBy(o => o.DataInicio).FirstOrDefault().SeqFormacaoEspecifica : 0,
                    SeqCurso = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso,
                    DescricaoTitulacao = x.AlunoHistorico.Aluno.DadosPessoais.Sexo == Sexo.Masculino ? x.AlunoHistorico.Formacoes.OrderBy(o => o.DataInicio).FirstOrDefault().Titulacao.DescricaoMasculino : x.AlunoHistorico.Formacoes.OrderBy(o => o.DataInicio).FirstOrDefault().Titulacao.DescricaoFeminino,
                    DescricaoTitulacaoXSD = x.AlunoHistorico.Formacoes.OrderBy(o => o.DataInicio).FirstOrDefault().Titulacao.DescricaoXSD
                });

                retorno.DescricaoNivelEnsino = dadosSolicitacao.DescricaoNivelEnsino;
                retorno.DescricaoCursoOferta = dadosSolicitacao.DescricaoCursoOferta;
                retorno.DescricaoDocumentoConclusao = dadosSolicitacao.DescricaoDocumentoConclusao;

                var specCursoFormacaoEspecifica = new CursoFormacaoEspecificaFilterSpecification() { SeqCurso = dadosSolicitacao.SeqCurso, SeqFormacaoEspecifica = dadosSolicitacao.SeqFormacaoEspecifica };
                var cursoFormacaoEspecifica = this.CursoFormacaoEspecificaDomainService.SearchByKey(specCursoFormacaoEspecifica, x => x.GrauAcademico);

                if (cursoFormacaoEspecifica != null)
                    retorno.DescricaoGrauAcademico = cursoFormacaoEspecifica.GrauAcademico.Descricao;

                retorno.DescricaoTitulacao = retorno.TokenTipoDocumento == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL &&
                                             !string.IsNullOrEmpty(dadosSolicitacao.DescricaoTitulacaoXSD) ? dadosSolicitacao.DescricaoTitulacaoXSD : dadosSolicitacao.DescricaoTitulacao;
            }
            else
            {
                var dadosAluno = this.AlunoDomainService.SearchProjectionByKey(retorno.SeqPessoaAtuacao, x => new
                {
                    DescricaoNivelEnsino = x.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.NivelEnsino.Descricao,
                    DescricaoCursoOferta = x.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                    DescricaoDocumentoConclusao = x.Historicos.FirstOrDefault(f => f.Atual).Formacoes.OrderBy(o => o.DataInicio).FirstOrDefault().DescricaoDocumentoConclusao,
                    SeqFormacaoEspecifica = x.Historicos.FirstOrDefault(f => f.Atual).Formacoes.OrderBy(o => o.DataInicio).FirstOrDefault().SeqFormacaoEspecifica,
                    SeqCurso = x.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso,
                    DescricaoTitulacao = x.Historicos.FirstOrDefault(f => f.Atual).Aluno.DadosPessoais.Sexo == Sexo.Masculino ? x.Historicos.FirstOrDefault(f => f.Atual).Formacoes.OrderBy(o => o.DataInicio).FirstOrDefault().Titulacao.DescricaoMasculino : x.Historicos.FirstOrDefault(f => f.Atual).Formacoes.OrderBy(o => o.DataInicio).FirstOrDefault().Titulacao.DescricaoFeminino,
                    DescricaoTitulacaoXSD = x.Historicos.FirstOrDefault(f => f.Atual).Formacoes.OrderBy(o => o.DataInicio).FirstOrDefault().Titulacao.DescricaoXSD
                });

                retorno.DescricaoNivelEnsino = dadosAluno.DescricaoNivelEnsino;
                retorno.DescricaoCursoOferta = dadosAluno.DescricaoCursoOferta;
                retorno.DescricaoDocumentoConclusao = dadosAluno.DescricaoDocumentoConclusao;

                var specCursoFormacaoEspecifica = new CursoFormacaoEspecificaFilterSpecification() { SeqCurso = dadosAluno.SeqCurso, SeqFormacaoEspecifica = dadosAluno.SeqFormacaoEspecifica };
                var cursoFormacaoEspecifica = this.CursoFormacaoEspecificaDomainService.SearchByKey(specCursoFormacaoEspecifica, x => x.GrauAcademico);

                if (cursoFormacaoEspecifica != null)
                {
                    retorno.DescricaoGrauAcademico = cursoFormacaoEspecifica.GrauAcademico.Descricao;
                }

                retorno.DescricaoTitulacao = retorno.TokenTipoDocumento == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL &&
                                             !string.IsNullOrEmpty(dadosAluno.DescricaoTitulacaoXSD) ? dadosAluno.DescricaoTitulacaoXSD : dadosAluno.DescricaoTitulacao;
            }

            retorno.ExibirMensagemInformativaBachareladoLicenciatura = false;
            if (retorno.DescricaoGrauAcademico == GRAU_ACADEMICO.BachareladoLicenciatura && retorno.TokenTipoDocumento == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL)
                retorno.ExibirMensagemInformativaBachareladoLicenciatura = true;

            var documentoConclusaoFormacoes = this.DocumentoConclusaoFormacaoDomainService.SearchBySpecification(new DocumentoConclusaoFormacaoFilterSpecification() { SeqDocumentoConclusao = seqDocumentoConclusao }, x => x.AlunoFormacao.AlunoHistorico);

            if (documentoConclusaoFormacoes.Any())
            {
                var seqNivelEnsino = documentoConclusaoFormacoes.First().AlunoFormacao.AlunoHistorico.SeqNivelEnsino;
                var instituicaoNivel = this.InstituicaoNivelDomainService.BuscarInstituicaoNivelPorNivelEnsino(seqNivelEnsino);
                var instituicaoNivelTipoDocumentoAcademico = this.InstituicaoNivelTipoDocumentoAcademicoDomainService.SearchByKey(new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification() { SeqInstituicaoNivel = instituicaoNivel.Seq, SeqTipoDocumentoAcademico = retorno.SeqTipoDocumentoAcademico });

                if (instituicaoNivelTipoDocumentoAcademico != null)
                    retorno.HabilitarSecaoRegistro = instituicaoNivelTipoDocumentoAcademico.HabilitaRegistroDocumento;
            }

            retorno.HabilitarBotaoApostilamento = retorno.TokenTipoDocumento == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA;
            retorno.ExibirBotaoApostilamento = retorno.TokenTipoDocumento == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA;
            retorno.ExibirSecaoEntrega = retorno.TokenSituacaoDocumentoAcademicoAtual == TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.AGUARDANDO_ENTREGA || retorno.TokenSituacaoDocumentoAcademicoAtual == TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.ENTREGUE;
            retorno.ExibirSecaoHistoricosDownload = retorno.SeqSolicitacaoServico.HasValue;
            retorno.FiltroHistoricoDownload = new DocumentoConclusaoFiltroHistoricoDownloadVO() { SeqSolicitacaoDocumentoConclusao = retorno.SeqSolicitacaoServico.GetValueOrDefault(), TokenTipoDocumento = retorno.TokenTipoDocumento };

            retorno.ExibirBotaoAnularDocumentoDigital = false;
            retorno.ExibirBotaoRestaurarDocumentoDigital = false;
            retorno.ExibirMensagemRestaurarDocumentoDigital = false;

            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(retorno.SeqPessoaAtuacao);
            var tokenNivelEnsino = NivelEnsinoDomainService.SearchProjectionByKey(new SMCSeqSpecification<NivelEnsino>(dadosOrigem.SeqNivelEnsino), x => x.Token);

            retorno.Situacoes.ForEach(a =>
            {
                a.DescricaoSituacaoDocumentoAcademico = a.Token != TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.INVALIDO ? a.DescricaoSituacaoDocumentoAcademico : a.TipoInvalidade.HasValue ? $"{a.DescricaoSituacaoDocumentoAcademico} ({a.TipoInvalidade.SMCGetDescription()})" : a.DescricaoSituacaoDocumentoAcademico;

                if (a.Token == TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.INVALIDO)
                {
                    var motivoInvalidade = a.MotivoInvalidadeDocumento?.SMCGetDescription() ?? "-";
                    var classificacaoInvalidade = a.DescricaoClassificacaoInvalidadeDocumento ?? "-";
                    var observacao = !string.IsNullOrEmpty(a.Observacao) ? a.Observacao : "-";
                    a.MotivoInvalidadeObservacao = $"Motivo: {motivoInvalidade} <br>" +
                                                   $"Classificação Invalidade: {classificacaoInvalidade} <br>" +
                                                   $"Observações: {observacao}";
                }
                else
                {
                    a.MotivoInvalidadeObservacao = a.Observacao;
                }
            });

            if (tokenNivelEnsino == TOKEN_NIVEL_ENSINO.GRADUACAO && retorno.SeqDocumentoDiplomaGAD.HasValue &&
                retorno.TokenTipoDocumento == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL || retorno.TokenTipoDocumento == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_FINAL ||
                retorno.TokenTipoDocumento == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_PARCIAL || retorno.TokenTipoDocumento == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_2VIA)
            {
                if (retorno.TokenSituacaoDocumentoAcademicoAtual == TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.ENTREGUE)
                {
                    retorno.ExibirBotaoAnularDocumentoDigital = true;
                }

                if (retorno.TokenSituacaoDocumentoAcademicoAtual == TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.INVALIDO)
                {
                    if (retorno.TipoInvalidade.HasValue && retorno.TipoInvalidade == TipoInvalidade.Permanente)
                    {
                        retorno.ExibirMensagemRestaurarDocumentoDigital = true;
                        retorno.ExibirBotaoRestaurarDocumentoDigital = true;
                    }
                    else if (retorno.TipoInvalidade.HasValue && retorno.TipoInvalidade == TipoInvalidade.Temporario && retorno.SeqSolicitacaoServico.HasValue)
                    {
                        var specSolicitacaoServico = new SMCSeqSpecification<SolicitacaoServico>(retorno.SeqSolicitacaoServico.Value);
                        var solicitacaoServico = this.SolicitacaoServicoDomainService.SearchProjectionByKey(specSolicitacaoServico, s => new
                        {
                            s.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                            s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
                            s.SituacaoAtual.SeqSituacaoEtapaSgf,
                        });
                        var etapasSGF = SGFHelper.BuscarEtapasSGFCache(solicitacaoServico.SeqTemplateProcessoSgf);
                        var etapaAtual = etapasSGF.FirstOrDefault(f => f.Seq == solicitacaoServico.SeqEtapaSgf);
                        var situacao = etapaAtual.Situacoes.FirstOrDefault(f => f.Seq == solicitacaoServico.SeqSituacaoEtapaSgf);

                        if (situacao.ClassificacaoSituacaoFinal.HasValue && situacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                        {
                            retorno.ExibirBotaoAnularDocumentoDigital = true;
                            retorno.ExibirBotaoRestaurarDocumentoDigital = true;
                        }

                        if (situacao.ClassificacaoSituacaoFinal.HasValue && situacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado)
                            retorno.ExibirBotaoAnularDocumentoDigital = true;
                    }
                }
            }
            return retorno;
        }

        public SMCPagerData<DocumentoConclusaoHistoricoDownloadListarVO> BuscarHistoricosDownload(DocumentoConclusaoFiltroHistoricoDownloadVO filtro)
        {
            #region ORDENAÇÃO AUTOMÁTICA

            //var spec = filtro.Transform<SolicitacaoDocumentoConclusaoEntregaDigitalFilterSpecification>();
            //var lista = this.SolicitacaoDocumentoConclusaoEntregaDigitalDomainService.SearchProjectionBySpecification(spec, a => new DocumentoConclusaoHistoricoDownloadListarVO()
            //{
            //    Seq = a.Seq,
            //    SeqSolicitacaoDocumentoConclusao = a.SeqSolicitacaoDocumentoConclusao,
            //    TipoArquivoDigital = a.TipoArquivoDigital,
            //    DataInclusao = a.DataInclusao,
            //    UsuarioInclusao = a.UsuarioInclusao
            //}, out int total).ToList();

            //return new SMCPagerData<DocumentoConclusaoHistoricoDownloadListarVO>(lista, total);

            #endregion ORDENAÇÃO AUTOMÁTICA

            #region ORDENAÇÃO MANUAL

            /*A ORDENAÇÃO E PAGINAÇÃO DESTE MÉTODO FORAM FEITAS MANUALMENTE POIS O GRID ESTÁ EM UMA ABA E NÃO FUNCIONA
             CORRETAMENTE A ORDENAÇÃO AUTOMÁTICA*/

            var spec = filtro.Transform<SolicitacaoDocumentoConclusaoEntregaDigitalFilterSpecification>();
            spec.SetOrderBy(o => o.DataInclusao, SMCSortDirection.Ascending);

            if (filtro.TokenTipoDocumento == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL)
                spec.TiposArquivoDigital = new List<TipoArquivoDigital> {
                    TipoArquivoDigital.XmlDiplomaDigital,
                    TipoArquivoDigital.RepresentacaoVisualDiplomaDigital,
                    TipoArquivoDigital.RelatorioManifestoAssinaturasDiplomaDigital };
            else
                spec.TiposArquivoDigital = new List<TipoArquivoDigital> {
                    TipoArquivoDigital.XMLHistoricoEscolar,
                    TipoArquivoDigital.RepresentacaoVisualHistoricoEscolar,
                    TipoArquivoDigital.RelatorioManifestoAssinaturasHistoricoEscolar };

            //LIMPANDO A ORDENAÇÃO QUE SERÁ FEITA MANUALMENTE E SETANDO O MAXRESULTS PARA NÃO BUSCAR TODOS OS REGISTROS DA TABELA
            int qtdeRegistros = this.SolicitacaoDocumentoConclusaoEntregaDigitalDomainService.Count();
            spec.MaxResults = qtdeRegistros > 0 ? qtdeRegistros : int.MaxValue;
            spec.ClearOrderBy();

            var lista = this.SolicitacaoDocumentoConclusaoEntregaDigitalDomainService.SearchProjectionBySpecification(spec, a => new DocumentoConclusaoHistoricoDownloadListarVO()
            {
                Seq = a.Seq,
                SeqSolicitacaoDocumentoConclusao = a.SeqSolicitacaoDocumentoConclusao,
                TipoArquivoDigital = a.TipoArquivoDigital,
                DataInclusao = a.DataInclusao,
                UsuarioInclusao = a.UsuarioInclusao,
                EnderecoIP = a.EnderecoIP,
                NomeServidorHost = a.NomeServidorHost
            }).ToList();

            int total = lista.Count();

            //ORDENAÇÃO MANUAL EM TODA A LISTA, NÃO SOMENTE NA PÁGINA ATUAL
            List<SMCSortInfo> listaOrdenacao = filtro.PageSettings.SortInfo;

            foreach (var sort in listaOrdenacao)
            {
                if (sort.FieldName == nameof(DocumentoConclusaoHistoricoDownloadListarVO.TipoArquivoDigital))
                {
                    if (sort.Direction == SMCSortDirection.Ascending)
                        lista = lista.OrderBy(o => o.TipoArquivoDigital.SMCGetDescription()).ToList();
                    else
                        lista = lista.OrderByDescending(o => o.TipoArquivoDigital.SMCGetDescription()).ToList();
                }
                if (sort.FieldName == nameof(DocumentoConclusaoHistoricoDownloadListarVO.DataInclusao))
                {
                    if (sort.Direction == SMCSortDirection.Ascending)
                        lista = lista.OrderBy(o => o.DataInclusao).ToList();
                    else
                        lista = lista.OrderByDescending(o => o.DataInclusao).ToList();
                }
            }

            //CONFIGURAÇÃO DE PAGINAÇÃO, RECUPERANDO OS REGISTROS DA PÁGINA ATUAL
            lista = lista.Skip((filtro.PageSettings.PageIndex - 1) * filtro.PageSettings.PageSize).Take(filtro.PageSettings.PageSize).ToList();

            return new SMCPagerData<DocumentoConclusaoHistoricoDownloadListarVO>(lista, total);

            #endregion ORDENAÇÃO MANUAL
        }

        public void SalvarStatusDocumentoDigital(long seqDocumentoConclusao, string tokenAcao, string observacao, MotivoInvalidadeDocumento motivoInvalidadeDocumento, TipoInvalidade? tipoCancelamento, long? seqClassificacaoInvalidadeDocumento)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    var documentoConclusao = this.SearchByKey(new SMCSeqSpecification<DocumentoConclusao>(seqDocumentoConclusao), x => x.TipoDocumentoAcademico);

                    var usuarioInclusao = string.Empty;
                    var sequencialUsuario = SMCContext.User?.SMCGetSequencialUsuario();
                    var nomeReduzido = SMCContext.User?.SMCGetNomeReduzido();

                    if (sequencialUsuario != null && nomeReduzido != null)
                        usuarioInclusao = $"{sequencialUsuario}/{nomeReduzido.ToUpper()}";
                    else
                        usuarioInclusao = SMCContext.User?.Identity?.Name;

                    if (tokenAcao == TOKEN_ACAO_DOCUMENTO_DIGITAL.ANULAR_DOCUMENTO_DIGITAL)
                    {
                        var specSituacaoDocumentoAcademico = new SituacaoDocumentoAcademicoFilterSpecification() { Token = TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.INVALIDO };
                        var seqSituacaoDocumentoAcademico = SituacaoDocumentoAcademicoDomainService.SearchProjectionBySpecification(specSituacaoDocumentoAcademico, s => s.Seq).FirstOrDefault();

                        if (seqSituacaoDocumentoAcademico == 0)
                            throw new TokenSituacaoDocumentoAcademicoNaoEncontradoException(TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.INVALIDO);

                        var novoDocumentoAcademicoHistoricoSituacao = new DocumentoAcademicoHistoricoSituacao()
                        {
                            SeqDocumentoAcademico = seqDocumentoConclusao,
                            SeqSituacaoDocumentoAcademico = seqSituacaoDocumentoAcademico,
                            Observacao = observacao,
                            MotivoInvalidadeDocumento = motivoInvalidadeDocumento,
                            TipoInvalidade = tipoCancelamento,
                            SeqClassificacaoInvalidadeDocumento = seqClassificacaoInvalidadeDocumento
                        };

                        DocumentoAcademicoHistoricoSituacaoDomainService.SaveEntity(novoDocumentoAcademicoHistoricoSituacao);

                        var modeloCancelarDocumentoAcademico = new AtualizaStatusDocumentoAcademicoVO
                        {
                            SeqDocumentoAcademico = documentoConclusao.SeqDocumentoGAD.Value,
                            Observacao = observacao,
                            TipoCancelamento = tipoCancelamento.SMCGetDescription(),
                            UsuarioInclusao = usuarioInclusao
                        };

                        if (documentoConclusao.TipoDocumentoAcademico.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL)
                        {
                            if (seqClassificacaoInvalidadeDocumento.HasValue)
                            {
                                var descricaoXSD = ClassificacaoInvalidadeDocumentoDomainService.BuscarClassificacaoInvalidadeDocumentoXSD(seqClassificacaoInvalidadeDocumento.Value);
                                modeloCancelarDocumentoAcademico.MotivoCancelamento = descricaoXSD;
                            }

                            var retornoGAD = APIDiplomaGAD.Execute<object>("Cancelar", modeloCancelarDocumentoAcademico);
                            var retornoCancelarDiploma = JsonConvert.DeserializeObject<MensagemHttp>(retornoGAD.ToString());

                            if (!string.IsNullOrEmpty(retornoCancelarDiploma.ErrorMessage))
                                throw new Exception(retornoCancelarDiploma.ErrorMessage);
                        }
                        else if (documentoConclusao.TipoDocumentoAcademico.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_2VIA ||
                                 documentoConclusao.TipoDocumentoAcademico.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_FINAL ||
                                 documentoConclusao.TipoDocumentoAcademico.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_PARCIAL)
                        {
                            var retornoGAD = APIHistoricoGAD.Execute<object>("Cancelar", modeloCancelarDocumentoAcademico);
                            var retornoCancelarHistoricoEscolar = JsonConvert.DeserializeObject<MensagemHttp>(retornoGAD.ToString());

                            if (!string.IsNullOrEmpty(retornoCancelarHistoricoEscolar.ErrorMessage))
                                throw new Exception(retornoCancelarHistoricoEscolar.ErrorMessage);
                        }
                    }
                    else if (tokenAcao == TOKEN_ACAO_DOCUMENTO_DIGITAL.RESTAURAR_DOCUMENTO_DIGITAL)
                    {
                        var documentosConclusaoFormacao = this.DocumentoConclusaoFormacaoDomainService.SearchProjectionBySpecification(new DocumentoConclusaoFormacaoFilterSpecification() { SeqDocumentoConclusao = seqDocumentoConclusao }, x => new
                        {
                            DataInclusao = x.DataInclusao,
                            SeqCurso = x.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso,
                            SeqGrauAcademico = x.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica.FirstOrDefault(f => f.SeqFormacaoEspecifica == x.AlunoFormacao.SeqFormacaoEspecifica).SeqGrauAcademico,
                            SeqFormacaoEspecifica = x.AlunoFormacao.SeqFormacaoEspecifica,
                            DescricaoDocumento = x.AlunoFormacao.DescricaoDocumentoConclusao,
                            SeqTitulacao = x.AlunoFormacao.SeqTitulacao
                        }).ToList();

                        if (documentosConclusaoFormacao.Any())
                        {
                            var documentoConclusaoFormacao = documentosConclusaoFormacao.OrderBy(o => o.DataInclusao).First();

                            var seqCurso = documentoConclusaoFormacao.SeqCurso;
                            var seqGrauAcademico = documentoConclusaoFormacao.SeqGrauAcademico;
                            var seqFormacaoEspecifica = documentoConclusaoFormacao.SeqFormacaoEspecifica;
                            var descricaoDocumento = documentoConclusaoFormacao.DescricaoDocumento;
                            var seqTitulacao = documentoConclusaoFormacao.SeqTitulacao;

                            var specDocumentoConclusao = new DocumentoConclusaoFilterSpecification()
                            {
                                SeqPessoaAtuacao = documentoConclusao.SeqAtuacaoAluno,
                                SeqTipoDocumentoAcademico = documentoConclusao.SeqTipoDocumentoAcademico,
                                SeqCursoDocumentoFormacao = seqCurso,
                                SeqGrauAcademico = seqGrauAcademico,
                                SeqsFormacoesEspecificas = new List<long>() { seqFormacaoEspecifica },
                                DescricaoDocumento = descricaoDocumento,
                                SeqTitulacao = seqTitulacao
                            };

                            var documentosConclusao = this.SearchBySpecification(specDocumentoConclusao, x => x.SituacaoAtual.SituacaoDocumentoAcademico).ToList();

                            if (documentosConclusao.Any(a => a.Seq != seqDocumentoConclusao && (a.SituacaoAtual.SituacaoDocumentoAcademico.ClasseSituacaoDocumento == ClasseSituacaoDocumento.Valido || a.SituacaoAtual.SituacaoDocumentoAcademico.ClasseSituacaoDocumento == ClasseSituacaoDocumento.EmissaoEmAndamento)))
                                throw new RestauracaoNaoPermitidaException();
                        }

                        var specDocumentoAcademicoHistoricoSituacao = new DocumentoAcademicoHistoricoSituacaoFilterSpecification { SeqDocumentoAcademico = seqDocumentoConclusao };
                        var seqSituacaoDocumentoAcademico = DocumentoAcademicoHistoricoSituacaoDomainService.SearchBySpecification(specDocumentoAcademicoHistoricoSituacao)
                                                                                                            .OrderByDescending(a => a.Seq).Take(2).LastOrDefault().SeqSituacaoDocumentoAcademico;

                        var novoDocumentoAcademicoHistoricoSituacao = new DocumentoAcademicoHistoricoSituacao()
                        {
                            SeqDocumentoAcademico = seqDocumentoConclusao,
                            SeqSituacaoDocumentoAcademico = seqSituacaoDocumentoAcademico,
                            MotivoInvalidadeDocumento = motivoInvalidadeDocumento,
                            Observacao = observacao
                        };

                        DocumentoAcademicoHistoricoSituacaoDomainService.SaveEntity(novoDocumentoAcademicoHistoricoSituacao);

                        var modeloRestaurarDiploma = new
                        {
                            SeqDocumentoAcademico = documentoConclusao.SeqDocumentoGAD.Value,
                            Observacao = observacao,
                            UsuarioInclusao = usuarioInclusao
                        };

                        if (documentoConclusao.TipoDocumentoAcademico.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL)
                        {
                            var retornoGAD = APIDiplomaGAD.Execute<object>("Restaurar", modeloRestaurarDiploma);
                            var retornoRestaurarDiploma = JsonConvert.DeserializeObject<MensagemHttp>(retornoGAD.ToString());

                            if (!string.IsNullOrEmpty(retornoRestaurarDiploma.ErrorMessage))
                                throw new Exception(retornoRestaurarDiploma.ErrorMessage);
                        }
                        else if (documentoConclusao.TipoDocumentoAcademico.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_2VIA ||
                                 documentoConclusao.TipoDocumentoAcademico.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_FINAL ||
                                 documentoConclusao.TipoDocumentoAcademico.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_PARCIAL)
                        {
                            var retornoGAD = APIHistoricoGAD.Execute<object>("Restaurar", modeloRestaurarDiploma);
                            var retornoRestaurarDiploma = JsonConvert.DeserializeObject<MensagemHttp>(retornoGAD.ToString());

                            if (!string.IsNullOrEmpty(retornoRestaurarDiploma.ErrorMessage))
                                throw new Exception(retornoRestaurarDiploma.ErrorMessage);
                        }
                    }

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }

        public void BuscarDocumentoConclusaoViaAnterior(long seq, ref List<DocumentoConclusaoViaAnteriorListarVO> lista)
        {
            var documentoConclusaoAnterior = this.SearchProjectionByKey(new SMCSeqSpecification<DocumentoConclusao>(seq), x => new DocumentoConclusaoViaAnteriorListarVO()
            {
                Seq = x.Seq,
                SeqDocumentoAcademicoViaAnterior = x.SeqDocumentoAcademicoViaAnterior,
                NumeroViaDocumentoAnterior = x.NumeroViaDocumento + "°",
                DescricaoTipoDocumentoAnterior = x.TipoDocumentoAcademico.Descricao,
                OrgaoDeRegistroAnterior = x.OrgaoRegistro.Sigla,
                NumeroRegistroAnterior = x.NumeroRegistro,
                DataRegistroAnterior = x.DataRegistro,
                NumeroProcessoAnterior = x.NumeroProcesso,
                DescricaoSituacaoDocumentoAcademico = x.SituacaoAtual.SituacaoDocumentoAcademico.Descricao
            });

            lista.Add(documentoConclusaoAnterior);

            if (documentoConclusaoAnterior.SeqDocumentoAcademicoViaAnterior.HasValue)
                BuscarDocumentoConclusaoViaAnterior(documentoConclusaoAnterior.SeqDocumentoAcademicoViaAnterior.Value, ref lista);
        }

        public SMCPagerData<DocumentoConclusaoListarVO> BuscarDocumentosConclusao(DocumentoConclusaoFiltroVO filtro)
        {
            #region Ordenação automática

            //var spec = filtro.Transform<DocumentoConclusaoFilterSpecification>();

            //if (filtro.SeqPessoa.HasValue)
            //{
            //    var pessoa = this.PessoaDomainService.SearchByKey(new SMCSeqSpecification<Pessoa>(filtro.SeqPessoa.Value), x => x.Atuacoes);
            //    var listaSeqsPessoasAtuacoes = pessoa.Atuacoes.Select(a => a.Seq).ToList();
            //    spec.SeqsPessoasAtuacoes = listaSeqsPessoasAtuacoes;
            //}

            //spec.SetOrderBy(o => o.Aluno.DadosPessoais.Nome);
            //spec.SetOrderBy(o => o.Aluno.Historicos.FirstOrDefault(a => a.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome);
            //spec.SetOrderBy(o => o.TipoDocumentoAcademico.Descricao);
            //spec.SetOrderBy(o => o.NumeroViaDocumento);

            //var lista = this.SearchProjectionBySpecification(spec, a => new DocumentoConclusaoListarVO()
            //{
            //    Seq = a.Seq,
            //    NumeroRegistroAcademico = a.Aluno.NumeroRegistroAcademico,
            //    NomeAluno = a.Aluno.DadosPessoais.Nome,
            //    Sexo = a.Aluno.DadosPessoais.Sexo,
            //    SeqEntidadeCursoOfertaLocalidade = a.Aluno.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade,
            //    SeqTipoDocumentoAcademico = a.SeqTipoDocumentoAcademico,
            //    DescricaoTipoDocumentoAcademico = a.TipoDocumentoAcademico.Descricao,
            //    NumeroViaDocumento = a.NumeroViaDocumento,
            //    DescricaoSituacaoDocumentoAcademicoAtual = a.SituacaoAtual.SituacaoDocumentoAcademico.Descricao,
            //    //TODO: ANA - Esse comando será habilitado quando for especificado o caso de uso UC_CNC_002_01_02 - Manter Documento Registro Antigo
            //    //HabilitarBotaoExcluir = a.LancamentoHistorico && a.SituacaoAtual.SituacaoDocumentoAcademico..
            //}, out int total).ToList();

            //foreach (var documentoConclusao in lista)
            //{
            //    documentoConclusao.NomeEntidadeCursoOfertaLocalidade = documentoConclusao.SeqEntidadeCursoOfertaLocalidade.HasValue ? this.EntidadeDomainService.BuscarEntidadeNome(documentoConclusao.SeqEntidadeCursoOfertaLocalidade.Value) : string.Empty;

            //    var documentoConclusaoFormacoes = this.DocumentoConclusaoFormacaoDomainService.SearchProjectionByKey(new DocumentoConclusaoFormacaoFilterSpecification() { SeqDocumentoConclusao = documentoConclusao.Seq }, x => new
            //    {
            //        x.AlunoFormacao.AlunoHistorico.SeqNivelEnsino,
            //        x.AlunoFormacao.Titulacao.DescricaoMasculino,
            //        x.AlunoFormacao.Titulacao.DescricaoFeminino,
            //        x.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica.FirstOrDefault(f => f.SeqFormacaoEspecifica == x.AlunoFormacao.SeqFormacaoEspecifica).GrauAcademico.Descricao
            //    });

            //    if (documentoConclusaoFormacoes != null)
            //    {
            //        var instituicaoNivel = this.InstituicaoNivelDomainService.BuscarInstituicaoNivelPorNivelEnsino(documentoConclusaoFormacoes.SeqNivelEnsino);
            //        var instituicaoNivelTipoDocumentoAcademico = this.InstituicaoNivelTipoDocumentoAcademicoDomainService.SearchByKey(new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification() { SeqInstituicaoNivel = instituicaoNivel.Seq, SeqTipoDocumentoAcademico = documentoConclusao.SeqTipoDocumentoAcademico });

            //        if (instituicaoNivelTipoDocumentoAcademico != null)
            //            documentoConclusao.HabilitarBotaoApostilamento = instituicaoNivelTipoDocumentoAcademico.HabilitaRegistroDocumento;

            //        documentoConclusao.DescricaoGrauAcademico = documentoConclusaoFormacoes.Descricao;
            //        documentoConclusao.DescricaoTitulacao = documentoConclusao.Sexo == Sexo.Masculino ? documentoConclusaoFormacoes.DescricaoMasculino : documentoConclusaoFormacoes.DescricaoFeminino;
            //    }
            //}

            //return new SMCPagerData<DocumentoConclusaoListarVO>(lista, total);

            #endregion Ordenação automática

            #region Ordenação manual com specification

            ///*A ORDENAÇÃO E PAGINAÇÃO DESTE MÉTODO FORAM FEITAS MANUALMENTE, POIS ESTAVA DANDO ERRO AO ORDENAR
            //DIRETO NO SPECIFICATION, ENTÃO PARA CONSEGUIR ORDENAR DEPOIS DE PESQUISAR VIA LINQ, TEVE QUE SER MANUAL*/

            //var spec = filtro.Transform<DocumentoConclusaoFilterSpecification>();

            //if (filtro.SeqPessoa.HasValue)
            //{
            //    var pessoa = this.PessoaDomainService.SearchByKey(new SMCSeqSpecification<Pessoa>(filtro.SeqPessoa.Value), x => x.Atuacoes);
            //    var listaSeqsPessoasAtuacoes = pessoa.Atuacoes.Select(a => a.Seq).ToList();
            //    spec.SeqsPessoasAtuacoes = listaSeqsPessoasAtuacoes;
            //}

            ////LIMPANDO A ORDENAÇÃO QUE SERÁ FEITA MANUALMENTE E SETANDO O MAXRESULTS PARA NÃO BUSCAR TODOS OS REGISTROS DA TABELA
            //int qtdeRegistros = this.Count();
            //spec.MaxResults = qtdeRegistros > 0 ? qtdeRegistros : int.MaxValue;
            //spec.ClearOrderBy();

            //var lista = this.SearchProjectionBySpecification(spec, a => new DocumentoConclusaoListarVO()
            //{
            //    Seq = a.Seq,
            //    NumeroRegistroAcademico = a.Aluno.NumeroRegistroAcademico,
            //    NomeAluno = a.Aluno.DadosPessoais.Nome,
            //    Sexo = a.Aluno.DadosPessoais.Sexo,
            //    SeqEntidadeCursoOfertaLocalidade = a.Aluno.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade,
            //    NomeEntidadeCursoOfertaLocalidade = a.Aluno.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome,
            //    SeqTipoDocumentoAcademico = a.SeqTipoDocumentoAcademico,
            //    DescricaoTipoDocumentoAcademico = a.TipoDocumentoAcademico.Descricao,
            //    NumeroViaDocumento = a.NumeroViaDocumento,
            //    DescricaoSituacaoDocumentoAcademicoAtual = a.SituacaoAtual.SituacaoDocumentoAcademico.Descricao,
            //    TipoDocumentoToken = a.TipoDocumentoAcademico.Token,
            //    //TODO: ANA - Esse comando será habilitado quando for especificado o caso de uso UC_CNC_002_01_02 - Manter Documento Registro Antigo
            //    //HabilitarBotaoExcluir = a.LancamentoHistorico && a.SituacaoAtual.SituacaoDocumentoAcademico..
            //}).ToList();

            //foreach (var documentoConclusao in lista)
            //{
            //    documentoConclusao.NomeEntidadeCursoOfertaLocalidade = documentoConclusao.SeqEntidadeCursoOfertaLocalidade.HasValue ? this.EntidadeDomainService.BuscarEntidadeNome(documentoConclusao.SeqEntidadeCursoOfertaLocalidade.Value) : string.Empty;

            //    var documentoConclusaoFormacoes = this.DocumentoConclusaoFormacaoDomainService.SearchProjectionByKey(new DocumentoConclusaoFormacaoFilterSpecification() { SeqDocumentoConclusao = documentoConclusao.Seq }, x => new
            //    {
            //        x.AlunoFormacao.AlunoHistorico.SeqNivelEnsino,
            //        x.AlunoFormacao.Titulacao.DescricaoMasculino,
            //        x.AlunoFormacao.Titulacao.DescricaoFeminino,
            //        x.AlunoFormacao.Titulacao.DescricaoXSD,
            //        x.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica.FirstOrDefault(f => f.SeqFormacaoEspecifica == x.AlunoFormacao.SeqFormacaoEspecifica).GrauAcademico.Descricao
            //    });

            //    if (documentoConclusaoFormacoes != null)
            //    {
            //        var instituicaoNivel = this.InstituicaoNivelDomainService.BuscarInstituicaoNivelPorNivelEnsino(documentoConclusaoFormacoes.SeqNivelEnsino);
            //        var instituicaoNivelTipoDocumentoAcademico = this.InstituicaoNivelTipoDocumentoAcademicoDomainService.SearchByKey(new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification() { SeqInstituicaoNivel = instituicaoNivel.Seq, SeqTipoDocumentoAcademico = documentoConclusao.SeqTipoDocumentoAcademico });

            //        if (instituicaoNivelTipoDocumentoAcademico != null)
            //            documentoConclusao.HabilitarBotaoApostilamento = instituicaoNivelTipoDocumentoAcademico.HabilitaRegistroDocumento;

            //        documentoConclusao.DescricaoGrauAcademico = documentoConclusaoFormacoes.Descricao;
            //        documentoConclusao.DescricaoTitulacao = documentoConclusao.TipoDocumentoToken == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL && !string.IsNullOrEmpty(documentoConclusaoFormacoes.DescricaoXSD) ? documentoConclusaoFormacoes.DescricaoXSD :
            //                                                documentoConclusao.Sexo == Sexo.Masculino ? documentoConclusaoFormacoes.DescricaoMasculino : documentoConclusaoFormacoes.DescricaoFeminino;
            //    }
            //}

            //int total = lista.Count();

            ////ORDENAÇÃO MANUAL EM TODA A LISTA, NÃO SOMENTE NA PÁGINA ATUAL
            //lista = lista.OrderBy(o => o.NomeAluno)
            //             .ThenBy(o => o.NomeEntidadeCursoOfertaLocalidade)
            //             .ThenBy(o => o.DescricaoTipoDocumentoAcademico)
            //             .ThenBy(o => o.NumeroViaDocumento)
            //             .ToList();

            ////CONFIGURAÇÃO DE PAGINAÇÃO, RECUPERANDO OS REGISTROS DA PÁGINA ATUAL
            //lista = lista.Skip((filtro.PageSettings.PageIndex - 1) * filtro.PageSettings.PageSize).Take(filtro.PageSettings.PageSize).ToList();

            //return new SMCPagerData<DocumentoConclusaoListarVO>(lista, total);

            #endregion Ordenação manual com specification

            #region Ordenação manual com query

            /*A ORDENAÇÃO E PAGINAÇÃO DESTE MÉTODO FORAM FEITAS MANUALMENTE, POIS ESTAVA DANDO ERRO AO ORDENAR
            DIRETO NO SPECIFICATION, ENTÃO PARA CONSEGUIR ORDENAR DEPOIS DE PESQUISAR VIA LINQ, TEVE QUE SER MANUAL*/

            var queryCountDocumentos = QUERY_COUNT_DOCUMENTOS_CONCLUSAO;
            var queryBuscarDocumentos = QUERY_BUSCAR_DOCUMENTOS_CONCLUSAO;
            var condicoesQuery = string.Empty;

            if (filtro.SeqPessoa.HasValue)
            {
                var pessoa = this.PessoaDomainService.SearchByKey(new SMCSeqSpecification<Pessoa>(filtro.SeqPessoa.Value), x => x.Atuacoes);
                var listaSeqsPessoasAtuacoes = pessoa.Atuacoes.Select(a => a.Seq).ToList();

                condicoesQuery += $" and dc.seq_atuacao_aluno in ({string.Join(",", listaSeqsPessoasAtuacoes)})";
            }

            if (filtro.SeqsEntidadesResponsaveis != null && filtro.SeqsEntidadesResponsaveis.Any())
                condicoesQuery += $" and ah.seq_entidade_vinculo in ({string.Join(",", filtro.SeqsEntidadesResponsaveis)})";

            if (filtro.SeqCursoOfertaLocalidade.HasValue)
                condicoesQuery += $" and col.seq_entidade = {filtro.SeqCursoOfertaLocalidade.Value}";

            if (filtro.SeqTipoDocumentoAcademico.HasValue)
                condicoesQuery += $" and tdc.seq_tipo_documento_academico = {filtro.SeqTipoDocumentoAcademico.Value}";

            if (filtro.SeqSituacaoDocumentoAcademico.HasValue)
                condicoesQuery += $" and sdc.seq_situacao_documento_academico = {filtro.SeqSituacaoDocumentoAcademico.Value}";

            if (filtro.TipoInvalidade.HasValue)
                condicoesQuery += $" and dchs.idt_dom_tipo_invalidade = {(int)filtro.TipoInvalidade.Value}";

            var queryFinalCountDocumentos = $"{queryCountDocumentos} {condicoesQuery}";
            var totalAuxiliar = this.RawQuery<int?>(queryFinalCountDocumentos);
            int total = 0;

            if (totalAuxiliar.Any(a => a != null))
                total = totalAuxiliar.FirstOrDefault(a => a != null).Value;

            var queryFinalBuscarDocumentos = $"{queryBuscarDocumentos} {condicoesQuery}";
            queryFinalBuscarDocumentos += @"order by NomeAluno, NomeEntidadeCursoOfertaLocalidade, DescricaoTipoDocumentoAcademico, NumeroViaDocumento
                                            OFFSET (@pageIndex) ROWS
                                            FETCH NEXT (@pageCount) ROWS ONLY";

            var skip = (filtro.PageSettings.PageIndex - 1) * filtro.PageSettings.PageSize;
            var take = filtro.PageSettings.PageSize;

            var lista = this.RawQuery<DocumentoConclusaoListarVO>(queryFinalBuscarDocumentos,
                                            new SMCFuncParameter("pageIndex", skip),
                                            new SMCFuncParameter("pageCount", take));

            foreach (var documentoConclusao in lista)
            {
                if (documentoConclusao.Token == TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.INVALIDO && documentoConclusao.TipoInvalidade.HasValue)
                    documentoConclusao.DescricaoSituacaoDocumentoAcademicoAtual = $"{documentoConclusao.DescricaoSituacaoDocumentoAcademicoAtual} ({documentoConclusao.TipoInvalidade.SMCGetDescription()})";

                documentoConclusao.NomeEntidadeCursoOfertaLocalidade = documentoConclusao.SeqEntidadeCursoOfertaLocalidade.HasValue ? this.EntidadeDomainService.BuscarEntidadeNome(documentoConclusao.SeqEntidadeCursoOfertaLocalidade.Value) : string.Empty;

                var documentoConclusaoFormacoes = this.DocumentoConclusaoFormacaoDomainService.SearchProjectionByKey(new DocumentoConclusaoFormacaoFilterSpecification() { SeqDocumentoConclusao = documentoConclusao.Seq }, x => new
                {
                    x.AlunoFormacao.AlunoHistorico.SeqNivelEnsino,
                    x.AlunoFormacao.Titulacao.DescricaoMasculino,
                    x.AlunoFormacao.Titulacao.DescricaoFeminino,
                    x.AlunoFormacao.Titulacao.DescricaoXSD,
                    x.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica.FirstOrDefault(f => f.SeqFormacaoEspecifica == x.AlunoFormacao.SeqFormacaoEspecifica).GrauAcademico.Descricao
                });

                if (documentoConclusaoFormacoes != null)
                {
                    documentoConclusao.DescricaoGrauAcademico = documentoConclusaoFormacoes.Descricao;
                    documentoConclusao.DescricaoTitulacao = documentoConclusao.TipoDocumentoToken == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL && !string.IsNullOrEmpty(documentoConclusaoFormacoes.DescricaoXSD) ? documentoConclusaoFormacoes.DescricaoXSD :
                                                            documentoConclusao.Sexo == Sexo.Masculino ? documentoConclusaoFormacoes.DescricaoMasculino : documentoConclusaoFormacoes.DescricaoFeminino;
                }
            }

            return new SMCPagerData<DocumentoConclusaoListarVO>(lista, total);

            #endregion Ordenação manual com query
        }

        public DocumentoConclusaoCabecalhoVO BuscarDocumentoConclusaoCabecalho(long seq)
        {
            var retorno = this.SearchProjectionByKey(new SMCSeqSpecification<DocumentoConclusao>(seq), x => new DocumentoConclusaoCabecalhoVO()
            {
                Seq = x.Seq,
                DescricaoTipoDocumentoAcademico = x.TipoDocumentoAcademico.Descricao,
                SeqDocumentoAcademicoHistoricoSituacaoAtual = x.SeqDocumentoAcademicoHistoricoSituacaoAtual,
                DescricaoSituacaoDocumentoAcademicoAtual = x.SituacaoAtual.SituacaoDocumentoAcademico.Descricao,
                NumeroProcesso = x.NumeroProcesso,
                NumeroViaDocumento = x.NumeroViaDocumento,
                TipoRegistroDocumento = x.TipoRegistroDocumento,
                OrgaoDeRegistro = x.OrgaoRegistro.Sigla,
                NumeroRegistro = x.NumeroRegistro,
                DataRegistro = x.DataRegistro,
                Livro = x.Livro,
                Folha = x.Folha
            });

            return retorno;
        }

        public void Excluir(long seq)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    var configToDelete = this.SearchByKey(new SMCSeqSpecification<DocumentoConclusao>(seq));
                    this.DeleteEntity(configToDelete);

                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }

        public void AtualizarDocumentoAcademicoDiplomaOuHistoricoDigital(long seqDocumentoAcademicoGAD)
        {
            var spec = new DocumentoConclusaoFilterSpecification
            {
                SeqDocumentoDiplomaGAD = seqDocumentoAcademicoGAD,
                ClasseSituacaoDocumento = ClasseSituacaoDocumento.EmissaoEmAndamento
            };

            var documentoConclusao = this.SearchProjectionBySpecification(spec, s => new
            {
                s.Seq,
                s.SeqAtuacaoAluno,
                s.SeqSolicitacaoServico,
                s.NumeroViaDocumento,
                s.TipoDocumentoAcademico.Token
            }).FirstOrDefault();

            if (documentoConclusao != null)
            {
                var specSituacaoDocumentoAcademico = new SituacaoDocumentoAcademicoFilterSpecification() { Token = TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.AGUARDANDO_ENTREGA };
                var seqSituacaoDocumentoAcademico = SituacaoDocumentoAcademicoDomainService.SearchProjectionBySpecification(specSituacaoDocumentoAcademico, s => s.Seq).FirstOrDefault();

                if (seqSituacaoDocumentoAcademico == 0)
                    throw new TokenSituacaoDocumentoAcademicoNaoEncontradoException(TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.AGUARDANDO_ENTREGA);

                using (var transacao = SMCUnitOfWork.Begin())
                {
                    var documentoAcademicoHistoricoSituacao = new DocumentoAcademicoHistoricoSituacao()
                    {
                        SeqDocumentoAcademico = documentoConclusao.Seq,
                        SeqSituacaoDocumentoAcademico = seqSituacaoDocumentoAcademico
                    };

                    DocumentoAcademicoHistoricoSituacaoDomainService.SaveEntity(documentoAcademicoHistoricoSituacao);

                    if (documentoConclusao.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL && documentoConclusao.NumeroViaDocumento == 1)
                        PessoaAtuacaoBloqueioDomainService.VerificaBloqueioPendenciaBiblioteca(documentoConclusao.SeqAtuacaoAluno);

                    if (documentoConclusao.SeqSolicitacaoServico.HasValue)
                        SolicitacaoDocumentoConclusaoDomainService.FinalizarSolicitacaoDiplomaDigital(documentoConclusao.SeqSolicitacaoServico.Value);

                    transacao.Commit();
                }
            }
        }

        public EntregaDocumentoDigitalPaginaVO BuscarDadosEntregaDocumentoDigital(long seqSolicitacaoServico, bool agrupar = false)
        {
            var retorno = new EntregaDocumentoDigitalPaginaVO();

            var spec = new DocumentoConclusaoFilterSpecification() { SeqSolicitacaoServico = seqSolicitacaoServico };
            var documentosSolicitacao = this.SearchProjectionBySpecification(spec, x => new
            {
                x.Seq,
                TokenSituacaoDocumentoAcademicoAtual = x.SituacaoAtual.SituacaoDocumentoAcademico.Token,
                TokenTipoDocumentoAcademico = x.TipoDocumentoAcademico.Token,
                x.DataInclusao,
                x.SeqAtuacaoAluno,
                x.SeqPessoaDadosPessoais,
                x.NumeroViaDocumento,
                x.SolicitacaoServico.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso,
                DescricaoCurso = x.SolicitacaoServico.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.Nome,
                x.SeqDocumentoGAD,
                FormacoesDocumento = x.FormacoesEspecificas.Select(y => new
                {
                    y.SeqDocumentoConclusao,
                    y.AlunoFormacao.DataInicio,
                    y.AlunoFormacao.SeqFormacaoEspecifica,
                    DescricaoFormacaoEspecifica = y.AlunoFormacao.FormacaoEspecifica.Descricao,
                    y.ObservacaoFormacao,
                    DescricaoTipoFormacaoEspecifica = y.AlunoFormacao.FormacaoEspecifica.TipoFormacaoEspecifica.Descricao,
                    TokenTipoFormacaoEspecifica = y.AlunoFormacao.FormacaoEspecifica.TipoFormacaoEspecifica.Token,
                    y.AlunoFormacao.DescricaoDocumentoConclusao,
                    y.AlunoFormacao.DataColacaoGrau,
                    DescricaoTitulacaoXSD = y.AlunoFormacao.Titulacao.DescricaoXSD,
                    DescricaoTitulacaoPorSexo = x.Aluno.DadosPessoais.Sexo == Sexo.Masculino ? y.AlunoFormacao.Titulacao.DescricaoMasculino : y.AlunoFormacao.Titulacao.DescricaoFeminino
                })
            }).ToList();

            var documentosAguardandoEntrega = documentosSolicitacao.Where(a => a.TokenSituacaoDocumentoAcademicoAtual == TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.AGUARDANDO_ENTREGA).ToList();

            if (documentosAguardandoEntrega.Any())
            {
                long seqAtuacaoAluno = 0;
                long? seqPessoaDadosPessoais = null;

                if (documentosAguardandoEntrega.Count() > 1)
                {
                    var documentoConclusao = documentosAguardandoEntrega.OrderBy(a => a.DataInclusao).FirstOrDefault();
                    seqAtuacaoAluno = documentoConclusao.SeqAtuacaoAluno;
                    seqPessoaDadosPessoais = documentoConclusao.SeqPessoaDadosPessoais;
                }
                else
                {
                    var documentoConclusao = documentosAguardandoEntrega.FirstOrDefault();
                    seqAtuacaoAluno = documentoConclusao.SeqAtuacaoAluno;
                    seqPessoaDadosPessoais = documentoConclusao.SeqPessoaDadosPessoais;
                }

                var pessoaAtuacao = PessoaAtuacaoDomainService.SearchByKey(new SMCSeqSpecification<PessoaAtuacao>(seqAtuacaoAluno), x => x.Pessoa.Filiacao);
                var pessoaDadosPessoais = PessoaDadosPessoaisDomainService.SearchByKey(new SMCSeqSpecification<PessoaDadosPessoais>(seqPessoaDadosPessoais.GetValueOrDefault()));

                if (!string.IsNullOrEmpty(pessoaAtuacao.Pessoa.Cpf))
                    retorno.Cpf = SMCMask.ApplyMaskCPF(pessoaAtuacao.Pessoa.Cpf.Trim());

                retorno.DataNascimento = pessoaAtuacao.Pessoa.DataNascimento;
                retorno.NumeroPassaporte = pessoaAtuacao.Pessoa.NumeroPassaporte;

                if (pessoaAtuacao.Pessoa.CodigoPaisEmissaoPassaporte.GetValueOrDefault() > 0)
                {
                    var dadosPaisPassaporte = LocalidadeService.BuscarPais(pessoaAtuacao.Pessoa.CodigoPaisEmissaoPassaporte.Value);
                    if (dadosPaisPassaporte != null)
                        retorno.NomePaisEmissaoPassaporte = dadosPaisPassaporte.Nome;
                }

                if (pessoaAtuacao.Pessoa.CodigoNacionalidade.GetValueOrDefault() > 0)
                {
                    var nacionalidade = this.PessoaService.BuscarNacionalidade(Convert.ToByte(pessoaAtuacao.Pessoa.CodigoNacionalidade.Value));
                    if (nacionalidade != null)
                        retorno.DescricaoNacionalidade = nacionalidade.Descricao;
                }

                retorno.Filiacao = pessoaAtuacao.Pessoa.Filiacao.Select(f => new EntregaDocumentoDigitalFiliacaoPaginaVO
                {
                    Seq = f.Seq,
                    SeqPessoa = f.SeqPessoa,
                    Nome = f.Nome,
                    TipoParentesco = f.TipoParentesco
                }).ToList();

                if (pessoaDadosPessoais != null)
                {
                    bool exibirNomeSocial = ExibirNomeSocial(pessoaDadosPessoais.SeqPessoa, TipoPessoa.Fisica);

                    retorno.NomeAluno = pessoaDadosPessoais.Nome;
                    retorno.NomeSocial = exibirNomeSocial ? pessoaDadosPessoais.NomeSocial : string.Empty;
                    retorno.Sexo = pessoaDadosPessoais.Sexo;
                    retorno.NumeroIdentidade = pessoaDadosPessoais.NumeroIdentidade;
                    retorno.OrgaoEmissorIdentidade = pessoaDadosPessoais.OrgaoEmissorIdentidade;
                    retorno.UfIdentidade = pessoaDadosPessoais.UfIdentidade;

                    if (pessoaAtuacao.Pessoa.CodigoPaisNacionalidade > 0)
                    {
                        var pais = this.LocalidadeService.BuscarPais((int)pessoaAtuacao.Pessoa.CodigoPaisNacionalidade);
                        if (pais != null)
                        {
                            if (pais.Nome.Trim().ToUpper() == NOME_PAIS.BRASIL)
                            {
                                if (pessoaDadosPessoais.CodigoCidadeNaturalidade.GetValueOrDefault() > 0 && !string.IsNullOrEmpty(pessoaDadosPessoais.UfNaturalidade))
                                {
                                    var cidade = this.LocalidadeService.BuscarCidade(pessoaDadosPessoais.CodigoCidadeNaturalidade.GetValueOrDefault(), pessoaDadosPessoais.UfNaturalidade);
                                    retorno.Naturalidade = $"{cidade?.Nome.Trim()} - {pessoaDadosPessoais.UfNaturalidade.Trim()}";
                                }
                            }
                            else
                            {
                                retorno.Naturalidade = pessoaDadosPessoais.DescricaoNaturalidadeEstrangeira;
                            }
                        }
                    }
                }

                retorno.DocumentosConclusao = new List<EntregaDocumentoDigitalDocumentoConclusaoPaginaVO>();

                foreach (var documento in documentosAguardandoEntrega)
                {
                    var formacao = documento.FormacoesDocumento.OrderBy(o => o.DataInicio).FirstOrDefault();

                    EntregaDocumentoDigitalDocumentoConclusaoPaginaVO documentoConclusao = new EntregaDocumentoDigitalDocumentoConclusaoPaginaVO
                    {
                        SeqDocumentoConclusao = documento.Seq,
                        NomeDiplomado = retorno.NomeAluno,
                        DescricaoCurso = formacao?.DescricaoDocumentoConclusao,
                        DescricaoTitulacao = !string.IsNullOrEmpty(formacao?.DescricaoTitulacaoXSD) ? formacao?.DescricaoTitulacaoXSD : formacao?.DescricaoTitulacaoPorSexo,
                        DataColacaoGrau = formacao?.DataColacaoGrau,
                        NumeroVia = $"{documento.NumeroViaDocumento}°",
                        TokenTipoDocumentoAcademico = documento.TokenTipoDocumentoAcademico,

                        DiplomaDigital = false
                    };

                    if (documento.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL)
                        documentoConclusao.DiplomaDigital = true;

                    documentoConclusao.ExibirGrauTitulacao = true;
                    if (formacao != null)
                    {
                        var specCursoFormacaoEspecifica = new CursoFormacaoEspecificaFilterSpecification() { SeqCurso = documento.SeqCurso, SeqFormacaoEspecifica = formacao.SeqFormacaoEspecifica };
                        var cursoFormacaoEspecifica = this.CursoFormacaoEspecificaDomainService.SearchByKey(specCursoFormacaoEspecifica, x => x.GrauAcademico);
                        if (cursoFormacaoEspecifica != null)
                        {
                            documentoConclusao.DescricaoGrauAcademico = cursoFormacaoEspecifica.GrauAcademico.Descricao;

                            if (documentoConclusao.DescricaoGrauAcademico == GRAU_ACADEMICO.BachareladoLicenciatura)
                                documentoConclusao.ExibirGrauTitulacao = false;
                        }
                    }
                    else
                    {
                        //Histórico pode não ter formação, então recupera os dados do curso sem passar pela formação
                        documentoConclusao.DescricaoCurso = documento.DescricaoCurso;
                    }

                    if (documento.SeqDocumentoGAD.HasValue)
                    {
                        var retornoCodigoVerificacao = BuscarCodigoVerificacaoResponseVO(documento.SeqDocumentoGAD.Value);
                        if (documentoConclusao.DiplomaDigital)
                        {
                            documentoConclusao.CodigoValidacaoDiploma = retornoCodigoVerificacao.CodigoVerificacao;
                        }
                        else
                        {
                            documentoConclusao.CodigoValidacaoHistorico = retornoCodigoVerificacao.CodigoVerificacao;

                            var retornoValidarHistoricoEscolar = APIHistoricoGAD.Execute<LinkValidarHistoricoEscolarResponseVO>($"ValidarHistoricoEscolar/{retornoCodigoVerificacao.CodigoVerificacao}", method: Method.GET);
                            documentoConclusao.UrlConsultaPublicaHistorico = retornoValidarHistoricoEscolar.LinkValidarlHistoricoEscolar;
                        }
                    }

                    documentoConclusao.FormacoesEspecificas = new List<EntregaDocumentoDigitalFormacaoPaginaVO>();

                    foreach (var formacaoDocumento in documento.FormacoesDocumento)
                    {
                        EntregaDocumentoDigitalFormacaoPaginaVO entregaDocumentoDigitalFormacao = new EntregaDocumentoDigitalFormacaoPaginaVO
                        {
                            SeqFormacaoEspecifica = formacaoDocumento.SeqFormacaoEspecifica,
                            SeqDocumentoConclusao = documento.Seq
                        };

                        if (documentoConclusao.DescricaoGrauAcademico == GRAU_ACADEMICO.BachareladoLicenciatura)
                        {
                            entregaDocumentoDigitalFormacao.DescricaoFormacaoEspefica = $"[{formacaoDocumento.DescricaoTipoFormacaoEspecifica}] {formacaoDocumento.DescricaoFormacaoEspecifica.Trim()}";
                            if (!string.IsNullOrEmpty(formacaoDocumento.ObservacaoFormacao))
                                entregaDocumentoDigitalFormacao.DescricaoFormacaoEspefica += $" / {formacaoDocumento.ObservacaoFormacao}";
                        }
                        else
                        {
                            if (formacaoDocumento.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.APROFUNDAMENTO ||
                                formacaoDocumento.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.ENFASE ||
                                formacaoDocumento.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.HABILITACAO ||
                                formacaoDocumento.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.LINHA_FORMACAO)
                            {
                                entregaDocumentoDigitalFormacao.DescricaoFormacaoEspefica = $"{formacaoDocumento.DescricaoTipoFormacaoEspecifica} em {formacaoDocumento.DescricaoFormacaoEspecifica}";
                            }
                            else
                            {
                                entregaDocumentoDigitalFormacao.DescricaoFormacaoEspefica = formacaoDocumento.DescricaoFormacaoEspecifica;
                            }
                        }
                        documentoConclusao.FormacoesEspecificas.Add(entregaDocumentoDigitalFormacao);
                    }
                    retorno.DocumentosConclusao.Add(documentoConclusao);
                }
                retorno.DocumentosConclusao = retorno.DocumentosConclusao.OrderBy(o => o.DataColacaoGrau).ToList();
            }

            if (agrupar)
                retorno.DocumentosConclusao = AgruparDiplomaHistoricoEscolar(retorno.DocumentosConclusao);

            return retorno;
        }

        private List<EntregaDocumentoDigitalDocumentoConclusaoPaginaVO> AgruparDiplomaHistoricoEscolar(List<EntregaDocumentoDigitalDocumentoConclusaoPaginaVO> documentosConclusao)
        {
            var listaTokensHistorico = new List<string> { TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_PARCIAL, TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_FINAL, TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_2VIA };

            var diploma = documentosConclusao.Where(a => a.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL).ToList();
            var historico = documentosConclusao.Where(a => listaTokensHistorico.Contains(a.TokenTipoDocumentoAcademico)).ToList();
            if (diploma.Any() && historico.Any())
            {
                var agruparPorFormacaoDescricao = documentosConclusao.SelectMany(s => s.FormacoesEspecificas).GroupBy(g => g.DescricaoFormacaoEspefica).ToList();
                var documentoConclusaoAuxiliar = new List<EntregaDocumentoDigitalDocumentoConclusaoPaginaVO>();
                var diplomaAuxiliar = new EntregaDocumentoDigitalDocumentoConclusaoPaginaVO();

                foreach (var formacaoDescricao in agruparPorFormacaoDescricao)
                {
                    var seqsDocumentoConclusao = formacaoDescricao.Select(s => s.SeqDocumentoConclusao);
                    diplomaAuxiliar = diploma.Where(w => seqsDocumentoConclusao.Contains(w.SeqDocumentoConclusao)).FirstOrDefault();

                    foreach (var item in formacaoDescricao)
                    {
                        if (item.SeqDocumentoConclusao != diplomaAuxiliar.SeqDocumentoConclusao)
                        {
                            var historicoAuxiliar = historico.Where(w => w.SeqDocumentoConclusao == item.SeqDocumentoConclusao).FirstOrDefault();
                            diplomaAuxiliar.CodigoValidacaoHistorico = historicoAuxiliar.CodigoValidacaoHistorico;
                            diplomaAuxiliar.UrlConsultaPublicaHistorico = historicoAuxiliar.UrlConsultaPublicaHistorico;
                        }
                    }
                    if (!documentoConclusaoAuxiliar.Contains(diplomaAuxiliar))
                        documentoConclusaoAuxiliar.Add(diplomaAuxiliar);
                }
                documentosConclusao = documentoConclusaoAuxiliar;
            }

            return documentosConclusao.OrderBy(o => o.DataColacaoGrau).ToList();
        }

        public List<SMCDatasourceItem> BuscarTiposDocumentoEntregaDigital()
        {
            var token = TOKEN_TIPO_DOCUMENTO_ENTREGA_DIGITAL.DOCUMENTOS_IDENTIFICACAO_PESSOAL;
            var tipoDocumento = TipoDocumentoService.BuscarTipoDocumentoPorToken(token);

            var retorno = new List<SMCDatasourceItem>()
            {
                new SMCDatasourceItem()
                {
                    Seq = tipoDocumento.Seq,
                    Descricao = tipoDocumento.Descricao
                }
            };

            return retorno;
        }

        public void SalvarEntregaDocumentoDigital(EntregaDocumentoDigitalPaginaVO modelo)
        {
            ValidarModeloEntregaDocumentoDigital(modelo);

            var specSolicitacao = new SMCSeqSpecification<SolicitacaoServico>(modelo.SeqSolicitacaoServicoAuxiliar);
            var dadosSolicitacao = this.SolicitacaoServicoDomainService.SearchProjectionByKey(specSolicitacao, x => new
            {
                x.NumeroProtocolo,
                x.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
                x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.SeqProcessoEtapa,
                x.SituacaoAtual.SolicitacaoServicoEtapa.SeqConfiguracaoEtapa,
                x.SituacaoAtual.SeqSolicitacaoServicoEtapa,
                x.SeqPessoaAtuacao,
                x.ConfiguracaoProcesso.Processo.SeqServico,
                NomeAluno = x.PessoaAtuacao.DadosPessoais.Nome,
                EmailsAluno = x.PessoaAtuacao.EnderecosEletronicos.Select(e => e.EnderecoEletronico).Where(w => w.TipoEnderecoEletronico == TipoEnderecoEletronico.Email).ToList(),
                x.PessoaAtuacao.DadosPessoais.NomeSocial,
                DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao,
                x.UsuariosResponsaveis.OrderByDescending(o => o.Seq).FirstOrDefault().SeqUsuarioResponsavel,
            });

            var tiposNotificacaoServico = TipoNotificacaoDomainService.BuscarTiposNotificacaoPorServicoSelect(dadosSolicitacao.SeqServico);
            var seqsTiposNotificacaoServico = tiposNotificacaoServico.Select(a => a.Seq).ToArray();
            var tiposNotificacao = NotificacaoService.BuscarTiposNotificacao(seqsTiposNotificacaoServico);

            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosSolicitacao.SeqPessoaAtuacao);

            var nomePessoaTag = string.Empty;

            if (!string.IsNullOrEmpty(dadosSolicitacao.NomeSocial))
                nomePessoaTag = dadosSolicitacao.NomeSocial;
            else
                nomePessoaTag = dadosSolicitacao.NomeAluno;

            var cancelarSolicitacao = false;
            var modeloCancelarSolicitacao = new CancelamentoSolicitacaoVO();

            using (var transaction = SMCUnitOfWork.Begin())
            {
                try
                {
                    //1.Se o valor do campo "Os dados pessoais estão corretos?” for igual a “Não”
                    if (!modelo.ConfirmacaoAluno.GetValueOrDefault())
                    {
                        //1.1. Cancelar/Anular a solicitação-serviço, documento-conclusão, documento-gad, serviço-sga,
                        //conforme RN_SRC_025 – Solicitações - Consistências quando cancelada a solicitação...

                        //Motivo cancelamento
                        var tokenAlunoIndeferiuDados = TOKEN_MOTIVO_SITUACAO_CANCELAMENTO.ALUNO_INDEFERIU_DADOS_DOCUMENTO;

                        var spec = new SMCSeqSpecification<SolicitacaoServico>(modelo.SeqSolicitacaoServicoAuxiliar);
                        var seqTemplateProcessoSGF = this.SolicitacaoServicoDomainService.SearchProjectionByKey(spec, s => s.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf);

                        var templateProcesso = TipoTemplateProcessoService.BuscarTemplateProcesso(seqTemplateProcessoSGF).FirstOrDefault();
                        var seqSituacao = SituacaoService.BuscarSeqSituacaoPorToken(TOKEN_SITUACAO_SGF.SOLICITACAO_CANCELADA, templateProcesso.SeqTipoTemplateProcesso);
                        var seqMotivoCancelamento = SituacaoService.BuscarSeqMotivoSituacaoPorToken(seqSituacao.Value, tokenAlunoIndeferiuDados);

                        if (seqMotivoCancelamento.GetValueOrDefault() == 0)
                            throw new TokenMotivoSolicitacaoIndeferidaNaoEncontradoException(tokenAlunoIndeferiuDados);

                        //Situação documento conclusão
                        var specSituacaoDocumentoAcademico = new SituacaoDocumentoAcademicoFilterSpecification() { Token = TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.INVALIDO };
                        var seqSituacaoDocumentoAcademico = SituacaoDocumentoAcademicoDomainService.SearchProjectionBySpecification(specSituacaoDocumentoAcademico, s => s.Seq).FirstOrDefault();

                        if (seqSituacaoDocumentoAcademico == 0)
                            throw new TokenSituacaoDocumentoAcademicoNaoEncontradoException(TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.INVALIDO);

                        var tipoDocumentoAcademico = SolicitacaoDocumentoConclusaoDomainService.SearchProjectionByKey(modelo.SeqSolicitacaoServicoAuxiliar, x => new
                        {
                            x.TipoDocumentoAcademico.Token
                        });

                        /*Setando as propriedades para cancelar solicitação após o fechamento da unidade de trabalho. Estava dando erro ao
                        cancelar a solicitação nesse momento, não suportando uma transação dentro da outra. Como o método de salvar cancelamento
                        é usado em várias partes, não foi alterado para usar ou não uma unidade trabalho como é feito no GAD.*/
                        cancelarSolicitacao = true;

                        var seqClassificacaoInvalidadeDocumento = ClassificacaoInvalidadeDocumentoDomainService.BuscarSeqClassificacaoInvalidadeDocumentoPorToken(TOKEN_CLASSIFICACAO_INVALIDADE_DOCUMENTO.ERRO_DE_FATO);
                        modeloCancelarSolicitacao = new CancelamentoSolicitacaoVO()
                        {
                            SeqSolicitacaoServico = modelo.SeqSolicitacaoServicoAuxiliar,
                            TipoCancelamento = TipoInvalidade.Temporario,
                            SeqMotivoCancelamento = seqMotivoCancelamento,
                            TokenMotivoCancelamento = tokenAlunoIndeferiuDados,
                            Observacao = modelo.Observacao,
                            SeqSituacaoDocumentoAcademico = seqSituacaoDocumentoAcademico,
                            MotivoInvalidadeDocumento = MotivoInvalidadeDocumento.IndeferidoPeloAluno,
                            ObservacaoDocumentoGAD = $"Aluno indeferiu os dados pessoais. Observações: {modelo.Observacao}",
                            SeqClassificacaoInvalidadeDocumento = seqClassificacaoInvalidadeDocumento
                        };

                        //1.2. Associar os documentos anexados na respectiva solicitação-serviço e atualizar a situação da
                        //documentação para "Entregue"
                        if (modelo.DocumentosUpload != null && modelo.DocumentosUpload.Count() > 0)
                        {
                            var token = TOKEN_TIPO_DOCUMENTO_ENTREGA_DIGITAL.DOCUMENTOS_IDENTIFICACAO_PESSOAL;
                            var tipoDocumento = TipoDocumentoService.BuscarTipoDocumentoPorToken(token);

                            if (tipoDocumento == null)
                                throw new DocumentoConclusaoEntregaInformacaoNaoEncontradaException($"tipo de documento ({TOKEN_TIPO_DOCUMENTO_ENTREGA_DIGITAL.DOCUMENTOS_IDENTIFICACAO_PESSOAL})");

                            var specDocumentoRequerido = new DocumentoRequeridoFilterSpecification()
                            {
                                SeqConfiguracaoEtapa = dadosSolicitacao.SeqConfiguracaoEtapa,
                                SeqTipoDocumento = tipoDocumento.Seq
                            };
                            var documentoRequerido = this.DocumentoRequeridoDomainService.SearchByKey(specDocumentoRequerido);

                            if (documentoRequerido == null)
                                throw new DocumentoConclusaoEntregaInformacaoNaoEncontradaException("documento requerido");

                            foreach (var documentoUpload in modelo.DocumentosUpload)
                            {
                                var arquivoAnexado = documentoUpload.ArquivoAnexado.Transform<ArquivoAnexado>();
                                ArquivoAnexadoDomainService.SaveEntity(arquivoAnexado);

                                SolicitacaoDocumentoRequerido solicitacaoDocumentoRequerido = new SolicitacaoDocumentoRequerido()
                                {
                                    SeqDocumentoRequerido = documentoRequerido.Seq,
                                    SeqSolicitacaoServico = modelo.SeqSolicitacaoServicoAuxiliar,
                                    SeqArquivoAnexado = arquivoAnexado.Seq,
                                    Observacao = documentoUpload.Descricao, //modelo.Observacao,
                                    DataEntrega = DateTime.Now,
                                    FormaEntregaDocumento = FormaEntregaDocumento.Upload,
                                    VersaoDocumento = VersaoDocumento.CopiaSimples,
                                    SituacaoEntregaDocumento = SituacaoEntregaDocumento.AguardandoValidacao,
                                    DataPrazoEntrega = null,
                                    DescricaoInconformidade = null,
                                    EntregaPosterior = false,
                                    EntregueAnteriormente = false,
                                    ObservacaoSecretaria = null
                                };

                                this.SolicitacaoDocumentoRequeridoDomainService.SaveEntity(solicitacaoDocumentoRequerido);
                            }

                            SolicitacaoServico solicitacaoServico = new SolicitacaoServico()
                            {
                                Seq = modelo.SeqSolicitacaoServicoAuxiliar,
                                SituacaoDocumentacao = SituacaoDocumentacao.Entregue
                            };

                            this.SolicitacaoServicoDomainService.UpdateFields(solicitacaoServico, x => x.SituacaoDocumentacao);
                        }

                        //1.3. Enviar notificação para o CRA/Diploma comunicando a necessidade de revisar o documento, conforme a
                        //RN_CNC_059 - Documento Conclusão - Notificação - Comunicar CRA/Diploma que aluno indeferiu os dados do documento
                        var seqTipoNotificacaoAlunoIndeferiu = tiposNotificacao.FirstOrDefault(a => a.Token.Trim() == TOKEN_TIPO_NOTIFICACAO.COMUNICADO_CRA_ALUNO_INDEFERIU_DADOS_DOCUMENTO)?.Seq;
                        if (!seqTipoNotificacaoAlunoIndeferiu.HasValue)
                            throw new DocumentoConclusaoEntregaInformacaoNaoEncontradaException($"tipo de notificação ({TOKEN_TIPO_NOTIFICACAO.COMUNICADO_CRA_ALUNO_INDEFERIU_DADOS_DOCUMENTO})");

                        var specProcessoEtapaNotificacaoAlunoIndeferiu = new ProcessoEtapaConfiguracaoNotificacaoSpecification()
                        {
                            SeqProcessoEtapa = dadosSolicitacao.SeqProcessoEtapa,
                            SeqTipoNotificacao = seqTipoNotificacaoAlunoIndeferiu,
                            SeqEntidadeResponsavel = dadosOrigem.SeqEntidadeResponsavel
                        };
                        var processoEtapaNotificacaoAlunoIndeferiu = this.ProcessoEtapaConfiguracaoNotificacaoDomainService.SearchByKey(specProcessoEtapaNotificacaoAlunoIndeferiu);

                        if (processoEtapaNotificacaoAlunoIndeferiu == null)
                            throw new DocumentoConclusaoEntregaInformacaoNaoEncontradaException($"configuração de notificação para a etapa atual ({TOKEN_TIPO_NOTIFICACAO.COMUNICADO_CRA_ALUNO_INDEFERIU_DADOS_DOCUMENTO})");

                        if (processoEtapaNotificacaoAlunoIndeferiu.EnvioAutomatico)
                        {
                            //Monta dicionário para merge dos dados
                            //NOM_PESSOA: buscar o nome social do solicitante, caso exista. Se não existir, buscar o nome do solicitante
                            //DSC_PROCESSO: buscar a descrição do processo da solicitação
                            //NUM_PROTOCOLO: buscar o número do protocolo da solicitação
                            //DSC_OBSERVACAO_SITUACAO_CANCELADA: buscar a observação informada pelo aluno sobre o indeferimento
                            Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
                            dadosMerge.Add("{{NOM_PESSOA}}", nomePessoaTag);
                            dadosMerge.Add("{{DSC_PROCESSO}}", dadosSolicitacao.DescricaoProcesso);
                            dadosMerge.Add("{{NUM_PROTOCOLO}}", dadosSolicitacao.NumeroProtocolo);
                            dadosMerge.Add("{{DSC_OBSERVACAO_SITUACAO_CANCELADA}}", modelo.Observacao);

                            //Montando requisição para notificação, não é enviado os destinatários pois já foi configurado na notificação (setor de diplomas)
                            NotificacaoEmailData data = new NotificacaoEmailData()
                            {
                                SeqConfiguracaoNotificacao = processoEtapaNotificacaoAlunoIndeferiu.SeqConfiguracaoTipoNotificacao,
                                DadosMerge = dadosMerge,
                                DataPrevistaEnvio = DateTime.Now,
                                PrioridadeEnvio = PrioridadeEnvio.QuandoPossivel
                            };

                            var emailUsuarioResponsavel = UsuarioService.BuscarEmailsUsuario(dadosSolicitacao.SeqUsuarioResponsavel, true);
                            if (emailUsuarioResponsavel.Count() > 0)
                                data.Destinatarios = new List<NotificacaoEmailDestinatarioData>() { new NotificacaoEmailDestinatarioData() { EmailDestinatario = string.Join(";", emailUsuarioResponsavel.Select(s => s.Email)) } };

                            //Chama o serviço de envio de notificação
                            long seqNotificacaoEnviada = this.NotificacaoService.SalvarNotificacao(data);

                            //Busca o sequencial da notificação-email-destinatário enviada
                            var envioDestinatario = this.NotificacaoService.BuscaNotificacaoEmailDestinatario(seqNotificacaoEnviada);
                            if (envioDestinatario.Count == 0)
                                throw new SolicitacaoServicoEnvioNotificacaoException(TOKEN_TIPO_NOTIFICACAO.COMUNICADO_CRA_ALUNO_INDEFERIU_DADOS_DOCUMENTO);

                            foreach (var item in envioDestinatario)
                            {
                                //Salva a referencia da notificação enviada para a solicitação de serviço
                                SolicitacaoServicoEnvioNotificacao solicitacaoEnvio = new SolicitacaoServicoEnvioNotificacao()
                                {
                                    SeqSolicitacaoServico = modelo.SeqSolicitacaoServicoAuxiliar,
                                    SeqProcessoEtapaConfiguracaoNotificacao = processoEtapaNotificacaoAlunoIndeferiu.Seq,
                                    SeqNotificacaoEmailDestinatario = item.Seq
                                };

                                this.SolicitacaoServicoEnvioNotificacaoDomainService.SaveEntity(solicitacaoEnvio);
                            }
                        }

                        //1.4.Enviar notificação para o Aluno formalizando que o diploma será revisto e por esse motivo o mesmo foi
                        //anulado, conforme a RN_CNC_060 - Documento Conclusão - Notificação - Comunicar Aluno que o Documento foi cancelado e será revisto
                        var seqTipoNotificacaoDocumentoCancelado = tiposNotificacao.FirstOrDefault(a => a.Token.Trim() == TOKEN_TIPO_NOTIFICACAO.COMUNICADO_ALUNO_DOCUMENTO_CANCELADO)?.Seq;
                        if (!seqTipoNotificacaoDocumentoCancelado.HasValue)
                            throw new DocumentoConclusaoEntregaInformacaoNaoEncontradaException($"tipo de notificação ({TOKEN_TIPO_NOTIFICACAO.COMUNICADO_ALUNO_DOCUMENTO_CANCELADO})");

                        var specProcessoEtapaNotificacaoDocumentoCancelado = new ProcessoEtapaConfiguracaoNotificacaoSpecification()
                        {
                            SeqProcessoEtapa = dadosSolicitacao.SeqProcessoEtapa,
                            SeqTipoNotificacao = seqTipoNotificacaoDocumentoCancelado,
                            SeqEntidadeResponsavel = dadosOrigem.SeqEntidadeResponsavel
                        };
                        var processoEtapaNotificacaoDocumentoCancelado = this.ProcessoEtapaConfiguracaoNotificacaoDomainService.SearchByKey(specProcessoEtapaNotificacaoDocumentoCancelado);

                        if (processoEtapaNotificacaoDocumentoCancelado == null)
                            throw new DocumentoConclusaoEntregaInformacaoNaoEncontradaException($"configuração de notificação para a etapa atual ({TOKEN_TIPO_NOTIFICACAO.COMUNICADO_ALUNO_DOCUMENTO_CANCELADO})");

                        if (processoEtapaNotificacaoDocumentoCancelado.EnvioAutomatico)
                        {
                            //Monta dicionário para merge dos dados
                            //NOM_PESSOA: buscar o nome social do solicitante, caso exista. Se não existir, buscar o nome do solicitante
                            //DSC_PROCESSO: buscar a descrição do processo da solicitação
                            //NUM_PROTOCOLO: buscar o número do protocolo da solicitação
                            Dictionary<string, string> dadosMerge = new Dictionary<string, string>
                            {
                                { "{{NOM_PESSOA}}", nomePessoaTag },
                                { "{{DSC_PROCESSO}}", dadosSolicitacao.DescricaoProcesso },
                                { "{{NUM_PROTOCOLO}}", dadosSolicitacao.NumeroProtocolo }
                            };

                            //Montando requisição para notificação, enviando os emails do aluno da solicitação de serviço como destinatários
                            NotificacaoEmailData data = new NotificacaoEmailData()
                            {
                                SeqConfiguracaoNotificacao = processoEtapaNotificacaoDocumentoCancelado.SeqConfiguracaoTipoNotificacao,
                                DadosMerge = dadosMerge,
                                DataPrevistaEnvio = DateTime.Now,
                                PrioridadeEnvio = PrioridadeEnvio.QuandoPossivel,
                                Destinatarios = new List<NotificacaoEmailDestinatarioData>()
                                {
                                    new NotificacaoEmailDestinatarioData()
                                    {
                                        EmailDestinatario = string.Join(";", dadosSolicitacao.EmailsAluno.Select(s => s.Descricao))
                                    }
                                }
                            };

                            //Chama o serviço de envio de notificação
                            long seqNotificacaoEnviada = this.NotificacaoService.SalvarNotificacao(data);

                            //Busca o sequencial da notificação-email-destinatário enviada
                            var envioDestinatario = this.NotificacaoService.BuscaNotificacaoEmailDestinatario(seqNotificacaoEnviada);
                            if (envioDestinatario.Count == 0)
                                throw new SolicitacaoServicoEnvioNotificacaoException(TOKEN_TIPO_NOTIFICACAO.COMUNICADO_ALUNO_DOCUMENTO_CANCELADO);

                            foreach (var item in envioDestinatario)
                            {
                                //Salva a referencia da notificação enviada para a solicitação de serviço
                                SolicitacaoServicoEnvioNotificacao solicitacaoEnvio = new SolicitacaoServicoEnvioNotificacao()
                                {
                                    SeqSolicitacaoServico = modelo.SeqSolicitacaoServicoAuxiliar,
                                    SeqProcessoEtapaConfiguracaoNotificacao = processoEtapaNotificacaoDocumentoCancelado.Seq,
                                    SeqNotificacaoEmailDestinatario = item.Seq
                                };

                                this.SolicitacaoServicoEnvioNotificacaoDomainService.SaveEntity(solicitacaoEnvio);
                            }
                        }

                        //1.5.Preencher os seguintes campos da respectiva solicitação-documento-conclusão
                        var specSolicitacaoDocumentoConclusao = new SMCSeqSpecification<SolicitacaoDocumentoConclusao>(modelo.SeqSolicitacaoServicoAuxiliar);
                        var solicitacaoDocumentoConclusao = this.SolicitacaoDocumentoConclusaoDomainService.SearchByKey(specSolicitacaoDocumentoConclusao);
                        solicitacaoDocumentoConclusao.ConfirmacaoAluno = false;

                        this.SolicitacaoDocumentoConclusaoDomainService.SaveEntity(solicitacaoDocumentoConclusao);
                    }
                    //2.Senão, se o valor do campo "Os dados pessoais estão corretos?” for igual a "Sim"
                    else
                    {
                        //2.1. Atualizar o histórico de situação da solicitação-serviço, conforme a RN_CNC_018 - Documento
                        //Conclusão - Atualização do histórico de situação da solicitação e documento ao finalizar processo com sucesso.
                        var etapasSGF = SGFHelper.BuscarEtapasSGFCache(dadosSolicitacao.SeqTemplateProcessoSgf);
                        var situacaoSGF = etapasSGF.FirstOrDefault(e => e.Seq == dadosSolicitacao.SeqEtapaSgf).Situacoes.FirstOrDefault(s => !s.SituacaoInicialEtapa && s.SituacaoFinalEtapa && !s.SituacaoSolicitante && s.SituacaoFinalProcesso && s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso);

                        if (situacaoSGF == null)
                            throw new DocumentoConclusaoEntregaInformacaoNaoEncontradaException("situação SGF");

                        SolicitacaoHistoricoSituacao novoHistoricoSituacao = new SolicitacaoHistoricoSituacao
                        {
                            SeqSolicitacaoServicoEtapa = dadosSolicitacao.SeqSolicitacaoServicoEtapa,
                            SeqSituacaoEtapaSgf = situacaoSGF.Seq,
                            CategoriaSituacao = situacaoSGF.CategoriaSituacao
                        };

                        this.SolicitacaoHistoricoSituacaoDomainService.SaveEntity(novoHistoricoSituacao);

                        var specSituacaoDocumentoAcademico = new SituacaoDocumentoAcademicoFilterSpecification() { Token = TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.ENTREGUE };
                        var seqSituacaoDocumentoAcademico = SituacaoDocumentoAcademicoDomainService.SearchProjectionBySpecification(specSituacaoDocumentoAcademico, s => s.Seq).FirstOrDefault();

                        //Caso não encontre o sequencial, dispara mensagem de erro
                        if (seqSituacaoDocumentoAcademico == 0)
                            throw new TokenSituacaoDocumentoAcademicoNaoEncontradoException(TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.ENTREGUE);

                        var modeloAuxiliar = BuscarDadosEntregaDocumentoDigital(modelo.SeqSolicitacaoServicoAuxiliar);
                        foreach (var documentoConclusao in modeloAuxiliar.DocumentosConclusao)
                        {
                            var documentoAcademicoHistoricoSituacao = new DocumentoAcademicoHistoricoSituacao()
                            {
                                SeqDocumentoAcademico = documentoConclusao.SeqDocumentoConclusao,
                                SeqSituacaoDocumentoAcademico = seqSituacaoDocumentoAcademico
                            };

                            this.DocumentoAcademicoHistoricoSituacaoDomainService.SaveEntity(documentoAcademicoHistoricoSituacao);
                        }

                        //2.2. Se o nível-ensino do aluno for igual a Graduação, deverá ser acionada a rotina do SGA
                        //st_altera_situacao_servico_diploma_digital para concluir o serviço (tramite-serviço) correspondente
                        //à solicitação-serviço do Acadêmico.
                        var tokenNivelEnsino = NivelEnsinoDomainService.SearchProjectionByKey(new SMCSeqSpecification<NivelEnsino>(dadosOrigem.SeqNivelEnsino), x => x.Token);

                        if (tokenNivelEnsino == TOKEN_NIVEL_ENSINO.GRADUACAO)
                        {
                            var usuarioOperacao = SMCContext.User?.SMCGetSequencialUsuario()?.ToString();
                            IntegracaoAcademicoService.ConcluirSolicitacaoDiplomaDigital(dadosSolicitacao.NumeroProtocolo, usuarioOperacao);
                        }

                        //2.3. Enviar notificação para o Aluno comunicando que os documentos estarão disponíveis no Portal do
                        //SGA, conforme a RN_CNC_061 - Documento Conclusão - Notificação - Comunicar Aluno que o Documento está disponível para download
                        var seqTipoNotificacaoDocumentoDisponivel = tiposNotificacao.FirstOrDefault(a => a.Token.Trim() == TOKEN_TIPO_NOTIFICACAO.COMUNICADO_ALUNO_DOCUMENTO_DISPONIVEL)?.Seq;
                        if (!seqTipoNotificacaoDocumentoDisponivel.HasValue)
                            throw new DocumentoConclusaoEntregaInformacaoNaoEncontradaException($"tipo de notificação ({TOKEN_TIPO_NOTIFICACAO.COMUNICADO_ALUNO_DOCUMENTO_DISPONIVEL})");

                        var specProcessoEtapaNotificacaoDocumentoDisponivel = new ProcessoEtapaConfiguracaoNotificacaoSpecification()
                        {
                            SeqProcessoEtapa = dadosSolicitacao.SeqProcessoEtapa,
                            SeqTipoNotificacao = seqTipoNotificacaoDocumentoDisponivel,
                            SeqEntidadeResponsavel = dadosOrigem.SeqEntidadeResponsavel
                        };
                        var processoEtapaNotificacaoDocumentoDisponivel = this.ProcessoEtapaConfiguracaoNotificacaoDomainService.SearchByKey(specProcessoEtapaNotificacaoDocumentoDisponivel);

                        if (processoEtapaNotificacaoDocumentoDisponivel == null)
                            throw new DocumentoConclusaoEntregaInformacaoNaoEncontradaException($"configuração de notificação para a etapa atual ({TOKEN_TIPO_NOTIFICACAO.COMUNICADO_ALUNO_DOCUMENTO_DISPONIVEL})");

                        if (processoEtapaNotificacaoDocumentoDisponivel.EnvioAutomatico)
                        {
                            //Monta dicionário para merge dos dados
                            //NOM_PESSOA: buscar o nome social do solicitante, caso exista. Se não existir, buscar o nome do solicitante
                            //DSC_PROCESSO: buscar a descrição do processo da solicitação
                            //NUM_PROTOCOLO: buscar o número do protocolo da solicitação
                            Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
                            dadosMerge.Add("{{NOM_PESSOA}}", nomePessoaTag);
                            dadosMerge.Add("{{DSC_PROCESSO}}", dadosSolicitacao.DescricaoProcesso);
                            dadosMerge.Add("{{NUM_PROTOCOLO}}", dadosSolicitacao.NumeroProtocolo);

                            //Montando requisição para notificação, enviando os emails do aluno da solicitação de serviço como destinatários
                            NotificacaoEmailData data = new NotificacaoEmailData()
                            {
                                SeqConfiguracaoNotificacao = processoEtapaNotificacaoDocumentoDisponivel.SeqConfiguracaoTipoNotificacao,
                                DadosMerge = dadosMerge,
                                DataPrevistaEnvio = DateTime.Now,
                                PrioridadeEnvio = PrioridadeEnvio.QuandoPossivel,
                                Destinatarios = new List<NotificacaoEmailDestinatarioData>()
                                {
                                    new NotificacaoEmailDestinatarioData()
                                    {
                                        EmailDestinatario = string.Join(";", dadosSolicitacao.EmailsAluno.Select(s => s.Descricao))
                                    }
                                }
                            };

                            //Chama o serviço de envio de notificação
                            long seqNotificacaoEnviada = this.NotificacaoService.SalvarNotificacao(data);

                            //Busca o sequencial da notificação-email-destinatário enviada
                            var envioDestinatario = this.NotificacaoService.BuscaNotificacaoEmailDestinatario(seqNotificacaoEnviada);
                            if (envioDestinatario.Count == 0)
                                throw new SolicitacaoServicoEnvioNotificacaoException(TOKEN_TIPO_NOTIFICACAO.COMUNICADO_ALUNO_DOCUMENTO_DISPONIVEL);

                            foreach (var item in envioDestinatario)
                            {
                                //Salva a referencia da notificação enviada para a solicitação de serviço
                                SolicitacaoServicoEnvioNotificacao solicitacaoEnvio = new SolicitacaoServicoEnvioNotificacao()
                                {
                                    SeqSolicitacaoServico = modelo.SeqSolicitacaoServicoAuxiliar,
                                    SeqProcessoEtapaConfiguracaoNotificacao = processoEtapaNotificacaoDocumentoDisponivel.Seq,
                                    SeqNotificacaoEmailDestinatario = item.Seq
                                };

                                this.SolicitacaoServicoEnvioNotificacaoDomainService.SaveEntity(solicitacaoEnvio);
                            }
                        }

                        //2.4. Inserir os arquivos do diploma-digital no acervo acadêmico do aluno, conforme a RN_CNC_062 -
                        //Documento Conclusão - Acervo Digital do Aluno (GED)

                        //2.5. Preencher os seguintes campos da respectiva solicitação-documento-conclusão
                        var specSolicitacaoDocumentoConclusao = new SMCSeqSpecification<SolicitacaoDocumentoConclusao>(modelo.SeqSolicitacaoServicoAuxiliar);
                        var solicitacaoDocumentoConclusao = this.SolicitacaoDocumentoConclusaoDomainService.SearchByKey(specSolicitacaoDocumentoConclusao);
                        solicitacaoDocumentoConclusao.ConfirmacaoAluno = true;

                        this.SolicitacaoDocumentoConclusaoDomainService.SaveEntity(solicitacaoDocumentoConclusao);
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    cancelarSolicitacao = false;

                    transaction.Rollback();
                    throw ex;
                }
            }

            if (cancelarSolicitacao)
            {
                this.SolicitacaoServicoDomainService.SalvarCancelamentoSolicitacao(modeloCancelarSolicitacao);
            }
        }

        public void ValidarModeloEntregaDocumentoDigital(EntregaDocumentoDigitalPaginaVO modelo)
        {
            if (!modelo.ConfirmacaoAluno.GetValueOrDefault())
            {
                if (modelo.DocumentosUpload != null && modelo.DocumentosUpload.Count() > 0)
                {
                    foreach (var documentoUpload in modelo.DocumentosUpload)
                    {
                        string nomeArquivo = documentoUpload.ArquivoAnexado.Name;
                        string extensao = Path.GetExtension(documentoUpload.ArquivoAnexado.Name);

                        if (documentoUpload.ArquivoAnexado != null && documentoUpload.ArquivoAnexado.Size > VALIDACAO_ARQUIVO_ANEXADO.TAMANHO_MAXIMO_5MB)
                        {
                            //Recuperando o tamanho do arquivo com a unidade
                            var tamanhoAnexo = documentoUpload.ArquivoAnexado.Size;
                            string[] sizes = { "B", "KB", "MB", "GB", "TB" };

                            int order = 0;
                            while (tamanhoAnexo >= 1024 && order < sizes.Length - 1)
                            {
                                order++;
                                tamanhoAnexo = tamanhoAnexo / 1024;
                            }

                            string tamanhoArquivo = String.Format("{0:0.##} {1}", tamanhoAnexo, sizes[order]);
                            throw new TamanhoArquivoEntregaDigitalException(nomeArquivo, tamanhoArquivo);
                        }

                        if (string.IsNullOrEmpty(extensao) || !VALIDACAO_ARQUIVO_ANEXADO.EXTENSOES_PERMITIDAS_ENTREGA_DOCUMENTO_DIGITAL.Contains(extensao))
                        {
                            throw new ExtensaoArquivoEntregaDigitalException(nomeArquivo);
                        }
                    }
                }
            }
        }

        public DownloadDocumentoDigitalPaginaVO BuscarDadosDownloadDocumentoDigital(long seqSolicitacaoServico)
        {
            var specSolicitacao = new SMCSeqSpecification<SolicitacaoDocumentoConclusao>(seqSolicitacaoServico);
            var dadosRetorno = this.SolicitacaoDocumentoConclusaoDomainService.SearchProjectionByKey(specSolicitacao, x => new DownloadDocumentoDigitalPaginaVO()
            {
                TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token,

                //Dados Diplomado
                CodigoAlunoMigracao = x.AlunoHistorico.Aluno.CodigoAlunoMigracao,
                NumeroRegistroAcademico = x.AlunoHistorico.Aluno.NumeroRegistroAcademico,
                SeqCicloLetivoFormado = x.AlunoHistorico.HistoricosCicloLetivo.FirstOrDefault(a => a.AlunoHistoricoSituacao.Any(b => b.SituacaoMatricula.Token == TOKENS_SITUACAO_MATRICULA.FORMADO)).SeqCicloLetivo,
            });

            var specDocumento = new DocumentoConclusaoFilterSpecification() { SeqSolicitacaoServico = seqSolicitacaoServico };
            var documentosSolicitacao = this.SearchProjectionBySpecification(specDocumento, x => new
            {
                x.Seq,
                x.SeqAtuacaoAluno,
                x.SeqPessoaDadosPessoais,
                TokenTipoDocumentoAcademico = x.TipoDocumentoAcademico.Token,
                TokenSituacaoDocumentoAcademicoAtual = x.SituacaoAtual.SituacaoDocumentoAcademico.Token,
                DescricaoSituacaoDocumentoAcademicoAtual = x.SituacaoAtual.SituacaoDocumentoAcademico.Descricao,
                CategoriaSituacaoDocumentoAcademicoAtual = x.SituacaoAtual.SituacaoDocumentoAcademico.ClasseSituacaoDocumento,
                x.DataInclusao,
                x.SeqDocumentoGAD,
                x.NumeroRegistro,
                x.DataRegistro,
                x.NumeroProcesso,
                DescricaoCurso = x.SolicitacaoServico.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.Nome,
                CodigoEmecCurso = x.SolicitacaoServico.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CodigoOrgaoRegulador,
                x.SolicitacaoServico.AlunoHistorico.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade,
                FormacoesDocumento = x.FormacoesEspecificas.Select(y => new
                {
                    y.AlunoFormacao.DataInicio,
                    y.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso,
                    DescricaoCurso = y.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.Nome,
                    CodigoEmecCurso = y.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CodigoOrgaoRegulador,
                    y.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade,
                    CodigoCursoOfertaLocalidade = y.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Codigo,
                    DescricaoTitulacao = x.Aluno.DadosPessoais.Sexo == Sexo.Masculino ? y.AlunoFormacao.Titulacao.DescricaoMasculino : y.AlunoFormacao.Titulacao.DescricaoFeminino,
                    DescricaoTitulacaoXSD = y.AlunoFormacao.Titulacao.DescricaoXSD,
                    y.AlunoFormacao.SeqFormacaoEspecifica,
                    y.ObservacaoFormacao,
                    DescricaoFormacaoEspecifica = y.AlunoFormacao.FormacaoEspecifica.Descricao,
                    DescricaoTipoFormacaoEspecifica = y.AlunoFormacao.FormacaoEspecifica.TipoFormacaoEspecifica.Descricao,
                    TokenTipoFormacaoEspecifica = y.AlunoFormacao.FormacaoEspecifica.TipoFormacaoEspecifica.Token,
                    y.AlunoFormacao.DataColacaoGrau
                })
            }).ToList();

            //Recuperando os documentos que foram deferidos (entregues) ou indeferidos (inválidos) no passo de entrega do documento digital
            var documentosDownload = documentosSolicitacao.Where(a => a.TokenSituacaoDocumentoAcademicoAtual == TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.ENTREGUE || a.TokenSituacaoDocumentoAcademicoAtual == TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.INVALIDO).ToList();

            dadosRetorno.DocumentosConclusao = new List<DownloadDocumentoDigitalDocumentoConclusaoPaginaVO>();

            foreach (var documento in documentosDownload)
            {
                DownloadDocumentoDigitalDocumentoConclusaoPaginaVO documentoConclusao = new DownloadDocumentoDigitalDocumentoConclusaoPaginaVO();
                documentoConclusao.SeqDocumentoConclusao = documento.Seq;
                documentoConclusao.CategoriaSituacaoDocumentoAcademicoAtual = documento.CategoriaSituacaoDocumentoAcademicoAtual;
                documentoConclusao.NomeDiplomado = dadosRetorno.NomeAluno;

                if (documento.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL)
                {
                    documentoConclusao.DiplomaDigital = true;
                    documentoConclusao.TituloSecao = $"Diploma {documento.CategoriaSituacaoDocumentoAcademicoAtual.SMCGetDescription().ToLower()}";
                }
                else
                {
                    documentoConclusao.DiplomaDigital = false;
                    documentoConclusao.TituloSecao = $"Histórico escolar {documento.CategoriaSituacaoDocumentoAcademicoAtual.SMCGetDescription().ToLower()}";
                    documentoConclusao.ExibirFormacao = true;
                }

                if (documento.SeqDocumentoGAD.HasValue)
                {
                    var retornoCodigoVerificacao = BuscarCodigoVerificacaoResponseVO(documento.SeqDocumentoGAD.Value);
                    if (documentoConclusao.DiplomaDigital)
                    {
                        documentoConclusao.CodigoValidacaoDiploma = retornoCodigoVerificacao.CodigoVerificacao;
                        //A url de consulta pública é montada na controller, usando uma url que está na chave do web config
                    }
                    else
                    {
                        documentoConclusao.CodigoValidacaoHistorico = retornoCodigoVerificacao.CodigoVerificacao;

                        var retornoValidarHistoricoEscolar = APIHistoricoGAD.Execute<LinkValidarHistoricoEscolarResponseVO>($"ValidarHistoricoEscolar/{retornoCodigoVerificacao.CodigoVerificacao}", method: Method.GET);
                        documentoConclusao.UrlConsultaPublicaHistorico = retornoValidarHistoricoEscolar.LinkValidarlHistoricoEscolar;
                    }
                }

                var formacao = documento.FormacoesDocumento.OrderBy(o => o.DataInicio).FirstOrDefault();
                documentoConclusao.DescricaoCurso = formacao?.DescricaoCurso.Trim();

                switch (dadosRetorno.TokenServico)
                {
                    case TOKEN_SERVICO.EMISSAO_HISTORICO_ESCOLAR_DIGITAL:
                        documentoConclusao.ExibirGrauTitulacao = false;
                        documentoConclusao.ExibirFormacao = false;
                        break;

                    default:
                        documentoConclusao.ExibirGrauTitulacao = true;
                        documentoConclusao.ExibirFormacao = false;
                        break;
                }

                if (formacao != null)
                {
                    var specCursoFormacaoEspecifica = new CursoFormacaoEspecificaFilterSpecification() { SeqCurso = formacao.SeqCurso, SeqFormacaoEspecifica = formacao.SeqFormacaoEspecifica };
                    var cursoFormacaoEspecifica = this.CursoFormacaoEspecificaDomainService.SearchByKey(specCursoFormacaoEspecifica, x => x.GrauAcademico);

                    if (cursoFormacaoEspecifica != null)
                    {
                        documentoConclusao.SeqGrauAcademico = cursoFormacaoEspecifica.SeqGrauAcademico;
                        documentoConclusao.DescricaoGrauAcademico = !string.IsNullOrEmpty(cursoFormacaoEspecifica.GrauAcademico.DescricaoXSD) ? cursoFormacaoEspecifica.GrauAcademico.DescricaoXSD : cursoFormacaoEspecifica.GrauAcademico.Descricao;

                        if (documentoConclusao.DescricaoGrauAcademico == GRAU_ACADEMICO.BachareladoLicenciatura)
                        {
                            documentoConclusao.ExibirGrauTitulacao = false;
                            documentoConclusao.ExibirFormacao = true;
                        }
                    }

                    documentoConclusao.DescricaoTitulacao = !string.IsNullOrEmpty(formacao.DescricaoTitulacaoXSD) ? formacao.DescricaoTitulacaoXSD : formacao.DescricaoTitulacao;
                }
                else
                {
                    documentoConclusao.DescricaoCurso = documento.DescricaoCurso;
                }

                List<string> listaFormacoes = new List<string>();
                if (documento.FormacoesDocumento.Any())
                {
                    var formacoesDocumentoObservacao = documento.FormacoesDocumento.Select(s => s.ObservacaoFormacao).Distinct();
                    foreach (var observacaoFormacao in formacoesDocumentoObservacao)
                    {
                        if (documentoConclusao.DescricaoGrauAcademico == GRAU_ACADEMICO.BachareladoLicenciatura)
                            listaFormacoes.Add(observacaoFormacao);
                    }
                }
                documentoConclusao.FormacaoEspecifica = string.Join(",", listaFormacoes);

                documentoConclusao.NumeroRegistro = documento.NumeroRegistro;
                documentoConclusao.DataRegistro = documento.DataRegistro;
                documentoConclusao.NumeroProcesso = documento.NumeroProcesso;
                documentoConclusao.DataColacao = formacao?.DataColacaoGrau;

                dadosRetorno.DocumentosConclusao.Add(documentoConclusao);
            }

            if (documentosDownload.Any())
            {
                long seqAtuacaoAluno = 0;
                long? seqPessoaDadosPessoais = null;

                if (documentosDownload.Count() > 1)
                {
                    var documentoConclusao = documentosDownload.OrderBy(a => a.DataInclusao).FirstOrDefault();
                    seqAtuacaoAluno = documentoConclusao.SeqAtuacaoAluno;
                    seqPessoaDadosPessoais = documentoConclusao.SeqPessoaDadosPessoais;
                }
                else
                {
                    var documentoConclusao = documentosDownload.FirstOrDefault();
                    seqAtuacaoAluno = documentoConclusao.SeqAtuacaoAluno;
                    seqPessoaDadosPessoais = documentoConclusao.SeqPessoaDadosPessoais;
                }

                var pessoaAtuacao = PessoaAtuacaoDomainService.SearchByKey(new SMCSeqSpecification<PessoaAtuacao>(seqAtuacaoAluno), x => x.Pessoa.Filiacao);
                var pessoaDadosPessoais = PessoaDadosPessoaisDomainService.SearchByKey(new SMCSeqSpecification<PessoaDadosPessoais>(seqPessoaDadosPessoais.GetValueOrDefault()));

                if (!string.IsNullOrEmpty(pessoaAtuacao.Pessoa.Cpf))
                    dadosRetorno.Cpf = SMCMask.ApplyMaskCPF(pessoaAtuacao.Pessoa.Cpf.Trim());

                dadosRetorno.DataNascimento = pessoaAtuacao.Pessoa.DataNascimento;
                dadosRetorno.NumeroPassaporte = pessoaAtuacao.Pessoa.NumeroPassaporte;

                if (pessoaAtuacao.Pessoa.CodigoPaisEmissaoPassaporte.GetValueOrDefault() > 0)
                {
                    var dadosPaisPassaporte = LocalidadeService.BuscarPais(pessoaAtuacao.Pessoa.CodigoPaisEmissaoPassaporte.Value);
                    if (dadosPaisPassaporte != null)
                        dadosRetorno.NomePaisEmissaoPassaporte = dadosPaisPassaporte.Nome;
                }

                if (pessoaAtuacao.Pessoa.CodigoNacionalidade.GetValueOrDefault() > 0)
                {
                    var nacionalidade = this.PessoaService.BuscarNacionalidade(Convert.ToByte(pessoaAtuacao.Pessoa.CodigoNacionalidade.Value));
                    if (nacionalidade != null)
                        dadosRetorno.DescricaoNacionalidade = nacionalidade.Descricao;
                }
                else
                {
                    dadosRetorno.DescricaoNacionalidade = pessoaAtuacao.Pessoa.TipoNacionalidade.SMCGetDescription();
                }

                if (pessoaDadosPessoais != null)
                {
                    bool exibirNomeSocial = ExibirNomeSocial(pessoaDadosPessoais.SeqPessoa, TipoPessoa.Fisica);

                    dadosRetorno.NomeAluno = pessoaDadosPessoais.Nome;
                    dadosRetorno.NomeSocial = exibirNomeSocial ? pessoaDadosPessoais.NomeSocial : string.Empty;
                    dadosRetorno.Sexo = pessoaDadosPessoais.Sexo;
                    dadosRetorno.NumeroIdentidade = pessoaDadosPessoais.NumeroIdentidade;
                    dadosRetorno.OrgaoEmissorIdentidade = pessoaDadosPessoais.OrgaoEmissorIdentidade;
                    dadosRetorno.UfIdentidade = pessoaDadosPessoais.UfIdentidade;

                    if (pessoaAtuacao.Pessoa.CodigoPaisNacionalidade > 0)
                    {
                        var pais = this.LocalidadeService.BuscarPais((int)pessoaAtuacao.Pessoa.CodigoPaisNacionalidade);
                        if (pais != null)
                        {
                            if (pais.Nome.Trim().ToUpper() == NOME_PAIS.BRASIL)
                            {
                                if (pessoaDadosPessoais.CodigoCidadeNaturalidade.GetValueOrDefault() > 0 && !string.IsNullOrEmpty(pessoaDadosPessoais.UfNaturalidade))
                                {
                                    var cidade = this.LocalidadeService.BuscarCidade(pessoaDadosPessoais.CodigoCidadeNaturalidade.GetValueOrDefault(), pessoaDadosPessoais.UfNaturalidade);
                                    dadosRetorno.Naturalidade = $"{cidade?.Nome.Trim()} - {pessoaDadosPessoais.UfNaturalidade.Trim()}";
                                }
                            }
                            else
                            {
                                dadosRetorno.Naturalidade = pessoaDadosPessoais.DescricaoNaturalidadeEstrangeira;
                            }
                        }
                    }
                }

                var documentoSolicitacao = documentosDownload.OrderBy(a => a.DataInclusao).FirstOrDefault();
                var formacao = documentoSolicitacao.FormacoesDocumento.OrderBy(o => o.DataInicio).FirstOrDefault();

                dadosRetorno.ExisteDocumentoDiplomaDigital = documentosDownload.Any(a => a.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL);

                if (dadosRetorno.ExisteDocumentoDiplomaDigital)
                    dadosRetorno.TituloSecaoInstituicaoEmissora = INSTITUICAO_EMISSORA.IES_EMISSORA_REGISTRADORA;
                else
                    dadosRetorno.TituloSecaoInstituicaoEmissora = INSTITUICAO_EMISSORA.IES_EMISSORA;

                //Os dados públicos vão ser exibidos se o documento não for inválido, então se tiver um documento inválido já não exibe
                dadosRetorno.CategoriaSituacaoDocumentoAcademicoAtual = documentosDownload.Any(a => a.CategoriaSituacaoDocumentoAcademicoAtual == ClasseSituacaoDocumento.Invalido) ? ClasseSituacaoDocumento.Invalido : documentoSolicitacao.CategoriaSituacaoDocumentoAcademicoAtual;

                //Dados Curso
                dadosRetorno.DescricaoCurso = formacao?.DescricaoCurso;

                dadosRetorno.CodigoEmecCurso = formacao?.CodigoEmecCurso;

                var dataConclusao = (DateTime?)null;
                if (formacao != null)
                {
                    if (formacao.CodigoCursoOfertaLocalidade.HasValue && dadosRetorno.SeqCicloLetivoFormado.HasValue)
                    {
                        var codCursoOfertaLocalidade = formacao.CodigoCursoOfertaLocalidade;
                        var cicloLetivo = CicloLetivoDomainService.SearchByKey(new SMCSeqSpecification<CicloLetivo>(dadosRetorno.SeqCicloLetivoFormado.Value));
                        var anoCicloLetivo = cicloLetivo.Ano;
                        var semestreCicloLetivo = cicloLetivo.Numero;

                        var limiteSemestre = IntegracaoAcademicoService.BuscarLimiteSemestre(codCursoOfertaLocalidade.Value, anoCicloLetivo, semestreCicloLetivo);

                        if (limiteSemestre != null && limiteSemestre.DataFimPeriodoLetivo.HasValue)
                        {
                            dataConclusao = limiteSemestre.DataFimPeriodoLetivo.Value;
                        }
                    }

                    var atosNormativosEntidadesCurso = AtoNormativoEntidadeDomainService.SearchBySpecification(new AtoNormativoEntidadeFilterSpecification
                    {
                        SeqEntidade = formacao.SeqCursoOfertaLocalidade,
                        SeqGrauAcademico = dadosRetorno.DocumentosConclusao.First().SeqGrauAcademico,
                        ConsiderarApenasAtosVigente = false
                    }, x => x.AtoNormativo.AssuntoNormativo, y => y.AtoNormativo.TipoAtoNormativo).ToList();

                    if (dataConclusao.HasValue)
                    {
                        atosNormativosEntidadesCurso = atosNormativosEntidadesCurso.Where(a => a.AtoNormativo.DataDocumento <= dataConclusao.Value).ToList();
                    }

                    var atosNormativosCurso = atosNormativosEntidadesCurso.Select(a => a.AtoNormativo).ToList();
                    if (atosNormativosCurso.Any())
                    {
                        var atosNormativosAutorizacao = atosNormativosCurso.Where(a => a.AssuntoNormativo.Token == TOKEN_ASSUNTO_NORMATIVO.AUTORIZACAO).ToList();
                        if (atosNormativosAutorizacao.Any())
                        {
                            var maiorDataDocumentoAutorizacao = atosNormativosAutorizacao.Max(a => a.DataDocumento);
                            var atoNormativoAutorizacao = atosNormativosAutorizacao.FirstOrDefault(a => a.DataDocumento == maiorDataDocumentoAutorizacao);

                            dadosRetorno.AtoAutorizacaoCurso = atoNormativoAutorizacao.Descricao;
                        }

                        var atosNormativosReconhecimento = atosNormativosCurso.Where(a => a.AssuntoNormativo.Token == TOKEN_ASSUNTO_NORMATIVO.RECONHECIMENTO).ToList();
                        if (atosNormativosReconhecimento.Any())
                        {
                            var maiorDataDocumentoReconhecimento = atosNormativosReconhecimento.Max(a => a.DataDocumento);
                            var atoNormativoReconhecimento = atosNormativosReconhecimento.FirstOrDefault(a => a.DataDocumento == maiorDataDocumentoReconhecimento);

                            dadosRetorno.AtoReconhecimentoCurso = atoNormativoReconhecimento.Descricao;
                        }

                        var atosNormativosRenovacaoReconhecimento = atosNormativosCurso.Where(a => a.AssuntoNormativo.Token == TOKEN_ASSUNTO_NORMATIVO.RENOVACAO_RECONHECIMENTO).ToList();
                        if (atosNormativosRenovacaoReconhecimento.Any())
                        {
                            var maiorDataDocumentoRenovacaoReconhecimento = atosNormativosRenovacaoReconhecimento.Max(a => a.DataDocumento);
                            var atoNormativoRenovacaoReconhecimento = atosNormativosRenovacaoReconhecimento.FirstOrDefault(a => a.DataDocumento == maiorDataDocumentoRenovacaoReconhecimento);

                            dadosRetorno.AtoRenovacaoReconhecimentoCurso = atoNormativoRenovacaoReconhecimento.Descricao;
                        }
                    }

                    var cursoOfertaLocalidade = CursoOfertaLocalidadeDomainService.SearchByKey(new SMCSeqSpecification<CursoOfertaLocalidade>(formacao.SeqCursoOfertaLocalidade), x => x.Enderecos);
                    var modalidade = ModalidadeDomainService.SearchByKey(new SMCSeqSpecification<Modalidade>(cursoOfertaLocalidade.SeqModalidade));

                    if (cursoOfertaLocalidade.Enderecos.Any(a => a.TipoEndereco == TipoEndereco.Comercial && a.Correspondencia.GetValueOrDefault()))
                    {
                        var enderecoComercial = cursoOfertaLocalidade.Enderecos.FirstOrDefault(a => a.TipoEndereco == TipoEndereco.Comercial && a.Correspondencia.GetValueOrDefault());

                        var enderecoAuxiliar = enderecoComercial.Logradouro;
                        enderecoAuxiliar += !string.IsNullOrEmpty(enderecoComercial.Numero) ? $", {enderecoComercial.Numero}" : "";
                        enderecoAuxiliar += !string.IsNullOrEmpty(enderecoComercial.Complemento) ? $", {enderecoComercial.Complemento}" : "";
                        enderecoAuxiliar += !string.IsNullOrEmpty(enderecoComercial.Bairro) ? $", {enderecoComercial.Bairro}" : "";

                        if (!string.IsNullOrEmpty(enderecoComercial.SiglaUf))
                        {
                            if (enderecoComercial.CodigoCidade.GetValueOrDefault() > 0)
                            {
                                var cidade = this.LocalidadeService.BuscarCidade(enderecoComercial.CodigoCidade.Value, enderecoComercial.SiglaUf);

                                //Se tem Cidade, então cidade e uf ficam preenchidas
                                //Se não tem Cidade, então cidade fica vazia e uf fica preenchida
                                enderecoAuxiliar += cidade != null ? $", {cidade.Nome.Trim()}, {enderecoComercial.SiglaUf}" : $", {enderecoComercial.SiglaUf}";
                            }
                            else
                            {
                                //Se não tem Cidade, então cidade fica vazia e uf fica preenchida
                                enderecoAuxiliar += $", {enderecoComercial.SiglaUf}";
                            }
                        }

                        if (!string.IsNullOrEmpty(enderecoComercial.Cep))
                        {
                            var auxCep = Regex.Replace(enderecoComercial.Cep, "[^0-9,]", "").Trim();
                            auxCep = SMCMask.ApplyMaskCEP(auxCep.Trim());
                            enderecoAuxiliar += $", {auxCep}";
                        }

                        dadosRetorno.EnderecoCurso = enderecoAuxiliar;
                    }

                    dadosRetorno.DescricaoModalidade = modalidade.DescricaoXSD;
                }
                else
                {
                    //Histórico pode não ter formação, então recupera os dados do curso sem passar pela formação
                    dadosRetorno.DescricaoCurso = documentoSolicitacao.DescricaoCurso;
                    dadosRetorno.CodigoEmecCurso = documentoSolicitacao.CodigoEmecCurso;

                    var cursoOfertaLocalidade = CursoOfertaLocalidadeDomainService.SearchByKey(new SMCSeqSpecification<CursoOfertaLocalidade>(documentoSolicitacao.SeqCursoOfertaLocalidade), x => x.Enderecos);
                    var modalidade = ModalidadeDomainService.SearchByKey(new SMCSeqSpecification<Modalidade>(cursoOfertaLocalidade.SeqModalidade));

                    if (cursoOfertaLocalidade.Enderecos.Any(a => a.TipoEndereco == TipoEndereco.Comercial && a.Correspondencia.GetValueOrDefault()))
                    {
                        var enderecoComercial = cursoOfertaLocalidade.Enderecos.FirstOrDefault(a => a.TipoEndereco == TipoEndereco.Comercial && a.Correspondencia.GetValueOrDefault());

                        var enderecoAuxiliar = enderecoComercial.Logradouro;
                        enderecoAuxiliar += !string.IsNullOrEmpty(enderecoComercial.Numero) ? $", {enderecoComercial.Numero}" : "";
                        enderecoAuxiliar += !string.IsNullOrEmpty(enderecoComercial.Complemento) ? $", {enderecoComercial.Complemento}" : "";
                        enderecoAuxiliar += !string.IsNullOrEmpty(enderecoComercial.Bairro) ? $", {enderecoComercial.Bairro}" : "";

                        if (!string.IsNullOrEmpty(enderecoComercial.SiglaUf))
                        {
                            if (enderecoComercial.CodigoCidade.GetValueOrDefault() > 0)
                            {
                                var cidade = this.LocalidadeService.BuscarCidade(enderecoComercial.CodigoCidade.Value, enderecoComercial.SiglaUf);

                                //Se tem Cidade, então cidade e uf ficam preenchidas
                                //Se não tem Cidade, então cidade fica vazia e uf fica preenchida
                                enderecoAuxiliar += cidade != null ? $", {cidade.Nome.Trim()}, {enderecoComercial.SiglaUf}" : $", {enderecoComercial.SiglaUf}";
                            }
                            else
                            {
                                //Se não tem Cidade, então cidade fica vazia e uf fica preenchida
                                enderecoAuxiliar += $", {enderecoComercial.SiglaUf}";
                            }
                        }

                        if (!string.IsNullOrEmpty(enderecoComercial.Cep))
                        {
                            var auxCep = Regex.Replace(enderecoComercial.Cep, "[^0-9,]", "").Trim();
                            auxCep = SMCMask.ApplyMaskCEP(auxCep.Trim());
                            enderecoAuxiliar += $", {auxCep}";
                        }

                        dadosRetorno.EnderecoCurso = enderecoAuxiliar;
                    }

                    dadosRetorno.DescricaoModalidade = modalidade.DescricaoXSD;
                }

                //Dados Instituição
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(documentoSolicitacao.SeqAtuacaoAluno);
                var instituicaoEnsino = InstituicaoEnsinoDomainService.SearchByKey(new SMCSeqSpecification<InstituicaoEnsino>(dadosOrigem.SeqInstituicaoEnsino), x => x.Enderecos);

                dadosRetorno.NomeInstituicao = instituicaoEnsino.Nome;
                dadosRetorno.CodigoEmecInstituicao = 338;

                if (instituicaoEnsino.CodigoUnidadeSeo.HasValue)
                {
                    var unidade = EstruturaOrganizacionalService.BuscarUnidadePorId(instituicaoEnsino.CodigoUnidadeSeo.Value);

                    if (unidade != null)
                    {
                        var pessoaJuridica = PessoaService.BuscarPessoaJuridica(unidade.CodigoPessoaEmpresa);

                        if (pessoaJuridica != null)
                        {
                            if (!string.IsNullOrEmpty(pessoaJuridica.Cnpj))
                            {
                                var auxCnpj = Regex.Replace(pessoaJuridica.Cnpj, "[^0-9,]", "").Trim();
                                auxCnpj = SMCMask.ApplyMaskCNPJ(auxCnpj.Trim());
                                dadosRetorno.CnpjInstituicao = auxCnpj;
                            }
                        }
                    }
                }

                if (instituicaoEnsino.Enderecos.Any(a => a.TipoEndereco == TipoEndereco.Comercial && a.Correspondencia.GetValueOrDefault()))
                {
                    var enderecoComercial = instituicaoEnsino.Enderecos.FirstOrDefault(a => a.TipoEndereco == TipoEndereco.Comercial && a.Correspondencia.GetValueOrDefault());

                    var enderecoAuxiliar = enderecoComercial.Logradouro;
                    enderecoAuxiliar += !string.IsNullOrEmpty(enderecoComercial.Numero) ? $", {enderecoComercial.Numero}" : "";
                    enderecoAuxiliar += !string.IsNullOrEmpty(enderecoComercial.Complemento) ? $", {enderecoComercial.Complemento}" : "";
                    enderecoAuxiliar += !string.IsNullOrEmpty(enderecoComercial.Bairro) ? $", {enderecoComercial.Bairro}" : "";

                    if (!string.IsNullOrEmpty(enderecoComercial.SiglaUf))
                    {
                        if (enderecoComercial.CodigoCidade.GetValueOrDefault() > 0)
                        {
                            var cidade = this.LocalidadeService.BuscarCidade(enderecoComercial.CodigoCidade.Value, enderecoComercial.SiglaUf);

                            //Se tem Cidade, então cidade e uf ficam preenchidas
                            //Se não tem Cidade, então cidade fica vazia e uf fica preenchida
                            enderecoAuxiliar += cidade != null ? $", {cidade.Nome.Trim()}, {enderecoComercial.SiglaUf}" : $", {enderecoComercial.SiglaUf}";
                        }
                        else
                        {
                            //Se não tem Cidade, então cidade fica vazia e uf fica preenchida
                            enderecoAuxiliar += $", {enderecoComercial.SiglaUf}";
                        }
                    }

                    if (!string.IsNullOrEmpty(enderecoComercial.Cep))
                    {
                        var auxCep = Regex.Replace(enderecoComercial.Cep, "[^0-9,]", "").Trim();
                        auxCep = SMCMask.ApplyMaskCEP(auxCep.Trim());
                        enderecoAuxiliar += $", {auxCep}";
                    }

                    dadosRetorno.EnderecoInstituicao = enderecoAuxiliar;
                }

                var atosNormativosEntidadesInstituicao = AtoNormativoEntidadeDomainService.SearchBySpecification(new AtoNormativoEntidadeFilterSpecification
                {
                    SeqEntidade = dadosOrigem.SeqInstituicaoEnsino,
                    ConsiderarApenasAtosVigente = false
                }, x => x.AtoNormativo.AssuntoNormativo, y => y.AtoNormativo.TipoAtoNormativo).ToList();

                if (dataConclusao.HasValue)
                {
                    atosNormativosEntidadesInstituicao = atosNormativosEntidadesInstituicao.Where(a => a.AtoNormativo.DataDocumento <= dataConclusao.Value).ToList();
                }

                var atosNormativosInstituicao = atosNormativosEntidadesInstituicao.Select(a => a.AtoNormativo).ToList();

                if (atosNormativosInstituicao.Any())
                {
                    var atosNormativosCredenciamento = atosNormativosInstituicao.Where(a => a.AssuntoNormativo.Token == TOKEN_ASSUNTO_NORMATIVO.CREDENCIAMENTO).ToList();

                    if (atosNormativosCredenciamento.Any())
                    {
                        var maiorDataDocumentoCredenciamento = atosNormativosCredenciamento.Max(a => a.DataDocumento);
                        var atoNormativoCredenciamento = atosNormativosCredenciamento.FirstOrDefault(a => a.DataDocumento == maiorDataDocumentoCredenciamento);

                        dadosRetorno.AtoCredenciamentoInstituicao = atoNormativoCredenciamento.Descricao;
                    }

                    var atosNormativosRecredenciamento = atosNormativosInstituicao.Where(a => a.AssuntoNormativo.Token == TOKEN_ASSUNTO_NORMATIVO.RECREDENCIAMENTO).ToList();

                    if (atosNormativosRecredenciamento.Any())
                    {
                        var maiorDataDocumentoRecredenciamento = atosNormativosRecredenciamento.Max(a => a.DataDocumento);
                        var atoNormativoRecredenciamento = atosNormativosRecredenciamento.FirstOrDefault(a => a.DataDocumento == maiorDataDocumentoRecredenciamento);

                        dadosRetorno.AtoRecredenciamentoInstituicao = atoNormativoRecredenciamento.Descricao;
                    }

                    var atosNormativosRenovacaoRecredenciamento = atosNormativosInstituicao.Where(a => a.AssuntoNormativo.Token == TOKEN_ASSUNTO_NORMATIVO.RENOVACAO_RECREDENCIAMENTO).ToList();

                    if (atosNormativosRenovacaoRecredenciamento.Any())
                    {
                        var maiorDataDocumentoRenovacaoRecredenciamento = atosNormativosRenovacaoRecredenciamento.Max(a => a.DataDocumento);
                        var atoNormativoRenovacaoRecredenciamento = atosNormativosRenovacaoRecredenciamento.FirstOrDefault(a => a.DataDocumento == maiorDataDocumentoRenovacaoRecredenciamento);

                        dadosRetorno.AtoRenovacaoRecredenciamentoInstituicao = atoNormativoRenovacaoRecredenciamento.Descricao;
                    }
                }

                //Dados Mantenedora
                var mantenedora = MantenedoraDomainService.SearchByKey(new SMCSeqSpecification<Mantenedora>(instituicaoEnsino.SeqMantenedora), x => x.Enderecos);
                dadosRetorno.RazaoSocialMantenedora = mantenedora.Nome;

                if (mantenedora.CodigoUnidadeSeo.HasValue)
                {
                    var unidade = EstruturaOrganizacionalService.BuscarUnidadePorId(mantenedora.CodigoUnidadeSeo.Value);

                    if (unidade != null)
                    {
                        var pessoaJuridica = PessoaService.BuscarPessoaJuridica(unidade.CodigoPessoaEmpresa);

                        if (pessoaJuridica != null)
                        {
                            if (!string.IsNullOrEmpty(pessoaJuridica.Cnpj))
                            {
                                var auxCnpj = Regex.Replace(pessoaJuridica.Cnpj, "[^0-9,]", "").Trim();
                                auxCnpj = SMCMask.ApplyMaskCNPJ(auxCnpj.Trim());
                                dadosRetorno.CnpjMantenedora = auxCnpj;
                            }
                        }
                    }
                }

                if (mantenedora.Enderecos.Any(a => a.TipoEndereco == TipoEndereco.Comercial && a.Correspondencia.GetValueOrDefault()))
                {
                    var enderecoComercial = mantenedora.Enderecos.FirstOrDefault(a => a.TipoEndereco == TipoEndereco.Comercial && a.Correspondencia.GetValueOrDefault());

                    var enderecoAuxiliar = enderecoComercial.Logradouro;
                    enderecoAuxiliar += !string.IsNullOrEmpty(enderecoComercial.Numero) ? $", {enderecoComercial.Numero}" : "";
                    enderecoAuxiliar += !string.IsNullOrEmpty(enderecoComercial.Complemento) ? $", {enderecoComercial.Complemento}" : "";
                    enderecoAuxiliar += !string.IsNullOrEmpty(enderecoComercial.Bairro) ? $", {enderecoComercial.Bairro}" : "";

                    if (!string.IsNullOrEmpty(enderecoComercial.SiglaUf))
                    {
                        if (enderecoComercial.CodigoCidade.GetValueOrDefault() > 0)
                        {
                            var cidade = this.LocalidadeService.BuscarCidade(enderecoComercial.CodigoCidade.Value, enderecoComercial.SiglaUf);

                            //Se tem Cidade, então cidade e uf ficam preenchidas
                            //Se não tem Cidade, então cidade fica vazia e uf fica preenchida
                            enderecoAuxiliar += cidade != null ? $", {cidade.Nome.Trim()}, {enderecoComercial.SiglaUf}" : $", {enderecoComercial.SiglaUf}";
                        }
                        else
                        {
                            //Se não tem Cidade, então cidade fica vazia e uf fica preenchida
                            enderecoAuxiliar += $", {enderecoComercial.SiglaUf}";
                        }
                    }

                    if (!string.IsNullOrEmpty(enderecoComercial.Cep))
                    {
                        var auxCep = Regex.Replace(enderecoComercial.Cep, "[^0-9,]", "").Trim();
                        auxCep = SMCMask.ApplyMaskCEP(auxCep.Trim());
                        enderecoAuxiliar += $", {auxCep}";
                    }

                    dadosRetorno.EnderecoMantenedora = enderecoAuxiliar;
                }
            }

            return dadosRetorno;
        }

        public ConsultaPublicaVO ValidarCodigoVerificacaoDiploma(string codigoVerificacao, string nomeCompletoDiplomado)
        {
            var retornoGAD = APIDiplomaGAD.Execute<BuscarCodigoDocumentoDiplomaResponseVO>($"BuscarDocumentoAcademico/{codigoVerificacao}", method: Method.GET);

            if (!string.IsNullOrEmpty(retornoGAD.ErrorMessage))
                throw new Exception(retornoGAD.ErrorMessage);

            if (retornoGAD.SeqDocumentoAcademico == 0)
                throw new CodigoVerificacaoNaoLocalizadoException();

            var spec = new DocumentoConclusaoFilterSpecification { SeqDocumentoDiplomaGAD = retornoGAD.SeqDocumentoAcademico, Nome = nomeCompletoDiplomado };

            var documentoConclusao = this.SearchProjectionByKey(spec, a => new ConsultaPublicaVO()
            {
                Seq = a.Seq,
                ClasseSituacaoDocumento = a.SituacaoAtual.SituacaoDocumentoAcademico.ClasseSituacaoDocumento,
                SeqPessoaAtuacao = a.SeqAtuacaoAluno,
                SeqPessoa = a.PessoaDadosPessoais.SeqPessoa,
                SeqPessoaDadosPessoais = a.SeqPessoaDadosPessoais,
                SeqCicloLetivoFormado = a.FormacoesEspecificas.Select(s => s.AlunoFormacao.AlunoHistorico.HistoricosCicloLetivo.FirstOrDefault(f => f.AlunoHistoricoSituacao.Any(b => b.SituacaoMatricula.Token == TOKENS_SITUACAO_MATRICULA.FORMADO)).SeqCicloLetivo).FirstOrDefault(),
                CodigoCursoOfertaLocalidade = a.FormacoesEspecificas.Select(s => s.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade).Select(s => s.Codigo).FirstOrDefault(),
                SeqCursoOferta = a.FormacoesEspecificas.Select(s => s.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta).Select(s => s.Seq).FirstOrDefault(),

                //Diplomado
                Cpf = a.Aluno.Pessoa.Cpf,

                //Dados do curso
                CodigoCursoEMEC = a.FormacoesEspecificas.Select(s => s.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade).Select(s => s.CodigoOrgaoRegulador).FirstOrDefault(),
                NomeCurso = a.FormacoesEspecificas.Select(s => s.AlunoFormacao.DescricaoDocumentoConclusao).FirstOrDefault(),
                DescricaoGrauAcademico = a.FormacoesEspecificas.Select(s => s.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica.FirstOrDefault(f => f.SeqFormacaoEspecifica == s.AlunoFormacao.SeqFormacaoEspecifica).GrauAcademico.Descricao).FirstOrDefault(),
                DescricaoTitulacao = a.Aluno.DadosPessoais.Sexo == Sexo.Masculino ? a.FormacoesEspecificas.Select(s => s.AlunoFormacao.Titulacao.DescricaoMasculino).FirstOrDefault() : a.FormacoesEspecificas.Select(s => s.AlunoFormacao.Titulacao.DescricaoFeminino).FirstOrDefault(),
                DescricaoTitulacaoXSD = a.FormacoesEspecificas.Select(s => s.AlunoFormacao.Titulacao.DescricaoXSD).FirstOrDefault(),
                DataConclusao = a.FormacoesEspecificas.Select(s => s.AlunoFormacao.DataConclusao).FirstOrDefault(),

                TokenFormaIngresso = a.FormacoesEspecificas.SelectMany(s => s.AlunoFormacao.AlunoHistorico.Aluno.Historicos).FirstOrDefault(f => f.Atual).FormaIngresso.Token,
                SeqCursoOfertaLocalidadeHistoricoAtual = a.FormacoesEspecificas.Select(s => s.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno).Select(s => s.SeqCursoOfertaLocalidade).FirstOrDefault(),

                //Dados do registro
                NumeroProcesso = a.NumeroProcesso,
                NumeroRegistro = a.NumeroRegistro,
                DataRegistro = a.DataRegistro,
                DataRegistroDOU = a.DataPublicacaoDOU,

                Historicos = a.Situacoes.OrderBy(o => o.DataInclusao).Select(x => new ConsultaPublicaHistoricoVO()
                {
                    Seq = x.Seq,
                    SeqDocumentoConclusao = x.SeqDocumentoAcademico,
                    SeqSituacaoDocumentoAcademico = x.SeqSituacaoDocumentoAcademico,
                    DescricaoSituacaoDocumentoAcademico = x.SituacaoDocumentoAcademico.Descricao,
                    Token = x.SituacaoDocumentoAcademico.Token,
                    DataInclusao = x.DataInclusao,
                    TipoInvalidade = x.ClassificacaoInvalidadeDocumento.TipoInvalidade,
                    DescricaoClassificacaoInvalidadeDocumento = x.ClassificacaoInvalidadeDocumento.Descricao
                }).OrderBy(o => o.DataInclusao).ToList(),
            });

            if (documentoConclusao == null)
                throw new CodigoVerificacaoNaoLocalizadoException();

            var exibirNomeSocial = ExibirNomeSocial(documentoConclusao.SeqPessoa, TipoPessoa.Fisica);

            var dadosPessoais = PessoaDadosPessoaisDomainService.SearchByKey(new SMCSeqSpecification<PessoaDadosPessoais>(documentoConclusao.SeqPessoaDadosPessoais.GetValueOrDefault()));
            documentoConclusao.Nome = exibirNomeSocial && !string.IsNullOrEmpty(dadosPessoais.NomeSocial) ? dadosPessoais.NomeSocial : dadosPessoais.Nome;
            documentoConclusao.NomeCivil = exibirNomeSocial && !string.IsNullOrEmpty(dadosPessoais.NomeSocial) ? dadosPessoais.Nome : string.Empty;

            var dataIngressoComMenorDataAdmissao = SolicitacaoDocumentoConclusaoDomainService.BuscarFormaEDataIngressoComMenorDataAdmissao(documentoConclusao.SeqPessoaAtuacao, documentoConclusao.SeqCursoOferta).DataAdmissao;
            documentoConclusao.DataIngresso = dataIngressoComMenorDataAdmissao;

            var versaoDiploma = new List<string> { VERSAO_DIPLOMA.V100, VERSAO_DIPLOMA.V101, VERSAO_DIPLOMA.V102, VERSAO_DIPLOMA.V103, VERSAO_DIPLOMA.V1041 };
            if (versaoDiploma.Contains(retornoGAD.VersaoDiploma) && documentoConclusao.TokenFormaIngresso == TOKEN_FORMA_INGRESSO.TRANSFERENCIA_DE_TURNO || documentoConclusao.TokenFormaIngresso == TOKEN_FORMA_INGRESSO.TROCA_DE_CURRICULO)
            {
                var retorno = BuscarFormaEDataIngressoAnteriorTransferenciaOuTrocaCurriculo(documentoConclusao.SeqPessoaAtuacao, documentoConclusao.SeqCursoOfertaLocalidadeHistoricoAtual);
                documentoConclusao.DataIngresso = retorno.DataAdmissao;
            }

            documentoConclusao.Valido = documentoConclusao.ClasseSituacaoDocumento == ClasseSituacaoDocumento.Valido;

            switch (documentoConclusao.ClasseSituacaoDocumento)
            {
                case ClasseSituacaoDocumento.Valido:
                    documentoConclusao.SituacaoDiploma = $"Diploma {documentoConclusao.ClasseSituacaoDocumento.SMCGetDescription()}";
                    break;

                case ClasseSituacaoDocumento.Invalido:
                    documentoConclusao.SituacaoDiploma = $"Diploma {documentoConclusao.ClasseSituacaoDocumento.SMCGetDescription()}";
                    documentoConclusao.MensagemInformativaDiploma = "O diploma digital referente ao código de validação e diplomado encontra-se inválido.";
                    break;

                case ClasseSituacaoDocumento.EmissaoEmAndamento:
                    documentoConclusao.SituacaoDiploma = documentoConclusao.ClasseSituacaoDocumento.SMCGetDescription();
                    documentoConclusao.MensagemInformativaDiploma = "A emissão do diploma digital referente ao código de validação e diplomado encontra-se em andamento";
                    break;

                default:
                    documentoConclusao.SituacaoDiploma = documentoConclusao.ClasseSituacaoDocumento.SMCGetDescription();
                    break;
            }

            if (documentoConclusao.Valido)
            {
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(documentoConclusao.SeqPessoaAtuacao);
                var instituicaoEnsino = InstituicaoEnsinoDomainService.SearchByKey(new SMCSeqSpecification<InstituicaoEnsino>(dadosOrigem.SeqInstituicaoEnsino));
                var mantenedora = MantenedoraDomainService.SearchByKey(new SMCSeqSpecification<Mantenedora>(instituicaoEnsino.SeqMantenedora));

                documentoConclusao.Cpf = $"***.{documentoConclusao.Cpf.Substring(3, 3)}.{documentoConclusao.Cpf.Substring(6, 3)}-**";

                //Instituição de ensino Emissora e Registradora
                documentoConclusao.CodigoMEC = 338;
                documentoConclusao.NomeInstituicaoEnsino = instituicaoEnsino.Nome;
                documentoConclusao.Mantenedora = mantenedora.Nome;
            }

            documentoConclusao.HistoricoInvalido = documentoConclusao.Historicos.Any(a => a.Token == TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.INVALIDO);
            if (documentoConclusao.HistoricoInvalido)
            {
                var historicosInativos = new List<ConsultaPublicaHistoricoVO>();
                var historicoTemporario = new ConsultaPublicaHistoricoVO();
                var historicoPermanente = new ConsultaPublicaHistoricoVO();
                DateTime? dataInicio = null;

                foreach (var historico in documentoConclusao.Historicos.OrderBy(d => d.DataInclusao))
                {
                    if (historico.Token == TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.INVALIDO)
                    {
                        if (historico.TipoInvalidade == TipoInvalidade.Temporario)
                        {
                            dataInicio = historico.DataInclusao;
                            historicoTemporario = new ConsultaPublicaHistoricoVO()
                            {
                                Seq = historico.Seq,
                                SeqDocumentoConclusao = historico.SeqDocumentoConclusao,
                                SeqSituacaoDocumentoAcademico = historico.SeqSituacaoDocumentoAcademico,
                                DescricaoSituacaoDocumentoAcademico = historico.DescricaoSituacaoDocumentoAcademico,
                                Token = historico.Token,
                                TipoInvalidade = historico.TipoInvalidade,
                                DescricaoClassificacaoInvalidadeDocumento = historico.DescricaoClassificacaoInvalidadeDocumento,
                            };
                        }
                        else if (historico.TipoInvalidade == TipoInvalidade.Permanente)
                        {
                            if (historicoTemporario.Seq != 0)
                            {
                                var dataFim = historico.DataInclusao;
                                historicoTemporario.PeriodoInvalidade = FormatarPeriodo(dataInicio.Value, dataFim);
                                historicosInativos.Add(historicoTemporario);
                                dataInicio = null;
                                historicoTemporario = new ConsultaPublicaHistoricoVO();
                            }

                            dataInicio = historico.DataInclusao;
                            historicoPermanente = new ConsultaPublicaHistoricoVO()
                            {
                                Seq = historico.Seq,
                                SeqDocumentoConclusao = historico.SeqDocumentoConclusao,
                                SeqSituacaoDocumentoAcademico = historico.SeqSituacaoDocumentoAcademico,
                                DescricaoSituacaoDocumentoAcademico = historico.DescricaoSituacaoDocumentoAcademico,
                                Token = historico.Token,
                                TipoInvalidade = historico.TipoInvalidade,
                                DescricaoClassificacaoInvalidadeDocumento = historico.DescricaoClassificacaoInvalidadeDocumento,
                            };
                        }
                    }
                    else
                    {
                        if (historicoTemporario.Seq != 0)
                        {
                            var dataFim = historico.DataInclusao;
                            historicoTemporario.PeriodoInvalidade = FormatarPeriodo(dataInicio.Value, dataFim);
                            historicosInativos.Add(historicoTemporario);
                            dataInicio = null;
                            historicoTemporario = new ConsultaPublicaHistoricoVO();
                        }
                        if (historicoPermanente.Seq != 0)
                        {
                            var dataFim = historico.DataInclusao;
                            historicoPermanente.PeriodoInvalidade = FormatarPeriodo(dataInicio.Value, dataFim);
                            historicosInativos.Add(historicoPermanente);
                            dataInicio = null;
                            historicoPermanente = new ConsultaPublicaHistoricoVO();
                        }
                    }
                }

                if (dataInicio.HasValue)
                {
                    if (historicoTemporario.Seq != 0)
                    {
                        historicoTemporario.PeriodoInvalidade = FormatarPeriodo(dataInicio.Value, null);
                        historicosInativos.Add(historicoTemporario);
                    }
                    if (historicoPermanente.Seq != 0)
                    {
                        historicoPermanente.PeriodoInvalidade = FormatarPeriodo(dataInicio.Value, null);
                        historicosInativos.Add(historicoPermanente);
                    }
                }
                documentoConclusao.Historicos = historicosInativos;
            }
            return documentoConclusao;
        }

        private string FormatarPeriodo(DateTime dataInicio, DateTime? dataFim)
        {
            if (dataFim.HasValue)
                return $"{dataInicio.ToString("dd/MM/yyyy HH:mm")} - {dataFim.Value.ToString("dd/MM/yyyy HH:mm")}";
            else
                return $"{dataInicio.ToString("dd/MM/yyyy HH:mm")}";
        }

        public (DateTime DataAdmissao, string Token, string Descricao, string DescricaoXSD) BuscarFormaEDataIngressoAnteriorTransferenciaOuTrocaCurriculo(long seqPessoaAtuacao, long seqCursoOfertaLocalidadeHistoricoAtual)
        {
            var specAlunoHistorico = new AlunoHistoricoFilterSpecification() { SeqAluno = seqPessoaAtuacao, SeqCursoOfertaLocalidade = seqCursoOfertaLocalidadeHistoricoAtual };
            var alunoHistoricos = AlunoHistoricoDomainService.SearchBySpecification(specAlunoHistorico, s => s.FormaIngresso).OrderBy(o => o.Seq).ToList();

            var auxAlunoHistoricoAtual = alunoHistoricos.FirstOrDefault(a => a.Atual);

            if (auxAlunoHistoricoAtual != null)
            {
                var indiceHistoricoAtual = alunoHistoricos.IndexOf(auxAlunoHistoricoAtual);
                var indiceHistoricoAnterior = indiceHistoricoAtual - 1;

                if (alunoHistoricos.ElementAtOrDefault(indiceHistoricoAnterior) != null)
                {
                    //Verificando se existe o índice
                    var auxHistoricoAnterior = alunoHistoricos.ToArray()[indiceHistoricoAnterior];
                    return (auxHistoricoAnterior.DataAdmissao, auxHistoricoAnterior.FormaIngresso.Token,
                            auxHistoricoAnterior.FormaIngresso.Descricao, auxHistoricoAnterior.FormaIngresso.DescricaoXSD);
                }
            }

            return (auxAlunoHistoricoAtual.DataAdmissao, auxAlunoHistoricoAtual.FormaIngresso.Token,
                    auxAlunoHistoricoAtual.FormaIngresso.Descricao, auxAlunoHistoricoAtual.FormaIngresso.DescricaoXSD);
        }

        public BuscarCodigoVerificacaoResponseVO BuscarCodigoVerificacaoResponseVO(long seqDocumentoDiplomaGAD)
        {
            var modeloBuscarCodigoVerificacao = new { SeqDocumentoAcademico = seqDocumentoDiplomaGAD };

            var retornoGAD = APIDiplomaGAD.Execute<object>("BuscarCodigoVerificacao", modeloBuscarCodigoVerificacao);
            var retornoCodigoVerificacao = JsonConvert.DeserializeObject<BuscarCodigoVerificacaoResponseVO>(retornoGAD.ToString());

            return retornoCodigoVerificacao;
        }

        public bool VerificarDocumentoConclusao(long seqPessoaAtuacao, string descricaoCursoDocumento, long? seqGrauAcademico, long? seqTitulacao, List<string> tokensTipoDocumentoAcademico)
        {
            var spec = new DocumentoConclusaoFilterSpecification
            {
                SeqPessoaAtuacao = seqPessoaAtuacao,
                ListaClasseSituacaoDocumento = new List<ClasseSituacaoDocumento>() { ClasseSituacaoDocumento.EmissaoEmAndamento, ClasseSituacaoDocumento.Valido },
                TokensTipoDocumentoAcademico = tokensTipoDocumentoAcademico
            };

            var documentosConclusao = this.SearchProjectionBySpecification(spec, s => new
            {
                s.Seq,
                s.TipoDocumentoAcademico.Token,
                FormacoesEspecificas = s.FormacoesEspecificas.Select(x => new
                {
                    x.Seq,
                    x.SeqDocumentoConclusao,
                    x.AlunoFormacao.DescricaoDocumentoConclusao,
                    SeqGrauAcademico = (long?)x.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica.FirstOrDefault(f => f.SeqFormacaoEspecifica == x.AlunoFormacao.SeqFormacaoEspecifica).GrauAcademico.Seq,
                    SeqTitulacao = (long?)x.AlunoFormacao.Titulacao.Seq,
                }).ToList(),
            }).ToList();

            var retorno = false;
            if (documentosConclusao == null || !documentosConclusao.Any())
                return retorno;

            foreach (var documentoConclusao in documentosConclusao)
            {
                if (documentoConclusao.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_PARCIAL)
                    retorno = documentoConclusao.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_PARCIAL;
                else
                    retorno = documentoConclusao.FormacoesEspecificas.Any(w => w.DescricaoDocumentoConclusao.Trim() == descricaoCursoDocumento.Trim() &&
                                                                               w.SeqGrauAcademico == seqGrauAcademico &&
                                                                               w.SeqTitulacao == seqTitulacao);
                if (retorno)
                    break;
            }
            return retorno;
        }

        public string BuscarDadosPessoaisDocumentoConclusao(long seqPessoaAtuacao, string descricaoCursoDocumento, long? seqGrauAcademico, long? seqTitulacao, long seqPessoaDadosPessoais, TipoIdentidade? tipoIdentidade)
        {
            var spec = new DocumentoConclusaoFilterSpecification
            {
                SeqPessoaAtuacao = seqPessoaAtuacao,
                ListaClasseSituacaoDocumento = new List<ClasseSituacaoDocumento>() { ClasseSituacaoDocumento.EmissaoEmAndamento, ClasseSituacaoDocumento.Valido },
                TokensTipoDocumentoAcademico = new List<string>() { TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL }
            };

            var documentosConclusao = this.SearchProjectionBySpecification(spec, s => new
            {
                s.Seq,
                s.TipoDocumentoAcademico.Token,
                s.SeqPessoaDadosPessoais,
                FormacoesEspecificas = s.FormacoesEspecificas.Select(x => new
                {
                    Seq = x.Seq,
                    SeqDocumentoConclusao = x.SeqDocumentoConclusao,

                    DescricaoDocumentoConclusao = x.AlunoFormacao.DescricaoDocumentoConclusao,
                    SeqGrauAcademico = (long?)x.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica.FirstOrDefault(f => f.SeqFormacaoEspecifica == x.AlunoFormacao.SeqFormacaoEspecifica).GrauAcademico.Seq,
                    SeqTitulacao = (long?)x.AlunoFormacao.Titulacao.Seq,
                }).ToList(),
            }).ToList();

            var retorno = string.Empty;
            if (documentosConclusao == null || !documentosConclusao.Any())
                return retorno;

            foreach (var documentoConclusao in documentosConclusao)
            {
                var encontrou = documentoConclusao.FormacoesEspecificas.Any(w => w.DescricaoDocumentoConclusao.Trim() == descricaoCursoDocumento.Trim() &&
                                                                            w.SeqGrauAcademico == seqGrauAcademico &&
                                                                            w.SeqTitulacao == seqTitulacao) && documentoConclusao.SeqPessoaDadosPessoais != seqPessoaDadosPessoais;
                if (encontrou)
                {
                    var seqs = new List<long>() { seqPessoaDadosPessoais, documentoConclusao.SeqPessoaDadosPessoais.Value };
                    var dadosPessoais = PessoaDadosPessoaisDomainService.SearchProjectionBySpecification(new PessoaDadosPessoaisFilterSpecification { Seqs = seqs }, s => new
                    {
                        s.Seq,
                        SeqPessoa = s.Pessoa.Seq,
                        s.Pessoa.TipoNacionalidade,
                        s.Nome,
                        s.NomeSocial,
                        s.Pessoa.Cpf,
                        s.NumeroIdentidade,
                        s.OrgaoEmissorIdentidade,
                        s.UfIdentidade,
                        s.DataExpedicaoIdentidade,
                        s.Pessoa.CodigoNacionalidade,
                        s.Pessoa.CodigoPaisNacionalidade,
                        s.CodigoCidadeNaturalidade,
                        s.UfNaturalidade,
                        s.DescricaoNaturalidadeEstrangeira,
                        s.NumeroIdentidadeEstrangeira,
                        s.Pessoa.NumeroPassaporte,
                        s.Pessoa.DataValidadePassaporte,
                        s.Pessoa.CodigoPaisEmissaoPassaporte,
                        s.Pessoa.DataNascimento,
                        s.Sexo
                    }).ToList();

                    retorno = "<table class='smc-sga-tabela-modal-emissao' id='controla-size-modal'><tr><th>Referência</th><th>ID</th><th>CPF</th><th>Nome</th><th>Nome social</th><th>Nacionalidade</th><th>Naturalidade</th><th>Nro identidade</th><th>Data expedição</th><th>Passaporte</th><th>Data validade</th><th>Identidade estrangeira</th><th>Data nascimento</th><th>Sexo</th></tr>";
                    foreach (var dado in dadosPessoais)
                    {
                        string referencia, nomeSocial, descricaoNacionalidade = string.Empty, passaporte = string.Empty, naturalidade = string.Empty, cpf = dado.Cpf.SMCRemoveNonDigits();
                        var cpfFormatado = SMCMask.ApplyMaskCPF(cpf.Trim());

                        referencia = dado.Seq == seqPessoaDadosPessoais ? "Histórico" : "Diploma";
                        var codigoPessoaCAD = PessoaDomainService.BuscarCodigoDePessoaNosDadosMestres(dado.SeqPessoa, TipoPessoa.Fisica, null);
                        var exibirNome = PessoaService.AutorizarNomeSocial(codigoPessoaCAD);

                        nomeSocial = exibirNome && !string.IsNullOrEmpty(dado.NomeSocial) ? dado.NomeSocial : "-";

                        if (dado.CodigoNacionalidade.GetValueOrDefault() > 0)
                        {
                            var nacionalidade = this.PessoaService.BuscarNacionalidade(Convert.ToByte(dado.CodigoNacionalidade.Value));
                            if (nacionalidade != null)
                                descricaoNacionalidade = nacionalidade.Descricao;
                        }

                        passaporte = $"{dado.NumeroPassaporte}";

                        if (dado.CodigoPaisNacionalidade > 0)
                        {
                            var pais = this.LocalidadeService.BuscarPais((int)dado.CodigoPaisNacionalidade);
                            if (pais != null)
                            {
                                if (pais.Nome.Trim().ToUpper() == NOME_PAIS.BRASIL)
                                {
                                    if (dado.CodigoCidadeNaturalidade > 0 && !string.IsNullOrEmpty(dado.UfNaturalidade))
                                    {
                                        var cidade = this.LocalidadeService.BuscarCidade(dado.CodigoCidadeNaturalidade.Value, dado.UfNaturalidade);
                                        naturalidade = $"{cidade?.Nome.Trim()} - {dado.UfNaturalidade}";
                                    }
                                }
                                else
                                {
                                    naturalidade = dado.DescricaoNaturalidadeEstrangeira;
                                }

                                if (!string.IsNullOrEmpty(passaporte))
                                    passaporte += $" - {pais?.Nome.Trim()}";
                                else
                                    passaporte = "-";
                            }
                        }

                        var identidade = $"{dado.NumeroIdentidade} - {dado.OrgaoEmissorIdentidade} - {dado.UfIdentidade}";
                        if (string.IsNullOrEmpty(dado.NumeroIdentidade) && string.IsNullOrEmpty(dado.OrgaoEmissorIdentidade) && string.IsNullOrEmpty(dado.UfIdentidade))
                            identidade = "-";

                        var dataExpedicao = dado.DataExpedicaoIdentidade.HasValue ? dado.DataExpedicaoIdentidade.Value.ToString("dd/MM/yyyy") : "-";
                        var dataValidade = dado.DataValidadePassaporte.HasValue ? dado.DataValidadePassaporte.Value.ToString("dd/MM/yyyy") : "-";
                        var identidadeEstrangeira = !string.IsNullOrEmpty(dado.NumeroIdentidadeEstrangeira) ? $"{dado.NumeroIdentidadeEstrangeira}" : "-";
                        var dataNascimento = dado.DataNascimento.HasValue ? dado.DataNascimento.Value.ToString("dd/MM/yyyy") : "-";

                        retorno += $"<tr><th>{referencia}</th><th>{dado.Seq}</th><th>{cpfFormatado}</th><th>{dado.Nome}</th><th>{nomeSocial}</th><th>{descricaoNacionalidade}</th><th>{naturalidade}</th><th>{identidade}</th><th>{dataExpedicao}</th><th>{passaporte}</th><th>{dataValidade}</th><th>{identidadeEstrangeira}</th><th>{dataNascimento}</th><th>{dado.Sexo}</th></tr>";
                    }
                    retorno += "</table>";
                    break;
                }
            }
            return retorno;
        }

        public (List<long> listaSeqsDocumentosAssociados, string listaDocumentosAssociados) ValidarDocumentoConclusaoParaAlterarStatus(long seqDocumentoConclusao, string tokenAcao)
        {
            var documentoConclusaoParaAlterarStatus = this.SearchProjectionByKey(new SMCSeqSpecification<DocumentoConclusao>(seqDocumentoConclusao), s => new
            {
                s.Seq,
                s.SeqAtuacaoAluno,
                s.TipoDocumentoAcademico.Token,
                FormacoesEspecificas = s.FormacoesEspecificas.Select(x => new
                {
                    Seq = x.Seq,
                    SeqDocumentoConclusao = x.SeqDocumentoConclusao,
                    DescricaoDocumentoConclusao = x.AlunoFormacao.DescricaoDocumentoConclusao,
                    SeqGrauAcademico = (long?)x.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica.FirstOrDefault(f => f.SeqFormacaoEspecifica == x.AlunoFormacao.SeqFormacaoEspecifica).GrauAcademico.Seq,
                    SeqTitulacao = (long?)x.AlunoFormacao.Titulacao.Seq,
                }).ToList(),
            });

            var tokensTipoDocumentoAcademico = new List<string>();
            switch (documentoConclusaoParaAlterarStatus.Token)
            {
                case TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL:
                    tokensTipoDocumentoAcademico = new List<string>() { TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_FINAL, TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_2VIA };
                    break;

                case TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_FINAL:
                case TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_2VIA:
                    tokensTipoDocumentoAcademico = new List<string>() { TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL };
                    break;
            }

            var documentosAssociados = string.Empty;
            var seqsDocumentosAssociados = new List<long>();
            if (!tokensTipoDocumentoAcademico.Any())
                return (seqsDocumentosAssociados, documentosAssociados);

            var spec = new DocumentoConclusaoFilterSpecification
            {
                SeqPessoaAtuacao = documentoConclusaoParaAlterarStatus.SeqAtuacaoAluno,
                TokensTipoDocumentoAcademico = tokensTipoDocumentoAcademico
            };

            if (tokenAcao == TOKEN_ACAO_DOCUMENTO_DIGITAL.ANULAR_DOCUMENTO_DIGITAL)
                spec.ListaClasseSituacaoDocumento = new List<ClasseSituacaoDocumento>() { ClasseSituacaoDocumento.EmissaoEmAndamento, ClasseSituacaoDocumento.Valido, ClasseSituacaoDocumento.Invalido };
            else
            {
                spec.ListaClasseSituacaoDocumento = new List<ClasseSituacaoDocumento>() { ClasseSituacaoDocumento.Invalido };
                spec.ListaMotivoInvalidadeDocumento = new List<MotivoInvalidadeDocumento?> { MotivoInvalidadeDocumento.Danificado, MotivoInvalidadeDocumento.Extraviado, MotivoInvalidadeDocumento.Descartado, MotivoInvalidadeDocumento.DadosInconsistentes };
            }

            var documentosConclusao = this.SearchProjectionBySpecification(spec, s => new
            {
                s.Seq,
                DescricaoTipoDocumentoAcademico = s.TipoDocumentoAcademico.Descricao,
                s.NumeroViaDocumento,
                DescricaoSituacaoAtual = s.SituacaoAtual.SituacaoDocumentoAcademico.Descricao,
                s.SituacaoAtual.SituacaoDocumentoAcademico.ClasseSituacaoDocumento,
                TipoInvalidade = (TipoInvalidade?)s.SituacaoAtual.ClassificacaoInvalidadeDocumento.TipoInvalidade,
                s.SolicitacaoServico.NumeroProtocolo,
                FormacoesEspecificas = s.FormacoesEspecificas.Select(x => new
                {
                    x.Seq,
                    x.SeqDocumentoConclusao,
                    x.AlunoFormacao.DescricaoDocumentoConclusao,
                    SeqGrauAcademico = (long?)x.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica.FirstOrDefault(f => f.SeqFormacaoEspecifica == x.AlunoFormacao.SeqFormacaoEspecifica).GrauAcademico.Seq,
                    SeqTitulacao = (long?)x.AlunoFormacao.Titulacao.Seq,
                }).ToList(),
            }).ToList();

            if (documentosConclusao == null || !documentosConclusao.Any())
                return (seqsDocumentosAssociados, documentosAssociados);

            foreach (var documentoConclusao in documentosConclusao)
            {
                if (documentoConclusao.ClasseSituacaoDocumento == ClasseSituacaoDocumento.Invalido && documentoConclusao.TipoInvalidade != TipoInvalidade.Temporario)
                    continue;

                bool temDocumento = documentoConclusao.FormacoesEspecificas.Any(w => w.DescricaoDocumentoConclusao.Trim() == documentoConclusaoParaAlterarStatus.FormacoesEspecificas.FirstOrDefault().DescricaoDocumentoConclusao.Trim() &&
                                                                                     w.SeqGrauAcademico == documentoConclusaoParaAlterarStatus.FormacoesEspecificas.FirstOrDefault().SeqGrauAcademico &&
                                                                                     w.SeqTitulacao == documentoConclusaoParaAlterarStatus.FormacoesEspecificas.FirstOrDefault().SeqTitulacao);
                if (temDocumento)
                {
                    seqsDocumentosAssociados.Add(documentoConclusao.Seq);
                    documentosAssociados += $"- {documentoConclusao.DescricaoTipoDocumentoAcademico} - {documentoConclusao.NumeroViaDocumento}° Via - {documentoConclusao.DescricaoSituacaoAtual} - {documentoConclusao.NumeroProtocolo} <br>";
                }
            }
            return (seqsDocumentosAssociados, documentosAssociados);
        }

        // RN_CNC_070 - Documento Conclusão - Cancelamento histórico escolar anterior
        public void InvalidarDocumentoConclusaoHistorico(long seqPessoaAtuacao, string descricaoCursoDocumento, long? seqCurso, long? seqGrauAcademico, long? seqTitulacao, string numeroProtocolo)
        {
            var spec = new DocumentoConclusaoFilterSpecification
            {
                SeqPessoaAtuacao = seqPessoaAtuacao,
                ListaClasseSituacaoDocumento = new List<ClasseSituacaoDocumento>() { ClasseSituacaoDocumento.EmissaoEmAndamento, ClasseSituacaoDocumento.Valido },
                GrupoDocumentoAcademico = GrupoDocumentoAcademico.HistoricoEscolar
            };

            var documentosConclusao = this.SearchProjectionBySpecification(spec, s => new
            {
                s.Seq,
                s.SeqSolicitacaoServico,
                s.SeqDocumentoGAD,
                s.TipoDocumentoAcademico.GrupoDocumentoAcademico,
                s.TipoDocumentoAcademico.Token,
                s.SolicitacaoServico.SituacaoAtual.CategoriaSituacao,
                s.SolicitacaoServico.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                FormacoesEspecificas = s.FormacoesEspecificas.Select(x => new
                {
                    x.Seq,
                    x.SeqDocumentoConclusao,
                    x.AlunoFormacao.DescricaoDocumentoConclusao,
                    x.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso,
                    SeqGrauAcademico = (long?)x.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica.FirstOrDefault(f => f.SeqFormacaoEspecifica == x.AlunoFormacao.SeqFormacaoEspecifica).GrauAcademico.Seq,
                    SeqTitulacao = (long?)x.AlunoFormacao.Titulacao.Seq,
                }).ToList(),
            }).ToList();

            foreach (var documentoConclusao in documentosConclusao)
            {
                if (documentoConclusao.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_PARCIAL && (documentoConclusao.FormacoesEspecificas == null || documentoConclusao.FormacoesEspecificas.Count() == 0))
                {
                    var seq = AlunoHistoricoDomainService.BuscarSequencialCursoAluno(seqPessoaAtuacao);
                    if (!seqCurso.HasValue || seq != seqCurso)
                        continue;
                }
                else if (!documentoConclusao.FormacoesEspecificas.Any(w => w.DescricaoDocumentoConclusao.Trim() == descricaoCursoDocumento.Trim() &&
                                                                     w.SeqCurso == seqCurso &&
                                                                     w.SeqGrauAcademico == seqGrauAcademico &&
                                                                     w.SeqTitulacao == seqTitulacao))
                {
                    continue;
                }

                //Motivo cancelamento
                var tokenRequeridoPeloSolicitante = TOKEN_MOTIVO_SITUACAO_CANCELAMENTO.REQUERIDO_PELO_SOLICITANTE;

                var templateProcesso = TipoTemplateProcessoService.BuscarTemplateProcesso(documentoConclusao.SeqTemplateProcessoSgf).FirstOrDefault();
                var seqSituacao = SituacaoService.BuscarSeqSituacaoPorToken(TOKEN_SITUACAO_SGF.SOLICITACAO_CANCELADA, templateProcesso.SeqTipoTemplateProcesso);
                var seqMotivoCancelamento = SituacaoService.BuscarSeqMotivoSituacaoPorToken(seqSituacao.Value, tokenRequeridoPeloSolicitante);

                if (seqMotivoCancelamento.GetValueOrDefault() == 0)
                    throw new TokenMotivoSolicitacaoIndeferidaNaoEncontradoException(tokenRequeridoPeloSolicitante);

                //Situação documento conclusão
                var specSituacaoDocumentoAcademico = new SituacaoDocumentoAcademicoFilterSpecification() { Token = TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.INVALIDO };
                var seqSituacaoDocumentoAcademico = SituacaoDocumentoAcademicoDomainService.SearchProjectionBySpecification(specSituacaoDocumentoAcademico, s => s.Seq).FirstOrDefault();

                if (seqSituacaoDocumentoAcademico == 0)
                    throw new TokenSituacaoDocumentoAcademicoNaoEncontradoException(TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.INVALIDO);

                var observacao = $"Histórico escolar invalidado mediante a emissão de nova via do histórico, conforme a solicitação através do protocolo: [{numeroProtocolo}].";
                if (documentoConclusao.Token == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_PARCIAL)
                {
                    observacao = $"Histórico escolar parcial invalidado mediante a emissão de nova via do histórico, conforme a solicitação através do protocolo: [{numeroProtocolo}].";
                }

                var seqClassificacaoInvalidadeDocumento = ClassificacaoInvalidadeDocumentoDomainService.BuscarSeqClassificacaoInvalidadeDocumentoPorToken(TOKEN_CLASSIFICACAO_INVALIDADE_DOCUMENTO.REEMISSAO_COMPLEMENTO_INFORMACAO);
                var modeloCancelarSolicitacao = new CancelamentoSolicitacaoVO()
                {
                    SeqSolicitacaoServico = documentoConclusao.SeqSolicitacaoServico.Value,
                    TipoCancelamento = TipoInvalidade.Permanente,
                    SeqMotivoCancelamento = seqMotivoCancelamento,
                    TokenMotivoCancelamento = tokenRequeridoPeloSolicitante,
                    Observacao = observacao,
                    SeqSituacaoDocumentoAcademico = seqSituacaoDocumentoAcademico,
                    MotivoInvalidadeDocumento = MotivoInvalidadeDocumento.Descartado,
                    ObservacaoDocumentoGAD = observacao,
                    SeqClassificacaoInvalidadeDocumento = seqClassificacaoInvalidadeDocumento
                };

                if (documentoConclusao.CategoriaSituacao != CategoriaSituacao.Encerrado)
                    this.SolicitacaoServicoDomainService.SalvarCancelamentoSolicitacao(modeloCancelarSolicitacao);
                else
                {
                    var novoDocumentoAcademicoHistoricoSituacao = new DocumentoAcademicoHistoricoSituacao()
                    {
                        SeqDocumentoAcademico = documentoConclusao.Seq,
                        SeqSituacaoDocumentoAcademico = modeloCancelarSolicitacao.SeqSituacaoDocumentoAcademico,
                        Observacao = modeloCancelarSolicitacao.Observacao,
                        MotivoInvalidadeDocumento = modeloCancelarSolicitacao.MotivoInvalidadeDocumento,
                        TipoInvalidade = modeloCancelarSolicitacao.TipoCancelamento,
                        SeqClassificacaoInvalidadeDocumento = modeloCancelarSolicitacao.SeqClassificacaoInvalidadeDocumento
                    };
                    DocumentoAcademicoHistoricoSituacaoDomainService.SaveEntity(novoDocumentoAcademicoHistoricoSituacao);

                    if (documentoConclusao.SeqDocumentoGAD.HasValue)
                    {
                        var usuarioInclusao = UsuarioLogado.RetornaUsuarioLogado();
                        var modeloDocumentoAcademico = new AtualizaStatusDocumentoAcademicoVO
                        {
                            SeqDocumentoAcademico = documentoConclusao.SeqDocumentoGAD.Value,
                            Observacao = modeloCancelarSolicitacao.Observacao,
                            TipoCancelamento = modeloCancelarSolicitacao.TipoCancelamento.SMCGetDescription(),
                            UsuarioInclusao = usuarioInclusao
                        };

                        var retornoGAD = APIHistoricoGAD.Execute<object>("Cancelar", modeloDocumentoAcademico);
                        var retornoCancelarHistoricoEscolar = JsonConvert.DeserializeObject<MensagemHttp>(retornoGAD.ToString());

                        if (!string.IsNullOrEmpty(retornoCancelarHistoricoEscolar.ErrorMessage))
                            throw new Exception(retornoCancelarHistoricoEscolar.ErrorMessage);
                    }
                }
            }
        }

        public bool DocumentoInvalidoTemporario(long seqDocumentoConclusao)
        {
            var retorno = false;
            var spec = new DocumentoConclusaoFilterSpecification() { Seq = seqDocumentoConclusao, TokenSituacaoAtual = TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.INVALIDO };
            var documentosConclusao = this.SearchProjectionByKey(spec, s => new
            {
                s.SituacaoAtual.SituacaoDocumentoAcademico.Token,
                TipoInvalidade = (TipoInvalidade?)s.SituacaoAtual.ClassificacaoInvalidadeDocumento.TipoInvalidade
            });

            if (documentosConclusao == null)
                return retorno;

            if (documentosConclusao.TipoInvalidade.HasValue && documentosConclusao.TipoInvalidade.Value == TipoInvalidade.Temporario)
                retorno = true;

            return retorno;
        }

        public DateTime? BuscarDataExpedicaoDiploma(long seqPessoaAtuacao, string descricaoCursoDocumento, long? seqGrauAcademico, long? seqTitulacao)
        {
            var spec = new DocumentoConclusaoFilterSpecification
            {
                SeqPessoaAtuacao = seqPessoaAtuacao,
                ListaClasseSituacaoDocumento = new List<ClasseSituacaoDocumento>() { ClasseSituacaoDocumento.Valido },
                TokensTipoDocumentoAcademico = new List<string>() { TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA, TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL }
            };

            var documentosConclusao = this.SearchProjectionBySpecification(spec, s => new
            {
                s.Seq,
                s.TipoDocumentoAcademico.Token,
                s.DataRegistro,
                FormacoesEspecificas = s.FormacoesEspecificas.Select(x => new
                {
                    x.Seq,
                    x.SeqDocumentoConclusao,
                    x.AlunoFormacao.DescricaoDocumentoConclusao,
                    SeqGrauAcademico = (long?)x.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica.FirstOrDefault(f => f.SeqFormacaoEspecifica == x.AlunoFormacao.SeqFormacaoEspecifica).GrauAcademico.Seq,
                    SeqTitulacao = (long?)x.AlunoFormacao.Titulacao.Seq,
                }).ToList(),
            }).ToList();

            DateTime? dataInclusao = null;
            if (documentosConclusao == null || !documentosConclusao.Any())
                return dataInclusao;

            foreach (var documentoConclusao in documentosConclusao)
            {
                if (documentoConclusao.FormacoesEspecificas.Any(w => w.DescricaoDocumentoConclusao.Trim() == descricaoCursoDocumento.Trim() &&
                                                                              w.SeqGrauAcademico == seqGrauAcademico &&
                                                                              w.SeqTitulacao == seqTitulacao))
                {
                    dataInclusao = documentoConclusao.DataRegistro;
                    break;
                }
            }
            return dataInclusao;
        }

        public bool ExibirNomeSocial(long seqPessoa, TipoPessoa fisica)
        {
            //Recupera o código de pessoa do CAD
            var codigoPessoaCAD = PessoaDomainService.BuscarCodigoDePessoaNosDadosMestres(seqPessoa, fisica, null);
            var exibirNomeSocial = PessoaService.AutorizarNomeSocial(codigoPessoaCAD);

            return exibirNomeSocial;
        }

        public long SalvarDocumentoConclusao(DocumentoConclusaoVO modelo)
        {
            var dominio = modelo.Transform<DocumentoConclusao>();
            this.SaveEntity(dominio);

            return dominio.Seq;
        }
    }
}