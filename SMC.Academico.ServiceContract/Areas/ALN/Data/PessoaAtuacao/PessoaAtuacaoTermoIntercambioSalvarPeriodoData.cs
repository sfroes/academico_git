using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class PessoaAtuacaoTermoIntercambioSalvarPeriodoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public long SeqParceriaIntercambio { get; set; }

        public long SeqAluno { get; set; }

        public long SeqCicloLetivo { get; set; }

        public long? SeqCursoOfertaLocalidadeTurno { get; set; }

        public long SeqPessoaAtuacaoTermoIntercambio { get; set; }
    }
}