using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class InstituicaoNivelTipoGrupoCurricularDomainService : AcademicoContextDomain<InstituicaoNivelTipoGrupoCurricular>
    {
        #region [ DomainServices ]

        private InstituicaoNivelDomainService InstituicaoNivelDomainService
        {
            get { return this.Create<InstituicaoNivelDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Busca todos os Tipos de Grupos Curriculares associados a um Nível de Ensino na Instituição atual
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do Nível de Ensino</param>
        /// <returns>Dados dos Níveis de Ensino do Nível informado na Instituição atual</returns>
        public List<SMCDatasourceItem> BuscarTipoGrupoCurricularNivelEnsinoSelect(long seqNivelEnsino)
        {
            var spec = new InstituicaoNivelTipoGrupoCurricularFilterSpecification() { SeqNivelEnsino = seqNivelEnsino };
            return this.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem() { Seq = p.TipoGrupoCurricular.Seq, Descricao = p.TipoGrupoCurricular.Descricao })
                .ToList();
        }

        /// <summary>
        /// Busca os Formatos de Configuração de Grupo respeitando a configuração de Nível de Ensino da Instituição
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Dados dos Formatos de Configuração de Grupo Curricular disponíveis na instituição</returns>
        public List<SMCDatasourceItem> BuscarFormatosConfiguracaoGrupoPorNivelEnsinoDaInstituicaoSelect(long seqNivelEnsino)
        {
            var spec = new InstituicaoNivelFilterSpecification { SeqNivelEnsino = seqNivelEnsino };
            var instituicaoNivel = this.InstituicaoNivelDomainService.SearchByKey(spec) ?? new InstituicaoNivel();

            // Retorna o o formato Crédito apenas se o Nível de Ensino informado permitir crédido na configuração da instituição
            return GetEnumItens<FormatoConfiguracaoGrupo>()
                .Where(w => w != FormatoConfiguracaoGrupo.Credito || instituicaoNivel.PermiteCreditoComponenteCurricular)
                .Select(s => new SMCDatasourceItem() { Seq = (long)s, Descricao = SMCEnumHelper.GetDescription(s) })
                .ToList();
        }

        /// <summary>
        /// Recupera os itens não ignorados de um enum
        /// </summary>
        /// <typeparam name="T">Tipo do enum</typeparam>
        /// <returns>Enumerador com os tipos não ignorados</returns>
        private IEnumerable<T> GetEnumItens<T>()
        {
            Type enumType = typeof(T);
            foreach (var item in Enum.GetValues(enumType))
            {
                if (!SMCEnumHelper.IsIgnored(enumType, item.ToString()))
                    yield return (T)item;
            }
        }
    }
}
