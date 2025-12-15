using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.ValueObjects
{
    public class DivisaoTurmaCicloLetivoVO : ISMCMappable
    {
        public long? SeqDivisaoTurma { get; set; }

        public long? SeqTurmaConfiguracaoComponente { get; set; }

        public long? SeqOfertaCurso { get; set; }

        public long? SeqTurma { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }

        public long? SeqDivisaoComponente { get; set; }

        public string DivisaoComponente { get; set; }

        /*[Número da divisão] + "-" + [Tipo divisão componente] + "-" + [Carga horária] + [Label parametrizado]*/

        public long? SeqTipoComponenteCurricular { get; set; }

        public short? NumeroDivisao { get; set; }

        public string TipoDivisaoComponente { get; set; }

        public short? CargaHoraria { get; set; }

        public string LabelParametrizado { get; set; }

        public List<ItemDivisaoComponenteVO> ItensDivisaoComponente { get; set; }
    }
}