using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class MensagemVO : ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqMensagem { get; set; }
        public long SeqPessoaAtuacao { get; set; }
        [SMCMapProperty("Mensagem.SeqTipoMensagem")]
        public long SeqTipoMensagem { get; set; }
        [SMCMapProperty("Mensagem.Descricao")]
        public string Descricao { get; set; }
        [SMCMapProperty("Mensagem.SeqArquivoAnexado")]
        public long? SeqArquivoAnexado { get; set; }
        [SMCMapProperty("Mensagem.SeqControleOrigem")]
        public long? SeqControleOrigem { get; set; }
        [SMCMapProperty("Mensagem.DataInicioVigencia")]
        public DateTime DataInicioVigencia { get; set; }
        [SMCMapProperty("Mensagem.DataFimVigencia")]
        public DateTime? DataFimVigencia { get; set; }
        [SMCMapProperty("Mensagem.ArquivoAnexado")]
        public SMCUploadFile ArquivoAnexado { get; set; }
        public bool ArquivoObrigatorio { get; set; }
        public bool AlunoFormado { get; set; }
        public bool TipoMensagemBloqueado { get; set; }
        public bool DataFimObrigatoria { get; set; }
        public bool PermitirOcorrenciaCicloLetivoAnterior { get; set; }
        public DateTime DataLimiteInicioVigencia { get; set; }
        public DateTime DataInicioVigenciaCicloAnterior { get; set; }

    }
}