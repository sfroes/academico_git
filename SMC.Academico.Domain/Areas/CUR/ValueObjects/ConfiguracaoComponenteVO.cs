using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Util;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ConfiguracaoComponenteVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public long SeqNivelEnsino { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoComplementar { get; set; }

        public bool Ativo { get; set; }

        public IList<ConfiguracaoComponenteDivisaoVO> DivisoesComponente { get; set; }

        public bool ExibirItensOrganizacao { get; set; }

        public short? ComponenteCurricularCargaHoraria { get; set; }

        public short? ComponenteCurricularCredito { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public IEnumerable<string> ComponenteCurricularEntidadesSigla { get; set; }

        public IEnumerable<string> EntidadeResponsavelOfertaMatrizSigla { get; set; }

        /// <summary>
        /// [Código da configuração] + "-" + [Descrição] + "-" + [Descrição complementar] + "-" + [Carga horária do componente curricular] 
        /// + [label parametrizado*] + "-" + [Créditos do componente curricular] + "Créditos" + "-"  
        /// + [Lista de siglas das entidades responsáveis do componente separadas por "/", ordenadas alfabeticamente]. 
        /// </summary>
        public string ConfiguracaoComponenteDescricaoCompleta
        {
            get
            {
                var result = $"{Codigo}";

                if (!string.IsNullOrEmpty(Descricao))
                    result += $" - {Descricao}";

                if (!string.IsNullOrEmpty(DescricaoComplementar))
                    result += $" - {DescricaoComplementar}";

                if (ComponenteCurricularCargaHoraria.HasValue)
                    result += $" - {ComponenteCurricularCargaHoraria.Value}";

                if (FormatoCargaHoraria.HasValue && FormatoCargaHoraria.Value != Common.Areas.CUR.Enums.FormatoCargaHoraria.Nenhum)
                    result += $" {SMCEnumHelper.GetDescription(FormatoCargaHoraria)}";

                if (ComponenteCurricularCredito.HasValue)
                {
                    var credito = ComponenteCurricularCredito.Value > 1 ? "Créditos" : "Crédito";
                    result += $" - {ComponenteCurricularCredito.Value} {credito}";
                }
                if (ComponenteCurricularEntidadesSigla != null && ComponenteCurricularEntidadesSigla.Any(x => x != null))
                    result += $" - {string.Join(" / ", ComponenteCurricularEntidadesSigla.OrderBy(x => x))}";

                return result;
            }
        }

        /// <summary>
        /// - A cada configuração de componente associada à turma, salvar uma descrição conforme concatenação dos campos:
        /// [Descrição da configuração do componente] + "-" + [Descrição complementar da configuração do componente*] 
        /// + ":**" + [Descrição do assunto de componente**] + "-" + [Carga horária do componente curricular referente à configuração] 
        /// + [label parametrizado***] + "-" + [Créditos do componente curricular referente à configuração] + "Créditos" + "-" 
        /// + [Sigla da entidade responsável pelas ofertas de matriz associadas a turma****]
        /// </summary>
        public string ConfiguracaoComponenteDescricaoTurma { get; set; }

        public bool PermiteAlunoSemNota { get; set; }

        public string DescricaoAssuntoComponente { get; set; }

        public bool Principal { get; set; }

        #region [ Desativado ]

        /// <summary>
        /// - A cada configuração de componente associada à turma, salvar uma descrição conforme concatenação dos campos:
        /// [Descrição da configuração do componente] + "-" + [Descrição complementar da configuração do componente*] 
        /// + ":**" + [Descrição do assunto de componente**] + "-" + [Carga horária do componente curricular referente à configuração] 
        /// + [label parametrizado***] + "-" + [Créditos do componente curricular referente à configuração] + "Créditos" + "-" 
        /// + [Sigla da entidade responsável pelas ofertas de matriz associadas a turma****]
        /// 
        /// * A descrição complementar da configuração de componente pode ser nula
        /// ** A turma pode ou não ter assunto associado.Em caso de não ter assunto, ":" não deve ser exibido.
        /// *** O label parametrizado é o conteúdo do campo Formato em Parâmetros por Instituição e Nível de Ensino, para o 
        /// tipo do componente referente à configuração.
        /// **** Para buscar a entidade responsável pela oferta de matriz é necessário:
        /// 1. Buscar o tipo de entidade parametrizada para o tipo de componente e o nível de ensino responsável pelo componente referente 
        /// à configuração principal da turma;
        /// 2. Buscar na hierarquia de entidade qual a entidade responsável pela oferta de curso por localidade e turno de todas as 
        /// ofertas de matriz associadas à turma Esta busca deve retornar uma entidade do tipo de entidade encontrado no item 1;
        /// 3. Concatenar as siglas das entidades encontradas no item 2, sem repetições.Caso exista mais de uma entidade responsável, 
        /// concatenar a sigla de todas elas, separando por "/", em ordem alfabética.
        /// 
        /// - Na ausência da carga horária ou do crédito, retirar os labels: [label parametrizado] e "Créditos". 
        /// </summary>
        /// <returns></returns>
        private string FormatarDescricaoConfiguracaoTurma()
        {
            // Validações de textos
            var DescricaoComplementarConfiguracaoComponente = string.IsNullOrEmpty(DescricaoComplementar) ? "" : $" - {DescricaoComplementar.Trim()}: ";
            var LabelParametrizado = FormatoCargaHoraria?.SMCGetDescription();
            var Credito = ComponenteCurricularCredito.HasValue && ComponenteCurricularCredito > 1 ? "Créditos" : "Crédito";
            var Siglas = EntidadeResponsavelOfertaMatrizSigla.SMCAny(x => !string.IsNullOrEmpty(x)) ? $" - {string.Join(" / ", EntidadeResponsavelOfertaMatrizSigla.OrderBy(x => x))}" : "";
            var DescricaoAssuntoComponenteTemp = string.IsNullOrEmpty(DescricaoAssuntoComponente) ? " " : $": {DescricaoAssuntoComponente.Trim()} - ";

            var descricaoFormatada = $"{Descricao.Trim()}{DescricaoComplementarConfiguracaoComponente}{DescricaoAssuntoComponenteTemp}{ComponenteCurricularCargaHoraria} " +
                                     $"{LabelParametrizado} - {ComponenteCurricularCredito} {Credito}{Siglas}";

            // Na ausência da carga horária ou do crédito, retirar os labels: [label parametrizado] e "Créditos".
            if (!ComponenteCurricularCargaHoraria.HasValue || !ComponenteCurricularCredito.HasValue)
            {
                descricaoFormatada = $"{Descricao.Trim()}{DescricaoComplementarConfiguracaoComponente}{DescricaoAssuntoComponenteTemp}{ComponenteCurricularCargaHoraria}{Siglas.Trim()}";
            }

            return descricaoFormatada;
        }

        #endregion [ Desativado ]
    }
}