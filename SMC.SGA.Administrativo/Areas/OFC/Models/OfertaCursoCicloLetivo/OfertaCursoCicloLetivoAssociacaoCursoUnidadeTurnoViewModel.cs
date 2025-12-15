using SMC.Academico.UI.Mvc.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.OFC.Models
{
    public class OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoViewModel : SMCPagerViewModel, ISMCMappable
    {
        [SMCHidden]
        public long SeqOfertaCursoCicloLetivo { get; set; }

        [SMCHidden]
        public long SeqAssociacaoCursoUnidadeTurno { get; set; }

        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [LookupCurso]
        [SMCSize(SMCSize.Grid18_24)]
        public SGALookupViewModel SeqCurso { get; set; }

        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSelect("Modalidades")]
        [SMCSize(SMCSize.Grid6_24)]
        public long? SeqModalidade { get; set; }

        public List<SMCSelectItem> Modalidades { get; set; }

        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSelect("UnidadesLocalidades")]
        [SMCSize(SMCSize.Grid12_24)]
        public long SeqUnidadeLocalidadeSelecionada { get; set; }

        public List<SMCSelectItem> UnidadesLocalidades { get; set; }

        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSelect("Formacoes")]
        [SMCSize(SMCSize.Grid12_24)]
        public long? SeqFormacao { get; set; }

        public List<SMCSelectItem> Formacoes { get; set; }
        
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSelect("Turnos")]
        [SMCSize(SMCSize.Grid6_24)]
        public long SeqTurno { get; set; }

        public List<SMCSelectItem> Turnos { get; set; }

        [SMCRequired]
        [SMCSelect("CurriculosPadrao")]
        [SMCSize(SMCSize.Grid18_24)]
        public long SeqCurriculoPadraoSelecionado { get; set; }

        public List<SMCSelectItem> CurriculosPadrao { get; set; }

    }
}