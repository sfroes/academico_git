using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
   public class IesEmissoraVO : ISMCMappable
    {
        public EnderecoVO Endereco { get; set; }
        public MantenedoraVO Mantenedora { get; set; }
        public string Nome { get; set; }
        public int? CodigoMEC { get; set; }
        public string Cnpj { get; set; }
        public AtoRegulatorioVO Credenciamento { get; set; }
        public AtoRegulatorioVO Recredenciamento { get; set; }
        public AtoRegulatorioVO RenovacaoDeRecredenciamento { get; set; }
    }
}
