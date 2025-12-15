using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.PES.Models.SuporteTecnico
{
    public class IntegracaoCSCViewModel : SMCViewModelBase
    {
        public string Token { get; set; }

        public long CodigoPessoa { get; set; }

        public DateTime DataAcesso { get; set; }

        public string CodigoEstabelecimento { get; set; }

        public List<int> CodigosServicosUsuarios { get; set; }

        public long? NumeroRegistroAcademico { get; set; }

        public string NomeCurso { get; set; }

        public string DescricaoTurno { get; set; }

        public string DescricaoUnidade { get; set; }

        public bool? IgnoreCache { get; set; }

        public bool Mobile { get; set; }

        public string UrlGestaoXChamados { get; set; }
    }
}
