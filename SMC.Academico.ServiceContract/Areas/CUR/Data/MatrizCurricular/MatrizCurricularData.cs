using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class MatrizCurricularData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqCurriculoCursoOferta { get; set; }

        public string Codigo { get; set; }

        public long SeqDivisaoCurricular { get; set; }

        public long SeqModalidade { get; set; }

        public string Descricao { get; set; }

        public string DescricaoComplementar { get; set; }

        public int NumeroSequencial { get; set; }

        public List<MatrizCurricularOfertaData> Ofertas { get; set; }

        public short? QuantidadeMesesPrevistoConclusao { get; set; }

        public short? QuantidadeMesesLimiteConclusao { get; set; }

        public short? QuantidadeMesesSolicitacaoProrrogacao { get; set; }

        public string DescricaoMatrizCurricular { get; set; }

        public string DescricaoComplementarMatrizCurricular { get; set; }
        public long SeqComponenteCurricularPadrao { get; set; }
        // Matriz Ativa        
        public bool Ativo { get; set; }
    }
}