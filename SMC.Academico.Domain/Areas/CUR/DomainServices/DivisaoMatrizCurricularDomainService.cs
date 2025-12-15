using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class DivisaoMatrizCurricularDomainService : AcademicoContextDomain<DivisaoMatrizCurricular>
    {
        #region [ DomainServices ]

        private DivisaoCurricularItemDomainService DivisaoCurricularItemDomainService
        {
            get { return this.Create<DivisaoCurricularItemDomainService>(); }
        }

        private MatrizCurricularDomainService MatrizCurricularDomainService
        {
            get { return this.Create<MatrizCurricularDomainService>(); }
        }

        #endregion [ DomainServices ]

        #region [ Queries ]

        #region [ _buscarIntegralizacaoDivisaoMatrizCurricularGrupo ]

        private string _buscarIntegralizacaoDivisaoMatrizCurricularGrupo =
                        @"  SELECT  DMC.seq_divisao_matriz_curricular AS SeqDivisaoMatrizCurricular,
                                    DIMAT.dsc_divisao_curricular_item AS DescricaoDivisao, 
	                                DMCG.qtd_hora_aula AS HoraAulaGrupo,
	                                DMCG.qtd_hora_relogio AS HoraGrupo,
	                                DMCG.qtd_creditos AS CreditoGrupo,
	                                DMCG.qtd_itens AS ItensGrupo,
                                    GC.seq_grupo_curricular AS SeqGrupoCurricular,
                                    GC.idt_dom_formato_configuracao_grupo AS FormatoConfiguracaoGrupo,
	                                TCGC.dsc_tipo_configuracao_grupo_curricular AS DescricaoTipoConfiguracaoGrupo
                            FROM [ACADEMICO].[CUR].[divisao_matriz_curricular] DMC 
                            INNER JOIN [ACADEMICO].[CUR].[divisao_curricular_item] DIMAT ON DMC.seq_divisao_curricular_item = DIMAT.seq_divisao_curricular_item 
                            INNER JOIN [ACADEMICO].[CUR].[divisao_matriz_curricular_grupo] DMCG ON DMC.seq_divisao_matriz_curricular = DMCG.seq_divisao_matriz_curricular 
                            INNER JOIN [ACADEMICO].[CUR].[curriculo_curso_oferta_grupo] CCOG ON DMCG.seq_curriculo_curso_oferta_grupo = CCOG.seq_curriculo_curso_oferta_grupo
                            INNER JOIN [ACADEMICO].[CUR].[grupo_curricular] GC ON CCOG.seq_grupo_curricular = GC.seq_grupo_curricular
                            INNER JOIN [ACADEMICO].[CUR].[tipo_configuracao_grupo_curricular] TCGC ON GC.seq_tipo_configuracao_grupo_curricular = TCGC.seq_tipo_configuracao_grupo_curricular
                            WHERE DMC.seq_divisao_matriz_curricular = {0} AND GC.seq_grupo_curricular IN ({1});";

        #endregion [ _buscarIntegralizacaoDivisaoMatrizCurricularGrupo ]

        #endregion [ Queries ]

        /// <summary>
        /// Busca as divisões de matrizes curriculares com as descrições
        /// </summary>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
        /// <returns>Dados das divisões de matrizes curriculares</returns>
        public List<SMCDatasourceItem> BuscarDivisoesMatrizCurricularDescricaoSelect(long seqMatrizCurricular)
        {
            var spec = new DivisaoMatrizCurricularFilterSpecification() { SeqMatrizCurricular = seqMatrizCurricular };

            var retorno = this.SearchProjectionBySpecification(spec,
                p => new SMCDatasourceItem()
                {
                    Seq = p.Seq,
                    Descricao = p.DivisaoCurricularItem.Descricao
                }).ToList();

            return retorno;
        }

        /// <summary>
        /// Busca as divisões de matrizes curriculares com os tipos na descrição
        /// </summary>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
        /// <returns>Dados das divisões de matrizes curriculares</returns>
        public List<SMCDatasourceItem> BuscarDivisoesMatrizCurricularTipoSelect(long seqMatrizCurricular)
        {
            var spec = new DivisaoMatrizCurricularFilterSpecification() { SeqMatrizCurricular = seqMatrizCurricular };
            spec.SetOrderBy(o => o.DivisaoCurricularItem.Numero);

            var retorno = this.SearchProjectionBySpecification(spec,
                p => new SMCDatasourceItem()
                {
                    Seq = p.DivisaoCurricularItem.Seq,
                    Descricao = p.DivisaoCurricularItem.Descricao
                }).ToList();

            return retorno;
        }

        /// <summary>
        /// Busca as divisões de matrizes curriculares com os tipos na descrição de acordo com o tipo de requisito (Pré ou Có)
        /// </summary>
        /// <param name="tipoRequisito">Tipo do requisito selecionado</param>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular selecionada</param>
        /// <param name="seqDivisaoMatrizCurricular">Sequencial da divisão matriz curricular selecionada</param>
        /// <returns>Dados das divisões de matrizes curriculares</returns>
        public List<SMCDatasourceItem> BuscarDivisoesMatrizCurricularTipoPorTipoRequisitoSelect(TipoRequisito tipoRequisito, long seqMatrizCurricular, long? seqDivisaoMatrizCurricular)
        {
            if (!seqDivisaoMatrizCurricular.HasValue)
                return new List<SMCDatasourceItem>();

            var numeroDivisao = this.DivisaoCurricularItemDomainService.SearchProjectionByKey(new SMCSeqSpecification<DivisaoCurricularItem>(seqDivisaoMatrizCurricular.Value), p => p.Numero);

            var spec = new DivisaoMatrizCurricularFilterSpecification();

            if (tipoRequisito == TipoRequisito.CoRequisito)
                spec.NumeroCoRequisito = numeroDivisao;

            if (tipoRequisito == TipoRequisito.PreRequisito)
                spec.NumeroPreRequisito = numeroDivisao;

            spec.SeqMatrizCurricular = seqMatrizCurricular;
            spec.SetOrderBy(o => o.DivisaoCurricularItem.Numero);

            var retorno = this.SearchProjectionBySpecification(spec,
                p => new SMCDatasourceItem()
                {
                    Seq = p.DivisaoCurricularItem.Seq,
                    Descricao = p.DivisaoCurricularItem.Numero + " - " + p.DivisaoCurricularItem.DivisaoCurricular.TipoDivisaoCurricular.Descricao
                }).ToList();

            return retorno;
        }

        /// <summary>
        /// Busca as divisões da matriz curricular com as configurações de grupo por divisão
        /// </summary>
        /// <param name="seq">Sequencial da divisão matriz curricular</param>
        /// <param name="seqGrupos">Lista de sequenciais de grupos da divisão</param>
        /// <returns>Lista de configurações com divisão e grupo curricular</returns>
        public List<IntegralizacaoMatrizGrupoVO> BuscarIntegralizacaoDivisaoMatrizCurricularGrupo(long seq, List<long> seqGrupos)
        {
            return RawQuery<IntegralizacaoMatrizGrupoVO>(string.Format(_buscarIntegralizacaoDivisaoMatrizCurricularGrupo, seq, string.Join(" , ", seqGrupos)));
        }
    }
}