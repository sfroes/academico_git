using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ConfigurarVagasOfertaCampanhaListaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long? Seq { get; set; }

        [SMCHidden]
        public long? SeqCampanha { get; set; }

        [SMCHidden]
        public string TipoOferta { get; set; }

        [SMCHidden]
        public string TipoOfertaToken { get; set; }

        [SMCHidden]
        public string Oferta { get; set; }

        public int Vagas { get; set; }

        public string TipoOfertaDisplay => TipoOferta;

        public string OfertaDisplay => Oferta;

        /// <summary>
        /// Vagas registradas na base de dados
        /// </summary>
        [SMCHidden]
        public int VagasBase { get; set; }

        /// <summary>
        /// Resultado da diferença das vagas inseridas com a quantidade de 
        /// vagas registrada na base
        /// </summary>
        [SMCHidden]
        public int VagasDiferenca => Vagas - VagasBase;

        /// <summary>
        /// RN_CAM_069 - Usar vagas disponíveis na campanha
        /// VagasDisponiveis = (VagasBase - Ocupadas)
        /// Subtração das vagas da oferta com a quantidade de ingressantes
        /// cuja situação é diferente de "Desistente" e "Cancelado (Prouni)
        /// </summary>
        public int Disponiveis { get; set; }

    }
}