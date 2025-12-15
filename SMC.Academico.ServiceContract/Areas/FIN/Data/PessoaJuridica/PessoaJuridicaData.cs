using SMC.Academico.ServiceContract.Data;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class PessoaJuridicaData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string RazaoSocial { get; set; }

        public string Cnpj { get; set; }

        public string NomeFantasia { get; set; }

        public List<EnderecoData> Enderecos { get; set; }

        public List<TelefoneData> Telefones { get; set; }

        public List<EnderecoEletronicoData> EnderecosEletronicos { get; set; }
    }

}
