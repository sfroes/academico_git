using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class CursoService : SMCServiceBase, ICursoService
    {
        #region [ Service ]

        private CursoDomainService CursoDomainService
        {
            get { return this.Create<CursoDomainService>(); }
        }

        private IEntidadeService EntidadeService
        {
            get { return this.Create<IEntidadeService>(); }
        }

        private EntidadeDomainService EntidadeDomainService
        {
            get { return this.Create<EntidadeDomainService>(); }
        }

        #endregion [ Service ]

        /// <summary>
        /// Buscar um curso pelo sequencial
        /// </summary>
        /// <param name="seq">Sequencia do curso a ser recuperado</param>
        /// <returns>Curso recuperado</returns>
        public CursoData BuscarCurso(long seq)
        {
            var curso = CursoDomainService.BuscarCurso(seq);
            return curso.Transform<CursoData>();
        }

        /// <summary>
        /// Buscar um curso com as configurações do tipo entidade curso
        /// </summary>
        /// <param name="seq">Sequencia do curso a ser recuperado</param>
        /// <returns>Curso com as configurações do tipo de entidade curso</returns>
        public CursoData BuscarCursoComConfiguracao(long seq)
        {
            var curso = CursoDomainService.BuscarCursoComHierarquia(seq);
            var configuracao = this.BuscarConfiguracaoDoCurso();
            var cursoData = curso.Transform<CursoData>(configuracao);

            return cursoData;
        }

        /// <summary>
        /// Buscar a configuração do curso para um novo cadastro
        /// </summary>
        /// <returns>A configuração da entidade externada curso</returns>
        public EntidadeData BuscarConfiguracaoDoCurso()
        {
            return EntidadeService.BuscaConfiguracoesEntidadeExternada(TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO);
        }

        /// <summary>
        /// Buscar as possíveis entidades superiories na visão organizacional de um tipo de entidade curso
        /// para montagem de select
        /// </summary>
        /// <param name="apenasAtivas">Considerar todas as entidades com situação ativas ou em ativação</param>
        /// <param name="usarNomeReduzido">Considerar o nome reduzido da entidade</param>
        /// <param name="usarSeqEntidade">Define se será retornada o seq da entidade ou o seq da hierarquia item que representa esta entidade</param>
        /// <param name="considerarGrupoPrograma">Considerar o grupo de programa</param>
        /// <returns>Lista de possíveis entidades superiores do curso</returns>
        public List<SMCDatasourceItem> BuscarHierarquiaSuperiorCursoSelect(bool apenasAtivas = false, bool usarNomeReduzido = false, bool usarSeqEntidade = false, bool considerarGrupoPrograma = false)
        {
            return CursoDomainService.BuscarCursoComHierarquiaSuperiorSelect(apenasAtivas, usarNomeReduzido, usarSeqEntidade, considerarGrupoPrograma);
        }

        /// <summary>
        /// Buscar as situações do tipo de entidade externada curso
        /// </summary>
        /// <returns>Lista de situações de um curso</returns>
        public List<SMCDatasourceItem> BuscarSituacoesCursoSelect()
        {
            return CursoDomainService.BuscarSituacoesCursoSelect();
        }

        /// <summary>
        /// Buscar os cursos que atendam os filtros informados e com a paginação correta
        /// </summary>
        /// <param name="filtros">Filtros da listagem de cursos</param>
        /// <returns>SMCPagerData com a lista de cursos</returns>
        public SMCPagerData<CursoListaData> BuscarCursos(CursoFiltroData filtros)
        {
            CursoFilterSpecification specPaginacao = filtros.Transform<CursoFilterSpecification>();

            var cursos = CursoDomainService.BuscarCursos(specPaginacao);

            if (filtros.ExibirPrimeiroCursoOfertasAtivas)
            {
                foreach (var curso in cursos)
                {
                    curso.CursosOferta = curso.CursosOferta.OrderByDescending(c => c.Ativo).ThenBy(c => c.Descricao).ToList();
                }
            }
            else
            {
                foreach (var curso in cursos)
                {
                    curso.CursosOferta = curso.CursosOferta.OrderBy(c => c.Descricao).ToList();
                }
            }

            return cursos.Transform<SMCPagerData<CursoListaData>>();
        }

        /// <summary>
        /// Buscar os cursos que atendam os filtros informados
        /// </summary>
        /// <param name="seqs">Sequencial do curso</param>
        /// <returns>Lista de cursos
        public List<CursoListaData> BuscarCursosLookupSelect(long[] seqs)
        {
            var spec = new CursoFilterSpecification { SeqsCursos = seqs.ToList() };
            var cursos = CursoDomainService.BuscarCursos(spec);
            return cursos.TransformList<CursoListaData>();
        }

        /// <summary>
        /// Recupera um curso pelo componente curricular
        /// </summary>
        /// <param name="seqGrupoCurricularComponente">Sequencial do componente curricular</param>
        /// <returns>Dados do curso</returns>
        public CursoData BuscarCursoPorGrupoCurricularComponente(long seqGrupoCurricularComponente)
        {
            return CursoDomainService.BuscarCursoPorGrupoCurricularComponente(seqGrupoCurricularComponente).Transform<CursoData>();
        }

        /// <summary>
        /// Salva um curso com suas hierarquias de entidade
        /// </summary>
        /// <param name="curso">Dados do curso a ser salvo</param>
        /// <returns>Sequencial do curso salvo</returns>
        public long SalvarCurso(CursoData curso)
        {
            var cursoVo = curso.Transform<CursoVO>();
            return CursoDomainService.SalvarCurso(cursoVo);
        }

        /// <summary>
        /// Recupera os cursos para replica da formação específica de programa
        /// </summary>
        /// <param name="filtros">Fintros para pesquina dos cursos</param>
        /// <returns>Lista de cursos encontrados</returns>
        public List<SMCDatasourceItem> BuscarCursosReplicarFormacaoEspecificaProgramaSelect(CursoReplicaFormacaoEspecificaProgramaFiltroData filtros)
        {
            return this.CursoDomainService.BuscarCursosReplicarFormacaoEspecificaProgramaSelect(filtros.Transform<CursoReplicaFormacaoEspecificaProgramaFiltroVO>());
        }
    }
}