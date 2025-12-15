using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class ConsultaAvaliacoesDivisaoTurmaVO : ISMCMappable
    {
        public long SeqDivisaoTurma { get; set; }

        public string Descricao { get; set; }

        public string Nota { get; set; }

        public List<DetalhesAvaliacaoVO> Avaliacoes { get; set; }
    }
}