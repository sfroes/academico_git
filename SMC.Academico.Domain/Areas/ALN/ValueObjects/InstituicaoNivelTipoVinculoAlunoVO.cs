using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class InstituicaoNivelTipoVinculoAlunoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoNivel { get; set; }

        public long SeqTipoVinculoAluno { get; set; }

        public bool? ExigeParceriaIntercambioIngresso { get; set; }

        public bool? ExigeCurso { get; set; }

        public bool? ConcedeFormacao { get; set; }

        public bool? ExigeOfertaMatrizCurricular { get; set; }

        public short? QuantidadeOfertaCampanhaIngresso { get; set; }

        public TipoCobranca TipoCobranca { get; set; }

        public bool? PossuiValorFixoMatricula { get; set; }

        public IList<InstituicaoNivelTipoTermoIntercambio> TiposTermoIntercambio { get; set; }

    }
}
