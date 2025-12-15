using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DadosMinimosCursoVO : ISMCMappable
    {
        public string NomeCurso { get; set; }
        public int? CodigoCursoEMEC { get; set; }
        public InformacoesTramitacaoEmecVO InformacoesTramitacaoEmec { get; set; }
        public List<HabilitacaoVO> Habilitacoes { get; set; }
        public AtoRegulatorioVO Autorizacao { get; set; }
        public AtoRegulatorioVO Reconhecimento { get; set; }
        public AtoRegulatorioVO RenovacaoReconhecimento { get; set; }


        [Obsolete("A partir da v1.03 é possível especificar mais de uma habilitação. Usar NomesHabilitacao")]
        public string NomeHabilitacao { get; set; }

        [Obsolete("A partir da v1.04 foi criado um tipo específico para habilitação. Usar Habilitacoes")]
        public List<string> NomesHabilitacao { get; set; }
    }
}