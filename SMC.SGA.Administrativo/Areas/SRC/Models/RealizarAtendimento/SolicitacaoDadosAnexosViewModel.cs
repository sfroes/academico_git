using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Formularios.Common.Areas.FRM.Enums;
using SMC.Formularios.UI.Mvc.Attributes;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoDadosAnexosViewModel : SolicitacaoServicoPaginaViewModelBase
    {

        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.ATENDIMENTO_PADRAO_ATENDIMENTO_DADOS_SOLICITACAO_ANEXO;

        public bool ExigeJustificativa { get; set; }

        public bool ExigeFormulario { get; set; }

        [SMCSGF(RenderMode = FormaExibicaoSecao.Nenhum)]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public List<FormularioPadraoDadoFormularioViewModel> DadoFormulario { get; set; }

        public string ObservacoesJustificativa { get; set; }

        public string DescricaoJustificativa { get; set; }

        public string DescricaoOriginal { get; set; }

        public string DescricaoAtualizada { get; set; }

        public List<KeyValuePair<long, string>> NomesFormularios { get; set; }

        public List<DocumentosAtendimentoItemViewModel> Documentos { get; set; }
    }
}