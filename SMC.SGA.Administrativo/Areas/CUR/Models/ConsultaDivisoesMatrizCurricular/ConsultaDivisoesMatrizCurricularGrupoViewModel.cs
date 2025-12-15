using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ConsultaDivisoesMatrizCurricularGrupoViewModel : SMCViewModelBase
    {
        public string DescricaoFormacaoEspecifica { get; set; }

        public List<string> DescricoesBeneficios { get; set; }

        public List<string> DescricoesCondicoesObrigatoriedade { get; set; }
    }
}