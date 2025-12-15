using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class TipoFormacaoEspecificaVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public string Token { get; set; }

        public bool Ativo { get; set; }

        public ClasseTipoFormacao? ClasseTipoFormacao { get; set; }

        public bool? ExigeGrau { get; set; }

        public bool? PermiteEmitirDocumentoConclusao { get; set; }

        public bool? GeraCarimbo { get; set; }

        public bool PermiteTitulacao { get; set; }

        public bool ExibeGrauDescricaoFormacao { get; set; }

        public List<TipoFormacaoEspecificaTipoCursoVO> TiposCurso { get; set; }
    }
}
