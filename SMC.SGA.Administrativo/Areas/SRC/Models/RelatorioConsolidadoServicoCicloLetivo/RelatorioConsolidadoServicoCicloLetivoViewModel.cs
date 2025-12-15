using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class RelatorioConsolidadoServicoCicloLetivoViewModel : SMCViewModelBase, ISMCMappable
    {
        public long? SeqGrupoPrograma { get; set; }

        public string NomeGrupoPrograma { get; set; }

        public long? SeqEtapa { get; set; }

        public string DescricaoEtapa { get; set; }

        public long? SeqCurso { get; set; }

        public string NomeCurso { get; set; }

        public int? QuantidadeNaoIniciada { get; set; }

        public int? QuantidadeEmAndamento { get; set; }

        public int? QuantidadeFimComSucesso { get; set; }

        public int? QuantidadeFimSemSucesso { get; set; }

        public int? QuantidadeCancelada { get; set; }

    }
}