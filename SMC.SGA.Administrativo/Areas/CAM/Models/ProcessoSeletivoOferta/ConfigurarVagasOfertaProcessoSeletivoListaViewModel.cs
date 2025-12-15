using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ConfigurarVagasOfertaProcessoSeletivoListaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long? Seq { get; set; }

        [SMCHidden]
        public long SeqCampanhaOferta { get; set; }

        [SMCHidden]
        public long? SeqProcessoSeletivo { get; set; }

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
    }
}