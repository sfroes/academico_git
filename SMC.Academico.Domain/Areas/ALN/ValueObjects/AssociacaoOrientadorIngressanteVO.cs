using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class AssociacaoOrientadorIngressanteVO : ISMCMappable
    {
        public long SeqIngressante { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long? SeqTipoOrientacao { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long? SeqTipoIntercambio { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public List<AssociacaoOrientadorIngressanteItemVO> Orientacoes { get; set; }
    }
}