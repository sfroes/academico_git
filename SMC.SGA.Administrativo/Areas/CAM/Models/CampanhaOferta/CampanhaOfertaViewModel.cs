using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaOfertaViewModel : SMCViewModelBase
    {
        /// <summary>
        /// Tipo Object necessário para implementação do lookup receber seq duplo, 
        /// da linha selecionada
        /// </summary>
        [SMCKey]
        [SMCOrder(0)]
        public object Seq { get; set; }

        //Problema que ocorre ao popular duas vezes, a propriedade SeqCampanha, causa o reset do lookup
        //[SMCHidden]
        //public long? SeqCampanha { get; set; }

        [SMCDescription]
        [SMCOrder(1)]
        public string Descricao { get; set; }

        [SMCHidden]
        public long? SeqTipoOferta { get; set; }

        [SMCHidden]
        public string TipoOferta { get; set; }

        [SMCHidden]
        public string TipoOfertaToken { get; set; }

        [SMCHidden]
        public string CursoOferta { get; set; }

        [SMCHidden]
        public string Localidade { get; set; }

        [SMCHidden]
        public string Turno { get; set; }

        [SMCHidden]
        public string AreaConcentracao { get; set; }

        [SMCHidden]
        public string LinhaPesquisa { get; set; }

        [SMCHidden]
        public string EixoTematico { get; set; }

        [SMCHidden]
        public string AreaTematica { get; set; }

        [SMCHidden]
        public string Orientador { get; set; }

        [SMCHidden]
        public string Turma { get; set; }

        [SMCHidden]
        public string Vagas { get; set; }

        [SMCHidden]
        public long? SeqOrientador { get; set; }

        [SMCHidden]
        public long? SeqFormacaoEspecifica { get; set; }

        [SMCHidden]
        public long? SeqTurma { get; set; }

        [SMCHidden]
        public long? SeqCursoOfertaLocalidadeTurno { get; set; }
    }
}