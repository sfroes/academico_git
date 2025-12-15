using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CampanhaCopiaProcessoSeletivoListaData : ISMCMappable
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

        public List<CampanhaCopiaEtapaProcessoGPIItemData> EtapasGPI { get; set; }

        public List<CampanhaCopiaConvocacaoProcessoSeletivoItemData> Convocacoes { get; set; }
    }
}