using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORT.ValueObjects;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.UnitOfWork;
using SMC.Notificacoes.Common.Areas.NTF.Enums;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ORT.DomainServices
{
    public class MudancaTipoTrabalhoAcademicoJob : SMCWebJobBase<MudancaTipoTrabalhoAcademicoSATVO, ListaTrabalhoAcademicoVO>
    {

        private PublicacaoBdpDomainService PublicacaoBdpDomainService => Create<PublicacaoBdpDomainService>();
        private EntidadeConfiguracaoNotificacaoDomainService EntidadeConfiguracaoNotificacaoDomainService => Create<EntidadeConfiguracaoNotificacaoDomainService>();
        private INotificacaoService NotificacaoService => this.Create<INotificacaoService>();

        public override ICollection<ListaTrabalhoAcademicoVO> GetItems(MudancaTipoTrabalhoAcademicoSATVO filtro)
        {
            var trabalhos = new List<ListaTrabalhoAcademicoVO>();
            try
            {
                var seqConfiguracaoNotificacao = EntidadeConfiguracaoNotificacaoDomainService.BuscarSeqConfiguracaoNotificacaoAtivo(filtro.SeqInstituicaoEnsino, TOKEN_TIPO_NOTIFICACAO.MUDANCA_TIPO_PUBLICACAO);
                if (seqConfiguracaoNotificacao == 0)
                    throw new Exception("Não existe configuração de notificação para mudança de publicação para a instituição de ensino informada!");

                var dataFormatada = DateTime.Today.ToString("yyyy-MM-dd");
                if (DateTime.TryParse(filtro.DataProcessamento, out DateTime result))
                {
                    dataFormatada = result.ToString("yyyy-MM-dd");
                }

                Scheduler.LogSucess($"Recuperando trabalhos com mudança no tipo de publicação de trabalho para a data: {dataFormatada}.");

                trabalhos = PublicacaoBdpDomainService.BuscarTrabalhoComMudancaTipoTrabalho(filtro.SeqInstituicaoEnsino, dataFormatada);

                if (trabalhos.Count > 0)
                {
                    trabalhos.SMCForEach(f => f.SeqConfiguracaoNotificacao = seqConfiguracaoNotificacao);
                    Scheduler.LogSucess($"Foram encontrados {trabalhos.Count} trabalhos para processamento.");
                }
                else
                {
                    if (DateTime.TryParse(dataFormatada, out DateTime data))
                        dataFormatada = data.ToString("dd/MM/yyyy");

                    Scheduler.LogSucess($"Nenhum trabalho atendeu aos critérios para a data: {dataFormatada}.");
                }
            }
            catch (Exception ex)
            {
                Scheduler.LogError("Não existe configuração de notificação para mudança de publicação para a instituição de ensino informada!");
            }

            return trabalhos;
        }

        public override bool ProcessItem(ListaTrabalhoAcademicoVO item)
        {
            try
            {
                Scheduler.LogSucess($"Encaminhado notificação do trabalho do aluno: {item.NomeAutor}, título {item.Titulo}.");

                var dadosMerge = new Dictionary<string, string>
                 {
                    { "{{PROGRAMA}}", item.NomeEntidadeVinculo },
                    { "{{NOM_PESSOA}}", item.NomeAutor },
                    { "{{TITULO}}", item.Titulo},
                    { "{{TIPO_AUTORIZACAO}}", item.TipoAutorizacao }
                 };

                var data = new NotificacaoEmailData()
                {
                    SeqConfiguracaoNotificacao = item.SeqConfiguracaoNotificacao,
                    DadosMerge = dadosMerge,
                    DataPrevistaEnvio = DateTime.Now,
                    PrioridadeEnvio = PrioridadeEnvio.QuandoPossivel,
                };

                using (var transacao = SMCUnitOfWork.Begin())
                {
                    long seqNotificacaoEnviada = this.NotificacaoService.SalvarNotificacao(data);
                    transacao.Commit();
                }

                Scheduler.LogSucess($"Notificação enviada para o trabalho do aluno {item.NomeAutor}");
                return true;
            }
            catch (Exception ex)
            {
                Scheduler.LogWaring($"Ocorreu um erro ao tentar enviar a notificação do trabalho do aluno: {item.NomeAutor}  Erro: {ex}");
                return false;
            }
        }
    }
}
