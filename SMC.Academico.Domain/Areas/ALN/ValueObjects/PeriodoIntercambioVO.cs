using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class PeriodoIntercambioVO : EntidadeVO, ISMCMappable
    {
        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public long SeqPessoaAtuacao { get; set; }
        
        public long SeqParceriaIntercambio { get; set; }

        public long SeqAluno { get; set; }

        public long SeqCicloLetivo { get; set; }

        public long? SeqCursoOfertaLocalidadeTurno { get; set; }

        public long? SeqPessoaAtuacaoTermoIntercambio { get; set; }

        public bool PermiteEdicao { get; set; }
    }
}