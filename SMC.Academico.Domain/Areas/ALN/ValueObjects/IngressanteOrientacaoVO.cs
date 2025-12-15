using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class IngressanteOrientacaoVO : ISMCMappable
    {
        public List<SMCDatasourceItem> Colaboradores { get; set; }

        public List<SMCDatasourceItem> InstituicoesExternas { get; set; }

        public List<SMCDatasourceItem> TiposParticipacaoOrientacao { get; set; }

        public long Seq { get; set; }

        public long SeqColaborador { get; set; }

        public TipoParticipacaoOrientacao TipoParticipacaoOrientacao { get; set; }

        public long SeqInstituicaoExterna { get; set; }

        public string NomeColaborador { get; set; }

        public string DescricaoTipoParticipacaoOrientacao { get; set; }

        public string NomeInstituicaoExterna { get; set; }

        public string ColaboradorParticipacaoConfirmacao { get; set; }
    }
}