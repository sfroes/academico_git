using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ProcessoEtapaSGFData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }
        
        public bool Obrigatorio { get; set; }

        public bool? AssociarEtapa { get; set; }
    }
}
