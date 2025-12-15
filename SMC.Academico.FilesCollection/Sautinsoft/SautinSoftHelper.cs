using Newtonsoft.Json;
using SMC.Academico.Common.Enums;
using System;
using System.Net;

namespace SMC.Academico.FilesCollection
{
    public class SautinSoftHelper
    {
        public static byte[] MailMergeToPdf(byte[] file, string nameFile, string typeFile, string tags)
        {
            var documento = new MailMergeDocumentFileData()
            {
                File = new DocumentFileData()
                {
                    FileData = file,
                    Name = nameFile,
                    Type = typeFile,
                },
                Fields = tags
            };

            //string requisicaoJson = JsonConvert.SerializeObject(documento, Newtonsoft.Json.Formatting.Indented);

            var result = SauntinsoftResquest.Send<Result<byte[]>>(documento, MetodoHttp.POST, SautinSoftEndPoint.MailMergeDocument);

            if (!result.success)
            {
                throw new Exception(result.errorMessage);
            }

            return result.data;
        }

        public static byte[] ConvertDocumentPdf(byte[] file, string nameFile, string typeFile)
        {
            var documento = new DocumentFileData()
            {
                FileData = file,
                Name = nameFile,
                Type = typeFile,
            };

            //string requisicaoJson = JsonConvert.SerializeObject(documento, Newtonsoft.Json.Formatting.Indented);

            var result = SauntinsoftResquest.Send<Result<byte[]>>(documento, MetodoHttp.POST, SautinSoftEndPoint.ConvertDocument);

            if (!result.success)
            {
                throw new Exception(result.errorMessage);
            }

            return result.data;
        }
    }
}
