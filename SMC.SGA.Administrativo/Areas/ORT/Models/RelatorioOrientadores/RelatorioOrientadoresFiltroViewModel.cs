using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "grupoSituacaoMatricula", GroupName = "Situação da matrícula", Size = SMCSize.Grid9_24)]
    public class RelatorioOrientadoresFiltroViewModel : SMCPagerFilterData, ISMCMappable
    {
        #region [ DataSources ]

        [SMCDataSource]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> TiposSituacaoMatricula { get; set; }

        #endregion [ DataSources ]

        /// <summary>
        /// Trazer somente os colaboradores que tenham a participação do tipo orientador - Isso para o Lookup de colaborador
        /// </summary>
        [SMCHidden]
        public bool Oritentador { get; set; } = false;

        [SMCHidden]
        public TipoAtividadeColaborador TipoAtividade { get; set; } = TipoAtividadeColaborador.Orientacao;

        [SMCRequired]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(EntidadesResponsaveis))]
        [SMCMapProperty("SeqsEntidadesResponsaveisHierarquiaItem")]
        public List<long?> SeqEntidadeResponsavel { get; set; }

        [ColaboradorLookup]
        [SMCDependency(nameof(TipoAtividade))]
        [SMCDependency(nameof(Oritentador))]
        [SMCFilter(true, true)]
        public ColaboradorLookupViewModel SeqColaborador { get; set; }

        [SMCGroupedProperty("grupoSituacaoMatricula")]
        [CicloLetivoLookup]
        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [SMCGroupedProperty("grupoSituacaoMatricula")]
        [SMCSelect(nameof(TiposSituacaoMatricula))]
        [SMCFilter(true, true)]
        [SMCConditionalReadonly(nameof(SeqCicloLetivo), SMCConditionalOperation.Equals, "")]
        [SMCConditionalRequired(nameof(SeqCicloLetivo), SMCConditionalOperation.GreaterThen, 0)]
        public List<long?> SeqsTiposSituacoesMatriculas { get; set; }

        [SMCRequired]
        [SMCRadioButtonList]
        public bool ExibirOrientacoesFinalizadas { get; set; } = false;
    }
}