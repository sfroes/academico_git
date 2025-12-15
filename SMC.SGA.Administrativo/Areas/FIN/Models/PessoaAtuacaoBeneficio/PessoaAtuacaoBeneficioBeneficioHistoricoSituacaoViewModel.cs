using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class PessoaAtuacaoBeneficioBeneficioHistoricoSituacaoViewModel : SMCViewModelBase
    {

        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }
        
        [SMCHidden]
        public long SeqPessoaAtuacaoBeneficio { get; set; }

        [SMCReadOnly]
        [SMCSelect]
        [SMCSize(SMCSize.Grid4_24)]
        public SituacaoChancelaBeneficio SituacaoChancelaBeneficio { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        [SMCReadOnly]        
        [SMCSize(SMCSize.Grid3_24)]
        public DateTime DataInicioSituacao { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24)]
        public string UsuarioInclusao { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid13_24)]
        [SMCValueEmpty("-")]
        public string Observacao { get; set; }

        [SMCHidden]
        public bool Atual { get; set; }
    }
}