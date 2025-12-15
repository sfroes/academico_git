using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class AtoNormativoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqAssuntoNormativo { get; set; }

        public long SeqTipoAtoNormativo { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public DateTime? DataPublicacao { get; set; }

        public DateTime DataDocumento { get; set; }

        public DateTime? DataPrazoValidade { get; set; }

        public string NumeroDocumento { get; set; }

        public string Descricao { get; set; }

        public string EnderecoEletronico { get; set; }

        public long? SeqArquivoAnexado { get; set; }

        public SMCUploadFile ArquivoAnexado { get; set; }

        public VeiculoPublicacao? VeiculoPublicacao { get; set; }

        public int? NumeroPublicacao { get; set; }

        public int? NumeroSecaoPublicacao { get; set; }

        public int? NumeroPaginaPublicacao { get; set; }

        public string DescricaoAssuntosNormativos { get; set; }

        public string DescricaoTiposAtoNormativos { get; set; }

        public bool HabilitaCampo { get; set; }
    }
}