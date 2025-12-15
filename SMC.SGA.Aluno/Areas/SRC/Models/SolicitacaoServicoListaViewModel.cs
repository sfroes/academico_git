using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.SRC.Models
{
    public class SolicitacaoServicoListaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCKey]
        public long SeqSolicitacaoServico { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoEtapa { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public string DescricaoSituacaoEtapa { get; set; }

        [SMCHidden]
        public CategoriaSituacao CategoriaSituacao { get; set; }

        [SMCHidden]
        public OrigemSolicitacaoServico OrigemSolicitacaoServico { get; set; }

        [SMCHidden]
        public bool Cancelado { get; set; }

        [SMCHidden]
        public bool Encerrado { get; set; }

        [SMCHidden]
        public bool ExisteUsuarioResponsavel { get; set; }

        [SMCSortable(allow: true, isDefault: false)]
        public string NumeroProtocolo { get; set; }

        [SMCSortable(allow: true, isDefault: false, sortFieldName: "ConfiguracaoProcesso.Processo.Descricao")]
        public string DescricaoProcesso { get; set; }

        public string DescricaoConcatenada
        {
            get
            {
                return $"{SMCEnumHelper.GetDescription(CategoriaSituacao)} | {DescricaoSituacaoEtapa}";
            }
        }

        [SMCSortable(allow: true, isDefault: true, sort: SMCSortDirection.Descending)]
        public DateTime DataSolicitacao { get; set; }

        [SMCHidden]
        public List<SolicitacaoServicoItemListaViewModel> Etapas { get; set; }

        [SMCHidden]
        public bool AbrirCentralPreMatricula { get; set; }

        [SMCHidden]
        public bool ProcessoEncerrado { get; set; }
    }
}