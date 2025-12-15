using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    /// <summary>
    /// Filtro de Configuração de Avaliação.
    /// </summary>
    public class ConfiguracaoAvaliacaoPpaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        /// <summary>
        /// Ciclo letivo    
        /// </summary>
        public long? SeqCicloLetivo { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public string Descricao { get; set; }

        public TipoAvaliacaoPpa? TipoAvaliacaoPpa { get; set; }

        public int? CodigoAvaliacaoPpa { get; set; }
    }
}
