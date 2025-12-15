using SMC.Academico.ServiceContract.Data;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class MantenedoraData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int? CodigoUnidadeSeo { get; set; }
        public long? SeqArquivoLogotipo { get; set; }
        public SMCUploadFile ArquivoLogotipo { get; set; }
        public IList<InstituicaoEnsinoData> InstituicoesEnsino { get; set; }
        public IList<EnderecoData> Enderecos { get; set; }
        public IList<EnderecoEletronicoData> EnderecosEletronicos { get; set; }
        public IList<TelefoneData> Telefones { get; set; }
    }
}