using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class CabecalhoInformacaoProcessoData : ISMCMappable
    {
        public long SeqProcesso { get; set; }

        public long? SeqGrupoEscalonamento { get; set; }

        public string DescricaoProcesso { get; set; }

        public string DescricaoGrupoEscalonamento { get; set; }
    }
}