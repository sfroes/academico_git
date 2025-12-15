using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class SolicitanteVO : ISMCMappable
    {
        public long Seq { get; set; }
        public TipoAtuacao TipoAtuacao { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public string Telefone { get; set; }

        public string EnderecoEletronico { get; set; }

        public bool EAluno { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public int? CodigoAlunoMigracao { get; set; }
    }
}