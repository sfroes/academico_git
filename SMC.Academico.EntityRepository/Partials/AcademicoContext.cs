using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.CustomFilters;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.CAM.CustomFilters;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CNC.CustomFilters;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CSO.CustomFilters;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CUR.CustomFilters;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.DCT.CustomFilters;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Academico.Domain.Areas.GRD.Models;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Academico.Domain.Areas.ORG.CustomFilters;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORT.CustomFilters;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Areas.PES.CustomFilters;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.SRC.CustomFilters;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.TUR.CustomFilters;
using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Academico.Domain.Models;
using SMC.Framework.Entity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SMC.Academico.EntityRepository
{
    public partial class AcademicoContext : SMCDbContext
    {
        protected override void AfterConfigureModelBuilder(DbModelBuilder modelBuilder)
        {
            // Esta configuração abaixo foi necessária pois a propriedade é required no banco de dados, entretanto, possui uma função no default que faz um cálculo para definir qual o número do protocolo.
            // O entity framework, quando a propriedade é required, requer que seja informado um valor na entidade ao mandar salvar
            // Desta maneira, colocamos a propriedade como sendo optional e marcamos ela como computada para que ela não vá na query gerada no insert
            // Não esquecer de tirar o [Required] da propriedade do modelo sempre que regerar a classe.
            // Validar se tem como remover o required do banco
            modelBuilder.Entity<SolicitacaoServico>()
                .Property(x => x.NumeroProtocolo)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed)
                .IsOptional();

            modelBuilder.Entity<CicloLetivo>()
                .Property(x => x.AnoNumeroCicloLetivo)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);

            modelBuilder.Entity<ArquivoAnexado>()
                .Property(x => x.UidArquivo)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);
        }

        protected override void Configure(SMCDbContextConfiguration configuration)
        {
            #region Filtros de dados

            configuration.Filter<InstituicaoEnsino>(FILTER.INSTITUICAO_ENSINO)
                         .Associated<TipoHierarquiaEntidade, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<InstituicaoNivel, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<Entidade, long>(i => (long)i.SeqInstituicaoEnsino)
                         .Associated<CicloLetivo, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<InstituicaoTipoEntidade, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<Pessoa, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<InstituicaoNivelModalidade, long>(i => i.InstituicaoNivel.SeqInstituicaoEnsino)
                         .Associated<TipoAgenda, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<InstituicaoTipoEvento, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<HierarquiaEntidade, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<Programa, long>(i => (long)i.SeqInstituicaoEnsino)
                         .Associated<Beneficio, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<CondicaoObrigatoriedade, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<InstituicaoNivelTipoComponenteCurricular, long>(i => i.InstituicaoNivel.SeqInstituicaoEnsino)
                         .Associated<InstituicaoNivelRegimeLetivo, long>(i => i.InstituicaoNivel.SeqInstituicaoEnsino)
                         .Associated<InstituicaoNivelTipoDivisaoCurricular, long>(i => i.InstituicaoNivel.SeqInstituicaoEnsino)
                         .Associated<InstituicaoNivelTipoGrupoCurricular, long>(i => i.InstituicaoNivel.SeqInstituicaoEnsino)
                         .Associated<ComponenteCurricular, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<Servico, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<InstituicaoNivelTipoAtividadeColaborador, long>(i => i.InstituicaoNivel.SeqInstituicaoEnsino)
                         .Associated<Processo, long>(i => i.Servico.SeqInstituicaoEnsino)
                         .Associated<InstituicaoMotivoBloqueio, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<InstituicaoNivelTipoVinculoAluno, long>(i => i.InstituicaoNivel.SeqInstituicaoEnsino)
                         .Associated<InstituicaoNivelTipoMensagem, long>(i => i.InstituicaoNivel.SeqInstituicaoEnsino)
                         .Associated<InstituicaoNivelTipoTermoIntercambio, long>(i => i.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqInstituicaoEnsino)
                         .Associated<InstituicaoNivelSituacaoMatricula, long>(i => i.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqInstituicaoEnsino)
                         .Associated<ParceriaIntercambio, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<Convocacao, long>(i => (long)i.ProcessoSeletivo.Campanha.EntidadeResponsavel.SeqInstituicaoEnsino)
                         .Associated<Contrato, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<Ingressante, long>(i => i.Pessoa.SeqInstituicaoEnsino)
                         .Associated<Colaborador, long>(i => i.Pessoa.SeqInstituicaoEnsino)
                         .Associated<PessoaAtuacao, long>(i => i.Pessoa.SeqInstituicaoEnsino)
                         .Associated<InstituicaoNivelTipoTrabalho, long>(i => i.InstituicaoNivel.SeqInstituicaoEnsino)
                         .Associated<InstituicaoNivelTipoMembroBanca, long>(i => i.InstituicaoNivel.SeqInstituicaoEnsino)
                         .Associated<InstituicaoNivelTipoTurma, long>(i => i.InstituicaoNivel.SeqInstituicaoEnsino)
                         .Associated<Turma, long>(i => i.CicloLetivoInicio.SeqInstituicaoEnsino)
                         .Associated<InstituicaoNivelTipoOrientacao, long>(i => i.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqInstituicaoEnsino)
                         .Associated<Aluno, long>(i => i.Pessoa.SeqInstituicaoEnsino)
                         .Associated<InstituicaoTipoEntidadeVinculoColaborador, long>(i => i.InstituicaoTipoEntidade.SeqInstituicaoEnsino)
                         .Associated<ViewCentralSolicitacaoServico, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<SolicitacaoServico, long>(i => i.PessoaAtuacao.Pessoa.SeqInstituicaoEnsino)
                         .Associated<ViewAluno, long>(i => i.SeqEntidadeInstituicao)
                         .Associated<DocumentoConclusao, long>(i => i.Aluno.Pessoa.SeqInstituicaoEnsino)
                         .IgnoredProperty<Ingressante, Entidade>(i => i.EntidadeResponsavel)
                         .Associated<AtoNormativo, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<TipoApostilamento, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<TipoDocumentoAcademico, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<InstituicaoNivelTipoDocumentoAcademico, long>(i => i.InstituicaoNivel.SeqInstituicaoEnsino)
                         .Associated<OrgaoRegistro, long>(i => i.SeqInstituicaoEnsino)
                         .Associated<DeclaracaoGenerica, long>(i => i.PessoaAtuacao.Pessoa.SeqInstituicaoEnsino);

            configuration.Filter<NivelEnsino>(FILTER.NIVEL_ENSINO, true)
                         .IgnoredProperty<NivelEnsino, long?>(i => i.SeqNivelEnsinoSuperior)
                         // Filtros de dados
                         .Associated<Curso, long>(i => i.SeqNivelEnsino)
                         .Associated<ComponenteCurricularNivelEnsino, long>(i => i.SeqNivelEnsino)
                         .Associated<InstituicaoNivel, long>(i => i.SeqNivelEnsino) // Usado apenas pelo papel Administrador Geral, entretanto, configurei o filtro de dados
                         .Associated<DivisaoCurricular, long>(i => i.SeqNivelEnsino) // Usado apenas pelo papel Administrador Geral, entretanto, configurei o filtro de dados
                         .Associated<ComponenteCurricular, ComponenteCurricularCustomFilter>() // Problema ao editar quando não tem acesso ao nivel de ensino.
                         .Associated<CicloLetivo, CicloLetivoCustomFilter>() // Problema ao editar quando não tem acesso ao nivel de ensino.
                         .Associated<GrauAcademico, GrauAcademicoCustomFilter>()
                         .Associated<ConfiguracaoProcessoNivelEnsino, long>(i => i.SeqNivelEnsino)
                         .Associated<InstituicaoNivelServico, long>(i => i.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqNivelEnsino)
                         .Associated<InstituicaoNivelTipoAtividadeColaborador, long>(i => i.InstituicaoNivel.SeqNivelEnsino)
                         .Associated<InstituicaoNivelBeneficio, long>(i => i.InstituicaoNivel.SeqNivelEnsino)
                         .Associated<InstituicaoNivelTipoVinculoAluno, long>(i => i.InstituicaoNivel.SeqNivelEnsino)
                         .Associated<InstituicaoNivelTipoMensagem, long>(i => i.InstituicaoNivel.SeqNivelEnsino)
                         .Associated<Ingressante, long>(i => i.SeqNivelEnsino)
                         .Associated<ColaboradorVinculo, long>(i => i.Cursos.FirstOrDefault().CursoOfertaLocalidade.CursoOferta.Curso.SeqNivelEnsino)
                         .Associated<InstituicaoNivelTipoTurma, long>(i => i.InstituicaoNivel.SeqNivelEnsino)
                         .Associated<Turma, TurmaNivelEnsinoCustomFilter>()

                         // Problemas do filtro de dados
                         .Associated<CursoOferta, long>(i => i.Curso.SeqNivelEnsino)
                         .Associated<InstituicaoNivelModalidade, long>(i => i.InstituicaoNivel.SeqNivelEnsino)
                         .Associated<InstituicaoNivelTipoGrupoCurricular, long>(i => i.InstituicaoNivel.SeqNivelEnsino)
                         .Associated<CursoOfertaLocalidade, long>(i => i.CursoOferta.Curso.SeqNivelEnsino)
                         .Associated<CursoOfertaLocalidadeFormacao, long>(i => i.CursoOfertaLocalidade.CursoOferta.Curso.SeqNivelEnsino)
                         .Associated<CursoOfertaLocalidadeTurno, long>(i => i.CursoOfertaLocalidade.CursoOferta.Curso.SeqNivelEnsino)
                         .Associated<MatrizCurricular, long>(i => i.CurriculoCursoOferta.CursoOferta.Curso.SeqNivelEnsino)
                         .Associated<MatrizCurricularOferta, long>(i => i.MatrizCurricular.CurriculoCursoOferta.CursoOferta.Curso.SeqNivelEnsino)
                         .Associated<InstituicaoNivelTipoTermoIntercambio, long>(i => i.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqNivelEnsino)
                         .Associated<InstituicaoNivelSituacaoMatricula, long>(i => i.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqNivelEnsino)
                         .Associated<InstituicaoNivelTipoOrientacao, long>(i => i.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqNivelEnsino)
                         .Associated<Aluno, long>(i => i.Historicos.FirstOrDefault(f => f.Atual).SeqNivelEnsino)
                         .Associated<Orientacao, long>(i => i.SeqNivelEnsino)
                         .Associated<PessoaAtuacao, PessoaAtuacaoNivelEnsinoCustomFilter>()
                         .Associated<ViewAluno, long>(i => i.SeqNivelEnsino);

            configuration.Filter<HierarquiaEntidadeItem>(FILTER.HIERARQUIA_ENTIDADE_LOCALIDADES, true, true)
                        .Associated<CursoOfertaLocalidade, CursoOfertaLocalidadeCustomFilter>()
                        .Associated<Turma, TurmaLocalidadeCustomFilter>()
                        .IgnoredProperty<InstituicaoEnsino, Entidade>(i => i.InstituicaoEnsino)
                        .IgnoredProperty<Entidade, Entidade>(i => i.InstituicaoEnsino)
                        .IgnoredProperty<Curso, Entidade>(i => i.InstituicaoEnsino)
                        .IgnoredProperty<CursoUnidade, Entidade>(i => i.InstituicaoEnsino)
                        .IgnoredProperty<CursoOfertaLocalidade, Entidade>(i => i.InstituicaoEnsino)
                        .IgnoredProperty<Programa, Entidade>(i => i.InstituicaoEnsino)
                        .IgnoredProperty<DivisaoTurma, Entidade>(i => i.Localidade);

            configuration.Filter<HierarquiaEntidadeItem>(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL, true, true)
                        .Associated<Curso, CursoCustomFilter>()
                        .Associated<CursoOfertaLocalidadeTurno, CursoOfertaLocalidadeTurnoCustomFilter>()
                        .Associated<Entidade, EntidadeCustomFilter>()
                        .Associated<Convocacao, ConvocacaoCustomFilter>()
                        .Associated<CampanhaOferta, CampanhaOfertaCustomFilter>()
                        .Associated<Ingressante, IngressanteEntidadeResponsavelCustomFilter>()
                        .Associated<Turma, TurmaEntidadeResponsavelCustomFilter>()
                        .Associated<Aluno, AlunoEntidadeResponsavelCustomFilter>()
                        .Associated<TrabalhoAcademico, TrabalhoAcademicoCustomFilter>()
                        .Associated<OrientacaoPessoaAtuacao, OrientacaoPessoaAtuacaoCustomFilter>()
                        .Associated<ViewCentralSolicitacaoServico, ViewCentralSolicitacaoServicoCustomFilter>()
                        .Associated<SolicitacaoServico, SolicitacaoServicoCustomFilter>()
                        .Associated<SolicitacaoMatricula, SolicitacaoMatriculaCustomFilter>()
                        .Associated<PessoaAtuacao, PessoaAtuacaoEntidadeResponsavelCustomFilter>()
                        .Associated<ProcessoEtapaConfiguracaoNotificacao, ProcessoEtapaConfiguracaoNotificacaoCustomFilter>()
                        .Associated<Processo, ProcessoCustomFilter>()
                        .Associated<ProcessoUnidadeResponsavel, ProcessoUnidadeResponsavelCustomFilter>()
                        .Associated<SolicitacaoHistoricoUsuarioResponsavel, SolicitacaoHistoricoUsuarioResponsavelCustomFilter>()
                        .Associated<ViewAluno, ViewAlunoEntidadeResponsavelCustomFilter>()
                        .Associated<PublicacaoBdp, PublicacaoBdpCustomFilter>()
                        .Associated<MatrizCurricularOferta, MatrizCurricularOfertaCustomFilter>()
                        .Associated<DocumentoConclusao, DocumentoConclusaoEntidadeResponsavelCustomFilter>()
                        .Associated<EnvioNotificacao, EnvioNotificacaoEntidadeResponsavelCustomFilter>()
                        .Associated<DeclaracaoGenerica, DeclaracaoGenericaCustomFilter>()
                        // Filtro de dados de colaborador foi implementado e retirado no bug 81064
                        // Verificar histórico para enteder os motivos. 
                        // Caso habilitar, necessário realizar teste em toda a aplicação!
                        //.Associated<Colaborador, ColaboradorEntidadeResponsavelCustomFilter>()
                        .IgnoredProperty<InstituicaoEnsino, Entidade>(i => i.InstituicaoEnsino)
                        .IgnoredProperty<Entidade, Entidade>(i => i.InstituicaoEnsino)
                        .IgnoredProperty<Curso, Entidade>(i => i.InstituicaoEnsino)
                        .IgnoredProperty<CursoUnidade, Entidade>(i => i.InstituicaoEnsino)
                        .IgnoredProperty<CursoOfertaLocalidade, Entidade>(i => i.InstituicaoEnsino)
                        .IgnoredProperty<CursoOfertaLocalidade, IList<HierarquiaEntidadeItem>>(i => i.HierarquiasEntidades)
                        .IgnoredProperty<Programa, Entidade>(i => i.InstituicaoEnsino)
                        .IgnoredProperty<SolicitacaoServico, Entidade>(i => i.EntidadeResponsavel)
                        .IgnoredProperty<SolicitacaoServico, Entidade>(i => i.EntidadeCompartilhada)
                        .IgnoredProperty<SolicitacaoMatricula, Entidade>(i => i.EntidadeResponsavel)
                        .IgnoredProperty<SolicitacaoMatricula, Entidade>(i => i.EntidadeCompartilhada);

            configuration.Filter<HierarquiaEntidadeItem>(FILTER.HIERARQUIA_ENTIDADE_UNIDADE_GESTORA, true, true)
                        .Associated<CursoUnidade, CursoUnidadeCustomFilter>()
                        .IgnoredProperty<InstituicaoEnsino, Entidade>(i => i.InstituicaoEnsino)
                        .IgnoredProperty<Entidade, Entidade>(i => i.InstituicaoEnsino)
                        .IgnoredProperty<Curso, Entidade>(i => i.InstituicaoEnsino)
                        .IgnoredProperty<CursoUnidade, Entidade>(i => i.InstituicaoEnsino)
                        .IgnoredProperty<CursoOfertaLocalidade, Entidade>(i => i.InstituicaoEnsino)
                        .IgnoredProperty<Programa, Entidade>(i => i.InstituicaoEnsino);

            //configuration.Filter<HierarquiaEntidadeItem>(FILTER.HIERARQUIA_ENTIDADE, x => x.Entidade.Nome, x => x.SeqItemSuperior);
            // Filtros de dados;

            #endregion Filtros de dados

            #region ALN

            configuration.Register<TipoVinculoAluno>()
                .OwnedCollection<FormaIngresso>(x => x.FormasIngresso);

            configuration.Register<ParceriaIntercambio>()
                .OwnedCollection<ParceriaIntercambioInstituicaoExterna>(x => x.InstituicoesExternas)
                .OwnedCollection<ParceriaIntercambioTipoTermo>(x => x.TiposTermo)
                .OwnedCollection<ParceriaIntercambioVigencia>(x => x.Vigencias)
                .OwnedCollection<ParceriaIntercambioArquivo>(x => x.Arquivos);

            configuration.Register<ParceriaIntercambioArquivo>()
                .OwnedEntity<ArquivoAnexado>(x => x.ArquivoAnexado);

            configuration.Register<TermoIntercambio>()
                .OwnedCollection<TermoIntercambioArquivo>(x => x.Arquivos)
                .OwnedCollection<TermoIntercambioTipoMobilidade>(x => x.TiposMobilidade)
                .OwnedCollection<TermoIntercambioVigencia>(x => x.Vigencias);

            configuration.Register<TermoIntercambioTipoMobilidade>()
               .OwnedCollection<TermoIntercambioPessoa>(x => x.Pessoas);

            configuration.Register<TermoIntercambioArquivo>()
              .OwnedEntity<ArquivoAnexado>(x => x.ArquivoAnexado);

            configuration.Register<InstituicaoNivelTipoVinculoAluno>()
               .OwnedCollection<InstituicaoNivelSituacaoMatricula>(x => x.SituacoesMatricula)
               .OwnedCollection<InstituicaoNivelFormaIngresso>(x => x.FormasIngresso)
               .OwnedCollection<InstituicaoNivelTipoTermoIntercambio>(x => x.TiposTermoIntercambio);

            configuration.Register<InstituicaoNivelFormaIngresso>()
              .OwnedCollection<InstituicaoNivelTipoProcessoSeletivo>(x => x.TiposProcessoSeletivo)
              .AssociatedEntity<FormaIngresso>(x => x.FormaIngresso);

            configuration.Register<InstituicaoNivelTipoOrientacao>()
               .OwnedCollection<InstituicaoNivelTipoOrientacaoParticipacao>(x => x.TiposParticipacao);

            configuration.Register<InstituicaoNivelTipoTermoIntercambio>()
               .AssociatedEntity<TipoTermoIntercambio>(x => x.TipoTermoIntercambio);

            configuration.Register<Ingressante>()
               .OwnedCollection(x => x.FormacoesEspecificas)
               .OwnedCollection(x => x.HistoricosSituacao)
               .OwnedCollection(x => x.Ofertas)
               .OwnedCollection(x => x.TermosIntercambio)
               .OwnedCollection(x => x.OrientacoesPessoaAtuacao)
               .OwnedCollection(x => x.CondicoesObrigatoriedade)
               .OwnedCollection(x => x.Enderecos)
               .OwnedEntity(x => x.DadosPessoais)
               .AssociatedCollection(x => x.EnderecosEletronicos)
               .AssociatedCollection(x => x.Telefones)
               .AssociatedEntity(x => x.Pessoa);

            configuration.Register<Aluno>()
                .OwnedCollection(x => x.Enderecos)
                .AssociatedEntity(x => x.Telefones)
                .AssociatedEntity(x => x.EnderecosEletronicos)
                .AssociatedEntity(x => x.DadosPessoais)
                .AssociatedEntity(x => x.Pessoa)
                .OwnedCollection(x => x.ReferenciasFamiliar)
                .OwnedCollection(x => x.Historicos)
                .OwnedCollection(x => x.Beneficios)
                .OwnedCollection(x => x.Bloqueios)
                .OwnedCollection(x => x.TermosIntercambio)
                .OwnedCollection(x => x.OrientacoesPessoaAtuacao)
                .OwnedCollection(x => x.CondicoesObrigatoriedade);

            configuration.Register<AlunoHistorico>()
                .OwnedCollection(x => x.Formacoes)
                .OwnedCollection(x => x.HistoricosCicloLetivo)
                .AssociatedEntity(x => x.Aluno)
                .AssociatedEntity(x => x.CicloLetivo)
                .AssociatedEntity(x => x.CursoOfertaLocalidadeTurno)
                .AssociatedEntity(x => x.Ingressante)
                .AssociatedEntity(x => x.NivelEnsino)
                .AssociatedEntity(x => x.SolicitacaoServico)
                .AssociatedEntity(x => x.PrevisoesConclusao);

            configuration.Register<AlunoHistoricoCicloLetivo>()
                .OwnedCollection(x => x.AlunoHistoricoSituacao)
                .OwnedCollection(x => x.PlanosEstudo)
                .AssociatedEntity(x => x.AlunoHistorico)
                .AssociatedEntity(x => x.CicloLetivo)
                .AssociatedEntity(x => x.SolicitacaoServico);

            configuration.Register<PlanoEstudo>()
                .OwnedCollection(x => x.Itens)
                .AssociatedEntity(x => x.AlunoHistoricoCicloLetivo)
                .AssociatedEntity(x => x.MatrizCurricularOferta);

            configuration.Register<PlanoEstudoItem>()
                .OwnedEntity(x => x.Orientacao, SchemeMapping.Insert);

            configuration.Register<AlunoFormacao>()
                .OwnedCollection(x => x.ApuracoesFormacao)
                .AssociatedEntity(x => x.AlunoHistorico)
                .AssociatedEntity(x => x.FormacaoEspecifica);

            configuration.Register<PessoaAtuacaoTermoIntercambio>()
                .OwnedCollection(x => x.Arquivos)
                .OwnedCollection(x => x.Periodos)
                .AssociatedEntity(x => x.Orientacao)
                .AssociatedEntity(x => x.PessoaAtuacao)
                .AssociatedEntity(x => x.TermoIntercambio);

            configuration.Register<ApuracaoAvaliacao>()
                .OwnedEntity(x => x.ArquivoAnexadoAtaDefesa);

            configuration.Register<PessoaAtuacaoTermoIntercambioArquivo>()
                .OwnedEntity(x => x.ArquivoAnexado)
                .AssociatedEntity(x => x.PessoaAtuacaoTermoIntercambio);

            configuration.Register<AlunoHistoricoSituacao>()
                .OwnedEntity(x => x.ArquivoAnexado);

            #endregion ALN

            #region APR

            configuration.Register<EscalaApuracao>()
                .OwnedCollection(x => x.Itens);

            configuration.Register<Material>()
                .OwnedEntity(x => x.ArquivoAnexado)
                .OwnedCollection(x => x.Visualizacoes);

            configuration.Register<HistoricoEscolar>()
                .OwnedCollection(x => x.Colaboradores);

            configuration.Register<Avaliacao>()
                .OwnedCollection(x => x.AplicacoesAvaliacao)
                .OwnedEntity(x => x.ArquivoAnexadoInstrucao);

            configuration.Register<AplicacaoAvaliacao>()
                .OwnedCollection(x => x.MembrosBancaExaminadora)
                .OwnedCollection(x => x.ApuracoesAvaliacao);

            configuration.Register<Aula>()
                .OwnedCollection(x => x.ApuracoesFrequencia);

            configuration.Register<OrigemAvaliacao>()
                .OwnedCollection(x => x.AplicacoesAvaliacao)
                .OwnedCollection(x => x.HistoricosEscolares);

            configuration.Register<EntregaOnline>()
                .OwnedCollection(x => x.Participantes)
                .OwnedCollection(x => x.HistoricosSituacao)
                .OwnedEntity(x => x.ArquivoAnexado);

            #endregion APR

            #region CAM

            // Ciclo Letivo
            configuration.Register<CicloLetivo>()
                .AssociatedCollection(x => x.NiveisEnsino)
                .OwnedCollection(x => x.TiposEvento);

            //Ciclo Letivo Tipo Evento
            configuration.Register<CicloLetivoTipoEvento>()
                .OwnedCollection(x => x.Parametros)
                .AssociatedCollection(x => x.NiveisEnsino);

            //Instituição Tipo Evento
            configuration.Register<InstituicaoTipoEvento>()
                .OwnedCollection(x => x.Parametros);

            //Tipo de Oferta
            configuration.Register<TipoOferta>()
                .OwnedCollection(x => x.UnidadesResponsaveis);

            //Tipo de Processo Seletivo
            configuration.Register<TipoProcessoSeletivo>()
                .OwnedCollection(x => x.TiposOferta)
                .OwnedCollection(x => x.TiposProcessosGpi);

            // Convocado
            configuration.Register<Convocado>()
                .OwnedCollection(x => x.Ofertas);

            // Convocação
            configuration.Register<Convocacao>()
                .OwnedCollection(x => x.Chamadas);

            // Campanha
            configuration.Register<Campanha>()
                .OwnedCollection(x => x.CiclosLetivos);

            // ProcessoSeletivo
            configuration.Register<ProcessoSeletivo>()
                .OwnedCollection(x => x.NiveisEnsino)
                .OwnedCollection(x => x.ProcessosMatricula);

            #endregion CAM

            #region CNC

            configuration.Register<Titulacao>()
                .OwnedCollection(x => x.DocumentosComprobatorios);

            configuration.Register<TipoDocumentoAcademico>()
                .OwnedCollection(x => x.Tags)
                .OwnedCollection(x => x.Servicos);

            configuration.Register<InstituicaoNivelTipoDocumentoAcademico>()
               .OwnedCollection(x => x.ModelosRelatorio)
               .OwnedCollection(x => x.FormacoesEspecificas);

            configuration.Register<InstituicaoNivelTipoDocumentoModeloRelatorio>()
               .OwnedEntity<ArquivoAnexado>(x => x.ArquivoModelo);

            configuration.Register<DocumentoConclusaoApostilamento>()
               .OwnedCollection(x => x.DocumentoConclusaoFormacao)
               .OwnedEntity<ArquivoAnexado>(x => x.ArquivoAnexado);

            configuration.Register<DocumentoConclusao>()
               .OwnedCollection(x => x.FormacoesEspecificas)
               .OwnedCollection(x => x.Situacoes);

            configuration.Register<SituacaoDocumentoAcademico>()
                .OwnedCollection(x => x.GruposDocumento);

            configuration.Register<DeclaracaoGenerica>()
                .OwnedCollection(x => x.Situacoes);

            #endregion CNC

            #region CSO

            // Hierarquia Classificação
            configuration.Register<HierarquiaClassificacao>()
                .OwnedCollection<Classificacao>(x => x.Classificacoes);

            // Tipo Hierarquia Classificação
            configuration.Register<TipoHierarquiaClassificacao>()
                .OwnedCollection<TipoClassificacao>(x => x.TiposClassificacao);

            // Tipo Classificação
            configuration.Register<TipoClassificacao>()
                .OwnedCollection<TipoClassificacao>(x => x.TiposClassificacaoFilhas);

            // Grau academico
            configuration.Register<GrauAcademico>()
                .AssociatedCollection<NivelEnsino>(x => x.NiveisEnsino);

            // Programa
            configuration.Register<Programa>()
                .OwnedCollection<Telefone>(x => x.Telefones)
                .OwnedCollection<Endereco>(x => x.Enderecos)
                .OwnedCollection<EnderecoEletronico>(x => x.EnderecosEletronicos)
                .OwnedCollection<EntidadeClassificacao>(x => x.Classificacoes)
                .OwnedCollection<EntidadeIdioma>(x => x.DadosOutrosIdiomas)
                .OwnedCollection<EntidadeHistoricoSituacao>(x => x.HistoricoSituacoes)
                .OwnedCollection<HierarquiaEntidadeItem>(x => x.HierarquiasEntidades)
                .OwnedCollection<ProgramaHistoricoNota>(x => x.HistoricoNotas)
                .OwnedEntity<ArquivoAnexado>(x => x.ArquivoLogotipo)
                .OwnedCollection<ProgramaTipoAutorizacaoBdp>(x => x.TiposAutorizacaoBdp);

            // ProgramaProposta
            configuration.Register<ProgramaProposta>()
                .OwnedEntity<DadoFormulario>(x => x.DadoFormulario);

            // Curso
            configuration.Register<Curso>()
                .OwnedCollection<Telefone>(x => x.Telefones)
                .OwnedCollection<Endereco>(x => x.Enderecos)
                .OwnedCollection<EnderecoEletronico>(x => x.EnderecosEletronicos)
                .OwnedCollection<EntidadeClassificacao>(x => x.Classificacoes)
                .OwnedCollection<EntidadeHistoricoSituacao>(x => x.HistoricoSituacoes)
                .OwnedCollection<HierarquiaEntidadeItem>(x => x.HierarquiasEntidades)
                .OwnedEntity<ArquivoAnexado>(x => x.ArquivoLogotipo)
                .OwnedCollection<FormacaoEspecifica>(x => x.FormacoesEspecificasEntidade)
                .OwnedCollection<CursoFormacaoEspecifica>(x => x.CursosFormacaoEspecifica)
                .OwnedCollection<CursoOferta>(x => x.CursosOferta);

            // CursoUnidade
            configuration.Register<CursoUnidade>()
                .OwnedCollection<Telefone>(x => x.Telefones)
                .OwnedCollection<Endereco>(x => x.Enderecos)
                .OwnedCollection<EnderecoEletronico>(x => x.EnderecosEletronicos)
                .OwnedCollection<EntidadeClassificacao>(x => x.Classificacoes)
                .OwnedCollection<EntidadeHistoricoSituacao>(x => x.HistoricoSituacoes)
                .OwnedCollection<HierarquiaEntidadeItem>(x => x.HierarquiasEntidades)
                .OwnedEntity<ArquivoAnexado>(x => x.ArquivoLogotipo);

            // CursoOfertaLocalidade
            configuration.Register<CursoOfertaLocalidade>()
                .OwnedCollection<Telefone>(x => x.Telefones)
                .OwnedCollection<Endereco>(x => x.Enderecos)
                .OwnedCollection<EnderecoEletronico>(x => x.EnderecosEletronicos)
                .OwnedCollection<EntidadeClassificacao>(x => x.Classificacoes)
                .OwnedCollection<EntidadeIdioma>(x => x.DadosOutrosIdiomas)
                .OwnedCollection<EntidadeHistoricoSituacao>(x => x.HistoricoSituacoes)
                .OwnedCollection<HierarquiaEntidadeItem>(x => x.HierarquiasEntidades)
                .OwnedCollection<CursoOfertaLocalidadeFormacao>(x => x.FormacoesEspecificas)
                .OwnedCollection<CursoOfertaLocalidadeTurno>(x => x.Turnos)
                .OwnedEntity<ArquivoAnexado>(x => x.ArquivoLogotipo);

            // CursoFormacaoEspecifica
            configuration.Register<CursoFormacaoEspecifica>()
               .OwnedCollection(x => x.Titulacoes)
               .AssociatedEntity(x => x.FormacaoEspecifica);

            // InstituicaoTipoEntidadeFormacaoEspecifica
            configuration.Register<InstituicaoTipoEntidadeFormacaoEspecifica>()
                .OwnedCollection<InstituicaoTipoEntidadeFormacaoEspecifica>(x => x.TiposFormacaoEspecificasFilhas);

            // FormacaoEspecifica
            configuration.Register<FormacaoEspecifica>()
                .OwnedCollection(x => x.FormacoesEspecificasFilhas);

            // InstituicaoNivelModadliade
            configuration.Register<InstituicaoNivelModalidade>()
                .OwnedCollection(x => x.TiposEntidadeLocalidade);

            // TipoFormacaoEspecifica
            configuration.Register<TipoFormacaoEspecifica>()
                .OwnedCollection(x => x.TiposCurso);

            #endregion CSO

            #region CUR

            configuration.Register<TipoComponenteCurricular>()
                .OwnedCollection(x => x.TiposDivisao);

            configuration.Register<InstituicaoNivelTipoComponenteCurricular>()
                .OwnedCollection<InstituicaoNivelTipoDivisaoComponente>(x => x.TiposDivisaoComponente);

            configuration.Register<ComponenteCurricular>()
                .OwnedCollection<ComponenteCurricularOrgaoRegulador>(x => x.OrgaosReguladores)
                .OwnedCollection<ComponenteCurricularEmenta>(x => x.Ementas)
                .OwnedCollection<ComponenteCurricularEntidade>(x => x.EntidadesResponsaveis)
                .OwnedCollection<ComponenteCurricularNivelEnsino>(x => x.NiveisEnsino)
                .OwnedCollection<ComponenteCurricularOrganizacao>(x => x.Organizacoes)
                .OwnedCollection<ConfiguracaoComponente>(x => x.Configuracoes);

            configuration.Register<ConfiguracaoComponente>()
                .OwnedCollection<DivisaoComponente>(x => x.DivisoesComponente);

            configuration.Register<GrupoConfiguracaoComponente>()
              .OwnedCollection<GrupoConfiguracaoComponenteItem>(x => x.Itens);

            configuration.Register<TipoConfiguracaoGrupoCurricular>()
                .AssociatedCollection<TipoConfiguracaoGrupoCurricular>(x => x.TiposConfiguracoesGrupoCurricularFilhos);

            configuration.Register<DivisaoCurricular>()
                .OwnedCollection<DivisaoCurricularItem>(x => x.Itens);

            configuration.Register<TipoGrupoCurricular>()
                .AssociatedCollection<TipoComponenteCurricular>(x => x.TiposComponenteCurricular);

            configuration.Register<Curriculo>()
                .OwnedCollection<CurriculoCursoOferta>(x => x.CursosOferta);

            configuration.Register<MatrizCurricular>()
              .OwnedCollection<MatrizCurricularOferta>(x => x.Ofertas)
              .OwnedCollection<DivisaoMatrizCurricular>(x => x.DivisoesMatrizCurricular);

            configuration.Register<MatrizCurricularOferta>()
                .OwnedCollection<MatrizCurricularOfertaExcecaoLocalidade>(x => x.ExcecoesLocalidade)
                .OwnedCollection<HistoricoSituacaoMatrizCurricularOferta>(x => x.HistoricosSituacao);

            configuration.Register<DivisaoMatrizCurricularComponente>()
              .OwnedCollection<MatrizCurricularDivisaoComponente>(x => x.DivisoesComponente)
              .AssociatedCollection<ComponenteCurricular>(x => x.ComponentesCurricularSubstitutos)
              .AssociatedCollection<Turno>(x => x.TurnosExcecao);

            configuration.Register<Classificacao>()
                .OwnedCollection<Classificacao>(x => x.ClassificacoesFilhas);

            configuration.Register<Requisito>()
               .OwnedCollection<RequisitoItem>(x => x.Itens)
               .OwnedCollection<MatrizCurricularRequisito>(x => x.MatrizesCurriculares);

            configuration.Register<Dispensa>()
             .OwnedEntity<DispensaGrupo>(x => x.GrupoOrigem)
            // .OwnedCollection<DispensaGrupoComponente>(x=> x.GrupoOrigem.Componentes)
             .OwnedEntity<DispensaGrupo>(x => x.GrupoDispensado)
            // .OwnedCollection<DispensaGrupoComponente>(x => x.GrupoDispensado.Componentes)
             .OwnedCollection<DispensaHistoricoVigencia>(x => x.HistoricosVigencia)
             .OwnedCollection<DispensaMatrizExcecao>(x => x.MatrizesExcecao);

            configuration.Register<DispensaGrupo>()
             .OwnedCollection<DispensaGrupoComponente>(x => x.Componentes);

            configuration.Register<GrupoCurricular>()
                .AssociatedCollection<Beneficio>(x => x.Beneficios)
                .AssociatedCollection<CondicaoObrigatoriedade>(x => x.CondicoesObrigatoriedade)
                .OwnedCollection<GrupoCurricular>(x => x.GruposCurricularesFilhos)
                .OwnedCollection<GrupoCurricularComponente>(x => x.ComponentesCurriculares);

            configuration.Register<ClassificacaoPeriodico>()
                .OwnedCollection<Periodico>(x => x.Periodicos);

            configuration.Register<Periodico>()
                .OwnedCollection<QualisPeriodico>(x => x.QualisPeriodico);

            configuration.Register<GrupoCurricularComponente>()
                .OwnedCollection<DivisaoMatrizCurricularComponente>(x => x.DivisoesMatrizCurricularComponente);

            configuration.Register<DocumentoAcademicoCurriculo>()
                .OwnedCollection(x => x.Situacoes);

            #endregion CUR

            #region DCT

            configuration.Register<Colaborador>()
                .AssociatedCollection(x => x.EnderecosEletronicos)
                .AssociatedCollection(x => x.Telefones)
                .OwnedCollection(x => x.Enderecos)
                .OwnedCollection(x => x.InstituicoesExternas)
                .OwnedCollection(x => x.Vinculos)
                .OwnedCollection(x => x.FormacoesAcademicas);

            configuration.Register<ColaboradorVinculo>()
                .OwnedCollection(x => x.ColaboradoresResponsaveis)
                .OwnedCollection(x => x.Cursos)
                .OwnedCollection(x => x.FormacoesEspecificas);

            configuration.Register<ColaboradorVinculoCurso>()
                .OwnedCollection(x => x.Atividades);

            configuration.Register<ColaboradorAptoComponente>()
                .AssociatedEntity<ComponenteCurricular>(x => x.ComponenteCurricular)
                .AssociatedEntity<Colaborador>(x => x.Colaborador);

            #endregion DCT

            #region FIN

            configuration.Register<PessoaJuridica>()
                .OwnedCollection<Telefone>(x => x.Telefones)
                .OwnedCollection<Endereco>(x => x.Enderecos)
                .OwnedCollection<EnderecoEletronico>(x => x.EnderecosEletronicos);

            configuration.Register<Beneficio>()
                .AssociatedCollection<PessoaJuridica>(x => x.ResponsaveisFinanceiros);

            configuration.Register<InstituicaoNivelBeneficio>()
                .OwnedCollection<ConfiguracaoBeneficio>(x => x.ConfiguracoesBeneficio);

            configuration.Register<Contrato>()
                .OwnedEntity<ArquivoAnexado>(x => x.ArquivoAnexado)
                .OwnedCollection<ContratoNivelEnsino>(x => x.NiveisEnsino)
                .OwnedCollection<ContratoCurso>(x => x.Cursos);

            configuration.Register<PessoaAtuacaoBeneficio>()
                .OwnedCollection<PessoaAtuacaoBeneficioResponsavelFinanceiro>(x => x.ResponsaveisFinanceiro)
                .OwnedCollection<BeneficioHistoricoSituacao>(x => x.HistoricoSituacoes)
                .OwnedCollection<BeneficioHistoricoVigencia>(x => x.HistoricoVigencias)
                .OwnedCollection<PessoaAtuacaoBeneficioAnexo>(x => x.ArquivosAnexo);

            configuration.Register<PessoaAtuacaoBeneficioAnexo>()
                .OwnedEntity<ArquivoAnexado>(x => x.ArquivoAnexado);

            #endregion FIN

            #region ORG

            // Mantenedora
            configuration.Register<Mantenedora>()
                .OwnedCollection<Telefone>(x => x.Telefones)
                .OwnedCollection<Endereco>(x => x.Enderecos)
                .OwnedCollection<EnderecoEletronico>(x => x.EnderecosEletronicos)
                .OwnedEntity<ArquivoAnexado>(x => x.ArquivoLogotipo);

            // Entidade
            configuration.Register<Entidade>()
                .OwnedCollection<Telefone>(x => x.Telefones)
                .OwnedCollection<Endereco>(x => x.Enderecos)
                .OwnedCollection<EnderecoEletronico>(x => x.EnderecosEletronicos)
                .OwnedCollection<EntidadeHistoricoSituacao>(x => x.HistoricoSituacoes)
                .OwnedCollection<EntidadeClassificacao>(x => x.Classificacoes)
                .OwnedEntity<ArquivoAnexado>(x => x.ArquivoLogotipo);

            // Instituição de Ensino
            configuration.Register<InstituicaoEnsino>()
                .OwnedCollection<Telefone>(x => x.Telefones)
                .OwnedCollection<Endereco>(x => x.Enderecos)
                .OwnedCollection<EnderecoEletronico>(x => x.EnderecosEletronicos)
                .OwnedEntity<ArquivoAnexado>(x => x.ArquivoLogotipo);

            // Instituição Nivel Calendario
            configuration.Register<InstituicaoNivelCalendario>()
                .OwnedCollection<InstituicaoNivelTipoEvento>(x => x.TiposEvento);

            // Nível de Ensino
            configuration.Register<NivelEnsino>()
                .OwnedCollection<NivelEnsino>(x => x.NiveisEnsinoFilhos);

            // Tipo Hierarquia Entidade Item
            configuration.Register<TipoHierarquiaEntidadeItem>()
                .OwnedCollection<TipoHierarquiaEntidadeItem>(x => x.ItensFilhos);

            // Hierarquia de Entidade Item
            configuration.Register<HierarquiaEntidadeItem>()
                .OwnedCollection<HierarquiaEntidadeItem>(x => x.ItensFilhos);

            // Instituição Tipo de Entidade
            configuration.Register<InstituicaoTipoEntidade>()
                .OwnedCollection<InstituicaoTipoEntidadeEndereco>(x => x.TiposEndereco)
                .OwnedCollection<InstituicaoTipoEntidadeEnderecoEletronico>(x => x.TiposEnderecoEletronico)
                .OwnedCollection<InstituicaoTipoEntidadeTelefone>(x => x.TiposTelefone)
                .OwnedCollection<InstituicaoTipoEntidadeSituacao>(x => x.Situacoes);

            configuration.Register<InstituicaoTipoEntidadeSituacao>()
                .AssociatedEntity<SituacaoEntidade>(x => x.SituacaoEntidade);

            // Instituição Nível  Modelo Relatório
            configuration.Register<InstituicaoNivelModeloRelatorio>()
                    .OwnedEntity<ArquivoAnexado>(x => x.ArquivoModelo);

            // Instituição Modelo Relatório 
            configuration.Register<InstituicaoModeloRelatorio>()
                    .OwnedEntity<ArquivoAnexado>(x => x.ArquivoModelo);

            // Ato Normativo
            configuration.Register<AtoNormativo>()
                    .OwnedCollection<AtoNormativoEntidade>(x => x.Entidades)
                    .OwnedEntity<ArquivoAnexado>(x => x.ArquivoAnexado);

            configuration.Register<EntidadeImagem>()
                .OwnedEntity<ArquivoAnexado>(x => x.ArquivoAnexado);

            #endregion ORG

            #region ORT

            configuration.Register<Orientacao>()
                .OwnedCollection(x => x.OrientacoesColaborador)
                .OwnedCollection(x => x.OrientacoesPessoaAtuacao);

            ///comentado pois ocorrer stackoverflow quando se tenta trabalhar com orientação
            //configuration.Register<OrientacaoPessoaAtuacao>()
            //    .OwnedEntity(x => x.Orientacao, SchemeMapping.Insert);

            configuration.Register<TrabalhoAcademico>()
                .OwnedCollection(x => x.Autores)
                .OwnedCollection(x => x.DivisoesComponente)
                .OwnedCollection(x => x.PublicacaoBdp);

            configuration.Register<TrabalhoAcademicoDivisaoComponente>()
                            .OwnedEntity(x => x.OrigemAvaliacao);

            configuration.Register<PublicacaoBdp>()
                .OwnedCollection(x => x.HistoricoSituacoes)
                .OwnedCollection(x => x.InformacoesIdioma)
                .OwnedCollection(x => x.Arquivos);

            configuration.Register<PublicacaoBdpIdioma>()
                .OwnedCollection(x => x.PalavrasChave);

            configuration.Register<ConfiguracaoNumeracaoTrabalho>()
                .OwnedCollection(x => x.Ofertas);

            #endregion ORT

            #region PES

            configuration.Register<Pessoa>()
                .OwnedCollection(x => x.DadosPessoais)
                .OwnedCollection(x => x.Filiacao)
                .OwnedCollection(x => x.Enderecos)
                .OwnedCollection(x => x.EnderecosEletronicos)
                .OwnedCollection(x => x.Telefones);

            configuration.Register<PessoaAtuacaoEndereco>()
                .AssociatedEntity(x => x.PessoaEndereco);

            configuration.Register<PessoaDadosPessoais>()
                //.OwnedCollection(x => x.Atuacoes)
                .OwnedEntity(x => x.ArquivoFoto);

            configuration.Register<PessoaEnderecoEletronico>()
                .OwnedEntity(x => x.EnderecoEletronico);

            configuration.Register<PessoaEndereco>()
                .OwnedEntity(x => x.Endereco);

            configuration.Register<PessoaTelefone>()
                .OwnedEntity(x => x.Telefone);

            configuration.Register<ReferenciaFamiliar>()
                .OwnedCollection(x => x.Enderecos)
                .OwnedCollection(x => x.EnderecosEletronicos)
                .OwnedCollection(x => x.Telefones);

            configuration.Register<PessoaAtuacaoBloqueio>()
                .OwnedCollection(x => x.Comprovantes)
                .OwnedCollection(x => x.Itens);

            configuration.Register<PessoaAtuacaoBloqueioComprovante>()
                .OwnedEntity(x => x.ArquivoAnexado);

            configuration.Register<TipoMensagem>()
                .OwnedCollection(x => x.TiposAtuacao)
                .OwnedCollection(x => x.TiposUso)
                .OwnedCollection(x => x.Tags);

            configuration.Register<Mensagem>()
                .OwnedEntity(x => x.ArquivoAnexado)
                .AssociatedEntity(x => x.TipoMensagem);

            configuration.Register<FormacaoAcademica>()
                .OwnedCollection(x => x.DocumentosApresentados);

            configuration.Register<Funcionario>()
                .AssociatedCollection(x => x.EnderecosEletronicos)
                .AssociatedCollection(x => x.Telefones)
                .OwnedCollection(x => x.Enderecos)
                .OwnedCollection(x => x.Vinculos);

            configuration.Register<PessoaAtuacaoDocumento>()
                .OwnedEntity(x => x.ArquivoAnexado);

            #endregion PES

            #region MAT

            configuration.Register<TipoSituacaoMatricula>()
                .OwnedCollection<SituacaoMatricula>(x => x.SituacoesMatricula);

            configuration.Register<SolicitacaoMatricula>()
                .OwnedCollection(x => x.Itens)
                .OwnedCollection(x => x.DocumentosRequeridos)
                .OwnedCollection(x => x.Etapas)
                .OwnedCollection(x => x.AlunosHistoricosCicloLetivo, SchemeMapping.Insert)
                .OwnedEntity(x => x.ArquivoTermoAdesao)
                .AssociatedEntity(x => x.PessoaAtuacao);

            configuration.Register<SolicitacaoReaberturaMatricula>()
                .OwnedCollection(x => x.DocumentosRequeridos)
                .OwnedCollection(x => x.Etapas)
                .OwnedCollection(x => x.AlunosHistoricosCicloLetivo, SchemeMapping.Insert)
                .AssociatedEntity(x => x.PessoaAtuacao);

            configuration.Register<SolicitacaoMatriculaItem>()
                .OwnedCollection(x => x.HistoricosSituacao);

            #endregion MAT

            #region SRC

            configuration.Register<Processo>()
                .OwnedCollection<ConfiguracaoProcesso>(x => x.Configuracoes)
                .OwnedCollection<GrupoEscalonamento>(x => x.GruposEscalonamento)
                .OwnedCollection<ProcessoEtapa>(x => x.Etapas)
                .OwnedCollection<ProcessoUnidadeResponsavel>(x => x.UnidadesResponsaveis)
                .AssociatedEntity<Servico>(x => x.Servico);

            configuration.Register<ConfiguracaoProcesso>()
                 .OwnedCollection<ConfiguracaoEtapa>(x => x.ConfiguracoesEtapa)
                 .OwnedCollection<ConfiguracaoProcessoCurso>(x => x.Cursos)
                 .OwnedCollection<ConfiguracaoProcessoNivelEnsino>(x => x.NiveisEnsino)
                 .OwnedCollection<ConfiguracaoProcessoTipoVinculoAluno>(x => x.TiposVinculoAluno);

            configuration.Register<ConfiguracaoEtapa>()
               .OwnedCollection<ConfiguracaoEtapaBloqueio>(x => x.ConfiguracoesBloqueio)
               .OwnedCollection<ConfiguracaoEtapaPagina>(x => x.ConfiguracoesPagina)
               .OwnedCollection<DocumentoRequerido>(x => x.DocumentosRequeridos)
               .OwnedCollection<GrupoDocumentoRequerido>(x => x.GruposDocumentoRequerido);

            configuration.Register<ConfiguracaoEtapaPagina>()
               .OwnedCollection<ArquivoSecaoPagina>(x => x.Arquivos)
               .OwnedCollection<TextoSecaoPagina>(x => x.TextosSecao);

            configuration.Register<ArquivoSecaoPagina>()
                .OwnedEntity<ArquivoAnexado>(x => x.ArquivoAnexado);

            configuration.Register<DocumentoRequerido>()
                .OwnedCollection<SolicitacaoDocumentoRequerido>(x => x.SolicitacoesDocumentoRequerido);

            configuration.Register<GrupoDocumentoRequerido>()
                .OwnedCollection<GrupoDocumentoRequeridoItem>(x => x.Itens);

            configuration.Register<ProcessoEtapa>()
                .OwnedCollection<Escalonamento>(x => x.Escalonamentos)
                .OwnedCollection<ConfiguracaoEtapa>(x => x.ConfiguracoesEtapa)
                .OwnedCollection<ProcessoEtapaFiltroDado>(x => x.FiltrosDados)
                .OwnedCollection<SituacaoItemMatricula>(x => x.SituacoesItemMatricula)
                .OwnedCollection<ProcessoEtapaConfiguracaoNotificacao>(x => x.ConfiguracoesNotificacao);

            configuration.Register<GrupoEscalonamento>()
                .OwnedCollection<GrupoEscalonamentoItem>(x => x.Itens);

            configuration.Register<GrupoEscalonamentoItem>()
                .OwnedCollection<GrupoEscalonamentoItemParcela>(x => x.Parcelas);

            configuration.Register<Escalonamento>()
                .OwnedCollection<GrupoEscalonamentoItem>(x => x.GruposEscalonamento);

            configuration.Register<Servico>()
                 .OwnedCollection<JustificativaSolicitacaoServico>(x => x.Justificativas)
                 .OwnedCollection<ServicoSituacaoAluno>(x => x.SituacoesAluno)
                 .OwnedCollection<ServicoSituacaoIngressante>(x => x.SituacoesIngressante)
                 .OwnedCollection<InstituicaoNivelServico>(x => x.InstituicaoNivelServicos)
                 .OwnedCollection<RestricaoSolicitacaoSimultanea>(x => x.RestricoesSolicitacaoSimultanea)
                 .OwnedCollection<ServicoMotivoBloqueioParcela>(x => x.MotivosBloqueioParcela)
                 .OwnedCollection<ServicoTipoNotificacao>(x => x.TiposNotificacao)
                 .OwnedCollection<ServicoTaxa>(x => x.Taxas)
                 .OwnedCollection<ServicoTipoDocumento>(x => x.TiposDocumento)
                 .OwnedCollection<ServicoParametroEmissaoTaxa>(x => x.ParametrosEmissaoTaxa);

            configuration.Register<SolicitacaoServico>()
                .OwnedCollection(x => x.Etapas)
                .OwnedCollection(x => x.DocumentosRequeridos)
                .OwnedCollection(x => x.Boletos);

            configuration.Register<SolicitacaoServicoBoleto>()
                .OwnedCollection(x => x.Taxas);

            configuration.Register<SolicitacaoDocumentoRequerido>()
                .OwnedEntity(x => x.ArquivoAnexado);

            configuration.Register<SolicitacaoDadoFormulario>()
                .OwnedCollection(x => x.DadosCampos);

            configuration.Register<SolicitacaoServicoEtapa>()
                .OwnedCollection(x => x.HistoricosSituacao);

            configuration.Register<SolicitacaoAtividadeComplementar>()
                .OwnedCollection(x => x.DocumentosRequeridos);

            configuration.Register<SolicitacaoDocumentoConclusao>()
                .OwnedCollection(x => x.DocumentosRequeridos);

            configuration.Register<SolicitacaoArtigo>()
                .AssociatedEntity(x => x.SolicitacaoAtividadeComplementar)
                .AssociatedEntity(x => x.QualisPeriodico);

            configuration.Register<ProcessoEtapaConfiguracaoNotificacao>()
                .OwnedCollection<ParametroEnvioNotificacao>(x => x.ParametrosEnvioNotificacao);

            configuration.Register<SolicitacaoDispensa>()
                .OwnedCollection(x => x.Destinos)
                .OwnedCollection(x => x.DocumentosRequeridos)
                .OwnedCollection(x => x.OrigensExternas)
                .OwnedCollection(x => x.OrigensInternas);

            configuration.Register<SolicitacaoDispensaGrupo>()
                .OwnedCollection(x => x.Destinos)
                .OwnedCollection(x => x.OrigensExternas)
                .OwnedCollection(x => x.OrigensInternas);

            configuration.Register<SolicitacaoIntercambio>()
                .OwnedCollection(x => x.DocumentosRequeridos)
                .OwnedCollection(x => x.Participantes);

            configuration.Register<TipoNotificacao>()
                .OwnedCollection<TipoNotificacaoAtributoAgendamento>(x => x.AtributosAgendamento);

            #endregion SRC

            #region TUR

            configuration.Register<Turma>()
                .OwnedCollection<TurmaConfiguracaoComponente>(x => x.ConfiguracoesComponente)
                .OwnedCollection<DivisaoTurma>(x => x.DivisoesTurma)
                .OwnedCollection<TurmaHistoricoSituacao>(x => x.HistoricosSituacao)
                .OwnedCollection<TurmaHistoricoFechamentoDiario>(x => x.HistoricosFechamentoDiario)
                .OwnedEntity<OrigemAvaliacao>(x => x.OrigemAvaliacao);

            configuration.Register<TurmaConfiguracaoComponente>()
                .OwnedCollection<RestricaoTurmaMatriz>(x => x.RestricoesTurmaMatriz);

            configuration.Register<DivisaoTurma>()
                .OwnedCollection<DivisaoTurmaColaborador>(x => x.Colaboradores)
                .OwnedEntity<OrigemAvaliacao>(x => x.OrigemAvaliacao);

            #endregion TUR

            #region GRD

            configuration.Register<EventoAula>()
                .OwnedCollection(x => x.Colaboradores);

            configuration.Register<GradeHorariaCompartilhada>()
                 .AssociatedEntity(x => x.CicloLetivo)
                 .AssociatedEntity(x => x.EntidadeResponsavel)
                 .OwnedCollection(x => x.Itens);

            #endregion GRD

            #region Shared

            // DadoFormulario
            configuration.Register<DadoFormulario>()
                    .OwnedCollection<DadoFormularioCampo>(x => x.DadosCampos);

            #endregion Shared
        }
    }
}