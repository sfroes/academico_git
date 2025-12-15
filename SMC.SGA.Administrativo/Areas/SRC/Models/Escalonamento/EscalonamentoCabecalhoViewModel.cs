using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class EscalonamentoCabecalhoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqProcesso { get; set; }

        [SMCHidden]
        public long SeqEscalonamento { get; set; }

        public string DescricaoProcesso { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime DataInicio { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataFim { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCValueEmpty("-")]
        public DateTime? DataEncerramento { get; set; }

        public string DescricaoEtapa { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }

        public bool ExigeEscalonamento { get; set; }

        public bool HabilitarGrupoEscalonamento { get; set; }
    }
}