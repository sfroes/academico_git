using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class GrauAcademicoDomainService : AcademicoContextDomain<GrauAcademico>
    {
        #region [ DomainService ]

        private CursoDomainService CursoDomainService
        {
            get { return this.Create<CursoDomainService>(); }
        }

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService
        {
            get { return this.Create<FormacaoEspecificaDomainService>(); }
        }

        private InstituicaoNivelDomainService InstituicaoNivelDomainService { get => Create<InstituicaoNivelDomainService>(); }

        private CursoOfertaLocalidadeFormacaoDomainService CursoOfertaLocalidadeFormacaoDomainService => Create<CursoOfertaLocalidadeFormacaoDomainService>();

        private AtoNormativoEntidadeDomainService AtoNormativoEntidadeDomainService => this.Create<AtoNormativoEntidadeDomainService>();

        private CursoFormacaoEspecificaDomainService CursoFormacaoEspecificaDomainService => this.Create<CursoFormacaoEspecificaDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Busca a lista de Grau Academico para popular um Select
        /// </summary>
        /// <param name="filtro">Todos os filtros que podem ser aplicados na listagem de grau academico</param>
        /// <returns>Lista de grau academico</returns>
        public List<SMCDatasourceItem> BuscarGrauAcademicoSelect(GrauAcademicoFiltroVO filtro)
        {
            if (filtro.SeqFormacaoEspecifica.HasValue && filtro.SeqFormacaoEspecifica > 0)
            {
                var specFormacao = new CursoFormacaoEspecificaFilterSpecification
                {
                    SeqFormacaoEspecifica = filtro.SeqFormacaoEspecifica.Value
                };
                var grauFormacao = CursoFormacaoEspecificaDomainService.SearchProjectionByKey(specFormacao, p => p.GrauAcademico);

                if (grauFormacao != null)
                {
                    var retorno = new List<SMCDatasourceItem>();
                    retorno.Add(new SMCDatasourceItem(grauFormacao.Seq, grauFormacao.Descricao));
                    return retorno;
                }
            }
            if (filtro.SeqCurso.HasValue && filtro.SeqNivelEnsino == null)
            {
                var cursoNivel = CursoDomainService.SearchByKey(new SMCSeqSpecification<Curso>(filtro.SeqCurso.Value));
                filtro.SeqNivelEnsino = cursoNivel == null ? new List<long>() : new List<long>() { cursoNivel.SeqNivelEnsino };
                filtro.Ativo = true;

                GrauAcademicoFilterSpecification spec = filtro.Transform<GrauAcademicoFilterSpecification>();

                var retorno = this.SearchBySpecification(spec, IncludesGrauAcademico.NiveisEnsino).TransformList<SMCDatasourceItem>();

                return retorno;
            }
            else if ((filtro.SeqNivelEnsino != null && filtro.SeqNivelEnsino.Count > 0) || filtro.RetornarTodos)
            {
                filtro.Ativo = true;

                if (filtro.RetornarTodos)
                {
                    filtro.SeqNivelEnsino = this.InstituicaoNivelDomainService.BuscarNiveisEnsinoReconhecidoLDBSelect().ConvertAll(c => c.Seq);
                }

                GrauAcademicoFilterSpecification spec = filtro.Transform<GrauAcademicoFilterSpecification>();

                var retorno = this.SearchBySpecification(spec, IncludesGrauAcademico.NiveisEnsino).TransformList<SMCDatasourceItem>();

                return retorno;
            }
            else
            {
                GrauAcademicoFilterSpecification spec = filtro.Transform<GrauAcademicoFilterSpecification>();
                spec.SetOrderBy(x => x.Descricao);
                return this.SearchBySpecification(spec).TransformList<SMCDatasourceItem>();

            }

            return new List<SMCDatasourceItem>();
        }

        /// <summary>
        /// Busca a lista de Grau Academico para popular um Select do lookup
        /// </summary>
        /// <param name="filtro">Todos os filtros que podem ser aplicados na listagem de grau academico</param>
        /// <returns>Lista de grau academico</returns>
        public List<SMCDatasourceItem> BuscarGrauAcademicoLookupSelect(GrauAcademicoFiltroVO filtro)
        {
            var retorno = new List<SMCDatasourceItem>();

            GrauAcademicoFilterSpecification spec;

            if (filtro.SeqFormacaoEspecifica.HasValue && filtro.SeqFormacaoEspecifica > 0)
            {
                var specFormacao = new CursoFormacaoEspecificaFilterSpecification
                {
                    SeqFormacaoEspecifica = filtro.SeqFormacaoEspecifica.Value
                };
                specFormacao.SetOrderBy(o => o.GrauAcademico.Descricao);
                var grauFormacao = CursoFormacaoEspecificaDomainService.SearchProjectionByKey(specFormacao, p => p.GrauAcademico);

                if (grauFormacao != null)
                {
                    retorno = new List<SMCDatasourceItem>();
                    retorno.Add(new SMCDatasourceItem(grauFormacao.Seq, grauFormacao.Descricao));
                    return retorno;
                }
            }

            if (filtro.SeqCurso.HasValue && filtro.SeqNivelEnsino == null)
            {
                var cursoNivel = CursoDomainService.SearchByKey(new SMCSeqSpecification<Curso>(filtro.SeqCurso.Value));
                filtro.SeqNivelEnsino = cursoNivel == null ? new List<long>() : new List<long>() { cursoNivel.SeqNivelEnsino };
                filtro.Ativo = true;

                spec = filtro.Transform<GrauAcademicoFilterSpecification>();
                spec.SetOrderBy(o => o.Descricao);
                retorno = this.SearchBySpecification(spec, IncludesGrauAcademico.NiveisEnsino).TransformList<SMCDatasourceItem>();

                return retorno;
            }

            if (filtro.SeqNivelEnsino == null || filtro.SeqNivelEnsino.Count == 0)
                filtro.SeqNivelEnsino = this.InstituicaoNivelDomainService.BuscarNiveisEnsinoReconhecidoLDBSelect().ConvertAll(c => c.Seq);

            spec = filtro.Transform<GrauAcademicoFilterSpecification>();
            spec.SetOrderBy(o => o.Descricao);
            retorno = this.SearchBySpecification(spec, IncludesGrauAcademico.NiveisEnsino).TransformList<SMCDatasourceItem>();

            return retorno;

        }

        public List<SMCDatasourceItem> BuscarGrauAcademicoPorEntidade(long? seqEntidade, long? seqAtoNormativo, long seq)
        {
            var seqsGrauAcademico = CursoOfertaLocalidadeFormacaoDomainService.BuscarGrauAcademicoPorCursoOfertaLocalidadeFormacao(seqEntidade);
            var specGrauAcademico = new GrauAcademicoFilterSpecification() { SeqsGrauAcademico = seqsGrauAcademico, Ativo = true };

            var listaGrauAcademico = new List<SMCDatasourceItem>();
            if (seqsGrauAcademico.Any())
            {
                listaGrauAcademico = this.SearchBySpecification(specGrauAcademico).TransformList<SMCDatasourceItem>();
                if (seqAtoNormativo.HasValue)
                {
                    var seqGrauAcademico = AtoNormativoEntidadeDomainService.BuscarSeqGrauAcademicoAtoNormativoEntidade(seq, seqEntidade, seqAtoNormativo);
                    if (seqGrauAcademico != null && listaGrauAcademico.Any(f => f.Seq == seqGrauAcademico))
                        listaGrauAcademico.FirstOrDefault(f => f.Seq == seqGrauAcademico).Selected = true;
                }
            }
            return listaGrauAcademico;
        }

        public List<SMCDatasourceItem> BuscarGrauAcademicoPorNivelEnsinoCurso(long seqCurso)
        {
            var cursoNivel = CursoDomainService.SearchByKey(new SMCSeqSpecification<Curso>(seqCurso));
            var specGrauAcademico = new GrauAcademicoFilterSpecification()
            {
                Ativo = true,
                SeqNivelEnsino = cursoNivel == null ? new List<long>() : new List<long>() { cursoNivel.SeqNivelEnsino }
            }.SetOrderBy(o => o.Descricao);

            var listaGrauAcademico = new List<SMCDatasourceItem>();

            listaGrauAcademico = this.SearchBySpecification(specGrauAcademico, IncludesGrauAcademico.NiveisEnsino).TransformList<SMCDatasourceItem>();

            return listaGrauAcademico;
        }
    }
}