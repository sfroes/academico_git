using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class PessoaAtuacaoBeneficioBeneficioHistoricoSituacaoData : ISMCMappable
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
