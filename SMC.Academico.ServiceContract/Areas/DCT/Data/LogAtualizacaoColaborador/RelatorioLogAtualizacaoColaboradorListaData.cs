using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class RelatorioLogAtualizacaoColaboradorListaData : ISMCMappable
    {
        public string DataProcessamento { get; set; }

        public string Professor { get; set; }

        public string Acao { get; set; }

        public string Motivo { get; set; }

        public string Entidade { get; set; }

        public string AtividadeFinalizada { get; set; }

        public string Aluno { get; set; }

        public string DataInicioAfastamento { get; set; }

        public string DataFimAfastamento { get; set; }

        // Propriedade adicionada pois o Relatório está avaliando apenas a primeira linha para verificar a visualização
        public bool ExibeColunasInicioFimAfastamento { get; set; }
    }
}