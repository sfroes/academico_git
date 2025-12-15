using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.Validators;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.Validators;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Framework.Domain.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class CursoDomainService : AcademicoContextDomain<Curso>
    {
        #region [ DomainService ]

        private EntidadeDomainService EntidadeDomainService
        {
            get { return Create<EntidadeDomainService>(); }
        }

        private TipoEntidadeDomainService TipoEntidadeDomainService
        {
            get { return Create<TipoEntidadeDomainService>(); }
        }

        private HierarquiaEntidadeDomainService HierarquiaEntidadeDomainService
        {
            get { return Create<HierarquiaEntidadeDomainService>(); }
        }

        private InstituicaoTipoEntidadeDomainService InstituicaoTipoEntidadeDomainService
        {
            get { return Create<InstituicaoTipoEntidadeDomainService>(); }
        }

        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService
        {
            get { return Create<HierarquiaEntidadeItemDomainService>(); }
        }

        private TipoHierarquiaEntidadeItemDomainService TipoHierarquiaEntidadeItemDomainService
        {
            get { return Create<TipoHierarquiaEntidadeItemDomainService>(); }
        }

        private GrupoCurricularComponenteDomainService GrupoCurricularComponenteDomainService
        {
            get { return Create<GrupoCurricularComponenteDomainService>(); }
        }

        private AlunoHistoricoDomainService AlunoHistoricoDomainService
        {
            get { return Create<AlunoHistoricoDomainService>(); }
        }

        private ColaboradorDomainService ColaboradorDomainService
        {
            get { return Create<ColaboradorDomainService>(); }
        }

        private CursoFormacaoEspecificaDomainService CursoFormacaoEspecificaDomainService
        {
            get { return Create<CursoFormacaoEspecificaDomainService>(); }
        }

        private TipoClassificacaoDomainService TipoClassificacaoDomainService
        {
            get { return Create<TipoClassificacaoDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Buscar um curso pelo sequencial
        /// </summary>
        /// <param name="seq">Sequencia do curso a ser recuperado</param>
        /// <returns>Curso recuperado</returns>
        public Curso BuscarCurso(long seq)
        {
            var includes = IncludesCurso.ArquivoLogotipo | IncludesCurso.Enderecos | IncludesCurso.EnderecosEletronicos |
                           IncludesCurso.HierarquiasEntidades | IncludesCurso.Telefones | IncludesCurso.HistoricoSituacoes_SituacaoEntidade |
                           IncludesCurso.NivelEnsino | IncludesCurso.HierarquiasEntidades_ItemSuperior_Entidade |
                           IncludesCurso.CursosFormacaoEspecifica_FormacaoEspecifica | IncludesCurso.FormacoesEspecificasEntidade |
                           IncludesCurso.CursosOferta | IncludesCurso.Classificacoes_Classificacao;

            var curso = this.SearchByKey(new SMCSeqSpecification<Curso>(seq), includes);

            return curso;
        }

        /// <summary>
        /// Buscar um curso com a hierarquia pelo sequencial
        /// </summary>
        /// <param name="seq">Sequencia do curso a ser recuperado</param>
        /// <returns>Curso recuperado</returns>
        public CursoVO BuscarCursoComHierarquia(long seq)
        {
            var curso = this.BuscarCurso(seq);

            var cursoVo = curso.Transform<CursoVO>();

            cursoVo.HierarquiasClassificacoes = this.EntidadeDomainService
                                                    .BuscarEntidadeHierarquiasClassificacaoPorNivelEnsino(curso.SeqNivelEnsino, curso.Classificacoes)
                                                    .TransformList<EntidadeHierarquiaClassificacaoVO>();

            foreach (var item in cursoVo.HierarquiasClassificacoes)
            {
                foreach (var classificacao in item.Classificacoes)
                {
                    var tipoClassificacao = TipoClassificacaoDomainService.SearchByKey(new SMCSeqSpecification<TipoClassificacao>(classificacao.SeqTipoClassificacao));
                    if (!string.IsNullOrEmpty(classificacao.CodigoExterno))
                        classificacao.Descricao = "[" + tipoClassificacao.Descricao + "]" + " - " + classificacao.CodigoExterno + " - " + classificacao.Descricao;
                    else
                        classificacao.Descricao = "[" + tipoClassificacao.Descricao + "]" + " - " + classificacao.Descricao;


                }
            }

            cursoVo.SeqsHierarquiaEntidadeItem = cursoVo.HierarquiasEntidades?.Where(i => i.SeqItemSuperior.HasValue).Select(i => i.SeqItemSuperior.Value).ToArray();

            return cursoVo;
        }

        /// <summary>
        /// Buscar as possíveis entidades superiories na visão organizacional de um tipo de entidade curso
        /// para montagem de select
        /// </summary>
        /// <param name="apenasAtivas">Considerar todas as entidades com situação ativas ou em ativação</param>
        /// <param name="usarSeqEntidadde">Define se será retornada o seq da entidade ou o seq da hierarquia item que representa esta entidade</param>
        /// <returns>Lista de possíveis entidades superiores do curso</returns>
        public List<SMCDatasourceItem> BuscarCursoComHierarquiaSuperiorSelect(bool apenasAtivas = false, bool usarNomeReduzido = false, bool usarSeqEntidadde = false, bool considerarGrupoPrograma = false)
        {
            var voTipoEntidade = TipoEntidadeDomainService.BuscarTipoEntidadeNaInstituicao(TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO);

            var selectHierarquia = HierarquiaEntidadeDomainService.BuscarEntidadeSuperiorSelect(voTipoEntidade.Seq, TipoVisao.VisaoOrganizacional, apenasAtivas, usarNomeReduzido, usarSeqEntidadde, considerarGrupoPrograma: considerarGrupoPrograma);

            return selectHierarquia;
        }

        /// <summary>
        /// Buscar as situações do tipo de entidade externada curso
        /// </summary>
        /// <returns>Lista de situações de um curso</returns>
        public List<SMCDatasourceItem> BuscarSituacoesCursoSelect()
        {
            var voTipoEntidade = TipoEntidadeDomainService.BuscarTipoEntidadeNaInstituicao(TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO);

            var retorno = InstituicaoTipoEntidadeDomainService.BuscarSituacoesTipoEntidadeDaInstituicaoSelect(voTipoEntidade.Seq, false);

            return retorno;
        }

        /// <summary>
        /// Buscar os cursos que atendam os filtros informados e com a paginação correta
        /// </summary>
        /// <param name="filtros">Filtros da listagem de cursos</param>
        /// <returns>SMCPagerData com a lista de cursos</returns>
        public SMCPagerData<Curso> BuscarCursos(CursoFilterSpecification filtros)
        {
            var includes = IncludesCurso.HierarquiasEntidades
                         | IncludesCurso.HistoricoSituacoes_SituacaoEntidade
                         | IncludesCurso.NivelEnsino
                         | IncludesCurso.HierarquiasEntidades_ItemSuperior_Entidade
                         | IncludesCurso.CursosOferta;

            int total = 0;
            var cursos = this.SearchBySpecification(filtros, out total, includes).ToList();

            return new SMCPagerData<Curso>(cursos, total);
        }

        /// <summary>
        /// Recupera um curso pelo componente curricular
        /// </summary>
        /// <param name="seqGrupoCurricularComponente">Sequencial do componente curricular</param>
        /// <returns>Dados do curso</returns>
        public Curso BuscarCursoPorGrupoCurricularComponente(long seqGrupoCurricularComponente)
        {
            return this.GrupoCurricularComponenteDomainService
                .SearchProjectionByKey(new SMCSeqSpecification<GrupoCurricularComponente>(seqGrupoCurricularComponente),
                                       p => p.GrupoCurricular.Curriculo.Curso);
        }

        /// <summary>
        /// Salva um curso com suas hierarquias de entidade
        /// </summary>
        /// <param name="cursoVo">Dados do curso a ser salvo</param>
        /// <returns>Sequencial do curso salvo</returns>
        public long SalvarCurso(CursoVO cursoVo)
        {
            var curso = cursoVo.Transform<Curso>();

            // Converte a hierarquia de classificações no formato do domínio
            curso.Classificacoes = EntidadeDomainService.GerarEntidadeClassificacoes(cursoVo);

            var seqTipoEntidadeCurso = this.TipoEntidadeDomainService
                                           .BuscarTipoEntidadeNaInstituicao(TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO)
                                           .Seq;

            foreach (var itemHierarquia in curso.HierarquiasEntidades)
            {
                var specSuperior = new HierarquiaEntidadeItemFilterSpecification() { Seq = itemHierarquia.SeqItemSuperior };
                var itemHierarquiaSuperior = HierarquiaEntidadeItemDomainService.SearchByKey(specSuperior, IncludesHierarquiaEntidadeItem.Entidade);

                var tipoHierarquiaEntidadeItemCurso = TipoHierarquiaEntidadeItemDomainService
                    .SearchByKey(new TipoHierarquiaEntidadeItemFilterSpecification()
                    {
                        SeqItemSuperior = itemHierarquiaSuperior.SeqTipoHierarquiaEntidadeItem,
                        SeqTipoEntidade = seqTipoEntidadeCurso
                    });

                itemHierarquia.SeqHierarquiaEntidade = itemHierarquiaSuperior.SeqHierarquiaEntidade;
                itemHierarquia.SeqTipoHierarquiaEntidadeItem = tipoHierarquiaEntidadeItemCurso.Seq;
                itemHierarquia.ItemSuperior = itemHierarquiaSuperior;
            }

            this.Validar(curso, new EntidadeValidator(), new CursoValidator());

            // Validar componente de telefone e endereço
            this.EntidadeDomainService.ValidarDadosContatoObrigatorios(curso);

            if (curso.Seq == 0)
                this.EntidadeDomainService.IncluirSituacao(curso);

            //O Nome do curso é obrigatório
            //if (curso.TipoCurso == TipoCurso.ABI && string.IsNullOrEmpty(curso.Nome))
            //    curso.Nome = "ABI em";

            this.EnsureFileIntegrity(curso, m => m.SeqArquivoLogotipo, m => m.ArquivoLogotipo);

            this.SaveEntity(curso);

            return curso.Seq;
        }

        private void Validar(Curso curso, params SMCValidator[] validatores)
        {
            var results = new List<SMCValidationResults>();
            foreach (var validador in validatores)
            {
                results.Add(validador.Validate(curso));
            }
            if (results.Count(c => !c.IsValid) > 0)
            {
                List<SMCValidationResults> errorList = results.Where(w => !w.IsValid).ToList();
                throw new SMCInvalidEntityException(errorList);
            }
        }

        /// <summary>
        /// Buscar a lista de sequenciais de configuração de componente através de uma lista de sequenciais de curso
        /// </summary>
        /// <param name="seqsCurso">Lista sequenciais de curso</param>
        /// <returns>Lista sequenciais de configuração de componentes</returns>
        public List<long?> BuscarConfiguracoesComponentesPorCursos(List<long> seqsCurso)
        {
            var specCursos = new CursoFilterSpecification() { SeqsCursos = seqsCurso };
            var registros = this.SearchProjectionBySpecification(specCursos, p => new
            {
                SeqConfiguracaoComponente = p.Curriculos.SelectMany(
                                            s => s.CursosOferta.SelectMany(
                                            m => m.MatrizesCurriculares.SelectMany(
                                            d => d.ConfiguracoesComponente.Select(
                                            c => c.SeqConfiguracaoComponente)))).ToList()
            }).ToList();

            var seqsConfiguracoes = registros.SelectMany(s => s.SeqConfiguracaoComponente).ToList();

            return seqsConfiguracoes;
        }

        /// <summary>
        /// Recupera as informações do curso do aluno
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação do aluno</param>
        /// <returns></returns>
        public (long? SeqCursoOfertaLocalidadeTurno, long? SeqCursoOfertaLocalidade, long? SeqCursoUnidade, long? SeqCurso) BuscarCursoDoAluno(long seqPessoaAtuacao)
        {
            AlunoHistoricoFilterSpecification spec = new AlunoHistoricoFilterSpecification()
            {
                Atual = true,
                SeqAluno = seqPessoaAtuacao
            };
            var seqs = AlunoHistoricoDomainService.SearchProjectionByKey(spec, a => new
            {
                SeqCursoOfertaLocalidadeTurno = (long?)a.SeqCursoOfertaLocalidadeTurno,
                SeqCursoOfertaLocalidade = (long?)a.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade,
                SeqCursoUnidade = (long?)a.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.SeqCursoUnidade,
                SeqCurso = (long?)a.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.SeqCurso
            });
            if (seqs != null)
                return (seqs.SeqCursoOfertaLocalidadeTurno, seqs.SeqCursoOfertaLocalidade, seqs.SeqCursoUnidade, seqs.SeqCurso);
            else
                return (null, null, null, null);
        }

        public List<Curso> BuscarCursosAssociadosAoProfessor(long seqPessoaAtuacao)
        {
            try
            {
                List<Curso> lista = new List<Curso>();
                var colaborador = ColaboradorDomainService.SearchByKey(new SMCSeqSpecification<Colaborador>(seqPessoaAtuacao), a => a.Vinculos[0].Cursos[0].CursoOfertaLocalidade.CursoUnidade.Curso);

                foreach (var vinculo in colaborador.Vinculos)
                {
                    foreach (var curso in vinculo.Cursos)
                    {
                        lista.Add(curso.CursoOfertaLocalidade.CursoUnidade.Curso);
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<SMCDatasourceItem> BuscarCursosReplicarFormacaoEspecificaProgramaSelect(CursoReplicaFormacaoEspecificaProgramaFiltroVO filtros)
        {
            var specHierarquiaEntidadeItem = new HierarquiaEntidadeItemFilterSpecification()
            {
                SeqEntidade = filtros.SeqEntidadeResponsavel
            };

            var seqsHierarquiasEntidadesItens = HierarquiaEntidadeItemDomainService
                .SearchProjectionBySpecification(specHierarquiaEntidadeItem, p => (long?)p.Seq)
                .ToList();

            if (!seqsHierarquiasEntidadesItens.Any())
                return new List<SMCDatasourceItem>();

            var specCurso = new CursoReplicaFormacaoEspecificaProgramaFilterSpecification()
            {
                SeqsEntidadesResponsaveis = seqsHierarquiasEntidadesItens,
                SeqFormacaoEspecifica = filtros.SeqFormacaoEspecifica,
                CategoriasAtividadesSituacoesAtuais = filtros.CategoriasAtividadesSituacoesAtuais
            };

            specCurso.SetOrderBy("Nome");

            return this.SearchProjectionBySpecification(specCurso, c => new SMCDatasourceItem()
            {
                Seq = c.Seq,
                Descricao = c.Nome
            }).ToList();
        }
    }
}