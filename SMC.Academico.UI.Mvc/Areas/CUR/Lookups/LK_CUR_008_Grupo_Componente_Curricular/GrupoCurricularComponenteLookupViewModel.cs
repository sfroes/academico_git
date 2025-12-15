using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.Util;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class GrupoCurricularComponenteLookupViewModel : SMCViewModelBase, ISMCLookupData, ISMCMappable
    {
        #region [ Comum ]

        /// <summary>
        /// Será substituído por um "index" da tree para possibilitar listar GruposCurriculares e ComponentesCurriculares na mesma tree
        /// </summary>
        [SMCKey]
        public long Seq { get; set; }

        /// <summary>
        /// Quando preenchido o registro representa um GrupoCurricular
        /// </summary>
        [SMCHidden]
        public long? SeqGrupoCurricular { get; set; }

        /// <summary>
        /// Quando preenchido o registro representa um GrupoCurricularComponente
        /// </summary>
        [SMCHidden]
        public long? SeqGrupoComponenteCurricular { get; set; }

        /// <summary>
        /// Sequencial do GrupoCurricular no domínio
        /// </summary>
        [SMCHidden]
        public long? SeqGrupoCurricularSuperior { get; set; }

        /// <summary>
        /// "Index" do nó pai
        /// </summary>
        [SMCHidden]
        [SMCParameter]
        public long? SeqPai { get; set; }

        /// <summary>
        /// Descrição do GrupoCurricular quando for grupo ou do Componente quando for GrupoCurricularComponente
        /// </summary>
        [SMCHidden]
        public string Descricao { get; set; }

        /// <summary>
        /// Descrição do tipo de configuração GrupoCurricular quando for grupo ou do Componente quando for GrupoCurricularComponente
        /// </summary>
        [SMCHidden]
        public string TipoConfiguracaoDescricao { get; set; }

        /// <summary>
        /// Enumerador com o formato para o grupo curricular
        /// </summary>
        [SMCHidden]
        public FormatoConfiguracaoGrupo? FormatoConfiguracaoGrupo { get; set; }

        /// <summary>
        /// Quantidade de itens para o grupo quando o formato selecionado é itens
        /// </summary>
        [SMCHidden]
        public short? QuantidadeItens { get; set; }

        /// <summary>
        /// Quantidade de hora aula para o grupo quando o formato selecionado é hora
        /// </summary>
        [SMCHidden]
        public short? QuantidadeHoraAula { get; set; }

        /// <summary>
        /// Quantidade de hora relogio para o grupo quando o formato selecionado é hora
        /// </summary>
        [SMCHidden]
        public short? QuantidadeHoraRelogio { get; set; }

        /// <summary>
        /// Quantidade de crédito para o grupo quando o formato selecionado é crédito
        /// </summary>
        [SMCHidden]
        public short? QuantidadeCreditos { get; set; }

        /// <summary>
        /// Formata a descrição segundo o tipo.
        /// Para Grupos apresenta a descrição é composta dos seguintes campos concatenados:
        /// [Descrição do grupo curricular] + "-" + [Descrição do tipo de configuração] + "-" + [Carga horária/Créditos/Quantidade]* + [Label: horas e horas-aulas/créditos/itens]*.
        /// Para Componente apresenta a descrição é composta dos seguintes campos concatenados:
        ///     [Código do componente] + "-" + [Descrição] + "-" + [Carga horária] + [label parametrizado] + "-" + [Créditos] + "Créditos".
        /// </summary>
        [SMCDescription]
        public string DescricaoFormatada
        {
            get
            {
                string retorno = string.Empty;

                if (!string.IsNullOrEmpty(Codigo) && !SeqGrupoCurricular.HasValue)
                    retorno = Codigo;

                if (!string.IsNullOrEmpty(Descricao?.Trim()))
                    if (retorno.Length > 0)
                        retorno += $" - {Descricao.Trim()}";
                    else
                        retorno = Descricao.Trim();

                if (!string.IsNullOrEmpty(TipoConfiguracaoDescricao) && SeqGrupoCurricular.HasValue)
                    if (retorno.Length > 0)
                        retorno += $" - {TipoConfiguracaoDescricao}";
                    else
                        retorno = TipoConfiguracaoDescricao;

                string formatoTipo = string.Empty;
                switch (FormatoConfiguracaoGrupo)
                {
                    case Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.CargaHoraria:
                        if (QuantidadeHoraAula.HasValue)
                            formatoTipo = $": {QuantidadeHoraAula} horas-aulas";
                        else
                            if (QuantidadeHoraRelogio.HasValue)
                            formatoTipo = $": {QuantidadeHoraRelogio} horas";
                        break;

                    case Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.Credito:
                        if (QuantidadeCreditos.HasValue)
                            formatoTipo = $": {QuantidadeCreditos} créditos";
                        break;

                    case Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.Itens:
                        if (QuantidadeItens.HasValue)
                            formatoTipo = $": {QuantidadeItens} itens";
                        break;

                    default:
                        break;
                }

                if (formatoTipo.Length > 0 && SeqGrupoCurricular.HasValue)
                    if (retorno.Length > 0)
                        retorno += $" : {formatoTipo}";
                    else
                        retorno = formatoTipo;

                if (CargaHoraria.HasValue && Formato.HasValue && !SeqGrupoCurricular.HasValue)
                    if (retorno.Length > 0)
                        retorno += $" - {CargaHoraria} - {SMCEnumHelper.GetDescription(Formato)}";
                    else
                        retorno = $"{CargaHoraria} - {SMCEnumHelper.GetDescription(Formato)}";

                if (Credito.HasValue && !SeqGrupoCurricular.HasValue)
                    if (retorno.Length > 0)
                        retorno += $" - {Credito} - créditos";
                    else
                        retorno = $"{Credito} - créditos";

                // Caso seja branco retorna nulo para o lookup recalcular a descrição
                return string.IsNullOrWhiteSpace(retorno) ? null : retorno;
            }
        }

        [SMCHidden]
        public string DescricaoGrupoCurricular { get; set; }

        #endregion [ Comum ]

        #region [ Visibilidade ]

        /// <summary>
        /// Indica se o GrupoCurricular têm Grupos Curriculares filhos
        /// </summary>
        [SMCHidden]
        public bool ContemGrupos { get; set; }

        /// <summary>
        /// Indica se o GrupoCurricular têm Componentes filhos
        /// </summary>
        [SMCHidden]
        public bool ContemComponentes { get; set; }

        #endregion [ Visibilidade ]

        #region [ Componente ]

        /// <summary>
        /// Código do Componente
        /// </summary>
        [SMCHidden]
        public string Codigo { get; set; }

        /// <summary>
        /// Carga Horária do Componente
        /// </summary>
        [SMCHidden]
        public short? CargaHoraria { get; set; }

        /// <summary>
        /// Descrição do formato do componente
        /// </summary>
        [SMCHidden]
        public FormatoCargaHoraria? Formato { get; set; }

        /// <summary>
        /// Créditos do componente
        /// </summary>
        [SMCHidden]
        public short? Credito { get; set; }

        /// <summary>
        /// O Componente tem comportamento de nó folha sem poder ser selecionado
        /// </summary>
        [SMCHidden]
        public bool Folha { get; set; }

        #endregion [ Componente ]
    }
}