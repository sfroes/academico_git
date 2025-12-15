using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ClassificacaoPeriodicoViewModel : SMCViewModelBase, ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public short AnoInicio { get; set; }

        public short AnoFim { get; set; }

        public bool Atual { get; set; }
    }
}