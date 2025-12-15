using SMC.Academico.Common.Areas.CSO.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.DomainServices;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class ProgramaPropostaDomainService : AcademicoContextDomain<ProgramaProposta>
    {
        private FormularioConfiguracaoDomainService FormularioConfiguracaoDomainService
        {
            get { return this.Create<FormularioConfiguracaoDomainService>(); }
        }

        public long SalvarProgramaProposta(ProgramaProposta programaProposta)
        {
            // Verifica se já existe alguma proposta para esse programa que tenha o ciclo letivo já utilizado
            var programaPropostaCicloLetivo = this.Count(programaProposta.Transform<ProgramaPropostaCicloLetivoCadastradoFilterSpecification>());
            if (programaPropostaCicloLetivo > 0)
                throw new ProgramaPropostaCicloLetivoExistenteException();

            // FIX: Melhorar isso abaixo
            // Atualiza o Seq do Dado Formulário
            programaProposta.DadoFormulario.Seq = programaProposta.SeqDadoFormulario;

            // Salva
            this.SaveEntity(programaProposta);

            // Retorna o sequencial
            return programaProposta.Seq;
        }
    }
}