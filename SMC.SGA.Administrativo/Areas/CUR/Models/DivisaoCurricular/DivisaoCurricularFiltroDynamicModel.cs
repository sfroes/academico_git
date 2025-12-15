using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class DivisaoCurricularFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCSize(SMCSize.Grid2_24)]
        [SMCFilter(true)]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCOrder(1)]
        [SMCRequired]
        [SMCDescription]
        [SMCSize(SMCSize.Grid7_24)]
        [SMCFilter(true)]
        [SMCMaxLength(100)]
        public string Descricao { get; set; }

        #region Nivel Ensino

        [SMCIgnoreProp]
        [SMCDataSource()]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoSelect))]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        /// <summary>
        /// SeqInstituicaoNivel
        /// </summary>
        [SMCFilter(true)]
        [SMCOrder(3)]
        [SMCRequired]
        [SMCSelect("InstituicaoNiveis")]
        [SMCSize(SMCSize.Grid5_24)]
        [SMCHidden(SMCViewMode.List)]
        public long SeqNivelEnsino { get; set; }

        #endregion Nivel Ensino
    }
}