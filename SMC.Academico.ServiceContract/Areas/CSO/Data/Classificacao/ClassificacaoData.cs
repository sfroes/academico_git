using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class ClassificacaoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTipoClassificacao { get; set; }

        public string Descricao { get; set; }

        public long? SeqClassificacaoSuperior { get; set; }

        /// <summary>
        /// Setado quando o nó deve ser selecionável no lookup
        /// </summary>
        public bool? TipoClassificacaoSelecionavel { get; set; }
    }
}