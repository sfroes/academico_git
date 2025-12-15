using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class AlunoOrientacaoViewModel : SMCViewModelBase
    {
        public string NomeOrientador { get; set; }

        public TipoParticipacaoOrientacao TipoParticipacaoOrientacao { get; set; }
    }
}