using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarEnadeData : ISMCMappable
    {
        public string Categoria { get; set; }
        public int? Ano { get; set; }
        public string Condicao { get; set; }
        public string Descricao { get; set; }
    }
}
