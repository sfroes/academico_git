using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class EntregaDocumentoDigitalDocumentoConclusaoPaginaVO : ISMCMappable
    {
        public bool DiplomaDigital { get; set; }

        public long SeqDocumentoConclusao { get; set; }

        public string DescricaoCurso { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public string DescricaoTitulacao { get; set; }

        public DateTime? DataColacaoGrau { get; set; }

        public string NumeroVia { get; set; }

        public string CodigoValidacaoDiploma { get; set; }

        public string CodigoValidacaoHistorico { get; set; }

        public string NomeDiplomado { get; set; }

        public string UrlConsultaPublicaDiploma { get; set; }

        public string UrlConsultaPublicaHistorico { get; set; }

        public bool ExibirGrauTitulacao { get; set; }

        public List<EntregaDocumentoDigitalFormacaoPaginaVO> FormacoesEspecificas { get; set; }

        public string TokenTipoDocumentoAcademico { get; set; }
    }
}
