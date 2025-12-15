using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class CurriculumVO : ISMCMappable
    {
        public string Codigo { get; set; } 
        public DateTimeOffset? DataCurriculo { get; set; } 
        public int? MinutosRelogioDaHoraAula { get; set; }
        public string NomeParaAreas { get; set; }
        public DadosMinimosCursoVO DadosCurso { get; set; }
        public IesEmissoraVO IesEmissora { get; set; }
        public List<DadosEtiquetaVO> Etiquetas { get; set; }
        public List<DadosAreaVO> Areas { get; set; }
        public EstruturaCurricularVO EstruturaCurricular { get; set; }
        public EstruturaAtividadeComplementarVO EstruturaAtividadeComplementar { get; set; }
        public List<CriterioIntegralizacaoVO> CriteriosIntegralizacao { get; set; }
        public string InformacoesAdicionais { get; set; }
        public bool IsNsf { get; set; }
    }
}
