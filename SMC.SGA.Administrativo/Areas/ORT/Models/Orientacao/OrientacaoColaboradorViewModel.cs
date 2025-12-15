using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class OrientacaoColaboradorViewModel : SMCViewModelBase
    {
        public string Nome { get; set; }

        public TipoParticipacaoOrientacao TipoParticipacaoOrientacao { get; set; }

        public string DataFormatada { get; set; }

        public string DadosColaboradorCompleto { get; set; }
    }
}