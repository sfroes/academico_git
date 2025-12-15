using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTipoMensagem { get; set; }

        public string DescricaoTipoMensagem { get; set; }

        public string Descricao { get; set; }

        public string DescricaoDecode { get; set; }

        public DateTime DataInicioVigencia { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public bool ExisteDocumentoConclusao { get; set; }

        public string TokenTipoDocumentoAcademico { get; set; }

        public long? SeqTipoDocumentoSolicitado { get; set; }

        public int? NumeroVia { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqInstituicaoNivel { get; set; }

        public bool? ReutilizarDados { get; set; }

        public string NomePais { get; set; }

        public string DescricaoViaAnterior { get; set; }

        public string DescricaoViaAtual { get; set; }

        public int? CodigoUnidadeSeo { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public bool HabilitarBotaoEditar { get; set; }

        public bool HabilitarBotaoExcluir { get; set; }

        public bool ExibirNomeSocial { get; set; }

        public string NomeAluno { get; set; }

        public string DocumentoAcademico { get; set; }
    }
}
