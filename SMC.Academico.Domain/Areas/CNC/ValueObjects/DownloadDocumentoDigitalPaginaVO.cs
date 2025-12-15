using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DownloadDocumentoDigitalPaginaVO : ISMCMappable
    {
        public long SeqSolicitacaoServicoAuxiliar { get; set; }

        public bool ExisteDocumentoDiplomaDigital { get; set; }

        public string TituloSecaoInstituicaoEmissora { get; set; }

        public string TokenServico { get; set; }

        public ClasseSituacaoDocumento CategoriaSituacaoDocumentoAcademicoAtual { get; set; }

        public List<DownloadDocumentoDigitalDocumentoConclusaoPaginaVO> DocumentosConclusao { get; set; }

        #region Dados Diplomado

        public int? CodigoAlunoMigracao { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public string Cpf { get; set; }

        public string NomeAluno { get; set; }

        public string NomeSocial { get; set; }

        public string NumeroIdentidade { get; set; }

        public string OrgaoEmissorIdentidade { get; set; }

        public string UfIdentidade { get; set; }

        public string NumeroPassaporte { get; set; }

        public int? CodigoPaisEmissaoPassaporte { get; set; }

        public string NomePaisEmissaoPassaporte { get; set; }

        public Sexo Sexo { get; set; }

        public int CodigoPaisNacionalidade { get; set; }

        public TipoNacionalidade TipoNacionalidade { get; set; }

        public int? CodigoNacionalidade { get; set; }

        public string DescricaoNacionalidade { get; set; }

        public int CodigoCidadeNaturalidade { get; set; }

        public string UfNaturalidade { get; set; }

        public string DescricaoNaturalidadeEstrangeira { get; set; }

        public string Naturalidade { get; set; }

        public DateTime? DataNascimento { get; set; }

        public long? SeqCicloLetivoFormado { get; set; }

        #endregion

        #region Dados Curso

        public string DescricaoCurso { get; set; }

        public long? CodigoEmecCurso { get; set; }

        public string DescricaoModalidade { get; set; }

        public string AtoAutorizacaoCurso { get; set; }

        public string AtoReconhecimentoCurso { get; set; }

        public string AtoRenovacaoReconhecimentoCurso { get; set; }

        public string EnderecoCurso { get; set; }

        #endregion

        #region Dados Instituição

        public string NomeInstituicao { get; set; }

        public long CodigoEmecInstituicao { get; set; }

        public string CnpjInstituicao { get; set; }

        public string EnderecoInstituicao { get; set; }

        public string AtoCredenciamentoInstituicao { get; set; }

        public string AtoRecredenciamentoInstituicao { get; set; }

        public string AtoRenovacaoRecredenciamentoInstituicao { get; set; }

        #endregion

        #region Dados Mantenedora

        public string RazaoSocialMantenedora { get; set; }

        public string CnpjMantenedora { get; set; }

        public string EnderecoMantenedora { get; set; }

        #endregion
    }
}
