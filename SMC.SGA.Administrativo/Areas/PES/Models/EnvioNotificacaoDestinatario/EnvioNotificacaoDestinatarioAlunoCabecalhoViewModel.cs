using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class EnvioNotificacaoDestinatarioAlunoCabecalhoViewModel : SMCViewModelBase
    {
        public long NumeroRegistroAcademico { get; set; }

        public string Nome { get; set; }

        public string DescricaoSituacaoMatricula { get; set; }

        public string NomeEntidadeResponsavel { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string DescricaoVinculo { get; set; }


    }
}