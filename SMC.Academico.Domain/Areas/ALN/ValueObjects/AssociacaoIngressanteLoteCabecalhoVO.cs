using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class AssociacaoIngressanteLoteCabecalhoVO : ISMCMappable
    {
        public long SeqIngressante { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public bool Falecido { get; set; }
    }
}