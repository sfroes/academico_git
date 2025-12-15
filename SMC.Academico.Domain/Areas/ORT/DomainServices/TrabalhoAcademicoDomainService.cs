using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Areas.ORT.Exceptions;
using SMC.Academico.Common.Areas.ORT.Includes;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.Common.Enums;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Areas.ORT.Resources;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.ORT.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.DomainServices;
using SMC.Academico.Domain.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Repository;
using SMC.Framework.Security;
using SMC.Framework.Security.Util;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Data;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORT.DomainServices
{
    public class TrabalhoAcademicoDomainService : AcademicoContextDomain<TrabalhoAcademico>
    {
        #region Querys

        private string _buscaComprovanteEntregaTrabalhoAcademico = @"
            declare @SEQ_TRABALHO bigint
            set @SEQ_TRABALHO = {0}

            select
	            ta.seq_trabalho_academico as SeqTrabalhoAcademicos,
	            ie.seq_instituicao_nivel as SeqInstituicaoNivel,
	            a.num_registro_academico as NumeroRegistroAcademico,
	            taa.nom_autor as NomeAutor,
	            ev.nom_entidade as NomeEntidadeResponsavel, -- NomeEntidadeVinculo
	            ec.nom_entidade as NomeCurso,
	            ne.dsc_nivel_ensino as DescricaoNivelEnsino,
	            t.dsc_turno as DescricaoTurno,
	            cast(cl.num_ciclo_letivo as varchar) + 'º/' + cast(cl.ano_ciclo_letivo as varchar) as DescricaoCicloLetivoIngresso,
	            tt.dsc_tipo_trabalho as DescricaoTipoTrabalho,
	            ta.dsc_titulo as DescricaoTitulo,
	            ta.dat_deposito_secretaria as DataDepositoSecretaria,
	            isnull(en.nom_cidade, '_____________________________________') as NomeCidadeLocalidade
            from	ORT.trabalho_academico ta
            join	ORT.tipo_trabalho tt
		            on ta.seq_tipo_trabalho = tt.seq_tipo_trabalho
            join	ORT.trabalho_academico_autoria taa
		            on ta.seq_trabalho_academico = taa.seq_trabalho_academico
            join	ALN.aluno a
		            on taa.seq_atuacao_aluno = a.seq_pessoa_atuacao
            join	ALN.aluno_historico ah
		            on a.seq_pessoa_atuacao = ah.seq_atuacao_aluno
		            and ah.ind_atual = 1
			join	org.entidade ev
					on ah.seq_entidade_vinculo = ev.seq_entidade
            join	CSO.curso_oferta_localidade_turno colt
		            on ah.seq_curso_oferta_localidade_turno = colt.seq_curso_oferta_localidade_turno
            join	CSO.turno t
		            on colt.seq_turno = t.seq_turno
            join	CAM.ciclo_letivo cl
		            on ah.seq_ciclo_letivo = cl.seq_ciclo_letivo
            join	CSO.curso_oferta_localidade col
		            on colt.seq_entidade_curso_oferta_localidade = col.seq_entidade
            join	CSO.curso_unidade cu
		            on col.seq_entidade_curso_unidade = cu.seq_entidade
            join	CSO.curso c
		            on cu.seq_entidade_curso = c.seq_entidade
            join	ORG.entidade ec
		            on c.seq_entidade = ec.seq_entidade
            join	ORG.nivel_ensino ne
		            on c.seq_nivel_ensino = ne.seq_nivel_ensino
            left join	ORG.entidade_endereco ee
			            on col.seq_entidade = ee.seq_entidade
            left join	endereco en
			            on ee.seq_endereco = en.seq_endereco
			            and en.ind_correspondencia = 1
            join	ORG.instituicao_nivel ie
		            on c.seq_nivel_ensino = ie.seq_nivel_ensino
		            and ta.seq_entidade_instituicao = ie.seq_entidade_instituicao
            where	ta.seq_trabalho_academico = @SEQ_TRABALHO";

        public DateTime? BuscarDataDepositoSecretaria(long seqTrabalhoAcademico)
        {
            return this.SearchProjectionByKey(seqTrabalhoAcademico, x => x.DataDepositoSecretaria);
        }

        #endregion Querys

        #region Domain Services

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private CriterioAprovacaoDomainService CriterioAprovacaoDomainService => Create<CriterioAprovacaoDomainService>();

        private HistoricoEscolarDomainService HistoricoEscolarDomainService => Create<HistoricoEscolarDomainService>();

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();
        private PessoaAtuacaoBloqueioDomainService PessoaAtuacaoBloqueioDomainService => Create<PessoaAtuacaoBloqueioDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private PublicacaoBdpIdiomaDomainService PublicacaoBdpIdiomaDomainService => Create<PublicacaoBdpIdiomaDomainService>();

        private PublicacaoBdpHistoricoSituacaoDomainService PublicacaoBdpHistoricoSituacaoDomainService => this.Create<PublicacaoBdpHistoricoSituacaoDomainService>();

        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();

        private DivisaoMatrizCurricularComponenteDomainService DivisaoMatrizCurricularComponenteDomainService { get => Create<DivisaoMatrizCurricularComponenteDomainService>(); }

        private InstituicaoNivelTipoTrabalhoDomainService InstituicaoNivelTipoTrabalhoDomainService { get => Create<InstituicaoNivelTipoTrabalhoDomainService>(); }

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService { get => Create<FormacaoEspecificaDomainService>(); }

        private ClassificacaoDomainService ClassificacaoDomainService { get => Create<ClassificacaoDomainService>(); }

        private MembroBancaExaminadoraDomainService MembroBancaExaminadoraDomainService { get => Create<MembroBancaExaminadoraDomainService>(); }

        private TrabalhoAcademicoAutoriaDomainService TrabalhoAcademicoAutoriaDomainService { get => Create<TrabalhoAcademicoAutoriaDomainService>(); }

        private DivisaoComponenteDomainService DivisaoComponenteDomainService { get => Create<DivisaoComponenteDomainService>(); }

        private TrabalhoAcademicoDivisaoComponenteDomainService TrabalhoAcademicoDivisaoComponenteDomainService { get => Create<TrabalhoAcademicoDivisaoComponenteDomainService>(); }

        private AlunoHistoricoDomainService AlunoHistoricoDomainService { get => Create<AlunoHistoricoDomainService>(); }

        private OrigemAvaliacaoDomainService OrigemAvaliacaoDomainService { get => Create<OrigemAvaliacaoDomainService>(); }

        private ApuracaoFormacaoDomainService ApuracaoFormacaoDomainService { get => Create<ApuracaoFormacaoDomainService>(); }

        private ISMCReportMergeService SMCReportMergeService
        {
            get { return this.Create<ISMCReportMergeService>(); }
        }

        private InstituicaoNivelModeloRelatorioDomainService InstituicaoNivelModeloRelatorioDomainService
        {
            get { return this.Create<InstituicaoNivelModeloRelatorioDomainService>(); }
        }

        private AlunoDomainService AlunoDomainService { get { return this.Create<AlunoDomainService>(); } }

        private PeriodoIntercambioDomainService PeriodoIntercambioDomainService => this.Create<PeriodoIntercambioDomainService>();

        private ProgramaTipoAutorizacaoBdpDomainService ProgramaTipoAutorizacaoBdpDomainService => this.Create<ProgramaTipoAutorizacaoBdpDomainService>();

        private ProgramaDomainService ProgramaDomainService => this.Create<ProgramaDomainService>();

        private PublicacaoBdpAutorizacaoDomainService PublicacaoBdpAutorizacaoDomainService => this.Create<PublicacaoBdpAutorizacaoDomainService>();

        private PublicacaoBdpDomainService PublicacaoBdpDomainService => this.Create<PublicacaoBdpDomainService>();

        private ArquivoAnexadoDomainService ArquivoAnexadoDomainService => Create<ArquivoAnexadoDomainService>();

        private AlunoHistoricoPrevisaoConclusaoDomainService AlunoHistoricoPrevisaoConclusaoDomainService => Create<AlunoHistoricoPrevisaoConclusaoDomainService>();

        private CicloLetivoDomainService CicloLetivoDomainService => Create<CicloLetivoDomainService>();
        private PlanoEstudoDomainService PlanoEstudoDomainService => Create<PlanoEstudoDomainService>();
        private MatrizCurricularOfertaDomainService MatrizCurricularOfertaDomainService => Create<MatrizCurricularOfertaDomainService>();
        private MatrizCurricularDomainService MatrizCurricularDomainService => Create<MatrizCurricularDomainService>();
        private PlanoEstudoItemDomainService PlanoEstudoItemDomainService => Create<PlanoEstudoItemDomainService>();
        private ConfiguracaoComponenteDomainService ConfiguracaoComponenteDomainService => Create<ConfiguracaoComponenteDomainService>();

        #endregion Domain Services

        #region Services

        private IIntegracaoAcademicoService IntegracaoAcademicoService => Create<IIntegracaoAcademicoService>();

        #endregion Services

        public SMCPagerData<TrabalhoAcademicoListaVO> BuscarTrabalhosAcademicos(TrabalhoAcademicoFilterSpecification filtro)
        {
            filtro.SetOrderBy(x => x.Autores.FirstOrDefault().NomeAutor);

            var result = this.SearchProjectionBySpecification(filtro, p => new TrabalhoAcademicoListaVO()
            {
                Seq = p.Seq,
                Data = p.DataDepositoSecretaria,
                Titulo = p.Titulo,
                TituloPortugues = p.PublicacaoBdp.FirstOrDefault().InformacoesIdioma.FirstOrDefault(f => f.Idioma == SMCLanguage.Portuguese).Titulo,
                TipoTrabalho = p.TipoTrabalho.Descricao,
                SeqTipoTrabalho = p.SeqTipoTrabalho,
                SeqNivelEnsino = p.SeqNivelEnsino,
                SeqInstituicaoEnsino = p.SeqInstituicaoEnsino,
                DataInicioAplicacaoAvaliacao = p.DivisoesComponente.SelectMany(d => d.OrigemAvaliacao.AplicacoesAvaliacao).Where(a => !a.DataCancelamento.HasValue).OrderByDescending(a => a.Seq).FirstOrDefault().DataInicioAplicacaoAvaliacao,
                NumeroDefesa = p.DivisoesComponente.SelectMany(d => d.OrigemAvaliacao.AplicacoesAvaliacao).Where(a => !a.DataCancelamento.HasValue).OrderByDescending(a => a.Seq).FirstOrDefault().ApuracoesAvaliacao.FirstOrDefault().NumeroDefesa,
                SituacaoHistoricoEscolar = p.DivisoesComponente.Select(oa => oa.OrigemAvaliacao.HistoricosEscolares.FirstOrDefault().SituacaoHistoricoEscolar).FirstOrDefault(),
                JustificativaSegundoDeposito = p.JustificativaSegundoDeposito,
                UsuarioInclusaoSegundoDeposito = p.UsuarioInclusaoSegundoDeposito,
                DataInclusaoSegundoDeposito = p.DataInclusaoSegundoDeposito,
                DataAutorizacaoSegundoDeposito = p.DataAutorizacaoSegundoDeposito,
                ArquivoAnexadoSegundoDeposito = p.SeqArquivoAnexadoSegundoDeposito.HasValue ? new SMCUploadFile
                {
                    GuidFile = p.ArquivoAnexadoSegundoDeposito.UidArquivo.ToString(),
                    Name = p.ArquivoAnexadoSegundoDeposito.Nome,
                    Size = p.ArquivoAnexadoSegundoDeposito.Tamanho,
                    Type = p.ArquivoAnexadoSegundoDeposito.Tipo
                } : null,
                Alunos = p.Autores.Select(s => new TrabalhoAcademicoAutorListaVO()
                {
                    SeqAluno = s.SeqAluno,
                    NumeroRegistroAcademico = s.Aluno.NumeroRegistroAcademico,
                    Nome = s.Aluno.DadosPessoais.Nome,
                    NomeSocial = s.Aluno.DadosPessoais.NomeSocial,
                    CursoOfertaLocalidade = s.Aluno.Historicos.Where(w => w.Atual).FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome,
                    Turno = s.Aluno.Historicos.Where(w => w.Atual).FirstOrDefault().CursoOfertaLocalidadeTurno.Turno.Descricao,

                }).ToList(),
                ProtocoloSolicitacaoInclusao = p.SolicitacaoServico.NumeroProtocolo
            }, out int total).ToList();

            foreach (var item in result)
            {
                var specInstituicaoNivelTipoTrabalho = new InstituicaoNivelTipoTrabalhoFilterSpecification()
                {
                    SeqInstituicaoEnsino = item.SeqInstituicaoEnsino,
                    SeqNivelEnsino = item.SeqNivelEnsino,
                    SeqTipoTrabalho = item.SeqTipoTrabalho
                };

                var resultado = this.InstituicaoNivelTipoTrabalhoDomainService.SearchProjectionByKey(specInstituicaoNivelTipoTrabalho, i => new
                {
                    geraFinanceiro = i.GeraFinanceiroEntregaTrabalho,
                    publicacaoBibliotecaObrigatoria = i.PublicacaoBibliotecaObrigatoria
                });

                item.Autores = new List<string>();
                item.NomesAutores = new List<string>();
                item.GeraFinanceiroEntregaTrabalho = resultado.geraFinanceiro;
                item.PublicacaoBibliotecaObrigatoria = resultado.publicacaoBibliotecaObrigatoria;

                foreach (var aluno in item.Alunos)
                {
                    var nome = string.IsNullOrEmpty(aluno.NomeSocial) ? aluno.Nome : $"{aluno.NomeSocial} ({aluno.Nome})";
                    var situacaoAtual = this.AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(aluno.SeqAluno, true);

                    aluno.TipoSituacao = situacaoAtual.Descricao;
                    aluno.CicloLetivo = situacaoAtual.DescricaoCicloLetivo;

                    //Nomes sem Formatações, Exibidos, principalmente para confirmação de Exclusão
                    item.NomesAutores.Add(nome);

                    item.Autores.Add(string.Format(MessagesResource.LabelAlunoFormatado, aluno.NumeroRegistroAcademico,
                                                  nome, aluno.CursoOfertaLocalidade, aluno.Turno, situacaoAtual.DescricaoCicloLetivo, situacaoAtual.Descricao));
                }

            }

            return new SMCPagerData<TrabalhoAcademicoListaVO>(result, total);
        }

        // A pesquisa avançada é feita com querys por causa da busca na hierarquia de entidades
        public SMCPagerData<TrabalhoAcademicoListaVO> ConsultaAvancadaTrabalhosAcademicos(TrabalhoAcademicoFiltroVO filtro)
        {
            string filtroPrograma = (filtro.SeqPrograma.HasValue) ?
                                        $"and ah.seq_entidade_vinculo = {filtro.SeqPrograma.Value}" :
                                        "";

            string filtroTipoTrabalho = (filtro.SeqsTipoTrabalho != null && filtro.SeqsTipoTrabalho.Count > 0) ?
                                        $"and ta.seq_tipo_trabalho in ({string.Join(",", filtro.SeqsTipoTrabalho)})" :
                                        "";

            // FIX: new SMCFuncParameter não está funcionando com DateTime.
            string filtroDataInicio = (filtro.DataInicio.HasValue) ?
                                        $"and aa.dat_inicio_aplicacao_avaliacao >= '{filtro.DataInicio.Value.ToString("yyyy-MM-dd 00:00:00")}'" :
                                        "";

            string filtroDataFim = (filtro.DataFim.HasValue) ?
                                        $"and aa.dat_inicio_aplicacao_avaliacao <= '{filtro.DataFim.Value.ToString("yyyy-MM-dd 23:59:59")}'" :
                                        "";

            string filtroTituloResumo = (!string.IsNullOrEmpty(filtro.TituloResumo)) ?
                                        $" and (pdi.dsc_titulo like '%{filtro.TituloResumo}%' or pdi.dsc_resumo like '%{filtro.TituloResumo}%')" :
                                        string.Empty;

            //Condicional Where do resultado da pesquisa sobre nome(autor, orientador, coorientador)
            string filtroAutorOrientadorCoorientador = string.Empty;

            if (!string.IsNullOrEmpty(filtro.Nome))
            {
                filtroAutorOrientadorCoorientador = " where ";
                if (!filtro.TipoPesquisaTrabalho.SMCAny())
                {
                    filtroAutorOrientadorCoorientador += $"A1.Autor like '%{filtro.Nome}%' or A1.Orientador like '%{filtro.Nome}%' or A1.Coorientador like '%{filtro.Nome}%'";
                }
                else
                {
                    foreach (var item in filtro.TipoPesquisaTrabalho)
                    {
                        switch (item)
                        {
                            case TipoPesquisaTrabalhoAcademico.Autor:
                                filtroAutorOrientadorCoorientador += $"A1.Autor like '%{filtro.Nome}%' or ";
                                break;

                            case TipoPesquisaTrabalhoAcademico.Orientador:
                                filtroAutorOrientadorCoorientador += $"A1.Orientador like '%{filtro.Nome}%' or ";
                                break;

                            case TipoPesquisaTrabalhoAcademico.Coorientador:
                                filtroAutorOrientadorCoorientador += $"A1.Coorientador like '%{filtro.Nome}%' or ";
                                break;

                            default:
                                break;
                        }
                    }

                    filtroAutorOrientadorCoorientador = filtroAutorOrientadorCoorientador.Substring(0, filtroAutorOrientadorCoorientador.Length - 4);
                }
            }

            string filtroSituacaoTrabalhoAcademico = string.Empty;
            if (filtro.EmPublicacao.HasValue)
            {
                //Na consulta "Em processo de publicação" listar os registros cujo o ano da defesa seja maior ou igual a 2019;
                filtroDataInicio = $"and aa.dat_inicio_aplicacao_avaliacao >= '2019-01-01 00:00:00'";

                filtroSituacaoTrabalhoAcademico = $"({(int)SituacaoTrabalhoAcademico.CadastradaAluno},{(int)SituacaoTrabalhoAcademico.AutorizadaLiberadaSecretaria},{(int)SituacaoTrabalhoAcademico.LiberadaBiblioteca})";
            }
            else
            {
                filtroSituacaoTrabalhoAcademico = $"({(int)SituacaoTrabalhoAcademico.LiberadaConsulta})";
            }

            string filtroAreaConhecimento = string.Empty;
            if (filtro.SeqAreaConhecimento.HasValue)
            {
                var classificacoes = ClassificacaoDomainService.BuscarHierarquiaInferiorClassificacao(filtro.SeqAreaConhecimento.Value);
                filtroAreaConhecimento = $@"
		                                    inner join
			                                    cso.curso_oferta_localidade_turno t
                                                on t.seq_curso_oferta_localidade_turno = ah.seq_curso_oferta_localidade_turno
                                            inner join
			                                    cso.curso_oferta_localidade l
                                                on l.seq_entidade = t.seq_entidade_curso_oferta_localidade
                                            inner join
			                                    cso.curso_oferta co
                                                on co.seq_curso_oferta = l.seq_curso_oferta
                                            inner join
			                                    cso.entidade_classificacao ec
                                                on ec.seq_entidade = co.seq_entidade_curso
                                            inner join
			                                    cso.classificacao c
                                                on c.seq_classificacao = ec.seq_classificacao
                                                and c.seq_classificacao in ({string.Join(",", classificacoes)})
                                            inner join
			                                    cso.hierarquia_classificacao hc
                                                on hc.seq_hierarquia_classificacao = c.seq_hierarquia_classificacao
                                            inner join
			                                    cso.tipo_hierarquia_classificacao thc
                                                on thc.seq_tipo_hierarquia_classificacao = hc.seq_tipo_hierarquia_classificacao
                                                and thc.dsc_token = '{TOKEN_TIPO_HIERARQUIA_CLASSIFICACAO.HIERARQUIA_CAPES}'";
            }

            //Cria a ordenação sempre levando em consideração primeiro o Tipo de trabalho e validando se existe mais ordenações
            string orderBy = "A1.DescricaoTipoTrabalho ASC";

            if (filtro.PageSettings.SortInfo.SMCAny())
            {
                orderBy += "," + string.Join(",", filtro.PageSettings.SortInfo.Select(f => $"A1.{f.FieldName} {SMCSortInfo.GetNameDirection(f.Direction)}"));
            }

            var offset = (filtro.PageSettings.PageIndex - 1) * filtro.PageSettings.PageSize;
            string query = $@"select *, count(*) over() Total
	                          from (
                                    select
                                        ta.seq_trabalho_academico as Seq,
                                        ta.dsc_titulo as Titulo,
                                        nv.dsc_nivel_ensino as DescricaoNivelEnsino,
                                        pdi.dsc_titulo as TituloPortugues,
                                        pdi.dsc_resumo as ResumoPortugues,
                                        ta.seq_tipo_trabalho as SeqTipoTrabalho,
                                        tt.dsc_tipo_trabalho as DescricaoTipoTrabalho,
	                                    aa.dat_inicio_aplicacao_avaliacao as [Data],
	                                    STUFF((select '|' + nom_autor +' ('+ nom_autor_formatado +')'
		                                    from
			                                    ort.trabalho_academico_autoria
		                                    where
			                                    seq_trabalho_academico = ta.seq_trabalho_academico
		                                    for xml path ('')), 1, 1, '')  as Autor,
                                        STUFF((select '|' + isnull(pdp.nom_pessoa,mba.nom_colaborador)
                                            from
                                                 apr.membro_banca_examinadora mba
                                                 left join pes.pessoa_atuacao pa
                                                        on pa.seq_pessoa_atuacao = mba.seq_atuacao_colaborador
                                                 left join pes.pessoa_dados_pessoais pdp
                                                        on pdp.seq_pessoa_dados_pessoais = pa.seq_pessoa_dados_pessoais
                                            where
			                                      mba.seq_aplicacao_avaliacao = aa.seq_aplicacao_avaliacao
                                                  and mba.idt_dom_tipo_membro_banca = 4
		                                          for xml path ('')), 1, 1, '') as Orientador,
                                        STUFF((select '|' + isnull(pdp.nom_pessoa,mba.nom_colaborador)
                                            from
                                                   apr.membro_banca_examinadora mba
                                                   left join pes.pessoa_atuacao pa
                                                        on pa.seq_pessoa_atuacao = mba.seq_atuacao_colaborador
                                                   left join pes.pessoa_dados_pessoais pdp
                                                        on pdp.seq_pessoa_dados_pessoais = pa.seq_pessoa_dados_pessoais
                                            where
			                                        mba.seq_aplicacao_avaliacao = aa.seq_aplicacao_avaliacao
			                                        and mba.idt_dom_tipo_membro_banca = 1
                                                    and mba.ind_participou = 1
		                                            for xml path ('')), 1, 1, '') as Coorientador
                                    from
                                        ort.trabalho_academico ta
                                        -- tipo trabalho para agrupar
	                                    inner join
		                                    ort.tipo_trabalho tt
		                                    on ta.seq_tipo_trabalho = tt.seq_tipo_trabalho
										-- nivel de ensino
  										inner join org.nivel_ensino nv
                                            on nv.seq_nivel_ensino = ta.seq_nivel_ensino
                                        inner join org.instituicao_nivel ine
                                            on ine.seq_nivel_ensino = nv.seq_nivel_ensino
                                            and ine.seq_entidade_instituicao = ta.seq_entidade_instituicao
                                        inner join ort.instituicao_nivel_tipo_trabalho inv
                                            on inv.seq_tipo_trabalho = ta.seq_tipo_trabalho
                                            and inv.seq_instituicao_nivel = ine.seq_instituicao_nivel
                                            and inv.ind_publicacao_biblioteca_obrigatoria = 1
	                                    -- publicações
	                                    inner join
		                                    ort.publicacao_bdp pb
		                                    on ta.seq_trabalho_academico = pb.seq_trabalho_academico
                                        --idiomas
									    inner join
										 ort.publicacao_bdp_idioma pdi
										 on pb.seq_publicacao_bdp = pdi.seq_publicacao_bdp
										 and pdi.ind_idioma_trabalho = 1
										 {filtroTituloResumo}
                                        --situações
	                                    inner join
		                                    ort.publicacao_bdp_historico_situacao pbhs
		                                    on pb.seq_publicacao_bdp = pbhs.seq_publicacao_bdp
		                                    -- filtra pela ultima situacao
		                                    and pbhs.seq_publicacao_bdp_historico_situacao = (select top 1 seq_publicacao_bdp_historico_situacao
															                                    from ort.publicacao_bdp_historico_situacao ipbhs
															                                    where ipbhs.seq_publicacao_bdp = pb.seq_publicacao_bdp
															                                    order by dat_inclusao desc)
		                                    -- liberado para consulta
		                                    and pbhs.idt_dom_situacao_trabalho_academico in {filtroSituacaoTrabalhoAcademico}

                                        -- autores
	                                    inner join
		                                    ort.trabalho_academico_autoria taa
		                                    on ta.seq_trabalho_academico = taa.seq_trabalho_academico

                                        -- data publicação
	                                    inner join
		                                    ort.trabalho_academico_divisao_componente tadc
		                                    on ta.seq_trabalho_academico = tadc.seq_trabalho_academico
	                                    inner join
		                                    apr.aplicacao_avaliacao aa
		                                    on tadc.seq_origem_avaliacao = aa.seq_origem_avaliacao
		                                    -- desconsidera avaliações canceladas
		                                    and aa.dat_cancelamento is null
		                                    -- filtra pela ultima avaliação
		                                    and aa.seq_aplicacao_avaliacao = (select top 1 iaa.seq_aplicacao_avaliacao
										                                        from apr.aplicacao_avaliacao iaa
										                                        where iaa.seq_origem_avaliacao = tadc.seq_origem_avaliacao
										                                        order by iaa.seq_aplicacao_avaliacao desc)
                                            {filtroDataInicio}
                                            {filtroDataFim}

                                        -- filtra pelas avaliações do tipo banca
                                        inner join
		                                        apr.avaliacao a
		                                        on a.seq_avaliacao = aa.seq_avaliacao
		                                        -- tipo banca
		                                        and a.idt_dom_tipo_avaliacao = 1

	                                    -- Filtro de programa
	                                    inner join
		                                    aln.aluno_historico ah
		                                    on taa.seq_atuacao_aluno = ah.seq_atuacao_aluno
		                                    and ah.ind_atual = 1
                                            {filtroPrograma}

                                        -- Filtro area de conhecimento
                                        {filtroAreaConhecimento}
                                    where
	                                    ta.seq_entidade_instituicao = @seq_entidade_instituicao
                                        {filtroTipoTrabalho}
                            ) as A1
                                {filtroAutorOrientadorCoorientador}
                            group by
	                            A1.Seq, A1.DescricaoNivelEnsino, A1.Titulo, A1.SeqTipoTrabalho, A1.DescricaoTipoTrabalho, A1.Autor, A1.Coorientador, A1.Data, A1.Orientador, A1.TituloPortugues, A1.ResumoPortugues
                            order by
	                            {orderBy}
                            offset {offset} rows fetch next {filtro.PageSettings.PageSize} rows only";

            var result = RawQuery<TrabalhoAcademicoListaVO>(query,
                                new SMCFuncParameter("seq_entidade_instituicao", filtro.SeqInstituicaoLogada));

            //Trabalho o resumo da pesquisa
            result = this.AjustarResumo(result, filtro.TituloResumo);

            int total = 0;
            foreach (var item in result)
            {
                total = item.Total;
                item.Autores = item.Autor?.Split('|').ToList();
                item.Orientadores = item.Orientador?.Split('|').OrderBy(o => o).ToList();
                item.Coorientadores = item.Coorientador?.Split('|').OrderBy(o => o).ToList();
            }

            return new SMCPagerData<TrabalhoAcademicoListaVO>(result, total);
        }

        // A pesquisa para futuras defesas
        public SMCPagerData<TrabalhoAcademicoListaVO> ConsultaFuturasDefesasTrabalhosAcademicos(TrabalhoAcademicoFiltroVO filtro)
        {
            string dataFutura = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            string filtroSituacaoFutura = $" where A1.Data >= '{dataFutura}'";

            //Cria a ordenação sempre levando em consideração primeiro o Tipo de trabalho e validando se existe mais ordenações
            string orderBy = "A1.DescricaoTipoTrabalho ASC";

            if (filtro.PageSettings.SortInfo.SMCAny())
            {
                orderBy += "," + string.Join(",", filtro.PageSettings.SortInfo.Select(f => $"A1.{f.FieldName} {SMCSortInfo.GetNameDirection(f.Direction)}"));
            }

            var offset = (filtro.PageSettings.PageIndex - 1) * filtro.PageSettings.PageSize;
            string query = $@"select *, count(*) over() Total
                                 from (
                                    select
                                        ta.seq_trabalho_academico as Seq,
                                        ta.dsc_titulo as Titulo,
                                        nv.dsc_nivel_ensino as DescricaoNivelEnsino,
                                        ta.seq_tipo_trabalho as SeqTipoTrabalho,
                                        tt.dsc_tipo_trabalho as DescricaoTipoTrabalho,
                                           aa.dat_inicio_aplicacao_avaliacao as [Data],
                                           STUFF((select '|' + nom_autor +' ('+ nom_autor_formatado +')'
                                                 from
                                                        ort.trabalho_academico_autoria
                                                 where
                                                        seq_trabalho_academico = ta.seq_trabalho_academico
                                                 for xml path ('')), 1, 1, '')  as Autor,
                                           STUFF((select '|' + isnull(pdp.nom_pessoa,mba.nom_colaborador)
                                                 from
                                                        apr.membro_banca_examinadora mba
                                                        left join pes.pessoa_atuacao pa
                                                            on pa.seq_pessoa_atuacao = mba.seq_atuacao_colaborador
                                                        left join pes.pessoa_dados_pessoais pdp
                                                            on pdp.seq_pessoa_dados_pessoais = pa.seq_pessoa_dados_pessoais
                                                  where
                                                        mba.seq_aplicacao_avaliacao = aa.seq_aplicacao_avaliacao
                                                        and mba.idt_dom_tipo_membro_banca = 4
                                                 for xml path ('')), 1, 1, '') as Orientador,
                                           STUFF((select '|' + isnull(pdp.nom_pessoa,mba.nom_colaborador)
                                                 from
                                                        apr.membro_banca_examinadora mba
                                                        left join pes.pessoa_atuacao pa
                                                            on pa.seq_pessoa_atuacao = mba.seq_atuacao_colaborador
                                                        left join pes.pessoa_dados_pessoais pdp
                                                            on pdp.seq_pessoa_dados_pessoais = pa.seq_pessoa_dados_pessoais
                                                  where
                                                        mba.seq_aplicacao_avaliacao = aa.seq_aplicacao_avaliacao
                                                        and mba.idt_dom_tipo_membro_banca = 1
                                                 for xml path ('')), 1, 1, '') as Coorientador
                                    from
                                        ort.trabalho_academico ta
                                        -- tipo trabalho para agrupar
                                           inner join
                                                 ort.tipo_trabalho tt
                                                 on ta.seq_tipo_trabalho = tt.seq_tipo_trabalho
                                        -- nivel de ensino
                                           inner join org.nivel_ensino nv
                                                 on nv.seq_nivel_ensino = ta.seq_nivel_ensino
                                           inner join org.instituicao_nivel ine
                                                 on ine.seq_nivel_ensino = nv.seq_nivel_ensino
                                                 and ine.seq_entidade_instituicao = ta.seq_entidade_instituicao
                                           inner join ort.instituicao_nivel_tipo_trabalho inv
                                                 on inv.seq_tipo_trabalho = ta.seq_tipo_trabalho
                                                 and inv.seq_instituicao_nivel = ine.seq_instituicao_nivel
                                                 and inv.ind_publicacao_biblioteca_obrigatoria = 1
                                        -- autores
                                           inner join
                                                 ort.trabalho_academico_autoria taa
                                                 on ta.seq_trabalho_academico = taa.seq_trabalho_academico
                                        -- data publicação
                                           inner join
                                                 ort.trabalho_academico_divisao_componente tadc
                                                 on ta.seq_trabalho_academico = tadc.seq_trabalho_academico
                                           inner join
                                                 apr.aplicacao_avaliacao aa
                                                 on tadc.seq_origem_avaliacao = aa.seq_origem_avaliacao
                                        -- filtra pela ultima avaliação
                                                 and aa.seq_aplicacao_avaliacao = (select top 1 iaa.seq_aplicacao_avaliacao
																					from apr.aplicacao_avaliacao iaa
                                                                                    where iaa.seq_origem_avaliacao = tadc.seq_origem_avaliacao
                                                                                    -- desconsidera avaliações canceladas
                                                                                    and aa.dat_cancelamento is null
                                                                                    order by iaa.seq_aplicacao_avaliacao desc)
                                        -- filtra pelas avaliações do tipo banca
											inner join apr.avaliacao a
                                                  on a.seq_avaliacao = aa.seq_avaliacao
                                        -- tipo banca
                                                  and a.idt_dom_tipo_avaliacao = 1
										-- Filtro de programa
											inner join aln.aluno_historico ah
                                                  on taa.seq_atuacao_aluno = ah.seq_atuacao_aluno
                                                 and ah.ind_atual = 1
                                         -- Filtro area de conhecimento
                                    where
                                           ta.seq_entidade_instituicao = @seq_entidade_instituicao

                            ) as A1
                            {filtroSituacaoFutura}
                            group by
                                   A1.Seq, A1.DescricaoNivelEnsino, A1.Titulo, A1.SeqTipoTrabalho, A1.DescricaoTipoTrabalho, A1.Autor, A1.Coorientador, A1.Data, A1.Orientador
                            order by
                                   {orderBy}
                            offset {offset} rows fetch next {filtro.PageSettings.PageSize} rows only
            ";

            var result = RawQuery<TrabalhoAcademicoListaVO>(query,
                                new SMCFuncParameter("seq_entidade_instituicao", filtro.SeqInstituicaoLogada));

            //Trabalho o resumo da pesquisa
            result = this.AjustarResumo(result, filtro.TituloResumo);

            int total = 0;
            foreach (var item in result)
            {
                total = item.Total;
                item.Autores = item.Autor?.Split('|').ToList();
                item.Orientadores = item.Orientador?.Split('|').OrderBy(o => o).ToList();
                item.Coorientadores = item.Coorientador?.Split('|').OrderBy(o => o).ToList();
            }

            return new SMCPagerData<TrabalhoAcademicoListaVO>(result, total);
        }

        private List<TrabalhoAcademicoListaVO> AjustarResumo(List<TrabalhoAcademicoListaVO> trabalhosAcademicos, string tituloResumo)
        {
            List<TrabalhoAcademicoListaVO> retorno = new List<TrabalhoAcademicoListaVO>();
            string resumo;
            bool existeTituloResumo = !string.IsNullOrEmpty(tituloResumo);

            foreach (var trabalho in trabalhosAcademicos)
            {
                try
                {
                    resumo = string.Empty;

                    if (existeTituloResumo && (trabalho.ResumoPortugues.IndexOf(tituloResumo, StringComparison.InvariantCultureIgnoreCase) > -1))
                    {
                        int totalCaractesres = 190;
                        int numeroCaracteresIncio = 30;
                        int numeroCaracteresFim;
                        int numeroCaracteresPesquisa = tituloResumo.Length;
                        int numeroCaracteresResumo = trabalho.ResumoPortugues.Length;
                        int posicaoAtual = trabalho.ResumoPortugues.IndexOf(tituloResumo, StringComparison.InvariantCultureIgnoreCase);
                        int posicaoInicio;
                        int posicaoFim;
                        string primeiraParte = string.Empty;
                        string segundaParte = string.Empty;
                        string reticenciasIncio = "...";
                        string reticenciasFim = "...";

                        //Cenario 1: onde a palavra pesquisada esta no incio da frase e o fim esta antes do fim
                        if (posicaoAtual < 30)
                        {
                            //significa que iremos começar a pesquisa no incio do resumo
                            posicaoInicio = 0;
                            reticenciasIncio = string.Empty;
                        }
                        else
                        {
                            //ele irá acrecentar a pesquisa buscando mais 30 caracteres
                            posicaoInicio = posicaoAtual - 30;
                        }

                        //numero de caracteres que seram acrecentados na parte final da pesquisa
                        numeroCaracteresFim = totalCaractesres - (numeroCaracteresIncio + numeroCaracteresPesquisa);

                        posicaoFim = posicaoAtual + numeroCaracteresPesquisa;

                        //verifica se os numeros que serão acrescentado não pegam o fim da string pois caso seja maior irá acrescentar ao incio da pesquisa
                        if ((numeroCaracteresFim + posicaoAtual + numeroCaracteresPesquisa) > numeroCaracteresResumo)
                        {
                            //acrescenta ao inicio da pesquisa o total que faltou ao fim da pesquisa
                            int numerosAcrescentaIncio = (numeroCaracteresFim + posicaoAtual + numeroCaracteresPesquisa) - numeroCaracteresResumo;
                            //numero de caracteres para a ser o numero que chegue ao fim do resumo
                            numeroCaracteresFim = numeroCaracteresResumo - posicaoFim;
                            //valida o inicio da pesquisa novamente
                            if ((posicaoInicio - numerosAcrescentaIncio) > 0)
                            {
                                posicaoInicio = posicaoInicio - numerosAcrescentaIncio;
                                numeroCaracteresIncio += numerosAcrescentaIncio;
                            }
                            else
                            {
                                posicaoInicio = 0;
                                reticenciasIncio = string.Empty;
                            }

                            reticenciasFim = string.Empty;
                        }

                        primeiraParte = reticenciasIncio + trabalho.ResumoPortugues.Substring(posicaoInicio, numeroCaracteresIncio);
                        segundaParte = trabalho.ResumoPortugues.Substring(posicaoFim, numeroCaracteresFim) + reticenciasFim;

                        resumo = primeiraParte + tituloResumo + segundaParte;
                    }
                }
                catch (Exception ex)
                {
                    resumo = string.Empty;
                }

                trabalho.ResumoPortugues = resumo;
                retorno.Add(trabalho);
            }

            return retorno;
        }

        public TrabalhoAcademicoVO AlterarTrabalhoAcademico(long seq)
        {
            var include = IncludesTrabalhoAcademico.TipoTrabalho
                        | IncludesTrabalhoAcademico.Autores
                        | IncludesTrabalhoAcademico.DivisoesComponente
                        | IncludesTrabalhoAcademico.InstituicaoEnsino
                        | IncludesTrabalhoAcademico.NivelEnsino
                        | IncludesTrabalhoAcademico.DivisoesComponente_DivisaoComponente
                        | IncludesTrabalhoAcademico.DivisoesComponente_OrigemAvaliacao
                        | IncludesTrabalhoAcademico.PublicacaoBdp;

            var result = this.SearchByKey(new SMCSeqSpecification<TrabalhoAcademico>(seq), include).Transform<TrabalhoAcademicoVO>();

            var seqAluno = result.Autores.FirstOrDefault().SeqAluno;

            result.SeqTipoTrabalhoComparacao = result.SeqTipoTrabalho;
            result.ExisteAvaliacaoCadastrada = ExisteAvaliacao(seq);
            result.AlterarDataDepositoSecretaria = !(result.DataDepositoSecretaria.HasValue);
            result.ExistePublicacaoBdp = ExistePublicacaoBdpIdiomaTrabalhoAcademico(seq);

            result.DivisoesComponente.SMCForEach(d =>
            {
                d.DescricaoDivisaoComponente = DivisaoComponenteDomainService.SearchProjectionByKey(new SMCSeqSpecification<DivisaoComponente>(d.SeqDivisaoComponente), w => w.ConfiguracaoComponente.Descricao);
            });

            // Avaliar exibição e edição do campo duração autorização parcial (em dias)

            // Verifica se gera transação financeira
            var specInstituicaoNivelTipoTrabalho = new InstituicaoNivelTipoTrabalhoFilterSpecification()
            {
                SeqTipoTrabalho = result.SeqTipoTrabalho,
                SeqNivelEnsino = result.SeqNivelEnsino,
                SeqInstituicaoEnsino = result.SeqInstituicaoEnsino,
            };
            result.GeraFinanceiroEntregaTrabalho = InstituicaoNivelTipoTrabalhoDomainService.SearchProjectionByKey(specInstituicaoNivelTipoTrabalho, i => i.GeraFinanceiroEntregaTrabalho);

            // Verifica situação da publicação
            var situacaoTrabalho = SearchProjectionByKey(new SMCSeqSpecification<TrabalhoAcademico>(seq),
                x => x.PublicacaoBdp.FirstOrDefault().HistoricoSituacoes.OrderByDescending(o => o.DataInclusao).FirstOrDefault())?.SituacaoTrabalhoAcademico;
            var situacaoBloqueada = situacaoTrabalho.HasValue && (situacaoTrabalho == SituacaoTrabalhoAcademico.LiberadaBiblioteca || situacaoTrabalho == SituacaoTrabalhoAcademico.LiberadaConsulta);

            /*  NV07 e NV11
                Campo deve ser exibido apenas na alteração do trabalho acadêmico.
                Desabilitar o campo caso o trabalho esteja na situação "Liberada para biblioteca" ou 'Liberada para consulta"
             */
            result.ExibeDuracao = result.GeraFinanceiroEntregaTrabalho;
            result.HabilitaDuracao = !situacaoBloqueada;

            result.HabilitaPotencial = SMCSecurityHelper.Authorize(UC_ORT_002_02_02.PERMITE_ALTERAR_RESPOSTA_POTENCIAL_REGISTRO);

            var descricaoTipoTrabalho = InstituicaoNivelTipoTrabalhoDomainService.BuscarTiposTrabalhoSeq(result.SeqTipoTrabalho);

            result.ExibirPotencial = false;

            if (!string.IsNullOrEmpty(descricaoTipoTrabalho) && descricaoTipoTrabalho.ToUpper().Equals("TESE") || descricaoTipoTrabalho.ToUpper().Equals("DISSERTAÇÃO"))
            {
                result.ExibirPotencial = true;
            }
            return result;
        }

        #region [ Excluir Trabalho Acadêmico ]

        /// <summary>
        /// RN_ORT_016 - Excluir Trabalho Acadêmico
        /// Verificar se o trabalho acadêmico já está cadastrado no BDP.Caso positivo, emitir a mensagem de
        /// erro abaixo e abortar operação: "Exclusão não permitida. Este trabalho já está com suas informações cadastradas na Biblioteca Digital."
        /// Verificar se já existe avaliação cadastrada para o componente asociado ao trabalho acadêmico em questão.
        /// Caso positivo, emitir a mensagem de erro abaixo e abortar operação:
        /// "Exclusão não permitida. Este trabalho já possui avaliação cadastrada para seu componente curricular."
        /// Caso, nenhuma das situações anteriores ocorra, ao excluir um trabalho acadêmico, deverão também ser
        /// excluídos os alunos (autores) e os componentes curriculares associados a ele.
        /// </summary>
        /// <param name="seq">TrabalhoAcademico</param>
        public void ExcluirTrabalhoAcademico(long seq)
        {
            // Início da transação
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                ValidarExclusaoTrabalhoAcademico(seq);

                //Localizar o Trabalho Acadêmico
                var trabalhoAcademico = VerificarTrabalhoAcademicoExiste(seq);

                if (trabalhoAcademico.SeqSolicitacaoServico.HasValue)
                    throw new ExisteSolicitacaoTrabalhoAcademicoException();

                ExcluirComponentesTrabalhoAcademico(seq);

                ExcluirAutoresTrabalhoAcademico(seq);

                ///Limpar as coleções
                trabalhoAcademico.DivisoesComponente?.Clear();

                trabalhoAcademico.Autores?.Clear();

                //Exclusão do Trabalho Acadêmico
                DeleteEntity(trabalhoAcademico.Transform<TrabalhoAcademico>());

                //Confirma a transação
                unitOfWork.Commit();
            }
        }

        private TrabalhoAcademicoVO VerificarTrabalhoAcademicoExiste(long seq)
        {
            //Localizar o Trabalho Acadêmico
            var trabalhoAcademico = AlterarTrabalhoAcademico(seq);

            //Valida o Trabalho Acadêmico
            if (trabalhoAcademico == null || trabalhoAcademico.Seq <= 0) { throw new TrabalhoAcademicoNaoExisteException(); }

            return trabalhoAcademico;
        }

        private void ExcluirAutoresTrabalhoAcademico(long seq)
        {
            //Localizar os autoras
            List<TrabalhoAcademicoAutoria> autores = this.SearchProjectionBySpecification(new SMCSeqSpecification<TrabalhoAcademico>(seq), x => x.Autores).FirstOrDefault().ToList();

            //Exclusão de autores
            foreach (var autor in autores)
            {
                TrabalhoAcademicoAutoriaDomainService.DeleteEntity(autor);
            }
        }

        private void ExcluirComponentesTrabalhoAcademico(long seq)
        {
            //Localizar os componentes
            var componentes = this.SearchProjectionByKey(new SMCSeqSpecification<TrabalhoAcademico>(seq), x => x.DivisoesComponente.Select(t => new { t.Seq, t.SeqOrigemAvaliacao })).ToList();

            //Exclusão dos Componentes
            foreach (var componente in componentes)
            {
                TrabalhoAcademicoDivisaoComponenteDomainService.DeleteEntity(componente.Seq);

                OrigemAvaliacaoDomainService.DeleteEntity(componente.SeqOrigemAvaliacao);
            }
        }

        /// <summary>
        /// Verificar se o trabalho acadêmico já está cadastrado no BDP. Caso positivo, emitir a mensagem de
        /// erro abaixo e abortar operação: "Exclusão não permitida. Este trabalho já está com suas informações cadastradas na Biblioteca Digital."
        /// Verificar se já existe avaliação cadastrada para o componente asociado ao trabalho acadêmico em questão.
        /// Caso positivo, emitir a mensagem de erro abaixo e abortar operação:
        /// "Exclusão não permitida. Este trabalho já possui avaliação cadastrada para seu componente curricular."
        /// </summary>
        /// <param name="seq"></param>
        private void ValidarExclusaoTrabalhoAcademico(long seq)
        {
            if (seq <= 0) { throw new TrabalhoAcademicoNaoExisteException(); }

            // Localiza a publicação do Trabalho Acadêmico
            PublicacaoBdp publicacaoBdp = this.SearchProjectionBySpecification(new SMCSeqSpecification<TrabalhoAcademico>(seq), x => x.PublicacaoBdp.FirstOrDefault()).FirstOrDefault();

            //Valida a publicação
            if (publicacaoBdp != null && publicacaoBdp.SeqTrabalhoAcademico == seq) { throw new TrabalhoAcademicoComPublicacaoBdpException(); }

            //Localizar a avaliação
            AplicacaoAvaliacao avaliacao = this.SearchProjectionBySpecification(new SMCSeqSpecification<TrabalhoAcademico>(seq), x => x.DivisoesComponente.FirstOrDefault().OrigemAvaliacao.AplicacoesAvaliacao.FirstOrDefault()).FirstOrDefault();

            // Valida a Avaliação
            if (avaliacao != null && avaliacao.Seq > 0) { throw new TrabalhoAcademicoComAvaliacaoException(); }
        }

        #endregion [ Excluir Trabalho Acadêmico ]

        public long SalvarTrabalhoAcademico(TrabalhoAcademicoVO trabalhoAcademicoVO)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    // Somente um aluno deverá ser associado. 
                    // Caso nenhum ou mais de um seja informado, exibir mensagem de erro abaixo e abortar a operação: 
                    // "É necessário a associação de somente um aluno.".
                    if (trabalhoAcademicoVO.Autores.Count != 1)
                        throw new TrabalhoAcademicoQuantidadeInvalidaAlunosException();

                    //Em casos que possuia a solicitação de serviço preenchida, chegava o seq da solicitação NULL no VO, dessa forma, no transform abaixo era atualizado esse valor.
                    //Portanto, foi necessário realizar o acesso a banco para manter a informação
                    var trabalhoAcademicoBanco = this.SearchByKey(new SMCSeqSpecification<TrabalhoAcademico>(trabalhoAcademicoVO.Seq));
                    if (trabalhoAcademicoBanco != null && trabalhoAcademicoBanco.SeqSolicitacaoServico != null)
                        trabalhoAcademicoVO.SeqSolicitacaoServico = trabalhoAcademicoBanco.SeqSolicitacaoServico;
                    ;
                    var trabalhoAcademico = trabalhoAcademicoVO.Transform<TrabalhoAcademico>();


                    var seqAluno = trabalhoAcademico.Autores.FirstOrDefault().SeqAluno;

                    // Busca a configuração do tipo de trabalho por instituição e nível de ensino.
                    var configuracaoTrabalho = InstituicaoNivelTipoTrabalhoDomainService.SearchBySpecification(new InstituicaoNivelTipoTrabalhoFilterSpecification()
                    {
                        SeqTipoTrabalho = trabalhoAcademico.SeqTipoTrabalho,
                        SeqInstituicaoEnsino = trabalhoAcademico.SeqInstituicaoEnsino,
                        SeqNivelEnsino = trabalhoAcademico.SeqNivelEnsino
                    }).FirstOrDefault();

                    // Se o tipo de trabalho selecionado estiver configurado por instituição logada e nível de ensino selecionado, 
                    // para gerar transação financeira na entrega:
                    if (configuracaoTrabalho != null && configuracaoTrabalho.GeraFinanceiroEntregaTrabalho)
                    {
                        // Apenas na inclusão
                        if (trabalhoAcademico.Seq == 0)
                        {
                            if (!trabalhoAcademico.DataDepositoSecretaria.HasValue)
                                throw new TrabalhoAcademicoSemDataException();
                            else if (!ValidarDataMinimaDepositoSecretaria(trabalhoAcademico.DataDepositoSecretaria.Value))
                                throw new TrabalhoAcademicoDataInvalidaException();

                            trabalhoAcademico.SeqSolicitacaoServico = null;

                            /* Verificar se existe alguma solicitação de serviço para o aluno de Depósito de Dissertação/Tese que não esteja atendida.
                               Caso afirmativo, emitir a mensagem de erro abaixo e abortar a operação:
                               "Inclusão não permitida. Este aluno possui solicitação de Depósito de Dissertação/Tese ainda não atendida. Para que o trabalho seja incluído esta solicitação deverá ser atendida."
                               Obs.: Token do servico = DEPOSITO_DISSERTACAO_TESE*/
                            var possuiSolicitacaoDepositoDissertacao = SolicitacaoServicoDomainService.Count(new SolicitacaoServicoFilterSpecification
                            {
                                SeqPessoaAtuacao = trabalhoAcademico.Autores.FirstOrDefault().SeqAluno,
                                CategoriasSituacoes = new List<CategoriaSituacao> { CategoriaSituacao.EmAndamento, CategoriaSituacao.Nenhum, CategoriaSituacao.Novo },
                                TokensServico = new List<string> { TOKEN_SERVICO.DEPOSITO_DISSERTACAO_TESE }
                            }) > 0;
                            if (possuiSolicitacaoDepositoDissertacao)
                                throw new TrabalhoAcademicoInclusaoSolicitacaoNaoAtendida();

                            // Ao registrar a data de depósito, considerar a regra abaixo para o horário:
                            // Se for data retroativa, ou seja, em uma dia anterior ao atual: Registrar a hora = 23:59
                            // Se a data registrada for a atual: Registrar o horário atual, ou seja, ao registrar o trabalho.
                            var dataDepositoSecretaria = trabalhoAcademico.DataDepositoSecretaria.Value.Date;
                            if (dataDepositoSecretaria.Date < DateTime.Now.Date)
                                trabalhoAcademico.DataDepositoSecretaria = new DateTime(dataDepositoSecretaria.Year, dataDepositoSecretaria.Month, dataDepositoSecretaria.Day, 23, 59, 59);
                            else
                                trabalhoAcademico.DataDepositoSecretaria = new DateTime(dataDepositoSecretaria.Year, dataDepositoSecretaria.Month, dataDepositoSecretaria.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                            if (trabalhoAcademico.Autores.Any())
                            {
                                // Somente será possível um trabalho deste tipo para o aluno, logo se houver outro trabalho 
                                // com este tipo para o aluno associado, exibir a mensagem de erro e abortar a operação: 
                                // "Inclusão não permitida. Já existe um trabalho acadêmico deste tipo cadastrado para o aluno informado.".
                                var trabalhosAluno = SearchProjectionBySpecification(new TrabalhoAcademicoFilterSpecification()
                                {
                                    SeqAluno = trabalhoAcademico.Autores.FirstOrDefault().SeqAluno,
                                    SeqTipoTrabalho = trabalhoAcademico.SeqTipoTrabalho
                                }, p => p.Seq).ToList();
                                if (trabalhosAluno.Any(t => trabalhoAcademico.Seq != t))
                                    throw new TrabalhoAcademicoTipoDuplicadoException();

                                /* Verificar se o aluno está com a situação de matrícula igual a "Matriculado" ou "Matriculado em mobilidade" na data de depósito informada. Se não estiver, emitir a mensagem de erro:
                                 “ Inclusão não permitida. O aluno deve estar  "Matriculado" na data de depósito informada”.
                                */
                                var situacaoAtual = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAlunoNaData(trabalhoAcademico.Autores.FirstOrDefault().SeqAluno, trabalhoAcademico.DataDepositoSecretaria.Value);
                                if (situacaoAtual == null || (situacaoAtual.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.MATRICULADO &&
                                     situacaoAtual.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.MATRICULADO_MOBILIDADE))
                                    throw new SMCApplicationException("Inclusão não permitida. O aluno deve estar \"Matriculado\" na data de depósito informada.");

                                // Executar os passos descritos em RN_ORT_008 - Inclusão de Data De Depósito
                                IncluirDataDeposito(trabalhoAcademico.Autores.FirstOrDefault().SeqAluno, trabalhoAcademico.DataDepositoSecretaria.Value, null);
                            }

                            // UC_ORT_002_02_02 - Alteração do comando "Salvar" para gravar o campo "Duração autorização parcial em dias.
                            // Gravar no campo num_dias_duracao_autorizacao_parcial o numero de dias de duração da autorização parcial caso esteja parametrizada
                            // para a entidade tipo 'programa' associada ao curso do aluno.

                            // Busca configuracao dos dias para o programa do aluno informado
                            short? diasAutorizacao = BuscarDiasAutorizacaoParcial(seqAluno);

                            trabalhoAcademico.NumeroDiasDuracaoAutorizacaoParcial = diasAutorizacao;
                        }

                        // Alteração confirmada excluir os registros de autorização e incluir 
                        // uma nova situação no historico: 'Aguardando cadastro pelo aluno'
                        if (trabalhoAcademicoVO.LimparRegistrosAutorizacao)
                        {
                            var publicacaoBDP = this.SearchProjectionByKey(
                                new SMCSeqSpecification<TrabalhoAcademico>(trabalhoAcademico.Seq), x => x.PublicacaoBdp)
                                .FirstOrDefault();

                            // Exclui as autorizações realizadas pelo aluno
                            PublicacaoBdpAutorizacaoDomainService.ExcluirAutorizacoes(publicacaoBDP.Seq);

                            // Inclui na publicação BDP o histórico da publicação com a situação "AguardandoCadastroAluno"
                            PublicacaoBdpDomainService.AlterarSituacao(publicacaoBDP.Seq, SituacaoTrabalhoAcademico.AguardandoCadastroAluno);
                        }
                    }
                    else
                    {
                        //Apenas na inclusão
                        /*Verificar se o aluno tem vínculo ativo no ciclo letivo corrente (verificar pelo tipo de situação de matrícula). Se não estiver, emitir a mensagem de erro:
                         “Inclusão não permitida. O aluno não tem vínculo ativo no ciclo letivo corrente”.
                         Validar apenas na inclusão de um novo trabalho acadêmico.*/
                        if (trabalhoAcademico.Seq == 0)
                        {
                            var situacaoAtual = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(trabalhoAcademico.Autores.FirstOrDefault().SeqAluno);
                            if (!situacaoAtual.VinculoAlunoAtivo.GetValueOrDefault())
                                throw new SMCApplicationException("Inclusão não permitida. O aluno não tem vínculo ativo no ciclo letivo corrente");

                            trabalhoAcademico.SeqSolicitacaoServico = null;

                        }

                        trabalhoAcademico.DataDepositoSecretaria = null;
                    }

                    var seqsDivisoesComponente = trabalhoAcademico.DivisoesComponente.Select(d => d.SeqDivisaoComponente);

                    var specDivisaoComponente = new DivisaoComponenteFilterSpecification() { Seqs = seqsDivisoesComponente.ToArray() };

                    var divisoesComponentes = DivisaoComponenteDomainService.SearchProjectionBySpecification(specDivisaoComponente, d => new
                    {
                        d.Seq,
                        d.SeqConfiguracaoComponente
                    }).ToList();

                    foreach (var divisaoComponente in trabalhoAcademico.DivisoesComponente)
                    {
                        bool divisaoAlterada = false;
                        // Caso esteja editando, verifica se esta divisão já possui origem de avaliação
                        if (trabalhoAcademico.Seq != 0)
                        {
                            // Verifica se esta divisão já existia no trabalho
                            var dadosOrigem = this.SearchProjectionByKey(trabalhoAcademico.Seq, x => x.DivisoesComponente.Select(d => new
                            {
                                Seq = d.Seq,
                                SeqOrigemAvaliacao = (long?)d.SeqOrigemAvaliacao,
                                SeqDivisaoComponente = (long?)d.SeqDivisaoComponente
                            }).FirstOrDefault(d => d.Seq == divisaoComponente.Seq));

                            if (dadosOrigem != null)
                            {
                                if (dadosOrigem.SeqOrigemAvaliacao.HasValue)
                                    divisaoComponente.SeqOrigemAvaliacao = dadosOrigem.SeqOrigemAvaliacao.Value;

                                divisaoAlterada = dadosOrigem.SeqDivisaoComponente != divisaoComponente.SeqDivisaoComponente;
                            }
                        }

                        if (divisaoComponente.SeqOrigemAvaliacao == 0 || divisaoAlterada)
                        {
                            var seqConfiguracaoComponente = divisoesComponentes.FirstOrDefault(d => d.Seq == divisaoComponente.SeqDivisaoComponente).SeqConfiguracaoComponente;

                            var criterio = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seqAluno), a =>
                                a.Historicos.FirstOrDefault(h => h.Atual)
                                 .HistoricosCicloLetivo.OrderByDescending(o => o.SeqCicloLetivo).FirstOrDefault()
                                 .PlanosEstudo.FirstOrDefault(p => p.Atual)
                                 .MatrizCurricularOferta.MatrizCurricular.ConfiguracoesComponente.Select(t => new
                                 {
                                     SeqConfiguracaoComponente = t.SeqConfiguracaoComponente,
                                     SeqCriterioAprovacao = t.SeqCriterioAprovacao,
                                     ApurarFrequencia = (bool?)t.CriterioAprovacao.ApuracaoFrequencia,
                                     NotaMaxima = t.CriterioAprovacao.NotaMaxima,
                                     SeqEscalaApuracao = t.CriterioAprovacao.SeqEscalaApuracao
                                 }).FirstOrDefault(c => c.SeqConfiguracaoComponente == seqConfiguracaoComponente));

                            divisaoComponente.OrigemAvaliacao = new OrigemAvaliacao()
                            {
                                SeqCriterioAprovacao = criterio?.SeqCriterioAprovacao,
                                TipoOrigemAvaliacao = TipoOrigemAvaliacao.TrabalhoAcademico,
                                ApurarFrequencia = criterio?.ApurarFrequencia,
                                NotaMaxima = criterio?.NotaMaxima,
                                SeqEscalaApuracao = criterio?.SeqEscalaApuracao,
                                Seq = divisaoComponente.SeqOrigemAvaliacao != 0 ? divisaoComponente.SeqOrigemAvaliacao : 0
                            };
                        }
                    }

                    this.SaveEntity(trabalhoAcademico);

                    unitOfWork.Commit();

                    return trabalhoAcademico.Seq;
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }



        /// <summary>
        /// Busca os dias parametrizados para o programa do aluno informado
        /// </summary>
        /// <param name="seqAluno">Identificador do aluno</param>
        /// <returns>Retorna os dias parametrizados para o programa; retorna nulo se não houver</returns>
        private short? BuscarDiasAutorizacaoParcial(long seqAluno)
        {
            // Preparação para buscar dias de autorização
            var seqPrograma = ProgramaDomainService.BuscarProgramaPorAluno(seqAluno);

            var specPrograma = new ProgramaTipoAutorizacaoBdpFilterSpecification()
            {
                SeqPrograma = seqPrograma,
                TipoAutorizacao = TipoAutorizacao.Parcial
            };

            // Pega numeros de dias para autorização se houver
            return ProgramaTipoAutorizacaoBdpDomainService
                    .SearchProjectionByKey(specPrograma, p => p.NumeroDiasDuracaoAutorizacao);
        }

        /// <summary>
        /// RN_ORT_006 - Gerar nome formatado
        /// O Nome Formatado será gerado de acordo com as normas ABNT: "Sobrenome" + "," + "Prenome".
        /// Exceções:
        /// *	Sobrenome indicativo de parentesco acompanham o último sobrenome: "Sobrenome" + "Último Sobrenome" + ", " + "Prenome".
        /// *	Sobrenome composto, expressão composta: "Sobrenome composto" + "," + "Prenome".
        /// *	Sobrenome contendo partículas como "de", "da" e "e", a partícula é citada posteriormente ao prenome: "Sobrenome" + "," + "Prenome"(até à última partícula).
        /// </summary>
        /// <returns>Nome formatado</returns>
        public string FormatarNome(long seqAutor)
        {
            var nome = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seqAutor), x => x.DadosPessoais.Nome);
            return FormatarNome(nome);
        }

        /// <summary>
        /// RN_ORT_006 - Gerar nome formatado
        /// O Nome Formatado será gerado de acordo com as normas ABNT: "Sobrenome" + "," + "Prenome".
        /// Exceções:
        /// *	Sobrenome indicativo de parentesco acompanham o último sobrenome: "Sobrenome" + "Último Sobrenome" + ", " + "Prenome".
        /// *	Sobrenome composto, expressão composta: "Sobrenome composto" + "," + "Prenome".
        /// *	Sobrenome contendo partículas como "de", "da" e "e", a partícula é citada posteriormente ao prenome: "Sobrenome" + "," + "Prenome"(até à última partícula).
        /// </summary>
        /// <returns>Nome formatado</returns>
        public string FormatarNome(string nome)
        {
            return SMCABNTHelper.FormataNomeABNT(nome);
        }

        public VisualizarTrabalhoAcademicoVO VisualizarTrabalhoAcademico(long seq)
        {
            var trabalho = SearchProjectionByKey(new SMCSeqSpecification<TrabalhoAcademico>(seq),
                            x => new VisualizarTrabalhoAcademicoVO
                            {
                                TipoTrabalho = x.TipoTrabalho.Descricao,
                                NivelEnsino = x.NivelEnsino.Descricao,
                                Titulo = x.Titulo,
                                Autores = x.Autores.Select(f => f.NomeAutor + " (" + f.NomeAutorFormatado + ") " + (f.EmailAutor.Length > 0 ? "/ " + f.EmailAutor : "")).ToList(),
                                Programa = x.Autores.FirstOrDefault().Aluno.Historicos.FirstOrDefault(f => f.Atual && f.EntidadeVinculo.TipoEntidade.Token == TOKEN_TIPO_ENTIDADE.GRUPO_PROGRAMA).EntidadeVinculo.Nome,
                                AreaConhecimento = x.Autores.FirstOrDefault().Aluno.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade
                                                        .CursoOferta.Curso.Classificacoes.Where(f => f.Classificacao.HierarquiaClassificacao.TipoHierarquiaClassificacao.Token == TOKEN_TIPO_HIERARQUIA_CLASSIFICACAO.HIERARQUIA_CAPES)
                                                                                         .Select(f => f.Classificacao.Descricao).ToList(),

                                Orientadores = x.DivisoesComponente.SelectMany(f => f.OrigemAvaliacao.AplicacoesAvaliacao
                                        .OrderByDescending(o => o.Seq)
                                        .FirstOrDefault(w => w.Avaliacao.TipoAvaliacao == TipoAvaliacao.Banca && !w.DataCancelamento.HasValue)
                                        .MembrosBancaExaminadora.Where(w => w.TipoMembroBanca == TipoMembroBanca.Orientador)
                                        .Select(h => h.SeqColaborador.HasValue ? h.Colaborador.DadosPessoais.Nome : h.NomeColaborador)).ToList(),

                                Coorientadores = x.DivisoesComponente.SelectMany(f => f.OrigemAvaliacao.AplicacoesAvaliacao
                                        .OrderByDescending(o => o.Seq)
                                        .FirstOrDefault(w => w.Avaliacao.TipoAvaliacao == TipoAvaliacao.Banca && !w.DataCancelamento.HasValue)
                                        .MembrosBancaExaminadora.Where(w => w.TipoMembroBanca == TipoMembroBanca.Coorientador && w.Participou == true)
                                            .Select(h => h.SeqColaborador.HasValue ? h.Colaborador.DadosPessoais.Nome : h.NomeColaborador)).ToList(),

                                BancaExaminadora = x.DivisoesComponente.SelectMany(f => f.OrigemAvaliacao.AplicacoesAvaliacao
                                        .OrderByDescending(o => o.Seq)
                                        .FirstOrDefault(w => w.Avaliacao.TipoAvaliacao == TipoAvaliacao.Banca && !w.DataCancelamento.HasValue)
                                        .MembrosBancaExaminadora.Where(w => w.TipoMembroBanca != TipoMembroBanca.Orientador && w.TipoMembroBanca != TipoMembroBanca.Coorientador && w.Participou == true)
                                            .Select(h => new BancaExaminadoraVO()
                                            {
                                                Nome = h.SeqColaborador.HasValue ? h.Colaborador.DadosPessoais.Nome : h.NomeColaborador,
                                                Instituicao = h.SeqInstituicaoExterna.HasValue ? h.InstituicaoExterna.Nome : h.NomeInstituicaoExterna,
                                                ComplementoInstituicao = h.ComplementoInstituicao
                                            })).ToList(),

                                DataDefesa = x.DivisoesComponente.Select(f => f.OrigemAvaliacao.AplicacoesAvaliacao.OrderByDescending(o => o.Seq).FirstOrDefault
                                                                                (g => g.Avaliacao.TipoAvaliacao == TipoAvaliacao.Banca).DataInicioAplicacaoAvaliacao).FirstOrDefault(),

                                Local = x.DivisoesComponente.Select(f => f.OrigemAvaliacao.AplicacoesAvaliacao.OrderByDescending(o => o.Seq).FirstOrDefault
                                                                                (g => g.Avaliacao.TipoAvaliacao == TipoAvaliacao.Banca).Local).FirstOrDefault(),

                                // Busca todos os telefones do tipo comercial do curso-oferta-localidade do aluno
                                TelefonesComercial = x.Autores.FirstOrDefault().Aluno.Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Telefones.Where(t => t.TipoTelefone == TipoTelefone.Comercial).ToList(),

                                Situacao = x.PublicacaoBdp.FirstOrDefault().HistoricoSituacoes.OrderByDescending(o => o.DataInclusao).FirstOrDefault().SituacaoTrabalhoAcademico,
                                PossuiPublicacaoBDP = x.PublicacaoBdp.Any(),
                                SeqPublicacaoBdp = x.PublicacaoBdp.FirstOrDefault().Seq,
                                InformacoesEstrangeiras = x.PublicacaoBdp.FirstOrDefault().InformacoesIdioma.Select(g =>
                                                            new TrabalhoAcademicoIdiomasVO
                                                            {
                                                                IdiomaTrabalho = g.IdiomaTrabalho,
                                                                Idioma = g.Idioma,
                                                                PalavrasChave = g.PalavrasChave.Select(h => h.PalavraChave).ToList(),
                                                                Resumo = g.Resumo,
                                                                Titulo = g.Titulo
                                                            }).ToList(),
                                TituloIdiomaTrabalho = x.PublicacaoBdp.FirstOrDefault().InformacoesIdioma.FirstOrDefault(f => f.IdiomaTrabalho).Titulo,
                                Arquivos = x.PublicacaoBdp.FirstOrDefault().Arquivos.Select(a => new PublicacaoBdpArquivoVO()
                                {
                                    TipoAutorizacao = a.TipoAutorizacao,
                                    NomeArquivo = a.NomeArquivo,
                                    TamanhoArquivo = a.TamanhoArquivo,
                                    UrlArquivo = a.UrlArquivo,
                                }).ToList(),

                                // Busca o seqs serem processados posteriormente
                                SeqFormacaoEspecifica = x.Autores.FirstOrDefault().Aluno.Historicos.FirstOrDefault(f => f.Atual).Formacoes.FirstOrDefault(f => !f.DataFim.HasValue).SeqFormacaoEspecifica
                            });

            // Busca as informações da publicação BDP (autorização)
            if (trabalho.SeqPublicacaoBdp.HasValue)
            {
                var autorização = PublicacaoBdpAutorizacaoDomainService.BuscarDadosAutorizacaoAtiva(trabalho.SeqPublicacaoBdp.Value);
                if (autorização != null)
                    trabalho.TipoAutorizacaoBDP = autorização.TipoAutorizacao;
            }

            // Formata o telefone a ser apresentado
            // Buscar o telefone comercial sem categoria, se não encontrar buscar o comercial com categoria secretaria
            if (trabalho.TelefonesComercial.Count > 0)
            {
                trabalho.Telefones = new List<string>();
                var telSemCategoria = trabalho.TelefonesComercial.FirstOrDefault(t => !t.CategoriaTelefone.HasValue);
                if (telSemCategoria != null)
                    trabalho.Telefones.Add(string.Format("({0}){1}", telSemCategoria.CodigoArea, telSemCategoria.Numero));
                else
                {
                    var telSecretaria = trabalho.TelefonesComercial.FirstOrDefault(t => t.CategoriaTelefone == CategoriaTelefone.Secretaria);
                    if (telSecretaria != null)
                        trabalho.Telefones.Add(string.Format("({0}){1}", telSecretaria.CodigoArea, telSecretaria.Numero));
                }
            }

            // Se o trabalho não possuir publicação no bdp, troca a data da defesa por data prevista defesa
            if (!trabalho.PossuiPublicacaoBDP)
            {
                trabalho.DataPrevistaDefesa = trabalho.DataDefesa;
                trabalho.DataDefesa = null;
            }
            else
            {
                // Caso o aluno já tenha informado as informações do trabalho por idioma, altera o título do trabalho
                // para o título do idioma configurado para ser o do trabalho
                trabalho.Titulo = !string.IsNullOrEmpty(trabalho.TituloIdiomaTrabalho) ? trabalho.TituloIdiomaTrabalho : trabalho.Titulo;

                // Ordena os dados dos idiomas casos existam
                trabalho.InformacoesEstrangeiras = trabalho.InformacoesEstrangeiras.OrderByDescending(i => i.IdiomaTrabalho).ThenBy(i => i.Idioma.GetDescriptionPortuguese()).ToList();
            }

            // Busca as áreas de concentração do trabalho de acordo com a formação específica do aluno (autor)
            if (trabalho.SeqFormacaoEspecifica.HasValue)
            {
                trabalho.AreaConcentracao = FormacaoEspecificaDomainService.BuscarPrimeiroNivelFormacaoEspecifica(trabalho.SeqFormacaoEspecifica.Value).Descricao;
            }

            if (trabalho.Arquivos.Count() > 0)
            {
                trabalho.Arquivos = trabalho.Arquivos.Where(w => w.TipoAutorizacao == trabalho.TipoAutorizacaoBDP).ToList();
            }

            return trabalho;
        }

        public CabecalhoPublicacaoBdpVO BuscarCabecalhoPublicacaoBdp(long seq)
        {
            var retorno = SearchProjectionByKey(new SMCSeqSpecification<TrabalhoAcademico>(seq), x => new CabecalhoPublicacaoBdpVO
            {
                SeqTrabalhoAcademico = x.Seq,
                SeqPublicacaoBdp = x.PublicacaoBdp.FirstOrDefault().Seq,
                EntidadeResponsavel = x.Autores.FirstOrDefault().Aluno.Historicos.OrderByDescending(o => o.DataInclusao).FirstOrDefault().EntidadeVinculo.Nome,
                SeqEntidadeResponsvel = x.Autores.FirstOrDefault().Aluno.Historicos.OrderByDescending(o => o.DataInclusao).FirstOrDefault().SeqEntidadeVinculo,
                OfertaCursoLocalidadeTurno = x.Autores.FirstOrDefault().Aluno.Historicos.OrderByDescending(o => o.DataInclusao).FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome
                                                + " " +
                                                x.Autores.FirstOrDefault().Aluno.Historicos.OrderByDescending(o => o.DataInclusao).FirstOrDefault().CursoOfertaLocalidadeTurno.Turno.Descricao,
                AreaConhecimento = x.Autores.FirstOrDefault().Aluno.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade
                                                        .CursoOferta.Curso.Classificacoes.Where(f => f.Classificacao.HierarquiaClassificacao.TipoHierarquiaClassificacao.Token == TOKEN_TIPO_HIERARQUIA_CLASSIFICACAO.HIERARQUIA_CAPES)
                                                                                         .Select(f => f.Classificacao.Descricao).ToList(),

                Situacao = x.PublicacaoBdp.FirstOrDefault().HistoricoSituacoes.OrderByDescending(o => o.DataInclusao).FirstOrDefault().SituacaoTrabalhoAcademico,
                DataSituacao = x.PublicacaoBdp.FirstOrDefault().HistoricoSituacoes.OrderByDescending(o => o.DataInclusao).FirstOrDefault().DataInclusao,

                SeqFormacaoEspecifica = x.Autores.FirstOrDefault().Aluno.Historicos.FirstOrDefault(f => f.Atual).Formacoes.FirstOrDefault(f => !f.DataFim.HasValue).SeqFormacaoEspecifica
            });

            if (retorno.SeqFormacaoEspecifica.HasValue)
            {
                retorno.AreaConcentracao = FormacaoEspecificaDomainService.BuscarPrimeiroNivelFormacaoEspecifica(retorno.SeqFormacaoEspecifica.Value).Descricao;
            }

            //retorno.BloqueiaAlteracoes = (retorno.Situacao == SituacaoTrabalhoAcademico.AutorizadaLiberadaSecretaria && !SMCSecurityHelper.Authorize(UC_ORT_003_02_02.LIBERAR_PARA_BIBLIOTECA)) ||
            //                             (retorno.Situacao == SituacaoTrabalhoAcademico.LiberadaBiblioteca && !SMCSecurityHelper.Authorize(UC_ORT_003_02_02.LIBERAR_PARA_CONSULTA)) ||
            //                             (retorno.Situacao == SituacaoTrabalhoAcademico.LiberadaConsulta);

            retorno.BloqueiaAlteracoes = retorno.Situacao == SituacaoTrabalhoAcademico.LiberadaConsulta;

            return retorno;
        }

        #region Avaliacao Trabalho Acadêmico

        public AvaliacaoTrabalhoAcademicoCabecalhoVO BuscarTrabalhoAcademicoCabecalho(long seq)
        {
            var result = this.SearchProjectionByKey(new SMCSeqSpecification<TrabalhoAcademico>(seq),
                                        x => new AvaliacaoTrabalhoAcademicoCabecalhoVO
                                        {
                                            DescricaoTipoTrabalho = x.TipoTrabalho.Descricao,
                                            Titulo = x.Titulo,
                                            Autores = x.Autores.Select(a => new AutorVO
                                            {
                                                Nome = a.Aluno.DadosPessoais.Nome,
                                                DescricaoNivelEnsino = a.Aluno.Historicos.FirstOrDefault(s => s.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.Curso.NivelEnsino.Descricao,
                                                DescricaoCurso = a.Aluno.Historicos.FirstOrDefault(s => s.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.Nome
                                            }
                                            ).ToList()
                                        });

            return result;
        }
        public NotificacaoTrabalhoAcademicoVO BuscarTrabalhoAcademicoModeloNotificacao(long seq)
        {
            var result = this.SearchProjectionByKey(new SMCSeqSpecification<TrabalhoAcademico>(seq),
                                        x => new NotificacaoTrabalhoAcademicoVO
                                        {
                                            DescricaoTipoTrabalho = x.TipoTrabalho.Descricao,
                                            Titulo = x.Titulo,
                                            SeqEntidade = x.SeqInstituicaoEnsino,
                                            PotencialNegocio = x.PotencialNegocio,
                                            PotencialPatente = x.PotencialPatente,
                                            PotencialRegistroSoftware = x.PotencialPatente,
                                            Autores = x.Autores.Select(a => new NotificacaoAutoresVO
                                            {
                                                Nome = a.Aluno.DadosPessoais.Nome,
                                                DescricaoEntidadeResponsavel = a.Aluno.Historicos.FirstOrDefault(f => f.Atual).EntidadeVinculo.Nome,
                                                ListaDescricaoFormacaoEspecifica = a.Aluno.Historicos.FirstOrDefault(f => f.Atual).Formacoes.Where(w => w.FormacaoEspecifica.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_CONCENTRACAO).Select(e => e.FormacaoEspecifica.Descricao.Trim()).ToList()
                                            }
                                            ).ToList()
                                        });
            result.NomeAutor = string.Join(", ", result.Autores.Select(s => s.Nome));
            result.Autores.SMCForEach(f => f.DescricaoFormacaoEspecifica = f.ListaDescricaoFormacaoEspecifica.Any() ? string.Join(", ", f.ListaDescricaoFormacaoEspecifica) : "-");

            return result;
        }

        public SMCPagerData<AvaliacaoTrabalhoAcademicoListaVO> BuscarAvaliacoesTrabalhoAcademico(long seq)
        {
            var spec = new SMCSeqSpecification<TrabalhoAcademico>(seq);

            var result = this.SearchProjectionBySpecification(spec,
                                         x => x.DivisoesComponente.Select(z => new AvaliacaoTrabalhoAcademicoListaVO
                                         {
                                             Seq = x.Seq,
                                             DataAutorizacaoSegundoDeposito = x.DataAutorizacaoSegundoDeposito,
                                             SeqNivelEnsino = x.SeqNivelEnsino,
                                             SeqConfiguracaoComponente = z.DivisaoComponente.SeqConfiguracaoComponente,
                                             DescricaoComponenteCurricular = z.DivisaoComponente.ConfiguracaoComponente.Descricao,
                                             SeqComponenteCurricular = z.DivisaoComponente.ConfiguracaoComponente.SeqComponenteCurricular,
                                             SituacaoHistoricoEscolar = z.OrigemAvaliacao.HistoricosEscolares.FirstOrDefault().SituacaoHistoricoEscolar,
                                             SeqOrigemAvaliacao = z.OrigemAvaliacao.Seq,
                                             SeqCriterioAprovacao = z.OrigemAvaliacao.SeqCriterioAprovacao,
                                             SeqsAutores = x.Autores.Select(a => a.SeqAluno).ToList(),
                                             Avaliacoes = z.OrigemAvaliacao.AplicacoesAvaliacao.Select(
                                                    a => new AvaliacaoTrabalhoAcademicoAvaliacaoVO
                                                    {
                                                        Seq = a.Seq,
                                                        SeqOrigemAvaliacao = a.SeqOrigemAvaliacao,
                                                        SeqTrabalhoAcademico = a.OrigemAvaliacao.DivisoesComponente.Select(conf => conf.SeqTrabalhoAcademico).FirstOrDefault(),
                                                        DataInicioAplicacaoAvaliacao = a.DataInicioAplicacaoAvaliacao,
                                                        SeqArquivoAnexadoAtaDefesa = a.ApuracoesAvaliacao.FirstOrDefault().SeqArquivoAnexadoAtaDefesa,
                                                        GuidArquivoAnexadoAtaDefesa = a.ApuracoesAvaliacao.FirstOrDefault().SeqArquivoAnexadoAtaDefesa.HasValue ? a.ApuracoesAvaliacao.FirstOrDefault().ArquivoAnexadoAtaDefesa.UidArquivo.ToString() : string.Empty,
                                                        Hora = a.DataInicioAplicacaoAvaliacao,
                                                        Local = a.Local,
                                                        DataCancelamento = a.DataCancelamento,
                                                        TipoAvaliacao = a.Avaliacao.TipoAvaliacao,
                                                        NotaConceito = (a.ApuracoesAvaliacao.FirstOrDefault().Nota.HasValue) ? a.ApuracoesAvaliacao.FirstOrDefault().Nota.ToString() : a.ApuracoesAvaliacao.FirstOrDefault().EscalaApuracaoItem.Descricao,
                                                        Nota = (short?)a.ApuracoesAvaliacao.FirstOrDefault().Nota,
                                                        SeqEscalaApuracaoItem = a.ApuracoesAvaliacao.FirstOrDefault().SeqEscalaApuracaoItem,
                                                        SituacaoHistoricoEscolar = a.OrigemAvaliacao.HistoricosEscolares.Any() ? a.OrigemAvaliacao.HistoricosEscolares.FirstOrDefault().SituacaoHistoricoEscolar : SituacaoHistoricoEscolar.Nenhum,
                                                        MembrosBancaExaminadora = a.MembrosBancaExaminadora.Select(m =>
                                                                           new MembroBancaExaminadoraVO
                                                                           {
                                                                               Seq = m.Seq,
                                                                               NomeInstituicaoExterna = m.NomeInstituicaoExterna,
                                                                               Instituicao = m.InstituicaoExterna.Nome,
                                                                               TipoMembroBanca = m.TipoMembroBanca,
                                                                               Nome = m.Colaborador.DadosPessoais.Nome,
                                                                               NomeColaborador = m.NomeColaborador,
                                                                               Participou = m.Participou,
                                                                               SeqColaborador = m.SeqColaborador,
                                                                               SeqInstituicaoExterna = m.SeqInstituicaoExterna,
                                                                               SeqAplicacaoAvaliacao = m.SeqAplicacaoAvaliacao,
                                                                               ComplementoInstituicao = m.ComplementoInstituicao
                                                                           }
                                                                    ).ToList()
                                                    }).OrderBy(o => o.DataInicioAplicacaoAvaliacao).ToList()
                                         }), out int total).FirstOrDefault().ToList();

            ValidarMembroBancaExaminadora(result);

            bool publicacaoBibliotecaObrigatoria = TipoTrabalhoPublicacaoBibliotecaObrigatoria(seq);

            if (result != null && result.Any())
            {
                foreach (var item in result)
                {
                    item.PublicacaoBiblioteca = publicacaoBibliotecaObrigatoria;

                    bool alunoFormado = false;
                    // Verifica algum dos alunos está formado
                    if (item.SeqsAutores != null && item.SeqsAutores.Any())
                    {
                        foreach (var seqAluno in item.SeqsAutores)
                        {
                            var situacaoAluno = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(seqAluno);
                            if (situacaoAluno?.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.FORMADO)
                            {
                                alunoFormado = true;
                                break;
                            }
                        }
                    }

                    if (item.Avaliacoes != null && item.Avaliacoes.Any())
                    {
                        foreach (var avaliacao in item.Avaliacoes)
                            avaliacao.AlunoFormado = alunoFormado;
                    }
                }
            }

            return new SMCPagerData<AvaliacaoTrabalhoAcademicoListaVO>(result, total);
        }

        /// <summary>
        /// NV06 - Se avaliação não tiver sido apurada, ou seja, sem nota ou conceito lançados:
        /// * Listar os membros da banca examinadora associados a avaliação em questão.
        /// Caso contrário:
        /// * Listar os membros da banca examinadora associados a avaliação em questão, que tiveram participação confirmada*/
        /// </summary>
        /// <param name="componentes"></param>
        private void ValidarMembroBancaExaminadora(List<AvaliacaoTrabalhoAcademicoListaVO> componentes)
        {
            foreach (var componente in componentes)
            {
                var dadosCriterio = CriterioAprovacaoDomainService.SearchByKey(componente.SeqCriterioAprovacao.Value);

                foreach (var avaliacao in componente.Avaliacoes)
                {
                    //avaliacao.SituacaoHistoricoEscolar = HistoricoEscolarDomainService.CalcularSituacaoFinal(new HistoricoEscolarSituacaoFinalVO
                    //{
                    //    IndicadorApuracaoFrequencia = dadosCriterio.ApuracaoFrequencia,
                    //    IndicadorApuracaoNota = dadosCriterio.ApuracaoNota,
                    //    SeqEscalaApuracao = dadosCriterio.SeqEscalaApuracao,
                    //    PercentualMinimoNota = dadosCriterio.PercentualNotaAprovado,
                    //    PercentualMinimoFrequencia = dadosCriterio.PercentualFrequenciaAprovado,
                    //    NotaMaxima = dadosCriterio.NotaMaxima,
                    //    TipoArredondamento = dadosCriterio.TipoArredondamento,
                    //    Nota = avaliacao.Nota,
                    //    SeqEscalaApuracaoItem = avaliacao.SeqEscalaApuracaoItem,
                    //});

                    avaliacao.MembrosBancaExaminadora = string.IsNullOrEmpty(avaliacao.NotaConceito)
                                        ? avaliacao?.MembrosBancaExaminadora
                                        : avaliacao?.MembrosBancaExaminadora?.Where(m => m.Participou == true).ToList();

                    //Monta a descrição do Membro da banca
                    avaliacao.MembrosBancaExaminadora.ForEach(m => MontarDescricaoMembroBancaExaminadora(m));

                    //Ordena a lista de colaboradores pelo nome do membro da banca examinadora
                    avaliacao.MembrosBancaExaminadora = avaliacao?.MembrosBancaExaminadora.OrderBy(m => m.DescricaoMembro).ToList();
                }
            }
        }

        /// <summary>
        /// Monta a descrição do Membro da banca examinadora, conforme regras
        /// </summary>
        /// <param name="membro"></param>
        /// <returns></returns>
        private void MontarDescricaoMembroBancaExaminadora(MembroBancaExaminadoraVO membro)
        {
            string nomeInstituicao = membro.Instituicao ?? membro.NomeInstituicaoExterna;
            string nomeColaborador = membro.Nome ?? membro.NomeColaborador;
            string complementoInstituicao = !string.IsNullOrEmpty(membro.ComplementoInstituicao) ? $" - {membro.ComplementoInstituicao}" : string.Empty;

            string tipoMembro = string.Empty;
            string instituicao = string.Empty;
            if (membro.TipoMembroBanca == TipoMembroBanca.Coorientador || membro.TipoMembroBanca == TipoMembroBanca.Orientador)
                tipoMembro = string.IsNullOrEmpty(membro.TipoMembroBanca.SMCGetDescription()) ? "" : $" ({membro.TipoMembroBanca.SMCGetDescription()})";

            instituicao = string.IsNullOrEmpty(nomeInstituicao) ? "" : $" ({nomeInstituicao}{complementoInstituicao})";

            membro.DescricaoMembro = $"{nomeColaborador}{tipoMembro}{instituicao}";
        }

        /// <summary>
        /// Busca o comprovante de entrega para cada autoria do trabalho academico
        /// </summary>
        /// UC_ORT_002_02_06 - Emitir de Comprovante de Entrega
        ///
        /// <param name="seq"></param>
        /// <returns></returns>
        public List<ComprovanteEntregaTrabalhoAcademicoVO> BuscarComprovantesEntregaTrabalhoAcademico(long seq)
        {
            return RawQuery<ComprovanteEntregaTrabalhoAcademicoVO>(string.Format(_buscaComprovanteEntregaTrabalhoAcademico, seq));
        }

        /// <summary>
        /// Busca o relatório de comprovante de entrega para cada autoria do trabalho academico
        /// </summary>
        /// UC_ORT_002_02_06 - Emitir de Comprovante de Entrega
        ///
        /// <param name="seq"></param>
        /// <returns></returns>
        public byte[] BuscarRelatorioEntregaTrabalhoAcademico(long seq)
        {
            var comprovantes = RawQuery<ComprovanteEntregaTrabalhoAcademicoVO>(string.Format(_buscaComprovanteEntregaTrabalhoAcademico, seq));
            var nomeUsuarioLogado = SMCContext.User.SMCGetNome();

            if (comprovantes.Any())
            {
                // Recupera o template do relatório
                var template = InstituicaoNivelModeloRelatorioDomainService.BuscarTemplateModeloRelatorio(comprovantes.FirstOrDefault().SeqInstituicaoNivel, ModeloRelatorio.ComprovanteEntregaTrabalhoAcademico);

                if (template == null)
                {
                    //Lançar exceção de template
                    throw new TemplateComprovanteNaoEncontradoException();
                }

                // Chama o serviço para gerar o relatório
                List<Dictionary<string, object>> modelDic = new List<Dictionary<string, object>>();
                var modelo = comprovantes.First();
                modelo.NomeUsuarioLogado = nomeUsuarioLogado;
                modelDic.Add(SMCReflectionHelper.ToDictionary(modelo));

                var bytesArquivo = SMCReportMergeService.MailMergeToPdf(template.ArquivoModelo.Conteudo, modelDic.ToArray());
            }
            return null;
        }

        #endregion Avaliacao Trabalho Acadêmico

        public List<TrabalhosVO> BuscarTrabalhosAcademicosAluno(long seqPessoaAtuacao)
        {
            TrabalhoAcademicoAlunoSpecification spec = new TrabalhoAcademicoAlunoSpecification()
            {
                SeqAluno = seqPessoaAtuacao,
                BancaCancelada = false,
            };

            var trabalhos = SearchProjectionBySpecification(spec,
                        x => new TrabalhosVO
                        {
                            SeqTrabalhoAcademico = x.Seq,
                            SeqConfiguracaoComponente = x.DivisoesComponente.Select(s => s.DivisaoComponente.ConfiguracaoComponente).FirstOrDefault().Seq,
                            Titulo_Componente = x.DivisoesComponente.Select(s => s.DivisaoComponente.ConfiguracaoComponente).FirstOrDefault().Descricao,
                            TituloTrabalhoAcademico = x.Titulo.Trim(),
                            TituloIdiomaTrabalho = x.PublicacaoBdp.Select(p => p.InformacoesIdioma.FirstOrDefault(i => i.IdiomaTrabalho).Titulo).FirstOrDefault(),
                            Avaliacoes = x.DivisoesComponente.Select(s => s.OrigemAvaliacao.AplicacoesAvaliacao.Where(w => !w.DataCancelamento.HasValue).OrderByDescending(a => a.DataInicioAplicacaoAvaliacao).Select(
                                    a => new AvaliacaoTrabalhoVO
                                    {
                                        SeqOrigemAvaliacao = s.SeqOrigemAvaliacao,
                                        SeqTrabalhoAcademico = x.Seq,
                                        SeqAplicacaoAvaliacao = a.Seq,
                                        Data = a.DataInicioAplicacaoAvaliacao,
                                        Aprovado = a.ApuracoesAvaliacao.FirstOrDefault().EscalaApuracaoItem.Aprovado,
                                        Resultado = (a.ApuracoesAvaliacao.FirstOrDefault().Nota.HasValue) ? a.ApuracoesAvaliacao.FirstOrDefault().Nota.ToString() : a.ApuracoesAvaliacao.FirstOrDefault().EscalaApuracaoItem.Descricao,
                                    }).FirstOrDefault()
                            ).ToList()
                        }).ToList();

            foreach (var trabalho in trabalhos)
            {
                var avaliacaoRecente = trabalho.Avaliacoes.FirstOrDefault();
                avaliacaoRecente.ComissaoExaminadora = MembroBancaExaminadoraDomainService.BuscarMembrosBancaExaminadora(avaliacaoRecente.SeqAplicacaoAvaliacao);
                trabalho.Seq = seqPessoaAtuacao;
                trabalho.Avaliacoes.RemoveAll(a => a == null); //Vai que tem algum nulo aí? Nunca se sabe...

                foreach (var avaliacao in trabalho.Avaliacoes)
                {
                    if (!avaliacao.Aprovado.HasValue)
                        avaliacao.Aprovado = HistoricoEscolarDomainService.TrabalhoAcademicoDoAlunoAprovado(avaliacao.SeqOrigemAvaliacao);

                    avaliacao.SeqConfiguracaoComponente = trabalho.SeqConfiguracaoComponente;
                    avaliacao.EsconderComissao = (avaliacao.ComissaoExaminadora == null || avaliacao.ComissaoExaminadora.Count <= 0);
                }
            }

            trabalhos = trabalhos.Where(s => s.Avaliacoes.Any(a => !a.Aprovado.HasValue || a.Aprovado.HasValue && a.Aprovado.Value)).OrderBy(t => t.Avaliacoes.OrderBy(a => a.Data).FirstOrDefault().Data).ToList();

            return trabalhos;
        }

        public bool TipoTrabalhoPublicacaoBibliotecaObrigatoria(long seqTrabalhoAcademico)
        {
            var dadosTrabalho = this.SearchProjectionByKey(seqTrabalhoAcademico, x => new
            {
                x.SeqTipoTrabalho,
                x.SeqNivelEnsino,
                x.SeqInstituicaoEnsino
            });

            var configuracaoTrabalho = InstituicaoNivelTipoTrabalhoDomainService
                .SearchBySpecification(new InstituicaoNivelTipoTrabalhoFilterSpecification()
                {
                    SeqTipoTrabalho = dadosTrabalho.SeqTipoTrabalho,
                    SeqNivelEnsino = dadosTrabalho.SeqNivelEnsino,
                    SeqInstituicaoEnsino = dadosTrabalho.SeqInstituicaoEnsino
                }).FirstOrDefault();

            var result = configuracaoTrabalho != null && configuracaoTrabalho.PublicacaoBibliotecaObrigatoria;

            return result;
        }

        public long BuscarSeqAlunoTrabalhoAcademico(long seqTrabalhoAcademico)
        {
            var seqAluno = this.SearchProjectionByKey(new SMCSeqSpecification<TrabalhoAcademico>(seqTrabalhoAcademico),

                         x => x.Autores.FirstOrDefault().SeqAluno);

            return seqAluno;
        }

        public DateTime? DataPublicacaoBdpTrabalhoAcademico(long seqTrabalhoAcademico)
        {
            //Verifico se existe uma publicação para o trabalho em questão
            var PublicacaoBDP = this.SearchProjectionByKey(
                new SMCSeqSpecification<TrabalhoAcademico>(seqTrabalhoAcademico), x => x.PublicacaoBdp).FirstOrDefault();

            return PublicacaoBDP?.DataPublicacao;
        }

        public SituacaoAlunoFormacao? BuscarSituacaoAlunoFormacaoHistorico(long seqTrabalhoAcademico)
        {
            long seqAluno = BuscarSeqAlunoTrabalhoAcademico(seqTrabalhoAcademico);

            var alunoHistorico = AlunoHistoricoDomainService.BuscarHistoricoAtualAluno(seqAluno, Common.Areas.ALN.Includes.IncludesAlunoHistorico.Formacoes_ApuracoesFormacao);

            return alunoHistorico?.Formacoes?.FirstOrDefault(f => !f.DataFim.HasValue)?.ApuracoesFormacao?.OrderByDescending(a => a.DataInicio).FirstOrDefault(a => a.DataInicio <= DateTime.Now)?.SituacaoAlunoFormacao;
        }

        /// <summary>
        /// Método que verifica se existe alguma avaliação, cadastrada para o trabalho acadêmico
        /// </summary>
        /// <param name="seqTrabalhoAcademico"></param>
        /// <returns>true, false</returns>
        public bool ExisteAvaliacao(long seqTrabalhoAcademico)
        {
            var result = this.SearchProjectionByKey(new SMCSeqSpecification<TrabalhoAcademico>(seqTrabalhoAcademico),
                                         x => x.DivisoesComponente.Any(z => z.OrigemAvaliacao.AplicacoesAvaliacao.Any()));

            return result;
        }

        /// <summary>
        /// Verifica a data de deposito de um trabalho para abertura/deferimento de solicitação de prorrogação
        /// de prazo de conclusão
        /// </summary>
        /// <param name="seqAluno">Sequencial da pessoa atuação (aluno)</param>
        /// <returns>Dados do trabalho/deposito do aluno/ data defesa / situacao historico escolar</returns>
        public (string TituloTrabalho, DateTime? DataDeposito, DateTime? DataDefesa, SituacaoHistoricoEscolar? Situacao) BuscarDatasDepositoDefesaTrabalho(long seqAluno)
        {
            // Busca os dados da pessoa atuação
            var dadosAluno = AlunoDomainService.BuscarInstituicaoNivelEnsinoESequenciaisPorAluno(seqAluno);

            // Busca o tipo de trabalho que gera pendencia financeira para o aluno
            var specTipoTrabalho = new InstituicaoNivelTipoTrabalhoFilterSpecification()
            {
                SeqInstituicaoEnsino = dadosAluno.SeqInstituicao,
                SeqNivelEnsino = dadosAluno.SeqNivelEnsino,
                GeraFinanceiroEntregaTrabalho = true
            };
            var tipoTrabalho = InstituicaoNivelTipoTrabalhoDomainService.SearchByKey(specTipoTrabalho);

            // Se encontrou trabalho com pendencia financeira...
            if (tipoTrabalho != null)
            {
                // Busca o trabalho do aluno do tipo que gera pendencia
                var specTrabalho = new TrabalhoAcademicoAlunoSpecification()
                {
                    SeqTipoTrabalho = tipoTrabalho.SeqTipoTrabalho,
                    SeqAluno = seqAluno
                };
                var trabalho = this.SearchProjectionByKey(specTrabalho, x => new
                {
                    Titulo = x.Titulo,
                    DataDepositoSecretaria = x.DataDepositoSecretaria,
                    Avaliacao = x.DivisoesComponente.FirstOrDefault().OrigemAvaliacao.AplicacoesAvaliacao.OrderByDescending(o => o.DataInclusao).FirstOrDefault(),
                    SituacaoHistoricoEscolar = x.DivisoesComponente.Select(oa => oa.OrigemAvaliacao.HistoricosEscolares.FirstOrDefault().SituacaoHistoricoEscolar).FirstOrDefault()
                });
                if (trabalho != null)
                {
                    return (trabalho.Titulo,
                            trabalho.DataDepositoSecretaria,
                            (trabalho.Avaliacao != null && !trabalho.Avaliacao.DataCancelamento.HasValue) ? (DateTime?)trabalho.Avaliacao.DataInicioAplicacaoAvaliacao : null,
                            trabalho.SituacaoHistoricoEscolar);
                }
                else
                    return (null, null, null, null);
            }
            else
            {
                return (null, null, null, null);
            }
        }

        public bool ValidarDataMinimaDepositoSecretaria(DateTime dataDepositoSecretaria)
        {
            // A data de depósito necessariamente deverá ser igual a data atual ou
            // maior/igual que a data referente ao sexto dia útil do mês corrente.

            var dataDeposito = dataDepositoSecretaria.Date;
            var dataAtual = DateTime.Now.Date;

            if (dataDeposito == dataAtual)
                return true;

            var primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date;
            var sextoDiaUtil = SMCDateTimeHelper.AddBusinessDays(primeiroDiaMes, 6, null).Date;

            if (dataDeposito >= sextoDiaUtil)
                return true;

            return false;
        }

        public bool ExistePublicacaoBdpIdiomaTrabalhoAcademico(long seqTrabalhoAcademico)
        {
            return PublicacaoBdpIdiomaDomainService.Count(new PublicacaoBdpIdiomaFilterSpecification() { SeqTrabalhoAcademico = seqTrabalhoAcademico }) > 0;
        }

        /// <summary>
        /// Recupera os dados do Tipo de trabalho acadêmico para um determinado aluno
        /// </summary>
        public (long SeqTipoTrabalho, long? SeqDivisaoComponente) RecuperarDadosTipoTrabalhoAcademico(long seqAluno)
        {
            // Recupera os dados da pessoa atuação
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno);

            // Recupera o seq tipo de trabalho
            var seqTipoTrabalho = InstituicaoNivelTipoTrabalhoDomainService.SearchProjectionByKey(new InstituicaoNivelTipoTrabalhoFilterSpecification
            {
                SeqInstituicaoEnsino = dadosOrigem.SeqInstituicaoEnsino,
                SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
                GeraFinanceiroEntregaTrabalho = true
            }, x => x.SeqTipoTrabalho);

            /*  3. Selecionar a divisão de configuração de componente (seq_divisao_componente):
                    * Cujo a divisão esteja associada a configuração de componente da matriz curricular do plano de estudo atual do aluno.
                    * E também o tipo da divisão está configurado por “instituição/nível/tipo do componente” e esteja nesta configuração associado ao tipo de trabalho selecionado no passo 1. (cur.instituicao_nivel_tipo_divisao_componente).*/

            // Este método busca os tipos de trabalho disponíveis. Segundo Jussara (01.06), somente exibirá UM, mesmo sendo uma lista.
            // Recomendou pegar desde método o primeiro que retornar.
            var seqDivisaoComponente = DivisaoMatrizCurricularComponenteDomainService.BuscarDivisaoComponenteCurricularSelect(new CUR.ValueObjects.DivisaoComponenteCurricularFiltroVO
            {
                SeqAluno = seqAluno,
                SeqTipoTrabalho = seqTipoTrabalho,
                SeqInstituicaoEnsino = dadosOrigem.SeqInstituicaoEnsino,
                SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
            })?.FirstOrDefault()?.Seq;

            return (seqTipoTrabalho, seqDivisaoComponente);
        }


        /// <summary>
        /// Implementa a RN_ORT_008 - Inclusão de Data De Depósito
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação (aluno) que está incluindo a data de deposito</param>
        /// <param name="dataDeposito">Data de deposito a ser realizada</param>
        /// <param name="seqSolicitacaoServico">Caso o deposito seja por solicitação, informar o sequencial da mesma.</param>
        public void IncluirDataDeposito(long seqPessoaAtuacao, DateTime dataDeposito, long? seqSolicitacaoServico)
        {
            // Busca os dados de origem da pessoa atuação
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

            /*  1. Executar o processo descrito na regra RN_ORT_007 - Estorna Lançamento GRA*/
            // RN_ORT_007
            /*  Executar a procedure st_estorna_lancamento_penalidade do SGA. Que tem como parâmetros:
                @cod_aluno INT = Código de aluno de migração (ALN.aluno)
                , @dat_deposito SMALLDATETIME = Data de depósito informada
                , @nom_usuario_operacao VARCHAR(60) = Usuário Logado
                , @dsc_erro VARCHAR(200) OUTPUT
            */
            IntegracaoAcademicoService.EstornaLancamentoPenalidade(new EstornaLancamentoPenalidadeData
            {
                CodigoAluno = (int)dadosOrigem.CodigoAlunoMigracao.Value,
                DataDeposito = dataDeposito,
                Usuario = SMCContext.User.Identity.Name
            });

            /* 2. Se existir o bloqueio “PRAZO_CONCLUSÃO_CURSO_ENCERRADO” com a situação de ‘bloqueado’, desbloqueá-lo conforme 
                valores abaixo:
                - dat_desbloqueio_efetivo = dat_deposito
                - usu_alteracao = usuário logado
                - dat_alteracao = GETDATE()
                - idt_dom_tipo_desbloqueio = 2 (Efetivo)
                - idt_dom_situacao_bloqueio = 2 (Desbloqueado)
                - dsc_justificativa_desbloqueio = 'Desbloqueado automaticamente pelo depósito da tese/dissertação'
                - usu_desbloqueio_efetivo = usuário logado
            */
            var bloqueioPrazoConclusao = PessoaAtuacaoBloqueioDomainService.SearchByKey(new PessoaAtuacaoBloqueioFilterSpecification
            {
                SeqPessoaAtuacao = seqPessoaAtuacao,
                TokenMotivoBloqueio = TOKEN_MOTIVO_BLOQUEIO.PRAZO_CONCLUSAO_CURSO_ENCERRADO,
                SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                DataBloqueioMenorOuIgualA = DateTime.Now
            });
            if (bloqueioPrazoConclusao != null)
            {
                bloqueioPrazoConclusao.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                bloqueioPrazoConclusao.DataDesbloqueioEfetivo = dataDeposito;
                bloqueioPrazoConclusao.TipoDesbloqueio = TipoDesbloqueio.Efetivo;
                bloqueioPrazoConclusao.JustificativaDesbloqueio = "Desbloqueado automaticamente pelo depósito da tese/dissertação";
                bloqueioPrazoConclusao.UsuarioDesbloqueioEfetivo = SMCContext.User.Identity.Name;

                PessoaAtuacaoBloqueioDomainService.UpdateFields(bloqueioPrazoConclusao, x => x.SituacaoBloqueio,
                                                                                        x => x.DataDesbloqueioEfetivo,
                                                                                        x => x.TipoDesbloqueio,
                                                                                        x => x.JustificativaDesbloqueio,
                                                                                        x => x.UsuarioDesbloqueioEfetivo);
            }

            /* 3. Se existir o bloqueio “IMPEDIMENTO_RENOVACAO_PRAZO_CONCLUSAO” com a situação de
                ‘bloqueado’, desbloqueá-lo conforme valores abaixo:
                - dat_desbloqueio_efetivo = dat_deposito
                - usu_alteracao = usuário logado
                - dat_alteracao = GETDATE() 
                - idt_dom_tipo_desbloqueio = 2 (Efetivo)
                - idt_dom_situacao_bloqueio = 2 (Desbloqueado)
                - dsc_justificativa_desbloqueio = 'Desbloqueado automaticamente pelo depósito da tese/dissertação'
                - usu_desbloqueio_efetivo = usuário logado
            */
            var bloqueioRenovacaoPrazoConclusao = PessoaAtuacaoBloqueioDomainService.SearchByKey(new PessoaAtuacaoBloqueioFilterSpecification
            {
                SeqPessoaAtuacao = seqPessoaAtuacao,
                TokenMotivoBloqueio = TOKEN_MOTIVO_BLOQUEIO.IMPEDIMENTO_RENOVACAO_PRAZO_CONCLUSAO,
                SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                DataBloqueioMenorOuIgualA = DateTime.Now
            });
            if (bloqueioRenovacaoPrazoConclusao != null)
            {
                bloqueioRenovacaoPrazoConclusao.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                bloqueioRenovacaoPrazoConclusao.DataDesbloqueioEfetivo = dataDeposito;
                bloqueioRenovacaoPrazoConclusao.TipoDesbloqueio = TipoDesbloqueio.Efetivo;
                bloqueioRenovacaoPrazoConclusao.JustificativaDesbloqueio = "Desbloqueado automaticamente pelo depósito da tese/dissertação";
                bloqueioRenovacaoPrazoConclusao.UsuarioDesbloqueioEfetivo = SMCContext.User.Identity.Name;

                PessoaAtuacaoBloqueioDomainService.UpdateFields(bloqueioRenovacaoPrazoConclusao, x => x.SituacaoBloqueio,
                                                                                                 x => x.DataDesbloqueioEfetivo,
                                                                                                 x => x.TipoDesbloqueio,
                                                                                                 x => x.JustificativaDesbloqueio,
                                                                                                 x => x.UsuarioDesbloqueioEfetivo);
            }

            /* 4. Se o aluno possuir uma situação de matricula 'PRAZO_CONCLUSAO_ENCERRADO' que não esteja excluída logicamente 
                (aluno_historico_situacao.dat_exclusao is null) e cuja data de inicio desta situação seja maior ou igual à data 
                de deposito informada, excluir logicamente esta situação de matricula.
                Nesta exclusão atualizar os dados da tabela aluno_historico_situação conforme valores abaixo:
                - dat_exclusao = getdate()
                - usu_exclusao =  usuário logado
                - dsc_observacao_exclusao = 'Excluido pelo deposito da tese/dissertação - Data de depósito: ' + data do deposito
                - seq_solicitacao_servico_exclusao = solicitação de serviço sendo atendida
                - dat_alteracao = getdate()
                - usu_alteracao = usuário logado
            */
            var dadosAluno = AlunoDomainService.SearchProjectionByKey(seqPessoaAtuacao, x => new
            {
                SeqsAlunoHistoricoSituacaoPrazoConclusaoEncerrado = x.Historicos.FirstOrDefault(h => h.Atual)
                                                   .HistoricosCicloLetivo.SelectMany(h => h.AlunoHistoricoSituacao)
                                                   .Where(s => !s.DataExclusao.HasValue && s.DataInicioSituacao >= dataDeposito && s.SituacaoMatricula.Token == TOKENS_SITUACAO_MATRICULA.PRAZO_CONCLUSAO_ENCERRADO).Select(s => s.Seq).ToList(),
                SeqsAlunoFormacao = x.Historicos.FirstOrDefault(h => h.Atual).Formacoes.Select(f => f.Seq).ToList()
            });
            if (dadosAluno != null && dadosAluno.SeqsAlunoHistoricoSituacaoPrazoConclusaoEncerrado != null && dadosAluno.SeqsAlunoHistoricoSituacaoPrazoConclusaoEncerrado.Any())
            {
                foreach (var seqAlunoHistoricoSituacao in dadosAluno.SeqsAlunoHistoricoSituacaoPrazoConclusaoEncerrado)
                {
                    var observacaoExclusao = "Excluido pelo deposito da tese/dissertação - Data de depósito: " + dataDeposito.ToString("dd/MM/yyyy HH:mm:ss");
                    AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(seqAlunoHistoricoSituacao, observacaoExclusao, seqSolicitacaoServico);
                }
            }

            /* 5. Verificar se a data de depósito está compreendida em algum período de intercâmbio do aluno (considerar data início e fim do
                intercâmbio).
                5.1. Se estiver, verificar qual é a data fim do período de intercâmbio.
                5.1.1. Se a data fim do intercâmbio for igual a data fim do ciclo letivo*, não fazer nada.
                5.1.2. Caso contrário, verificar se no ciclo letivo correspondente à data fim do intercâmbio existe alguma situação cujo tipo é
                “MATRICULADO”.
                5.1.2.1. Se não existir, não fazer nada.
                5.1.2.2. Se existir, incluir a situação "PROVAVEL_FORMANDO", com a data início igual data fim do período de intercâmbio +1 dia
                (hora 00:00). Caso exista outra situação na mesma data, excluir logicamente, setando (desconsiderar situações com data de exclusão
                já setada):
                - Data de exclusão: data e hora corrente do sistema
                - Usuário de exclusão: usuário logado
                - Observação de exclusão: Excluído pelo deposito da tese/dissertação - Data de depósito: ‘ + data do deposito’
                - Solicitação de exclusão: sequencial da solicitação de serviço se o depósito tiver sido realizado através de atendimento de uma
                solicitação pela central de serviços. Caso contrário, gravar o valor null.
                5.2. Se não estiver, realizar a inclusão da situação de matricula 'PROVAVEL_FORMANDO' para o aluno no ciclo letivo correspondente a data de deposito. 
                Caso este ciclo não exista para o aluno, o mesmo deverá ser incluído.
                Este registro será incluído na tabela aluno_historico_situação onde:
                - seq_situação_matricula = sequencial da tabela mat.situacao_matricula onde dsc_token = 'PROVAVEL_FORMANDO'
                - seq_solicitacao_servico = solicitação de serviço sendo atendida
                - dat_inicio_situacao = dat_deposito
                - dsc_observacao = ‘Inclusão de nova situação a partir do depósito da tese/dissertação’
                - dat_exclusao = null
                - usu_exclusao = null
                - dsc_observacao_exclusao = null
                - seq_solicitacao_servico_exclusao = null
            */
            // Busca os dados do ciclo letivo na data de deposito
            var cicloLetivo = ConfiguracaoEventoLetivoDomainService.BuscarCicloLetivo(dataDeposito, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

            // Verifica se a data de depósito está compreendida em algum período de intercâmbio do aluno (considerar data início e fim do intercâmbio). 
            var specInter = new PeriodoIntercambioFilterSpecification()
            {
                SeqAluno = seqPessoaAtuacao,
                DataInicioSituacao = dataDeposito
            };
            var intercambio = PeriodoIntercambioDomainService.SearchByKey(specInter);
            if (intercambio != null)
            {
                // Encontrou intercâmbio na data de depósito...
                // Obs.: Os períodos de intercâmbio não tem sobreposição de datas, por isso a pesquisa com SearchByKey
                // Se tiver só existirá 1 periodo!

                // 5.1.1. Se a data fim do intercâmbio for igual a data fim do ciclo letivo (da data de deposito), não fazer nada.
                // Nesse caso a preparação para a matrícula e renovação irá tratar a situação de PROVAVEL_FORMANDO

                //  5.1.2. Caso contrário, verificar se no ciclo letivo correspondente à data fim do intercâmbio existe alguma situação 
                // cujo tipo é “MATRICULADO”.
                if (intercambio.DataFim != cicloLetivo.DataFim)
                {
                    // Busca o ciclo letivo na data de fim do intercambio
                    var cicloLetivoFimIntercambio = ConfiguracaoEventoLetivoDomainService.BuscarCicloLetivo(intercambio.DataFim, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
                    if (cicloLetivoFimIntercambio != null)
                    {

                        // Verifica se no ciclo letivo do fim do intercambio existe situação com TIPO = MATRICULADO
                        var specSituacaoInter = new AlunoHistoricoSituacaoFilterSpecification()
                        {
                            SeqPessoaAtuacaoAluno = seqPessoaAtuacao,
                            SeqCicloLetivo = cicloLetivoFimIntercambio.SeqCicloLetivo,
                            TokenTipoSituacao = TOKENS_TIPO_SITUACAO_MATRICULA.MATRICULADO
                        };
                        var qtdSituacaoMatriculado = AlunoHistoricoSituacaoDomainService.Count(specSituacaoInter);

                        // 5.1.2.1.Se não existir, não fazer nada.
                        // 5.1.2.2.Se existir, incluir a situação "PROVAVEL_FORMANDO", com a data início igual data fim do período 
                        // de intercâmbio + 1 dia
                        if (qtdSituacaoMatriculado > 0)
                        {
                            // Caso exista outra situação na mesma data, excluir logicamente
                            // Obs.: Está excluindo as outra situações antes de incluir o histórico de provavel formando
                            // pois caso contrário o próprio registro de histórico incluido será na sequencia excluido.
                            var specOutraSituacao = new AlunoHistoricoSituacaoFilterSpecification()
                            {
                                SeqPessoaAtuacaoAluno = seqPessoaAtuacao,
                                SeqCicloLetivo = cicloLetivoFimIntercambio.SeqCicloLetivo,
                                DataInicioSituacao = intercambio.DataFim.AddDays(1)
                            };
                            var outrasSitMesmaData = AlunoHistoricoSituacaoDomainService.SearchBySpecification(specOutraSituacao).ToList();
                            foreach (var situacao in outrasSitMesmaData)
                            {
                                var observacaoExclusao = "Excluido pelo deposito da tese/dissertação - Data de depósito: " + dataDeposito.ToString("dd/MM/yyyy HH:mm:ss");
                                AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacao.Seq, observacaoExclusao, seqSolicitacaoServico);
                            }

                            // Inclui o histórico de provavel formando
                            var historico = new IncluirAlunoHistoricoSituacaoVO()
                            {
                                SeqAluno = seqPessoaAtuacao,
                                SeqCicloLetivo = cicloLetivoFimIntercambio.SeqCicloLetivo,
                                TokenSituacao = TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO,
                                SeqSolicitacaoServico = seqSolicitacaoServico,
                                DataInicioSituacao = intercambio.DataFim.AddDays(1),
                                Observacao = "Inclusão de nova situação a partir do depósito da tese/dissertação"
                            };
                            AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(historico);
                        }
                    }
                }
            }
            else
            {
                // 5.2. Se não estiver, realizar a inclusão da situação de matricula 'PROVAVEL_FORMANDO' para o aluno no ciclo letivo correspondente a data de deposito. 
                // Caso este ciclo não exista para o aluno, o mesmo deverá ser incluído.
                var historico = new IncluirAlunoHistoricoSituacaoVO()
                {
                    SeqAluno = seqPessoaAtuacao,
                    SeqCicloLetivo = cicloLetivo.SeqCicloLetivo,
                    TokenSituacao = TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO,
                    SeqSolicitacaoServico = seqSolicitacaoServico,
                    DataInicioSituacao = dataDeposito,
                    Observacao = "Inclusão de nova situação a partir do depósito da tese/dissertação",
                    CriarAlunoHistoricoCicloLetivo = true
                };
                AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(historico);
            }

            /* 6. Realizar a inclusão da situação "Provável concluinte" na apuração de todas as formações registradas para o aluno. 
                Ou seja, incluir registro na tabela (cnc.apuracao_formacao), conforme atributos listados abaixo:
                - seq_aluno_formacao = Todos os registros existentes na base para o seq_aluno_historico atual do aluno em questão.
                - idt_dom_situacao_aluno_formacao = 'Provável concluinte'
                - dat_inicio = data do depósito (considerar também o horário)
            */
            if (dadosAluno != null && dadosAluno.SeqsAlunoFormacao != null && dadosAluno.SeqsAlunoFormacao.Any())
            {
                foreach (var seqAlunoFormacao in dadosAluno.SeqsAlunoFormacao)
                {
                    ApuracaoFormacao novaApuracao = new ApuracaoFormacao
                    {
                        SeqAlunoFormacao = seqAlunoFormacao,
                        SituacaoAlunoFormacao = SituacaoAlunoFormacao.ProvavelConcluinte,
                        DataInicio = dataDeposito,
                    };
                    ApuracaoFormacaoDomainService.SaveEntity(novaApuracao);
                }
            }
        }

        /// <summary>
        /// Implementação da RN_ORT_025 - Incluir Autorização Novo Depósito
        /// </summary>
        /// <param name="seqTrabalhoAcademico">Sequencial do trabalho academico</param>
        /// <param name="justificativa">Justificativa para inclusão do segundo depósito</param>
        /// <param name="dataAutorizacao">Data de autorização para o novo deposito</param>
        /// <param name="arquivo">Arquivo anexo</param>
        public void IncluirSegundoDeposito(long seqTrabalhoAcademico, string justificativa, DateTime dataAutorizacao, ArquivoAnexado arquivo)
        {
            // Buscar dados do trabalho
            var specTrabalho = new SMCSeqSpecification<TrabalhoAcademico>(seqTrabalhoAcademico);
            var trabalhoAcademico = this.SearchProjectionByKey(specTrabalho, t => new
            {
                Seq = t.Seq,
                SeqAluno = t.Autores.FirstOrDefault().SeqAluno,
                Avaliacoes = t.DivisoesComponente.Select(s => s.OrigemAvaliacao.AplicacoesAvaliacao.Where(w => !w.DataCancelamento.HasValue).OrderByDescending(a => a.DataInicioAplicacaoAvaliacao).Select(
                        a => new
                        {
                            Data = a.DataInicioAplicacaoAvaliacao,
                        }).FirstOrDefault()).ToList()
            });

            // Busca os dados do aluno
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(trabalhoAcademico.SeqAluno);

            //TASK - 60552 1 - Comentar, no código implementado, o trecho abaixo: Este item não será mais verificado.
            // 1. Se o aluno estiver com a situação com data imediatamente anterior à data de autorização, no ciclo letivo* correspondente a data de
            // autorização, cujo tipo de situação possui o token : CANCELADO(desconsiderar a situação que esteja com data de exclusão
            // informada), abortar a operação e exibir a seguinte mensagem de erro: “Não é possível prosseguir. O aluno está com situação de matrícula cancelada na data de autorização informada.”

            //var sitDataAnteriorAutorizacao = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAlunoNaData(trabalhoAcademico.SeqAluno, dataAutorizacao.AddDays(-1));
            //if (sitDataAnteriorAutorizacao?.TokenTipoSituacaoMatricula == TOKENS_TIPO_SITUACAO_MATRICULA.CANCELADO)
            //    throw new SituacaoMatriculaCanceladaDataAutorizacaoException();

            // 1. Se o aluno estiver com a situação de matrícula com a maior data de início entre todos os históricos de ciclo letivo, cujo tipo de
            // situação possui o token: CANCELADO(desconsiderar a situação que esteja com data de exclusão informada), abortar a operação e
            // a seguinte mensagem de erro: “Não é possível prosseguir. O aluno está com situação de matrícula cancelada.”

            var sitAtual = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(trabalhoAcademico.SeqAluno);
            if (sitAtual?.TokenTipoSituacaoMatricula == TOKENS_TIPO_SITUACAO_MATRICULA.CANCELADO)
                throw new SituacaoMatriculaCanceladaException();

            // 2. Se a data da autorização for menor que a maior data de inicio de avaliação associada ao trabalho(desconsiderar as avaliações
            // que esteja com data de cancelamento informada) abortar a operação e exibir a seguinte mensagem de erro:
            // “Não é possível prosseguir.A data de autorização deve ser maior que a data da defesa em que o aluno foi reprovado.”

            if (dataAutorizacao < trabalhoAcademico.Avaliacoes.FirstOrDefault().Data)
                throw new DataAutorizacaoInferiorDataDefesaException();


            //3 - Implementar os itens abaixo(corresponde ao item 3 da regra):
            //-Verificar se a data de autorização informada é maior  que a data fim do periodo letivo do ciclo letivo imediatamente posterior ao ciclo letivo da data da defesa :
            //Calcular a data fim  do tipo de evento ' periodo do ciclo letivo' conforme abaixo:
            //-Buscar a data fim do periodo do ciclo letivo na configuração de evento letivo conforme RN_CAM_030 -Retorna período do evento letivo passando como parâmetros:
            //1) Ciclo letivo = ciclo letivo imediatamente posterior ao ciclo letivo da defesa da tese/ dissertação que esta recebendo a autorização
            //2) Tipo de evento = PERIODO_CICLO_LETIVO
            //3) Curso oferta localidade turno = curso oferta localidade turno do aluno
            //4) Tipo Aluno = 2(veterano)
            var cicloLetivoNormal = ConfiguracaoEventoLetivoDomainService.BuscarCicloLetivo(trabalhoAcademico.Avaliacoes.FirstOrDefault().Data, dadosOrigem.SeqCursoOfertaLocalidadeTurno, Common.Areas.ALN.Enums.TipoAluno.Veterano, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
            long? seqCicloPosterior = CicloLetivoDomainService.BuscarProximoCicloLetivo(cicloLetivoNormal.SeqCicloLetivo);

            if (seqCicloPosterior.HasValue)
            {
                var periodoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(seqCicloPosterior.Value,
                                                                           dadosOrigem.SeqCursoOfertaLocalidadeTurno,
                                                                           Common.Areas.ALN.Enums.TipoAluno.Veterano,
                                                                            TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);


                //3.2 - Se a data da autorização informada for maior que a data fim do período do ciclo letivo que foi retornado no item 3.1 enviar a mensagem de erro:
                //“Não é possível prosseguir. A data de autorização deve ser menor ou igual a<dd/ mm / aaaa > que corresponde a data de fim do ciclo letivo imediatamente posterior ao ciclo da defesa'.
                if (dataAutorizacao > periodoLetivo.DataFim)
                    throw new DataAutorizacaoMaiorDataFimException(periodoLetivo.DataFim);
            }

            // 4. Se a data de autorização for maior que a data de previsão de conclusão, abortar a operação e exibir a seguinte mensagem de erro:
            // “Não é possível prosseguir.A data de autorização deve ser menor que a data de previsão de conclusão do aluno(<< data de previsão
            // de conclusão atual >>).O aluno deve solicitar prorrogação de prazo de conclusão.”

            var spec = new AlunoHistoricoPrevisaoConclusaoFilterSpecification()
            {
                SeqPessoaAtuacao = trabalhoAcademico.SeqAluno,
                AlunoHistoricoAtual = true
            };

            var previsao = AlunoHistoricoPrevisaoConclusaoDomainService.SearchBySpecification(spec).OrderByDescending(x => x.DataInclusao).FirstOrDefault();
            if (dataAutorizacao > previsao?.DataPrevisaoConclusao)
                throw new DataAutorizacaoInferiorDataConclusaoException(previsao.DataPrevisaoConclusao);


            //Se nenhuma das condições acima ocorrer, prosseguir:
            // · Alterar o tipo do trabalho para o tipo de trabalho de cancelamento correspondente, que esta parametrizado em ' instituição nivel
            // tipo de trabalho' considerando o nível de ensino do aluno
            var seqTipoTrabalhoCancelamento = InstituicaoNivelTipoTrabalhoDomainService.SearchProjectionByKey(new InstituicaoNivelTipoTrabalhoFilterSpecification
            {
                SeqInstituicaoEnsino = dadosOrigem.SeqInstituicaoEnsino,
                SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
            }, x => x.SeqTipoTrabalhoCancelamento);


            var modelBloqueioPrazo = new PessoaAtuacaoBloqueioVO()
            {
                SeqPessoaAtuacao = trabalhoAcademico.SeqAluno,
                Observacao = "Bloqueado em função da oportunidade de segundo depósito",
                SituacaoBloqueio = SituacaoBloqueio.Bloqueado
            };

            // · Bloquear o bloqueio do aluno cujo token é PRAZO_CONCLUSAO_CURSO_ENCERRADO, conforme: 
            // - Situação: “Bloqueado”
            // - Data do bloqueio: Data de previsão de conclusão atual do aluno + 1
            // - Data do desbloqueio efetivo e temporário: null 
            // - Usuário do desbloqueio efetivo e temporário: null
            // - Observação do desbloqueio: null 
            // - Justificativa do desbloqueio: null 
            // - Tipo do desbloqueio: null 
            // - Observação do bloqueio: “Bloqueado em função da oportunidade de segundo depósito"
            PessoaAtuacaoBloqueioDomainService.BloquearBloqueioAluno(modelBloqueioPrazo, TOKEN_MOTIVO_BLOQUEIO.PRAZO_CONCLUSAO_CURSO_ENCERRADO, null, previsao.DataPrevisaoConclusao.AddDays(1));

            // No ciclo letivo* correspondente a data de autorização, verificar:

            var cicloDataAutorizacao = ConfiguracaoEventoLetivoDomainService.BuscarCicloLetivo(dataAutorizacao, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
            if (cicloDataAutorizacao != null)
            {

                var situacoesAlunoCicloLetivoSemExclusao = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAlunoNoCicloLetivoSegundoDeposito(trabalhoAcademico.SeqAluno, cicloDataAutorizacao.SeqCicloLetivo, false);
                var situacoesAlunoCicloLetivoSemInformarExclusao = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAlunoNoCicloLetivoSegundoDeposito(trabalhoAcademico.SeqAluno, cicloDataAutorizacao.SeqCicloLetivo, null);
                var situacoesAlunoCicloLetivoExcluido = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAlunoNoCicloLetivoSegundoDeposito(trabalhoAcademico.SeqAluno, cicloDataAutorizacao.SeqCicloLetivo, true);

                var cicloPrevisao = ConfiguracaoEventoLetivoDomainService.BuscarCicloLetivo(previsao.DataPrevisaoConclusao, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

                // OBSERVAÇÃO: se não existir ciclo letivo na data da autorização ou se não existir registro no histórico do aluno com ciclo
                //correspondente ao da data de autorização, não executar as regras abaixo.
                if (situacoesAlunoCicloLetivoSemExclusao != null)
                {
                    // 1.Se o aluno estiver com a situação atual no ciclo letivo* correspondente a data de autorização, cujo token é
                    // provavel_formando(desconsiderar a situação que esteja com data de exclusão informada)
                    if (situacoesAlunoCicloLetivoSemExclusao?.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO)
                    {
                        // 1.1. Incluir a situação com o token MATRICULADO , no histórico do ciclo letivo encontrado, com a data início igual a data de
                        // autorização e a observação: “Inclusão de nova situação em função da oportunidade de segundo depósito”
                        var situacao = new IncluirAlunoHistoricoSituacaoVO()
                        {
                            SeqAluno = trabalhoAcademico.SeqAluno,
                            SeqCicloLetivo = cicloDataAutorizacao.SeqCicloLetivo,
                            TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO,
                            DataInicioSituacao = dataAutorizacao,
                            Observacao = "Inclusão de nova situação em função da oportunidade de segundo depósito"
                        };

                        AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(situacao);

                        // 1.1.1.Verificar se a data de previsão de conclusão pertence ao mesmo ciclo letivo.
                        // Caso pertença, verificar se já existe uma situação no ciclo com token PRAZO_CONCLUSAO_ENCERRADO(desconsiderar a situação que esteja com data de exclusão informada).
                        // Se existir, não fazer nada.
                        // Se não existir, incluir a situação PRAZO_CONCLUSAO_ENCERRADO, com a data início igual a data de
                        // previsão de conclusão +1 dia e a observação “Retorno da situação em função da oportunidade de segundo depósito”
                        // 1.1.2.Caso não pertença, não fazer nada.
                        if (cicloDataAutorizacao?.SeqCicloLetivo == cicloPrevisao?.SeqCicloLetivo)
                        {
                            var situacoesNoCiclo = AlunoHistoricoSituacaoDomainService.BuscarSituacoesAlunoNoCicloLetivo(trabalhoAcademico.SeqAluno, cicloDataAutorizacao.SeqCicloLetivo);
                            if (!situacoesNoCiclo.Any(s => s.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.PRAZO_CONCLUSAO_ENCERRADO && !s.DataExclusao.HasValue))
                            {

                                var situacaoPrazoEncerrado = new IncluirAlunoHistoricoSituacaoVO()
                                {
                                    SeqAluno = trabalhoAcademico.SeqAluno,
                                    SeqCicloLetivo = cicloDataAutorizacao.SeqCicloLetivo,
                                    TokenSituacao = TOKENS_SITUACAO_MATRICULA.PRAZO_CONCLUSAO_ENCERRADO,
                                    DataInicioSituacao = previsao.DataPrevisaoConclusao.AddDays(1),
                                    Observacao = "Retorno da situação em função da oportunidade de segundo depósito"
                                };

                                AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(situacaoPrazoEncerrado);

                            }
                        }
                    }
                }

                // 2.Se existir apenas a situação cujo token é APTO_MATRICULA
                if (situacoesAlunoCicloLetivoSemInformarExclusao?.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA)
                {
                    //2.1.Incluir uma nova situação com o token APTO_MATRICULA com a data de inicio igual a data de início da situação cujo token e
                    //APTO_MATRICULA que foi excluída e Observação: “Inclusão de nova situação em função da oportunidade de segundo depósito”.

                    var situacaoAptoExcluidaSpec = new AlunoHistoricoSituacaoFilterSpecification
                    {
                        Excluido = true,
                        TokenSituacao = TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA,
                        SeqPessoaAtuacaoAluno = trabalhoAcademico.SeqAluno
                    };

                    var situacaoAptoExcluida = AlunoHistoricoSituacaoDomainService.SearchByKey(situacaoAptoExcluidaSpec);

                    var situacaoSegundoDeposito = new IncluirAlunoHistoricoSituacaoVO()
                    {
                        SeqAluno = trabalhoAcademico.SeqAluno,
                        SeqCicloLetivo = cicloDataAutorizacao.SeqCicloLetivo,
                        TokenSituacao = TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA,
                        DataInicioSituacao = situacaoAptoExcluida.DataInicioSituacao,
                        Observacao = "Inclusão de nova situação em função da oportunidade de segundo depósito"
                    };

                    AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(situacaoSegundoDeposito);



                    var model = new PessoaAtuacaoBloqueioVO()
                    {
                        SeqPessoaAtuacao = trabalhoAcademico.SeqAluno,
                        Observacao = "Bloqueado em função da oportunidade de segundo depósito",
                        SituacaoBloqueio = SituacaoBloqueio.Bloqueado
                    };

                    //2.2 Bloquear o bloqueio do aluno cujo token é PRAZO_CONCLUSAO_CURSO_ENCERRADO, conforme:
                    //-Situação: “Bloqueado”
                    //-Data do bloqueio: Data de previsão de conclusão atual do aluno + 1
                    //- Data do desbloqueio efetivo e temporário: null
                    //- Usuário do desbloqueio efetivo e temporário: null
                    //- Observação do desbloqueio: null
                    //- Justificativa do desbloqueio: null
                    //- Tipo do desbloqueio: null
                    //- Observação do bloqueio: “Bloqueado em função da oportunidade de segundo depósito”
                    PessoaAtuacaoBloqueioDomainService.BloquearBloqueioAluno(model, TOKEN_MOTIVO_BLOQUEIO.PRAZO_CONCLUSAO_CURSO_ENCERRADO, null, previsao.DataPrevisaoConclusao.AddDays(1));
                }

                // 3.Se o aluno estiver com a situação atual no ciclo letivo* correspondente a data de autorização, cujo token é
                //PROVAVEL_FORMANDO com data de exclusão informada:
                if (situacoesAlunoCicloLetivoExcluido?.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO)
                {
                    //3.1.Incluir a situação com o token ‘APTO_MATRICULA’ , no histórico do ciclo letivo encontrado, com a data início igual a data de
                    //‘APTO_MATRICULA’ excluída e a observação: “Inclusão de nova situação em função da oportunidade de segundo depósito”.
                    var specSituacaoApto = new AlunoHistoricoSituacaoFilterSpecification
                    {
                        TokenSituacao = TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA,
                        Excluido = true,
                        SeqPessoaAtuacaoAluno = trabalhoAcademico.SeqAluno
                    };

                    var historicoSituacaoApto = AlunoHistoricoSituacaoDomainService.SearchByKey(specSituacaoApto);
                    if (historicoSituacaoApto != null)
                    {
                        var situacaoAptoMatricula = new IncluirAlunoHistoricoSituacaoVO()
                        {
                            SeqAluno = trabalhoAcademico.SeqAluno,
                            SeqCicloLetivo = cicloDataAutorizacao.SeqCicloLetivo,
                            TokenSituacao = TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA,
                            DataInicioSituacao = historicoSituacaoApto.DataInicioSituacao,
                            Observacao = "Inclusão de nova situação em função da oportunidade de segundo depósito"
                        };

                        AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(situacaoAptoMatricula);
                    }

                    //3.2. Incluir a situação com o token MATRICULADO, no histórico do ciclo letivo encontrado, com a data início igual a data da
                    //autorização e a observação: “Inclusão de nova situação em função da oportunidade de segundo depósito”.
                    var situacaoMatriculado = new IncluirAlunoHistoricoSituacaoVO()
                    {
                        SeqAluno = trabalhoAcademico.SeqAluno,
                        SeqCicloLetivo = cicloDataAutorizacao.SeqCicloLetivo,
                        TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO,
                        DataInicioSituacao = dataAutorizacao,
                        Observacao = "Inclusão de nova situação em função da oportunidade de segundo depósito"
                    };

                    AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(situacaoMatriculado);


                    //3.2.1.Verificar se a data de previsão de conclusão pertence ao mesmo ciclo letivo*.Caso pertença, verificar se já existe uma situação
                    //no ciclo com token PRAZO_CONCLUSAO_ENCERRADO(desconsiderar situações com data de exclusão).Se existir, não fazer nada.
                    if (cicloDataAutorizacao?.SeqCicloLetivo == cicloPrevisao?.SeqCicloLetivo)
                    {
                        var situacoesNoCiclo = AlunoHistoricoSituacaoDomainService.BuscarSituacoesAlunoNoCicloLetivo(trabalhoAcademico.SeqAluno, cicloDataAutorizacao.SeqCicloLetivo);
                        if (!situacoesNoCiclo.Any(s => s.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.PRAZO_CONCLUSAO_ENCERRADO && !s.DataExclusao.HasValue))
                        {
                            //Se não existir, incluir a situação PRAZO_CONCLUSAO_ENCERRADO, com a data início igual a data de previsão de conclusão +1
                            //dia e a observação “Retorno da situação em função da oportunidade de segundo depósito”.
                            var situacaoPrazoEncerrado = new IncluirAlunoHistoricoSituacaoVO()
                            {
                                SeqAluno = trabalhoAcademico.SeqAluno,
                                SeqCicloLetivo = cicloDataAutorizacao.SeqCicloLetivo,
                                TokenSituacao = TOKENS_SITUACAO_MATRICULA.PRAZO_CONCLUSAO_ENCERRADO,
                                DataInicioSituacao = previsao.DataPrevisaoConclusao.AddDays(1),
                                Observacao = "Retorno da situação em função da oportunidade de segundo depósito"
                            };

                            AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(situacaoPrazoEncerrado);
                        }
                    }

                    var modelBloqueio = new PessoaAtuacaoBloqueioVO()
                    {
                        SeqPessoaAtuacao = trabalhoAcademico.SeqAluno,
                        Observacao = "Bloqueado em função da oportunidade de segundo depósito",
                        SituacaoBloqueio = SituacaoBloqueio.Bloqueado
                    };

                    // 3.3 Bloquear o bloqueio do aluno cujo token é PRAZO_CONCLUSAO_CURSO_ENCERRADO, conforme: 
                    // - Situação: “Bloqueado”
                    // - Data do bloqueio: Data de previsão de conclusão atual do aluno + 1
                    // - Data do desbloqueio efetivo e temporário: null 
                    // - Usuário do desbloqueio efetivo e temporário: null
                    // - Observação do desbloqueio: null 
                    // - Justificativa do desbloqueio: null 
                    // - Tipo do desbloqueio: null 
                    // - Observação do bloqueio: “Bloqueado em função da oportunidade de segundo depósitO"
                    PessoaAtuacaoBloqueioDomainService.BloquearBloqueioAluno(modelBloqueio, TOKEN_MOTIVO_BLOQUEIO.PRAZO_CONCLUSAO_CURSO_ENCERRADO, null, previsao.DataPrevisaoConclusao.AddDays(1));

                    //3.4.Se existir plano de estudos ATUAL no ciclo letivo correspondente a data de autorização:
                    var specPlano = new PlanoEstudoFilterSpecification()
                    {
                        SeqAluno = trabalhoAcademico.SeqAluno,
                        Atual = true,
                        SeqCicloLetivo = cicloDataAutorizacao.SeqCicloLetivo
                    };

                    var planoEstudo = PlanoEstudoDomainService.SearchByKey(specPlano);
                    if (planoEstudo != null)
                    {
                        //3.4.1.Alterar o indicador de atual do plano de estudos para o valor não.
                        planoEstudo.Atual = false;
                        this.UpdateFields(planoEstudo, x => x.Atual);

                        var matrizCurricularOfertaSpec = new MatrizCurricularOfertaFilterSpecification
                        {
                            Seq = planoEstudo.SeqMatrizCurricularOferta
                        };

                        var matrizCurricularOferta = MatrizCurricularOfertaDomainService.SearchByKey(matrizCurricularOfertaSpec);
                        if (matrizCurricularOferta != null)
                        {
                            var matrizCurricularSpec = new MatrizCurricularFilterSpecification { Seq = matrizCurricularOferta.SeqMatrizCurricular };
                            var matrizCurricular = MatrizCurricularDomainService.SearchByKey(matrizCurricularSpec);
                            if (matrizCurricular != null)
                            {

                                //3.4.2.Incluir um novo registro de plano de estudos , para o ciclo letivo correspondente a data de autorização de acordo com os
                                //valores abaixo:
                                //-Aluno histórico por ciclo letivo: buscar o aluno histórico por ciclo letivo, de acordo com o ciclo letivo encontrado e o aluno histórico mais atual do aluno em questão;
                                //-Oferta de matriz: oferta de matriz, do plano de estudos que foi colocado com indicador de atual com o valor não.
                                //- Indicador de atual: sim;
                                //-Descrição da observação: "Plano de estudos criado em função da oportunidade de segundo depósito”"
                                PlanoEstudo plano = new PlanoEstudo()
                                {
                                    SeqAlunoHistoricoCicloLetivo = planoEstudo.SeqAlunoHistoricoCicloLetivo,
                                    SeqMatrizCurricularOferta = planoEstudo.SeqMatrizCurricularOferta,
                                    Atual = true,
                                    Observacao = "Plano de estudos criado em função da oportunidade de segundo depósito"
                                };
                                PlanoEstudoDomainService.InsertEntity(plano);

                                //3.4.2.1.Incluir o componente padrão da matriz curricular do aluno.
                                if (plano.Itens == null || !plano.Itens.Any())
                                {
                                    plano.Itens = new List<PlanoEstudoItem>();

                                    var specConfiguracaoComponente = new ConfiguracaoComponenteFilterSpecification { SeqComponenteCurricular = matrizCurricular.SeqComponenteCurricularPadrao };
                                    var configuracaoComponente = ConfiguracaoComponenteDomainService.SearchByKey(specConfiguracaoComponente);

                                    var planoEstudoItemPadrao = new PlanoEstudoItem()
                                    {
                                        SeqPlanoEstudo = plano.Seq,
                                        SeqConfiguracaoComponente = configuracaoComponente.Seq,
                                        DataInclusao = DateTime.Now,
                                        UsuarioInclusao = User.Identity.Name
                                    };

                                    PlanoEstudoItemDomainService.InsertEntity(planoEstudoItemPadrao);
                                }
                            }
                        }

                        //4.Caso contrário, não fazer nada
                    }
                }

                // PARA CADA CICLO LETIVO POSTERIOR AO CICLO ENCONTRADO ANTERIORMENTE*
                long? proxCiclo = null;

                if (cicloDataAutorizacao != null)
                {
                    proxCiclo = CicloLetivoDomainService.BuscarProximoCicloLetivo(cicloDataAutorizacao.SeqCicloLetivo);
                }

                // Quando não houver proximo ciclo, o retorno sera null e o loop encerrado.
                while (proxCiclo != null)
                {
                    var sitProximoCiclo = AlunoHistoricoSituacaoDomainService.BuscarSituacoesAlunoNoCicloLetivo(trabalhoAcademico.SeqAluno, proxCiclo.Value);

                    //1.Se existir apenas a situação cujo token é APTO_MATRICULA
                    var tokenAptoMatricula = sitProximoCiclo.Count();

                    if (tokenAptoMatricula == 1)
                    {
                        //1.1 Incluir uma nova situação com o token APTO_MATRICULA com a data de inicio igual a data de início da situação cujo o token e
                        //APTO_MATRICULA que foi excluída e Observação: “Inclusão de nova situação em função da oportunidade de segundo depósito”.
                        var situacaoSegundoDeposito = new IncluirAlunoHistoricoSituacaoVO()
                        {
                            SeqAluno = trabalhoAcademico.SeqAluno,
                            SeqCicloLetivo = proxCiclo.Value,
                            TokenSituacao = TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA,
                            DataInicioSituacao = sitProximoCiclo.First().DataInicioSituacao,
                            Observacao = "Inclusão de nova situação em função da oportunidade de segundo depósito"
                        };

                        AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(situacaoSegundoDeposito);

                        var specBloqueioAlunoPrazo = new PessoaAtuacaoBloqueioFilterSpecification
                        {
                            TokenMotivoBloqueio = TOKEN_MOTIVO_BLOQUEIO.PRAZO_CONCLUSAO_CURSO_ENCERRADO,
                            SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                            DataBloqueioMenorOuIgualA = sitProximoCiclo.First().DataInicioSituacao,
                            SeqPessoaAtuacao = trabalhoAcademico.SeqAluno
                        };

                        var bloqueioAlunoPrazoConclusao = PessoaAtuacaoBloqueioDomainService.SearchBySpecification(specBloqueioAlunoPrazo);

                        var specAlunoHistoricoSituacao = new AlunoHistoricoSituacaoFilterSpecification { SeqPessoaAtuacaoAluno = trabalhoAcademico.SeqAluno, TokenSituacao = TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA, Excluido = true };
                        var alunoHistoricoSituacao = AlunoHistoricoSituacaoDomainService.SearchByKey(specAlunoHistoricoSituacao);
                        if (alunoHistoricoSituacao != null)
                        {
                            var specBloqueioAlunoImpedimento = new PessoaAtuacaoBloqueioFilterSpecification
                            {
                                TokenMotivoBloqueio = TOKEN_MOTIVO_BLOQUEIO.IMPEDIMENTO_RENOVACAO_PRAZO_CONCLUSAO,
                                SeqPessoaAtuacao = trabalhoAcademico.SeqAluno,
                                SeqSolicitacaoServico = alunoHistoricoSituacao.SeqSolicitacaoServico
                            };

                            var bloqueiosAlunoImpedimento = PessoaAtuacaoBloqueioDomainService.SearchBySpecification(specBloqueioAlunoImpedimento);

                            //1.2 Verificar se a pessoa-atuação em questão possui o bloqueio com o token PRAZO_CONCLUSAO_CURSO_ENCERRADO, com a
                            //situação bloqueado e com a data de bloqueio menor ou igual a data início do ciclo letivo(PERIODO_CICLO_LETIVO) em questão.
                            if (bloqueioAlunoPrazoConclusao.Any())
                            {
                                //1.2.1.Verificar se existe bloqueio correspondente à solicitação vinculada à situação de matrícula APTO_MATRICULA, que possui o
                                //token IMPEDIMENTO_RENOVACAO_PRAZO_CONCLUSAO.
                                if (bloqueiosAlunoImpedimento.Any())
                                {

                                    var model = new PessoaAtuacaoBloqueioVO()
                                    {
                                        SeqPessoaAtuacao = trabalhoAcademico.SeqAluno,
                                        Observacao = "Bloqueado em função da oportunidade de segundo depósito",
                                        SituacaoBloqueio = SituacaoBloqueio.Bloqueado
                                    };

                                    //1.2.1.1.Se existir, atualizá-lo de acordo com:
                                    //-Situação: “Bloqueado”
                                    //-Data do desbloqueio efetivo e temporário: null
                                    //- Usuário do desbloqueio efetivo e temporário: null
                                    //- Observação do desbloqueio: null
                                    //- Justificativa do desbloqueio: null
                                    //- Tipo do desbloqueio: null
                                    //- Observação do bloqueio: “Bloqueado em função da oportunidade de segundo depósito”.
                                    PessoaAtuacaoBloqueioDomainService.BloquearBloqueioAluno(model, TOKEN_MOTIVO_BLOQUEIO.IMPEDIMENTO_RENOVACAO_PRAZO_CONCLUSAO);
                                }
                                else
                                {

                                    var pessoaAtuacao = PessoaAtuacaoDomainService.SearchByKey(trabalhoAcademico.SeqAluno);
                                    if (pessoaAtuacao != null)
                                    {
                                        var model = new PessoaAtuacaoBloqueioVO()
                                        {
                                            SeqPessoaAtuacao = pessoaAtuacao.Seq,
                                            SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                                            DataBloqueio = DateTime.Now,
                                            DescricaoReferenciaAtuacao = pessoaAtuacao.Descricao,
                                        };
                                        //1.2.1.2.Caso não existir, associar à pessoa-atuação o bloqueio que possui o token
                                        //IMPEDIMENTO_RENOVACAO_PRAZO_CONCLUSAO, de acordo com os valores:
                                        //-Pessoa-atuação: sequencial da pessoa - atuação em questão
                                        //-Motivo de bloqueio: IMPEDIMENTO_RENOVACAO_PRAZO_CONCLUSAO
                                        //- Descrição: “Impedimento de matrícula - Prazo de conclusão de curso encerrado - Segundo depósito”
                                        //-Situação: bloqueado
                                        //- Data de bloqueio: data corrente do sistema
                                        //- Descrição de referência da atuação: descrição da pessoa atuação em questão
                                        //-Cadastro por integração: sim
                                        //- Solicitação de serviço: nulo
                                        PessoaAtuacaoBloqueioDomainService.CriarBloqueioAluno(model, TOKEN_MOTIVO_BLOQUEIO.IMPEDIMENTO_RENOVACAO_PRAZO_CONCLUSAO, true);
                                    }
                                }
                            }
                        }
                    }

                    // 2.Se existir a situação PROVAVEL_FORMANDO
                    var sitFormando = sitProximoCiclo.Where(s => s.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO).FirstOrDefault();
                    if (sitFormando != null)
                    {
                        var situacaoAptoExcluidaSpec = new AlunoHistoricoSituacaoFilterSpecification
                        {
                            Excluido = true,
                            TokenSituacao = TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA,
                            SeqPessoaAtuacaoAluno = trabalhoAcademico.SeqAluno
                        };

                        var situacaoAptoExcluida = AlunoHistoricoSituacaoDomainService.SearchByKey(situacaoAptoExcluidaSpec);

                        // 2.1 Incluir uma nova situação com o token APTO_MATRICULA com a data de inicio igual a data de início da situação cujo o token e
                        // APTO_MATRICULA que foi excluída e Observação: “Inclusão de nova situação em função da oportunidade de segundo depósito”.
                        var situacaoSegundoDepositoAptoMatricula = new IncluirAlunoHistoricoSituacaoVO()
                        {
                            SeqAluno = trabalhoAcademico.SeqAluno,
                            SeqCicloLetivo = sitFormando.SeqCiclo,
                            TokenSituacao = TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA,
                            DataInicioSituacao = situacaoAptoExcluida.DataInicioSituacao,
                            Observacao = "Inclusão de nova situação em função da oportunidade de segundo depósito"
                        };

                        AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(situacaoSegundoDepositoAptoMatricula);

                        // 2.2.Incluir a situação com o token MATRICULADO, no histórico do ciclo letivo encontrado, com
                        //-Data início: data início da situação PROVAVEL_FORMANDO, que foi excluída
                        //-Observação: “Inclusão de nova situação em função da oportunidade de segundo depósito”
                        var situacaoSegundoDepositoMatriculado = new IncluirAlunoHistoricoSituacaoVO()
                        {
                            SeqAluno = trabalhoAcademico.SeqAluno,
                            SeqCicloLetivo = sitFormando.SeqCiclo,
                            TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO,
                            DataInicioSituacao = sitFormando.DataInicioSituacao,
                            Observacao = "Inclusão de nova situação em função da oportunidade de segundo depósito"
                        };

                        AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(situacaoSegundoDepositoMatriculado);

                        // 2.3. Verificar se a data de previsão de conclusão pertence ao ciclo letivo em questão.
                        // 2.3.2.Caso não pertença, não fazer nada.
                        if (cicloPrevisao?.SeqCicloLetivo == proxCiclo.Value)
                        {
                            // 2.3.1. Caso pertença, verificar se já existe uma situação no ciclo com token PRAZO_CONCLUSAO_ENCERRADO (desconsiderar a situação que esteja com data de exclusão informada).
                            // 2.3.1.1 Se existir, não fazer nada.
                            if (!sitProximoCiclo.Any(s => s.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.PRAZO_CONCLUSAO_ENCERRADO && !s.DataExclusao.HasValue))
                            {
                                // 2.3.1.2.Se não existir, incluir a situação PRAZO_CONCLUSAO_ENCERRADO, com:
                                // - Data início: data de previsão de conclusão + 1 dia
                                // - Observação: “Retorno da situação em função da oportunidade de segundo depósito”.
                                var situacaoPrazoEncerradoCiclo = new IncluirAlunoHistoricoSituacaoVO()
                                {
                                    SeqAluno = trabalhoAcademico.SeqAluno,
                                    SeqCicloLetivo = sitFormando.SeqCiclo,
                                    TokenSituacao = TOKENS_SITUACAO_MATRICULA.PRAZO_CONCLUSAO_ENCERRADO,
                                    DataInicioSituacao = previsao.DataPrevisaoConclusao.AddDays(1),
                                    Observacao = "Retorno da situação em função da oportunidade de segundo depósito"
                                };

                                AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(situacaoPrazoEncerradoCiclo);
                            }
                        }

                        //2.4.Se existir plano de estudos ATUAL no ciclo letivo correspondente a data de autorização:
                        var specPlanoCicloPosterior = new PlanoEstudoFilterSpecification()
                        {
                            SeqAluno = trabalhoAcademico.SeqAluno,
                            Atual = true,
                            SeqCicloLetivo = seqCicloPosterior
                        };

                        var planoEstudoPosterior = PlanoEstudoDomainService.SearchByKey(specPlanoCicloPosterior);

                        if (planoEstudoPosterior != null)
                        {
                            //2.4.1.Alterar o indicador de atual do plano de estudos para o valor não.
                            planoEstudoPosterior.Atual = false;
                            this.UpdateFields(planoEstudoPosterior, x => x.Atual);

                            var matrizCurricularOfertaSpec = new MatrizCurricularOfertaFilterSpecification
                            {
                                Seq = planoEstudoPosterior.SeqMatrizCurricularOferta
                            };

                            var matrizCurricularOferta = MatrizCurricularOfertaDomainService.SearchByKey(matrizCurricularOfertaSpec);
                            if (matrizCurricularOferta != null)
                            {
                                var matrizCurricularSpec = new MatrizCurricularFilterSpecification { Seq = matrizCurricularOferta.SeqMatrizCurricular };
                                var matrizCurricular = MatrizCurricularDomainService.SearchByKey(matrizCurricularSpec);
                                if (matrizCurricular != null)
                                {
                                    //2.4.2.Incluir um novo registro de plano de estudos , para o ciclo letivo correspondente a data de autorização de acordo com os valores abaixo:
                                    //-Aluno histórico por ciclo letivo: buscar o aluno histórico por ciclo letivo, de acordo com o ciclo letivo encontrado e o aluno histórico mais atual do aluno em questão;
                                    //-Oferta de matriz: oferta de matriz, do plano de estudos que foi colocado com indicador de atual com o valor não.
                                    //-Indicador de atual: sim;
                                    //-Descrição da observação: "Plano de estudos criado em função da oportunidade de segundo depósito”"
                                    PlanoEstudo plano = new PlanoEstudo()
                                    {
                                        SeqAlunoHistoricoCicloLetivo = planoEstudoPosterior.SeqAlunoHistoricoCicloLetivo,
                                        SeqMatrizCurricularOferta = planoEstudoPosterior.SeqMatrizCurricularOferta,
                                        Atual = true,
                                        Observacao = "Plano de estudos criado em função da oportunidade de segundo depósito"
                                    };
                                    PlanoEstudoDomainService.InsertEntity(plano);

                                    //2.4.2.1. Incluir o componente padrão da matriz curricular do aluno.
                                    if (plano.Itens == null || !plano.Itens.Any())
                                    {
                                        plano.Itens = new List<PlanoEstudoItem>();

                                        var specConfiguracaoComponente = new ConfiguracaoComponenteFilterSpecification { SeqComponenteCurricular = matrizCurricular.SeqComponenteCurricularPadrao };
                                        var configuracaoComponente = ConfiguracaoComponenteDomainService.SearchByKey(specConfiguracaoComponente);

                                        var planoEstudoItemPadrao = new PlanoEstudoItem()
                                        {
                                            SeqPlanoEstudo = plano.Seq,
                                            SeqConfiguracaoComponente = configuracaoComponente.Seq,
                                            DataInclusao = DateTime.Now,
                                            UsuarioInclusao = User.Identity.Name

                                        };

                                        PlanoEstudoItemDomainService.InsertEntity(planoEstudoItemPadrao);
                                    }
                                }
                            }
                        }
                    }

                    proxCiclo = CicloLetivoDomainService.BuscarProximoCicloLetivo(proxCiclo.Value);
                }

                // Incluir o segundo deposito
                var trabalhoAcademicoSegundoDeposito = new TrabalhoAcademico()
                {
                    Seq = trabalhoAcademico.Seq,
                    SeqTipoTrabalho = seqTipoTrabalhoCancelamento.Value,
                    JustificativaSegundoDeposito = justificativa,
                    DataAutorizacaoSegundoDeposito = dataAutorizacao,
                    DataInclusaoSegundoDeposito = DateTime.Now,
                    UsuarioInclusaoSegundoDeposito = SMCContext.User.Identity.Name
                };

                // Se arquivo informado, salvar o anexo enviado
                if (arquivo != null)
                {
                    trabalhoAcademicoSegundoDeposito.ArquivoAnexadoSegundoDeposito = arquivo;
                    ArquivoAnexadoDomainService.SaveEntity(trabalhoAcademicoSegundoDeposito.ArquivoAnexadoSegundoDeposito);

                    // O seq do arquivo anexo é preenchido apos salvar
                    trabalhoAcademicoSegundoDeposito.SeqArquivoAnexadoSegundoDeposito = trabalhoAcademicoSegundoDeposito.ArquivoAnexadoSegundoDeposito.Seq;

                    this.EnsureFileIntegrity(trabalhoAcademicoSegundoDeposito, m => m.SeqArquivoAnexadoSegundoDeposito, m => m.ArquivoAnexadoSegundoDeposito);
                }

                this.UpdateFields(trabalhoAcademicoSegundoDeposito, x => x.JustificativaSegundoDeposito,
                                                    x => x.DataAutorizacaoSegundoDeposito,
                                                    x => x.SeqTipoTrabalho,
                                                    x => x.DataInclusaoSegundoDeposito,
                                                    x => x.UsuarioInclusaoSegundoDeposito,
                                                    x => x.SeqArquivoAnexadoSegundoDeposito);

            }
            else
                throw new TrabalhoAcademicoSemCicloLetivoException();
        }

        /// <summary>
        /// UC_ORT_002_02_03 - Alterar regra para habilitar agendamento da banca
        /// Verifica se o tipo trabalho está parametrizado para não gerar transação financeira na entrega do trabalho, permitindo a inclusão manual
        /// e retorna se é obrigátória ou não a publicação do trabalho na biblioteca
        /// </summary>
        /// <param name="seq">TrabalhoAcademico</param>
        public bool AtendeRegraHabilitarAgendamentoBanca(long seqTrabalhoAcademico)
        {
            var dadosTrabalho = this.SearchProjectionByKey(seqTrabalhoAcademico, x => new
            {
                x.SeqTipoTrabalho,
                x.SeqNivelEnsino,
                x.SeqInstituicaoEnsino
            });

            var configuracaoTrabalho = InstituicaoNivelTipoTrabalhoDomainService.SearchBySpecification(new InstituicaoNivelTipoTrabalhoFilterSpecification()
            {
                SeqTipoTrabalho = dadosTrabalho.SeqTipoTrabalho,
                SeqNivelEnsino = dadosTrabalho.SeqNivelEnsino,
                SeqInstituicaoEnsino = dadosTrabalho.SeqInstituicaoEnsino,
            }).FirstOrDefault();

            var result = configuracaoTrabalho != null &&
                         configuracaoTrabalho.PermiteInclusaoManual &&
                        !configuracaoTrabalho.GeraFinanceiroEntregaTrabalho &&
                        !configuracaoTrabalho.PublicacaoBibliotecaObrigatoria;

            return result;
        }
    }
}