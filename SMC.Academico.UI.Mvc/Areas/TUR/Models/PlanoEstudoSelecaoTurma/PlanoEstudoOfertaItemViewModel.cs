using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.TUR.Models
{
    public class PlanoEstudoOfertaItemViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqSolicitacaoMatricula { get; set; }
        
        [SMCHidden]
        public int SeqTurma { get; set; }
        
        [SMCHidden]
        public int Ano { get; set; }
        
        [SMCHidden]
        public int Semestre { get; set; }
        
        [SMCHidden]
        public int Periodo { get; set; }

        public TurmaOfertaMatricula Pertence { get; set; }
        
        public string DescricaoTurma { get; set; }
        
        public List<string> ListaCargasHorarias { get; set; }
        
        public string DescricaoConteudoTurma { get; set; }
        
        public string NomeDisciplina { get; set; }
    }
}
