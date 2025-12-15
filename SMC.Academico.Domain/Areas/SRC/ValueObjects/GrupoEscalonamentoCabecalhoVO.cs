using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class GrupoEscalonamentoCabecalhoVO : ISMCMappable
    {
        public long SeqProcesso { get; set; }

        public string DescricaoProcesso { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public DateTime? DataEncerramento { get; set; }

        public long SeqGrupoEscalonamento { get; set; }

        public string DescricaoGrupoEscalonamento { get; set; }

        public bool Ativo { get; set; }

        public bool ProcessoEncerrado { get; set; }

        public bool ProcessoExpirado { get; set; }

        public bool PossuiParcelas { get; set; }

        public bool TodasEtapasEncerradas { get; set; }

        public bool NaoPermiteAssociarSolicitacaoEtapasExpiradas { get; set; }

        public int QuantidadeSolicitacoes { get; set; }

        public PermiteReabrirSolicitacao PermiteReabrirSolicitacao { get; set; }

        public bool? PermitirNotificacao { get; set; }

        public bool PossuiParcelasVencimentoMenorEscalonamento { get; set; }

        public bool HabilitaBtnComPermissaoManutencaoProcesso { get; set; }
    }
}