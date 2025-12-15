using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class InstituicaoNivelTipoOrientacaoParticipacaoViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        [SMCOrder(1)]
        public long Seq { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(Framework.SMCSize.Grid7_24)]
        public TipoParticipacaoOrientacao TipoParticipacaoOrientacao { get; set; }

        [SMCRequired]
        [SMCRadioButtonList]
        [SMCSize(Framework.SMCSize.Grid7_24)]
        public bool ObrigatorioOrientacao { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(Framework.SMCSize.Grid7_24)]
        public OrigemColaborador OrigemColaborador { get; set; }
    }
}