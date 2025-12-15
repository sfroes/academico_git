using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class TipoMensagemFiltroVO : ISMCMappable
    {
        public string Descricao { get; set; }

        public CategoriaMensagem CategoriaMensagem { get; set; }

        public bool? PermiteCadastroManual { get; set; }
    }
}

 