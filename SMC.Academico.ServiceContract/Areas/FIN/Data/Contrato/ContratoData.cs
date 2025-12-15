using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.FIN
{
    public class ContratoData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }
        
        public string NumeroRegistro { get; set; }
        
        public string Descricao { get; set; }
        
        public DateTime DataInicioValidade { get; set; }
         
        public DateTime? DataFimValidade { get; set; }
        
        public long SeqArquivoAnexado { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public SMCUploadFile ArquivoAnexado { get; set; }
 
        public List<ContratoCursoData> Cursos { get; set; }
         
        public List<ContratoNiveisEnsinoData> NiveisEnsino { get; set; }
    }
}
