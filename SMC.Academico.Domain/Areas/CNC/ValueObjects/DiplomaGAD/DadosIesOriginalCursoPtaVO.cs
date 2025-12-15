using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DadosIesOriginalCursoPtaVO : ISMCMappable
    {
        public string Nome { get; set; }
        public int? CodigoMEC { get; set; }
        public string Cnpj { get; set; }
        public EnderecoVO Endereco { get; set; }
        public AtoRegulatorioBaseVO Descredenciamento { get; set; }
    }
}
