using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DadosPrivadosDiplomadoVO : ISMCMappable
    {
        public List<FiliacaoVO> Filiacao { get; set; }
        public HistoricoVO Historico { get; set; }
        public InformacoesProcessoJudicialVO InformacoesProcessoJudicial { get; set; }

        [Obsolete("OBSOLETO. Utilizar CargaHorariaCursoV2")]
        public int? CargaHorariaCurso { get; set; }

        public CargaHorariaVO CargaHorariaCursoV2 { get; set; }
        public IngressoVO Ingresso { get; set; }
    }
}
