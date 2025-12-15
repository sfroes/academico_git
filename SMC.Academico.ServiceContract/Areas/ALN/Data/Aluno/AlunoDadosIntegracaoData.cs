using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class AlunoDadosIntegracaoData : ISMCMappable
    {
        public long CodigoAlunoMigracao { get; set; }

        public long SeqOrigem { get; set; }
    }
}