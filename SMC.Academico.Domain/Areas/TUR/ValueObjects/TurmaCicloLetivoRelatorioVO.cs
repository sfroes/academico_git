using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Util;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class TurmaCicloLetivoRelatorioVO : ISMCMappable
    {
        public long? SeqCursoOferta { get; set; }

        public string DescricaoOferta { get; set; }

        public string DescricaoLocalidade { get; set; }

        public string DescricaoTurno { get; set; }

        public long SeqTurma { get; set; }

        public int CodigoTurma { get; set; }

        public short NumeroTurma { get; set; }

        public string CodigoTurmaFormatado { get { return $"{CodigoTurma}.{NumeroTurma}"; } }

        public string DescricaoConfiguracaoTurma { get; set; }

        public string DescricaoTipoTurma { get; set; }

        public short? QuantidadeVagas { get; set; }

        public int? QuantidadeVagasOcupadas { get; set; }

        public string DescricaoComponenteCurricularOrganizacao { get; set; }

        public string DivisaoComponenteDescricaoFormatado
        {
            get
            {
                if (DivisaoComponenteFormato == null)
                    return $"{DivisaoComponenteNumero} - {(string.IsNullOrEmpty(DescricaoComponenteCurricularOrganizacao) ? string.Empty : DescricaoComponenteCurricularOrganizacao + " -")} {DivisaoComponenteDescricao} - {DivisaoComponenteCargaHoraria}";
                else
                    return $"{DivisaoComponenteNumero} - {(string.IsNullOrEmpty(DescricaoComponenteCurricularOrganizacao) ? string.Empty : DescricaoComponenteCurricularOrganizacao + " -")} {DivisaoComponenteDescricao} - {DivisaoComponenteCargaHoraria} {SMCEnumHelper.GetDescription(DivisaoComponenteFormato)}";
            }
        }

        public short DivisaoComponenteNumero { get; set; }

        public short DivisaoComponenteCargaHoraria { get; set; }

        public string DivisaoComponenteDescricao { get; set; }

        public FormatoCargaHoraria? DivisaoComponenteFormato { get; set; }

        public long SeqDivisaoTurma { get; set; }

        public short DivisaoNumeroGrupo { get; set; }

        public string DivisaoLocalidade { get; set; }

        public short DivisaoQuantidadeVagas { get; set; }

        public string DivisaoInformacoes { get; set; }

        public string DivisaoCodificacaoFormatado { get { return $"{CodigoTurmaFormatado}.{DivisaoComponenteNumero}.{DivisaoNumeroGrupo.ToString().PadLeft(3, '0')}"; } }

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
