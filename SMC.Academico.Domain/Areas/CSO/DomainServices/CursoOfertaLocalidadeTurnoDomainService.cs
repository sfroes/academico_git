using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class CursoOfertaLocalidadeTurnoDomainService : AcademicoContextDomain<CursoOfertaLocalidadeTurno>
    {
        #region [ DomainService ]

        private HierarquiaEntidadeDomainService HierarquiaEntidadeDomainService { get => Create<HierarquiaEntidadeDomainService>(); }

        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService { get => Create<HierarquiaEntidadeItemDomainService>(); }

        private TipoEntidadeDomainService TipoEntidadeDomainService { get => Create<TipoEntidadeDomainService>(); }

        #endregion [ DomainService ]

        public long BuscarSeqEntidadeResponsavel(long seqCursoOfertaLocalidadeTurno)
        {
            var seqEntidade = this.SearchProjectionByKey(new SMCSeqSpecification<CursoOfertaLocalidadeTurno>(seqCursoOfertaLocalidadeTurno),
                x => x.CursoOfertaLocalidade.CursoOferta.Curso.HierarquiasEntidades.Where(he => he.HierarquiaEntidade.DataInicioVigencia <= DateTime.Now &&
                                                                                                (he.HierarquiaEntidade.DataFimVigencia == null || he.HierarquiaEntidade.DataFimVigencia >= DateTime.Now) &&
                                                                                                he.HierarquiaEntidade.TipoHierarquiaEntidade.TipoVisao == TipoVisao.VisaoOrganizacional
                                                                                        ).FirstOrDefault().ItemSuperior.SeqEntidade);

            return seqEntidade;
        }

        public List<SMCDatasourceItem> BuscarTurnosPorLocalidadeCusroOfertaSelect(long? seqLocalidade, long? seqCursoOferta)
        {
            var spec = new CursoOfertaLocalidadeTurnoFilterSpecification() { SeqLocalidade = seqLocalidade, SeqCursoOferta = seqCursoOferta };

            var result = this.SearchProjectionBySpecification(
                spec,
                t => new SMCDatasourceItem()
                {
                    Seq = t.SeqTurno,
                    Descricao = t.Turno.Descricao
                });

            return result.ToList();
        }

        /// <summary>
        /// Recupera os sequenciais de todos os curso-oferta-localidade-turno vinculados aos cursos filhos da entidade informada na visão organizacional
        /// </summary>
        /// <param name="seqEntidadeResponsavel">Sequencial da entidade responsável</param>
        /// <returns>Sequenciais dos cursos oferta localidade turno</returns>
        public long[] BuscarSeqsCursoOfertaLocalidadePorEntidadeResponsavel(long seqEntidadeResponsavel)
        {
            var hierarquia = HierarquiaEntidadeDomainService.BuscarHierarquiaVigente(TipoVisao.VisaoOrganizacional);
            if (hierarquia == null)
                return new long[] { };

            // Recupera todas as instâncias da entidade informada na hierarquia organizacional
            var specHierarquiaItem = new HierarquiaEntidadeItemFilterSpecification()
            {
                SeqEntidade = seqEntidadeResponsavel,
                TipoVisaoHierarquia = TipoVisao.VisaoOrganizacional
            };
            var seqRaizes = HierarquiaEntidadeItemDomainService.SearchProjectionBySpecification(specHierarquiaItem, p => p.Seq);
            var hierarquiaItens = HierarquiaEntidadeItemDomainService.BuscarHierarquiaEntidadeItens(seqRaizes);

            var seqTipoCurso = TipoEntidadeDomainService.BuscarTipoEntidadeNaInstituicao(TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO)?.Seq ?? 0;
            var seqCursos = hierarquiaItens.Where(w => w.Entidade.SeqTipoEntidade == seqTipoCurso).Select(s => s.SeqEntidade).ToArray();

            var specCursoOfertaLocalidadeTurno = new SMCContainsSpecification<CursoOfertaLocalidadeTurno, long>(
                p => p.CursoOfertaLocalidade.CursoOferta.SeqCurso, seqCursos);
            return SearchProjectionBySpecification(specCursoOfertaLocalidadeTurno, p => p.Seq).ToArray();
        }

        public CursoOfertaLocalidadeTurnoVO BuscarCursoOfertaLocalidadeTurnoPorCursoOfertaLocalidadeETurno(long seqCursoOfertaLocalidade, long seqTurno)
        {
            var filtro = new CursoOfertaLocalidadeTurnoFilterSpecification()
            {
                SeqCursoOfertaLocalidade = seqCursoOfertaLocalidade,
                SeqTurno = seqTurno
            };

            var registro = this.SearchBySpecification(filtro).FirstOrDefault();

            return registro.Transform<CursoOfertaLocalidadeTurnoVO>();          
        }

        public List<CursoOfertaLocalidadeTurno> BuscarCursoOfertasLocalidadeTurnoAtivoPorEntidadeResponsavel(long seqEntidadeResponsavel)
        {
            if (seqEntidadeResponsavel == 0)
            {
                return new List<CursoOfertaLocalidadeTurno>();
            }

            var seqsCurso = HierarquiaEntidadeItemDomainService
                .BuscarHierarquiaEntidadeItensPorEntidadeVisaoOganizacional(seqEntidadeResponsavel, TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO)
                .ToArray();

            var spec = new CursoOfertaLocalidadeTurnoFilterSpecification()
            {
                SeqsCurso = seqsCurso,
                Ativo = true,
                CursoOfertaAtivo = true,
                CursoOfertaLocalidadeAtivo = true
            };

            var cursoOfertasLocalidadeTurno = this.SearchBySpecification(spec).ToList();

            return cursoOfertasLocalidadeTurno;
        }
    }
}