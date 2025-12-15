using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DadosCursoVO : ISMCMappable
    {
        public List<string> Enfases { get; set; } // Não pode ser definido para NSF.
        public string Modalidade { get; set; } //enum Presencial, EAD 
        public string ModalidadeNSF { get; set; }//enum Presencial, EAD, Semipresencial
        public TituloConferidoVO TituloConferido { get; set; }
        public string GrauConferido { get; set; } //enum Tecnólogo, Bacharelado, Licenciatura, Curso sequencial
        public EnderecoVO EnderecoCurso { get; set; }
        public PoloVO Polo { get; set; }
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
