using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class EntidadeFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public string Nome { get; set; }

        public long? SeqTipoEntidade { get; set; }

        public List<long> SeqsTipoEntidade { get; set; }

        public bool? ApenasAtivos { get; set; }

        public bool? Externada { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public bool? PermiteAtoNormativo { get; set; }

        public bool? LookupEntidadeAtoNormativo { get; set; }

        /// <summary>
        /// Desativa todos os filtros de dados exceto o filtro de instituição
        /// </summary>
        public bool IgnorarFiltros { get; set; }

        public CategoriaAtividade? CategoriaAtividade { get; set; }

        public int? CodigoUnidadeSeo { get; set; }

        public TipoOrgaoRegulador? TipoOrgaoRegulador { get; set; }

        public int? CodigoOrgaoRegulador { get; set; }

        public List<long> SeqsEntidadesCompartilhadas { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

    }
}