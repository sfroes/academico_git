using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Professor.Models
{
    public class ProfessorSeletorViewModel : SMCViewModelBase
    {
        /// <summary>
        /// Instituição de ensino selecionada pelo usuário
        /// </summary>
        [SMCSize(SMCSize.Grid24_24)]
        [SMCSelect(nameof(InstituicoesEnsino), AutoSelectSingleItem = true)]
        [SMCRequired]
        public long? SeqInstituicaoEnsino { get; set; }

        /// <summary>
        /// Lista de instituições de ensino disponíveis para o usuário
        /// </summary>
        [SMCDataSource]
        public List<SMCDatasourceItem> InstituicoesEnsino { get; set; }

        /// <summary>
        /// Professor selecionado pelo usuário
        /// </summary>
        public long? SeqProfessor { get; set; }
                
        /// <summary>
        /// Flag para habilitar ou não a seleção de professor
        /// </summary>
        public bool HabilitarSelecao { get; set; }
    }
}