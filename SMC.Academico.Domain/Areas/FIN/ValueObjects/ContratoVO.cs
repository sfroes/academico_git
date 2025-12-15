using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class ContratoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string NumeroRegistro { get; set; }

        public string Descricao { get; set; }

        public DateTime DataInicioValidade { get; set; }

        public DateTime? DataFimValidade { get; set; }

        public long SeqArquivoAnexado { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public SMCUploadFile ArquivoAnexado { get; set; }

        public List<ContratoCurso> Cursos { get; set; }

        public List<ContratoNivelEnsino> NiveisEnsino { get; set; }
    }
}
