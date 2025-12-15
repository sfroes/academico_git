using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CicloLetivoPlanoEstudoTurmasVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqPai { get; set; }

        public string Descricao { get; set; }

        public bool ExibirMenu { get; set; }
    }
}