using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class SolicitacaoServicoPessoaAtuacaoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public string NumeroProtocolo { get; set; }

        public long? SeqTipoServico { get; set; }

        public long? SeqServico { get; set; }

        public CategoriaSituacao? CategoriaSituacao { get; set; }

        public DateTime? DataSolicitacao { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public List<OrigemSolicitacaoServico> OrigensSolicitacaoServico { get; set; }
    }
}