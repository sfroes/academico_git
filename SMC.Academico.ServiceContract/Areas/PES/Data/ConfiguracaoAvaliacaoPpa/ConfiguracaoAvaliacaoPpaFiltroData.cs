using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    /// <summary>
    /// Filtro de Configuração de Avaliação.
    /// </summary>
    public class ConfiguracaoAvaliacaoPpaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public string Descricao { get; set; }

        public List<TipoAvaliacaoPpa> ListaTipoAvaliacaoPpa { get; set; }

        public int? CodigoAvaliacaoPpa { get; set; }

    }
}
