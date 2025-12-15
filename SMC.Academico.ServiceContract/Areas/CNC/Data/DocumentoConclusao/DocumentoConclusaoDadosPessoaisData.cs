using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data.DocumentoConclusao
{
    public class DocumentoConclusaoDadosPessoaisData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoa { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public Sexo Sexo { get; set; }

        public DateTime DataNascimento { get; set; }

        public DateTime DataInclusao { get; set; }

        public string UsuarioInclusao { get; set; }

        public string NumeroIdentidade { get; set; }

        public string OrgaoEmissorIdentidade { get; set; }

        public string UfIdentidade { get; set; }

        public DateTime? DataExpedicaoIdentidade { get; set; }

        public TipoNacionalidade TipoNacionalidade { get; set; }

        public string UfNaturalidade { get; set; }

        public int? CodigoCidadeNaturalidade { get; set; }

        public string DescricaoNaturalidadeEstrangeira { get; set; }

        public int CodigoPaisNacionalidade { get; set; }

        public string Nacionalidade { get; set; }

        public string Naturalidade { get; set; }
    }
}
