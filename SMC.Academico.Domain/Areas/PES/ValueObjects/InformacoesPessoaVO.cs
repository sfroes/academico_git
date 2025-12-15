using SMC.Academico.Common.Areas.PES.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class InformacoesPessoaVO : ISMCMappable
    {
        #region [ Primitive Properties ]

        public long Seq { get; set; }

        [SMCMapProperty("Pessoa.SeqInstituicaoEnsino")]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCMapProperty("Pessoa.SeqUsuarioSAS")]
        public long? SeqUsuarioSAS { get; set; }

        [SMCMapProperty("DadosPessoais.Nome")]
        public string Nome { get; set; }

        [SMCMapProperty("DadosPessoais.NomeSocial")]
        public string NomeSocial { get; set; }

        public string NomeSocialReadOnly { get; set; }

        [SMCMapProperty("Pessoa.Cpf")]
        public string Cpf { get; set; }

        public string CpfReadOnly { get; set; }

        [SMCMapProperty("Pessoa.NumeroPassaporte")]
        public string NumeroPassaporte { get; set; }

        [SMCMapProperty("Pessoa.CodigoPaisEmissaoPassaporte")]
        public int? CodigoPaisEmissaoPassaporte { get; set; }

        [SMCMapProperty("Pessoa.DataValidadePassaporte")]
        public DateTime? DataValidadePassaporte { get; set; }

        public long SeqPessoa { get; set; }

        public long SeqPessoaDadosPessoais { get; set; }

        [SMCMapProperty("DadosPessoais.SeqArquivoFoto")]
        public long? SeqArquivoFoto { get; set; }

        [SMCMapProperty("Pessoa.DataNascimento")]
        public DateTime? DataNascimento { get; set; }

        public DateTime? DataNascimentoReadOnly { get; set; }

        [SMCMapProperty("Pessoa.Falecido")]
        public bool Falecido { get; set; }

        [SMCMapProperty("DadosPessoais.EstadoCivil")]
        public EstadoCivil? EstadoCivil { get; set; }

        [SMCMapProperty("DadosPessoais.Sexo")]
        public Sexo Sexo { get; set; }

        [SMCMapProperty("DadosPessoais.RacaCor")]
        public RacaCor? RacaCor { get; set; }

        [SMCMapProperty("Pessoa.TipoNacionalidade")]
        public TipoNacionalidade TipoNacionalidade { get; set; }

        public TipoNacionalidade TipoNacionalidadeReadOnly { get; set; }

        [SMCMapProperty("Pessoa.CodigoPaisNacionalidade")]
        public int CodigoPaisNacionalidade { get; set; }

        public int CodigoPaisNacionalidadeReadOnly { get; set; }

        [SMCMapProperty("DadosPessoais.UfNaturalidade")]
        public string UfNaturalidade { get; set; }

        [SMCMapProperty("DadosPessoais.CodigoCidadeNaturalidade")]
        public int? CodigoCidadeNaturalidade { get; set; }

        [SMCMapProperty("DadosPessoais.DescricaoNaturalidadeEstrangeira")]
        public string DescricaoNaturalidadeEstrangeira { get; set; }

        [SMCMapProperty("DadosPessoais.NumeroIdentidade")]
        public string NumeroIdentidade { get; set; }

        [SMCMapProperty("DadosPessoais.NumeroIdentidadeEstrangeira")]
        public string NumeroIdentidadeEstrangeira { get; set; }

        [SMCMapProperty("DadosPessoais.OrgaoEmissorIdentidade")]
        public string OrgaoEmissorIdentidade { get; set; }

        [SMCMapProperty("DadosPessoais.UfIdentidade")]
        public string UfIdentidade { get; set; }

        [SMCMapProperty("DadosPessoais.DataExpedicaoIdentidade")]
        public DateTime? DataExpedicaoIdentidade { get; set; }

        [SMCMapProperty("DadosPessoais.NumeroRegistroCnh")]
        public string NumeroRegistroCnh { get; set; }

        [SMCMapProperty("DadosPessoais.CategoriaCnh")]
        public CategoriaCnh? CategoriaCnh { get; set; }

        [SMCMapProperty("DadosPessoais.DataEmissaoCnh")]
        public DateTime? DataEmissaoCnh { get; set; }

        [SMCMapProperty("DadosPessoais.DataVencimentoCnh")]
        public DateTime? DataVencimentoCnh { get; set; }

        [SMCMapProperty("DadosPessoais.NumeroTituloEleitor")]
        public string NumeroTituloEleitor { get; set; }

        [SMCMapProperty("DadosPessoais.NumeroZonaTituloEleitor")]
        public string NumeroZonaTituloEleitor { get; set; }

        [SMCMapProperty("DadosPessoais.NumeroSecaoTituloEleitor")]
        public string NumeroSecaoTituloEleitor { get; set; }

        [SMCMapProperty("DadosPessoais.UfTituloEleitor")]
        public string UfTituloEleitor { get; set; }

        [SMCMapProperty("DadosPessoais.TipoPisPasep")]
        public TipoPisPasep? TipoPisPasep { get; set; }

        [SMCMapProperty("DadosPessoais.NumeroPisPasep")]
        public string NumeroPisPasep { get; set; }

        [SMCMapProperty("DadosPessoais.DataPisPasep")]
        public DateTime? DataPisPasep { get; set; }

        [SMCMapProperty("DadosPessoais.NumeroDocumentoMilitar")]
        public string NumeroDocumentoMilitar { get; set; }

        [SMCMapProperty("DadosPessoais.CsmDocumentoMilitar")]
        public string CsmDocumentoMilitar { get; set; }

        [SMCMapProperty("DadosPessoais.TipoDocumentoMilitar")]
        public TipoDocumentoMilitar? TipoDocumentoMilitar { get; set; }

        [SMCMapProperty("DadosPessoais.UfDocumentoMilitar")]
        public string UfDocumentoMilitar { get; set; }

        [SMCMapProperty("DadosPessoais.TipoRegistroProfissional")]
        public TipoRegistroProfissional? TipoRegistroProfissional { get; set; }

        [SMCMapProperty("DadosPessoais.NumeroRegistroProfissional")]
        public string NumeroRegistroProfissional { get; set; }

        [SMCMapProperty("DadosPessoais.NecessidadeEspecial")]
        public bool NecessidadeEspecial { get; set; }

        [SMCMapProperty("DadosPessoais.TipoNecessidadeEspecial")]
        public TipoNecessidadeEspecial? TipoNecessidadeEspecial { get; set; }

        public string Descricao { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        public bool PermitirAlterarDadosPessoaAtuacao { get; set; }

        public bool PermitirAlterarDadosPessoaAtuacaoNomeSocial { get; set; }

        #endregion [ Primitive Properties ]

        #region [ Navigation Properties ]

        [SMCMapProperty("DadosPessoais.ArquivoFoto")]
        public SMCUploadFile ArquivoFoto { get; set; }

        public List<PessoaAtuacaoEnderecoVO> Enderecos { get; set; }

        public List<PessoaEnderecoEletronicoVO> EnderecosEletronicos { get; set; }

        public List<PessoaTelefoneVO> Telefones { get; set; }

        [SMCMapProperty("Pessoa.Filiacao")]
        public List<PessoaFiliacaoVO> Filiacao { get; set; }

        [SMCMapProperty("Pessoa.Filiacao")]
        public List<PessoaFiliacaoReadOnlyVO> FiliacaoReadOnly { get; set; }

        public List<PessoaAtuacaoCondicaoObrigatoriedadeVO> CondicoesObrigatoriedade { get; set; }

        #endregion [ Navigation Properties ]
    }
}