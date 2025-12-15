using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class AssociacaoOrientadorIngressanteData : ISMCMappable
    {
        public long SeqIngressante { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long? SeqTipoOrientacao { get; set; }

        public  long? SeqTipoIntercambio { get; set; }

        public List<AssociacaoOrientadorIngressanteItemData> Orientacoes { get; set; }
    }
}