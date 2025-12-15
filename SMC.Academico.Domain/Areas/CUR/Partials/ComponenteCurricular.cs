using SMC.Academico.Common.Areas.CUR.Enums;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.Models
{
    public partial class ComponenteCurricular
    {
        /// <summary>
        /// Recupera o sequencial nível de ensino responsável
        /// </summary>
        /// <returns>Sequencial do nível de ensino responsável</returns>
        public long RecuperarSeqNivelEnsinoResponsavel()
        {
            return this.NiveisEnsino?.SingleOrDefault(s => s.Responsavel)?.SeqNivelEnsino ?? 0;
        }

        /// <summary>
        /// Recupera o nome do nível de ensino responsável
        /// </summary>
        /// <returns>Descrição do nível de ensino</returns>
        public string RecuperarDescricaoNivelEnsinoResponsavel()
        {
            return this.NiveisEnsino?.SingleOrDefault(s => s.Responsavel)?.NivelEnsino.Descricao;
        }

        /// <summary>
        /// Recupera os tipos de gestão das divisões do tipo do componente
        /// </summary>
        /// <returns>Tipos de gestão das divisões do tipo do componente</returns>
        public List<TipoGestaoDivisaoComponente> RecuperarTipoGestaoDivisaoComponente()
        {
            return this.TipoComponente?.TiposDivisao?.Select(s => s.TipoGestaoDivisaoComponente).Distinct().ToList();
        }
    }
}