using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class ChancelaListaVO : ISMCMappable
    {
        //x.ConfiguracaoProcesso.Processo.Seq
        public long SeqProcesso { get; set; }

        //x.ConfiguracaoProcesso.Processo.Descricao
        public string DescricaoProcesso { get; set; }

        public List<ChancelaItemListaVO> Chancelas { get; set; }
    }
}
