using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class SolicitacaoServicoPessoaAtuacaoListaData : ISMCMappable
    {
        public string NumeroProtocolo { get; set; }

        public string DescricaoProcesso { get; set; }

        public string DescricaoSituacaoEtapa { get; set; }

        public CategoriaSituacao CategoriaSituacao { get; set; }

        public OrigemSolicitacaoServico OrigemSolicitacaoServico { get; set; }

        public DateTime DataSolicitacao { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public bool Cancelado { get; set; }

        public bool Encerrado { get; set; }

        public bool ExisteUsuarioResponsavel { get; set; }

        public bool AbrirCentralPreMatricula { get; set; }

        public bool ProcessoEncerrado { get; set; }

        public List<SolicitacaoServicoPessoaAtuacaoItemListaData> Etapas { get; set; }
    }
}