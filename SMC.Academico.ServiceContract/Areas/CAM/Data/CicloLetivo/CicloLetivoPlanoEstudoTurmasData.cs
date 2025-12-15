using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CicloLetivoPlanoEstudoTurmasData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long? SeqPai { get; set; }

        public string Descricao { get; set; }

        public bool ExibirMenu { get; set; }
    }
}