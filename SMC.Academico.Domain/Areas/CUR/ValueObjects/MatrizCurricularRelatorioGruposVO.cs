using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class MatrizCurricularRelatorioGruposVO : ISMCMappable
    {
        public long SeqMatrizCurricular { get; set; }

        public short? NumeroDivisaoCurricular { get; set; }

        public string DescricaoDivisaoCurricular { get; set; }

        public string CodigoConfiguracaoComponente { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public string DescricaoComplementarConfiguracaoComponente { get; set; }

        public short? CargaHorariaConfiguracaoComponente { get; set; }

        public short? CreditosConfiguracaoComponente { get; set; }

        public short VagasConfiguracaoComponente { get; set; }

        public long SeqTipoDivisaoComponente { get; set; }

        public string DescricaoTipoDivisaoComponente { get; set; }

        public long? SeqModalidadeDivisaoComponente { get; set; }

        public string ModalidadeDivisaoComponente { get; set; }

        public short CargaHorariaDivisaoComponente { get; set; }

        public short ProfessoresDivisaoComponente { get; set; }

        public short GruposDivisaoComponente { get; set; }

        public long? GruposSeq { get; set; }

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

        public string GruposConfiguracaoCompleto
        {
            get
            {
                string formatoTipo = string.Empty;
                switch (GruposFormatoConfiguracao)
                {
                    case FormatoConfiguracaoGrupo.CargaHoraria:
                        if (GruposQuantidadeHorariaAula.HasValue)
                            formatoTipo = $" : {GruposQuantidadeHorariaAula} horas-aulas";
                        else
                            formatoTipo = $" : {GruposQuantidadeHorariaRelogio} horas";
                        break;
                    case FormatoConfiguracaoGrupo.Credito:
                        formatoTipo = $" : {GruposQuantidadeCredito} créditos";
                        break;
                    case FormatoConfiguracaoGrupo.Itens:
                        formatoTipo = $" : {GruposQuantidadeItens} itens";
                        break;
                    default:
                        break;
                }

                return $"{GruposTipoConfiguracaoDescricao}{formatoTipo}.";
            }
        }

        public bool Configuracao { get; set; }

        public string FormatoCargaHoraria { get; set; }

        public long OrdenacaoRegistro { get; set; }
        
        public long? GruposSeqPai { get; set; }
    }
}
