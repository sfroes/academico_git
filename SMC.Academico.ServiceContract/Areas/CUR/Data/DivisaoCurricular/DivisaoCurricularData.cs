using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class DivisaoCurricularData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public string Descricao { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqTipoDivisaoCurricular { get; set; }

        public string DescricaoTipoDivisaoCurricular { get; set; }

        public long SeqRegimeLetivo { get; set; }

        public string DescricaoRegimeLetivo { get; set; }

        public long SeqInstituicaoNivelEnsino { get; set; }

        public long SeqInstituicaoNivelTipoDivisaoCurricular { get; set; }

        public long SeqInstituicaoNivelRegimeLetivo { get; set; }
        
        public List<DivisaoCurricularItemData> Itens { get; set; }
    }
}