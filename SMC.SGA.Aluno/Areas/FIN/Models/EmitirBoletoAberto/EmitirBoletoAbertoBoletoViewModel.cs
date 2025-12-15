using SMC.Framework.UI.Mvc;
using SMC.Framework.Util;
using SMC.Financeiro.Common.Areas.GRA.Enums;

namespace SMC.SGA.Aluno.Areas.FIN.Models
{
    public class EmitirBoletoAbertoBoletoViewModel : SMCViewModelBase
    {
        public string NomePessoa { get; set; }

        public SituacaoTitulo SituacaoTitulo { get; set; }
        
        public string DescricaoSituacaoTitulo { get { return SMCEnumHelper.GetDescription(SituacaoTitulo); } }

        public int SeqTitulo { get; set; }

        public long SeqServico { get; set; }

    }
}