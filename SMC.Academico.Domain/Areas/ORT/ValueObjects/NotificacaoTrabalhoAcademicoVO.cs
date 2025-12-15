using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class NotificacaoTrabalhoAcademicoVO : ISMCMappable
    {
        public string NomeAutor { get; set; }

        public string DescricaoTipoTrabalho { get; set; }

        public string Titulo { get; set; }

        public long SeqEntidade { get; set; }

        public bool? PotencialPatente { get; set; }

        public bool? PotencialRegistroSoftware { get; set; }

        public bool? PotencialNegocio { get; set; }

        public List<NotificacaoAutoresVO> Autores { get; set; }
    }
}
