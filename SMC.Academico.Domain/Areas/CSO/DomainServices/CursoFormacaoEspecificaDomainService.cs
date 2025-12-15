using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.Common.Areas.CSO.Exceptions;
using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class CursoFormacaoEspecificaDomainService : AcademicoContextDomain<CursoFormacaoEspecifica>
    {
        #region [ DomainService ]

        private CursoDomainService CursoDomainService => Create<CursoDomainService>();

        private CursoOfertaDomainService CursoOfertaDomainService => Create<CursoOfertaDomainService>();

        private CursoOfertaLocalidadeFormacaoDomainService CursoOfertaLocalidadeFormacaoDomainService => Create<CursoOfertaLocalidadeFormacaoDomainService>();

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService => Create<FormacaoEspecificaDomainService>();

        private HierarquiaEntidadeDomainService HierarquiaEntidadeDomainService => Create<HierarquiaEntidadeDomainService>();

        private GrauAcademicoDomainService GrauAcademicoDomainService => Create<GrauAcademicoDomainService>();

        private TipoFormacaoEspecificaDomainService TipoFormacaoEspecificaDomainService => Create<TipoFormacaoEspecificaDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Buscar a lista de formações específicas relacionadas ao curso para popular um Select
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Lista de formação especifica</returns>
        public List<SMCDatasourceItem> BuscarCursoFormacaoEspecificaSelect(long seqCurso)
        {
            CursoFormacaoEspecificaFilterSpecification specCursoFormacao = new CursoFormacaoEspecificaFilterSpecification();

            specCursoFormacao.SeqCurso = seqCurso;

            return this.SearchBySpecification(specCursoFormacao, IncludesCursoFormacaoEspecifica.FormacaoEspecifica).Select(s => s.FormacaoEspecifica).TransformList<SMCDatasourceItem>();
        }

        /// <summary>
        /// Buscar a lista de formações específicas das entidades superior ao curso para popular um Select
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Lista de formação especifica</returns>
        public List<SMCDatasourceItem> BuscarTipoFormacaoEspecificaResponsavelSelect(long seqCurso)
        {
            var curso = CursoDomainService.SearchByKey(new SMCSeqSpecification<Curso>(seqCurso), IncludesCurso.HierarquiasEntidades_ItemSuperior_Entidade);

            FormacaoEspecificaFilterSpecification specFormacao = new FormacaoEspecificaFilterSpecification();

            specFormacao.SeqEntidades = curso.HierarquiasEntidades.Select(s => s.ItemSuperior.SeqEntidade).ToList();

            return FormacaoEspecificaDomainService.SearchBySpecification(specFormacao).TransformList<SMCDatasourceItem>();
        }

        /// <summary>
        /// Salvar a relação curso formação específica de acordo com o tipo de formação utilizado
        /// Não retorna o seq é uma gravação mestre detalhe
        /// <param name="formacao">Objeto com os dados da formação específica de acordo com o tipo</param>
        /// </summary>
        public long SalvarCursoFormacaoEspecifica(CursoFormacaoEspecificaVO formacao)
        {
            ValidarModelo(formacao);

            CursoFormacaoEspecifica entity = formacao.Transform<CursoFormacaoEspecifica>();

            // Caso esteja editando e a SeqFormacaoEspecifica seja 0, significa que estou editando um cadastro simples ou ABI
            // Recupera a formação específica para definir o sequencial
            if (formacao.CursoTipoFormacao == CursoTipoFormacao.Cadastro_Simples || formacao.CursoTipoFormacao == CursoTipoFormacao.Cadastro_Oferta)
            {
                var curso = CursoDomainService.BuscarCurso(formacao.SeqCurso);

                //Verifica duplicidade da descrição em cadastro simples
                if (formacao.CursoTipoFormacao == CursoTipoFormacao.Cadastro_Simples
                 && curso.CursosFormacaoEspecifica.Any(w => w.Seq != formacao.Seq
                                                    && w.FormacaoEspecifica.Descricao == formacao.FormacaoEspecifica.Descricao
                                                    && w.SeqGrauAcademico == formacao.SeqGrauAcademico))
                    throw new CursoFormacaoEspecificaDuplicadoException();

                //Verifica duplicidade do cursooferta em cadastro por curso oferta
                if (formacao.CursoTipoFormacao == CursoTipoFormacao.Cadastro_Oferta
                 && formacao.FormacaoEspecifica.SeqCursoOferta.HasValue
                 && curso.CursosFormacaoEspecifica.Any(w => w.Seq != formacao.Seq
                                                    && w.FormacaoEspecifica.SeqCursoOferta == formacao.FormacaoEspecifica.SeqCursoOferta.Value))
                    throw new CursoFormacaoEspecificaDuplicadoException();

                // Recupera a informação de formação específica que está salva para esta formação (caso seja edição)
                if (entity.Seq != 0)
                {
                    entity.FormacaoEspecifica = this.SearchProjectionByKey(new SMCSeqSpecification<CursoFormacaoEspecifica>(formacao.Seq), x => x.FormacaoEspecifica);

                    if (formacao.CursoTipoFormacao == CursoTipoFormacao.Cadastro_Simples)
                    {
                        // Atualiza as informações da Formação Específica
                        entity.FormacaoEspecifica.SeqTipoFormacaoEspecifica = formacao.FormacaoEspecifica.SeqTipoFormacaoEspecifica;
                        entity.FormacaoEspecifica.Descricao = formacao.FormacaoEspecifica.Descricao;
                    }
                }

                if (formacao.CursoTipoFormacao == CursoTipoFormacao.Cadastro_Oferta && formacao.FormacaoEspecifica.SeqCursoOferta.HasValue)
                {
                    // Recuperar grau academico do curso oferta selecionado
                    var cursoOferta = CursoOfertaDomainService.BuscarCursoOferta(formacao.FormacaoEspecifica.SeqCursoOferta.Value);
                    entity.FormacaoEspecifica.Descricao = cursoOferta.Descricao;
                }
            }
            else
            {
                entity.FormacaoEspecifica = null;
            }

            if (entity.FormacaoEspecifica != null)
            {
                // Caso seja um novo, cria a formação específica
                entity.FormacaoEspecifica.CursoOfertaFormacao = null;
                entity.FormacaoEspecifica.Cursos = null;
                entity.FormacaoEspecifica.CursosOfertaLocalidade = null;
                entity.FormacaoEspecifica.CursosOfertas = null;
                entity.FormacaoEspecifica.EntidadeResponsavel = null;
                entity.FormacaoEspecifica.FormacaoEspecificaSuperior = null;
                entity.FormacaoEspecifica.FormacoesEspecificasFilhas = null;
                entity.FormacaoEspecifica.TipoFormacaoEspecifica = null;
                entity.FormacaoEspecifica.Ativo = DateTime.Today >= formacao.DataInicioVigencia && (!formacao.DataFimVigencia.HasValue || DateTime.Today <= formacao.DataFimVigencia);

                // salva a formação específica
                FormacaoEspecificaDomainService.SaveEntity(entity.FormacaoEspecifica);
                entity.SeqFormacaoEspecifica = entity.FormacaoEspecifica.Seq;
            }

            entity.Curso = null;

            // Salva o pai
            this.SaveEntity(entity);

            return entity.Seq;
        }

        private void ValidarModelo(CursoFormacaoEspecificaVO modelo)
        {
            if (modelo.TitulacaoRequeridoPorFormacao && modelo.GrauAcademicoRequerido && modelo.Titulacoes.Count == 0)
                throw new CursoFormacaoEspecificaTitulacaoObrigatorioException();

            if (modelo.Titulacoes.Any())
            {
                if (modelo.Titulacoes.Where(w => w.Ativo == true).Count() > 1)
                    throw new CursoFormacaoEspecificaTitulacaoSomenteUmaAssociadaAtivaException();

                var titulacaoDuplicada = modelo.Titulacoes.Select(s => s.SeqTitulacao).ToList()
                                                  .GroupBy(g => g).Where(w => w.Count() > 1).Select(group => group.Key).ToList();
                if (titulacaoDuplicada.Count() > 0)
                    throw new CursoFormacaoEspecificaTitulacaoDuplicadaException();
            }

            if (modelo.SeqFormacaoEspecifica != 0)
            {
                CursoFormacaoEspecificaFilterSpecification spec = new CursoFormacaoEspecificaFilterSpecification()
                {
                    SeqCurso = modelo.SeqCurso,
                    SeqFormacaoEspecifica = modelo.SeqFormacaoEspecifica
                };

                var cursoFormacaoEspecificaPorFormacao = this.SearchBySpecification(spec).ToList();

                if (cursoFormacaoEspecificaPorFormacao.Any(a => a.Seq != modelo.Seq))
                    throw new CursoFormacaoEspecificaFormacaoJaAssociadaException();

                var formacaoEspecifica = this.FormacaoEspecificaDomainService.SearchByKey(new SMCSeqSpecification<FormacaoEspecifica>(modelo.SeqFormacaoEspecifica));
                var cursoOfertas = this.CursoOfertaDomainService.SearchBySpecification(new CursoOfertaFilterSpecification() { SeqFormacaoEspecifica = modelo.SeqFormacaoEspecifica }).ToList();

                if (DateTime.Today >= modelo.DataInicioVigencia && (!modelo.DataFimVigencia.HasValue || DateTime.Today <= modelo.DataFimVigencia) && !formacaoEspecifica.Ativo.GetValueOrDefault())
                    throw new CursoFormacaoEspecificaVigenteFormacaoInativaException();

                if (!(DateTime.Today >= modelo.DataInicioVigencia && (!modelo.DataFimVigencia.HasValue || DateTime.Today <= modelo.DataFimVigencia)) && cursoOfertas.Any(a => a.Ativo))
                    throw new CursoFormacaoEspecificaNaoVigenteCursoOfertaAtivoException();
            }
        }

        /// <summary>
        /// Configura um novo item de CursoFormacaoEspecifica
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso atual</param>
        /// <returns>CursoFormacaoEspecificaVO com os dados configurados</returns>
        public CursoFormacaoEspecificaVO ConfigurarCursoFormacaoEspecifica(long seqCurso)
        {
            CursoFormacaoEspecificaVO ret = new CursoFormacaoEspecificaVO();
            ret.SeqCurso = seqCurso;
            AplicaDadosCurso(ret);
            return ret;
        }

        /// <summary>
        /// Verificar se existe pelo menos uma Formação Especifica para o Curso Selecionado
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>True or false</returns>
        /// </summary>
        public bool VerificarExisteCursoFormacaoEspecifica(long seqCurso)
        {
            //recupera os curso formação especifica já cadastrado
            CursoFormacaoEspecificaFilterSpecification specCursoFormacao = new CursoFormacaoEspecificaFilterSpecification();
            specCursoFormacao.SeqCurso = seqCurso;

            var cursoFormacao = this.SearchBySpecification(specCursoFormacao);

            return cursoFormacao.Count() > 0;
        }

        public CursoFormacaoEspecificaVO BuscarCursoFormacaoEspecifica(long seq)
        {
            var cursoFormacaoEspecifica = this.SearchByKey(new SMCSeqSpecification<CursoFormacaoEspecifica>(seq),
                                                            IncludesCursoFormacaoEspecifica.FormacaoEspecifica
                                                          | IncludesCursoFormacaoEspecifica.FormacaoEspecifica_TipoFormacaoEspecifica
                                                          | IncludesCursoFormacaoEspecifica.FormacaoEspecifica_CursosOfertas
                                                          | IncludesCursoFormacaoEspecifica.Titulacoes
                                                          | IncludesCursoFormacaoEspecifica.Titulacoes_Titulacao
                                                          | IncludesCursoFormacaoEspecifica.GrauAcademico).Transform<CursoFormacaoEspecificaVO>();

            // Aplica o CursoTipoFormacao
            AplicaDadosCurso(cursoFormacaoEspecifica);

            // Retorna
            return cursoFormacaoEspecifica;
        }

        /// <summary>
        /// Faz a busca dos CursoFormacaoEspecifica cadastrados para um curso
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Retorna a lista de CursoFormacaoEspecifica cadastrados com suas superiores</returns>

        public List<CursoFormacaoEspecificaNodeVO> BuscarCursoFormacoesEspecificas(CursoFormacaoEspecificaFilterSpecification filtro)
        {
            var formacoesCurso = CursoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Curso>(filtro.SeqCurso.Value), p => p.CursosFormacaoEspecifica.Select(s => new
            {
                SeqCursoFormacaoEspecifica = s.Seq,
                s.SeqCurso,
                s.SeqFormacaoEspecifica,
                s.DataInicioVigencia,
                s.DataFimVigencia
            }).ToList());

            var hierarquiaFormacoes = FormacaoEspecificaDomainService.BuscarFormacoesEspecificas(new FormacaoEspecificaFiltroVO() { SeqCurso = filtro.SeqCurso });

            var nodes = new List<CursoFormacaoEspecificaNodeVO>();
            foreach (var itemHierarquia in hierarquiaFormacoes)
            {
                var node = new CursoFormacaoEspecificaNodeVO()
                {
                    Seq = itemHierarquia.Seq,
                    SeqFormacaoEspecificaSuperior = itemHierarquia.SeqFormacaoEspecificaSuperior,
                    SeqCurso = filtro.SeqCurso.GetValueOrDefault(),
                    Descricao = itemHierarquia.ExibeGrauDescricaoFormacao && !string.IsNullOrEmpty(itemHierarquia.DescricaoGrauAcademico) ?
                                $"[{itemHierarquia.DescricaoTipoFormacaoEspecifica}] {itemHierarquia.Descricao} ({itemHierarquia.DescricaoGrauAcademico})" :
                                $"[{itemHierarquia.DescricaoTipoFormacaoEspecifica}] {itemHierarquia.Descricao}",
                    Ativo = itemHierarquia.Ativo.GetValueOrDefault()
                };
                var formacaoCurso = formacoesCurso.FirstOrDefault(f => f.SeqFormacaoEspecifica == itemHierarquia.Seq);
                if (formacaoCurso != null)
                {
                    node.SeqCursoFormacaoEspecifica = formacaoCurso.SeqCursoFormacaoEspecifica;
                    node.Ativo = DateTime.Today >= formacaoCurso.DataInicioVigencia &&
                                (!formacaoCurso.DataFimVigencia.HasValue || DateTime.Today <= formacaoCurso.DataFimVigencia.Value);
                    node.TipoCursoFormacaoEspecificaFolha = itemHierarquia.TipoFormacaoEspecificaFolha;

                    var cursoOfertaLocalidadeFormacao = this.CursoOfertaLocalidadeFormacaoDomainService.SearchBySpecification(
                        new CursoOfertaLocalidadeFormacaoFilterSpecification() { SeqFormacaoEspecifica = itemHierarquia.Seq, SeqCurso = formacaoCurso.SeqCurso }).ToList();

                    node.PossuiOfertaCursoLocalidade = cursoOfertaLocalidadeFormacao.Any();
                }
                nodes.Add(node);
            }

            return nodes.OrderByDescending(o => o.Ativo).ThenBy(o => o.Descricao).ToList();
        }

        /// <summary>
        /// Busa a descrição da formação específica segundo a regra RN_CSO_024 - Mascara - Formação Específica
        /// </summary>
        /// <param name="seqTipoFormacaoEspecifica">Sequencial do tipo da formação específica</param>
        /// <param name="seqGrauAcademico">Sequencial do grau da formação específica</param>
        /// <returns>Descrição da formação específica segundo a regra RN_CSO_024</returns>
        public string BuscarDescricaoFormacaoEspecifica(long? seqTipoFormacaoEspecifica, long? seqGrauAcademico)
        {
            if (!seqTipoFormacaoEspecifica.HasValue)
                return "";

            var specFormacao = new SMCSeqSpecification<TipoFormacaoEspecifica>(seqTipoFormacaoEspecifica.Value);
            var dadosFormacao = this.TipoFormacaoEspecificaDomainService.SearchProjectionByKey(specFormacao, p => new
            {
                p.Descricao,
                p.Token
            });

            if (dadosFormacao.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.GRAU && seqGrauAcademico.HasValue)
            {
                var specGrau = new SMCSeqSpecification<GrauAcademico>(seqGrauAcademico.Value);
                return this.GrauAcademicoDomainService.SearchProjectionByKey(specGrau, p => p.Descricao);
            }
            else
                return "";
        }

        private void AplicaDadosCurso(CursoFormacaoEspecificaVO vo)
        {
            // Armazena o objeto do curso atual
            var curso = vo.Curso;

            // Armazena a lista de formação atual
            long totalFormacao = 0;

            // Carrega o curso caso esteja null
            if (curso == null || curso.HierarquiasEntidades == null)
            {
                curso = CursoDomainService.SearchByKey(new SMCSeqSpecification<Curso>(vo.SeqCurso), IncludesCurso.NivelEnsino | IncludesCurso.HierarquiasEntidades | IncludesCurso.HierarquiasEntidades_ItemSuperior);
                vo.Curso = curso;
            }

            // Caso tenha HierarquiasEntidades, faz a consulta para buscar as entidades relacionadas afim de buscar a lista de formações específicas das entidades
            if (curso.HierarquiasEntidades != null && curso.HierarquiasEntidades.Count != 0)
            {
                // Buscar apenas da visão organizacional ativa
                var hierarquia = HierarquiaEntidadeDomainService.BuscarHierarquiaVigente(TipoVisao.VisaoOrganizacional);

                // Atribui o sequencial das entidades responsáveis ao VO (para ser repassado ao lookup)
                vo.SeqsEntidadesResponsaveis = curso.HierarquiasEntidades.Where(x => x.SeqHierarquiaEntidade == hierarquia.Seq).Select(s => s.ItemSuperior.SeqEntidade).ToList();

                // Carrega as Formações Específicas
                FormacaoEspecificaFilterSpecification specFormacao = new FormacaoEspecificaFilterSpecification();
                specFormacao.SeqEntidades = vo.SeqsEntidadesResponsaveis;
                totalFormacao = FormacaoEspecificaDomainService.Count(specFormacao);
            }

            if (curso.TipoCurso == TipoCurso.ABI)
                vo.CursoTipoFormacao = CursoTipoFormacao.Cadastro_Oferta;
            else
            {
                // Preenche a lista de detalhe para retorno de acordo com o tipo
                if (totalFormacao == 0)
                    vo.CursoTipoFormacao = CursoTipoFormacao.Cadastro_Simples;
                else
                    vo.CursoTipoFormacao = CursoTipoFormacao.Selecao_Formacao;
            }

            var filtro = new InstituicaoNivelTipoFormacaoEspecificaFilterSpecification()
            {
                SeqNivelEnsino = new List<long>() { vo.Curso.SeqNivelEnsino },
                SeqInstituicaoEnsino = vo.Curso.SeqInstituicaoEnsino,
                TipoCurso = vo.Curso.TipoCurso,
                Ativo = null
            };
            var tiposFormacao = TipoFormacaoEspecificaDomainService.BuscarTipoFormacaoEspecificaPorNivelEnsino(filtro);

            if (vo.Seq == 0)
            {
                vo.GrauAcademicoRequerido = tiposFormacao.Count(c => c.ExigeGrau && c.Ativo) == 1;
                vo.TitulacaoRequeridoPorFormacao = tiposFormacao.Count(c => c.PermiteTitulacao && c.Ativo) == 1;
            }
            else
            {
                vo.GrauAcademicoRequerido = tiposFormacao.Count(w => w.Seq == vo.FormacaoEspecifica.SeqTipoFormacaoEspecifica && w.ExigeGrau) == 1;
                vo.TitulacaoRequeridoPorFormacao = tiposFormacao.Count(w => w.Seq == vo.FormacaoEspecifica.SeqTipoFormacaoEspecifica && w.PermiteTitulacao) == 1;
                vo.SeqFormacaoEspecificaID = vo.SeqFormacaoEspecifica;
                vo.DescricaoTipoFormacaoEspecifica = tiposFormacao.Where(w => w.Seq == vo.FormacaoEspecifica.SeqTipoFormacaoEspecifica).Select(s => s.Descricao).FirstOrDefault();
            }
        }

        /// <summary>
        /// Exclui uma formação específica de curso
        /// </summary>
        /// <param name="seq">Sequencial do curso formação específica</param>
        public void ExcluirCursoFormacaoEspecifica(long seq)
        {
            // Cadastro simples ou ABI precisa remover o registro de associação CursoFormacaoEspecifica e o registro de FormacaoEspecifica
            var cursoformacaoespecifica = BuscarCursoFormacaoEspecifica(seq);

            if (cursoformacaoespecifica.FormacaoEspecifica.CursosOfertas.Count > 0)
                throw new CursoFormacaoEspecificaComOfertaException();

            this.DeleteEntity(seq);

            if (cursoformacaoespecifica.CursoTipoFormacao == CursoTipoFormacao.Cadastro_Simples || cursoformacaoespecifica.CursoTipoFormacao == CursoTipoFormacao.Cadastro_Oferta)
                FormacaoEspecificaDomainService.DeleteEntity(cursoformacaoespecifica.SeqFormacaoEspecifica);
        }

        public void SalvarReplicarCursoFormacaoEspecifica(ReplicarCursoFormacaoEspecificaVO modelo)
        {
            using (var unityOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    foreach (var seqCursoOfertaLocalidade in modelo.SeqsCursosOfertasLocalidades)
                    {
                        var cursoOfertaLocalidadeFormacao = new CursoOfertaLocalidadeFormacao()
                        {
                            SeqCursoOfertaLocalidade = seqCursoOfertaLocalidade,
                            SeqFormacaoEspecifica = modelo.SeqFormacaoEspecifica
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

        public List<long?> BuscarGrauAcademico(CursoFormacaoEspecificaFilterSpecification filtro)
        {
            return this.SearchBySpecification(filtro).Select(s => s.SeqGrauAcademico).ToList();
        }

        public bool CursoFormacaoEspefificaPossuiGrau(long? seqCurso, long? seqFormacaoEspecifica)
        {
            if (seqFormacaoEspecifica.HasValue)
            {
                var filtro = new CursoFormacaoEspecificaFilterSpecification
                {
                    SeqFormacaoEspecifica = seqFormacaoEspecifica,
                    SeqCurso = seqCurso
                };
                var grauFormacao = this.SearchProjectionByKey(filtro, p => p.SeqGrauAcademico);

                return grauFormacao.HasValue;
            }

            return false;
        }

        public List<CursoFormacaoEspecificaVO> BuscarCursoFormacaoEspecifica(CursoFormacaoEspecificaFilterSpecification filtro)
        {
            return this.SearchBySpecification(filtro, IncludesCursoFormacaoEspecifica.Titulacoes).TransformList<CursoFormacaoEspecificaVO>();
        }
    }
}