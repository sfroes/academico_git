using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class MatrizCurricularVO : ISMCMappable
    {
        public string Disciplina { get; set; }
        public string Periodo { get; set; }

        [Obsolete("À partir da v1.03 usar CargaHorariaV2")]
        public int CargaHoraria { get; set; }

        public CargaHorariaVO CargaHorariaV2 { get; set; }
        public string TipoAvaliacao { get; set; } //enum Nota, NotaAteCem, Conceito, ConceitoRM, ConceitoEspecificoDoCurso
        public string Conceito { get; set; } //enum A+, A, A-, B+, B, B-, C+, C, C-, D+, D, D-, E+, E, E-, F+, F, F-
        public string ConceitoRM { get; set; }//enum A, B, C, APD, APP, APR
        public string ConceitoEspecificoDoCurso { get; set; }
        public double? Nota { get; set; }
        public double? NotaAteCem { get; set; }
        public string Situacao { get; set; }//enum Aprovado, Reprovado, EstudoAproveitado, Trancado
        public List<DocenteVO> Docentes { get; set; }
    }
}
