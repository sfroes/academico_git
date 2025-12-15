using System.Collections.Generic;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class DestinatarioEnvioNotificacaoVO : ISMCMappable
    {
        public string Nome { get; set; }
        public string NomeSocial { get; set; }
        public string CursoOferta { get; set; }
        public List<string> Emails { get; set; }
        public long SeqInstituicaoEnsino { get; set; }

    }
}
