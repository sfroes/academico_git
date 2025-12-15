using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class SolicitacaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Protocolo { get; set; }

        public OrigemSolicitacaoServico OrigemSolicitacaoServico { get; set; }

        public long SeqProcesso { get; set; }
        public string Processo { get; set; }

        public string HistoricoAluno { get; set; }

        public long? SeqGrupoEscalonamento { get; set; }

        public string GrupoEscalonamento { get; set; }

        public SituacaoDocumentacao SituacaoDocumentacao { get; set; }

        public string UsuarioResponsavel { get; set; }

        public List<string> UsuariosResponsaveis { get; set; }

        public DateTime? DataPrevistaSolucao { get; set; }

        public DateTime? DataSolucao { get; set; }

        public long? SeqProcessoEtapaAtual { get; set; }

        public string SituacaoAtual { get; set; }

        public string EtapaAtual { get; set; }

        public string EtapaAnterior { get; set; }

        public string TokenServico { get; set; }

        public string TokenTipoServico { get; set; }
    }
}