using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class RelatorioConsolidadoSituacaoFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        #region [ DataSources ]

        /// Seleção múltipla
        /// NV02 Listar somente as entidades do tipo de entidade "Grupo de Programa" e de acordo com as regras: RN_USG_001 -
        /// Filtro por Instituição de Ensino e RN_USG_005 - Filtro por Entidade Responsável, em ordem alfabética.
        /// </summary>
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        #endregion [ DataSources ]

        /// <summary>
        /// LK_CAM_002 - Ciclo Letivo
        /// </summary>
        [SMCRequired]
        [CicloLetivoLookup]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        /// <summary>
        /// Seleção múltipla
        /// NV01 Listar somente as entidades do tipo de entidade "Grupo de Programa" e de acordo com as regras: RN_USG_001 -
        /// Filtro por Instituição de Ensino e RN_USG_005 - Filtro por Entidade Responsável, em ordem alfabética.
        /// </summary>
        [SMCMapProperty("SeqEntidadesResponsaveis")]
        [SMCSelect(nameof(EntidadesResponsaveis))]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid12_24)]
        public List<long> SeqsEntidadeResponsavel { get; set; }


        /// <summary>
        /// Tipo de atuação 
        /// Seleção múltipla
        /// Valores: "Aluno" e "Ingressante".
        /// Por default todos os valores virão selecionados.
        /// </summary>
        [SMCRequired]
        [SMCCheckBoxList(IgnoredEnumItems = new object[] { TipoAtuacao.Colaborador })]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid6_24)]
        public List<TipoAtuacao> TipoAtuacoes { get; set; } = new List<TipoAtuacao>() { TipoAtuacao.Aluno, TipoAtuacao.Ingressante };

    }
}