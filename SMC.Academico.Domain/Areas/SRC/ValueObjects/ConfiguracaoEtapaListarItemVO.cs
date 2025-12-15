using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ConfiguracaoEtapaListarItemVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public long SeqProcesso { get; set; }

        public long SeqConfiguracaoProcesso { get; set; }

        public string DescricaoConfiguracaoProcesso { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public string DescricaoConfiguracaoEtapa { get; set; }

        public bool SolicitacaoAssociada { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }

        public bool PossuiDocumentosRequeridos { get; set; }
    }
}
