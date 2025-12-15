using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class GrupoCurricularDescricaoVO : ISMCMappable
    {
        public string Descricao { get; set; }

        /// <summary>
        /// Descrição da formação específica no formato "[ TipoFormacao ] formacao"
        /// </summary>
        public string DescricaoFormacaoEspecifica { get; set; }

        public List<string> DescricoesBeneficios { get; set; }

        public List<string> DescricoesCondicoesObrigatoriedade { get; set; }
    }
}