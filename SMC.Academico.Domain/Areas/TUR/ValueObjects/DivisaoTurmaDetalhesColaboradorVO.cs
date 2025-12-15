using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class DivisaoTurmaDetalhesColaboradorVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqColaborador { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public string NomeFormatado { get { return string.IsNullOrEmpty(NomeSocial) ? Nome : NomeSocial; } }

        public short QuantidadeCargaHoraria { get; set; }

        /// FIX - CAROL - ALTERAÇÃO ORGANIZAÇÃO DE COMPONENTE
        //public List<DivisaoTurmaDetalhesColaboradorOrganizacaoVO> ColaboradorOrganizacaoComponente { get; set; }
    }
}
