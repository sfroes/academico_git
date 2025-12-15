using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class MatrizCurricularOfertaData : ISMCMappable
    {
        public long? Seq { get; set; }

        [SMCMapProperty("Seq")]
        public long SeqDivisao { get; set; }

        public long SeqMatrizCurricular { get; set; }

        public long SeqCurriculoCursoOferta { get; set; }

        [SMCMapProperty("CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade")]
        public long SeqCursoOfertaLocalidade { get; set; }

        [SMCMapProperty("CursoOfertaLocalidadeTurno.SeqTurno")]
        public long SeqCursoOfertaTurno { get; set; }

        public DateTime DataInicioVigencia { get; set; }

        public DateTime? DataFinalVigencia { get; set; }

        public string DescricaoUnidade { get; set; }

        public long SeqEntidadeLocalidade { get; set; }

        public string DescricaoMatrizCurricular { get; set; }

        public string DescricaoLocalidade { get; set; }

        public string DescricaoTurno { get; set; }

        public string Codigo { get; set; }

        public short? NumeroPeriodoAtivo { get; set; }

        public List<SMCDatasourceItem> Turnos { get; set; }

        public string CurriculoCodigo { get; set; }

        public string CurriculoDescricao { get; set; }

        public string CurriculoDescricaoComplementar { get; set; }

        public SituacaoMatrizCurricularOferta? HistoricoSituacaoAtual { get; set; }

        public List<MatrizCurricularOfertaExcecaoLocalidadeData> ExcecoesLocalidade { get; set; }

        /// <summary>
        /// Descrição utilizada pelo OfertaMatrizCurricularLookup
        /// </summary>
        public string Descricao { get; set; }

        public string DescricaoComplementarMatrizCurricular { get; set; }

        public string OfertaCompleto { get; set; }
    }
}