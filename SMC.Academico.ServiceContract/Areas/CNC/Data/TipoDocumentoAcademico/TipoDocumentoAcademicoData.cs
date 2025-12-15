using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class TipoDocumentoAcademicoData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public string Descricao { get; set; }

        public string Token { get; set; }

        public GrupoDocumentoAcademico GrupoDocumentoAcademico { get; set; }

        public List<TipoDocumentoAcademicoServicoData> Servicos { get; set; }

        public List<TipoDocumentoAcademicoTagData> Tags { get; set; }
    }
}
