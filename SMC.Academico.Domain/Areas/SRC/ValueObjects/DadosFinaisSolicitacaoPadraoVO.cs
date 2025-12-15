using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DadosFinaisSolicitacaoPadraoVO : ISMCMappable
    {
        public bool ExigeJustificativa { get; set; }

        public bool ExigeFormulario { get; set; }

        public List<SolicitacaoDadoFormulario> DadoFormulario { get; set; }

        public long SeqJustificativa { get; set; }

        public string ObservacoesJustificativa { get; set; }

        public string DescricaoJustificativa { get; set; }

        public string DescricaoOriginal { get; set; }

        public string DescricaoAtualizada { get; set; }
        
        public List<KeyValuePair<long, string>> NomesFormularios { get; set; }

        public string SituacaoAtualSolicitacao { get; set; }

        public string ObservacaoSituacaoAtual { get; set; }

    }
}