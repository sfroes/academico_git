using SMC.Academico.Common.Areas.CSO.Exceptions;
using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Helpers;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class InstituicaoNivelModalidadeDomainService : AcademicoContextDomain<InstituicaoNivelModalidade>
    {
        #region [ DomainService ]

        private CursoUnidadeDomainService CursoUnidadeDomainService
        {
            get { return this.Create<CursoUnidadeDomainService>(); }
        }

        private InstituicaoNivelDomainService InstituicaoNivelDomainService
        {
            get { return this.Create<InstituicaoNivelDomainService>(); }
        }

        private HierarquiaEntidadeDomainService HierarquiaEntidadeDomainService
        {
            get { return this.Create<HierarquiaEntidadeDomainService>(); }
        }

        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService
        {
            get { return this.Create<HierarquiaEntidadeItemDomainService>(); }
        }

        private TipoHierarquiaEntidadeDomainService TipoHierarquiaEntidadeDomainService
        {
            get { return this.Create<TipoHierarquiaEntidadeDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca modalidades para a listagem de acordo com o curso unidade
        /// </summary>
        /// <param name="seqCursoUnidade">Sequencial do curso unidade</param>
        /// <returns>Lista de modalidades</returns>
        public List<SMCDatasourceItem> BuscarModalidadesPorCursoUnidadeSelect(long seqCursoUnidade)
        {
            long seqNivelEnsinoCurso = this.CursoUnidadeDomainService
                .SearchByKey(new SMCSeqSpecification<CursoUnidade>(seqCursoUnidade), IncludesCursoUnidade.Curso)
                ?.Curso
                ?.SeqNivelEnsino
                ?? 0;

            long seqInstituicaoNivelEnsinoCurso = this.InstituicaoNivelDomainService
                .SearchProjectionByKey(new InstituicaoNivelFilterSpecification() { SeqNivelEnsino = seqNivelEnsinoCurso }, p => p.Seq);

            var modalidades = this.BuscarModalidadesPorInstituicaoNivelEnsinoSelect(seqInstituicaoNivelEnsinoCurso);

            return modalidades;
        }

        /// <summary>
        /// Busca modalidades para a listagem de acordo com o instituição nível ensino
        /// </summary>
        /// <param name="seqInstituicaoNivelEnsino">Sequencial da instituição nível ensino</param>
        /// <returns>Lista de modalidades</returns>
        public List<SMCDatasourceItem> BuscarModalidadesPorInstituicaoNivelEnsinoSelect(long seqInstituicaoNivelEnsino)
        {
            return this.BuscarModalidadesPorNivelEnsinoSelect(seqInstituicaoNivelEnsino: seqInstituicaoNivelEnsino);
        }

        /// <summary>
        /// Busca modalidades para a listagem de acordo com o instituição nível ensino
        /// </summary>
        /// <param name="seqInstituicaoNivelEnsino">Sequencial da instituição nível ensino</param>
        /// <param name="seqNivelEnsino">Sequencial do nível ensino</param>
        /// <returns>Lista de modalidades</returns>
        public List<SMCDatasourceItem> BuscarModalidadesPorNivelEnsinoSelect(long? seqInstituicaoNivelEnsino = null, long? seqNivelEnsino = null)
        {
            var spec = new InstituicaoNivelModalidadeFilterSpecification()
            {
                SeqInstituicaoNivel = seqInstituicaoNivelEnsino,
                SeqNivelEnsino = seqNivelEnsino
            };

            spec.SetOrderBy(x => x.Modalidade.Descricao);

            var niveisEnsinoInstituicao = this.SearchProjectionBySpecification(spec,
                m => new SMCDatasourceItem()
                {
                    Seq = m.SeqModalidade,
                    Descricao = m.Modalidade.Descricao
                }).ToList();

            if (niveisEnsinoInstituicao.Count == 0)
            {
                var specInstituicaoNivel = new InstituicaoNivelFilterSpecification()
                {
                    Seq = seqInstituicaoNivelEnsino,
                    SeqNivelEnsino = seqNivelEnsino
                };
                var includesInstituicaoNivel = IncludesInstituicaoNivel.NivelEnsino;
                var instituicaoNivelEnsino = this.InstituicaoNivelDomainService.SearchByKey(specInstituicaoNivel, includesInstituicaoNivel);
                throw new InstituicaoNivelModalidadeNaoAssociadoException(instituicaoNivelEnsino.NivelEnsino.Descricao);
            }

            return niveisEnsinoInstituicao;
        }

        /// <summary>
        /// Busca modalidades para a listagem de acordo com o instituição
        /// </summary>
        /// <returns>Lista de modalidades</returns>
        public List<SMCDatasourceItem> BuscarModalidadesPorInstituicaoSelect()
        {
            var modalidades = this.SearchProjectionAll(p => new SMCDatasourceItem()
            {
                Seq = p.SeqModalidade,
                Descricao = p.Modalidade.Descricao
            }, isDistinct: true).ToList().OrderBy(o => o.Descricao).ToList();

            return modalidades;
        }


        /// <summary>
        /// Busca modalidades para a listagem de acordo com o instituição logada
        /// </summary>
        /// <param name="seqInstituicao">Sequencial da instituição</param>
        /// <returns>Lista de modalidades</returns>
        public List<SMCDatasourceItem> BuscarModalidadesPorInstituicaoLogadaSelect(long seqInstituicao)
        {
            var spec = new InstituicaoNivelModalidadeFilterSpecification()
            {
                SeqInstituicao = seqInstituicao
            };
            spec.SetOrderBy(x => x.Modalidade.Descricao);

            var modalidades = this.SearchProjectionBySpecification(spec,
                m => new SMCDatasourceItem()
                {
                    Seq = m.SeqModalidade,
                    Descricao = m.Modalidade.Descricao
                }, isDistinct: true).ToList();

            return modalidades;
        }

        public List<SMCDatasourceItem> BuscarLocalidadesPorModalidadeSelect(long? seqModalidade, long? seqInstituicaoNivel, long? seqCursoUnidade, bool desativarfiltrosHierarquia = false)
        {
            try
            {
                if (desativarfiltrosHierarquia)
                {
                    this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_LOCALIDADES);
                    this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                    this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_POLOS_VIRTUAIS);
                    this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_UNIDADE_GESTORA);

                    HierarquiaEntidadeItemDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_LOCALIDADES);
                    HierarquiaEntidadeItemDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                    HierarquiaEntidadeItemDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_POLOS_VIRTUAIS);
                    HierarquiaEntidadeItemDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_UNIDADE_GESTORA);

                    HierarquiaEntidadeDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_LOCALIDADES);
                    HierarquiaEntidadeDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                    HierarquiaEntidadeDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_POLOS_VIRTUAIS);
                    HierarquiaEntidadeDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_UNIDADE_GESTORA);

                    TipoHierarquiaEntidadeDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_LOCALIDADES);
                    TipoHierarquiaEntidadeDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                    TipoHierarquiaEntidadeDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_POLOS_VIRTUAIS);
                    TipoHierarquiaEntidadeDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_UNIDADE_GESTORA);
                }

                const TipoVisao visaoLocalidades = TipoVisao.VisaoLocalidades;

                // Busca a hierarquia na visão vigente da instituição de ensino
                var hierarquia = this.HierarquiaEntidadeDomainService.BuscarHierarquiaVigente(visaoLocalidades);

                // Se não encontrou a hierarquia, erro
                if (hierarquia == null)
                {
                    throw new HierarquiaEntidadeNaoConfiguradaException(visaoLocalidades.SMCGetDescription());
                }

                // Recupera o sequencial da unidade do curso unidade
                var seqUnidade = this.CursoUnidadeDomainService.SearchProjectionByKey(new SMCSeqSpecification<CursoUnidade>(seqCursoUnidade.GetValueOrDefault()), p =>
                    p.HierarquiasEntidades.FirstOrDefault().ItemSuperior.SeqEntidade);

                // Recupera o seq da unidade na hierarquia com a visão de localidades
                var specHierarquiaCursoUnidade = new HierarquiaEntidadeItemFilterSpecification()
                {
                    SeqEntidade = seqUnidade,
                    SeqHierarquiaEntidade = hierarquia.Seq,
                    TipoVisaoHierarquia = visaoLocalidades
                };
                var seqHierarquiaCursoUnidade = this.HierarquiaEntidadeItemDomainService.SearchProjectionByKey(specHierarquiaCursoUnidade, p => p.Seq);

                //Recupera os tipos de localidade, pelo nivel ensino e modalidade
                var specInstituicaoNivelModalidade = new InstituicaoNivelModalidadeFilterSpecification() { SeqInstituicaoNivel = seqInstituicaoNivel, SeqModalidade = seqModalidade };

                // Recupera o seq do tipo localidade na instituição atual
                var listaTiposLocalidade = this.SearchProjectionByKey(specInstituicaoNivelModalidade,
                                                    p => p.TiposEntidadeLocalidade.Select(s => s.SeqTipoEntidadeLocalidade));

                // Recupera a hierarquia de entidades do curso unidade informado
                var hierarquiaCursoUnidade = this.HierarquiaEntidadeItemDomainService.BuscarHierarquiaEntidadeItens(seqItemRaiz: seqHierarquiaCursoUnidade);

                var localidades = hierarquiaCursoUnidade
                    .Where(w => listaTiposLocalidade.Contains(w.Entidade.SeqTipoEntidade))
                    .Select(s => new SMCDatasourceItem(s.SeqEntidade, s.Entidade.Nome))
                    .ToList();

                // Caso não encontre as localidades tenta identificar a falha
                /* Retirar essa consistência. Se não encontrou nenhuma localidade, select monta vazio! (TSK 57025)
                if (localidades.Count == 0)
                {
                    // Verifica se configurou o tipo de hierarquia com visão localidade
                    this.TipoHierarquiaEntidadeDomainService
                        .BuscarTipoHierarquiaEntidadeNaInstituicao(visaoLocalidades);

                    // Caso tenha tipo de hierarquia com a visão configurada e não tenha itens assume que não têm árvore
                    throw new TipoHierarquiaEntidadeSemArvoreException(visaoLocalidades.SMCGetDescription());
                }
                */
                return localidades;
            }
            finally
            {
                FilterHelper.AtivarFiltros(this);
            }
        }
    }
}