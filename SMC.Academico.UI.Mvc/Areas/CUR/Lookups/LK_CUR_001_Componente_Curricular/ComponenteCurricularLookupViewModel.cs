using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class ComponenteCurricularLookupViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCHidden]
        [SMCKey]
        public long? Seq { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public string DescricaoNivelEnsino { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public List<ComponenteCurricularEntidadeLookupViewModel> EntidadesResponsaveis { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public string DescricaoTipoComponenteCurricular { get; set; }

        [SMCCssClass("smc-size-md-1 smc-size-xs-1 smc-size-sm-1 smc-size-lg-1")]
        public string Codigo { get; set; }

        [SMCCssClass("smc-size-md-5 smc-size-xs-5 smc-size-sm-5 smc-size-lg-5")]
        public string Descricao { get; set; }

        [SMCCssClass("smc-size-md-1 smc-size-xs-1 smc-size-sm-1 smc-size-lg-1")]
        public bool Ativo { get; set; }

        [SMCCssClass("smc-size-md-1 smc-size-xs-1 smc-size-sm-1 smc-size-lg-1")]
        public short? CargaHoraria { get; set; }

        [SMCCssClass("smc-size-md-1 smc-size-xs-1 smc-size-sm-1 smc-size-lg-1")]
        public short? Credito { get; set; }

        [SMCDescription]
        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        public string DescricaoCompleta
        {
            get
            {
                //Para funcionamento do lookup recuperar o seq do banco e fazer a pesquisa 
                if (string.IsNullOrEmpty(Codigo) || string.IsNullOrEmpty(Descricao))
                    return null;

                return $"{Codigo} - {Descricao}";
            }
        }

        [SMCHidden]
        [SMCMapProperty("FormatoCargaHoraria")]
        public FormatoCargaHoraria? Formato { get; set; }
    }
}