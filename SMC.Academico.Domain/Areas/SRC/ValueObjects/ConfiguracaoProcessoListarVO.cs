using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ConfiguracaoProcessoListarVO : ISMCMappable
    {
        public long SeqProcesso { get; set; }

        public bool SolicitacaoAssociada { get; set; }

        public bool ProcessoEncerrado { get; set; }

        public long Seq { get; set; }

        public string Descricao { get; set; }
    }
}
