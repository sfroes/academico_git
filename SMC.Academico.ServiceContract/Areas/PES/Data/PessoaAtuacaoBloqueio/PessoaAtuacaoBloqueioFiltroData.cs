using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoBloqueioFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public List<long> SeqTipoBloqueio { get; set; }
        public List<long> SeqMotivoBloqueio { get; set; }
        public SituacaoBloqueio? SituacaoBloqueio { get; set; }
        public TipoDesbloqueio? TipoDesbloqueio { get; set; }
        public long? SeqPessoaAtuacao { get; set; }
        public DateTime? DataBloqueioInicio { get; set; }
        public DateTime? DataBloqueioFim { get; set; }
        public long? SeqPessoa { get; set; }
    }
}