using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ConfiguracaoComponenteData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoComplementar { get; set; }

        public bool Ativo { get; set; }

        public List<ConfiguracaoComponenteDivisaoData> DivisoesComponente { get; set; }

        public bool ExibirItensOrganizacao { get; set; }

        public bool PermiteAlunoSemNota { get; set; }

        public bool Principal { get; set; }
    }
}