using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class MensagemListaData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; } 
         
        [SMCMapProperty("TipoMensagem.Descricao")]        
        public string DescricaoTipoMensagem { get; set; }
         
        [SMCMapProperty("DescricaoMensagem")]
        [SMCMapProperty("Content")]
        public string Mensagem { get; set; }

        [SMCMapProperty("EventDate")]
        public DateTime DataInicioVigencia { get; set; }

        [SMCMapProperty("ItemType")]
        public string ClasseCss { get; set; }

        public bool CadastroManual { get; set; }

        public string UsuarioInclusao { get; set; }

        public CategoriaMensagem CategoriaMensagem { get; set; }

        public string MensagemExcluir { get; set; }

        public string PeriodoVigencia { get; set; }

        public string DataUsuarioInclusao { get; set; }
    }
}
