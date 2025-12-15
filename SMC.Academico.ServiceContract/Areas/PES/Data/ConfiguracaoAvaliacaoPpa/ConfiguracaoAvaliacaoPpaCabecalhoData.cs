using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
    {
    /// <summary>
    /// Configuração de Avaliação.
    /// </summary>
    public class ConfiguracaoAvaliacaoPpaCabecalhoData : ISMCMappable
    {
        #region Primitive Properties
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public TipoAvaliacaoPpa? TipoAvaliacaoPpa { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public Nullable<int> CodigoAvaliacaoPpa { get; set; }


        #endregion Primitive Properties

        public Entidade EntidadeResponsavel { get; set; }

    }
}
