using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        public string Protocolo { get; set; }

        public OrigemSolicitacaoServico OrigemSolicitacaoServico { get; set; }

        [SMCHidden]
        public long SeqProcesso { get; set; }

        public string Processo { get; set; }

        public string HistoricoAluno { get; set; }

        [SMCHidden]
        public long? SeqGrupoEscalonamento { get; set; }

        public string GrupoEscalonamento { get; set; }

        public SituacaoDocumentacao SituacaoDocumentacao { get; set; }

        public string UsuarioResponsavel { get; set; }

        public List<string> UsuariosResponsaveis { get; set; }

        public DateTime? DataPrevistaSolucao { get; set; }

        public DateTime? DataSolucao { get; set; }

        public string SituacaoAtual { get; set; }

        [SMCHidden]
        public long? SeqProcessoEtapaAtual { get; set; }

        [SMCHidden]
        public string EtapaAtual { get; set; }

        [SMCHidden]
        public string EtapaAnterior { get; set; }

        [SMCHidden]
        public string TokenServico { get; set; }

        [SMCHidden]
        public string TokenTipoServico { get; set; }
    }
}