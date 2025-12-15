using SMC.Academico.Common.Areas.CSO.Exceptions;
using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.Validators;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.Validators;
using SMC.Framework;
using SMC.Framework.Domain.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Repository;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class ProgramaDomainService : AcademicoContextDomain<Programa>
    {
        #region Querys

        private const string QUERY_PROGRAMA_POR_ALUNO =
            @"
                SELECT
	                si.seq_entidade
                FROM	aln.aluno_historico ah
                JOIN	cso.curso_oferta_localidade_turno colt
	                ON colt.seq_curso_oferta_localidade_turno = ah.seq_curso_oferta_localidade_turno
                JOIN	cso.curso_oferta_localidade col
	                ON col.seq_entidade = colt.seq_entidade_curso_oferta_localidade
                JOIN	cso.curso_oferta co
	                ON co.seq_curso_oferta = col.seq_curso_oferta
                JOIN	org.hierarquia_entidade_item hei
	                ON hei.seq_entidade = co.seq_entidade_curso
                JOIN	org.hierarquia_entidade_item si
	                ON si.seq_hierarquia_entidade = hei.seq_hierarquia_entidade
	                AND si.seq_hierarquia_entidade_item = hei.seq_hierarquia_entidade_item_superior
                JOIN	org.tipo_hierarquia_entidade_item thei
	                ON si.seq_tipo_hierarquia_entidade_item = thei.seq_tipo_hierarquia_entidade_item
                JOIN	org.tipo_entidade te
	                ON thei.seq_tipo_entidade = te.seq_tipo_entidade
                    AND te.dsc_token = @TOKEN
                JOIN	org.hierarquia_entidade he
	                ON he.seq_hierarquia_entidade = hei.seq_hierarquia_entidade
                JOIN	org.tipo_hierarquia_entidade the
	                ON he.seq_tipo_hierarquia_entidade = the.seq_tipo_hierarquia_entidade
                    AND	the.idt_dom_tipo_visao = @TIPO_VISAO
                WHERE ah.seq_atuacao_aluno = @SEQ_ALUNO
            ";

        #endregion Querys

        #region Domain Service

        private EntidadeDomainService EntidadeDomainService
        {
            get { return this.Create<EntidadeDomainService>(); }
        }

        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService
        {
            get { return this.Create<HierarquiaEntidadeItemDomainService>(); }
        }

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService
        {
            get { return this.Create<FormacaoEspecificaDomainService>(); }
        }

        private CursoDomainService CursoDomainService
        {
            get { return this.Create<CursoDomainService>(); }
        }

        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService
        {
            get { return this.Create<CursoOfertaLocalidadeDomainService>(); }
        }

        private CursoFormacaoEspecificaDomainService CursoFormacaoEspecificaDomainService
        {
            get { return this.Create<CursoFormacaoEspecificaDomainService>(); }
        }

        private CursoOfertaLocalidadeFormacaoDomainService CursoOfertaLocalidadeFormacaoDomainService
        {
            get { return this.Create<CursoOfertaLocalidadeFormacaoDomainService>(); }
        }

        private AlunoDomainService AlunoDomainService { get => this.Create<AlunoDomainService>(); }

        private TipoEntidadeDomainService TipoEntidadeDomainService { get => this.Create<TipoEntidadeDomainService>(); }

        private AtoNormativoDomainService AtoNormativoDomainService => Create<AtoNormativoDomainService>();

        #endregion Domain Service

        /// <summary>
        /// Busca os programas com hierarquia e histórico de situações que atendam os filtros informados
        /// </summary>
        /// <param name="filtros">Filtros da listagem de programas</param>
        /// <returns>Lista de programas com o primeiro nível de FormacaoEspecialidade associado</returns>
        public SMCPagerData<ProgramaVO> BuscarProgramas(ProgramaFilterSpecification filtros)
        {
            int total;
            var includes = IncludesPrograma.HierarquiasEntidades
                         | IncludesPrograma.HistoricoSituacoes
                         | IncludesPrograma.HistoricoSituacoes_SituacaoEntidade;

            var programas = this.SearchBySpecification(filtros, out total, includes)
                .TransformList<ProgramaVO>();

            var formacaoEspecificaDomainService = this.FormacaoEspecificaDomainService;
            var cursoDomainService = this.CursoDomainService;
            foreach (var programa in programas)
            {
                programa.Cursos = this.RecuperarCursosPrograma(cursoDomainService, programa.Seq);
            }

            return new SMCPagerData<ProgramaVO>(programas, total);
        }

        /// <summary>
        /// Busca um programa com as confirgurações de entidade
        /// </summary>
        /// <param name="seq">Sequencia do programa a ser recuperado</param>
        /// <returns>Dados do Programa e configurações de enditade</returns>
        public ProgramaVO BuscarPrograma(long seq)
        {
            var includes = IncludesPrograma.ArquivoLogotipo
                         | IncludesPrograma.Classificacoes_Classificacao
                         | IncludesPrograma.DadosOutrosIdiomas
                         | IncludesPrograma.Enderecos
                         | IncludesPrograma.EnderecosEletronicos
                         | IncludesPrograma.HierarquiasEntidades_ItemSuperior_Entidade
                         | IncludesPrograma.HistoricoNotas
                         | IncludesPrograma.Telefones
                         | IncludesPrograma.RegimeLetivo
                         | IncludesPrograma.TiposAutorizacaoBdp;

            var programa = this.SearchByKey(new ProgramaFilterSpecification() { Seq = seq }, includes);
            var programaVo = programa.Transform<ProgramaVO>();
            programaVo.SeqHierarquiaEntidadeItemSuperior = programa.HierarquiasEntidades.First().SeqItemSuperior.Value;
            programaVo.DescricaoEntidadeResponsavel = programa.HierarquiasEntidades.First().ItemSuperior.Entidade.Nome;
            programaVo.HierarquiasClassificacoes = this.EntidadeDomainService.GerarEntidadeClassificacoes(programa.SeqTipoEntidade, programa.Classificacoes);

            programaVo.AtivaAbaAtoNormativo = TipoEntidadeDomainService.PermiteAtoNormativo(programaVo.SeqTipoEntidade, TOKEN_TIPO_ENTIDADE_EXTERNADA.PROGRAMA);
            if (programaVo.AtivaAbaAtoNormativo)
            {
                var retorno = AtoNormativoDomainService.BuscarAtoNormativoPorEntidade(seqEntidade: programaVo.Seq, SeqInstituicaoEnsino: programaVo.SeqInstituicaoEnsino);

                programaVo.HabilitaColunaGrauAcademico = retorno.Where(w => w.DescricaoGrauAcademico != null).Select(s => s.DescricaoGrauAcademico).Any();
                programaVo.AtoNormativo = retorno;
            }

            return programaVo;
        }

        /// <summary>
        /// Grava um programa e suas dependências. Idiomas e sua hierarquia(gerada no serviço)
        /// </summary>
        /// <param name="programaVo">Programa a ser gravado incluindo idiomas</param>
        /// <returns>Sequencia do programa gravado</returns>
        public long SalvarPrograma(ProgramaVO programaVo)
        {
            var programa = programaVo.Transform<Programa>();

            // Converte a hierarquia de classificações no formato do domínio
            programa.Classificacoes = EntidadeDomainService.GerarEntidadeClassificacoes(programaVo);

            this.Validar(programa, new EntidadeValidator(), new ProgramaValidator());

            // Validar componente de telefone e endereço
            this.EntidadeDomainService.ValidarDadosContatoObrigatorios(programa);

            var entidadeDomainService = this.EntidadeDomainService;

            if (programa.Seq == 0)
            {
                entidadeDomainService.IncluirSituacao(programa);
                programa.HierarquiasEntidades = new List<HierarquiaEntidadeItem>();
                programa.HierarquiasEntidades.Add(new HierarquiaEntidadeItem() { SeqItemSuperior = programa.SeqHierarquiaEntidadeItemSuperior });
            }
            else
            {
                programa.HierarquiasEntidades = this.HierarquiaEntidadeItemDomainService
                    .SearchBySpecification(new HierarquiaEntidadeItemFilterSpecification() { SeqEntidade = programa.Seq })
                    .ToList();
                foreach (var item in programa.HierarquiasEntidades)
                {
                    item.SeqItemSuperior = programa.SeqHierarquiaEntidadeItemSuperior;
                }
            }
            entidadeDomainService.AtualizarHierarquiaEntidadeExternada(programa, TOKEN_TIPO_ENTIDADE_EXTERNADA.PROGRAMA);

            this.EnsureFileIntegrity(programa, m => m.SeqArquivoLogotipo, m => m.ArquivoLogotipo);

            this.SaveEntity(programa);

            return programa.Seq;
        }

        /// <summary>
        /// Recupera os programas de um grupo de programas
        /// </summary>
        /// <param name="seqEntidadeVinculo">Sequencial do grupo de programas</param>
        /// <returns>Sequenciais dos programas filhos do grupo de programas informado</returns>
        public List<long> BuscarSeqsProgramasGrupo(long seqEntidadeVinculo)
        {
            return HierarquiaEntidadeItemDomainService.BuscarHierarquiaEntidadeItensPorEntidadeVisaoOganizacional(seqEntidadeVinculo, TOKEN_TIPO_ENTIDADE_EXTERNADA.PROGRAMA);
        }

        private void Validar(Programa programa, params SMCValidator[] validatores)
        {
            // Validar TipoAutorizacao Texto Completo informado
            if (!programa.TiposAutorizacaoBdp.Select(x => x.TipoAutorizacao).Contains(TipoAutorizacao.TextoCompleto))
            {
                throw new ProgramaSemTipoAutorizacaoTextoCompletoException();
            }

            //Validar mesmo nome resumido do programa
            if (!string.IsNullOrEmpty(programa.NomeReduzido))
            {
                var spec = new EntidadeFilterSpecification() 
                { 
                    SeqTipoEntidade = programa.SeqTipoEntidade, 
                    NomeReduzido = programa.NomeReduzido 
                };
                if (!programa.IsNew())
                    spec.Seq = programa.Seq;

                //Se houver outro programa com o mesmo nome reduzido que não seja o próprio programa que está sendo gravado
                //dispara exceção
                if (this.EntidadeDomainService.Count(spec) > 0)
                    throw new ProgramaMesmoNomeResumidoException();
            }

            //Validar outras regras que estão nos validators
            var results = new List<SMCValidationResults>();

            foreach (var validador in validatores)
            {
                results.Add(validador.Validate(programa));
            }
            if (results.Count(c => !c.IsValid) > 0)
            {
                List<SMCValidationResults> errorList = results.Where(w => !w.IsValid).ToList();
                throw new SMCInvalidEntityException(errorList);
            }
        }

        /// <summary>
        /// Recupera os Cursos filhos de um programa ordenados pela descrição do nível de ensino e nome do curso
        /// </summary>
        /// <param name="formacaoEspecificaDomainService">Instância do domain service</param>
        /// <param name="seqPrograma">Sequencial do programa</param>
        /// <returns>Cursos filhos do programa</returns>
        private List<CursoVO> RecuperarCursosPrograma(CursoDomainService cursoDomainService, long seqPrograma)
        {
            // Recupera os sequenciais dos itens de hierarquia filhos do programa
            var specHierarquiaPrograma = new HierarquiaEntidadeItemFilterSpecification()
            {
                SeqEntidade = seqPrograma
            };
            var hierarquiasEntidadePrograma = HierarquiaEntidadeItemDomainService
                .SearchProjectionBySpecification(specHierarquiaPrograma, p => (long?)p.Seq)
                .ToList();

            //Caso não tenha hierarquia, não é possível identificar os filhos
            //Retorna uma lista vazia.
            if (!hierarquiasEntidadePrograma.Any())
                return new List<CursoVO>();

            // Recupera os cursos com seus níveis de ensino
            var includeCurso = IncludesCurso.NivelEnsino;
            var specCurso = new CursoFilterSpecification()
            {
                SeqsEntidadesResponsaveis = hierarquiasEntidadePrograma
            };
            var orderCurso = new List<SMCSortInfo>();
            orderCurso.Add(new SMCSortInfo($"{nameof(Curso.NivelEnsino)}.{nameof(NivelEnsino.Descricao)}", SMCSortDirection.Ascending));
            orderCurso.Add(new SMCSortInfo(nameof(Curso.Nome), SMCSortDirection.Ascending));
            specCurso.SetOrderBy(orderCurso);

            return cursoDomainService
                .SearchBySpecification(specCurso, includeCurso)
                .TransformList<CursoVO>();
        }

        /// <summary>
        /// Buscar programa do aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Sequencial do progrma</returns>
        public long BuscarProgramaPorAluno(long seqAluno)
        {
            long seqPrograma = this.RawQuery<long>(QUERY_PROGRAMA_POR_ALUNO,
                                                  new SMCFuncParameter("SEQ_ALUNO", seqAluno),
                                                  new SMCFuncParameter("TOKEN", TOKEN_TIPO_ENTIDADE.PROGRAMA),
                                                  new SMCFuncParameter("TIPO_VISAO", TipoVisao.VisaoOrganizacional)).FirstOrDefault();

            return seqPrograma;
        }

        public List<ReplicaFormacaoEspecificaProgramaConfirmacaoVO> BuscarItensSelecionadosReplicarFormacaoEspecificaPrograma(ReplicaFormacaoEspecificaProgramaVO model)
        {
            var lista = new List<ReplicaFormacaoEspecificaProgramaConfirmacaoVO>();

            var specCurso = new CursoFilterSpecification() { SeqsCursos = model.SeqsCursos };

            specCurso.SetOrderBy("Nome");

            var cursosSelecionados = CursoDomainService.SearchProjectionBySpecification(specCurso, c => new ReplicaFormacaoEspecificaProgramaConfirmacaoVO()
            {
                Seq = c.Seq,
                Descricao = c.Nome
            }).ToList();

            var specCursoOfertaLocalidade = new CursoOfertaLocalidadeFilterSpecification() { Seqs = model.SeqsCursosOfertasLocalidades.ToArray() };

            specCursoOfertaLocalidade.SetOrderBy("Nome");

            var cursosOfertasLocalidadesSelecionados = this.CursoOfertaLocalidadeDomainService.SearchProjectionBySpecification(specCursoOfertaLocalidade, c => new ReplicaFormacaoEspecificaProgramaConfirmacaoVO()
            {
                Seq = c.Seq,
                Descricao = c.Nome,
                SeqPai = c.CursoOferta.SeqCurso
            }).ToList();

            lista.AddRange(cursosSelecionados);

            lista.AddRange(cursosOfertasLocalidadesSelecionados);

            return lista;
        }

        public void SalvarReplicarFormacaoEspecificaPrograma(ReplicaFormacaoEspecificaProgramaVO model)
        {
            using (var unityOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    foreach (var seqCurso in model.SeqsCursos)
                    {
                        var titulacoes = model.CursosTitulacoes.Single(s => s.SeqCurso == seqCurso).Titulacoes;
                        var cursoFormacaoEspecifica = new CursoFormacaoEspecifica()
                        {
                            SeqCurso = seqCurso,
                            SeqFormacaoEspecifica = model.SeqFormacaoEspecifica,
                            DataInicioVigencia = model.DataInicioVigenciaFormacaoCurso,
                            DataFimVigencia = model.DataFimVigenciaFormacaoCurso,
                            SeqGrauAcademico = titulacoes.Select(s => s.SeqGrauAcademico).FirstOrDefault()
                        };

                        if (model.CursosTitulacoes.SMCAny(a => a.SeqCurso == seqCurso))
                        {
                            cursoFormacaoEspecifica.Titulacoes = new List<CursoFormacaoEspecificaTitulacao>();
                            foreach (var titulacao in titulacoes)
                            {
                                var cursoFormacaoEspecificaTitulacao = new CursoFormacaoEspecificaTitulacao()
                                {
                                    SeqTitulacao = titulacao.SeqTitulacao,
                                    Ativo = titulacao.Ativo
                                };

                                cursoFormacaoEspecifica.Titulacoes.Add(cursoFormacaoEspecificaTitulacao);
                            }
                        }

                        this.CursoFormacaoEspecificaDomainService.SaveEntity(cursoFormacaoEspecifica);
                    }

                    foreach (var seqCursoOfertaLocalidade in model.SeqsCursosOfertasLocalidades)
                    {
                        var cursoOfertaLocalidadeFormacao = new CursoOfertaLocalidadeFormacao()
                        {
                            SeqCursoOfertaLocalidade = seqCursoOfertaLocalidade,
                            SeqFormacaoEspecifica = model.SeqFormacaoEspecifica
                        };

                        this.CursoOfertaLocalidadeFormacaoDomainService.SaveEntity(cursoOfertaLocalidadeFormacao);
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
    }
}