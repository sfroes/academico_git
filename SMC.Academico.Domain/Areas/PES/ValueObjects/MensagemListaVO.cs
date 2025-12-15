using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class MensagemListaVO : ISMCMappable
    {
        public long Seq { get; set; }
        [SMCMapProperty("Mensagem.TipoMensagem.Descricao")]
        public string DescricaoTipoMensagem { get; set; }
        [SMCMapProperty("Mensagem.Descricao")]
        public string DescricaoMensagem { get; set; }
        [SMCMapProperty("Mensagem.DataInclusao")]
        public DateTime DataInclusao { get; set; }
        [SMCMapProperty("Mensagem.UsuarioInclusao")]
        public string UsuarioInclusao { get; set; }
        [SMCMapProperty("Mensagem.TipoMensagem.PermiteCadastroManual")]
        public bool CadastroManual { get; set; }
        [SMCMapProperty("Mensagem.TipoMensagem.CategoriaMensagem")]
        public CategoriaMensagem CategoriaMensagem { get; set; }
        [SMCMapProperty("Mensagem.TipoMensagem.Token")]
        public string TokenTipoMensagem { get; set; }
        public string MensagemExcluir { get; set; }
        public long SeqPessoaAtuacao { get; set; }
        [SMCMapProperty("Mensagem.DataInicioVigencia")]
        public DateTime DataInicioVigencia { get; set; }
        [SMCMapProperty("Mensagem.DataFimVigencia")]
        public DateTime? DataFimVigencia { get; set; }
        public string PeriodoVigencia { get; set; }
        public string DataUsuarioInclusao { get; set; }
        public long SeqMensagem { get; set; }
        [SMCMapProperty("Mensagem.SeqTipoMensagem")]
        public long SeqTipoMensagem { get; set; }

    }
}