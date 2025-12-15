using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ConfiguracaoComponenteLookupListaData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoComplementar { get; set; }

        public bool Ativo { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public long SeqNivelEnsino { get; set; }

        public short? ComponenteCurricularCargaHoraria { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public short? ComponenteCurricularCredito { get; set; }

        public IEnumerable<string> ComponenteCurricularEntidadesSigla { get; set; }

        public string ConfiguracaoComponenteDescricaoCompleta { get; set; }

        public List<ConfiguracaoComponenteDivisaoListarData> DivisoesComponente { get; set; }
    }
}