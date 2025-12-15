using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.DomainServices;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Data;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using SMC.Framework.Specification;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class ProgramaPropostaService : SMCServiceBase, IProgramaPropostaService
    {
        #region [ Serviços ]

        private FormularioConfiguracaoDomainService FormularioConfiguracaoDomainService
        {
            get { return this.Create<FormularioConfiguracaoDomainService>(); }
        }

        private ProgramaPropostaDomainService ProgramaPropostaDomainService
        {
            get { return this.Create<ProgramaPropostaDomainService>(); }
        }

        private NivelEnsinoDomainService NivelEnsinoDomainService
        {
            get { return this.Create<NivelEnsinoDomainService>(); }
        }

        #endregion [ Serviços ]

        public ProgramaPropostaData BuscarConfiguracoesFormularioProposta()
        {
            var configuracao = FormularioConfiguracaoDomainService.BuscarConfiguracaoFormularioProposta(TOKEN_FORMULARIO_CONFIGURACAO.PROPOSTA_PROGRAMA);

            var modelProposta = new ProgramaPropostaData()
            {
                DadoFormulario = new SGADadoFormularioData()
                {
                    SeqFormulario = configuracao.SeqFormularioSgf,
                    SeqVisao = configuracao.SeqVisaoSgf,
                    SeqFormularioConfiguracao = configuracao.Seq
                }
            };

            modelProposta.SeqsNiveisEnsino = NivelEnsinoDomainService.BuscarSeqsNiveisEnsinoStrictoSensu();

            return modelProposta;
        }

        public ProgramaPropostaData BuscarProgramaProposta(long seq)
        {
            return ProgramaPropostaDomainService.SearchByKey(new SMCSeqSpecification<ProgramaProposta>(seq), IncludesProgramaProposta.DadoFormulario | IncludesProgramaProposta.DadoFormulario_DadosCampos).Transform<ProgramaPropostaData>();
        }

        public long SalvarProgramaProposta(ProgramaPropostaData model)
        {
            return ProgramaPropostaDomainService.SalvarProgramaProposta(model.Transform<ProgramaProposta>());
        }
    }
}