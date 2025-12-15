using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarEnadeVO : ISMCMappable
    {
        public string Categoria { get; set; }
        public int? Ano { get; set; }
        public string Condicao { get; set; }
        public string Descricao { get; set; }
    }
}
