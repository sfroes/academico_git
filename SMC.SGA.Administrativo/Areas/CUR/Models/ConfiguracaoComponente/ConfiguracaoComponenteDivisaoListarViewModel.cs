using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.Util;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ConfiguracaoComponenteDivisaoListarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCIgnoreProp]
        public string TipoDivisaoDescricao { get; set; }

        [SMCIgnoreProp]
        public string ModalidadeDescricao { get; set; }

        [SMCIgnoreProp]
        public short Numero { get; set; }

        [SMCIgnoreProp]
        public short CargaHoraria { get; set; }

        [SMCIgnoreProp]
        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        [SMCDescription]
        public string DescricaoFormatada { get; set; }
        
        [SMCIgnoreProp]
        public short? CargaHorariaGrade { get; set; }

        public string CargaHorariaGradeFormatada { get; set; }
           
        [SMCIgnoreProp]
        public long SeqConfiguracaoComponente { get; set; }


    }
}