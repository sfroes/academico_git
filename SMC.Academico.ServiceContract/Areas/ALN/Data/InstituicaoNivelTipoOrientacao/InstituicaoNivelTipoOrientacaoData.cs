using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class InstituicaoNivelTipoOrientacaoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoNivelTipoVinculoAluno { get; set; }

        public long? SeqInstituicaoNivelTipoTermoIntercambio { get; set; }

        public string TipoTermoIntercambio { get; set; }

        public string TipoOrientacao { get; set; }

        public long SeqTipoOrientacao { get; set; }

        public CadastroOrientacao CadastroOrientacaoIngressante { get; set; }

        public CadastroOrientacao CadastroOrientacaoAluno { get; set; }        

        public short? QuantidadeMaximaAluno { get; set; }

        public List<InstituicaoNivelTipoOrientacaoParticipacaoData> TiposParticipacao { get; set; }
    }
}