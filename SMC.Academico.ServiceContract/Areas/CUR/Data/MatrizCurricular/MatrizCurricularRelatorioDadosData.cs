using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class MatrizCurricularRelatorioDadosData : ISMCMappable
    {
        public long SeqMatrizCurricular { get; set; }

        public short NumeroDivisaoCurricular { get; set; }

        public string DescricaoDivisaoCurricular { get; set; }

        public long SeqConfiguracaoComponente { get; set; }

        public string CodigoConfiguracaoComponente { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public string DescricaoComplementarConfiguracaoComponente { get; set; }

        public short? CargaHorariaConfiguracaoComponente { get; set; }

        public short? CreditosConfiguracaoComponente { get; set; }

        public short VagasConfiguracaoComponente { get; set; }

        public long SeqTipoDivisaoComponente { get; set; }

        public string DescricaoTipoDivisaoComponente { get; set; }

        public long SeqModalidadeDivisaoComponente { get; set; }

        public string ModalidadeDivisaoComponente { get; set; }

        public short CargaHorariaDivisaoComponente { get; set; }       

        public short ProfessoresDivisaoComponente { get; set; }

        public short GruposDivisaoComponente { get; set; }

        public long GruposSeq { get; set; }

        public string GruposDescricao { get; set; }

        public short? GruposCargaHoraria { get; set; }

        public short? GruposCredito { get; set; }

        public long GruposTipoConfiguracao { get; set; }

        public string GruposTipoConfiguracaoDescricao { get; set; }

        public short? GruposQuantidadeHorariaAula { get; set; }

        public short? GruposQuantidadeHorariaRelogio { get; set; }

        public short? GruposQuantidadeCredito { get; set; }

        public short? GruposQuantidadeItens { get; set; }

        public FormatoConfiguracaoGrupo? GruposFormatoConfiguracao { get; set; }

        public string GruposConfiguracaoCompleto { get; set; }

        public bool Configuracao { get; set; }

        public string FormatoCargaHoraria { get; set; }
    }
}