using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class TipoMensagemListaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public CategoriaMensagem CategoriaMensagem { get; set; }

        public bool PermiteCadastroManual { get; set; }

        public List<TipoMensagemTipoAtuacao> TiposAtuacao { get; set; }

        public List<TipoMensagemTipoUso> TiposUso { get; set; }
    }
}