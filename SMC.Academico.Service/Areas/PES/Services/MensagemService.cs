using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class MensagemService : SMCServiceBase, IMensagemService
    {
        #region [ DomainServices ]

        private MensagemDomainService MensagemDomainService
        {
            get { return Create<MensagemDomainService>(); }
        }

        #endregion [ DomainServices ]

        public SMCPagerData<MensagemListaData> ListarMensagens(MensagemFiltroData filtro)
        {
            var spec = filtro.Transform<MensagemFilterSpecification>();

            spec.SetOrderByDescending(x => x.DataInclusao);

            var mensagens = MensagemDomainService.SearchProjectionBySpecification(spec, x => new MensagemListaData
            {
                DataInicioVigencia = x.DataInicioVigencia,
                Mensagem = x.Descricao,
                ClasseCss = x.TipoMensagem.ClasseCss,
            }, out int total);
            return new SMCPagerData<MensagemListaData>(mensagens, total);
        }
    }
}