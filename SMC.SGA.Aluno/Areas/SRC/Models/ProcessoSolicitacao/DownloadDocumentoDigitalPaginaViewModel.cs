using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.SRC.Models
{
    public class DownloadDocumentoDigitalPaginaViewModel : SolicitacaoServicoPaginaViewModelBase
    {
        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.DOWNLOAD_DOCUMENTO_DIGITAL;

        public override string ChaveTextoBotaoProximo => "Botao_Sairprocesso";

        [SMCHidden]
        public long SeqSolicitacaoServicoAuxiliar { get; set; }

        [SMCHidden]
        public bool ExisteDocumentoDiplomaDigital { get; set; }

        [SMCHidden]
        public string TituloSecaoInstituicaoEmissora { get; set; }

        [SMCHidden]
        public ClasseSituacaoDocumento CategoriaSituacaoDocumentoAcademicoAtual { get; set; }

        public List<DownloadDocumentoDigitalDocumentoConclusaoPaginaViewModel> DocumentosConclusao { get; set; }

        #region Dados Diplomado

        [SMCSize(SMCSize.Grid3_24)]
        public int? CodigoAlunoMigracao { get; set; }

        [SMCHidden]
        public long NumeroRegistroAcademico { get; set; }

        [SMCSize(SMCSize.Grid3_24)]
        public string Cpf { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string NomeAluno { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string NomeSocial { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string NumeroIdentidade { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string OrgaoEmissorIdentidade { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string UfIdentidade { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string NumeroPassaporte { get; set; }

        [SMCHidden]
        public int? CodigoPaisEmissaoPassaporte { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string NomePaisEmissaoPassaporte { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public Sexo Sexo { get; set; }

        [SMCHidden]
        public int CodigoPaisNacionalidade { get; set; }

        [SMCHidden]
        public TipoNacionalidade TipoNacionalidade { get; set; }

        [SMCHidden]
        public int? CodigoNacionalidade { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string DescricaoNacionalidade { get; set; }

        [SMCHidden]
        public int CodigoCidadeNaturalidade { get; set; }

        [SMCHidden]
        public string UfNaturalidade { get; set; }

        [SMCHidden]
        public string DescricaoNaturalidadeEstrangeira { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string Naturalidade { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        public DateTime DataNascimento { get; set; }

        [SMCHidden]
        public long? SeqCicloLetivoFormado { get; set; }

        #endregion

        #region Dados Curso

        [SMCSize(SMCSize.Grid6_24)]
        public string DescricaoCurso { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public long? CodigoEmecCurso { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCConditionalDisplay(nameof(ExisteDocumentoDiplomaDigital), SMCConditionalOperation.Equals, true)]
        public string DescricaoModalidade { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(ExisteDocumentoDiplomaDigital), SMCConditionalOperation.Equals, true)]
        public string AtoAutorizacaoCurso { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(ExisteDocumentoDiplomaDigital), SMCConditionalOperation.Equals, true)]
        public string AtoReconhecimentoCurso { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string AtoRenovacaoReconhecimentoCurso { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(ExisteDocumentoDiplomaDigital), SMCConditionalOperation.Equals, true)]
        public string EnderecoCurso { get; set; }

        #endregion

        #region Dados Instituição

        [SMCSize(SMCSize.Grid8_24)]
        public string NomeInstituicao { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public long CodigoEmecInstituicao { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string CnpjInstituicao { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(ExisteDocumentoDiplomaDigital), SMCConditionalOperation.Equals, true)]
        public string EnderecoInstituicao { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(ExisteDocumentoDiplomaDigital), SMCConditionalOperation.Equals, true)]
        public string AtoCredenciamentoInstituicao { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string AtoRecredenciamentoInstituicao { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string AtoRenovacaoRecredenciamentoInstituicao { get; set; }

        #endregion

        #region Dados Mantenedora

        [SMCSize(SMCSize.Grid8_24)]
        public string RazaoSocialMantenedora { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string CnpjMantenedora { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(ExisteDocumentoDiplomaDigital), SMCConditionalOperation.Equals, true)]
        public string EnderecoMantenedora { get; set; }

        #endregion
    }
}