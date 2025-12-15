using SMC.Academico.Domain.Models;
using SMC.Academico.Domain.ValueObjects;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class ReferenciaFamiliarVO : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public TipoParentesco TipoParentesco { get; set; }

        public string NomeParente { get; set; }

        public List<EnderecoVO> Enderecos { get; set; }
        
        public List<EnderecoEletronico> EnderecosEletronicos { get; set; }

        public List<TelefoneVO> Telefones { get; set; }
        
    }
}