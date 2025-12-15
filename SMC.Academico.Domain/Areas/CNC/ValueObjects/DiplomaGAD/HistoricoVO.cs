using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class HistoricoVO : ISMCMappable
    {
        public string CodigoCurriculo { get; set; } // Propriedade obrigatória para informar o código do currículo ao qual o aluno está vinculado no momento da emissão do Histórico Escolar
        public DateTimeOffset DataEmissaoHistorico { get; set; }
        public string HoraEmissaoHistorico { get; set; } // Hora de emissão do histórico escolar. A hora de emissão será definida no momento da emissão de forma automática. Caso seja informada uma hora, sobrescreverá a hora gerada automaticamente.
        public CargaHorariaVO CargaHorariaCursoIntegralizadaV2 { get; set; }
        public CargaHorariaVO CargaHorariaCurso { get; set; }
        public SituacaoDiscenteVO SituacaoAtualDiscente { get; set; }
        public string NomeParaAreas { get; set; } // Propriedade opcional e deve conter o nome usado pelo Curso para se referir às Áreas de Especialização, Linhas de Formação, ou Competências. Caso omitido, assume-se que o termo ‘Área de Especialização’
        public List<AreaVO> Areas { get; set; } //Propriedade opcional e descreve as Áreas de Especialização integralizada pelo aluno durante o curso, caso o projeto pedagógico do Curso permita
        public List<ParticipacaoEnadeVO> ParticipacoesEnade { get; set; } //Em caso de segunda via, pode ser utilizado o valor null para informar que informações de Enade não se aplicam ao aluno ou lista vazia para informar que aquele aluno não tem informações de participação no Enade.
        public List<ElementoHistoricoVO> ElementosHistorico { get; set; }
        public IngressoVO Ingresso { get; set; }
        public string InformacoesAdicionais { get; set; }


        [Obsolete("OBSOLETO a partir da v1.04.1. Utilizar ParticipacoesEnade")]
        public DateTimeOffset? DataProvaEnade { get; set; }

        [Obsolete("À partir da v1.03 usar CargaHorariaCursoIntegralizadaV2")]
        public int? CargaHorariaCursoIntegralizada { get; set; }


        public string SituacaoAluno { get; set; } //enum Aprovado 
        public SituacaoAlunoEnadeVO SituacaoEnade { get; set; }

        [Obsolete("OBSOLETO a partir da v1.04.1. Utilizar ElementosHistorico")]
        public List<MatrizCurricularVO> MatrizCurricular { get; set; }
    }
}
