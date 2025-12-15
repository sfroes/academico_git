using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data.PessoaAtuacaoBeneficio
{
    public class PessoaAtuacaoBeneficioAnexoData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public Guid? UidArquivoAnexado { get; set; }

        public long SeqPessoaAtuacaoBeneficio { get; set; }

        public SMCUploadFile ArquivoAnexado { get; set; }

        public string Observacao { get; set; }

        public DateTime? DataInclusao { get; set; }
    }
}