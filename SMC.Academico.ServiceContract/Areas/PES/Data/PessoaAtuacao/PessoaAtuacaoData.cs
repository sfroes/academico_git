using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Data;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqPessoa { get; set; }

        public long SeqPessoaDadosPessoais { get; set; }

        public long? SeqUsuarioSAS { get; set; }

        public SMCUploadFile ArquivoFoto { get; set; }

        public long? SeqArquivoFoto { get; set; }

        [SMCMapProperty("DadosPessoais.Nome")]
        public string Nome { get; set; }

        [SMCMapProperty("DadosPessoais.NomeSocial")]
        public string NomeSocial { get; set; }

        public string NomeSocialReadOnly { get; set; }

        [SMCMapProperty("Pessoa.DataNascimento")]
        public DateTime? DataNascimento { get; set; }

        public DateTime? DataNascimentoReadOnly { get; set; }

        [SMCMapProperty("Pessoa.Falecido")]
        public bool Falecido { get; set; }

        public EstadoCivil? EstadoCivil { get; set; }

        [SMCMapProperty("DadosPessoais.Sexo")]
        public Sexo Sexo { get; set; }

        public RacaCor? RacaCor { get; set; }

        [SMCMapProperty("Pessoa.Cpf")]
        public string Cpf { get; set; }

        public string CpfReadOnly { get; set; }

        [SMCMapProperty("Pessoa.NumeroPassaporte")]
        public string NumeroPassaporte { get; set; }

        [SMCMapProperty("Pessoa.CodigoPaisEmissaoPassaporte")]
        public int? CodigoPaisEmissaoPassaporte { get; set; }

        [SMCMapProperty("Pessoa.DataValidadePassaporte")]
        public DateTime? DataValidadePassaporte { get; set; }

        public TipoNacionalidade TipoNacionalidade { get; set; }

        public TipoNacionalidade TipoNacionalidadeReadOnly { get; set; }

        public int CodigoPaisNacionalidade { get; set; }

        public int CodigoPaisNacionalidadeReadOnly { get; set; }

        public string UfNaturalidade { get; set; }

        public int? CodigoCidadeNaturalidade { get; set; }

        public string DescricaoNaturalidadeEstrangeira { get; set; }

        public string NumeroIdentidade { get; set; }

        public string OrgaoEmissorIdentidade { get; set; }

        public string UfIdentidade { get; set; }

        public DateTime? DataExpedicaoIdentidade { get; set; }

        public string NumeroRegistroCnh { get; set; }

        public CategoriaCnh? CategoriaCnh { get; set; }

        public DateTime? DataEmissaoCnh { get; set; }

        public DateTime? DataVencimentoCnh { get; set; }

        public string NumeroTituloEleitor { get; set; }

        public string NumeroZonaTituloEleitor { get; set; }

        public string NumeroSecaoTituloEleitor { get; set; }

        public string UfTituloEleitor { get; set; }

        public TipoPisPasep? TipoPisPasep { get; set; }

        public string NumeroPisPasep { get; set; }

        public DateTime? DataPisPasep { get; set; }

        public string NumeroDocumentoMilitar { get; set; }

        public string CsmDocumentoMilitar { get; set; }

        public TipoDocumentoMilitar? TipoDocumentoMilitar { get; set; }

        public string UfDocumentoMilitar { get; set; }

        public TipoRegistroProfissional? TipoRegistroProfissional { get; set; }

        public string NumeroRegistroProfissional { get; set; }

        public bool NecessidadeEspecial { get; set; }

        public TipoNecessidadeEspecial? TipoNecessidadeEspecial { get; set; }

        public List<PessoaFiliacaoData> Filiacao { get; set; }

        public List<PessoaFiliacaoReadOnlyData> FiliacaoReadOnly { get; set; }

        public List<PessoaAtuacaoEnderecoData> Enderecos { get; set; }

        public List<PessoaEnderecoEletronicoData> EnderecosEletronicos { get; set; }

        public List<PessoaTelefoneData> Telefones { get; set; }

        public string Descricao { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        public bool PermitirAlterarDadosPessoaAtuacao { get; set; }

        public bool PermitirAlterarDadosPessoaAtuacaoNomeSocial { get; set; }

        public string NumeroIdentidadeEstrangeira { get; set; }
    }
}