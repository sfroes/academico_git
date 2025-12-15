using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class InstituicaoNivelBeneficioListarDynamicModel : SMCDynamicViewModel
    {

        #region [ DataSource ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoDaInstituicaoSelect))]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCDataSource(dataSource: "Beneficio")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IFINDynamicService))]
        public List<SMCDatasourceItem> Beneficios { get; set; }

        #endregion [ DataSource ]

        [SMCKey]
        public override long Seq { get; set; }

        public long SeqInstituicaoNivel { get; set; }

        public long SeqBeneficio { get; set; }

        [SMCInclude("ConfiguracoesBeneficio")]
        [SMCIgnoreProp]
        public List<InstituicaoNivelBeneficioListarConfiguracaBeneficioViewModel> ConfiguracoesBeneficio { get; set; }

        [SMCInclude("BeneficiosHistoricosValoresAuxilio")]
        [SMCIgnoreProp]
        public List<InstituicaoNivelBeneficioListarBeneficioHistoricoValorAuxilioViewModel> BeneficiosHistoricosValoresAuxilio { get; set; }

        #region Campos para mensagem

        [SMCInclude("Beneficio")]
        [SMCMapProperty("Beneficio.Descricao")]
        [SMCIgnoreProp]
        public string DescricaoBeneficio { get; set; }

        [SMCInclude("InstituicaoNivel.NivelEnsino")]
        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCIgnoreProp]
        public string DescricaoInstituicaoNivel { get; set; }

        #endregion Campos para mensagem
    }
}