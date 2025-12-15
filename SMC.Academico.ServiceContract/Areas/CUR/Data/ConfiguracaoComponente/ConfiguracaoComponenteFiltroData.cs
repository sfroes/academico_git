using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ConfiguracaoComponenteFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long?[] SeqConfiguracoesComponentes { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string CodigoOuDescricao { get; set; }

        public string DescricaoComplementar { get; set; }

        public bool? Ativo { get; set; }

        public TipoGestaoDivisaoComponente? TipoGestaoDivisaoComponente { get; set; }

        public bool IgnorarFiltroDados { get; set; }

        public List<long> SeqsMatrizCurricular { get; set; }
    }
}