using SMC.Academico.ServiceContract.Data;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long? SeqUsuarioSAS { get; set; }

        public TipoNacionalidade TipoNacionalidade { get; set; }

        public int CodigoPaisNacionalidade { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public virtual DateTime? DataValidadePassaporte { get; set; }

        public virtual int? CodigoPaisEmissaoPassaporte { get; set; }

        public DateTime? DataNascimento { get; set; }

        #region [ DadosPessoaisAtual ]

        [SMCMapMethod("RecuperarArquivoFotoAtual")]
        public SMCUploadFile ArquivoFoto { get; set; }

        [SMCMapMethod("RecuperarSeqArquivoFotoAtual")]
        public long? SeqArquivoFoto { get; set; }

        [SMCMapMethod("RecuperarNomeAtual")]
        public string Nome { get; set; }

        [SMCMapMethod("RecuperarNomeSocialAtual")]
        public string NomeSocial { get; set; }

        [SMCMapMethod("RecuperarEstadoCivilAtual")]
        public EstadoCivil? EstadoCivil { get; set; }

        [SMCMapMethod("RecuperarSexoAtual")]
        public Sexo Sexo { get; set; }

        [SMCMapMethod("RecuperarRacaCorAtual")]
        public RacaCor RacaCor { get; set; }

        [SMCMapMethod("RecuperarUfNaturalidadeAtual")]
        public string UfNaturalidade { get; set; }

        [SMCMapMethod("RecuperarCodigoCidadeNaturalidadeAtual")]
        public int? CodigoCidadeNaturalidade { get; set; }

        [SMCMapMethod("RecuperarDescricaoNaturalidadeEstrangeiraAtual")]
        public string DescricaoNaturalidadeEstrangeira { get; set; }

        [SMCMapMethod("RecuperarNumeroIdentidadeAtual")]
        public string NumeroIdentidade { get; set; }

        [SMCMapMethod("RecuperarOrgaoEmissorIdentidadeAtual")]
        public string OrgaoEmissorIdentidade { get; set; }

        [SMCMapMethod("RecuperarUfIdentidadeAtual")]
        public string UfIdentidade { get; set; }

        [SMCMapMethod("RecuperarDataExpedicaoIdentidadeAtual")]
        public DateTime? DataExpedicaoIdentidade { get; set; }

        [SMCMapMethod("RecuperarNumeroTituloEleitorAtual")]
        public string NumeroTituloEleitor { get; set; }

        [SMCMapMethod("RecuperarNumeroZonaTituloEleitorAtual")]
        public string NumeroZonaTituloEleitor { get; set; }

        [SMCMapMethod("RecuperarNumeroSecaoTituloEleitorAtual")]
        public string NumeroSecaoTituloEleitor { get; set; }

        [SMCMapMethod("RecuperarUfTituloEleitorAtual")]
        public string UfTituloEleitor { get; set; }

        [SMCMapMethod("RecuperarNumeroPisPasepAtual")]
        public string NumeroPisPasep { get; set; }

        [SMCMapMethod("RecuperarNecessidadeEspecialAtual")]
        public bool NecessidadeEspecial { get; set; }

        [SMCMapMethod("RecuperarTipoNecessidadeEspecialAtual")]
        public TipoNecessidadeEspecial? TipoNecessidadeEspecial { get; set; }

        public List<PessoaFiliacaoData> Filiacao { get; set; }

        #endregion [ DadosPessoaisAtual ]
    }
}