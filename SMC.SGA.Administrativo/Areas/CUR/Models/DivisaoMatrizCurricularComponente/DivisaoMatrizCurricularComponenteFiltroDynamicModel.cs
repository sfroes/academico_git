using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class DivisaoMatrizCurricularComponenteFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IDivisaoMatrizCurricularService), nameof(IDivisaoMatrizCurricularService.BuscarDivisoesMatrizCurricularDescricaoSelect),
            values: new string[] { nameof(SeqMatrizCurricular) })]
        public List<SMCDatasourceItem> DivisoesMatrizCurricular { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IComponenteCurricularService), nameof(IComponenteCurricularService.BuscarComponenteCurricularPorMatrizSelect),
           values: new string[] { nameof(SeqMatrizCurricular) })]
        public List<SMCDatasourceItem> ComponentesCurriculares { get; set; }

        #endregion [ DataSources ]

        [SMCSize(SMCSize.Grid6_24)]
        [SMCSelect(nameof(DivisoesMatrizCurricular))]
        [SMCFilter(true, true)]
        public long? SeqDivisaoMatrizCurricular { get; set; }

        [SMCSize(SMCSize.Grid14_24)]
        [SMCSelect(nameof(ComponentesCurriculares))]
        [SMCFilter(true, true)]
        public long? SeqComponenteCurricular { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqCurriculoCursoOferta { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqMatrizCurricular { get; set; }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new MatrizCurricularNavigationGroup(this);
        }
    }
}