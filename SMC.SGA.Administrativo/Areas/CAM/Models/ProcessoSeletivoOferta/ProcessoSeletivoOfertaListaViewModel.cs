using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ProcessoSeletivoOfertaListaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqCampanhaOferta { get; set; }

        [SMCHidden]
        public string TipoOfertaToken { get; set; }

        public string TipoOferta { get; set; }

        public string Oferta { get; set; }

        public int Vagas { get; set; }

        public int Ocupadas { get; set; }

        public int Disponíveis => Vagas - Ocupadas;

        [SMCHidden]
        public bool PossuiVinculoConvocacao { get; set; }

        /// <summary>
        /// Resultado da diferença das vagas inseridas com a quantidade de 
        /// vagas registrada na base
        /// </summary> 
        [SMCHidden]
        public int VagasDiferenca { get; set; }
    }
}