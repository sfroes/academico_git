using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DocumentoDiplomaVO : ISMCMappable
    {
        public CriarDadosDiplomaVO Diploma { get; set; }
        public CriarDadosDocumentacaoAcademicaVO DocumentacaoAcademica { get; set; }

        [Obsolete("OBSOLETO. Utilizar type")]
        public bool IsNsf { get; set; }

        public string DegreeType { get; set; } //enum Default, Nsf, JudicialDecision
    }
}
