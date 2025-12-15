
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class InstituicaoNivelTipoVinculoAlunoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region DataSource 

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoDaInstituicaoSelect))]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoVinculoAlunoService), nameof(ITipoVinculoAlunoService.BuscarTiposVinculoAlunoSelect))]
        public List<SMCDatasourceItem> TiposVinculoAluno { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoTermoIntercambioService), nameof(ITipoTermoIntercambioService.BuscarTiposTermosIntercambiosSelect))]
        public List<SMCDatasourceItem> TiposTermosIntercambios { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoOrientacaoService), nameof(ITipoOrientacaoService.BuscarTipoOrientacaoNaoOrientacaoTurmaSelect))]
        public List<SMCDatasourceItem> TiposOrientacao { get; set; }

        #endregion        

        [SMCFilter]
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        [SMCSelect("NiveisEnsino")]
        public long? SeqInstituicaoNivel { get; set; }

        [SMCFilter]
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCSelect("TiposVinculoAluno")]
        public long? SeqTipoVinculoAluno { get; set; }

        [SMCFilter]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCSelect("TiposTermosIntercambios")]
        public long? SeqTipoTermoIntercambio { get; set; }

        [SMCFilter]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid7_24)]
        [SMCSelect("TiposOrientacao")]
        public long? SeqTipoOrientacao { get; set; } 
    }
}