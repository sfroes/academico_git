using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class CabecalhoInformacaoProcessoVO : ISMCMappable
    {
        public long SeqProcesso { get; set; }

        public long? SeqGrupoEscalonamento { get; set; }

        public string DescricaoProcesso { get; set; }

        public string DescricaoGrupoEscalonamento { get; set; }
    }
}