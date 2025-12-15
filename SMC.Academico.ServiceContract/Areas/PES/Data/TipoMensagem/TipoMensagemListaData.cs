using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class TipoMensagemListaData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public CategoriaMensagem CategoriaMensagem { get; set; }

        public bool PermiteCadastroManual { get; set; }
         
        public List<TipoMensagemTipoAtuacaoData> TiposAtuacao { get; set; }
         
        public List<TipoMensagemTipoUsoData> TiposUso { get; set; }
    }
}
