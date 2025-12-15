using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class TurmaDivisoesDetailVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqTurma { get; set; }

        public string Turma { get; set; }

        public string DivisaoDescricao { get; set; }

        public string GrupoNumero { get; set; }

        public long SeqLocalidade { get; set; }

        public string DescricaoLocalidade { get; set; }

        public short? DivisaoVagas { get; set; }

        public string InformacoesAdicionais { get; set; }

        public bool GeraOrientacao { get; set; }

        public short? QuantidadeVagasOcupadas { get; set; }

        public bool HabilitaBtnConfiguracaoGrade { get; set; }

        public string InstructionConfiguracaoGrade { get; set; }

        public bool HabilitaBtnListaFrequencia { get; set; }

        public string InstructionListaFrequencia { get; set; }

        public bool HabilitaBtnLancarFrequencia { get; set; }

        public string InstructionLancarFrequencia { get; set; }

        public bool HabilitaBtnOrientacaoTurma { get; set; }

        public string InstructionOrientacaoTurma { get; set; }

        public long SeqOrigemAvaliacao { get; set; }
    }
}
