using SMC.Academico.Common.Areas.CSO.Exceptions;
using SMC.Academico.Common.Areas.CSO.Exceptions.CursoOfertaLocalidade;
using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.Validators;
using SMC.Academico.Domain.Helpers;
using SMC.Framework;
using SMC.Framework.DataFilters;
using SMC.Framework.Domain.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Validation;
using SMC.Localidades.Common.Areas.LOC.Enums;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using SMC.Notificacoes.Common.Areas.NTF.Enums;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class CursoOfertaLocalidadeDomainService : AcademicoContextDomain<CursoOfertaLocalidade>
    {
        #region [ Services ]

        private INotificacaoService NotificacaoService => this.Create<INotificacaoService>();

        private ILocalidadeService LocalidadeService => this.Create<ILocalidadeService>();

        #endregion [ Services ]

        #region [ DomainService ]

        private CurriculoCursoOfertaDomainService CurriculoCursoOfertaDomainService => Create<CurriculoCursoOfertaDomainService>();

        private CursoOfertaLocalidadeFormacaoDomainService CursoOfertaLocalidadeFormacaoDomainService => Create<CursoOfertaLocalidadeFormacaoDomainService>();

        private CursoOfertaDomainService CursoOfertaDomainService => Create<CursoOfertaDomainService>();

        private CursoUnidadeDomainService CursoUnidadeDomainService => Create<CursoUnidadeDomainService>();

        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService => Create<FormacaoEspecificaDomainService>();

        private HierarquiaEntidadeDomainService HierarquiaEntidadeDomainService => Create<HierarquiaEntidadeDomainService>();

        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService => Create<HierarquiaEntidadeItemDomainService>();

        private InstituicaoNivelDomainService InstituicaoNivelDomainService => Create<InstituicaoNivelDomainService>();

        private InstituicaoNivelModalidadeDomainService InstituicaoNivelModalidadeDomainService => Create<InstituicaoNivelModalidadeDomainService>();

        private TipoEntidadeDomainService TipoEntidadeDomainService => Create<TipoEntidadeDomainService>();

        private TipoHierarquiaEntidadeDomainService TipoHierarquiaEntidadeDomainService => Create<TipoHierarquiaEntidadeDomainService>();

        private CursoDomainService CursoDomainService => Create<CursoDomainService>();

        private MatrizCurricularOfertaExcecaoLocalidadeDomainService MatrizCurricularOfertaExcecaoLocalidadeDomainService => Create<MatrizCurricularOfertaExcecaoLocalidadeDomainService>();

        private EntidadeConfiguracaoNotificacaoDomainService EntidadeConfiguracaoNotificacaoDomainService => Create<EntidadeConfiguracaoNotificacaoDomainService>();

        private SituacaoEntidadeDomainService SituacaoEntidadeDomainService => Create<SituacaoEntidadeDomainService>();

        private AtoNormativoDomainService AtoNormativoDomainService => Create<AtoNormativoDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Busca ofertas de curso com seus cursos, niveis e localidades
        /// </summary>
        /// <param name="filtroVO">Filtros para a pesquisa</param>
        /// <returns>Dados das ofertas de curso paginados</returns>
        public SMCPagerData<CursoOfertaLocalidade> BuscarCursoOfertasLocalidade(CursoOfertaLocalidadeFiltroVO filtroVO)
        {
            var filtros = filtroVO.Transform<CursoOfertaLocalidadeFilterSpecification>();
            filtros.SeqsNiveisEnsino = filtros.SeqsNiveisEnsino != null && filtros.SeqsNiveisEnsino.Any(a => a != 0) ? filtros.SeqsNiveisEnsino : null;

            // Se nao foi informado 
            List<long> seqEntidadesResponsaveis = new List<long>();

            // só será executado a rawWquery, se tiver sido enviado seqentidade ou uma lista de seqentidades
            if (filtroVO.SeqsEntidadesResponsaveis == null && filtroVO.SeqEntidadeResponsavel != 0)
            {
                seqEntidadesResponsaveis.Add(filtroVO.SeqEntidadeResponsavel);
            }
            else if (filtroVO.SeqsEntidadesResponsaveis != null && filtroVO.SeqEntidadeResponsavel == 0)
            {
                seqEntidadesResponsaveis.AddRange(filtroVO.SeqsEntidadesResponsaveis);
            }

            // Busca as entidades que estão na hierarquia abaixo da entidade responsável que são do tipo curso
            /*select seq_entidade from ORG.fn_buscar_entidade_filha_hierarquia(ENTIDADE_ORIGEM, TIPO_VISAO, TIPO_ENTIDADE_DESTINO)*/
            if (seqEntidadesResponsaveis.Any(a => a != 0))
            {
                var seqTipoEntidadeCurso = TipoEntidadeDomainService.BuscarTipoEntidadeNaInstituicao(TOKEN_TIPO_ENTIDADE.CURSO).Seq;
                List<long> seqsEntidades = new List<long>();
                foreach (var seqEntidadeResponsavel in seqEntidadesResponsaveis)
                {
                    var seqs = RawQuery<long>($"select seq_entidade from ORG.fn_buscar_entidade_filha_hierarquia({seqEntidadeResponsavel}, {(int)TipoVisao.VisaoOrganizacional}, {seqTipoEntidadeCurso})");
                    if (seqs != null)
                        seqsEntidades.AddRange(seqs);
                }
                filtros.SeqsEntidades = seqsEntidades.Distinct().ToList();
            }

            try
            {
                int total = 0;
                var includes = IncludesCursoOfertaLocalidade.CursoOferta_Curso_NivelEnsino
                             | IncludesCursoOfertaLocalidade.HistoricoSituacoes_SituacaoEntidade
                             | IncludesCursoOfertaLocalidade.HierarquiasEntidades_ItemSuperior_Entidade
                             | IncludesCursoOfertaLocalidade.Modalidade
                             | IncludesCursoOfertaLocalidade.Turnos;

                // Configura a ordenação por Nível de Ensino / Curso / Oferta de Curso / Localidade /  Modalidade
                var orderBy = new List<SMCSortInfo>();
                orderBy.Add(new SMCSortInfo("CursoOferta.Curso.NivelEnsino.Descricao", SMCSortDirection.Ascending));
                orderBy.Add(new SMCSortInfo("CursoOferta.Curso.Nome", SMCSortDirection.Ascending));
                orderBy.Add(new SMCSortInfo("CursoOferta.Descricao", SMCSortDirection.Ascending));
                orderBy.Add(new SMCSortInfo("Modalidade.Descricao", SMCSortDirection.Ascending));
                filtros.SetOrderBy(orderBy);
                if (filtroVO.IgnorarFiltros)
                {
                    FilterHelper.AtivarApenasFiltros(this, FILTER.INSTITUICAO_ENSINO);
                }
                else
                {
                    if (SMCDataFilterHelper.UserRequiresDataFilters(false))
                    {
                        // Recupera os cursos que respeitam a regra RN_USG_005 - Filtro por Entidade Responsável na visão organizacional,
                        // já que o curso oferta localidade só existe na hierarquia com a visão localidade
                        filtros.SeqsCursos = CursoDomainService.SearchProjectionAll(p => p.Seq).ToArray();
                    }
                    HierarquiaEntidadeItemDomainService.DesativarFiltrosHierarquiaItem(this);
                    HierarquiaEntidadeItemDomainService.AtivarFiltroHierarquiaItem(TipoVisao.VisaoLocalidades, this);
                }
                var ofertas = this.SearchBySpecification(filtros, out total, includes);

                return new SMCPagerData<CursoOfertaLocalidade>(ofertas, total);
            }
            finally
            {
                FilterHelper.AtivarFiltros(this);
            }
        }

        /// <summary>
        /// Busca ofertas de curso com seus cursos, niveis e localidades
        /// </summary>
        /// <param name="filtroVO">Filtros para a pesquisa</param>
        /// <returns>Dados das ofertas de curso</returns>
        public List<CursoOfertaLocalidade> BuscarCursoOfertasLocalidadePorToken(CursoOfertaLocalidadeFiltroVO filtroVO)
        {
            var filtros = filtroVO.Transform<CursoOfertaLocalidadeFilterSpecification>();

            // Busca as entidades que estão na hierarquia abaixo da entidade responsável que são do tipo curso
            /*select seq_entidade from ORG.fn_buscar_entidade_filha_hierarquia(ENTIDADE_ORIGEM, TIPO_VISAO, TIPO_ENTIDADE_DESTINO)*/
            if (filtroVO.SeqsEntidadesResponsaveis != null)
            {
                var seqTipoEntidade = TipoEntidadeDomainService.BuscarTipoEntidadeNaInstituicao(filtroVO.TokenTipoEntidade).Seq;
                List<long> seqsEntidades = new List<long>();
                foreach (var seqEntidadeResponsavel in filtroVO.SeqsEntidadesResponsaveis)
                {
                    var seqs = RawQuery<long>($"select seq_entidade from ORG.fn_buscar_entidade_filha_hierarquia({seqEntidadeResponsavel}, {(int)TipoVisao.VisaoOrganizacional}, {seqTipoEntidade})");
                    if (seqs != null)
                        seqsEntidades.AddRange(seqs);
                }
                filtros.SeqsEntidades = seqsEntidades.Distinct().ToList();
            }

            try
            {
                int total = 0;
                var includes = IncludesCursoOfertaLocalidade.CursoOferta_Curso_NivelEnsino
                             | IncludesCursoOfertaLocalidade.HistoricoSituacoes_SituacaoEntidade
                             | IncludesCursoOfertaLocalidade.HierarquiasEntidades_ItemSuperior_Entidade
                             | IncludesCursoOfertaLocalidade.Modalidade
                             | IncludesCursoOfertaLocalidade.Turnos;

                // Configura a ordenação por Nível de Ensino / Curso / Oferta de Curso / Localidade /  Modalidade
                var orderBy = new List<SMCSortInfo>();
                orderBy.Add(new SMCSortInfo("CursoOferta.Curso.NivelEnsino.Descricao", SMCSortDirection.Ascending));
                orderBy.Add(new SMCSortInfo("CursoOferta.Curso.Nome", SMCSortDirection.Ascending));
                orderBy.Add(new SMCSortInfo("CursoOferta.Descricao", SMCSortDirection.Ascending));
                orderBy.Add(new SMCSortInfo("Modalidade.Descricao", SMCSortDirection.Ascending));
                filtros.SetOrderBy(orderBy);

                if (filtroVO.IgnorarFiltros)
                {
                    FilterHelper.AtivarApenasFiltros(this, FILTER.INSTITUICAO_ENSINO);
                }
                else
                {
                    if (SMCDataFilterHelper.UserRequiresDataFilters(false))
                    {
                        // Recupera os cursos que respeitam a regra RN_USG_005 - Filtro por Entidade Responsável na visão organizacional,
                        // já que o curso oferta localidade só existe na hierarquia com a visão localidade
                        filtros.SeqsCursos = CursoDomainService.SearchProjectionAll(p => p.Seq).ToArray();
                    }
                    HierarquiaEntidadeItemDomainService.DesativarFiltrosHierarquiaItem(this);
                    HierarquiaEntidadeItemDomainService.AtivarFiltroHierarquiaItem(TipoVisao.VisaoLocalidades, this);
                }

                var ofertas = this.SearchBySpecification(filtros, out total, includes).ToList();

                return ofertas;
            }
            finally
            {
                FilterHelper.AtivarFiltros(this);
            }
        }

        /// <summary>
        /// Busca ofertas de curso com seus cursos, niveis e localidades para o retorno do lookup
        /// </summary>
        /// <param name="seqs">Sequenciais selecionados</param>
        /// <returns>Dados das ofertas de curso para o grid do lookup</returns>
        public List<CursoOfertaLocalidade> BuscarCursoOfertasLocalidadeGridLookup(long[] seqs)
        {
            try
            {
                var includes = IncludesCursoOfertaLocalidade.CursoOferta_Curso_NivelEnsino
             | IncludesCursoOfertaLocalidade.HistoricoSituacoes_SituacaoEntidade
             | IncludesCursoOfertaLocalidade.HierarquiasEntidades_ItemSuperior_Entidade
             | IncludesCursoOfertaLocalidade.Modalidade;
                HierarquiaEntidadeItemDomainService.DesativarFiltrosHierarquiaItem(this);
                HierarquiaEntidadeItemDomainService.AtivarFiltroHierarquiaItem(TipoVisao.VisaoLocalidades, this);
                var specIn = new SMCContainsSpecification<CursoOfertaLocalidade, long>(p => p.Seq, seqs);

                return this.SearchBySpecification(specIn, includes)
                    .ToList();
            }
            finally
            {
                HierarquiaEntidadeItemDomainService.AtivarFiltrosHierarquiaItem(this);
            }
        }

        /// <summary>
        /// Busca o curso oferta localidade para o cabeçalho de acordo com o curso unidade
        /// </summary>
        /// <param name="seqCursoUnidade">Sequencial do curso unidade</param>
        /// <returns>Dados do cabeçalho de curso oferta localidade</returns>
        public CursoOfertaLocalidadeCabecalhoVO BurcarCursoOfertaLocalidadeCabecalhoPorCursoUnidade(long seqCursoUnidade, bool desativarfiltrosHierarquia = false)
        {
            try
            {
                if (desativarfiltrosHierarquia)
                {
                    CursoUnidadeDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_LOCALIDADES);
                    CursoUnidadeDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                    CursoUnidadeDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_POLOS_VIRTUAIS);
                    CursoUnidadeDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_UNIDADE_GESTORA);
                }

                var cursoUnidade = this.CursoUnidadeDomainService.SearchByKey(new SMCSeqSpecification<CursoUnidade>(seqCursoUnidade), IncludesCursoUnidade.HierarquiasEntidades_ItemSuperior_Entidade | IncludesCursoUnidade.Curso);

                return new CursoOfertaLocalidadeCabecalhoVO()
                {
                    Seq = cursoUnidade.Curso.Seq,
                    NomeCurso = cursoUnidade.Curso.Nome,
                    NomeUnidade = cursoUnidade.HierarquiasEntidades.Single().ItemSuperior.Entidade.Nome
                };
            }
            finally
            {
                FilterHelper.AtivarFiltros(this);
            }
        }

        /// <summary>
        /// Busca o curso oferta localidade de acordo com o sequencial da entidade
        /// </summary>
        /// <param name="seqEntidade">Sequencial da entidade responsável</param>
        /// <returns>Dados da configuração do curso oferta localidade e sequencial do curso e instituição nível</returns>
        public CursoOfertaLocalidadeVO BuscarConfiguracoesCursoOfertaLocalidade(long seqEntidade)
        {
            var entidadeData = new CursoOfertaLocalidadeVO();

            // Recupera o sequencial do curso com o seqEntidade do CursoUnidade
            var specCursoUnidade = new SMCSeqSpecification<CursoUnidade>(seqEntidade);
            var seqCurso = this.CursoUnidadeDomainService.SearchProjectionByKey(specCursoUnidade, p => p.SeqCurso);

            // recupera o curso com ofertas, histórico e itens de hierarquia
            var includesCurso = IncludesCurso.CursosOferta
                              | IncludesCurso.HistoricoSituacoes_SituacaoEntidade
                              | IncludesCurso.HierarquiasEntidades;
            var specCurso = new SMCSeqSpecification<Curso>(seqCurso);
            var curso = this.CursoDomainService.SearchByKey(specCurso, includesCurso);

            entidadeData.SeqCurso = curso.Seq;

            // Recupera a instituição nível equivalente a do curso
            var specNivel = new InstituicaoNivelFilterSpecification() { SeqInstituicaoEnsino = curso.SeqInstituicaoEnsino, SeqNivelEnsino = curso.SeqNivelEnsino };
            long seqNivelInstituicao = this.InstituicaoNivelDomainService
                .SearchProjectionByKey(specNivel, p => p.Seq);

            entidadeData.SeqNivelEnsino = curso.SeqNivelEnsino;
            entidadeData.SeqInstituicaoNivel = seqNivelInstituicao;
            entidadeData.SeqSituacaoAtual = curso.HistoricoSituacoes.FirstOrDefault(f => f.Atual)?.SeqSituacaoEntidade ?? 0;
            entidadeData.SeqSituacaoAtualCursoOfertaLocalidade = entidadeData.SeqSituacaoAtual;
            entidadeData.SeqsEntidadesResponsaveis = curso?.HierarquiasEntidades.Select(s => s.SeqItemSuperior ?? 0).ToList();

            // Verificação de pré-requisitos
            // Oferta de curso
            if (curso.CursosOferta.SMCCount() == 0)
            {
                throw new OfertaCursoNaoCadastradaException(curso.Nome);
            }

            return entidadeData;
        }

        /// <summary>
        /// Busca o curso oferta localidade com suas dependências
        /// </summary>
        /// <param name="seq">Sequencial do curso oferta localidade</param>
        /// <returns>Dados do curso oferta localidade</returns>
        public CursoOfertaLocalidadeVO BuscarCursoOfertaLocalidade(long seq, bool desativarfiltrosHierarquia = false)
        {
            var includes = IncludesCursoOfertaLocalidade.ArquivoLogotipo
                         | IncludesCursoOfertaLocalidade.Classificacoes_Classificacao
                         | IncludesCursoOfertaLocalidade.CursoOferta
                         | IncludesCursoOfertaLocalidade.Enderecos
                         | IncludesCursoOfertaLocalidade.EnderecosEletronicos
                         | IncludesCursoOfertaLocalidade.FormacoesEspecificas
                         | IncludesCursoOfertaLocalidade.HierarquiasEntidades_ItemSuperior_Entidade
                         | IncludesCursoOfertaLocalidade.Telefones
                         | IncludesCursoOfertaLocalidade.Turnos;

            try
            {
                if (desativarfiltrosHierarquia)
                {
                    HierarquiaEntidadeItemDomainService.DesativarFiltrosHierarquiaItem(this);
                    HierarquiaEntidadeItemDomainService.AtivarFiltroHierarquiaItem(TipoVisao.VisaoLocalidades, this);
                }

                var cursoOfertaLocalidadeDominio = this.SearchByKey(new SMCSeqSpecification<CursoOfertaLocalidade>(seq), includes);

                var cursoOfertaLocalidadeConfiguracao = this.BuscarConfiguracoesCursoOfertaLocalidade(cursoOfertaLocalidadeDominio.SeqCursoUnidade);

                var cursoOfertaLocalidadeConfiguradoData = cursoOfertaLocalidadeDominio.Transform<CursoOfertaLocalidadeVO>(cursoOfertaLocalidadeConfiguracao);

                cursoOfertaLocalidadeConfiguradoData.FormacoesEspecificas = this.ConverterFormacoesEspecificas(cursoOfertaLocalidadeDominio);
                cursoOfertaLocalidadeConfiguradoData.HierarquiasClassificacoes = this.EntidadeDomainService.GerarEntidadeClassificacoes(cursoOfertaLocalidadeDominio.SeqTipoEntidade, cursoOfertaLocalidadeDominio.Classificacoes);
                cursoOfertaLocalidadeConfiguradoData.SeqLocalidade = cursoOfertaLocalidadeDominio.HierarquiasEntidades.First().ItemSuperior.SeqEntidade;
                cursoOfertaLocalidadeConfiguradoData.NomeLocalidade = cursoOfertaLocalidadeDominio.HierarquiasEntidades.First().ItemSuperior.Entidade.Nome;
                cursoOfertaLocalidadeConfiguradoData.DescricaoCursoOferta = cursoOfertaLocalidadeDominio.CursoOferta.Descricao;
                cursoOfertaLocalidadeConfiguradoData.SeqSituacaoAtualCursoOfertaLocalidade = cursoOfertaLocalidadeConfiguradoData.SeqSituacaoAtual;

                cursoOfertaLocalidadeConfiguradoData.AtivaAbaAtoNormativo = TipoEntidadeDomainService.PermiteAtoNormativo(cursoOfertaLocalidadeDominio.SeqTipoEntidade, TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO_OFERTA_LOCALIDADE);
                if (cursoOfertaLocalidadeConfiguradoData.AtivaAbaAtoNormativo)
                {
                    var retorno = AtoNormativoDomainService.BuscarAtoNormativoPorEntidade(seqEntidade: cursoOfertaLocalidadeConfiguradoData.Seq, SeqInstituicaoEnsino: cursoOfertaLocalidadeDominio.SeqInstituicaoEnsino);

                    cursoOfertaLocalidadeConfiguradoData.HabilitaColunaGrauAcademico = retorno.Where(w => w.DescricaoGrauAcademico != null).Select(s => s.DescricaoGrauAcademico).Any();
                    cursoOfertaLocalidadeConfiguradoData.AtoNormativo = retorno;
                }

                return cursoOfertaLocalidadeConfiguradoData;
            }
            finally
            {
                FilterHelper.AtivarFiltros(this);
            }
        }

        /// <summary>
        /// Busca o curso oferta localidade para a listagem de acordo com o curso unidade
        /// </summary>
        /// <param name="seqCursoUnidade">Sequencial do curso unidade</param>
        /// <returns>Lista de curso oferta localidade</returns>
        public List<SMCDatasourceItem> BuscarCursoOfertaLocalidadePorCursoUnidadeSelect(long seqCursoUnidade)
        {
            var ofertas = this.CursoUnidadeDomainService
                .SearchByKey(new SMCSeqSpecification<CursoUnidade>(seqCursoUnidade), IncludesCursoUnidade.Curso_CursosOferta)
                ?.Curso
                ?.CursosOferta
                ?? new List<CursoOferta>();

            return ofertas
                .Select(s => new SMCDatasourceItem(s.Seq, s.Descricao))
                .OrderBy(o => o.Descricao)
                .ToList();
        }

        /// <summary>
        /// Busca as localidades de uma unidade para um curso oferta localidade
        /// </summary>
        /// <param name="seqCursoUnidade">Sequencial do curso unidade</param>
        /// <returns>Lista de localidades</returns>
        public List<SMCDatasourceItem> BuscarLocalidadesTipoCursoOfertaLocalidadeSelect(long seqCursoUnidade)
        {
            const TipoVisao visaoLocalidades = TipoVisao.VisaoLocalidades;

            // Busca a hierarquia na visão vigente da instituição de ensino
            var hierarquia = this.HierarquiaEntidadeDomainService.BuscarHierarquiaVigente(visaoLocalidades);

            // Se não encontrou a hierarquia, erro
            if (hierarquia == null)
            {
                throw new HierarquiaEntidadeNaoConfiguradaException(visaoLocalidades.SMCGetDescription());
            }

            // Recupera o sequencial da unidade do curso unidade
            var seqUnidade = this.CursoUnidadeDomainService.SearchProjectionByKey(new SMCSeqSpecification<CursoUnidade>(seqCursoUnidade), p =>
                p.HierarquiasEntidades.FirstOrDefault().ItemSuperior.SeqEntidade);

            // Recupera o seq da unidade na hierarquia com a visão de localidades
            var specHierarquiaCursoUnidade = new HierarquiaEntidadeItemFilterSpecification()
            {
                SeqEntidade = seqUnidade,
                SeqHierarquiaEntidade = hierarquia.Seq,
                TipoVisaoHierarquia = visaoLocalidades
            };
            var seqHierarquiaCursoUnidade = this.HierarquiaEntidadeItemDomainService.SearchProjectionByKey(specHierarquiaCursoUnidade, p => p.Seq);

            // Recupera o seq do tipo localidade na instituição atual
            var seqTipoLocalidade = this.TipoEntidadeDomainService.BuscarTipoEntidadeNaInstituicao(TOKEN_TIPO_ENTIDADE.LOCALIDADE).Seq;

            // Recupera a hierarquia de entidades do curso unidade informado
            var hierarquiaCursoUnidade = this.HierarquiaEntidadeItemDomainService.BuscarHierarquiaEntidadeItens(seqItemRaiz: seqHierarquiaCursoUnidade);

            var localidades = hierarquiaCursoUnidade
                .Where(w => w.Entidade.SeqTipoEntidade == seqTipoLocalidade)
                .Select(s => new SMCDatasourceItem(s.SeqEntidade, s.Entidade.Nome))
                .ToList();

            // Caso não encontre as localidades tenta identificar a falha
            if (localidades.Count == 0)
            {
                // Verifica se configurou o tipo de hierarquia com visão localidade
                this.TipoHierarquiaEntidadeDomainService
                    .BuscarTipoHierarquiaEntidadeNaInstituicao(visaoLocalidades);

                // Caso tenha tipo de hierarquia com a visão configurada e não tenha itens assume que não têm árvore
                throw new TipoHierarquiaEntidadeSemArvoreException(visaoLocalidades.SMCGetDescription());
            }

            return localidades;
        }

        public List<SMCDatasourceItem> BuscarLocalidadesPorModalidadeSelect(long? seqModalidade, long? seqInstituicaoNivel, long? seqCursoUnidade, bool desativarfiltrosHierarquia = false)
        {
            return this.InstituicaoNivelModalidadeDomainService.BuscarLocalidadesPorModalidadeSelect(seqModalidade, seqInstituicaoNivel, seqCursoUnidade, desativarfiltrosHierarquia);
        }

        /// <summary>
        /// Busca todas as localidades ativas na visão de localidades vigente
        /// </summary>
        /// <returns>Dados das localidades</returns>
        public List<SMCDatasourceItem> BuscarLocalidadesAtivasSelect(bool apenasAtivos = true)
        {
            return this.HierarquiaEntidadeDomainService.BuscarEntidadesFolhaVisaoSelect(TipoVisao.VisaoLocalidades, TOKEN_TIPO_ENTIDADE.LOCALIDADE, apenasAtivos: apenasAtivos, usarSeqEntidade: true);
        }

        /// <summary>
        /// Busca uma lista de unidades/localidades de acordo com a tabela curso oferta localidade
        /// definida pelo sequencial do curriculo curso oferta e pelo sequencial de modalidade
        /// </summary>
        /// <param name="seqCurriculoCursooferta">Sequencial do curriculo curso oferta</param>
        /// <param name="seqModalidade">Sequencial da modalidade</param>
        /// <returns>Lista de unidades/localidades</returns>
        public List<SMCDatasourceItem> BuscarUnidadesLocalidadesPorCurriculoCursoOfertaSelect(long seqCurriculoCursoOferta, long seqModalidade)
        {
            var curriculoCursoOferta = this.CurriculoCursoOfertaDomainService.BuscarCurriculoCursoOferta(seqCurriculoCursoOferta);
            List<SMCDatasourceItem> result = new List<SMCDatasourceItem>();

            if (curriculoCursoOferta != null)
            {
                foreach (var cursoOfertaLocalidade in curriculoCursoOferta?.CursoOferta.CursosOfertaLocalidade.Where(w => w.SeqModalidade == seqModalidade))
                {
                    SMCDatasourceItem item = new SMCDatasourceItem();
                    string nomeCursoUnidade = EntidadeDomainService.BuscarEntidadeNome(cursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.Single().ItemSuperior.SeqEntidade);
                    string nomeEntidade = EntidadeDomainService.BuscarEntidadeNome(cursoOfertaLocalidade.HierarquiasEntidades.Single().ItemSuperior.SeqEntidade);
                    item.Seq = cursoOfertaLocalidade.Seq;
                    item.Descricao = $"{nomeCursoUnidade} / {nomeEntidade}";
                    result.Add(item);
                }
            }

            return result;
        }

        public List<SMCDatasourceItem> BuscarCursoOfertasLocalidadeReplicarCursoFormacaoEspecificaSelect(long seqCurso, long seqFormacaoEspecifica)
        {
            List<SMCDatasourceItem> result = new List<SMCDatasourceItem>();

            var spec = new CursoOfertaLocalidadeFilterSpecification()
            {
                SeqCurso = seqCurso, //A respectiva Oferta-Curso está associada ao Curso enviado como parâmetro
                CursoOfertaAtivo = true // A respectiva Oferta-Curso está ativa
            };

            var cursoOfertasLocalidade = this.SearchBySpecification(spec, IncludesCursoOfertaLocalidade.CursoOferta).ToList();
            //A respectiva Oferta-Curso não é por Formação Específica ou, a Formação Específica da Oferta-Curso é a Formação enviada como parâmetro
            cursoOfertasLocalidade = cursoOfertasLocalidade.Where(a => !a.CursoOferta.SeqFormacaoEspecifica.HasValue || a.CursoOferta.SeqFormacaoEspecifica == seqFormacaoEspecifica).ToList();

            foreach (var cursoOfertaLocalidade in cursoOfertasLocalidade)
            {
                //A Formação-Especifica enviada como parâmetro não está associada na Oferta-Curso-Localidade
                var cursoOfertaLocalidadeFormacao = this.CursoOfertaLocalidadeFormacaoDomainService.SearchBySpecification(
                       new CursoOfertaLocalidadeFormacaoFilterSpecification() { SeqCursoLocalidade = cursoOfertaLocalidade.Seq, SeqFormacaoEspecifica = seqFormacaoEspecifica }).ToList();

                if (!cursoOfertaLocalidadeFormacao.Any())
                {
                    var nomeEntidade = this.EntidadeDomainService.BuscarEntidadeNome(cursoOfertaLocalidade.Seq);

                    SMCDatasourceItem item = new SMCDatasourceItem()
                    {
                        Seq = cursoOfertaLocalidade.Seq,
                        Descricao = nomeEntidade
                    };

                    result.Add(item);
                }
            }

            return result.OrderBy(o => o.Descricao).ToList();
        }

        /// <summary>
        /// Recupera a mascara de curso oferta localidade segundo a regra RN_CSO_027
        /// </summary>
        /// <param name="seqCursoOferta">Sequencial do curso oferta</param>
        /// <param name="seqLocalidade">Sequencial do item de hierarquia da localidade</param>
        /// <returns></returns>
        public string RecuperarMascaraCursoOfertaLocalidade(long? seqCursoOferta, long? seqLocalidade)
        {
            string descricaoCursoOferta = string.Empty;
            string nomeLocalidade = string.Empty;
            string retorno = string.Empty;

            if (seqCursoOferta.HasValue)
            {
                var specCursoOferta = new SMCSeqSpecification<CursoOferta>(seqCursoOferta.Value);
                descricaoCursoOferta = this.CursoOfertaDomainService.SearchProjectionByKey(specCursoOferta, p => p.Descricao);
            }

            if (seqLocalidade.HasValue)
            {
                var specLocalidade = new SMCSeqSpecification<Entidade>(seqLocalidade.Value);
                nomeLocalidade = this.EntidadeDomainService.SearchProjectionByKey(specLocalidade, p => p.Nome);
            }

            if (!string.IsNullOrEmpty(descricaoCursoOferta))
                retorno += $"{descricaoCursoOferta}";
            if (!string.IsNullOrEmpty(nomeLocalidade))
                retorno += $" - {nomeLocalidade}";

            return retorno;
        }

        /// <summary>
        /// Grava o CursoOfertaLocaldiade e suas dependências
        /// </summary>
        /// <param name="cursoOfertaLocalidadeVo">Dados do CursoOfertaLocaldiade a ser gravado</param>
        /// <returns>Sequencial do CursoOfertaLocaldiade gravado</returns>
        public long SalvarCursoOfertaLocalidade(CursoOfertaLocalidadeVO cursoOfertaLocalidadeVo, bool desativarfiltrosHierarquia = false)
        {
            if (cursoOfertaLocalidadeVo.FormacoesEspecificas == null || (cursoOfertaLocalidadeVo.FormacoesEspecificas != null && cursoOfertaLocalidadeVo.FormacoesEspecificas.Count() == 0))
                throw new CursoOfertaLocalidadeInclusaoFormacoesEspecificasException();

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

            var editando = true;
            using (var transacao = SMCUnitOfWork.Begin())
            {
                var cursoOfertaLocalidade = cursoOfertaLocalidadeVo.Transform<CursoOfertaLocalidade>();

                //FIX: Essa a triuição é necessária porque na tela existem dois campos com o mesmo nome.
                //Um campo é a situação atual que fica no filtro do lookup de curso oferta
                //Outro campo é a situação atual na tela de cadastro de curso oferta localidade
                cursoOfertaLocalidade.SeqSituacaoAtual = cursoOfertaLocalidadeVo.SeqSituacaoAtualCursoOfertaLocalidade;

                // Recupera ou cria o HierarquiaEntidadeItem que relaciona esta CursoOfertaLocalidade à Localidade
                var cursoOfertaLocalidadeHierarquia = this.HierarquiaEntidadeItemDomainService
                    .SearchByKey(new HierarquiaEntidadeItemFilterSpecification() { SeqEntidade = cursoOfertaLocalidadeVo.Seq }) ??
                                 new HierarquiaEntidadeItem();

                var itemHierarquiaOfertaLocalidade = this.HierarquiaEntidadeItemDomainService
                    .SearchByKey(new HierarquiaEntidadeItemFilterSpecification() { SeqEntidade = cursoOfertaLocalidadeVo.SeqLocalidade, TipoVisaoHierarquia = TipoVisao.VisaoLocalidades });

                // Atualiza o HierarquiaEntidadeItem e vincula este ao curso
                cursoOfertaLocalidadeHierarquia.SeqItemSuperior = itemHierarquiaOfertaLocalidade.Seq;
                cursoOfertaLocalidade.HierarquiasEntidades = new List<HierarquiaEntidadeItem>(1);
                cursoOfertaLocalidade.HierarquiasEntidades.Add(cursoOfertaLocalidadeHierarquia);
                // Coverte as hierarquias relacionadas no formato do domínio
                cursoOfertaLocalidade.FormacoesEspecificas = this.ConverterFormacoesEspecificas(cursoOfertaLocalidadeVo);
                cursoOfertaLocalidade.Classificacoes = EntidadeDomainService.GerarEntidadeClassificacoes(cursoOfertaLocalidadeVo);

                //Validações
                this.Validar(cursoOfertaLocalidade, new EntidadeValidator());
                this.EntidadeDomainService.ValidarDadosContatoObrigatorios(cursoOfertaLocalidade);

                var entidadeDomainService = this.EntidadeDomainService;

                if (cursoOfertaLocalidade.Seq == 0)
                {
                    entidadeDomainService.IncluirSituacao(cursoOfertaLocalidade);
                    editando = false;
                }

                entidadeDomainService.AtualizarHierarquiaEntidadeExternada(cursoOfertaLocalidade, TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO_OFERTA_LOCALIDADE, true);

                this.EnsureFileIntegrity(cursoOfertaLocalidade, m => m.SeqArquivoLogotipo, m => m.ArquivoLogotipo);

                this.SaveEntity(cursoOfertaLocalidade);

                #region Chamada dos serviços do GRA

                // Chama os serviços do GRA para inclusão/alteração do serviço
                // Recupera o pai do item superior para pegar o codigo SEO do núcleo
                // Comentado pois ficou para depois a implementação desta funcionalidade (Task 27548)
                /*if (editando)
                {
                    FinanceiroService.AlteraServico(new Financeiro.ServiceContract.Areas.FIN.Data.AlteraServicoParametroData
                    {
                        CodigoNucleo = 0,
                        CodigoServicoOrigem = cursoOfertaLocalidade.Seq,
                        SeqOrigem = cursoOfertaLocalidade.SeqOrigemFinanceira,
                        DescricaoServico = cursoOfertaLocalidade.Nome,
                        CodigoUnidadeServico = cursoOfertaLocalidade.CodigoUnidadeSeo.Value,
                        TipoOperacaoAlteracaoServico = TipoOperacaoAlteracaoServico.Alteracao
                    });
                }
                else
                {
                    FinanceiroService.IncluiServico(new Financeiro.ServiceContract.Areas.FIN.Data.ServicoParametroData
                    {
                        CodigoNucleo = 0,
                        CodigoServicoOrigem = cursoOfertaLocalidade.Seq,
                        CodigoUnidadeServico = cursoOfertaLocalidade.CodigoUnidadeSeo.Value,
                        //DataFimOferta = ,
                        //DataInicioOferta =,
                        DescricaoServico = cursoOfertaLocalidade.Nome,
                        ResponsabilidadeTitular = false,
                        SeqOrigem = cursoOfertaLocalidade.SeqOrigemFinanceira
                    });
                }*/

                #endregion Chamada dos serviços do GRA

                //Apenas na inclusão, enviar notificação de nova oferta de curso por localidade
                if (cursoOfertaLocalidadeVo.Seq == 0)
                    EnviarNotificacaoNovaOfertaCursoLocalidade(cursoOfertaLocalidadeVo, cursoOfertaLocalidade);

                transacao.Commit();

                FilterHelper.AtivarFiltros(this);

                return cursoOfertaLocalidade.Seq;
            }
        }

        /// <summary>
        /// Recupera as ofertas de curso por localidade filhas das entidades responsáveis informadas
        /// </summary>
        /// <param name="seqEntidadeVinculo">Sequencial da entidade responsável </param>
        /// <returns>Ofertas de curso por localidade que atendam aos filtros</returns>
        public List<SMCDatasourceItem> BuscarCursoOfertasLocalidadeAtivasPorEntidadeResponsavelSelect(long seqEntidadeVinculo)
        {
            if (seqEntidadeVinculo == 0)
            {
                return new List<SMCDatasourceItem>();
            }

            var seqsCursos = HierarquiaEntidadeItemDomainService
                .BuscarHierarquiaEntidadeItensPorEntidadeVisaoOganizacional(seqEntidadeVinculo, TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO)
                .ToArray();

            var spec = new CursoOfertaLocalidadeFilterSpecification() { SeqsCursos = seqsCursos, };
            spec.SetOrderBy(o => o.Nome);
            var cursoOfertasLocalidade = this.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
            {
                Seq = p.Seq,
                Descricao = p.Nome,
                DataAttributes = new List<SMCKeyValuePair>()
                {
                    new SMCKeyValuePair() { Key = "DescricaoNivelEnsino", Value = p.CursoOferta.Curso.NivelEnsino.Descricao },
                    new SMCKeyValuePair() { Key = "NomeLocalidade",       Value = p.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome }
                }
            }).ToList();

            return cursoOfertasLocalidade;
        }

        public List<CursoOfertaLocalidade> BuscarCursoOfertasLocalidadeAtivasPorEntidadeResponsavel(long seqEntidadeVinculo)
        {
            if (seqEntidadeVinculo == 0)
            {
                return new List<CursoOfertaLocalidade>();
            }

            var seqsCursos = HierarquiaEntidadeItemDomainService
                .BuscarHierarquiaEntidadeItensPorEntidadeVisaoOganizacional(seqEntidadeVinculo, TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO)
                .ToArray();

            var spec = new CursoOfertaLocalidadeFilterSpecification()
            {
                SeqsCursos = seqsCursos,
                Ativa = true,
                CursoOfertaAtivo = true
            };
            spec.SetOrderBy(o => o.Nome);

            var cursoOfertasLocalidade = this.SearchBySpecification(spec).ToList();

            return cursoOfertasLocalidade;
        }

        /// <summary>
        /* Ao incluir a oferta de curso por localidade, enviar a notificação correspondente ao tipo COMUNICADO_NOVA_OFERTA_CURSO_LOCALIDADE parametrizada para a entidade responsável
         * do respectivo curso, conforme a parametrização de notificação por entidade. Substituir as tags do email de acordo com os seguintes critérios:
         * [USUARIO_CRIACAO] = retornar a descrição do usuário responsável pela criação da oferta de curso por localidade
         * [DATA_CRIACAO] = retornar a data e hora de criação da oferta de curso por localidade
         * [ID_OFERTA_CURSO_LOCALIDADE] = buscar o ID da oferta de curso por localidade
         * [DESCRICAO_OFERTA_CURSO_LOCALIDADE] = retornar a descrição da oferta de curso por localidade no seguinte formato: [Descrição da Oferta de Curso] - [Descrição da Localidade]
         * [SITUACAO_ATUAL_OFERTA_CURSO_LOCALIDADE] = retornar a descrição da situação atual da oferta de curso por localidade.*/

        private void EnviarNotificacaoNovaOfertaCursoLocalidade(CursoOfertaLocalidadeVO cursoOfertaLocalidadeVO, CursoOfertaLocalidade novoCursoOfertaLocalidade)
        {
            // Recupera o sequencial da configuração da notificação
            var seqConfiguracaoNotificacao = EntidadeConfiguracaoNotificacaoDomainService.BuscarSeqConfiguracaoNotificacaoAtivo(cursoOfertaLocalidadeVO.SeqInstituicaoEnsino.GetValueOrDefault(), TOKEN_TIPO_NOTIFICACAO.COMUNICADO_NOVA_OFERTA_CURSO_LOCALIDADE);

            // Caso não eocnotre sequencial, dispara um erro
            if (seqConfiguracaoNotificacao == 0)
                throw new EntidadeConfiguracaoNotificacaoNaoEncotradaException();

            // Recupera a situação atual da entidade
            var situacaoAtual = SituacaoEntidadeDomainService.SearchByKey(new SMCSeqSpecification<SituacaoEntidade>(novoCursoOfertaLocalidade.SeqSituacaoAtual));

            // Prepara os dados oara serem substituídos no corpo da notificação
            Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
            dadosMerge.Add("{{ID_OFERTA_CURSO_LOCALIDADE}}", novoCursoOfertaLocalidade.Seq.ToString());
            dadosMerge.Add("{{DESCRICAO_OFERTA_CURSO_LOCALIDADE}}", RecuperarMascaraCursoOfertaLocalidade(cursoOfertaLocalidadeVO.SeqCursoOferta, cursoOfertaLocalidadeVO.SeqLocalidade));
            dadosMerge.Add("{{SITUACAO_ATUAL_OFERTA_CURSO_LOCALIDADE}}", situacaoAtual.Descricao);
            dadosMerge.Add("{{USUARIO_CRIACAO}}", novoCursoOfertaLocalidade.UsuarioInclusao);
            dadosMerge.Add("{{DATA_CRIACAO}}", novoCursoOfertaLocalidade.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss"));

            // Cria o data com os dados para serem enviados
            NotificacaoEmailData data = new NotificacaoEmailData()
            {
                SeqConfiguracaoNotificacao = seqConfiguracaoNotificacao,
                DadosMerge = dadosMerge,
                DataPrevistaEnvio = DateTime.Now,
                PrioridadeEnvio = PrioridadeEnvio.QuandoPossivel,
            };

            // Chama o serviço de envio de notificação
            long seqNotificacaoEnviada = this.NotificacaoService.SalvarNotificacao(data);

            // Busca o sequencial da notificação-email-destinatário enviada
            var envioDestinatario = this.NotificacaoService.BuscaNotificacaoEmailDestinatario(seqNotificacaoEnviada);
            if (envioDestinatario.Count == 0)
                throw new CursoOfertaLocalidadeInclusaoEnvioNotificacaoException(TOKEN_TIPO_NOTIFICACAO.COMUNICADO_NOVA_OFERTA_CURSO_LOCALIDADE);
        }

        private List<CursoOfertaLocalidadeFormacao> ConverterFormacoesEspecificas(CursoOfertaLocalidadeVO cursoOfertaLocalidade)
        {
            if (cursoOfertaLocalidade.FormacoesEspecificas == null)
                return null;

            var seqFormacoesEspecificasNovas = cursoOfertaLocalidade
                .FormacoesEspecificas
                .Select(s => s.Seq)
                .ToList();

            var formacoesEspecificasGravadas = this.CursoOfertaLocalidadeFormacaoDomainService
                .SearchBySpecification(new CursoOfertaLocalidadeFormacaoFilterSpecification() { SeqCursoLocalidade = cursoOfertaLocalidade.Seq });

            //Mantêm apenas as formações ainda selecionadas
            var formacoesEspecificasAtualizadas = formacoesEspecificasGravadas
                .Where(w => seqFormacoesEspecificasNovas.Contains(w.SeqFormacaoEspecifica))
                .ToList();

            var seqFormacoesEspecificasAtualizadas = formacoesEspecificasAtualizadas
                .Select(s => s.SeqFormacaoEspecifica)
                .ToList();

            //Inclui na lista as novas fomações
            formacoesEspecificasAtualizadas
                .AddRange(cursoOfertaLocalidade
                    .FormacoesEspecificas
                    .Where(w => !seqFormacoesEspecificasAtualizadas.Contains(w.Seq))
                    .Select(s => new CursoOfertaLocalidadeFormacao()
                    {
                        SeqCursoOfertaLocalidade = cursoOfertaLocalidade.Seq,
                        SeqFormacaoEspecifica = s.Seq
                    }));

            return formacoesEspecificasAtualizadas;
        }

        private List<FormacaoEspecificaHierarquiaVO> ConverterFormacoesEspecificas(CursoOfertaLocalidade cursoOfertaLocalidade)
        {
            var seqsFormacoes = cursoOfertaLocalidade
                .FormacoesEspecificas
                .Select(s => s.SeqFormacaoEspecifica)
                .ToArray();

            var formacoesEspecificasHierarquia = this.FormacaoEspecificaDomainService
                .BuscarFormacoesEspecificasHierarquia(seqsFormacoes)
                .ToList();

            return formacoesEspecificasHierarquia;
        }

        private void Validar(CursoOfertaLocalidade cursoOfertaLocalidade, params SMCValidator[] validatores)
        {
            var results = new List<SMCValidationResults>();
            foreach (var validador in validatores)
            {
                results.Add(validador.Validate(cursoOfertaLocalidade));
            }
            if (results.Count(c => !c.IsValid) > 0)
            {
                List<SMCValidationResults> errorList = results.Where(w => !w.IsValid).ToList();
                throw new SMCInvalidEntityException(errorList);
            }
        }

        /// <summary>
        /// Buscar as localidades definidas na(s) matriz(es) associada(s) à turma.
        ///    - Se houver mais de uma matriz, exibir a união de todas as localidades de todas as matrizes.
        ///    - As localidades de exceção definidas nas ofertas de matrizes para as configurações que foram associadas à turma.
        /// Se a turma que estiver sendo cadastrada não possuir oferta de matriz  associada, listar todas as localidades para a instituição de ensino em questão.
        /// </summary>
        /// <param name="ofertasMatriz"></param>
        /// <returns>Lista de Localidades</returns>
        public List<SMCDatasourceItem> BuscarLocalidadesMatrizTurma(List<MatrizCurricularOfertaVO> ofertasMatriz)
        {
            var localidades = new List<SMCDatasourceItem>();
            if (ofertasMatriz != null)
            {
                /*Listar as localidades definidas na(s) matriz(es) associada(s) à turma .
                 * Se houver mais de uma matriz, exibir a união de todas as localidades de todas as matrizes. */
                foreach (var localidadeMatriz in ofertasMatriz)
                {
                    var qtd = ofertasMatriz.Count(x => x.SeqEntidadeLocalidade == localidadeMatriz.SeqEntidadeLocalidade);

                    // Se a oferta de matriz é comum para todas as outras Matrizes
                    if (qtd == ofertasMatriz.Count)
                    {
                        if (localidadeMatriz.SeqEntidadeLocalidade > 0 && !localidades.Any(a => a.Seq == localidadeMatriz.SeqEntidadeLocalidade))
                        {
                            localidades.Add(new SMCDatasourceItem() { Seq = localidadeMatriz.SeqEntidadeLocalidade, Descricao = localidadeMatriz.DescricaoLocalidade });
                        }
                    }
                }
                // Todas as exceções de todas as matrizes
                var todasExcecoesLocalidades = MatrizCurricularOfertaExcecaoLocalidadeDomainService.BuscarMatrizesCurricularExcecoesLocalidades(ofertasMatriz.Select(s => s.SeqMatrizCurricular).ToList());

                foreach (var execaoLocalidade in todasExcecoesLocalidades)
                {
                    /* Se houver mais de uma matriz, exibir a união de todas as exceçõesLocalidades de todas as matrizes. */
                    // Verifico quantadas vezes a exceção repete na sua lista
                    var qtdExcecao = todasExcecoesLocalidades.Count(x => x.SeqEntidadeLocalidade == execaoLocalidade.SeqEntidadeLocalidade);

                    // Verifico se a quantidade de repetição é equivalente a mesma quantidade de Matrizes (se existe esta exceção para cada Exceção de cada Matriz)
                    if (qtdExcecao == ofertasMatriz.Count)
                    {
                        // Deve ser adicionada a lsita de localidades, apenas as exceções que são comuns para todas as ofertas de matrizes
                        if (execaoLocalidade.SeqEntidadeLocalidade > 0 && !localidades.Any(a => a.Seq == execaoLocalidade.SeqEntidadeLocalidade))
                        {
                            localidades.Add(new SMCDatasourceItem() { Seq = execaoLocalidade.SeqEntidadeLocalidade, Descricao = execaoLocalidade.DescricaoLocalidade });
                        }
                    }
                }
            }
            else
            {
                /* se a turma que estiver sendo cadastrada não possuir oferta de matriz  associada, listar todas as
                 * localidades para a instituição de ensino em questão.*/
                //Buscar as localidades da instituição logada quando não for informado oferta no step 3
                localidades = BuscarLocalidadesAtivasSelect();
            }

            return localidades;
        }

        public List<SMCDatasourceItem> BuscarCursosOfertasLocalidadesReplicarFormacaoEspecificaProgramaSelect(CursoOfertaLocalidadeReplicaFormacaoEspecificaProgramaFiltroVO filtros)
        {
            var spec = filtros.Transform<CursoOfertaLocalidadeReplicaFormacaoEspecificaProgramaFilterSpecification>();

            spec.SetOrderBy("Nome");

            return this.SearchProjectionBySpecification(spec, c => new SMCDatasourceItem()
            {
                Seq = c.Seq,
                Descricao = c.Nome
            }).ToList();
        }

        public bool CursoOfertaLocalidadeExigeGrau(long seq)
        {
            var retorno = false;
            var entidade = this.SearchByKey(new SMCSeqSpecification<CursoOfertaLocalidade>(seq),
                                IncludesCursoOfertaLocalidade.TipoEntidade |
                                IncludesCursoOfertaLocalidade.FormacoesEspecificas_FormacaoEspecifica_TipoFormacaoEspecifica);
            if (entidade != null)
                retorno = entidade.TipoEntidade.Token == TOKEN_TIPO_ENTIDADE.CURSO_OFERTA_LOCALIDADE &&
                          entidade.FormacoesEspecificas.Where(w => w.FormacaoEspecifica.TipoFormacaoEspecifica.ExigeGrau == true).Count() > 0;
            return retorno;
        }

        /// <summary>
        /// Busca dados da identificação EMEC com endereço - RN_CNC_050 / RN_CNC_052 
        /// </summary>
        public DadosCursoOfertaLocalidadeVO BuscarIdentificacaoEmecComEnderecoParaDocumentoAcademico(long seq)
        {
            var cursoOfertaLocalidade = this.SearchByKey(new SMCSeqSpecification<CursoOfertaLocalidade>(seq), x => x.Enderecos, x => x.CursoUnidade.Curso);

            if (cursoOfertaLocalidade == null)
                throw new CursoOfertaLocalidadeInformacaoNaoEncontradaException("curso oferta localidade");

            var retorno = new DadosCursoOfertaLocalidadeVO
            {
                Seq = cursoOfertaLocalidade.Seq,
                NomeCursoCurriculo = cursoOfertaLocalidade.CursoUnidade.Curso.Nome,
                SeqModalidade = cursoOfertaLocalidade.SeqModalidade,
                Endereco = new EnderecoVO()
            };

            if (cursoOfertaLocalidade.CodigoOrgaoRegulador.HasValue)
                retorno.CodigoOrgaoRegulador = cursoOfertaLocalidade.CodigoOrgaoRegulador;
            else
                throw new CursoOfertaLocalidadeInformacaoNaoEncontradaException("Código curso emec");

            PreencherEnderecoComercial(retorno, cursoOfertaLocalidade);

            return retorno;
        }

        private void PreencherEnderecoComercial(DadosCursoOfertaLocalidadeVO retorno, CursoOfertaLocalidade cursoOfertaLocalidade)
        {
            var enderecoComercial = cursoOfertaLocalidade.Enderecos.FirstOrDefault(a => a.TipoEndereco == TipoEndereco.Comercial);
            if (enderecoComercial == null)
                throw new CursoOfertaLocalidadeInformacaoNaoEncontradaException("endereço comercial");

            retorno.Endereco.Logradouro = FormatarString.Truncate(enderecoComercial.Logradouro, 255);

            if (!string.IsNullOrEmpty(enderecoComercial.Numero))
                retorno.Endereco.Numero = FormatarString.Truncate(enderecoComercial.Numero, 60);

            if (!string.IsNullOrEmpty(enderecoComercial.Complemento))
                retorno.Endereco.Complemento = FormatarString.Truncate(enderecoComercial.Complemento, 60);

            if (!string.IsNullOrEmpty(enderecoComercial.Bairro))
                retorno.Endereco.Bairro = FormatarString.Truncate(enderecoComercial.Bairro, 60);
            else
                throw new CursoOfertaLocalidadeInformacaoNaoEncontradaException("bairro do endereço");

            if (!string.IsNullOrEmpty(enderecoComercial.Cep))
            {
                var cep = FormatarString.ObterSomenteNumeros(enderecoComercial.Cep);
                retorno.Endereco.Cep = FormatarString.Truncate(cep, 8);
            }
            else
            {
                throw new CursoOfertaLocalidadeInformacaoNaoEncontradaException("cep do endereço");
            }

            if (!string.IsNullOrEmpty(enderecoComercial.SiglaUf))
            {
                retorno.Endereco.Uf = FormatarString.Truncate(enderecoComercial.SiglaUf, 2);

                if (enderecoComercial.CodigoCidade.GetValueOrDefault() > 0)
                {
                    var cidade = this.LocalidadeService.BuscarCidade(enderecoComercial.CodigoCidade.Value, enderecoComercial.SiglaUf);
                    if (cidade != null)
                    {
                        retorno.Endereco.NomeMunicipio = FormatarString.Truncate(cidade.Nome, 255);

                        if (cidade.CodigoMunicipioIBGE.HasValue)
                            retorno.Endereco.CodigoMunicipio = FormatarString.Truncate(cidade.CodigoMunicipioIBGE.ToString(), 7);
                        else
                            throw new CursoOfertaLocalidadeInformacaoNaoEncontradaException("código do município IBGE do endereço");
                    }
                    else
                        throw new CursoOfertaLocalidadeInformacaoNaoEncontradaException("cidade do endereço");
                }
                else
                    throw new CursoOfertaLocalidadeInformacaoNaoEncontradaException("código da cidade do endereço");
            }
            else
                throw new CursoOfertaLocalidadeInformacaoNaoEncontradaException("sigla uf do endereço");
        }
    }
}