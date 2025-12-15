using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class ChancelaListaData : ISMCMappable
    {

        //x.ConfiguracaoProcesso.Processo.Seq
        public long SeqProcesso { get; set; }

        //x.ConfiguracaoProcesso.Processo.Descricao
        public string DescricaoProcesso { get; set; }

        public List<ChancelaItemListaData> Chancelas { get; set; }
    }
}