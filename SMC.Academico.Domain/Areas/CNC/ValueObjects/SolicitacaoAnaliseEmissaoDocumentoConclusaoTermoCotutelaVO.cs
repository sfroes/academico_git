using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoTermoCotutelaVO : ISMCMappable
    {
        public long SeqTermoIntercambio { get; set; }
        public TipoParceriaIntercambio TipoParceriaIntercambio { get; set; }
        public long SeqInstituicaoEnsino { get; set; }
        public string NomeInstituicaoEnsino { get; set; }
        public int CodigoPaisInstituicaoExterna { get; set; }
    }
}