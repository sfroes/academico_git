using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DocumentoConclusaoConsultaVO : ISMCMappable
    {
        public long NumeroRegistroAcademico { get; set; }

        public int? CodigoAlunoMigracao { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public string NomeCompleto { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string DescricaoCursoOferta { get; set; }

        public string DescricaoDocumentoConclusao { get; set; }

        public string DescricaoApostilamento { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public string DescricaoTitulacao { get; set; }

        public long Seq { get; set; }

        public long? SeqPessoaDadosPessoais { get; set; }

        public long SeqTipoDocumentoAcademico { get; set; }

        public string DescricaoTipoDocumentoAcademico { get; set; }

        public bool LancamentoHistorico { get; set; }

        public long? SeqSolicitacaoServico { get; set; }

        public string CodigoMigracaoDocumento { get; set; }

        public long? SeqDocumentoDiplomaGAD { get; set; }

        public string TokenSituacaoDocumentoAcademicoAtual { get; set; }

        public string TokenTipoDocumento { get; set; }

        public string NumeroProtocolo { get; set; }

        public bool PossuiApostilamentos { get; set; }

        public short NumeroViaDocumento { get; set; }

        public long? SeqDocumentoConclusaoViaAnterior { get; set; }

        public bool ExibirMensagemInformativaBachareladoLicenciatura { get; set; }

        public List<DocumentoConclusaoFormacaoListarVO> FormacoesEspecificas { get; set; }

        public string UsuarioImpressao { get; set; }

        public DateTime? DataImpressao { get; set; }

        public long? NumeroSerie { get; set; }

        public TipoRegistroDocumento? TipoRegistroDocumento { get; set; }

        public string NumeroProcesso { get; set; }

        public string GrupoRegistro { get; set; }

        public string OrgaoDeRegistro { get; set; }

        public string NumeroRegistro { get; set; }

        public DateTime? DataRegistro { get; set; }

        public string UsuarioRegistro { get; set; }

        public string Livro { get; set; }

        public string Folha { get; set; }

        public bool ExibirSecaoEntrega { get; set; }

        public bool? ConfirmacaoAluno { get; set; }

        public bool ExibirSecaoHistoricosDownload { get; set; }

        public DocumentoConclusaoFiltroHistoricoDownloadVO FiltroHistoricoDownload { get; set; }

        public List<DocumentoConclusaoHistoricoListarVO> Situacoes { get; set; }

        public List<DocumentoConclusaoViaAnteriorListarVO> DocumentoConclusaoViaAnterior { get; set; }

        public List<DocumentoConclusaoMensagemVO> InformacoesAdicionais { get; set; }

        public bool ExibirBotaoApostilamento { get; set; }

        public bool HabilitarBotaoApostilamento { get; set; }

        public bool HabilitarSecoes { get; set; }

        public bool HabilitarSecaoRegistro { get; set; }

        public bool ExibirBotaoAnularDocumentoDigital { get; set; }

        public bool ExibirBotaoRestaurarDocumentoDigital { get; set; }

        public bool ExibirMensagemRestaurarDocumentoDigital { get; set; }

        public TipoInvalidade? TipoInvalidade { get; set; }
    }
}
