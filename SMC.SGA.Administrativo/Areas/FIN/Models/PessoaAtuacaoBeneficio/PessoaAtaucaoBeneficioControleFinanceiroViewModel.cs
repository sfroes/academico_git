using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class PessoaAtaucaoBeneficioControleFinanceiroViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public int? SeqContratoBeneficioFinanceiro { get; set; }

        //FIX: Remover ao corrigir o mapeamento de long para lookup dentro de um mestre detalhe
        [SMCHidden]
        public long? SeqCicloLetivo { get => CicloLetivo?.Seq; set => CicloLetivo = new CicloLetivoLookupReturnViewModel() { Seq = value }; }

        [CicloLetivoLookup]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public CicloLetivoLookupReturnViewModel CicloLetivo { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public DateTime? DataInicio { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCValueEmpty("-")]
        public DateTime? DataFim { get; set; }

        [SMCReadOnly]
        [SMCValueEmpty("-")]
        public DateTime? DataExclusao { get; set; }
    }
}