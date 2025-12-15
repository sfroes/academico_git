using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Helpers;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class MatrizCurricularOfertaDomainService : AcademicoContextDomain<MatrizCurricularOferta>
    {
        #region [ DomainServices ]

        private CampanhaOfertaDomainService CampanhaOfertaDomainService => Create<CampanhaOfertaDomainService>();
        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();
        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();
        private TermoIntercambioDomainService TermoIntercambioDomainService => Create<TermoIntercambioDomainService>();
        private DispensaDomainService DispensaDomainService => Create<DispensaDomainService>();

        #endregion [ DomainServices ]

        #region [ Queries ]

        #region [ _buscarIntegralizacaoConfiguracoesMatrizCurricularOferta ]

        private string _buscarIntegralizacaoConfiguracoesMatrizCurricularOferta =
                        @"  SELECT  DMAT.seq_divisao_matriz_curricular AS SeqDivisaoMatrizCurricular,
                                    IIF(DMCC.seq_divisao_matriz_curricular is null,CAST(0 AS SMALLINT) ,DIMAT.num_item) AS NumeroDivisao,
	                                IIF(DMCC.seq_divisao_matriz_curricular is null,'' ,DIMAT.dsc_divisao_curricular_item ) AS DescricaoDivisao,
	                                GC.seq_grupo_curricular AS SeqGrupoCurricular,
                                    GC.dsc_grupo_curricular AS DescricaoGrupo,
                                    GC.idt_dom_formato_configuracao_grupo AS FormatoConfiguracaoGrupo,
	                                GC.qtd_hora_aula AS HoraAulaGrupo,
	                                GC.qtd_hora_relogio AS HoraGrupo,
	                                GC.qtd_creditos AS CreditoGrupo,
	                                GC.qtd_itens AS ItensGrupo,
                                    GC.seq_formacao_especifica AS SeqFormacaoEspecifica,
	                                TCGC.dsc_tipo_configuracao_grupo_curricular AS DescricaoTipoConfiguracaoGrupo,
                                    DMCC.seq_configuracao_componente AS SeqConfiguracaoComponente,
                                    CONC.seq_componente_curricular AS SeqComponenteCurricular,
                                    TCONCC.sgl_tipo_componente_curricular AS SiglaTipoComponenteCurricular,
                                    CONC.cod_configuracao_componente AS CodigoConfiguracao,
									CONC.dsc_configuracao_componente AS DescricaoConfiguracao,
                                    CONCC.qtd_carga_horaria as CargaHoraria,
									CONCC.qtd_credito as Credito,
                                    GCC.ind_permite_origem_dispensa_mesmo_curriculo as PermiteOrigemDispensaMesmoCurriculo
                            FROM [ACADEMICO].[CUR].[matriz_curricular_oferta] MATO
                            INNER JOIN [ACADEMICO].[CUR].[matriz_curricular] MAT ON MATO.seq_matriz_curricular = MAT.seq_matriz_curricular
                            LEFT JOIN [ACADEMICO].[CUR].[divisao_matriz_curricular] DMAT ON MAT.seq_matriz_curricular = DMAT.seq_matriz_curricular
                            LEFT JOIN [ACADEMICO].[CUR].[divisao_curricular_item] DIMAT ON DMAT.seq_divisao_curricular_item = DIMAT.seq_divisao_curricular_item
                            LEFT JOIN [ACADEMICO].[CUR].[divisao_matriz_curricular_componente] DMCC ON MAT.seq_matriz_curricular = DMCC.seq_matriz_curricular
                            and	DMCC.seq_divisao_matriz_curricular = DMAT.seq_divisao_matriz_curricular
                            LEFT JOIN [ACADEMICO].[CUR].[grupo_curricular_componente] GCC ON DMCC.seq_grupo_curricular_componente = GCC.seq_grupo_curricular_componente
                            LEFT JOIN [ACADEMICO].[CUR].[configuracao_componente] CONC ON CONC.seq_configuracao_componente = DMCC.seq_configuracao_componente
                            LEFT JOIN [ACADEMICO].[CUR].[componente_curricular] CONCC ON CONC.seq_componente_curricular = CONCC.seq_componente_curricular
							LEFT JOIN [ACADEMICO].[CUR].[tipo_componente_curricular] TCONCC ON CONCC.seq_tipo_componente_curricular = TCONCC.seq_tipo_componente_curricular
							LEFT JOIN [ACADEMICO].[CUR].[grupo_curricular] GC ON GCC.seq_grupo_curricular = GC.seq_grupo_curricular
                            LEFT JOIN [ACADEMICO].[CUR].[tipo_configuracao_grupo_curricular] TCGC ON GC.seq_tipo_configuracao_grupo_curricular = TCGC.seq_tipo_configuracao_grupo_curricular
                            WHERE MATO.seq_matriz_curricular_oferta = {0}
                            ";

        #endregion [ _buscarIntegralizacaoConfiguracoesMatrizCurricularOferta ]

        #region [ _validaBeneficioIntegralizacaoConfiguracoesMatrizCurricularOferta ]

        private string _validaBeneficioIntegralizacaoConfiguracoesMatrizCurricularOferta =
                        @"  AND
							(
								EXISTS(SELECT GCB.seq_grupo_curricular FROM [ACADEMICO].[CUR].[grupo_curricular_beneficio] GCB WHERE GCB.seq_grupo_curricular = GC.seq_grupo_curricular AND GCB.seq_beneficio IN ({0}))
								OR
								NOT EXISTS(SELECT GCB.seq_grupo_curricular FROM [ACADEMICO].[CUR].[grupo_curricular_beneficio] GCB WHERE GCB.seq_grupo_curricular = GC.seq_grupo_curricular)
							)
							";

        #endregion [ _validaBeneficioIntegralizacaoConfiguracoesMatrizCurricularOferta ]

        #region [ _validaCondicaoIntegralizacaoConfiguracoesMatrizCurricularOferta ]

        private string _validaCondicaoIntegralizacaoConfiguracoesMatrizCurricularOferta =
                        @"  AND
							(
								EXISTS(SELECT GCCO.seq_grupo_curricular FROM [ACADEMICO].[CUR].[grupo_curricular_condicao_obrigatoriedade] GCCO WHERE GCCO.seq_grupo_curricular = GC.seq_grupo_curricular AND GCCO.seq_condicao_obrigatoriedade IN ({0}))
								OR
								NOT EXISTS(SELECT GCCO.seq_grupo_curricular FROM [ACADEMICO].[CUR].[grupo_curricular_condicao_obrigatoriedade] GCCO WHERE GCCO.seq_grupo_curricular = GC.seq_grupo_curricular)
							)
                            ";

        #endregion [ _validaCondicaoIntegralizacaoConfiguracoesMatrizCurricularOferta ]

        #region [ _validaFormacaoIntegralizacaoConfiguracoesMatrizCurricularOferta ]

        private string _validaFormacaoIntegralizacaoConfiguracoesMatrizCurricularOferta =
                        @"  AND
							(
                                GC.seq_formacao_especifica IS NULL
                                OR
								GC.seq_formacao_especifica IN ({0})
							)
                            ";

        #endregion [ _validaFormacaoIntegralizacaoConfiguracoesMatrizCurricularOferta ]

        #region [ _filtroIntegralizacaoComponentesMatrizCurricularOferta ]

        private string _filtroIntegralizacaoComponentesMatrizCurricularOferta =
                        @"  AND
							(
                                CONC.seq_componente_curricular IN ({0})
							)
                            ";

        #endregion [ _filtroIntegralizacaoComponentesMatrizCurricularOferta ]

        #region [ _filtroIntegralizacaoComponentesDiferentesMatrizCurricularOferta ]

        private string _filtroIntegralizacaoComponentesDiferentesMatrizCurricularOferta =
                        @"  AND
							(
                                CONC.seq_componente_curricular NOT IN ({0})
							)
                            ";

        #endregion [ _filtroIntegralizacaoComponentesDiferentesMatrizCurricularOferta ]

        #region [ _filtroIntegralizacaoComponentesDescricaoMatrizCurricularOferta ]

        private string _filtroIntegralizacaoComponentesDescricaoMatrizCurricularOferta =
                        @"  AND
							(
                                CONC.dsc_configuracao_componente LIKE '%{0}%'
							)
                            ";

        #endregion [ _filtroIntegralizacaoComponentesDescricaoMatrizCurricularOferta ]

        #endregion [ Queries ]

        /// <summary>
        /// Busca matriz curricular oferta para o cabeçalho
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular oferta</param>
        /// <returns>Matriz curricular oferta data para o cabeçalho</returns>
        public MatrizCurricularOfertaCabecalhoVO BuscarMatrizCurricularOfertaCabecalho(long seq)
        {
            var matrizCurricularOferta = this.SearchProjectionByKey(new SMCSeqSpecification<MatrizCurricularOferta>(seq),
                                s => new MatrizCurricularOfertaCabecalhoVO
                                {
                                    Seq = s.Seq,
                                    Codigo = s.Codigo,
                                    DescricaoUnidade = s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                                    DescricaoLocalidade = s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                                    DescricaoTurno = s.CursoOfertaLocalidadeTurno.Turno.Descricao,
                                    CurriculoCodigo = s.MatrizCurricular.CurriculoCursoOferta.Curriculo.Codigo,
                                    CurriculoDescricao = s.MatrizCurricular.CurriculoCursoOferta.Curriculo.Descricao,
                                    CurriculoDescricaoComplementar = s.MatrizCurricular.CurriculoCursoOferta.Curriculo.DescricaoComplementar,
                                    MatrizCodigo = s.MatrizCurricular.Codigo,
                                    MatrizDescricao = s.MatrizCurricular.Descricao,
                                    MatrizDescricaoComplementar = s.MatrizCurricular.DescricaoComplementar,
                                });

            return matrizCurricularOferta;
        }

        /// <summary>
        /// Busca matriz curricular oferta
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular oferta</param>
        /// <returns>Matriz curricular oferta</returns>
        public MatrizCurricularOfertaVO BuscarMatrizCurricularOferta(long seq, bool ignorarFiltroDados = false)
        {
            if (ignorarFiltroDados) { FilterHelper.DesativarFiltros(this); }

            var matrizCurricularOferta = this.SearchByKey(new SMCSeqSpecification<MatrizCurricularOferta>(seq), IncludesMatrizCurricularOferta.MatrizCurricular)
                .Transform<MatrizCurricularOfertaVO>();

            if (ignorarFiltroDados) { FilterHelper.AtivarFiltros(this); }

            return matrizCurricularOferta;
        }

        /// <summary>
        /// Busca o SeqCursoOfertaLocalidadeTurno da matriz curricular oferta
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular oferta</param>
        /// <returns>SeqCursoOfertaLocalidadeTurno da Matriz curricular oferta</returns>
        public long BuscarMatrizCurricularOfertaSeqCursoOfertaLocalidadeTurno(long seq, bool ignorarFiltroDados = false)
        {
            if (ignorarFiltroDados) { FilterHelper.DesativarFiltros(this); }

            var matrizCurricularOfertaSeqCursoOfertaLocalidadeTurno = this.SearchProjectionByKey(seq, x => x.SeqCursoOfertaLocalidadeTurno);

            if (ignorarFiltroDados) { FilterHelper.AtivarFiltros(this); }

            return matrizCurricularOfertaSeqCursoOfertaLocalidadeTurno;
        }

        /// <summary>
        /// Lista matriz curricular oferta de acordo com filtros
        /// </summary>
        /// <param name="filtro">Filtro matriz curricular oferta</param>
        /// <returns>Lista paginada das matrizes curriculares ofertas</returns>
        public List<MatrizCurricularOfertaVO> BuscarMatrizesCurricularesOfertas(MatrizCurricularOfertaFiltroVO filtro)
        {
            var spec = filtro.Transform<MatrizCurricularOfertaFilterSpecification>();
            var dataAtual = filtro.DataHistorico ?? DateTime.Now.Date;
            var matrizCurricularOferta = this.SearchProjectionBySpecification(spec,
                s => new MatrizCurricularOfertaVO()
                {
                    Seq = s.Seq,
                    SeqCursoOfertaLocalidadeTurno = s.SeqCursoOfertaLocalidadeTurno,
                    Codigo = s.Codigo,
                    DescricaoUnidade = s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                    DescricaoLocalidade = s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                    DescricaoTurno = s.CursoOfertaLocalidadeTurno.Turno.Descricao,
                    NumeroPeriodoAtivo = s.NumeroPeriodoAtivo,
                    QuantidadeMesesSolicitacaoProrrogacao = s.MatrizCurricular.QuantidadeMesesSolicitacaoProrrogacao,
                    DataInicioVigencia = s.HistoricosSituacao.Where(w => w.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.Ativa).Max(m => m.DataInicio),
                    DataFinalVigencia = s.HistoricosSituacao.Where(w => w.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.Extinta).Max(m => m.DataInicio),
                    HistoricoSituacaoAtual = s.HistoricosSituacao.Count(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)) > 0 ?
                                                                                                 s.HistoricosSituacao.Where(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)).FirstOrDefault().SituacaoMatrizCurricularOferta :
                                                                                                 s.HistoricosSituacao.Where(w => w.DataFim == null).FirstOrDefault().SituacaoMatrizCurricularOferta,
                });

            return matrizCurricularOferta.ToList();
        }

        /// <summary>
        /// Busca as matrizes que atendam os filtros informados
        /// </summary>
        /// <param name="filtros">Dados do filtro</param>
        /// <returns>KeyValue das matrizes com a chave e a descrição no formato
        /// [RN_CUR_016 - Exibição de descrição de oferta de matriz curricular] + [Situação atual da oferta de matriz]</returns>
        public List<SMCDatasourceItem> BuscarMatrizesCurricularesOfertasPorCampanhaSelect(MatrizCurricularOfertaFiltroVO filtros)
        {
            DateTime dataMatriz = DateTime.MaxValue;

            if (filtros.SeqTipoVinculoAluno.HasValue && filtros.SeqNivelEnsino.HasValue && filtros.SeqCicloLetivo.HasValue)
            {
                var specConfigNivel = new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
                {
                    SeqTipoVinculoAluno = filtros.SeqTipoVinculoAluno,
                    SeqNivelEnsino = filtros.SeqNivelEnsino
                };
                var vinculoConcedeFormacao = InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionByKey(specConfigNivel, p =>
                    p.ConcedeFormacao || p.TiposTermoIntercambio.Any(a => a.ConcedeFormacao));

                // Caso o vínculo conceda formação, recupera a menor data de início de periodo letivo com base nas ofertas selecionadas
                if (vinculoConcedeFormacao)
                {
                    var specCursosOfertasLocalidadesTurnos = new SMCContainsSpecification<CampanhaOferta, long>(p => p.Seq, filtros.SeqsCampanhaOferta.ToArray());
                    var seqsCursoOfertaLocalidadeTurno = this.CampanhaOfertaDomainService.SearchProjectionBySpecification(specCursosOfertasLocalidadesTurnos, p =>
                        p.Itens.FirstOrDefault().SeqCursoOfertaLocalidadeTurno)
                        .Where(w => w.HasValue).Select(s => s.Value).ToList();

                    foreach (var seqCursoOfertaLocalidadeTurno in seqsCursoOfertaLocalidadeTurno)
                    {
                        // Recupera o periodo letivo conforme a regra RN_CAM_030
                        var periodoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(filtros.SeqCicloLetivo.Value,
                            seqCursoOfertaLocalidadeTurno,
                            TipoAluno.Calouro,
                            TOKEN_TIPO_EVENTO.PERIODO_LETIVO);
                        if (dataMatriz > periodoLetivo.DataInicio)
                        {
                            dataMatriz = periodoLetivo.DataInicio;
                        }
                    }
                }
                // Caso contrário, recupera a menor data nos termos associados
                else if (filtros.SeqsTermoIntercambio.SMCAny())
                {
                    var specTermoIntercambio = new SMCContainsSpecification<TermoIntercambio, long>(p => p.Seq, filtros.SeqsTermoIntercambio.ToArray());
                    dataMatriz = TermoIntercambioDomainService.SearchProjectionBySpecification(specTermoIntercambio, p => p.Vigencias.Min(m => m.DataInicio)).Min();
                }
                else
                {
                    return new List<SMCDatasourceItem>();
                }
            }

            // Recupera apenas as matrizes ativas da data base
            var specCampanhaOferta = new SMCContainsSpecification<CampanhaOferta, long>(p => p.Seq, filtros.SeqsCampanhaOferta.ToArray());
            var consultaMatrizes = this.CampanhaOfertaDomainService.SearchProjectionBySpecification(specCampanhaOferta, p =>
                p.Itens.FirstOrDefault().CursoOfertaLocalidadeTurno.MatrizesCurriculares
                .Where(w => w.HistoricosSituacao
                    .Any(a =>
                        a.DataInicio <= dataMatriz && (!a.DataFim.HasValue || a.DataFim >= dataMatriz) &&
                        a.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.Ativa
                    )
                )
                .Select(s => new
                {
                    s.Seq,
                    s.MatrizCurricular.Descricao,
                    s.MatrizCurricular.DescricaoComplementar,
                    Localidade = s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                    Turno = s.CursoOfertaLocalidadeTurno.Turno.Descricao
                }).ToList()).SelectMany(s => s);

            var matrizes = new List<SMCDatasourceItem>();

            if (consultaMatrizes == null)
                return matrizes;

            foreach (var item in consultaMatrizes)
            {
                string descricaoMatriz = GerarDescricaoMatrizCurricuarOferta(item.Descricao, item.DescricaoComplementar, item.Localidade, item.Turno);
                matrizes.Add(new SMCDatasourceItem(item.Seq, $"{descricaoMatriz} - {SituacaoMatrizCurricularOferta.Ativa.SMCGetDescription()}"));
            }

            // Ordena em memória pois a descrição da matriz é composta por vários campos
            return matrizes.OrderBy(o => o.Descricao).ToList();
        }

        /// <summary>
        /// Monta a descrição da oferta de matriz curricular conforme a regra RN_CUR_016 Exibição de descrição de oferta de matriz curricular
        /// </summary>
        /// <param name="descricao">Descrição da matriz curricular (que já contêm o código)</param>
        /// <param name="localidade">Descrição da localidade</param>
        /// <param name="turno">Descrição do turno</param>
        /// <returns>Descrição no formato [Código] + " - " + [Descrição da matriz curricular]+ "-" + [localidade] + "-" + [Turno]</returns>
        public static string GerarDescricaoMatrizCurricuarOferta(string descricao, string descricaoComplementar, string localidade, string turno)
        {
            return string.IsNullOrEmpty(descricaoComplementar) ? $"{descricao} - {localidade} - {turno}" : $"{descricao} - {descricaoComplementar} - {localidade} - {turno}";
        }

        /// <summary>
        /// Valida se a matriz curricular oferta possui uma associação com a configuração de componente da turma
        /// </summary>
        /// <param name="seqMatrizCurricularOferta">Sequencial da matriz curricular oferta</param>
        /// <param name="seqConfiguracaoComponente">Sequencial da configuração de componente</param>
        /// <returns>True: Se a oferta estiver associada a configuração de componente, caso contrário False</returns>
        public bool ValidarMatrizCurricularOfertaConfiguracaoComponente(long seqMatrizCurricularOferta, long seqConfiguracaoComponente)
        {
            FilterHelper.DesativarFiltros(this);
            var matrizCurricularOferta = this.SearchProjectionByKey(new SMCSeqSpecification<MatrizCurricularOferta>(seqMatrizCurricularOferta), p => p.MatrizCurricular.ConfiguracoesComponente).Any(c => c.SeqConfiguracaoComponente == seqConfiguracaoComponente);
            FilterHelper.AtivarFiltros(this);
            return matrizCurricularOferta;
        }

        /// <summary>
        /// Busca as configurações de componentes da matriz curricular oferta planificado para agrupar por número da divisão e grupo curricular
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular oferta</param>
        /// <param name="seqsBeneficios">Sequenciais dos benefícios da pessoa atuação</param>
        /// <param name="seqsCondicoes">Sequenciais de condições de obrigatoriedade da pessoa atuação</param>
        /// <param name="seqsFormacoes">Sequenciais de formações específica da pessoa atuação</param>
        /// <returns>Lista de configurações com divisão e grupo curricular</returns>
        public List<IntegralizacaoMatrizCurricularOfertaVO> BuscarIntegralizacaoConfiguracoesMatrizCurricularOferta(MatrizCurricularOfertaIntegralizacaoFiltroVO filtro)
        {
            string query = string.Format(_buscarIntegralizacaoConfiguracoesMatrizCurricularOferta, filtro.Seq);

            if (!filtro.DesconsiderarFiltroBeneficios)
            {
                if (filtro.SeqsBeneficios != null && filtro.SeqsBeneficios.Count > 0)
                    query += string.Format(_validaBeneficioIntegralizacaoConfiguracoesMatrizCurricularOferta, string.Join(" , ", filtro.SeqsBeneficios));
                else
                    query += string.Format(_validaBeneficioIntegralizacaoConfiguracoesMatrizCurricularOferta, "0");
            }

            if (!filtro.DesconsiderarFiltroCondicoes)
            {
                if (filtro.SeqsCondicoes != null && filtro.SeqsCondicoes.Count > 0)
                    query += string.Format(_validaCondicaoIntegralizacaoConfiguracoesMatrizCurricularOferta, string.Join(" , ", filtro.SeqsCondicoes));
                else
                    query += string.Format(_validaCondicaoIntegralizacaoConfiguracoesMatrizCurricularOferta, "0");
            }

            // Neste caso quando a pessoa atuação não tem formação retorna todos os valores
            if (filtro.SeqsFormacoes != null && filtro.SeqsFormacoes.Count > 0)
                query += string.Format(_validaFormacaoIntegralizacaoConfiguracoesMatrizCurricularOferta, string.Join(" , ", filtro.SeqsFormacoes));

            // Filtros de integralização informados pelo aluno
            if (filtro.SeqsComponentesFiltros != null && filtro.SeqsComponentesFiltros.Count > 0)
                query += string.Format(_filtroIntegralizacaoComponentesMatrizCurricularOferta, string.Join(" , ", filtro.SeqsComponentesFiltros));

            if (filtro.SeqsComponentesDiferentesFiltros != null && filtro.SeqsComponentesDiferentesFiltros.Count > 0)
                query += string.Format(_filtroIntegralizacaoComponentesDiferentesMatrizCurricularOferta, string.Join(" , ", filtro.SeqsComponentesDiferentesFiltros));

            if (!string.IsNullOrEmpty(filtro.DescricaoComponentesFiltro))
                query += string.Format(_filtroIntegralizacaoComponentesDescricaoMatrizCurricularOferta, filtro.DescricaoComponentesFiltro);

            return RawQuery<IntegralizacaoMatrizCurricularOfertaVO>(query);
        }

        /// <summary>
        /// Método que valida se alguma das matrizes de ofertas possuem vínculo com a hierarquia de entidade, do usuário.
        /// </summary>
        /// <param name="seqs">Lista de seqs de MatrizCurricularOferta</param>
        /// <returns>True = Alguma das ofertas pode ser acessada pelo usuário | False = Nenhuma oferta pode ser acessada pelo usuário</returns>
        public bool ValidarMatrizCurricularOfertas(List<long> seqs)
        {
            var spec = new MatrizCurricularOfertaFilterSpecification() { Seqs = seqs };
            return Count(spec) > 0;
        }

        #region [ LK_CUR_005_Oferta_de_Matriz_Curricular ]

        /// <summary>
        /// Busca as matrizes curricular que atendam os filtros informados e com a paginação correta
        /// </summary>
        /// <param name="filtros">Sequencial do curriculo curso oferta</param>
        /// <returns>SMCPagerData com a lista de matrizes curricular</returns>
        public SMCPagerData<MatrizCurricularOfertaVO> BuscarMatrizesCurricularLookupOferta(MatrizCurricularOfertaFiltroVO filtros)
        {
            var spec = filtros.Transform<MatrizCurricularOfertaFilterSpecification>();
            var pageSettings = spec.GetPageSetting();
            pageSettings.PageSize = int.MaxValue;
            spec.SetPageSetting(pageSettings);

            if (filtros.SeqDispensa.HasValue && filtros.SeqDispensa > 0)
            {
                var dispensa = DispensaDomainService.BuscarDispensa(filtros.SeqDispensa.Value);

                //Filtro de matriz curriculares que possui seu grupo de componentes, os componentes listados como origem
                spec.GrupoOrigemSeqComponentesCurriculares = dispensa.GrupoOrigens.Select(s => s.Seq).ToList();

                //Filtro de matriz curriculares que NÃO possui no seu grupo de componentes, os componentes listados como dispensado
                spec.GrupoDispensadoSeqComponentesCurriculares = dispensa.GrupoDispensados.Select(s => s.Seq).ToList();
            }

            if (filtros.IgnorarFiltroDados) { FilterHelper.DesativarFiltros(this); }

            var matrizOfertas = BuscarMatrizesCurricularOfertasHistoricosSituacaoAtual(spec);

            if (filtros.IgnorarFiltroDados) { FilterHelper.AtivarFiltros(this); }

            /// Se houver Ciclo letivo é feito o filtro da Situação, conforme o período do ciclo letivo,
            /// caso contrário, permanece o filtro existente, com a data atua.
            matrizOfertas = AtualizarSituacaoHistoricoOfertasMatrizCicloLetivo(matrizOfertas, filtros.SeqCicloLetivo);

            var total = matrizOfertas.Count();
            if (filtros.PageSettings != null)
            {
                matrizOfertas = matrizOfertas?
                                    .Skip((filtros.PageSettings.PageIndex - 1) * filtros.PageSettings.PageSize)
                                    .Take(filtros.PageSettings.PageSize).ToList();
            }

            return new SMCPagerData<MatrizCurricularOfertaVO>(matrizOfertas, total);
        }

        #region [ Métodos Privados - Buscar Matrizes Curricular Lookup Oferta ]

        /// <summary>
        /// Busca as matrizes e suas ofertas, com a situação atual e todo seu histórico de situações
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        private List<MatrizCurricularOfertaVO> BuscarMatrizesCurricularOfertasHistoricosSituacaoAtual(MatrizCurricularOfertaFilterSpecification spec)
        {
            DateTime dataAtual = DateTime.Now.Date;

            return SearchProjectionBySpecification(spec,
                s => new MatrizCurricularOfertaVO()
                {
                    Seq = s.Seq,
                    DescricaoMatrizCurricular = s.MatrizCurricular.Descricao,
                    SeqMatrizCurricular = s.SeqMatrizCurricular,
                    SeqEntidadeLocalidade = s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Seq,
                    SeqCurriculoCursoOferta = s.MatrizCurricular.SeqCurriculoCursoOferta,
                    Codigo = s.Codigo,
                    DescricaoUnidade = s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                    DescricaoLocalidade = s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                    DescricaoTurno = s.CursoOfertaLocalidadeTurno.Turno.Descricao,
                    DescricaoComplementarMatrizCurricular = s.MatrizCurricular.DescricaoComplementar,
                    NumeroPeriodoAtivo = s.NumeroPeriodoAtivo,
                    SeqCursoOfertaLocalidadeTurno = s.SeqCursoOfertaLocalidadeTurno,

                    DataInicioVigencia = s.HistoricosSituacao.Count(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)) > 0 ?
                                                s.HistoricosSituacao.Where(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)).FirstOrDefault().DataInicio :
                                                s.HistoricosSituacao.Where(w => w.DataFim == null).FirstOrDefault().DataInicio,

                    DataFinalVigencia = s.HistoricosSituacao.Count(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)) > 0 ?
                                                s.HistoricosSituacao.Where(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)).FirstOrDefault().DataFim :
                                                s.HistoricosSituacao.Where(w => w.DataFim == null).FirstOrDefault().DataFim,

                    HistoricoSituacaoAtual = s.HistoricosSituacao.Count(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)) > 0 ?
                                                s.HistoricosSituacao.Where(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)).FirstOrDefault().SituacaoMatrizCurricularOferta :
                                                s.HistoricosSituacao.Where(w => w.DataFim == null).FirstOrDefault().SituacaoMatrizCurricularOferta,

                    HistoricosSituacao = s.HistoricosSituacao.Select(h => new HistoricoSituacaoMatrizCurricularOfertaVO()
                    {
                        Seq = h.Seq,
                        SeqMatrizCurricularOferta = h.SeqMatrizCurricularOferta,
                        DataInicio = h.DataInicio,
                        DataFim = h.DataFim,
                        SituacaoMatrizCurricularOferta = h.SituacaoMatrizCurricularOferta
                    }).ToList(),
                }).ToList();
        }

        /// <summary>
        /// O filtro deverá listar somente as Ofertas de Matriz Curricular, cuja Matriz possui as situações
        /// "Em extinção" e "Ativa" com vigência coincidente com o período letivo do ciclo letivo* passado
        /// como parâmetro, de acordo com cada oferta de matriz:
        /// </summary>
        /// <param name="matrizes"></param>
        /// <param name="seqCicloLetivo"></param>
        private List<MatrizCurricularOfertaVO> AtualizarSituacaoHistoricoOfertasMatrizCicloLetivo(List<MatrizCurricularOfertaVO> matrizOfertas, long? seqCicloLetivo)
        {
            if (seqCicloLetivo.HasValue && matrizOfertas.SMCAny())
            {
                foreach (var oferta in matrizOfertas)
                {
                    AtualizarSituacaoHistoricoOfertaCicloLetivo(oferta, seqCicloLetivo.Value);
                }
                // somentes as Ofertas Ativas e em extinção
                matrizOfertas = matrizOfertas.Where(o => o.HistoricoSituacaoAtual != null
                                                            && (o.HistoricoSituacaoAtual == SituacaoMatrizCurricularOferta.Ativa
                                                             || o.HistoricoSituacaoAtual == SituacaoMatrizCurricularOferta.EmExtincao)).ToList();
            }
            return matrizOfertas;
        }

        /// <summary>
        /// A data início da situação da oferta de matriz deve ser menor ou igual a data início do ciclo letivo
        /// e a data fim da situação da oferta deve ser nula, ou ser maior ou igual a data fim do ciclo letivo.
        /// </summary>
        /// <param name="oferta"></param>
        /// <param name="seqCicloLetivo"></param>
        private void AtualizarSituacaoHistoricoOfertaCicloLetivo(MatrizCurricularOfertaVO oferta, long seqCicloLetivo)
        {
            try
            {
                var periodoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(seqCicloLetivo,
                                                                       oferta.SeqCursoOfertaLocalidadeTurno,
                                                                       null,
                                                                       TOKEN_TIPO_EVENTO.PERIODO_LETIVO);
                if (periodoLetivo == null) { return; }

                var situacaoHistorico = oferta.HistoricosSituacao.FirstOrDefault(x => x.DataInicio <= periodoLetivo.DataInicio
                                                                             && (x.DataFim == null || x.DataFim >= periodoLetivo.DataFim));
                if (situacaoHistorico == null)
                {
                    var situacaoAtiva = oferta.HistoricosSituacao.FirstOrDefault(x => x.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.Ativa);
                    var situacaoEmExtincao = oferta.HistoricosSituacao.FirstOrDefault(x => x.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.EmExtincao);

                    if (situacaoAtiva != null && situacaoEmExtincao != null)
                    {
                        if (situacaoAtiva.DataInicio <= periodoLetivo.DataInicio && (situacaoEmExtincao.DataFim == null || situacaoEmExtincao.DataFim >= periodoLetivo.DataFim))
                        {
                            situacaoHistorico = situacaoEmExtincao;
                        }
                    }
                }

                oferta.DataInicioVigencia = situacaoHistorico?.DataInicio;
                oferta.DataFinalVigencia = situacaoHistorico?.DataFim;
                oferta.HistoricoSituacaoAtual = situacaoHistorico?.SituacaoMatrizCurricularOferta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion [ Métodos Privados - Buscar Matrizes Curricular Lookup Oferta ]

        /// <summary>
        /// Busca a matrize curricular com a oferta selecionada para retorno do lookup
        /// </summary>
        /// <param name="SeqMatrizCurricularOferta">Sequencial da matriz curricular oferta</param>
        /// <returns>Objeto matriz curricular oferta</returns>
        public MatrizCurricularOfertaVO BuscarMatrizesCurricularLookupOfertaSelecionado(long seqMatrizCurricularOferta)
        {
            DateTime dataAtual = DateTime.Now.Date;
            var filtros = new MatrizCurricularOfertaFilterSpecification() { Seq = seqMatrizCurricularOferta };

            FilterHelper.DesativarFiltros(this);

            var retorno = this.SearchProjectionByKey(filtros,
                                s => new MatrizCurricularOfertaVO()
                                {
                                    Seq = s.Seq,
                                    SeqCurriculoCursoOferta = s.MatrizCurricular.SeqCurriculoCursoOferta,
                                    SeqMatrizCurricular = s.Seq,
                                    Codigo = s.Codigo,
                                    Descricao = s.MatrizCurricular.Descricao,
                                    DescricaoUnidade = s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                                    DescricaoLocalidade = s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                                    SeqEntidadeLocalidade = s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Seq,
                                    DescricaoTurno = s.CursoOfertaLocalidadeTurno.Turno.Descricao,
                                    DescricaoMatrizCurricular = s.MatrizCurricular.Descricao,
                                    DescricaoComplementarMatrizCurricular = s.MatrizCurricular.DescricaoComplementar,
                                    NumeroPeriodoAtivo = s.NumeroPeriodoAtivo,

                                    DataInicioVigencia = s.HistoricosSituacao.Count(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)) > 0 ?
                                                                s.HistoricosSituacao.Where(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)).FirstOrDefault().DataInicio :
                                                                s.HistoricosSituacao.Where(w => w.DataFim == null).FirstOrDefault().DataInicio,

                                    DataFinalVigencia = s.HistoricosSituacao.Count(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)) > 0 ?
                                                            s.HistoricosSituacao.Where(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)).FirstOrDefault().DataFim :
                                                            s.HistoricosSituacao.Where(w => w.DataFim == null).FirstOrDefault().DataFim,

                                    HistoricoSituacaoAtual = s.HistoricosSituacao.Count(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)) > 0 ?
                                                                    s.HistoricosSituacao.Where(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)).FirstOrDefault().SituacaoMatrizCurricularOferta :
                                                                    s.HistoricosSituacao.Where(w => w.DataFim == null).FirstOrDefault().SituacaoMatrizCurricularOferta,
                                    ExcecoesLocalidade = s.ExcecoesLocalidade.Select(e => new MatrizCurricularOfertaExcecaoLocalidadeVO()
                                    {
                                        Seq = e.Seq,
                                        SeqEntidadeLocalidade = e.SeqEntidadeLocalidade,
                                        SeqMatrizCurricularOferta = e.SeqMatrizCurricularOferta,
                                        DescricaoLocalidade = e.EntidadeLocalidade.Nome
                                    }).ToList(),
                                });

            FilterHelper.AtivarFiltros(this);

            return retorno;
        }

        #endregion [ LK_CUR_005_Oferta_de_Matriz_Curricular ]
    }
}