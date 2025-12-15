using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.SGA.Administrativo.Areas.OFC.Models;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.OFC.Services
{
    public interface IOfertaCursoCicloLetivoControllerService
    {
        #region Oferta de Curso por Ciclo Letivo
        
        SMCPagerData<OfertaCursoCicloLetivoListaViewModel> BuscarOfertasCursosCiclosLetivos(OfertaCursoCicloLetivoFiltroViewModel filtros);
        
        OfertaCursoCicloLetivoViewModel BuscarOfertaCursoCicloLetivo(long seqOfertaCursoCicloLetivo);

        long SalvarOfertaCursoCicloLetivo(OfertaCursoCicloLetivoViewModel modelo);

        void ExcluirOfertaCursoCicloLetivo(long seqOfertaCursoCicloLetivo);
        
        OfertaCursoCicloLetivoDadosViewModel BuscarDadosOfertaCursoCicloLetivo(long seqOfertaCursoCicloLetivo);

        #endregion

        #region Associação de Curso/Unidade/Turno

        SMCPagerData<OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoListaViewModel> BuscarCursosUnidadesTurnos(OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoFiltroViewModel filtros);

        long SalvarAssociacaoCursoUnidadeTurno(OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoViewModel modelo);

        void ExcluirAssociacaoCursoUnidadeTurno(SMCEncryptedLong seqAssociacaoCursoUnidadeTurno);

        #endregion

        #region Associação de Curso/Unidade/Turno Lote

        SMCPagerData<OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoLoteListaViewModel> BuscarCursosUnidadesTurnosLote(OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoLoteFiltroViewModel filtros);

        OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoViewModel BuscarAssociacaoCursoUnidadeTurno(long seqAssociacaoCursoUnidadeTurno);

        void SalvarAssociacaoCursoUnidadeTurnoLote(List<object> selectedValues, DateTime dataInicio, DateTime dataFim);

        #endregion
        
        #region Associação de Localidades
        
        SMCPagerData<OfertaCursoCicloLetivoAssociacaoLocalidadeListaViewModel> BuscarLocalidadesAssociadas(OfertaCursoCicloLetivoAssociacaoLocalidadeFiltroViewModel filtros);

        void IncluirAssociacaoLocalidades(SMCEncryptedLong seqOfertaCursoCicloLetivo, Framework.UI.Mvc.Html.SMCTreeViewModel<long> treeviewLocalidades);

        void ExcluirAssociacaoLocalidade(SMCEncryptedLong seqAssociacaoLocalidade);

        #endregion

        #region Associação de Polos

        SMCPagerData<OfertaCursoCicloLetivoAssociacaoPoloListaViewModel> BuscarPolosAssociados(OfertaCursoCicloLetivoAssociacaoPoloFiltroViewModel filtros);

        void ExcluirAssociacaoPolo(SMCEncryptedLong seqAssociacaoPolo);

        void IncluirAssociacaoPolos(SMCEncryptedLong seqOfertaCursoCicloLetivo, Framework.UI.Mvc.Html.SMCTreeViewModel<long> treeviewPolos);

        #endregion

        
    }
}