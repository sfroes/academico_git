using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class RestricaoSolicitacaoSimultaneaData : ISMCMappable
    {       
        public long Seq { get; set; }

        public long SeqServico { get; set; }

        public long SeqServicoRestricao { get; set; }
    }
}
