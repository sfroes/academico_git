using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class PessoaAtuacaoBeneficioBeneficioHistoricoVigenciaViewModel : SMCViewModelBase
    {

        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }
        
        [SMCHidden]
        public long SeqPessoaAtuacaoBeneficio { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        [SMCReadOnly]        
        [SMCSize(SMCSize.Grid3_24)]
        public DateTime DataInicioVigencia { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        [SMCSize(SMCSize.Grid3_24)]
        public DateTime? DataFimVigencia { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24)]
        public string UsuarioInclusao { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid5_24)]
        [SMCValueEmpty("-")]
        public string DescricaoMotivoAlteracaoBeneficio { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid9_24)]
        [SMCValueEmpty("-")]
        public string Observacao { get; set; }

        [SMCValueEmpty("-")]
        public DateTime? DataInclusao { get; set; }

        [SMCHidden]
        public bool Atual { get; set; }
    }
}