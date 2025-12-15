using SMC.Academico.ServiceContract.Data;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class ReferenciaFamiliarData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public TipoParentesco TipoParentesco { get; set; }

        public string NomeParente { get; set; }

        public List<EnderecoData> Enderecos { get; set; }

        public List<EnderecoEletronicoData> EnderecosEletronicos { get; set; }

        public List<TelefoneData> Telefones { get; set; }

        public List<SMCDatasourceItem> TiposEnderecos { get; set; }

        public List<SMCDatasourceItem> Paises { get; set; }
    }
}