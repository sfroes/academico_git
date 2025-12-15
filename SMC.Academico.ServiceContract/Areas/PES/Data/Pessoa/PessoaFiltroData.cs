using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Constants;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public class PessoaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        [DataMember]
        public long? Seq { get; set; }

        [DataMember]
        public TipoNacionalidade? TipoNacionalidade { get; set; }

        [DataMember]
        public string Cpf { get; set; }

        [DataMember]
        public string NumeroPassaporte { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public DateTime? DataNascimento { get; set; }

        [DataMember]
        public bool? DadosPessoaisCadastrados { get; set; }

        [DataMember]
        public long? SeqUsuarioSAS { get; set; }

        [DataMember]
        public TipoAtuacao? TipoAtuacao { get; set; }
    }
}