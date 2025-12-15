using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.FIN.Interfaces
{
    public interface IConfiguracaoBeneficioService : ISMCService
    {
        /// <summary>
        /// Buscar configuracao beneficio conforme regra de negocio
        /// </summary>
        /// <param name="filtro">Sequencial da instituicao nivel beneficio</param>
        /// <returns>Lista configurações de beneficios seguindo regra</returns>
        SMCPagerData<ConfiguracaoBeneficioData> BuscarConfiguracoesBeneficios(ConfiguracaoBeneficioFiltroData filtro);

        /// <summary>
        /// Valida se beneficio pode ser configurado 
        /// </summary>
        /// <param name="seqInstituicaoNivelBeneficio">Seq instituicao nivel beneficio</param>
        /// <returns>Retorna execption seguindo a regra de negocio</returns>
        bool VerificarDeducaoBeneficio(long seqInstituicaoNivelBeneficio);

        /// <summary>
        /// Buscar a configuração beneficio referente a uma determinda instiuição nivel beneficio
        /// </summary>
        /// <param name="seqInstituicaoNivelBenefico">Seq intituição nivel beneficio</param>
        /// <returns>As configurações de um determinado nivel beneficio</returns>
        ConfiguracaoBeneficioData BuscarDadosConfiguracaoBeneficio(ConfiguracaoBeneficioData configuracaoBeneficio);

        /// <summary>
        /// Salvar a configuração de beneficio
        /// </summary>
        /// <param name="beneficioHistorico">Modelo configuração beneficio</param>
        /// <returns>Seq configuração beneficio</returns>
        long SalvarConfiguracaoBeneficio(ConfiguracaoBeneficioData configuracaoBeneficio);

        /// <summary>
        /// Excluir Configuração de Beneficio
        /// </summary>
        /// <param name="seq">Seq configuração de beneficio</param>
        void ExcluirConfiguracaoBeneficio(long seq);

        /// <summary>
        /// Verificar se beneficio esta associado a algum ingressante
        /// </summary>
        /// <param name="seq">Seq de configuração beneficio</param>
        /// <returns>Retorna true ou false caso exista alguma associação</returns>
        bool VerificarAssociacaoPessoaBeneficio(long seq);

        ConfiguracaoBeneficioData AlterarConfiguracoesBeneficios(long seq);
    }
}
