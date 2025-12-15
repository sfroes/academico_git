using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;
using SMC.SGA.Administrativo.Areas.CUR.Views.Requisito.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class RequisitoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqMatrizCurricular { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqCurriculoCursoOferta { get; set; }

        [SMCIgnoreProp]
        public long? SeqComponenteCurricular { get; set; }

        [SMCIgnoreProp]
        public string DescricaoComponenteCurricular { get; set; }

        [SMCIgnoreProp]
        public long? SeqDivisaoCurricularItem { get; set; }

        [SMCIgnoreProp]
        public string DescricaoDivisaoCurricularItem { get; set; }

        /// <summary>
        /// Retorna o sequencial do componente ou divisão do requisito para agrupar os registros
        /// </summary>
        [SMCIgnoreProp]
        public long SeqGrupo { get => SeqComponenteCurricular.HasValue ? SeqComponenteCurricular.Value : SeqDivisaoCurricularItem.GetValueOrDefault(); }

        /// <summary>
        /// Retorna a descrição do grupo com o label do componente ou divisão
        /// </summary>
        [SMCIgnoreProp]
        public string DescricaoGrupo
        {
            get
            {
                return SeqComponenteCurricular.HasValue ?
                    string.Format(UIResource.Label_ComponenteCurricular, DescricaoComponenteCurricular) :
                    string.Format(UIResource.Label_DivisaoCurricularItem, DescricaoDivisaoCurricularItem);
            }
        }

        [SMCHidden]
        public bool UnicaMatrizAssociada { get; set; }

        public TipoRequisitoAssociado Associado { get; set; }

        public List<RequisitoListarItemViewModel> Itens { get; set; }

        public List<RequisitoListarMatrizCurricularViewModel> MatrizesCurriculares { get; set; }
    }
}