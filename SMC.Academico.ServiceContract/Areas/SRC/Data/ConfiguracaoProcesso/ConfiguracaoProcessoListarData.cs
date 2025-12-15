using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ConfiguracaoProcessoListarData : ISMCMappable
    {
        public long SeqProcesso { get; set; }

        public bool SolicitacaoAssociada { get; set; }

        public bool ProcessoEncerrado { get; set; }

        public long Seq { get; set; }

        public string Descricao { get; set; }
    }
}
