using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.ALN.Services
{
    public class InstituicaoNivelTipoOrientacaoService : SMCServiceBase, IInstituicaoNivelTipoOrientacaoService
    {
        #region [ DomainServices ]

        private InstituicaoNivelTipoOrientacaoDomainService InstituicaoNivelTipoOrientacaoDomainService
        {
            get { return this.Create<InstituicaoNivelTipoOrientacaoDomainService>(); }
        }

        #endregion [ DomainServices ]

        public long SalvarInstituicaoNivelTipoOrientacao(InstituicaoNivelTipoOrientacaoData modelo)
        {
            return this.InstituicaoNivelTipoOrientacaoDomainService.SalvarInstituicaoNivelTipoOrientacao(modelo.Transform<InstituicaoNivelTipoOrientacao>());
        }

        /// <summary>
        /// Busca os tipos de orientação que atendam aos filtros informados
        /// </summary>
        /// <param name="filtros">Dados dos filtros</param>
        /// <returns>Tipos de orientação ordenados por descrição</returns>
        public List<SMCDatasourceItem> BuscarTiposOrientacaoSelect(InstituicaoNivelTipoOrientacaoFiltroData filtros)
        {
            return this.InstituicaoNivelTipoOrientacaoDomainService.BuscarTiposOrientacaoSelect(filtros.Transform<InstituicaoNivelTipoOrientacaoFiltroVO>());
        }

        /// <summary>
        /// Busca os tipos de orientação que atendam aos filtros informados
        /// Ao corrigir o erro de bind de datasource do Dynamic remover esse metodo e utilizar o BuscarTiposOrientacaoSelect
        /// SMC.SGA.Administrativo.Areas.ALN.Models.IngressanteDynamicModel.TiposOrientacaoPessoaAtuacao
        /// </summary>
        /// <param name="filtros">Dados dos filtros</param>
        /// <returns>Tipos de orientação ordenados por descrição</returns>
        public List<SMCDatasourceItem> BuscarTiposOrientacaoGambiarraBindDynamicIngressanteSelect(InstituicaoNivelTipoOrientacaoGambiarraBindDynamicIngressanteFiltroData filtros)
        {
            return this.InstituicaoNivelTipoOrientacaoDomainService.BuscarTiposOrientacaoSelect(filtros.Transform<InstituicaoNivelTipoOrientacaoFiltroVO>());
        }

        /// <summary>
        /// Busca os tipos de orientação que permite orientação manual
        /// </summary>
        /// <returns>Tipos de orientação select</returns>
        public List<SMCDatasourceItem> BuscarTiposOrientacaoPermiteManutencaoManualSelect()
        {
            return this.InstituicaoNivelTipoOrientacaoDomainService.BuscarTiposOrientacaoPermiteManutencaoManualSelect();
        }

        /// <summary>
        /// Buscar o numero máximo de alunos por orientação
        /// </summary>
        /// <param name="filtro">Filtros a serem selecionados</param>
        /// <returns>O numero de alunos possivel em uma orientação</returns>
        public short? BuscarNumeroMaximoAlunosOrientacao(InstituicaoNivelTipoOrientacaoFiltroData filtro)
        {
            return this.InstituicaoNivelTipoOrientacaoDomainService.BuscarNumeroMaximoAlunosOrientacao(filtro.Transform<InstituicaoNivelTipoOrientacaoFilterSpecification>());
        }

        /// <summary>
        /// Buscar todas os tipos de orientação
        /// </summary>
        /// <param name="filtros">Filtros a serem selecionados</param>
        /// <returns>Lista de tipos de orientação</returns>
        public List<InstituicaoNivelTipoOrientacaoData> BuscarTiposOritencoes(InstituicaoNivelTipoOrientacaoFiltroData filtros)
        {
            return this.InstituicaoNivelTipoOrientacaoDomainService.BuscarTiposOritencoes(filtros.Transform<InstituicaoNivelTipoOrientacaoFilterSpecification>()).TransformList<InstituicaoNivelTipoOrientacaoData>();
        }
    }
}