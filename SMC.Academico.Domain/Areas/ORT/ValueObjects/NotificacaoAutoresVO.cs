using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class NotificacaoAutoresVO : ISMCMappable
    {
        public string Nome { get; set; }

        public string DescricaoEntidadeResponsavel { get; set; }

        public List<string> ListaDescricaoFormacaoEspecifica { get; set; }

        public string DescricaoFormacaoEspecifica { get; set; }
    }
}
