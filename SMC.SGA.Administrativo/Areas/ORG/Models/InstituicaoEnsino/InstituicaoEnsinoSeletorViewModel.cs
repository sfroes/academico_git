using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class InstituicaoEnsinoSeletorViewModel
    {
        /// <summary>
        /// Instituição de ensino selecionada pelo usuário
        /// </summary>
        [SMCSize(SMCSize.Grid24_24)]
        [SMCSelect("InstituicoesEnsino")]
        public long? SeqInstituicaoEnsino { get; set; }

        /// <summary>
        /// Lista de instituições de ensino disponíveis para o usuário
        /// </summary>
        public List<SMCDatasourceItem> InstituicoesEnsino { get; set; }

        /// <summary>
        /// Flag para habilitar ou não a seleção da instituição de ensino
        /// </summary>
        public bool HabilitarSelecao { get; set; }
    }
}