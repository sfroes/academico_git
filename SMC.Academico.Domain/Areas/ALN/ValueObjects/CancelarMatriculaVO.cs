using SMC.Academico.Domain.Models;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class CancelarMatriculaVO : ISMCMappable
    {
        #region Parametros documentação EA (RN_MAT_083 - Cancelamento de matrícula)

        public string TokenSituacaoCancelamento { get; set; }

        public string ObservacaoSituacaoMatricula { get; set; }

        public DateTime DataReferencia { get; set; }

        public long? SeqSolicitacaoServico { get; set; }

        public bool Jubilado { get; set; }

        public bool CancelarBeneficio { get; set; }

        public string ObservacaoCancelamentoSolicitacao { get; set; }

        public short TipoCancelamentoSGP { get; set; }

        #endregion

        #region Parametros auxiliares

        public long SeqPessoaAtuacao { get; set; }

        public long? SeqAlunoHistorico { get; set; }

        public int? CodigoAlunoSGP { get; set; }

        public string NumeroProtocolo { get; set; }

        public long? SeqCicloLetivoReferencia { get; set; }

        public long? SeqArquivoAnexado { get; set; }

        public ArquivoAnexado ArquivoAnexado { get; set; }

        #endregion
    }
}