using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Lookups
{
    public class CampanhaOfertaLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        #region [ DataSources ]

        [SMCIgnoreProp]
        public List<SMCDatasourceItem> UnidadesResponsaveis { get; set; }

        [SMCIgnoreProp]
        public List<SMCDatasourceItem> Campanhas { get; set; }

        [SMCIgnoreProp]
        public List<SMCDatasourceItem> ProcessosSeletivos { get; set; }

        #endregion [ DataSources ]

        #region [ Parâmetros ]

        [SMCHidden]
        public bool? Ativas { get; set; }

        [SMCHidden]
        public List<long> SeqNivelEnsino { get; set; }

        [SMCHidden]
        [SMCKey]
        public long? Seq { get; set; }

        [SMCHidden]
        public long? SeqCicloLetivo
        {
            get => CicloLetivo?.Seq;
            set => CicloLetivo = new CicloLetivoLookupViewModel() { Seq = value };
        }

        [SMCHidden]
        public bool? CicloLetivoSomenteLeitura { get; set; }

        [SMCHidden]
        public bool SeqEntidadeResponsavelSomenteLeitura { get; set; }

        [SMCHidden]
        public bool SeqCampanhaSomenteLeitura { get; set; }

        [SMCHidden]
        public bool SeqProcessoSeletivoSomenteLeitura { get; set; }

        [SMCHidden]
        public long? SeqTipoVinculoAluno { get; set; }

        #endregion [ Parâmetros ]

        [CicloLetivoLookup]
        [SMCConditionalReadonly(nameof(CicloLetivoSomenteLeitura), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public CicloLetivoLookupViewModel CicloLetivo { get; set; }

        [SMCConditionalReadonly(nameof(SeqEntidadeResponsavelSomenteLeitura), true, PersistentValue = true)]
        [SMCSelect(nameof(UnidadesResponsaveis), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid12_24)]
        public long? SeqEntidadeResponsavel { get; set; }

        // por unidade ciclo ou unidade responsavel
        //FIX: Remover o readonly fixo ao corrigir o conditional readonly com persistent value e conditional rule
        //[SMCReadOnly]
        [SMCConditionalReadonly(nameof(SeqCampanhaSomenteLeitura), true, PersistentValue = true)]
        //[SMCConditionalReadonly(nameof(CicloLetivo), "", RuleName = "R2")]
        //[SMCConditionalReadonly(nameof(SeqEntidadeResponsavel), "", RuleName = "R3")]
        //[SMCConditionalRule("(R1) || (R2 && R3)")]
        [SMCDependency(nameof(CicloLetivo), "BuscarCampanhas", "Campanha", "CAM", false)]
        [SMCDependency(nameof(SeqEntidadeResponsavel), "BuscarCampanhas", "Campanha", "CAM", false)]
        [SMCSelect(nameof(Campanhas), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid9_24)]
        public long? SeqCampanha { get; set; }

        [SMCConditionalReadonly(nameof(SeqProcessoSeletivoSomenteLeitura), true, PersistentValue = true)]
        [SMCDependency(nameof(SeqCampanha), "BuscarProcessosSeletivos", "Ingressante", "ALN", true)]
        [SMCSelect(nameof(ProcessosSeletivos))]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public long? SeqProcessoSeletivo { get; set; }

        [SMCDescription]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid20_24, SMCSize.Grid12_24)]
        public string Descricao { get; set; }
    }
}