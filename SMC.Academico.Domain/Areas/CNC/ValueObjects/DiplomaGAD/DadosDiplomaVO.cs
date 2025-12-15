using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DadosDiplomaVO : ISMCMappable
    {
        public DiplomadoVO Diplomado { get; set; }
        public DateTimeOffset? DataConclusao { get; set; }
        public DadosCursoVO DadosCurso { get; set; }
        public List<InformacaoAssinanteVO> Assinantes { get; set; }
        public IesEmissoraVO IesEmissora { get; set; }
        public List<string> DeclaracoesAcercaProcesso { get; set; }// Utilizada quando o diploma for do tipo Decisão Judicial Propriedade opcional para o preenchimento de declarações sobre o processo judicial, circunstâncias de emissão, ausência de informações, ou qualquer declaração que julgar pertinente.
        public DadosIesOriginalCursoPtaVO DadosIesOriginalCursoPTA { get; set; }
    }
}
