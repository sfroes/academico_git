using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class FormacaoEspecificaLookupPrepareFilter : ISMCFilter<FormacaoEspecificaLookupFiltroViewModel>
    {
        public FormacaoEspecificaLookupFiltroViewModel Filter(SMCControllerBase controllerBase, FormacaoEspecificaLookupFiltroViewModel filter)
        {
            //Buscar sempre as formações específicas ativas
            filter.Ativo = true;

            if (filter.SeqCursoOferta.HasValue)
            {
                filter.SeqCurso = controllerBase.Create<ICursoOfertaService>().BuscarCursoOferta(filter.SeqCursoOferta.Value)?.SeqCurso;
            }

            ///Regra mais que fala que se o Desenvolvedor passar que deve-se considerar tipos de formação especifica 
            if (filter.ConsiderarSometeTipoFomacaoEspecifica)
            {
                if(filter.SeqTipoFormacaoEspecifica.Count > 0 && filter.SelecaoNivelFolha == false && filter.SelecaoNivelSuperior == false)
                {
                    throw new System.Exception("Confira parametros. Seguir regra se passou com true considerar somente tipo de formação especifica, tipos de formação especifica algum, nivel folha e nivel superior tem que ser false.");
                }
            }
            //FIX: Verificar a tela de Curso Oferta Localidade para corrigir a referência
            //if (!filter.SeqFormacaoEspecifica.HasValue)
            //   filter.SeqFormacaoEspecifica = filter.SeqCursoOferta?.SeqFormacaoEspecifica;

            //FIX: Verificar nos lookups de formação específica e Curso Oferta os nomes das proprierdades em qeustão
            //Para as telas ou filtros em que esses lookups estão juntos, sendo utilizados com dependência a um select
            //múltiplo de entidades responsáveis, o nome da propriedade deve ser o mesmo nos dois lookups para que o
            //mapeamento possa funcionar.

            //FIX: Verificar a necessidade de considerar essa validação ao implementar o caso de uso
            //UC_CAM_001_03 - Consulta Candidato

            //Se o curso oferta tiver sido selecionado
            //if (filter.SeqCursoOferta != null && filter.SeqCursoOferta.HasValue)
            //{
            //    //Desconsiderar a lista de entidades responsaveis
            //    if (filter.SeqsEntidadesResponsaveis != null)
            //    {
            //        filter.SeqsEntidadesResponsaveis.Clear();
            //    }
            //    else
            //    {
            //        //Senão, considerar a lista de entidades responsaveis selecionada
            //        filter.SeqsEntidadesResponsaveis = new System.Collections.Generic.List<long>();
            //        foreach (var item in filter.SeqsEntidadesResponsaveis)
            //        {
            //            filter.SeqsEntidadesResponsaveis.Add(item.Value);
            //        }
            //    }

            //    //Caso o cusro oferta selecionado tenha formação específica vinculada, usar esta no filtro do lookup.
            //    filter.SeqFormacaoEspecifica = controllerBase.Create<ICursoOfertaService>().BuscarCursoOferta(filter.SeqCursoOferta.GetValueOrDefault())?.SeqFormacaoEspecifica;
            //}

            return filter;
        }
    }
}