using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.ServiceContract.Data;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class MaterialListarDynamicModel : SMCDynamicViewModel, ISMCTreeNode
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCMapForceFromTo]
        [SMCMapProperty("SeqSuperior")]
        public long? SeqPai { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqOrigemMaterial { get; set; }

        [SMCHidden]
        [SMCParameter]
        public TipoOrigemMaterial TipoOrigemMaterial { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqOrigem { get; set; }

        //Descrição da origem do material.
        [SMCHidden]
        [SMCParameter]
        public string DescricaoOrigem { get; set; }

        [SMCHidden]
        public long? SeqArquivoAnexado { get; set; }

        [SMCHidden]
        public Guid? UidArquivoAnexado { get; set; }

        [SMCSelect]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        public TipoMaterial TipoMaterial { get; set; }

        [SMCRequired]
        [SMCDescription]
        [SMCSize(SMCSize.Grid16_24)]
        public string Descricao { get; set; }

        [SMCSize(SMCSize.Grid19_24)]
        public string UrlLink { get; set; }

        [SMCMultiline]
        [SMCSize(SMCSize.Grid24_24)]
        public string Observacao { get; set; }

        /// <summary>
        /// O Componente tem comportamento de nó folha sem poder ser selecionado
        /// </summary>
        [SMCHidden]
        public bool Folha { get => TipoMaterial != TipoMaterial.Pasta; }

        [SMCHidden]
        public DateTime DataInclusao { get; set; }

        [SMCHidden]
        public DateTime? DataAlteracao { get; set; }

        [SMCHidden]
        public ArquivoAnexadoData ArquivoAnexado { get; set; }
    }
}