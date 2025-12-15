using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ConfiguracaoEtapaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public long SeqProcesso { get; set; }

        public long SeqConfiguracaoProcesso { get; set; }

        public string OrientacaoEtapa { get; set; }

        public string DescricaoTermoEntregaDocumentacao { get; set; }
        
        public bool CamposReadyOnly { get; set; }
    }
}
