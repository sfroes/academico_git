using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Models;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class MantenedoraVO : ISMCMappable
    {
        public long Seq { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int? CodigoUnidadeSeo { get; set; }
        public long? SeqArquivoLogotipo { get; set; }
        public SMCUploadFile ArquivoLogotipo { get; set; }
        public IList<InstituicaoEnsino> InstituicoesEnsino { get; set; }
        public IList<Endereco> Enderecos { get; set; }
        public IList<EnderecoEletronico> EnderecosEletronicos { get; set; }
        public IList<Telefone> Telefones { get; set; }
    }
}