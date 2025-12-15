using SMC.Framework.UI.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.CNC.Models
{
    public class IntegralizacaoTipoComponenteCurricularViewModel : SMCViewModelBase
    {
        public long SeqComponenteCurricular { get; set; }

        public long SeqTipoComponenteCurricular { get; set; }

        public string Sigla { get; set; }

        public string Descricao { get; set; }
    }
}
