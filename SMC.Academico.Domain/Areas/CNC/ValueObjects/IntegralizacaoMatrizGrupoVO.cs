using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class IntegralizacaoMatrizGrupoVO : ISMCMappable
    {
        public string DescricaoDivisao { get; set; }

        public long SeqGrupoCurricular { get; set; }

        public string DescricaoGrupo { get; set; }

        public long? SeqGrupoCurricularSuperior { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public decimal? PercentualConclusaoGrupo { get; set; }

        public string ConfiguracaoGrupo
        {
            get
            {
                string retorno = DescricaoTipoConfiguracaoGrupo;

                switch (FormatoConfiguracaoGrupo)
                {
                    case Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.CargaHoraria:
                        if (HoraGrupo.HasValue)
                            retorno += $": {HoraGrupo} Hora(s)";
                        else
                            retorno += $": {HoraAulaGrupo} Hora-aula(s)";
                        break;
                    case Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.Credito:
                        if (CreditoGrupo.HasValue)
                            retorno += $": {CreditoGrupo} Crédito(s)";
                        break;
                    case Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.Itens:
                        if (ItensGrupo.HasValue)
                            retorno += $": {ItensGrupo} Item(s)";
                        break;
                    default:
                        break;
                }

                return retorno;
            }
        }

        public FormatoConfiguracaoGrupo? FormatoConfiguracaoGrupo { get; set; }

        public short? HoraAulaGrupo { get; set; }

        public short? HoraGrupo { get; set; }

        public short? CreditoGrupo { get; set; }

        public short? ItensGrupo { get; set; }

        public string DescricaoTipoConfiguracaoGrupo { get; set; }

        public string TokenTipoConfiguracaoGrupo { get; set; }

        public short OrdenacaoTipoConfiguracaoGrupo
        {
            get
            {               
                switch (TokenTipoConfiguracaoGrupo)
                {
                    case TOKEN_TIPO_CONFIGURACAO_GRUPO_CURRICULAR.TKN_TODOS_OBRIGATORIOS:
                        return 1;
                    case TOKEN_TIPO_CONFIGURACAO_GRUPO_CURRICULAR.TKN_MAXIMO_A_CURSAR:
                        return 2;
                    case TOKEN_TIPO_CONFIGURACAO_GRUPO_CURRICULAR.TKN_MINIMO_A_CURSAR:
                        return 3;
                    case TOKEN_TIPO_CONFIGURACAO_GRUPO_CURRICULAR.TKN_NENHUM_OBRIGATORIO:
                        return 4;
                    default:
                        return 5;
                }
            }
        }

        public List<IntegralizacaoMatrizGrupoVO> GruposFilhos { get; set; }

        public List<IntegralizacaoMatrizConfiguracaoVO> Configuracoes { get; set; }

        public List<GrupoCurricularInformacaoVO> Beneficios { get; set; }

        public List<GrupoCurricularInformacaoVO> CondicoesObrigatorias { get; set; }

        public List<GrupoCurricularInformacaoFormacaoVO> FormacoesEspecificas { get; set; }

        public List<string> GruposDivisoes { get; set; }

        public string MensagemDispensaGrupo { get; set; }

        public List<long?> SeqSolicitacaoServico { get; set; }

        public List<string> ProtocoloDispensaGrupo { get; set; }
    }
}