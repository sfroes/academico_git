using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class ParticipacaoEnadeVO : ISMCMappable
    {
        public string Situacao { get; set; } //enum Habilitado, Irregular, Não habilitado
        public InformacaoEnadeVO Informacoes { get; set; }
        public EnadeNaoHabilitadoVO NaoHabilitado { get; set; }
    }
}
