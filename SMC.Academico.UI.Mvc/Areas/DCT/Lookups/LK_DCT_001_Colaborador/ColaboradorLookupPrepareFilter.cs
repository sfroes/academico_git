using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.Academico.UI.Mvc.Areas.DCT.Lookups
{
    public class ColaboradorLookupPrepareFilter : ISMCFilter<ColaboradorLookupFiltroViewModel>
    {
        public ColaboradorLookupFiltroViewModel Filter(SMCControllerBase controllerBase, ColaboradorLookupFiltroViewModel filter)
        {
            // Caso a origem do colaborador seja interno, desconsidera a instituição externa que possa ser informada como interna
            if (filter.OrigemColaborador == OrigemColaborador.Interno)
                filter.SeqInstituicaoExterna = null;

            if (filter.SeqTurma.HasValue)
            {
                filter.EntidadesColaborador = controllerBase.Create<IColaboradorService>().BuscarEntidadeVinculoColaboradorPorTurmaSelect(filter.SeqTurma.Value);
            }
            else
            {
                filter.EntidadesColaborador = controllerBase.Create<IEntidadeService>().BuscarEntidadesVinculoColaboradorSelect(false);
            }

            filter.TiposVinculoColaborador = controllerBase.Create<ITipoVinculoColaboradorService>().BuscarTipoVinculoColaboradorDeEntidadesVinculoSelect(criaVinculoInstitucional: filter.CriaVinculoInstitucional);

            filter.TiposAtividadeColaborador = controllerBase.Create<IInstituicaoNivelTipoAtividadeColaboradorService>().BuscarTiposAtividadeColaboradorSelect(new InstituicaoNivelTipoAtividadeColaboradorFiltroData());

            filter.TipoAtividadeReadOnly = filter.TipoAtividade.HasValue;

			filter.SeqEntidadeVinculoReadOnly = filter.SeqEntidadeVinculo.HasValue;

            if (filter.DataInicioVinculo.HasValue)
            {
                filter.DataInicio = filter.DataInicioVinculo;
            }

            if (filter.DataFimVinculo.HasValue)
            {
                filter.DataFim = filter.DataFimVinculo;
            }

			filter.DataInicioReadOnly = filter.DataInicio.HasValue;

			filter.DataFimReadOnly = filter.DataFim.HasValue;

            

			return filter;
        }
    }
}