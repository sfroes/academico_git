using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.DCT.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class ColaboradorVinculoAtividadeViewModel : SMCViewModelBase
    {
        [SMCKey]
        public long Seq { get; set; }

        public TipoAtividadeColaborador TipoAtividadeColaborador { get; set; }
    }
}