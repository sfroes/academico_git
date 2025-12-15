using SMC.Academico.Common.Areas.CAM.Exceptions;
using SMC.Academico.Common.Areas.CAM.Includes;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CAM.Validators;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Calendarios.ServiceContract.Areas.CLD.Interfaces;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Specification;
using SMC.Framework.Util;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CAM.DomainServices
{
    public class InstituicaoTipoEventoDomainService : AcademicoContextDomain<InstituicaoTipoEvento>
    {
        #region DomainServices

        private EventoLetivoAgdDomainService EventoLetivoAgdDomainService
        {
            get { return this.Create<EventoLetivoAgdDomainService>(); }
        }

        #endregion DomainServices

        #region Services

        private ITipoEventoService TipoEventoService
        {
            get { return this.Create<ITipoEventoService>(); }
        }

        #endregion Services

        public long? BuscarSeqTipoEventoPorToken(string token, long seqInstituicaoEnsino)
        {
            return SearchProjectionByKey(new InstituicaoTipoEventoFilterSpecification() { Token = token, SeqInstituicaoEnsino = seqInstituicaoEnsino }, 
                                            x => x.SeqTipoEventoAgd);
        }

        public long SalvarInstituicaoTipoEvento(InstituicaoTipoEventoVO modelo)
        {
            ValidarInstituicaoTipoEvento(modelo);

            var instituicaoTipoEvento = modelo.Transform<InstituicaoTipoEvento>();

            this.SaveEntity(instituicaoTipoEvento, new InstituicaoTipoEventoValidator());

            return instituicaoTipoEvento.Seq;
        }

        private void ValidarInstituicaoTipoEvento(InstituicaoTipoEventoVO instituicaoTipoEventoVO)
        {
            //Caso seja alteração de registro
            if (instituicaoTipoEventoVO.Seq > 0)
            {
                var instituicaoTipoEventoAtual = this.SearchByKey(new SMCSeqSpecification<InstituicaoTipoEvento>(instituicaoTipoEventoVO.Seq),
                                                                    IncludesInstituicaoTipoEvento.CiclosLetivosTipoEvento |
                                                                    IncludesInstituicaoTipoEvento.Parametros |
                                                                    IncludesInstituicaoTipoEvento.TipoAgenda);

                var tipoEventoAgdAtual = this.TipoEventoService.BuscarTipoEvento(instituicaoTipoEventoAtual.SeqTipoEventoAgd);

                //Valida se o tipo de evento já tiver sido parametrizado em algum ciclo letivo.
                if (instituicaoTipoEventoAtual.CiclosLetivosTipoEvento.Any())
                {
                    //Não permitir alterar o tipo de evento do AGD
                    if (instituicaoTipoEventoVO.SeqTipoEventoAgd != instituicaoTipoEventoAtual.SeqTipoEventoAgd)
                        throw new ImpossivelAlterarTipoEventoException(tipoEventoAgdAtual.Descricao);

                    //Não permitir alterar o tipo de abrangência
                    if (instituicaoTipoEventoVO.AbrangenciaEvento != instituicaoTipoEventoAtual.AbrangenciaEvento)
                        throw new ImpossivelAlterarAbrangenciaException();

                    //Não permitir alterar o valor do campo “Somente um por parametrização?”
                    if (instituicaoTipoEventoVO.ApenasUmaParametrizacao != instituicaoTipoEventoAtual.ApenasUmaParametrizacao)
                        throw new ImpossivelAlterarApenasUmaParametrizacaoException();

                    //Não permitir excluir ou alterar um tipo de parâmetro
                    //Não é permitido incluir um novo tipo de parâmetro como obrigatório ou alterar o tipo de parâmetro de
                    //obrigatório “Não” para “Sim”

                    //Recuperando as listas de parâmetros
                    var ListaParametrosAtuais = instituicaoTipoEventoAtual.Parametros;
                    var ListaParametrosEditados = instituicaoTipoEventoVO.Parametros.Where(x => x.Seq > 0);
                    var ListaParametrosAdicionados = instituicaoTipoEventoVO.Parametros.Where(x => x.Seq == 0);

                    //Auxiliar
                    var itemEncontrado = new InstituicaoTipoEventoParametroVO();

                    //Percorre a lista de parâmetros existentes para comparar os itens
                    foreach (var item in ListaParametrosAtuais)
                    {
                        //Recupera o parâmetro da lista de parâmetros editados
                        itemEncontrado = ListaParametrosEditados.FirstOrDefault(x => x.Seq == item.Seq);

                        //Se não encontrou o parâmetro, ele foi excluido
                        if (itemEncontrado == null)
                            throw new ImpossivelExcluirTipoParametroException(SMCEnumHelper.GetDescription(item.TipoParametroEvento));
                        else
                        //Se encontrou o parâmetro, verifica se seu tipo foi alterado
                            if (itemEncontrado.TipoParametroEvento != item.TipoParametroEvento)
                            throw new ImpossivelAlterarTipoParametroException(SMCEnumHelper.GetDescription(item.TipoParametroEvento));

                        //Se encontrou o parâmetro, verifica de a propriedade obrigatório foi alterada se "Sim" para "Não"
                        if (!item.Obrigatorio && itemEncontrado.Obrigatorio)
                            throw new ImpossivelAlterarObrigatorioDeNaoParaSimException(SMCEnumHelper.GetDescription(item.TipoParametroEvento));
                    }

                    //Verifica de a propriedade obrigatório foi selecionada como "Sim" para itens adicionados
                    itemEncontrado = ListaParametrosAdicionados.FirstOrDefault(x => x.Obrigatorio);
                    if (itemEncontrado != null)
                        throw new ImpossivelAlterarObrigatorioDeNaoParaSimException(SMCEnumHelper.GetDescription(itemEncontrado.TipoParametroEvento));
                }

                //Valida se existir evento letivo cadastrado para o tipo de evento AGD.
                if (this.EventoLetivoAgdDomainService.Count(new SMCSeqSpecification<EventoLetivoAgd>(instituicaoTipoEventoVO.SeqTipoEventoAgd)) > 0)
                {
                    //Não deixar trocar o tipo de agenda
                    if (instituicaoTipoEventoVO.SeqTipoAgenda != instituicaoTipoEventoAtual.SeqTipoAgenda)
                        throw new ImpossivelAlterarTipoAgendaException(instituicaoTipoEventoAtual.TipoAgenda.Descricao);
                }
            }
        }
    }
}