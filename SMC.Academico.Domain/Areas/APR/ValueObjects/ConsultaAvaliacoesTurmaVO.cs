using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class ConsultaAvaliacoesTurmaVO : ISMCMappable
    {
        public long SeqTurma { get; set; }

        public string Descricao { get; set; }

        public string Falta { get; set; }

        public string NotaTotal { get; set; }

        public string NotaTotalReavaliacao { get; set; }

        public string Situacao { get; set; }

        public List<DetalhesAvaliacaoVO> Avaliacoes { get; set; }

        public bool DiarioFechado { get; set; }

        public bool PossuiApuracaoFrequencia { get; set; }

        public List<ConsultaAvaliacoesDivisaoTurmaVO> DivisoesTurma { get; set; }
    }
}