using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class InformacaoEnadeVO : ISMCMappable
    {
        public string Condicao { get; set; } //enum Ingressante, Concluinte 
        public string Edicao { get; set; }
    }
}
