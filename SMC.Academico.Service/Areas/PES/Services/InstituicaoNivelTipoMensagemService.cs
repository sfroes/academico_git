using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System;
using SMC.Academico.Domain.Areas.PES.Specifications;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class InstituicaoNivelTipoMensagemService : SMCServiceBase, IInstituicaoNivelTipoMensagemService
    {
        #region [ DomainService ]

        private InstituicaoNivelTipoMensagemDomainService InstituicaoNivelTipoMensagemDomainService
        {
            get { return Create<InstituicaoNivelTipoMensagemDomainService>(); }
        }

        #endregion

        public InstituicaoNivelTipoMensagemData BuscarInstituicaoNivelTipoMensagem(long seq)
        {
            return SMCMapperHelper.Create<InstituicaoNivelTipoMensagemData>(InstituicaoNivelTipoMensagemDomainService.SearchByKey(new SMCSeqSpecification<InstituicaoNivelTipoMensagem>(seq)));
        }

        public string BuscarMensagem(long seq)
        {
            return InstituicaoNivelTipoMensagemDomainService.SearchProjectionByKey(new SMCSeqSpecification<InstituicaoNivelTipoMensagem>(seq), a => a.MensagemPadrao);
        }

        /// <summary>
        /// Listar, para seleção, os tipos de mensagens associados à instituição e nível de ensino da pessoa em questão
        /// e que possuem o mesmo tipo de atuação da pesssoa em questão.
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da Pessoa Atuação para consultar o Tipo de Atuação.</param>
        /// <param name="apenasCadastroManual">Considerar apenas tipos considerados para cadastro manual ou não</param>
        /// <returns>Lista de Tipos de Mensagens.</returns>
        public List<SMCDatasourceItem> BuscarTiposMensagemSelect(long seqPessoaAtuacao, bool apenasCadastroManual)
        {
            return InstituicaoNivelTipoMensagemDomainService.BuscarTiposMensagemSelect(seqPessoaAtuacao, apenasCadastroManual);
        }

        /// <summary>
        /// Lista os InstituicaoNivelTipoMensagem pelo Seq de um Tipo de Mensagem.
        /// </summary>
        /// <param name="seqTipoMensagem">Sequencial da entidade TipoMensagem</param>
        /// <param name="permiteCadastroManual">Flag para informar se retorna ou não apenas tipos que mensagens que permitem cadastro manual.</param>
        /// <returns>Lista de InstituicaoNivelTipoMensagem</returns>
        public List<InstituicaoNivelTipoMensagemData> BuscarInstituicaoNivelTipoMensagens(InstituicaoNivelTipoMensagemFiltroData filtro)
        {
            return InstituicaoNivelTipoMensagemDomainService.BuscarInstituicaoNivelTipoMensagens(filtro.Transform<InstituicaoNivelTipoMensagemFilterSpecification>()).TransformList<InstituicaoNivelTipoMensagemData>();
        }

        public InstituicaoNivelTipoMensagemData BuscarInstituicaoNivelTipoMensagem(InstituicaoNivelTipoMensagemFiltroData filtro)
        {
            return InstituicaoNivelTipoMensagemDomainService.BuscarInstituicaoNivelTipoMensagem(filtro.Transform<InstituicaoNivelTipoMensagemFilterSpecification>()).Transform<InstituicaoNivelTipoMensagemData>();
        }

        public InstituicaoNivelTipoMensagemData BuscarInstituicaoNivelTipoMensagemSemRefinar(InstituicaoNivelTipoMensagemFiltroData filtro)
        {
            return InstituicaoNivelTipoMensagemDomainService.BuscarInstituicaoNivelTipoMensagemSemRefinar(filtro.Transform<InstituicaoNivelTipoMensagemFilterSpecification>()).Transform<InstituicaoNivelTipoMensagemData>();
        }
    }
}
