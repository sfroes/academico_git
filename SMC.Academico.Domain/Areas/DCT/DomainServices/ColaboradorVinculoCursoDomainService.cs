using SMC.Academico.Common.Areas.DCT.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.Domain.Helpers;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.DCT.DomainServices
{
    public class ColaboradorVinculoCursoDomainService : AcademicoContextDomain<ColaboradorVinculoCurso>
    {
        #region DomainService

        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService => Create<CursoOfertaLocalidadeDomainService>();

        private ColaboradorVinculoDomainService ColaboradorVinculoDomainService => Create<ColaboradorVinculoDomainService>();

        #endregion


        public long[] BuscarColaboradorVinculoCursos(ColaboradorFiltroVO filtro)
        {
            if (filtro.TipoAtividade.HasValue ||
                filtro.SeqsEntidadesVinculo.SMCAny() ||
                filtro.SeqCursoOferta.HasValue ||
                filtro.SeqsCursoOferta.SMCAny() ||
                filtro.SeqCursoOfertaLocalidade.HasValue ||
                filtro.SeqsCursoOfertaLocalidade.SMCAny())
            {
                try
                {
                    if (filtro.IgnorarFiltros)
                    {
                        FilterHelper.AtivarApenasFiltros(this, FILTER.INSTITUICAO_ENSINO);
                    }

                    //Como a pesquisa por SeqCursoOfertaLocalidade ou SeqsCursoOfertaLocalidade são
                    //as mesmas adicionamos ao array caso exista
                    if (filtro.SeqCursoOfertaLocalidade.HasValue)
                    {
                        if (filtro.SeqsCursoOfertaLocalidade.SMCAny() && !filtro.SeqsCursoOfertaLocalidade.Contains(filtro.SeqCursoOfertaLocalidade.Value))
                        {
                            filtro.SeqsCursoOfertaLocalidade.Append(filtro.SeqCursoOfertaLocalidade.Value);
                        }
                        else if (!filtro.SeqsCursoOfertaLocalidade.SMCAny())
                        {
                            filtro.SeqsCursoOfertaLocalidade = new long[] { filtro.SeqCursoOfertaLocalidade.Value};
                        }
                        filtro.SeqCursoOfertaLocalidade = null;
                    }

                    var spec = new ColaboradorVinculoCursoFilterSpecification()
                    {
                        TipoAtividadeColaborador = filtro.TipoAtividade,
                        SeqsEntidadesVinculo = filtro.SeqsEntidadesVinculo,
                        SeqCursoOferta = filtro.SeqCursoOferta,
                        SeqsCursoOferta = filtro.SeqsCursoOferta,
                        SeqCursoOfertaLocalidade = filtro.SeqCursoOfertaLocalidade,
                        SeqsCursoOfertaLocalidade = filtro.SeqsCursoOfertaLocalidade
                    };
                    return this.SearchProjectionBySpecification(spec, p => p.Seq).ToArray();
                }
                finally
                {
                    FilterHelper.AtivarFiltros(this);
                }
            }
            return null;
        }

        /// <summary>
        /// Listar todos os cursos do vinculados ao colaborador
        /// </summary>
        /// <param name="spec">Filtros de pesquisa</param>
        /// <returns>Lista de colaboradores vinculo curso</returns>
        public List<ColaboradorVinculoCursoVO> ListarColaboradorVinculoCursos(ColaboradorVinculoCursoFiltroVO filtro)
        {
            var includes = IncludesColaboradorVinculoCurso.ColaboradorVinculo | IncludesColaboradorVinculoCurso.CursoOfertaLocalidade | IncludesColaboradorVinculoCurso.Atividades;
            ColaboradorVinculoCursoFilterSpecification spec = new ColaboradorVinculoCursoFilterSpecification() { SeqColaboradorVinculo = filtro.SeqColaboradorVinculo };

            var resultQuery = this.SearchBySpecification(spec, includes).ToList();
            var retorno = resultQuery.TransformList<ColaboradorVinculoCursoVO>();

            foreach (var item in retorno)
            {
                item.TiposAtividades = this.ColaboradorVinculoDomainService.BuscarTiposAtividadeCursoOfertaLocalidadeSelect(item.SeqCursoOfertaLocalidade);
                item.TipoAtividadeColaborador = resultQuery.FirstOrDefault(f => f.Seq == item.Seq).Atividades.Select(s => s.TipoAtividadeColaborador).ToList();
            }

            return retorno;
        }

        /// <summary>
        /// Salvar colaborador vinculo curso
        /// </summary>
        /// <param name="modelo">Dados do vinculo a ser gravado</param>
        public void SalvarColaboradorVinculoCurso(ColaboradorVinculoVO modelo)
        {
            // Recupera o vinculo informado do banco para preservar os campos que não são considerados na tela
            var vinculo = ColaboradorVinculoDomainService.SearchByKey(modelo.Seq, IncludesColaboradorVinculo.Cursos_Atividades);
            // Troca a lista de cursos com suas ativades pelos dados informados na tela
            vinculo.Cursos = new List<ColaboradorVinculoCurso>();
            if (modelo.Cursos.SMCAny())
            {
                foreach (var cursoVO in modelo.Cursos)
                {
                    var curso = cursoVO.Transform<ColaboradorVinculoCurso>();
                    curso.Atividades = cursoVO.TipoAtividadeColaborador.Select(s => new ColaboradorVinculoAtividade() { TipoAtividadeColaborador = s }).ToList();
                    vinculo.Cursos.Add(curso);
                }
            }
            // Deixa o framework tratar os relacionamentos criados, atualizados ou removidos
            ColaboradorVinculoDomainService.SaveEntity(vinculo);
        }
    }
}