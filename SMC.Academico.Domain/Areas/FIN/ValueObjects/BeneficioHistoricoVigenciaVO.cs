using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class BeneficioHistoricoVigenciaVO : ISMCMappable
    {
        public  long Seq { get; set; }

        public long SeqPessoaAtuacaoBeneficio { get; set; }

        public DateTime DataInicioVigencia { get; set; }

        public DateTime? DataFimVigencia { get; set; }

        public string Observacao { get; set; }

        public long? SeqMotivoAlteracaoBeneficio { get; set; }

        public bool Atual { get; set; }

        public string DescricaoMotivoAlteracaoBeneficio { get; set; }

        public DateTime? DataInclusao { get; set; }

        public string UsuarioInclusao { get; set; }
    }
}
