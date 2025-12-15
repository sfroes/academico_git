using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class TermoIntercambioListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid8_24)]
        public override long Seq { get; set; }
         
        [SMCSize(SMCSize.Grid8_24)]
        public string Descricao { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string NivelEnsino { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string TipoTermoIntercambio { get; set; }

        public long SeqParceriaIntercambioInstituicaoExterna { get; set; }

        public bool Ativo
        {
            get
            {
                bool retorno = false;

                if((DataInicio <= DateTime.Now.Date && DataFim >= DateTime.Now.Date) 
                    || (DataInicio == null && DataFim == null))
                {
                    retorno = true;
                }

                return retorno;
            }
        }

        public string InstituicaoEnsinoExterna { get; set; }

        [SMCValueEmpty("-")]
        public DateTime? DataInicio { get; set; }

        [SMCValueEmpty("-")]
        public DateTime? DataFim { get; set; }

        public SMCMasterDetailList<TermoIntercambioVigenciaViewModel> Vigencias { get; set; }

        [SMCHidden]
        public SMCMasterDetailList<ParceriaIntercambioArquivoViewModel> Arquivos { get; set; }

        public bool PossuiAnexo {
            get { return (Arquivos != null && Arquivos.Count > 0) ? true : false; }
        }

        public SMCMasterDetailList<TermoIntercambioTipoMobilidadeViewModel> TiposMobilidade { get; set; }
    }
}