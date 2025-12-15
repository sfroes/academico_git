using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using System;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class RelatorioAcompanhamentoNotaViewModel : SMCViewModelBase, ISMCMappable
    {
        public TurmaNotaViewModel Turma { get; set; }
        public DivisaoTurmaNotaViewModel DivisaoTurma { get; set; }
        public bool TemTurma { get; set; }
        public DateTime DataRelatorio { get; set; }
    }
}