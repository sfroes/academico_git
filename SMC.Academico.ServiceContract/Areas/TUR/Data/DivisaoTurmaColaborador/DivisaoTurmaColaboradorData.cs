using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class DivisaoTurmaColaboradorData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTurma { get; set; }

        public long SeqDivisao { get; set; }

        public long SeqDivisaoTurma { get; set; }

        public long SeqColaborador { get; set; }

        public ColaboradorData Colaborador { get; set; }

        public short? QuantidadeCargaHoraria { get; set; }
                
        public string DescricaoTipoOrganizacao { get; set; }

        public bool ExisteHistoricoEscolarAluno { get; set; }
    }
}
