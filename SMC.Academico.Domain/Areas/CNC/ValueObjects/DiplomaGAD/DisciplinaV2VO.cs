using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DisciplinaV2VO : ISMCMappable
    {
        public string CodigoDisciplina { get; set; }
        public string Situacao { get; set; } //enum Aprovado, Reprovado, Pendente
        public AprovacaoDisciplinaVO Aprovacao { get; set; }
        public string Disciplina { get; set; }
        public string Periodo { get; set; }
        public List<CargaHorariaComEtiquetaVO> CargaHorariaComEtiqueta { get; set; } //Pode ser associada a uma etiqueta definida no Currículo Escolar permitindo identificar a natureza da carga horária dentro do Currículo do Curso
        public string TipoAvaliacao { get; set; } //enum Nota, NotaAteCem, Conceito, ConceitoRM, ConceitoEspecificoDoCurso
        public string Conceito { get; set; } //enum A+, A, A-, B+, B, B-, C+, C, C-, D+, D, D-, E+, E, E-, F+, F, F-
        public string ConceitoRM { get; set; } //enum  A, B, C, APD, APP, APR
        public string ConceitoEspecificoDoCurso { get; set; }
        public double? Nota { get; set; }
        public double? NotaAteCem { get; set; }
        public List<DocenteVO> Docentes { get; set; }

        [Obsolete("OBSOLETO.Utilizar CargaHorariaV2")]
        public int? CargaHoraria { get; set; }

        public CargaHorariaVO CargaHorariaV2 { get; set; }
    }
}
