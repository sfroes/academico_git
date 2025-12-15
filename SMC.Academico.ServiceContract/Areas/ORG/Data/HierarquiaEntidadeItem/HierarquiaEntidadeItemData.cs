using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class HierarquiaEntidadeItemData : ISMCMappable, ISMCSeq​​
    {
        /// <summary>
        /// Seq do item
        /// </summary>
        public long Seq { get; set; }

        /// <summary>
        /// Seq do pai do item
        /// </summary>
        public long? SeqItemSuperior { get; set; }

        [SMCMapProperty("ItemSuperior.Entidade.Nome")]
        public string NomeItemSuperior { get; set; }

        [SMCMapProperty("ItemSuperior.Entidade.Sigla")]
        public string SiglaItemSuperior { get; set; }
                
        public string DescricaoItemSuperior
        {
            get
            {   
                if(string.IsNullOrEmpty(SiglaItemSuperior))
                    return string.Format("{0}", NomeItemSuperior);
                else
                    return string.Format("{0} - {1}", SiglaItemSuperior, NomeItemSuperior);
            }
        } 
        
        /// <summary>
        /// Seq da entidade vinculada ao item
        /// </summary>
        public long SeqEntidade { get; set; }
               
        /// <summary>
        /// Seq da Hierarquia da entidade
        /// </summary>
        public long SeqHierarquiaEntidade { get; set; }

        public long SeqTipoHierarquiaEntidadeItem { get; set; }
    }
}
