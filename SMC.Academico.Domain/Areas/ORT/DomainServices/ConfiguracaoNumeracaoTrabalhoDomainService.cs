using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.ORT.ValueObjects;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORT.DomainServices
{
    public class ConfiguracaoNumeracaoTrabalhoDomainService : AcademicoContextDomain<ConfiguracaoNumeracaoTrabalho>
    {
        #region [ DomainService ]

        #endregion [ DomainService ]

        public SMCPagerData<ConfiguracaoNumeracaoTrabalhoListaVO> BuscarConfiguracaoNumeracaoTrabalho(ConfiguracaoNumeracaoTrabalhoFilterSpecification filtro)
        {
            //filtro.SetOrderBy(x => x.Seq.FirstOrDefault());

            var lista = this.SearchBySpecification(filtro).ToList();

            var result = this.SearchProjectionBySpecification(filtro, p => new ConfiguracaoNumeracaoTrabalhoListaVO()
            {
                Seq = p.Seq,
                Descricao = p.Descricao,
                EntidadeResponsavel = p.EntidadeResponsavel.Nome,
                NumeroUltimaNumeracao = p.NumeroUltimaNumeracao,
                Ofertas = p.Ofertas.Select( x => new ConfiguracaoNumeracaoTrabalhoOfertaListaVO() {
                    Seq = x.Seq,
                    Descricao = x.CursoOfertaLocalidade.Nome
                }).ToList()
               
            }, out int total).ToList();


            return new SMCPagerData<ConfiguracaoNumeracaoTrabalhoListaVO>(result, total);
        }

        public long SalvarConfiguracaoNumeracaoTrabalho(ConfiguracaoNumeracaoTrabalho configuracaoNumeracaoTrabalho)
        {
            

            //Salva o objeto grupo configuração componente
            this.SaveEntity(configuracaoNumeracaoTrabalho);

            return configuracaoNumeracaoTrabalho.Seq;
        }

    }
}