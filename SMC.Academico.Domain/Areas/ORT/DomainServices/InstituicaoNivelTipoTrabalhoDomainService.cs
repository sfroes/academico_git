using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.ORT.DomainServices
{
    public class InstituicaoNivelTipoTrabalhoDomainService : AcademicoContextDomain<InstituicaoNivelTipoTrabalho>
    {
        public string BuscarTiposTrabalhoSeq(long seqTipoTrabalho)
        {
            var spec = new InstituicaoNivelTipoTrabalhoFilterSpecification()
            {
                SeqTipoTrabalho = seqTipoTrabalho
            };
           var descricao = this.SearchProjectionByKey(spec, x => x.TipoTrabalho.Descricao);

            return descricao;

        }

    }
}
