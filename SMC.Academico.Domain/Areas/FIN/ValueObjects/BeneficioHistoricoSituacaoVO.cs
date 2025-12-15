using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class BeneficioHistoricoSituacaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacaoBeneficio { get; set; }

        public SituacaoChancelaBeneficio SituacaoChancelaBeneficio { get; set; }

        public DateTime DataInicioSituacao { get; set; }

        public string Observacao { get; set; }

        public string UsuarioInclusao { get; set; }

        public bool Atual { get; set; }
    }
}
