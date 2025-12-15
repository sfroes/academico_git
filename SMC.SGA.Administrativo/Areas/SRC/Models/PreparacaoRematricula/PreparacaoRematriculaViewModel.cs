using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class PreparacaoRematriculaViewModel : SMCViewModelBase
    {
        [SMCMapProperty("Seq")]
        public long SeqProcesso { get; set; }
        
        public string Descricao { get; set; }

        public string DescricaoTipoServico { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataInicio { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataFim { get; set; }


        public long? SeqAgendamento { get; set; }

        public long? SeqUltimoHistoricoAgendamento { get; set; }
    }
}