using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoBloqueioItemData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacaoBloqueio { get; set; }

        public string Descricao { get; set; }

        public SituacaoBloqueio SituacaoBloqueio { get; set; }

        public string CodigoIntegracaoSistemaOrigem { get; set; }

        public DateTime? DataInclusao { get; set; }

        public string UsuarioInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string UsuarioAlteracao { get; set; }

        public bool BloquearSituacao { get; set; }

        public TipoDesbloqueio TipoDesbloqueioDetalhe { get; set; }
    }
}