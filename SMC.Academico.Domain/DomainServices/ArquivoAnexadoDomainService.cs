using SMC.Academico.Domain.Models;
using SMC.Academico.Domain.Specifications;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace SMC.Academico.Domain.DomainServices
{
    public class ArquivoAnexadoDomainService : AcademicoContextDomain<ArquivoAnexado>
    {
        public FileInfo BuscarECompactarArquivos(IEnumerable<long> seqs)
        {
            // Cria um diretório temporário para salvar e compactar
            DirectoryInfo dir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));
            if (dir.Exists) dir.Delete(true);
            dir.Create();

            // Recupera os arquivos
            int total = 0;
            foreach (var seqArquivoAnexado in seqs)
            {
                // Recupera o arquivo
                var arquivo = this.SearchProjectionByKey(seqArquivoAnexado, x => new { x.Nome, x.Conteudo });
                if (arquivo != null)
                {
                    total++;

                    // Cria um nome temporário para o arquivo
                    var nomeArquivo = $"{total} - {arquivo.Nome}";
                    var pathArquivo = Path.Combine(dir.FullName, nomeArquivo);

                    // Salva o arquivo no diretório
                    File.WriteAllBytes(pathArquivo, arquivo.Conteudo);
                }
            }

            if (total == 0)
                throw new SMCApplicationException("Nenhum arquivo foi encontrado com os sequenciais informados.");

            // Zipa e retorna para o usuário
            var fileZipado = SMCFileHelper.ZipFolder(dir);

            // Remove o diretório temporário
            dir.Delete(true);

            // Retorna
            return fileZipado;
        }

        public SMCUploadFile BuscarArquivoAnexadoPorGuid(Guid uidArquivo)
        {
            var arquivoAnexado = this.SearchByKey(new ArquivoAnexadoFilterSpecification() { UidArquivo = uidArquivo });
            var retorno = arquivoAnexado.Transform<SMCUploadFile>();

            return retorno;
        }
    }
}