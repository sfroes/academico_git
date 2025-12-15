using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class DivisaoTurmaColaboradorVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTurma { get; set; }

        public long SeqDivisao { get; set; }
        
        public long SeqDivisaoTurma { get; set; }

        public long SeqColaborador { get; set; }

        public ColaboradorVO Colaborador { get; set; }

        public short? QuantidadeCargaHoraria { get; set; }

        public string DescricaoTipoOrganizacao { get; set; }

        public bool ExisteHistoricoEscolarAluno { get; set; }
    }
}
