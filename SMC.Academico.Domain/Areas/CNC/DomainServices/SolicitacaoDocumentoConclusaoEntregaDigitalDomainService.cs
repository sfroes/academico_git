using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework;
using System.Net;
using System.Net.Sockets;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class SolicitacaoDocumentoConclusaoEntregaDigitalDomainService : AcademicoContextDomain<SolicitacaoDocumentoConclusaoEntregaDigital>
    {
        public long SalvarLogDownloadDocumentoDigital(long seqSolicitacaoDocumentoConclusao, TipoArquivoDigital tipoArquivoDigital)
        {
            var dominio = new SolicitacaoDocumentoConclusaoEntregaDigital()
            {
                SeqSolicitacaoDocumentoConclusao = seqSolicitacaoDocumentoConclusao,
                TipoArquivoDigital = tipoArquivoDigital,
            };

            string ipAcesso = SMCContext.ClientAddress.Ip;
            string server = SMCContext.HostName;

            if (string.IsNullOrEmpty(ipAcesso))
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAcesso = ip.ToString();
                    }
                }
            }

            ipAcesso = ipAcesso ?? "???.???.???.???";

            dominio.EnderecoIP = ipAcesso;
            dominio.NomeServidorHost = server;

            this.SaveEntity(dominio);

            return dominio.Seq;
        }
    }
}
