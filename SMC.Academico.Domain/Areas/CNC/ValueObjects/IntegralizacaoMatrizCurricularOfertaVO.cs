using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class IntegralizacaoMatrizCurricularOfertaVO : ISMCMappable
    {
        public long SeqDivisaoMatrizCurricular { get; set; }

        public short NumeroDivisao { get; set; }

        public string DescricaoDivisao { get; set; }

        public long SeqGrupoCurricular { get; set; }

        public string DescricaoGrupo { get; set; }

        public FormatoConfiguracaoGrupo? FormatoConfiguracaoGrupo { get; set; }

        public short? HoraAulaGrupo { get; set; }

        public short? HoraGrupo { get; set; }

        public short? CreditoGrupo { get; set; }

        public short? ItensGrupo { get; set; }

        public string DescricaoTipoConfiguracaoGrupo { get; set; }

        public long SeqConfiguracaoComponente { get; set; }

        public string SiglaTipoComponenteCurricular { get; set; }

        public string CodigoConfiguracao { get; set; }

        public string DescricaoConfiguracao { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public string Nota { get; set; }

        public string DescricaoEscalaApuracaoItem { get; set; }

        public short? CargaHoraria { get; set; }

        public short? Credito { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public bool PermiteOrigemDispensaMesmoCurriculo { get; set; }
    }
}