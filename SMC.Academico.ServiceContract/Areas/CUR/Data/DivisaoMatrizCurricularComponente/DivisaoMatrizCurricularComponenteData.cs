using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class DivisaoMatrizCurricularComponenteData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqMatrizCurricular { get; set; }

        public long? SeqMatrizCurricularOferta { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public long SeqCurriculoCursoOferta { get; set; }

        public long SeqGrupoCurricularComponente { get; set; }

        /// <summary>
        /// Define se o componente do grupo curricular compontente desta divisão exige subistituto
        /// </summary>
        public bool GrupoCurricularComponenteExigeAssunto { get; set; }

        public bool QuantidadeVagasObrigatoria { get; set; }

        public bool CriterioAprovacaoObrigatorio { get; set; }

        public bool GrupoCurricularDivisaoCadastrada { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }

        public long? SeqDivisaoMatrizCurricular { get; set; }

        public short? QuantidadeVagas { get; set; }

        public long? SeqCriterioAprovacao { get; set; }

        public string CriterioNotaMaxima { get; set; }

        public string CriterioPercentualNotaAprovado { get; set; }

        public string CriterioPercentualFrequenciaAprovado { get; set; }

        public long? SeqEscalaApuracao { get; set; }

        public string CriterioDescricaoEscalaApuracao { get; set; }

        public List<SMCDatasourceItem> DivisoesComponentes { get; set; }

        public List<MatrizCurricularDivisaoComponenteData> DivisoesComponente { get; set; }

        public List<SMCDatasourceItem> Turnos { get; set; }

        public List<long> TurnosExcecao { get; set; }

        public List<ComponenteCurricularListaData> ComponentesCurricularSubstitutos { get; set; }

        public short DivisaoMatrizCurricularNumero { get; set; }

        public string DivisaoMatrizCurricularDescricao { get; set; }

        public bool ApuracaoFrequencia { get; set; }
    }
}