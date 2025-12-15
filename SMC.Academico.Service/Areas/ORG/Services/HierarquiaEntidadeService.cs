using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class HierarquiaEntidadeService : SMCServiceBase, IHierarquiaEntidadeService
    {
        #region [ DomainService ]

        public HierarquiaEntidadeDomainService HierarquiaEntidadeDomainService
        {
            get { return this.Create<HierarquiaEntidadeDomainService>(); }
        }

        public TipoEntidadeDomainService TipoEntidadeDomainService
        {
            get { return this.Create<TipoEntidadeDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca as hierarquia de entidade  de acordo com o filtro
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Lista de hierarquias de entidades</returns>
        public SMCPagerData<HierarquiaEntidadeListaData> BuscarHierarquiasEntidade(HierarquiaEntidadeFiltroData filtros)
        {
            return HierarquiaEntidadeDomainService
                .BuscarHierarquiasEntidade(filtros.Transform<HierarquiaEntidadeFiltroVO>())
                .Transform<SMCPagerData<HierarquiaEntidadeListaData>>();
        }

        /// <summary>
        /// Salva a hierarquia de entidade
        /// </summary>
        /// <param name="hierarquiaEntidade">Hierarquia de entidade a ser salva</param>
        /// <returns>Sequencial da hierarquia de entidade salva</returns>
        public long SalvarHierarquiaEntidade(HierarquiaEntidadeData hierarquiaEntidade)
        {
            HierarquiaEntidade hierarquia = SMCMapperHelper.Create<HierarquiaEntidade>(hierarquiaEntidade);
            return HierarquiaEntidadeDomainService.SalvarHierarquiaEntidade(hierarquia);
        }

        /// <summary>
        /// Bsuca uma HierarquiaEntidade através de seu Seq
        /// </summary>
        /// <param name="seqHierarquiaEntidade"></param>
        /// <returns></returns>
        public HierarquiaEntidadeData BuscarHierarquiaEntidade(long seqHierarquiaEntidade)
        {
            return HierarquiaEntidadeDomainService.SearchByKey<HierarquiaEntidade, HierarquiaEntidadeData>(seqHierarquiaEntidade);
        }

        /// <summary>
        /// Busca as possíveis entidades superiories na visão organizacional de um tipo de entidade
        /// para montagem de select
        /// </summary>
        /// <param name="seqTipoEntidade">Sequencial do tipo de entidade</param>
        /// <returns>Lista de possíveis entidades superiores</returns>
        public List<SMCDatasourceItem> BuscarHierarquiaOrganizacionalSuperiorSelect(long seqTipoEntidade, bool apenasAtivas = false)
        {
            return HierarquiaEntidadeDomainService.BuscarEntidadeSuperiorSelect(seqTipoEntidade, TipoVisao.VisaoOrganizacional, apenasAtivas);
        }

        public List<SMCDatasourceItem> BuscarHierarquiaSuperiorSelect(long seqTipoEntidade, TipoVisao tipoiVisao, bool apenasAtivas = false)
        {
            return HierarquiaEntidadeDomainService.BuscarEntidadeSuperiorSelect(seqTipoEntidade, tipoiVisao, apenasAtivas);
        }

        /// <summary>
        /// Busca as possíveis entidades superiories na visão organizacional do tipo entidade CURSO
        /// para montagem de select
        /// </summary>
        /// <param name="seqTipoEntidade">Sequencial do tipo de entidade</param>
        /// <returns>Lista de possíveis entidades superiores</returns>
        public List<SMCDatasourceItem> BuscarHierarquiaOrganizacionalSuperiorProcessoSelect(bool apenasAtivas = false, bool usarNomeReduzido = false)
        {
            var tipoEntidade = this.TipoEntidadeDomainService.BuscarTipoEntidadeNaInstituicao(TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO);

            return HierarquiaEntidadeDomainService.BuscarEntidadeSuperiorSelect(tipoEntidade.Seq, TipoVisao.VisaoOrganizacional, apenasAtivas, usarNomeReduzido);
        }

        public List<SMCDatasourceItem> BuscarHierarquiaOrganizacionalSuperiorAlunoSelect(bool apenasAtivas = false, bool usarNomeReduzido = false, bool usarSeqEntidade = false)
        {
            var tipoEntidade = this.TipoEntidadeDomainService.BuscarTipoEntidadeNaInstituicao(TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO);

            return HierarquiaEntidadeDomainService.BuscarEntidadeSuperiorSelect(tipoEntidade.Seq, TipoVisao.VisaoOrganizacional, apenasAtivas, usarNomeReduzido, usarSeqEntidade, false, true);
        }

        /// <summary>
        /// Busca as entidades folhas para um tipo de entidade em uma visão.
        /// </summary>
        /// <returns>Lista de entidades</returns>
        public List<SMCDatasourceItem> BuscarEntidadesFolhaVisaoSelect()
        {
            return HierarquiaEntidadeDomainService.BuscarEntidadesFolhaVisaoSelect(TipoVisao.VisaoLocalidades, "LOCALIDADE");
        }

        public List<SMCDatasourceItem> BuscarEntidadesHierarquiaSelect()
        {
            return HierarquiaEntidadeDomainService.BuscarEntidadesFolhaVisaoSelect(TipoVisao.VisaoLocalidades, "LOCALIDADE", usarSeqEntidade: true);
        }
    }
}