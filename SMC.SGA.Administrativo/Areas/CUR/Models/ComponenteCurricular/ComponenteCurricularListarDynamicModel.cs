using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.App_GlobalResources;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ComponenteCurricularListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        public string Codigo { get; set; }

        [SMCDescription]
        [SMCHidden]
        public string Descricao { get; set; }

        [SMCHidden]
        public short? CargaHoraria { get; set; }

        [SMCHidden]
        public short? Credito { get; set; }

        [SMCHidden]
        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid2_24)]
        public string DescricaoTipoComponenteCurricular { get; set; }

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid22_24)]
        public string DescricaoCompleta { get; set; }

        [SMCHidden]
        public TipoOrganizacao? TipoOrganizacao { get; set; }

        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid12_24)]
        public string DescricaoTipoOrganizacao
        {
            get
            {
                return TipoOrganizacao.GetValueOrDefault(Academico.Common.Areas.CUR.Enums.TipoOrganizacao.Nenhum)
                            != Academico.Common.Areas.CUR.Enums.TipoOrganizacao.Nenhum ?
                            TipoOrganizacao.SMCGetDescription() : UIResource.Label_Nenhum_Registro;
            }
        }

        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<ComponenteCurricularNivelEnsinoViewModel> NiveisEnsino { get; set; }

        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<ComponenteCurricularEntidadeResponsavelViewModel> EntidadesResponsaveis { get; set; }

        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<ComponenteCurricularLegadoViewModel> ComponentesLegado { get; set; }

        [SMCHidden]
        public bool PermiteConfiguracaoComponente { get; set; }

        [SMCHidden]
        public ConfiguracaoComponenteCurricular ConfiguracaoComponenteCurricular { get; set; }
    }
}