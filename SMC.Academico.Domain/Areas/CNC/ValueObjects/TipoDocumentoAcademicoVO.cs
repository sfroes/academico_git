using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class TipoDocumentoAcademicoVO : ISMCMappable, ISMCSeq
    {  
        public long Seq { get; set; }      
        
        public long SeqInstituicaoEnsino { get; set; }

        public string Token { get; set; }

        public GrupoDocumentoAcademico GrupoDocumentoAcademico { get; set; }

        public string Descricao { get; set; }        

        public List<TipoDocumentoAcademicoServicoVO> Servicos { get; set; }

        public List<TipoDocumentoAcademicoTagVO> Tags { get; set; }
    }
}
