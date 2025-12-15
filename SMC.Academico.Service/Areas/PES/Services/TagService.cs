using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class TagService : SMCServiceBase, ITagService
    {
        #region [ DomainServices ]

        private TagDomainService TagDomainService => this.Create<TagDomainService>();

        #endregion [ DomainServices ]

        /// 
        /// <summary>
        /// Buscar os dados da tag pelo seq
        /// </summary>
        /// <returns>Retorna a tag</returns>
        public TagData BuscarTag(long seq)
        {
            return TagDomainService.BuscarTag(seq).Transform<TagData>();
        }

        public long SalvarTag(TagData tag)
        {
            return TagDomainService.SalvarTag(tag.Transform<TagVO>());
        }

        public bool ExibirMensagem(TagData tag)
        {
            return TagDomainService.ExibirMensagem(tag.Transform<TagVO>());
        }

    }
}