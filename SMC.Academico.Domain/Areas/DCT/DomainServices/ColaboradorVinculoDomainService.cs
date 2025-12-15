using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.DCT.Constants;
using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.DCT.Exceptions;
using SMC.Academico.Common.Areas.DCT.Includes;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.GRD.DomainServices;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Helpers;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security.Util;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using SMC.Pessoas.ServiceContract.Areas.PES.Data;
using SMC.Pessoas.ServiceContract.Areas.PES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.DCT.DomainServices
{
    public class ColaboradorVinculoDomainService : AcademicoContextDomain<ColaboradorVinculo>
    {
        #region [ DomainService ]

        private CursoOfertaLocalidadeTurnoDomainService CursoOfertaLocalidadeTurnoDomainService => Create<CursoOfertaLocalidadeTurnoDomainService>();
        private TipoEntidadeDomainService TipoEntidadeDomainService => Create<TipoEntidadeDomainService>();
        private ConfiguracaoComponenteDomainService ConfiguracaoComponenteDomainService => Create<ConfiguracaoComponenteDomainService>();
        private ColaboradorVinculoCursoDomainService ColaboradorVinculoCursoDomainService => Create<ColaboradorVinculoCursoDomainService>();
        private ColaboradorDomainService ColaboradorDomainService => Create<ColaboradorDomainService>();
        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService => Create<CursoOfertaLocalidadeDomainService>();
        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();
        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService => Create<HierarquiaEntidadeItemDomainService>();
        private InstituicaoNivelTipoAtividadeColaboradorDomainService InstituicaoNivelTipoAtividadeColaboradorDomainService => Create<InstituicaoNivelTipoAtividadeColaboradorDomainService>();
        private PessoaDomainService PessoaDomainService => Create<PessoaDomainService>();
        private ProgramaDomainService ProgramaDomainService => Create<ProgramaDomainService>();
        private TipoFormacaoEspecificaDomainService TipoFormacaoEspecificaDomainService => Create<TipoFormacaoEspecificaDomainService>();
        private TipoVinculoColaboradorDomainService TipoVinculoColaboradorDomainService => Create<TipoVinculoColaboradorDomainService>();
        private PessoaDadosPessoaisDomainService PessoaDadosPessoaisDomainService => Create<PessoaDadosPessoaisDomainService>();
        private EventoAulaDomainService EventoAulaDomainService => Create<EventoAulaDomainService>();

        #endregion [ DomainService ]

        #region [ Service ]

        private IPessoaService PessoaService => Create<IPessoaService>();

        #endregion [ Service ]

        #region [Propriedades]

        private const string INCLUAO = "Inclusão";

        private const string ALTERACAO = "Alteração";

        #endregion [Propriedades]

        public long[] BuscarColaboradorVinculos(ColaboradorFiltroVO filtro)
        {
            if (filtro.SeqEntidadeVinculo.HasValue ||
                filtro.SeqsEntidadesVinculo.SMCAny() ||
                !string.IsNullOrEmpty(filtro.TokenEntidadeVinculo) ||
                filtro.TokensEntidadeVinculo.SMCAny() ||
                filtro.SeqsTiposEntidadesVinculo.SMCAny() ||
                filtro.SeqTipoVinculoColaborador.HasValue ||
                filtro.DataInicio.HasValue ||
                filtro.VinculoAtivo.HasValue ||
                filtro.PermiteInclusaoManualVinculo.HasValue ||
                filtro.CriaVinculoInstitucional.HasValue ||
                filtro.SeqsColaboradorVinculoCurso.SMCAny())
            {
                try
                {
                    if (filtro.IgnorarFiltros)
                    {
                        FilterHelper.AtivarApenasFiltros(this, FILTER.INSTITUICAO_ENSINO);
                    }
                    var spec = new ColaboradorVinculoFilterSpecification()
                    {
                        SeqsEntidadesVinculo = filtro.SeqsEntidadesVinculo,
                        SeqEntidadeVinculo = filtro.SeqEntidadeVinculo,
                        TokenEntidadeVinculo = filtro.TokenEntidadeVinculo,
                        TokensEntidadeVinculo = filtro.TokensEntidadeVinculo,
                        SeqsTiposEntidadesVinculo = filtro.SeqsTiposEntidadesVinculo,
                        SeqTipoVinculoColaborador = filtro.SeqTipoVinculoColaborador,
                        DataInicio = filtro.DataInicio,
                        DataFim = filtro.DataFim,
                        VinculoAtivo = filtro.VinculoAtivo,
                        PermiteInclusaoManualVinculo = filtro.PermiteInclusaoManualVinculo,
                        CriaVinculoInstitucional = filtro.CriaVinculoInstitucional,
                        SeqsColaboradorVinculoCurso = filtro.SeqsColaboradorVinculoCurso,
                        SeqColaborador = filtro.Seq,
                        SeqsEntidadesResponsaveis = filtro.SeqsEntidadesResponsaveis
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

        public long[] FiltroVinculoColaboradores(ref ColaboradorFiltroVO filtros)
        {
            filtros.SeqsColaboradorVinculoCurso = ColaboradorVinculoCursoDomainService.BuscarColaboradorVinculoCursos(filtros);
            return BuscarColaboradorVinculos(filtros);
        }

        public bool VerificarVinculosAtivosColaborador(long seqPessoaAtuacao, long[] seqsCursoOfertaLocalidade, DateTime dataReferencia)
        {
            // Busca todos os grupos de programas dos curso oferta localidades
            List<long> seqsGruposProgramas = null;
            if (seqsCursoOfertaLocalidade != null && seqsCursoOfertaLocalidade.Any())
            {
                seqsGruposProgramas = new List<long>();

                // Busca o tipo de entidade grupo de programa
                var tipoEntidadeGrupoPrograma = TipoEntidadeDomainService.BuscarTipoEntidadeNaInstituicao(TOKEN_TIPO_ENTIDADE.GRUPO_PROGRAMA);
                foreach (var seqCursoOfertaLocalidade in seqsCursoOfertaLocalidade)
                {
                    // busca o sequencial da entidade desta hierarquia
                    var seqCurso = CursoOfertaLocalidadeDomainService.SearchProjectionByKey(seqCursoOfertaLocalidade, x => x.CursoOferta.SeqCurso);
                    var seqs = ConfiguracaoComponenteDomainService.BuscarSeqsEntidadesSuperiores(seqCurso, tipoEntidadeGrupoPrograma.Seq, TipoVisao.VisaoOrganizacional);
                    seqsGruposProgramas.AddRange(seqs);
                }
            }

            var filtroVO = new ColaboradorFiltroVO
            {
                Seq = seqPessoaAtuacao,
                DataInicio = dataReferencia,
                DataFim = dataReferencia,
                SeqsCursoOfertaLocalidade = seqsCursoOfertaLocalidade,
                SeqsEntidadesVinculo = seqsGruposProgramas?.Distinct().ToArray()
            };

            // Busca os vínculos ativos do colaborador
            var vinculos = BuscarColaboradorVinculos(filtroVO);

            return vinculos.Any();
        }

        /// <summary>
        /// Configura o tipo de formação específica que pode ser selecionado como linha de pesquia
        /// </summary>
        /// <returns>ColaboradorVinculoData com o tipo da formação específcia configurado</returns>
        public ColaboradorVinculoVO BuscarConfiguracaoColaboradorVinculo(long seqColaborador)
        {
            var configuracao = new ColaboradorVinculoVO();
            var specFormacao = new TipoFormacaoEspecificaFilterSpecification() { Token = TOKEN_TIPO_FORMACAO_ESPECIFICA.LINHA_PESQUISA };

            configuracao.SeqTipoFormacaoEspecifica = TipoFormacaoEspecificaDomainService.SearchProjectionByKey(specFormacao, p => p.Seq);
            configuracao.PermitirAlterarDataFimVinculo = true;

            return configuracao;
        }

        /// <summary>
        /// Busca um vinculo de colaborador com suas depêndencias
        /// </summary>
        /// <param name="seq">Sequencial do colaborador</param>
        /// <returns>Dados do colaborador</returns>
        public ColaboradorVinculoVO BuscarColaboradorVinculo(long seq)
        {
            ColaboradorVinculoVO colaboradorVinculoVo = null;
            try
            {
                var includes = IncludesColaboradorVinculo.ColaboradoresResponsaveis_ColaboradorResponsavel
                 | IncludesColaboradorVinculo.Cursos_Atividades
                 | IncludesColaboradorVinculo.Cursos_CursoOfertaLocalidade_HierarquiasEntidades_ItemSuperior_Entidade
                 | IncludesColaboradorVinculo.EntidadeVinculo_TipoEntidade
                 | IncludesColaboradorVinculo.FormacoesEspecificas
                 | IncludesColaboradorVinculo.Colaborador_Professores;

                HierarquiaEntidadeItemDomainService.DesativarFiltrosHierarquiaItem(this);
                HierarquiaEntidadeItemDomainService.AtivarFiltroHierarquiaItem(TipoVisao.VisaoOrganizacional, this);
                HierarquiaEntidadeItemDomainService.AtivarFiltroHierarquiaItem(TipoVisao.VisaoLocalidades, this);

                var spec = new SMCSeqSpecification<ColaboradorVinculo>(seq);
                var colaboradorVinculo = this.SearchByKey(spec, includes);

                colaboradorVinculoVo = colaboradorVinculo.Transform<ColaboradorVinculoVO>();
                if (colaboradorVinculo != null)
                {
                    colaboradorVinculoVo.EntidadeVinculoGrupoPrograma = colaboradorVinculo?.EntidadeVinculo?.TipoEntidade?.Token == TOKEN_TIPO_ENTIDADE.GRUPO_PROGRAMA;
                    colaboradorVinculoVo.ExibirColaboradorResponsavel = this.TipoVinculoColaboradorDomainService.RetornarTipoVinculoNecessitaAcompanhamento(colaboradorVinculo.SeqTipoVinculoColaborador);
                    foreach (var item in colaboradorVinculoVo.Cursos)
                    {
                        item.TipoAtividadeColaborador = colaboradorVinculo.Cursos.FirstOrDefault(f => f.Seq == item.Seq).Atividades.Select(sa => sa.TipoAtividadeColaborador).ToList();
                        item.TiposAtividades = this.BuscarTiposAtividadeCursoOfertaLocalidadeSelect(item.SeqCursoOfertaLocalidade);
                    }

                    colaboradorVinculoVo.SeqsEntidadesResponsaveis = this.ProgramaDomainService.BuscarSeqsProgramasGrupo(colaboradorVinculoVo.SeqEntidadeVinculo).ToArray();
                    colaboradorVinculoVo.DescricaoTipoVinculoSelect = this.TipoVinculoColaboradorDomainService.SearchProjectionByKey(new SMCSeqSpecification<TipoVinculoColaborador>(colaboradorVinculoVo.SeqTipoVinculoColaborador), p => p.Descricao);

                    colaboradorVinculoVo.PermiteAlterarDadosColaborador = SMCSecurityHelper.Authorize(UC_DCT_001_06_02.PERMITIR_ALTERAR_DADOS_COLABORADOR);

                    var specFormacao = new TipoFormacaoEspecificaFilterSpecification() { Token = TOKEN_TIPO_FORMACAO_ESPECIFICA.LINHA_PESQUISA };
                    colaboradorVinculoVo.SeqTipoFormacaoEspecifica = TipoFormacaoEspecificaDomainService.SearchProjectionByKey(specFormacao, p => p.Seq);

                    bool permitirAlterarDataFimVinculo = false;

                    // TSK 54732
                    /*
                        1- Para os colaboradores que pertencem ao quadro de professores da PUC 
                        (ind_inserido_por_carga = 1)
                        Permitir que o usuário altere a data fim do vinculo para todos os colaboradores  
                        que estejam com a situação 'normal'. Caso o colaborador esteja com a situação 
                        'afastado' ou 'demitido' somente os usuários que possuem permissão no token 
                        'Permitir Alterar Data Fim Vinculo', poderão alterar este campo.

                        2-Para os colaboradores que não pertencem ao quadro de professores da PUC 
                        (ind_inserido_por_carga = 0)
                        -Permitir que a data fim do vinculo possa ser incluída ou alterada sem nenhuma restrição
                     */
                    if (colaboradorVinculo.InseridoPorCarga)
                    {
                        if (colaboradorVinculo.Colaborador.Professores.Any(a => a.SituacaoProfessor == SituacaoProfessor.Normal))
                        {
                            permitirAlterarDataFimVinculo = true;
                        }
                        else if (colaboradorVinculo.Colaborador.Professores.Any(c => c.SituacaoProfessor == SituacaoProfessor.Afastado || c.SituacaoProfessor == SituacaoProfessor.Demitido))
                        {
                            if (SMCSecurityHelper.Authorize(UC_DCT_001_06_04.PERMITIR_ALTERAR_DATA_FIM_VINCULO))
                            {
                                permitirAlterarDataFimVinculo = true;
                            }
                        }
                    }
                    else
                    {
                        permitirAlterarDataFimVinculo = true;
                    }
                    colaboradorVinculoVo.PermitirAlterarDataFimVinculo = permitirAlterarDataFimVinculo;

                    //colaboradorVinculoVo.PermitirAlterarDataFimVinculo = colaboradorVinculo.Colaborador.Professores.Any(a => a.SituacaoProfessor == SituacaoProfessor.Normal) ||
                    //                                                     (colaboradorVinculo.Colaborador.Professores.Any(a => a.SituacaoProfessor != SituacaoProfessor.Normal) &&
                    //                                                     SMCSecurityHelper.Authorize(UC_DCT_001_06_04.PERMITIR_ALTERAR_DATA_FIM_VINCULO));
                }
            }
            finally
            {
                HierarquiaEntidadeItemDomainService.AtivarFiltrosHierarquiaItem(this);
            }

            return colaboradorVinculoVo;
        }

        /// <summary>
        /// Busca os tipos de atividade para o nível de ensino do curso oferta localidade informado na instituição
        /// </summary>
        /// <param name="seqCursoOfertaLocalidade">Sequencial do curso oferta localidade com o nível de ensino</param>
        /// <returns>Atividades associadas ao nível de ensino do curso oferta localidae</returns>
        public List<SMCDatasourceItem> BuscarTiposAtividadeCursoOfertaLocalidadeSelect(long seqCursoOfertaLocalidade)
        {
            var specCursoOferta = new SMCSeqSpecification<CursoOfertaLocalidade>(seqCursoOfertaLocalidade);
            long seqNivelEnsino = this.CursoOfertaLocalidadeDomainService.SearchProjectionByKey(specCursoOferta, p => p.CursoOferta.Curso.SeqNivelEnsino);

            var specAtividades = new InstituicaoNivelTipoAtividadeColaboradorFilterSpecification() { SeqNivelEnsino = seqNivelEnsino };
            var atividadesColaboradorNivel = this.InstituicaoNivelTipoAtividadeColaboradorDomainService
                .SearchProjectionBySpecification(specAtividades, p => p.TipoAtividadeColaborador)
                .ToList();

            var atividadesColaboradorNivelSelect = atividadesColaboradorNivel
                .OrderBy(o => SMCEnumHelper.GetDescription(o))
                .Select(s => new SMCDatasourceItem((long)s, SMCEnumHelper.GetDescription(s)))
                .ToList();

            return atividadesColaboradorNivelSelect;
        }

        /// <summary>
        /// Verifica se a entidade vinculada é do tipo grupo programa
        /// </summary>
        /// <param name="seqEntidadeVinculo">Sequencial da entidade vinculada</param>
        /// <returns>Verdadeiro se a entidade é do tipo grupo programa</returns>
        public bool ValidarVinculoGrupoPrograma(long seqEntidadeVinculo)
        {
            var spec = new SMCSeqSpecification<Entidade>(seqEntidadeVinculo);
            var tokenTipoEntidade = this.EntidadeDomainService.SearchProjectionByKey(spec, p => p.TipoEntidade.Token);
            return tokenTipoEntidade == TOKEN_TIPO_ENTIDADE.GRUPO_PROGRAMA;
        }

        /// <summary>
        /// Exclui o colaborador informado
        /// </summary>
        /// <param name="seq">Sequencial do colaborador a ser excluído</param>
        /// <exception cref="ExclusaoColaboradorVinculoNaoPermitidaException">Caso seja solicitada a exclusão de um colaborador inserido por carga</exception>
        public void ExcluirColaboradorVinculo(long seq)
        {
            var vinculo = SearchByKey(new SMCSeqSpecification<ColaboradorVinculo>(seq));
            if (vinculo.InseridoPorCarga)
            {
                throw new ExclusaoColaboradorVinculoNaoPermitidaException();
            }
            DeleteEntity(vinculo);
        }

        /// <summary>
        /// Busca vinculos do colaborador
        /// </summary>
        /// <param name="filtros">Filtros para busca</param>
        /// <returns>Dados paginados dos vinculos do colaborador</returns>
        public SMCPagerData<ColaboradorVinculoListaVO> BuscarVinculosColaborador(ColaboradorVinculoFilterSpecification spec)
        {
            int total = 0;

            spec.SetOrderByDescending(s => s.DataInicio);

            var filtroHierarquia = GetDataFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            if (filtroHierarquia.SMCAny())
            {
                FilterHelper.DesativarFiltros(this);
            }

            var retorno = this.SearchProjectionBySpecification(spec, p => new ColaboradorVinculoListaVO()
            {
                Seq = p.Seq,
                SeqColaborador = p.SeqColaborador,
                SeqEntidadeVinculo = p.SeqEntidadeVinculo,
                NomeEntidadeVinculo = p.EntidadeVinculo.Nome,
                InseridoPorCarga = p.InseridoPorCarga,
                DescricaoTipoVinculoColaborador = p.TipoVinculoColaborador.Descricao,
                SeqsEntidadeVinculoHierarquiaItens = p.EntidadeVinculo.HierarquiasEntidades.Select(s => s.Seq).ToList(),
                EntidadeResponsavelAcessivelFiltroDados = true,
                DataInicio = p.DataInicio,
                DataFim = p.DataFim,
                Cursos = p.Cursos.OrderBy(o => o.CursoOfertaLocalidade.Nome).Select(s => new ColaboradorVinculoCursoVO()
                {
                    NomeCursoOfertaLocalidade = s.CursoOfertaLocalidade.Nome,
                    NomeLocalidade = s.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                    Seq = s.Seq,
                    SeqCursoOfertaLocalidade = s.SeqCursoOfertaLocalidade,
                    TipoAtividadeColaborador = s.Atividades.Select(sa => sa.TipoAtividadeColaborador).ToList()
                }).ToList()
            }, out total).ToList();

            if (filtroHierarquia.SMCAny())
            {
                FilterHelper.AtivarFiltros(this);
                foreach (var item in retorno)
                {
                    item.EntidadeResponsavelAcessivelFiltroDados = filtroHierarquia.Intersect(item.SeqsEntidadeVinculoHierarquiaItens).Any();
                }
            }

            return new SMCPagerData<ColaboradorVinculoListaVO>(retorno, total);
        }

        public void ValidarDatasVinculo(ColaboradorVinculoVO model)
        {
            var dataFimPeriodo = model.DataFim ?? DateTime.MaxValue;

            var vinculoRequerAcompanhamentoSupervisor = TipoVinculoColaboradorDomainService.SearchProjectionByKey(model.SeqTipoVinculoColaborador, p => p.RequerAcompanhamentoSupervisor);

            // Valida as datas das formações específicas
            if (model.FormacoesEspecificas != null && model.FormacoesEspecificas.Any())
            {
                foreach (var formacao in model.FormacoesEspecificas)
                {
                    var dataFimFormacao = formacao.DataFim ?? DateTime.MaxValue;

                    // Valida a data início com a data que foi informada no vínculo do colaborador em questão
                    if (model.DataInicio > formacao.DataInicio)
                        throw new ColaboradorVinculoDatasPeriodoException();
                    // Valida a data fim com a data que foi informada no vínculo do colaborador em questão
                    else if (dataFimPeriodo < dataFimFormacao)
                        throw new ColaboradorVinculoDatasPeriodoException();
                }
            }

            if (vinculoRequerAcompanhamentoSupervisor)
            {
                if (!model.DataFim.HasValue)
                {
                    throw new ColaboradorTipoVinculoRequerDataFimException();
                }
                if (!model.ColaboradoresResponsaveis.SMCAny())
                {
                    throw new ColaboradorVinculoResponsavelObrigatorioException();
                }
                var validacaoSobreposicao = ValidacaoData.ValidarSobreposicaoPeriodos(model.ColaboradoresResponsaveis, nameof(ColaboradorVinculoVO.DataInicio), nameof(ColaboradorVinculoVO.DataFim));
                if (!validacaoSobreposicao)
                {
                    throw new ColaboradorVinculoSupervisaoSobrepostaException();
                }
                var validacaoContinuidade = ValidacaoData.ValidarContinuidadePeriodos(model.ColaboradoresResponsaveis, m => m.DataInicio, m => m.DataFim);
                if (!validacaoContinuidade)
                {
                    throw new ColaboradorVinculoIntervaloResponsavelException();
                }
            }

            // Valida se as datas informadas estão de acordo com a regra de negócio NV14
            /* O período de datas de supervisão do professor responsável deverá estar dentro do período de datas do vínculo do professor pós-doc e do professor responsável. Ou seja:
                * -A data início deverá necessariamente ser maior ou igual a data início do vínculo do professor pós-doc e do professor responsável.
                * -A data fim deverá necessariamente ser menor ou igual a data fim do vínculo do professor pós-doc e do professor responsável.

                Se a data fim do vínculo for informada, a data fim de supervisão do professor deverá ser obrigatória.*/
            if (model.ColaboradoresResponsaveis != null && model.ColaboradoresResponsaveis.Any())
            {
                foreach (var colaborador in model.ColaboradoresResponsaveis)
                {
                    //var dadosColaborador = ColaboradorDomainService.BuscarColaboradorLookup(colaborador.SeqColaboradorResponsavel);
                    var dataFimColaborador = colaborador.DataFim ?? DateTime.MaxValue;

                    // Valida a data início com a data que foi informada no vínculo do colaborador em questão
                    if (model.DataInicio > colaborador.DataInicio)
                        //throw new SMCApplicationException($"Data início do vínculo do professor responsável {dadosColaborador.NomeSocial ?? dadosColaborador.Nome} deve ser maior ou igual a data início do vínculo do colaborador.");
                        throw new ColaboradorVinculoDatasPeriodoException();
                    // Valida a data fim com a data que foi informada no vínculo do colaborador em questão
                    else if (dataFimPeriodo < dataFimColaborador)
                        //throw new SMCApplicationException($"Data de fim do vínculo do professor responsável {dadosColaborador.NomeSocial ?? dadosColaborador.Nome} deve ser menor ou igual a data fim do vínculo do colaborador.");
                        throw new ColaboradorVinculoDatasPeriodoException();
                    // Passou.. verifica com o período de vínculo do professor responsável
                    else
                    {
                        // Recupera os vínculos ativos do colaborador
                        var vinculos = BuscarVinculosColaborador(new ColaboradorVinculoFilterSpecification { SeqColaborador = colaborador.SeqColaboradorResponsavel });
                        if (vinculos == null || !vinculos.Any())
                            //throw new SMCApplicationException($"Professor responsável {dadosColaborador.NomeSocial ?? dadosColaborador.Nome} não possui nenhum vínculo ativo");
                            throw new ColaboradorVinculoDatasPeriodoException();

                        bool encontrou = false;
                        foreach (var vinculo in vinculos)
                        {
                            var dataFimVinculo = vinculo.DataFim ?? DateTime.MaxValue;

                            if (vinculo.DataInicio <= colaborador.DataInicio && dataFimVinculo >= dataFimColaborador)
                            {
                                encontrou = true;
                                break;
                            }
                        }

                        if (!encontrou)
                            //throw new SMCApplicationException($"As datas de início e fim do vínculo do professor responsável {dadosColaborador.NomeSocial ?? dadosColaborador.Nome} não corresponde com nenhum período de vínculo ativo do professor na instituição.");
                            throw new ColaboradorVinculoDatasPeriodoException();
                    }
                }
            }

            if (model.DataFim.HasValue)
            {
                var retorno = BuscarColaboradorVinculo(model.Seq);

                if (retorno != null)
                {
                    var seqsCursoOfertaLocalidade = retorno.Cursos.Select(s => s.SeqCursoOfertaLocalidade).ToList();

                    if (seqsCursoOfertaLocalidade.Any())
                    {
                        var spec = new EventoAulaFilterSpecification { SeqsColaborador = new List<long>() { model.SeqColaborador } };
                        var eventos = EventoAulaDomainService.SearchProjectionBySpecification(spec, e => new
                        {
                            e.Seq,
                            e.SeqDivisaoTurma,
                            e.Data,
                            SeqsCursoOfertaLocalidade = e.DivisaoTurma.Turma.ConfiguracoesComponente.SelectMany(sm => sm.RestricoesTurmaMatriz.Select(s => s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Seq)).ToList(),
                            Associado = e.DivisaoTurma.Turma.ConfiguracoesComponente.SelectMany(w => w.RestricoesTurmaMatriz).Any(s => seqsCursoOfertaLocalidade.Contains(s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Seq))
                        }).ToList();

                        if (retorno.DataFim.HasValue)
                        {
                            var alterouData = DateTime.Compare(model.DataFim.Value, retorno.DataFim.Value);
                            if (alterouData < 0)
                            {
                                var outrosRegistros = BuscarVinculosColaborador(new ColaboradorVinculoFilterSpecification { SeqEntidadeVinculo = model.SeqEntidadeVinculo, SeqColaborador = model.SeqColaborador }).Where(w => w.Seq != retorno.Seq && w.DataInicio > retorno.DataInicio).ToList();
                                DateTime? dataInicioProximoVinculo;
                                if (outrosRegistros != null && outrosRegistros.Any())
                                {
                                    dataInicioProximoVinculo = outrosRegistros.OrderByDescending(o => o.Seq).FirstOrDefault().DataInicio;
                                    eventos.Where(w => w.Associado).SMCForEach(f =>
                                    {
                                        if (f.Data >= model.DataFim && (outrosRegistros != null && f.Data < dataInicioProximoVinculo.Value))
                                            throw new ColaboradorVinculoAssociadoEventoAulaPosterior();
                                    });
                                }
                                else
                                {
                                    eventos.Where(w => w.Associado).SMCForEach(f =>
                                    {
                                        if (f.Data >= model.DataFim)
                                            throw new ColaboradorVinculoAssociadoEventoAulaPosterior();
                                    });
                                }
                            }
                        }
                        else
                        {
                            eventos.Where(w => w.Associado).SMCForEach(f =>
                            {
                                if (f.Data >= model.DataFim)
                                    throw new ColaboradorVinculoAssociadoEventoAulaPosterior();
                            });
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Salvar dados do colaborador vinculo
        /// </summary>
        /// <param name="colaboradorVinculoVO">Dados as serem salvos</param>
        /// <returns>Sequencial do colaborador</returns>
        public long SalvarColaboradorVinculo(ColaboradorVinculoVO colaboradorVinculoVO)
        {
            var model = colaboradorVinculoVO.Transform<ColaboradorVinculo>();

            ///Não deverá ser permitido que um mesmo colaborador tenha um vínculo para a mesma entidade,
            ///tipo e período de datas coincidentes. Caso isso ocorra, emitir mensagem de erro abaixo e abortar a
            ///operação: "Inclusão/Alteração não permitida. Para este professor/pesquisador já existe um vínculo para mesma entidade, tipo e período coincidentes.".
            var vinculos = this.SearchBySpecification(new ColaboradorVinculoFilterSpecification() { SeqColaborador = colaboradorVinculoVO.SeqColaborador }).ToList();

            ///Caso seja uma edição ele subistiui pelo modelo mais novo
            vinculos = vinculos.Where(w => w.Seq != colaboradorVinculoVO.Seq).ToList();
            vinculos.Add(model);

            var grupoVinculo = vinculos.Where(w => w.SeqEntidadeVinculo == model.SeqEntidadeVinculo).GroupBy(g => $"{g.SeqEntidadeVinculo} | {g.SeqTipoVinculoColaborador}");

            foreach (var item in grupoVinculo)
            {
                if (!ValidacaoData.ValidarSobreposicaoPeriodos(item.ToList(), nameof(ColaboradorVinculo.DataInicio), nameof(ColaboradorVinculo.DataFim)))
                {
                    throw new ColaboradorVinculoEntidadeTipoDataCoincidenteException(colaboradorVinculoVO.Seq == 0 ? INCLUAO : ALTERACAO);
                }
            }

            ValidarSobreposicaoPeriodosFormacoesEspecificas(model.FormacoesEspecificas, colaboradorVinculoVO.Seq == 0 ? INCLUAO : ALTERACAO);
            ValidarDatasVinculo(colaboradorVinculoVO);

            var configTipoVinculo = TipoVinculoColaboradorDomainService.SearchProjectionByKey(model.SeqTipoVinculoColaborador, p => new
            {
                p.CriaVinculoInstitucional,
                p.RequerAcompanhamentoSupervisor
            });

            // Caso o tipo de vínculo não necessite de acompanhamento, o campo de professores não será enviado no post,
            // então a lista é iniciada com vazio para limpar os valores anteriores no caso de uma edição.
            if (model.Seq > 0 && !configTipoVinculo.RequerAcompanhamentoSupervisor)
            {
                model.ColaboradoresResponsaveis = new List<ColaboradorResponsavelVinculo>();
                model.TituloPesquisa = null;
                model.Observacao = null;
            }

            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                // Inclui uma atuação no CAD apenas se o vinculo for ativo e criar vinculo institucional
                if (model.DataInicio <= DateTime.Today && (!model.DataFim.HasValue || DateTime.Today <= model.DataFim) && configTipoVinculo.CriaVinculoInstitucional)
                {
                    var dadosColaborador = ColaboradorDomainService.SearchProjectionByKey(model.SeqColaborador, p => new 
                    {
                        p.SeqPessoa, 
                        p.Pessoa.Cpf,
                        p.Pessoa.NumeroPassaporte,
                        p.Pessoa.DataNascimento
                    });

                    // Task 51461 - Se não tiver CPF ou Passaporte, nesse ponto não foi inserido uma pessoa em Dados Mestres.
                    // Task 57733 - Se não informou a data de nascimento, também não deve integrar com os Dados Mestres.
                    // Se não integra com DadosMestres, também não integra com o CAD
                    if (dadosColaborador.DataNascimento.HasValue && (!string.IsNullOrEmpty(dadosColaborador.Cpf) || !string.IsNullOrEmpty(dadosColaborador.NumeroPassaporte)))
                    {
                        int codigoPessoaDadosMestres = PessoaDomainService.BuscarCodigoDePessoaNosDadosMestres(dadosColaborador.SeqPessoa, TipoPessoa.Fisica, null);
                        var result = PessoaService.IncluirAtuacao(new AtuacaoData()
                        {
                            CodigoPessoa = codigoPessoaDadosMestres,
                            CodigoPapel = 9,
                            CodigoSistema = "SGA",
                            CodigoConexao = 24
                        });
                        if (!result)
                            throw new SMCApplicationException($"Falha ao criar uma atuação para o colaborador {model.SeqColaborador} no CAD");
                    }
                }
                this.SaveEntity(model);
                transacao.Commit();
            }
            return model.Seq;
        }

        /// <summary>
        /// Verificar se existe a mesma formação especifica com período de datas coincidentes
        /// </summary>
        /// <param name="formacaoEspecificas">Formações específicas do vínculo</param>
        /// <param name="operacao">Operação para a mensagem de erro</param>
        /// <exception cref="ColaboradorVinculoMesmaFormacaoDatasCoincidentesException">Caso ocorra sobreposição de datas para dois vínculos com a mesma formação</exception>
        public void ValidarSobreposicaoPeriodosFormacoesEspecificas(IEnumerable<ColaboradorVinculoFormacaoEspecifica> formacaoEspecificas, string operacao)
        {
            if (formacaoEspecificas.Any())
            {
                var gruposFormacoes = formacaoEspecificas.GroupBy(g => g.SeqFormacaoEspecifica);
                foreach (var grupo in gruposFormacoes)
                {
                    if (!ValidacaoData.ValidarSobreposicaoPeriodos(grupo.ToList(), nameof(ColaboradorVinculoFormacaoEspecifica.DataInicio), nameof(ColaboradorVinculoFormacaoEspecifica.DataFim)))
                    {
                        throw new ColaboradorVinculoMesmaFormacaoDatasCoincidentesException(operacao);
                    }
                }
            }
        }

        public List<SMCDatasourceItem> BuscarPosDoutorandosSelect(long seqEntidadeResponsavel)
        {
            var listaPorDoutorandos = new List<SMCDatasourceItem>();

            var specColaboradorVinculo = new ColaboradorVinculoFilterSpecification()
            {
                SeqEntidadeVinculo = seqEntidadeResponsavel,
                VinculoAtivo = false,
                TokenTipoVinculoColaborador = TOKEN_TIPO_VINCULO_COLABORADOR.POS_DOUTORANDO
            };

            var colaboradoresVinculo = this.SearchProjectionBySpecification(specColaboradorVinculo, c => new
            {
                c.Colaborador.Seq,
                c.Colaborador.DadosPessoais.Nome,
                c.Colaborador.DadosPessoais.NomeSocial,
            }).Distinct().ToList();

            colaboradoresVinculo.ForEach(c =>
            {
                listaPorDoutorandos.Add(new SMCDatasourceItem()
                {
                    Seq = c.Seq,
                    Descricao = PessoaDadosPessoaisDomainService.FormatarNomeSocial(c.Nome, c.NomeSocial)
                });
            });

            return listaPorDoutorandos.OrderBy(o => o.Descricao).ToList();
        }

        public List<SMCDatasourceItem> BuscarVinculosColaboradorSelect(long seqColaborador, long seqEntidadeResponsavel)
        {
            var listaVinculosColaborador = new List<SMCDatasourceItem>();

            var spec = new ColaboradorVinculoFilterSpecification()
            {
                SeqEntidadeVinculo = seqEntidadeResponsavel,
                SeqColaborador = seqColaborador,
                VinculoAtivo = false,
                TokenTipoVinculoColaborador = TOKEN_TIPO_VINCULO_COLABORADOR.POS_DOUTORANDO
            };

            var vinculosColaborador = this.SearchProjectionBySpecification(spec, c => new
            {
                c.Seq,
                c.DataInicio,
                c.DataFim,
                c.TituloPesquisa
            }).ToList();

            vinculosColaborador.ForEach(v =>
            {
                listaVinculosColaborador.Add(new SMCDatasourceItem()
                {
                    Seq = v.Seq,
                    Descricao = $"{v.DataInicio.ToString("dd/MM/yyyy")} a {v.DataFim.Value.ToString("dd/MM/yyyy")} - {(string.IsNullOrEmpty(v.TituloPesquisa) ? "NÃO INFORMADO" : v.TituloPesquisa)}"
                });
            });

            return listaVinculosColaborador;
        }

        public RelatorioCertificadoPosDoutorListaVO BuscarDadosCertificadoPosDoutor(RelatorioCertificadoPosDoutorFiltroVO filtro)
        {
            var spec = new SMCSeqSpecification<ColaboradorVinculo>(filtro.SeqColaboradorVinculo);
            var colaboradorVinculo = this.SearchProjectionByKey(spec, c => new
            {
                c.Seq,
                c.DataInicio,
                c.DataFim,
                NomeColaborador = c.Colaborador.DadosPessoais.Nome,
                c.Colaborador.DadosPessoais.NomeSocial,
                SexoColaborador = c.Colaborador.DadosPessoais.Sexo,
                c.TituloPesquisa,
                NomeEntidadeVinculo = c.EntidadeVinculo.Nome,
                ProfessorResponsavelMaiorDataFim = c.ColaboradoresResponsaveis.Where(p => p.DataFim.HasValue).Select(p => new
                {
                    p.DataFim,
                    p.ColaboradorResponsavel.DadosPessoais.Nome,
                    p.ColaboradorResponsavel.DadosPessoais.NomeSocial,
                    p.ColaboradorResponsavel.DadosPessoais.Sexo
                }).OrderByDescending(o => o.DataFim).FirstOrDefault(),
                ProfessorResponsavelDataFimNula = c.ColaboradoresResponsaveis.Where(p => !p.DataFim.HasValue).Select(p => new
                {
                    p.DataFim,
                    p.ColaboradorResponsavel.DadosPessoais.Nome,
                    p.ColaboradorResponsavel.DadosPessoais.NomeSocial,
                    p.ColaboradorResponsavel.DadosPessoais.Sexo
                }).FirstOrDefault(),
            });

            var dadosCertificado = new RelatorioCertificadoPosDoutorListaVO()
            {
                DataEmissao = $"{DateTime.Today.Day} de {SMCDateTimeHelper.GetMonthName(DateTime.Today.Month)} de {DateTime.Today.Year}.",
                DataInicioVinculo = colaboradorVinculo.DataInicio.ToString("dd/MM/yyyy"),
                DataFimVinculo = colaboradorVinculo.DataFim.GetValueOrDefault().ToString("dd/MM/yyyy"),
                NomeColaboradorPosDoutorando = PessoaDadosPessoaisDomainService.FormatarNomeSocial(colaboradorVinculo.NomeColaborador, colaboradorVinculo.NomeSocial),
                NomeEntidadeResponsavel = colaboradorVinculo.NomeEntidadeVinculo,
                TituloPesquisa = !string.IsNullOrEmpty(colaboradorVinculo.TituloPesquisa) ? colaboradorVinculo.TituloPesquisa : "NÃO INFORMADO"
            };

            var strSupervisionado = colaboradorVinculo.SexoColaborador == Sexo.Feminino ? "supervisionada" : "supervisionado";
            if (colaboradorVinculo.ProfessorResponsavelDataFimNula != null)
            {
                var strPor = colaboradorVinculo.ProfessorResponsavelDataFimNula.Sexo == Sexo.Feminino ? "pela professora" : "pelo professor";
                dadosCertificado.NomeProfessorResponsavel = $"{strSupervisionado} {strPor} {PessoaDadosPessoaisDomainService.FormatarNomeSocial(colaboradorVinculo.ProfessorResponsavelDataFimNula.Nome, colaboradorVinculo.ProfessorResponsavelDataFimNula.NomeSocial)}";
            }
            else if (colaboradorVinculo.ProfessorResponsavelMaiorDataFim != null)
            {
                var strPor = colaboradorVinculo.ProfessorResponsavelMaiorDataFim.Sexo == Sexo.Feminino ? "pela professsora" : "pelo professor";
                dadosCertificado.NomeProfessorResponsavel = $"{strSupervisionado} {strPor} {PessoaDadosPessoaisDomainService.FormatarNomeSocial(colaboradorVinculo.ProfessorResponsavelMaiorDataFim.Nome, colaboradorVinculo.ProfessorResponsavelMaiorDataFim.NomeSocial)}";
            }
            else
                dadosCertificado.NomeProfessorResponsavel = "NÃO INFORMADO";

            return dadosCertificado;
        }
    }
}