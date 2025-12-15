using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Financeiro.Common.Areas.GRA.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class IntegralizacaoMatrizCurricularOfertaCabecalhoData : ISMCMappable
    {
        public long? SeqAluno { get; set; }

        public long? SeqIngressante { get; set; }

        public long RA { get; set; }

        public int? CodigoMigracao { get; set; }

        public string Nome { get; set; }

        public string Situacao { get; set; }

        public string Vinculo { get; set; }

        public string DescricaoFormaIngresso { get; set; }

        public bool ExibirOfertaMatriz { get; set; }

        public string OfertaMatriz { get; set; }

        public string SituacaoOfertaMatriz { get; set; }

        public string OfertaCurso { get; set; }

        public string Localidade { get; set; }

        public string Turno { get; set; }

        public SituacaoIngressante SituacaoIngressante { get; set; }

        public string OfertaCampanha { get; set; }

        public bool ExibirDisciplinaIsolada { get; set; }

        public string VinculoDisciplinaIsolada { get; set; }

        public List<IntegralizacaoTipoComponenteCurricularData> TiposComponentesCurriculares { get; set; }

        [SMCMapProperty("DadosConclusaoCursoAluno.PercentualConclusaoGeral")]
        public decimal? PercentualConclusaoGeral { get; set; }
    }
}
