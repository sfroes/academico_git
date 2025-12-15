using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CampanhaCopiaProcessoSeletivoListaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqProcessoGpi { get; set; }

        public string Descricao { get; set; }

        public string TipoProcessoSeletivo { get; set; }

        public bool? CopiarProcessoGPI { get; set; }

        public string DescricaoProcessoGPI { get; set; }

        public long? SeqCicloLetivoReferenciaProcessoGPI { get; set; }

        public DateTime? DataInicioInscricaoProcessoGPI { get; set; }
        public DateTime? DataFimInscricaoProcessoGPI { get; set; }
        public bool? CopiarNotificacoesGPI { get; set; }

        public List<CampanhaCopiaEtapaProcessoGPIItemVO> EtapasGPI { get; set; }

        public List<CampanhaCopiaConvocacaoProcessoSeletivoItemVO> Convocacoes { get; set; }
    }
}