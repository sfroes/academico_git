using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class MatrizCurricularOfertaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqCurriculoCursoOferta { get; set; }

        public long SeqMatrizCurricular { get; set; }

        [SMCMapProperty("CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade")]
        public long SeqCursoOfertaLocalidade { get; set; }

        [SMCMapProperty("CursoOfertaLocalidadeTurno.SeqTurno")]
        public long SeqCursoOfertaTurno { get; set; }

        [SMCMapProperty("MatrizCurricular.Descricao")]
        public string DescricaoMatrizCurricular { get; set; }

        public string DescricaoUnidade { get; set; }

        public long SeqEntidadeLocalidade { get; set; }

        public string DescricaoLocalidade { get; set; }

        [SMCMapProperty("CursoOfertaLocalidadeTurno.Seq")]
        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        public DateTime? DataInicioVigencia { get; set; }

        public DateTime? DataFinalVigencia { get; set; }

        public string DescricaoTurno { get; set; }

        public string Codigo { get; set; }

        public short? NumeroPeriodoAtivo { get; set; }

        public List<SMCDatasourceItem> Turnos { get; set; }

        public string CurriculoCodigo { get; set; }

        public string CurriculoDescricao { get; set; }

        public string CurriculoDescricaoComplementar { get; set; }

        public SituacaoMatrizCurricularOferta? HistoricoSituacaoAtual { get; set; }

        public List<MatrizCurricularOfertaExcecaoLocalidadeVO> ExcecoesLocalidade { get; set; }

        public string Descricao { get; set; }

        [SMCMapProperty("MatrizCurricular.QuantidadeMesesSolicitacaoProrrogacao")]
        public short? QuantidadeMesesSolicitacaoProrrogacao { get; set; }

        public string DescricaoComplementarMatrizCurricular { get; set; }

        public List<HistoricoSituacaoMatrizCurricularOfertaVO> HistoricosSituacao { get; set; }

        public string OfertaCompleto
        {
            get
            {
                return string.IsNullOrEmpty(DescricaoComplementarMatrizCurricular) ?
                        $"{DescricaoMatrizCurricular} - {DescricaoLocalidade} - {DescricaoTurno}"
                      : $"{DescricaoMatrizCurricular} - {DescricaoComplementarMatrizCurricular} - {DescricaoLocalidade} - {DescricaoTurno}";
            }
        }
    }
}