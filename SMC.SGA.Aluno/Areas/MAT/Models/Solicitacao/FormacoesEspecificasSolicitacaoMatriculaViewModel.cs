using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Aluno.Areas.MAT.Models
{
    public class FormacoesEspecificasSolicitacaoMatriculaViewModel : SMCViewModelBase
    {
        public string DescricaoTipoFormacaoEspecifica { get; set; }

        public string DescricoesFormacoesEspecificas { get; set; }
    }
}