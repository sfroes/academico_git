using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class TurmaCompartilhadaGrupoVO : ISMCMappable
    {
        public int CodigoTurma { get; set; }

        public int QuantidadeConfiguracao { get; set; }
    }
}