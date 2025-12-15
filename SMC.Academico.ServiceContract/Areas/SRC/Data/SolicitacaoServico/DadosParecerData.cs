using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class DadosParecerData : ISMCMappable
    {
        public string DescricaoProcesso { get; set; }

        public string NomeSocial { get; set; }

        public string Nome { get; set; }

        public string Protocolo { get; set; }

        public string OrientacoesDeferimento { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public bool ValidarSituacaoFutura { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        public DateTime DataSolicitacao { get; set; }

    }
}