using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class ChancelaItemListaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqProcesso { get; set; }

        public string DescricaoProcesso { get; set; }

        public string NomePessoaAtuacao { get; set; }

        public string DescricaoSituacao { get; set; }

        public string DescricaoOfertaCurso { get; set; }

        public string NumeroProtocolo { get; set; }

        public bool Bloqueado { get; set; }

        public List<string> Bloqueios { get; set; }

        public string DataEscalonamento { get; set; }

        public bool EscalonamentoVigente { get; set; }

        public bool EtapaChancelaLiberada { get; set; }

        public bool EtapaPermiteChancela { get; set; }

        public bool PermiteVisualizarPlanoEstudo { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public string TokenEtapaSGF { get; set; }

        public bool Destaque { get; set; }

        public bool ExibirIntegralizacao { get; set; }

        public bool SolicitacaoComAtendimentoIniciado { get; set; }

        public bool UsuarioLogadoEResponsavelAtualPelaSolicitacao { get; set; }
    }
}
