using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class IntegralizacaoMatrizGrupoData : ISMCMappable
    {
        public long SeqGrupoCurricular { get; set; }

        public long? SeqGrupoCurricularSuperior { get; set; }

        public string DescricaoGrupo { get; set; }

        public string ConfiguracaoGrupo { get; set; }

        public decimal? PercentualConclusaoGrupo { get; set; }

        public List<IntegralizacaoMatrizGrupoData> GruposFilhos { get; set; }

        public List<IntegralizacaoMatrizConfiguracaoData> Configuracoes { get; set; }

        public List<GrupoCurricularInformacaoData> Beneficios { get; set; }

        public List<GrupoCurricularInformacaoData> CondicoesObrigatorias { get; set; }
               
        public List<GrupoCurricularInformacaoFormacaoData> FormacoesEspecificas { get; set; }

        public List<string> GruposDivisoes { get; set; }
               
        public string MensagemDispensaGrupo { get; set; }

        public List<long?> SeqSolicitacaoServico { get; set; }

        public List<string> ProtocoloDispensaGrupo { get; set; }
    }
}
