using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.MAT.Models
{
    public class TurmaSelecionadaItemViewModel : SMCViewModelBase
    {
        public TurmaOfertaMatricula PertenceAoCurriculo { get; set; }
        public string DescricaoConteudoTurma { get; set; }
        public string NomeDisciplina { get; set; }
        public List<string> ListaCargasHorarias { get; set; }
        public bool PermiteExclusao { get; set; }

        public long SeqSolicitacaoMatricula { get; set; }
        public long SeqSolicitacaoMatriculaItem { get; set; }
        public long SeqConfiguracaoEtapa { get; set; }
        public int SeqTurma { get; set; }
    }
}