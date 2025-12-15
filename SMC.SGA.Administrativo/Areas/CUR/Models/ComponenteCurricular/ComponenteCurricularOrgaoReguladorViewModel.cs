using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ComponenteCurricularOrgaoReguladorViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCDependency("RegistroTipoOrgaoRegulador", "TipoOrgaoReguladorInstituicaoNivel", "ComponenteCurricular", true)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid6_24)]
        public string TipoOrgaoReguladorDescricao { get; set; }

        [SMCDescription]
        [SMCMaxLength(20)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCUnique]
        public string Codigo { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime DataInicio { get; set; }

        [SMCMinDate(nameof(DataInicio))]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime? DataFim { get; set; }
    }
}