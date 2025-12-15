using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoVO : ISMCMappable
    {
        public long SeqSolicitacaoServicoAuxiliar { get; set; }

        public long SeqConfiguracaoEtapaAuxiliar { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqPessoa { get; set; }

        public long SeqPessoaAtuacaoAuxiliar { get; set; }

        public long SeqPessoaDadosPessoais { get; set; }

        public string NomeAluno { get; set; }

        public string NomeSocial { get; set; }

        public DateTime? DataInclusaoDadosPessoais { get; set; }

        public string UsuarioInclusaoDadosPessoais { get; set; }

        public long NumeroRA { get; set; }

        public string Cpf { get; set; }

        public Sexo Sexo { get; set; }

        public long? CodigoAlunoMigracao { get; set; }

        public int CodigoPessoaCAD { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public long? SeqCurso { get; set; }

        public long SeqCursoOferta { get; set; }

        //public long? SeqGrauAcademico { get; set; }

        public string DescricaoGrauAcademicoSelecionado { get; set; }

        public long? SeqGrauAcademicoSelecionado { get; set; }

        public long? SeqCicloLetivoFormado { get; set; }

        public long? SeqTitulacao { get; set; }

        public string DescricaoTitulacao { get; set; }

        public string DescricaoModalidade { get; set; }

        public string DescricaoCursoDocumento { get; set; }

        public string DescricaoCurso { get; set; }

        public long? SeqTipoDocumentoAcademicoAuxiliar { get; set; }

        public string DescricaoTipoDocumentoAcademicoAuxiliar { get; set; }

        public long SeqTipoDocumentoAcademico { get; set; }

        public string DescricaoTipoDocumentoAcademico { get; set; }

        public bool EmissaoPermitida { get; set; }

        public string MotivoEmissaoNaoPermitida { get; set; }

        public string TokenMotivoEmissaoNaoPermitida { get; set; }

        public long SeqMotivoEmissaoNaoPermitida { get; set; }

        public string ObservacoesEmissaoNaoPermitida { get; set; }

        public int? NumeroVia { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string DescricaoCursoOferta { get; set; }

        public long SeqNivelEnsino { get; set; }

        public string TokenNivelEnsino { get; set; }

        public string DescricaoCursoOfertaLocalidade { get; set; }

        public long SeqCursoOfertaLocalidade { get; set; }

        public int? CodigoCursoOfertaLocalidade { get; set; }

        public long? SeqCursoOfertaLocalidadeHistoricoAtual { get; set; }

        public int? CodigoUnidadeSeo { get; set; }

        public string ReconhecimentoCurso { get; set; }

        public bool ExibirComandoDocumentacaoAcademica { get; set; }

        public bool ExibirComandoRVDD { get; set; }

        public bool ExibirMensagemDiplomaDigitalBachareladoLicenciatura { get; set; }

        public bool ExibirMensagemDiplomaDigitalGrauAcademicoSemAssociacao { get; set; }

        public bool ExibirMensagemHistoricoEscolarNumeroViaMaiorUm { get; set; }

        public bool ExibirMensagemDiplomaPrimeiraViaComHistorico { get; set; }

        public bool ExibirMensagemAutorizacaoNomeSocial { get; set; }

        public string MensagemDiplomaDigital { get; set; }

        public List<SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaVO> FormacoesEspecificasConcluidas { get; set; }

        public List<SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaVO> FormacoesEspecificasViasAnteriores { get; set; }

        public EstadoCivil? EstadoCivil { get; set; }

        public DateTime? DataNascimento { get; set; }

        public List<PessoaFiliacaoVO> Filiacao { get; set; }

        public string NumeroIdentidade { get; set; }

        public string OrgaoEmissorIdentidade { get; set; }

        public string UfIdentidade { get; set; }

        public DateTime? DataExpedicaoIdentidade { get; set; }

        public string TipoDocumento { get; set; }

        public string NumeroDocumento { get; set; }

        public string NumeroPassaporte { get; set; }

        public int? CodigoPaisEmissaoPassaporte { get; set; }

        public string NomePaisEmissaoPassaporte { get; set; }

        public DateTime? DataValidadePassaporte { get; set; }

        public string NumeroIdentidadeEstrangeira { get; set; }

        public string NumeroRegistroCnh { get; set; }

        public string UfNaturalidade { get; set; }

        public int CodigoCidadeNaturalidade { get; set; }

        public string DescricaoNaturalidadeEstrangeira { get; set; }

        public string DescricaoNacionalidade { get; set; }

        public int? CodigoNacionalidade { get; set; }

        public long CodigoPaisNacionalidade { get; set; }

        public TipoNacionalidade TipoNacionalidade { get; set; }

        public int? NumeroViaAnterior { get; set; }

        public long? SeqViaAnterior { get; set; }

        public string TokenTipoDocumentoAcademicoAnterior { get; set; }

        public string DescricaoTipoDocumentoAcademicoAnterior { get; set; }

        public long? SeqDocumentoDiplomaGADAnterior { get; set; }

        public string Cotutela { get; set; }

        public DateTime? DataDefesa { get; set; }

        public long SeqTemplateProcessoSGF { get; set; }

        public string TokenServico { get; set; }

        public List<SolicitacaoAnaliseEmissaoDocumentoConclusaoTermoCotutelaVO> TermosCotutela { get; internal set; }

        public bool ExisteDocumentoConclusao { get; set; }

        public string Naturalidade { get; set; }

        public string InformacaoViaAnterior { get; set; }

        public List<CursoFormacaoEspecificaGrauAcademicoVO> CursosFormacaoEspecificaGrauAcademico { get; set; }

        public bool ExibirReconhecimentoCurso { get; set; }

        public string DescricaoFormaIngresso { get; set; }

        public string DescricaoXSDFormaIngresso { get; set; }

        public string TokenFormaIngresso { get; set; }

        public DateTime DataAdmissao { get; set; }

        public long? SeqTipoDocumentoSolicitado { get; set; }

        public string DescricaoTipoDocumentoSolicitado { get; set; }

        public DateTime? DataConclusao { get; set; }

        public bool DesabilitarTipoIdentidade { get; set; }

        public TipoIdentidade? TipoIdentidade { get; set; }

        public bool ExibirSecaoDocumentacaoAcademica { get; set; }

        public FormatoHistoricoEscolar? FormatoHistoricoEscolar { get; set; }

        public decimal? CargaHorariaCurso { get; set; }

        public decimal? CargaHorariaIntegralizada { get; set; }

        public DateTime? DataEmissaoHistorico { get; set; }

        public DateTime? DataEnade { get; set; }

        public string SituacaoEnade { get; set; }

        public List<SolicitacaoAnaliseEmissaoDocumentoConclusaoDocumentacaoVO> DocumentacaoComprobatoria { get; set; }

        public bool ExibirSecaoDadosRegistro { get; set; }

        public string DescricaoViaAnterior { get; set; }

        public bool? ReutilizarDados { get; set; }

        public bool HabilitarBotaoNovaMensagem { get; set; }

        public string TokenTipoDocumentoAcademico { get; set; }

        public long SeqInstituicaoNivel { get; set; }

        public string NomePais { get; set; }

        public string DescricaoViaAnteriorMensagem { get; set; }

        public string DescricaoViaAtual { get; set; }

        public bool CamposReadOnly { get; set; }

        public long? SeqOrgaoRegistro { get; set; }

        public string DescricaoCursoFormacaoEmissao { get; set; }

        public long? SeqGrauAcademicoEmissao { get; set; }

        public long? SeqTitulacaoEmissao { get; set; }

        public string DescricaoSituacaoAtualMatricula { get; set; }

        public string DescricaoXSDSituacaoAtualMatricula { get; set; }

        public string TokenSituacaoAtualMatricula { get; set; }

        public bool EmissaoDiplomaDigital1Via { get; set; }

        public bool ExisteFormacaoAssociada { get; set; }

        public DateTime? DataEmissaoHistoricoEmissaoDiploma { get; set; }

        public string Protocolo { get; set; }

        public bool ExibirNomeSocial { get; set; }

        public List<SMCDatasourceItem> TiposGrauAcademico { get; set; }

        public bool HabilitarCampoGrauAcademico { get; set; }

        public bool ExibirDadosDiploma { get; set; }

        public bool ExibirDadosHistorico { get; set; }

        public DateTime DataExpedicaoDiploma { get; set; }
    }
}