using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class SolicitacaoDispensaItensDispensadosData : ISMCMappable
    {
        public List<SMCDatasourceItem> CiclosLetivos { get; set; }

        public bool BloquearCicloLetivo { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public long SeqCicloLetivo { get; set; }

        public long SeqCurriculoCursoOferta { get; set; }

        public long SeqInstituicaoNivelResponsavel { get; set; }

        public List<SolicitacaoDispensaComponenteCurricularData> ComponentesCurriculares { get; set; }

        /// <summary>
        /// Componentes curriculares que vieram por carga mas estão fora da matriz do aluno. Devem ser exibidos na tela como mensagem de alerta para tomada de decisão
        /// </summary>
        public List<SMCDatasourceItem> ComponentesCurricularesForaMatriz { get; set; }

        public List<SolicitacaoDispensaGrupoCurricularData> GruposCurriculares { get; set; }

        public long SeqMatrizCurricular { get; set; }

        public decimal CursadosTotalCargaHorariaHoras { get; set; }

        public decimal CursadosTotalCargaHorariaHorasAula { get; set; }

        public decimal CursadosTotalCreditos { get; set; }

        public decimal DispensaTotalCargaHorariaHoras { get; set; }

        public decimal DispensaTotalCargaHorariaHorasAula { get; set; }

        public decimal DispensaTotalCreditos { get; set; }

        public bool ExibirGrupoComponente { get; set; }
    }
}