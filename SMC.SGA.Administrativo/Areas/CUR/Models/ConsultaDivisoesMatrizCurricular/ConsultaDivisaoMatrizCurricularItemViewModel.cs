using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ConsultaDivisaoMatrizCurricularItemViewModel : SMCViewModelBase, ISMCSeq
    {
        /// <summary>
        /// Sequencial da divisão da matriz curricular
        /// </summary>
        public long Seq { get; set; }

        public short NumeroDivisaoCurricularItem { get; set; }

        public string DescricaoDivisaoCurricularItem { get; set; }

        public List<ConsultaDivisaoMatrizCurricularComponenteItemViewModel> ConfiguracoesComponentes { get; set; }

        /// <summary>
        /// Hierarquia dos grupos associados à divisão
        /// </summary>
        public List<GrupoCurricularListarDynamicModel> ComponentesGrupos { get; set; }

        public List<SMCTreeViewNode<GrupoCurricularListarDynamicModel>> ComponentesGruposTree
        {
            get { return SMCTreeView.For(ComponentesGrupos); }
        }

        /// <summary>
        /// Hierarquia dos grupos associados à divisão
        /// </summary>
        public List<GrupoCurricularListarDynamicModel> ConfiguracoesGrupos { get; set; }

        public List<SMCTreeViewNode<GrupoCurricularListarDynamicModel>> ConfiguracoesGruposTree
        {
            get { return SMCTreeView.For(ConfiguracoesGrupos); }
        }
    }
}