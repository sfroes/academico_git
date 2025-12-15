using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ConfiguracaoComponeteMatrizFiltroViewModel : SMCPagerViewModel
    {
        #region [ DataSources ]

        public List<SMCSelectListItem> DivisoesMatrizCurricular { get; set; }
        public List<SMCSelectListItem> ComponentesCurriculares { get; set; }
        public List<SMCSelectListItem> TipoComponenteComponenteCurricular { get; set; }
        public List<SMCDatasourceItem> somente { get; set; }

        #endregion [ DataSources ]

        [SMCSize(SMCSize.Grid6_24)]
        [SMCSelect(nameof(DivisoesMatrizCurricular))]
        [SMCFilter(true, true)]
        public long? SeqDivisaoMatrizCurricular { get; set; }

        [SMCSize(SMCSize.Grid18_24)]
        [SMCFilter]
        public string DescricaoGrupoCurricular { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCSelect(nameof(ComponentesCurriculares))]
        [SMCFilter(true, true)]
        public long? SeqComponenteCurricular { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCSelect(nameof(TipoComponenteComponenteCurricular))]
        [SMCFilter(true, true)]
        public long? SeqTipoComponente { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCCheckBoxList(IgnoredEnumItems = new object[] { ComponentesConfiguracaoCadastrada.ComConfiguracao, ComponentesConfiguracaoCadastrada.AmbasConfiguracao })]
        [SMCFilter(true, true)]
        public ComponentesConfiguracaoCadastrada? SomenteComponentesSemConfiguracao { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqCurriculoCursoOferta { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqMatrizCurricular { get; set; }

        [SMCIgnoreProp]
        public long? SeqGrupoCurricularComponente { get; set; }
    }
}