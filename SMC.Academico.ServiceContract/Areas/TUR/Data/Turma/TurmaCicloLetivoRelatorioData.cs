using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaCicloLetivoRelatorioData : ISMCMappable
    {
        public long? SeqCursoOferta { get; set; }

        public string DescricaoOferta { get; set; }

        public string DescricaoLocalidade { get; set; }

        public string DescricaoTurno { get; set; }

        public long SeqTurma { get; set; }
               
        public string CodigoTurmaFormatado { get; set; }

        public string DescricaoConfiguracaoTurma { get; set; }

        public string DescricaoTipoTurma { get; set; }

        public short? QuantidadeVagas { get; set; }

        public short? QuantidadeVagasOcupadas { get; set; }

        public string DivisaoComponenteDescricaoFormatado { get; set; }

        public short DivisaoComponenteNumero { get; set; }

        public short DivisaoComponenteCargaHoraria { get; set; }

        public string DivisaoComponenteDescricao { get; set; }

        public FormatoCargaHoraria DivisaoComponenteFormato { get; set; }

        public string DivisaoComponenteFormatoDescricao { get; set; }

        public long SeqDivisaoTurma { get; set; }

        public short DivisaoNumeroGrupo { get; set; }

        public string DivisaoLocalidade { get; set; }

        public short DivisaoQuantidadeVagas { get; set; }

        public string DivisaoInformacoes { get; set; }

        public string DivisaoCodificacaoFormatado { get; set; }

        //Turma Colaborador
        public long? SeqPessoaColaborador { get; set; }

        public string NomeColaborador { get; set; }

        public string NomeSocialColaborador { get; set; }

        //Divisao Colaborador
        public long? SeqPessoaDivisaoColaborador { get; set; }

        public string NomeDivisaoColaborador { get; set; }

        public string NomeSocialDivisaoColaborador { get; set; }
    }
}
