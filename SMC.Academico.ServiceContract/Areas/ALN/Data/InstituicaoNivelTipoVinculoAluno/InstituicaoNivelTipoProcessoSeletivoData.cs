using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class InstituicaoNivelTipoProcessoSeletivoData : ISMCMappable
    {
        public long Seq { get; set; }
         
        public long SeqInstituicaoNivelFormaIngresso { get; set; }
         
        public long SeqTipoProcessoSeletivo { get; set; }

        [SMCMapProperty("TipoProcessoSeletivo.Descricao")]
        public string TipoProcessoSeletivoDescricao { get; set; }
    }
}
