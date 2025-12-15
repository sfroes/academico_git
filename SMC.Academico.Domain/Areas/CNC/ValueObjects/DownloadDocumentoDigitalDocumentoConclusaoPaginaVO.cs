using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DownloadDocumentoDigitalDocumentoConclusaoPaginaVO : ISMCMappable
    {
        public bool DiplomaDigital { get; set; }

        public long SeqDocumentoConclusao { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public ClasseSituacaoDocumento CategoriaSituacaoDocumentoAcademicoAtual { get; set; }

        public string TituloSecao { get; set; }

        public string CodigoValidacaoDiploma { get; set; }

        public string CodigoValidacaoHistorico { get; set; }

        public string DescricaoCurso { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public string DescricaoTitulacao { get; set; }

        public string NumeroRegistro { get; set; }

        public DateTime? DataRegistro { get; set; }

        public string NumeroProcesso { get; set; }

        public DateTime? DataColacao { get; set; }

        public string FormacaoEspecifica { get; set; }

        public bool ExibirGrauTitulacao { get; set; }

        public bool ExibirFormacao { get; set; }

        public string NomeDiplomado { get; set; }

        public string UrlConsultaPublicaDiploma { get; set; }

        public string UrlConsultaPublicaHistorico { get; set; }
    }
}
