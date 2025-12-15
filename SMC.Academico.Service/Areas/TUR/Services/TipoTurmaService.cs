using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.TUR.Services
{
    public class TipoTurmaService : SMCServiceBase, ITipoTurmaService
    {
        #region [ DomainService ]

        private TipoTurmaDomainService TipoTurmaDomainService
        {
            get { return this.Create<TipoTurmaDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca o tipo de turma pelo sequencial
        /// </summary>
        /// <param name="seq"></param>
        /// <returns>Registro tipo de turma</returns>
        public TipoTurmaData BuscarTipoTurma(long seq)
        {
            return TipoTurmaDomainService.BuscarTipoTurma(seq).Transform<TipoTurmaData>();
        }

        /// <summary>
        /// Busca o select de tipo de turma
        /// </summary>
        /// <returns>Lista de tipos de turma</returns>
        public List<SMCDatasourceItem> BuscarTiposTurmasSelect()
        {
            return TipoTurmaDomainService.BuscarTiposTurmasSelect();
        }

        /// <summary>
        /// Busca o select de tipo de turma curricular de acordo com o seqConfiguracaoComponente para obter o instituição nível
        /// </summary>
        /// <param name="seqConfiguracaoComponente">Sequencial da configuração do componente selecionado</param>
        /// <returns>Lista de tipos de turma com instituição nível associado</returns>
        public List<SMCDatasourceItem> BuscarTiposTurmasPorConfiguracaoComponenteSelect(long seqConfiguracaoComponenteHidden)
        {
            return TipoTurmaDomainService.BuscarTiposTurmasPorConfiguracaoComponenteSelect(seqConfiguracaoComponenteHidden);
        }

        /// <summary>
        /// Verifica se existe associação do tipo de turma com a configuração de componente selecionada como principal
        /// </summary>
        /// <param name="seqTipoTurma"></param>
        /// <param name="seqConfiguracaoComponente"></param>
        /// <returns>Retorna se exite associação</returns>
        public bool VerificarTipoTurmaInstituicaoNivel(long seqTipoTurma, long seqConfiguracaoComponente)
        {
            return TipoTurmaDomainService.VerificarTipoTurmaInstituicaoNivel(seqTipoTurma, seqConfiguracaoComponente);
        }

        /// <summary>
        /// RN 39 - Se o tipo de turma estiver parametrizado para não permitir associação de oferta de matriz, verirficar se a
        /// configuração principal é de um componente que exige associação de assunto de componete.Caso seja, abortar a
        /// operação e enviar a seguinte mensagem: "Não é possível prosseguir. Para o tipo de turma em questão, é necessário selecionar uma configuração de
        /// componente que seja de um componente que não exige associação de assunto de componente."
        /// </summary>
        /// <param name="seqTipoTurma"></param>
        /// <param name="seqConfiguracaoComponente"></param>
        /// <returns>Retorna se permite este tipo de turma com a configuração principal</returns>
        public bool VerificarTipoTurmaConfiguracaoAssunto(long seqTipoTurma, long seqConfiguracaoComponente)
        {
            return TipoTurmaDomainService.VerificarTipoTurmaConfiguracaoAssunto(seqTipoTurma, seqConfiguracaoComponente);
        }

        /// <summary>
        /// Busca tipos de turma, conforme o parâmetro
        /// </summary>
        /// <returns>Lista de tipos de turma</returns>
        public List<SMCDatasourceItem> BuscarTiposTurmasSelectPorNivelEnsino(long seqNivelEnsino)
        {
            return TipoTurmaDomainService.BuscarTiposTurmasSelectPorNivelEnsino(seqNivelEnsino);
        }

        public List<TipoTurmaData> BuscarTiposTurmasPorComponenteCurricular(long seqComponenteCurricular, List<long> seqsMatrizesCurricularesOferta)
        {
            return TipoTurmaDomainService.BuscarTiposTurmasPorComponenteCurricular(seqComponenteCurricular, seqsMatrizesCurricularesOferta).TransformList<TipoTurmaData>();
        }
    }
}
