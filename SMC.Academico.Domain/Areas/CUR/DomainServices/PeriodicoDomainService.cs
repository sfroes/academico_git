using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Framework.Exceptions;
using SMC.Framework.Model;
using SMC.Framework.Spreadsheet.Xlsx;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class PeriodicoDomainService : AcademicoContextDomain<Periodico>
    {
        private static readonly Type _type = typeof(PeriodicoImportacaoVO);

        #region Sevicos

        private ClassificacaoPeriodicoDomainService ClassificacaoPeriodicoDomainService
        {
            get { return this.Create<ClassificacaoPeriodicoDomainService>(); }
        }

        private QualisPeriodicoDomainService QualisPeriodicoDomainService
        {
            get { return this.Create<QualisPeriodicoDomainService>(); }
        }

        #endregion Sevicos

        public List<SMCDatasourceItem> BuscarAreaAvaliacaoSelect(long seqClassificacaoPeriodico)
        {
            var include = IncludesPeriodico.ClassificacaoPeriodico | IncludesPeriodico.QualisPeriodico;

            var spec = new PeriodicoFilterSpecification() { SeqClassificacaoPeriodico = seqClassificacaoPeriodico };

            var periodico = this.SearchBySpecification(spec, include).ToArray();

            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            foreach (var item in periodico)
            {
                foreach (var area in item.QualisPeriodico)
                {
                    if (retorno.Count(a => a.Descricao == area.DescricaoAreaAvaliacao) == 0)
                    {
                        retorno.Add(new SMCDatasourceItem(area.Seq, area.DescricaoAreaAvaliacao));
                    }
                }
            }

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarQualiCapesSelect(long seqClassificacaoPeriodico)
        {
            var spec = new QualisPeriodicoFilterSpecification() { SeqClassificacaoPeriodico = seqClassificacaoPeriodico };
            var qualisCapes = QualisPeriodicoDomainService.SearchProjectionBySpecification(spec, p => p.QualisCapes, true).ToList();

            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            foreach (var item in qualisCapes)
            {
                var descricaoCapes = SMCEnumHelper.GetDescription(item);
                retorno.Add(new SMCDatasourceItem((long)item, descricaoCapes));
            }

            return retorno.OrderBy(o => o.Descricao).ToList();
        }

        public SMCPagerData<Periodico> BuscarPeriodicosCapes(PeriodicoFilterSpecification spec)
        {
            int total = 0;

            var includes = IncludesPeriodico.ClassificacaoPeriodico | IncludesPeriodico.QualisPeriodico;

            var result = this.SearchBySpecification(spec, out total, includes).ToList();

            return new SMCPagerData<Periodico>(result, total);
        }

        public string RemoverAcentos(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";
            else
            {
                byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(input);
                return System.Text.Encoding.UTF8.GetString(bytes).Trim();
            }
        }

        public void SalvarPeriodo(PeriodicoVO periodico)
        {
            ClassificacaoPeriodico modelClassificaPeriodico = new ClassificacaoPeriodico();
            Periodico modelPeriodico = new Periodico();
            QualisPeriodico modelQualisPeriodico = new QualisPeriodico();

            List<PeriodicoImportacaoVO> DadosPlanilha;

            if (periodico.ArquivoAnexado.Type == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                DadosPlanilha = ValidarPlanilhaXLSX(periodico).OrderBy(o => o.Título).ThenBy(th => th.ISSN).ThenBy(th => th.Estrato).ThenBy(th => th.AreaAvaliacao).ToList();
            }
            else
            {
                throw new PeriodicoArquivoTipoInvalidoException();
            }

            if (DadosPlanilha.Count > 0)
            {
                // Iniciando a transacao
                using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
                {
                    modelClassificaPeriodico.AnoInicio = (short)periodico.AnoInicio;
                    modelClassificaPeriodico.AnoFim = (short)periodico.AnoFim;
                    modelClassificaPeriodico.Descricao = periodico.Descricao;
                    modelClassificaPeriodico.Periodicos = new List<Periodico>();
                    modelClassificaPeriodico.Atual = ValidarDatasClassificacaoPeriodico(periodico);

                    ClassificacaoPeriodicoDomainService.SaveEntity(modelClassificaPeriodico);

                    string ultimoLido = string.Empty;

                    var coun = DadosPlanilha.Where(w => w.Título == "Historiae").Count();

                    foreach (var item in DadosPlanilha)
                    {
                        if (ultimoLido != RemoverAcentos(item.Título).ToLower())
                        {
                            // Modelo Periodico
                            modelPeriodico = new Periodico();
                            modelPeriodico.Descricao = item.Título.Trim();
                            modelPeriodico.SeqClassificacaoPeriodico = modelClassificaPeriodico.Seq;

                            // Modelo Qualis Periodico
                            modelQualisPeriodico = new QualisPeriodico();
                            modelQualisPeriodico.CodigoISSN = item.ISSN;
                            modelQualisPeriodico.DescricaoAreaAvaliacao = item.AreaAvaliacao;

                            // Convert texto no enum
                            Common.Areas.CUR.Enums.QualisCapes qualisCapes;
                            Enum.TryParse(item.Estrato, out qualisCapes);
                            modelQualisPeriodico.QualisCapes = qualisCapes;

                            // Adiciona o modelo qualis periodico ao modelo periodico
                            modelPeriodico.QualisPeriodico = new List<QualisPeriodico>();
                            modelPeriodico.QualisPeriodico.Add(modelQualisPeriodico);

                            // Adciona o modelo periodico a classificação Periodica
                            modelClassificaPeriodico.Periodicos.Add(modelPeriodico);

                            ultimoLido = RemoverAcentos(item.Título).ToLower();
                        }
                        else
                        {
                            // Modelo Qualis Periodico
                            modelQualisPeriodico = new QualisPeriodico();
                            modelQualisPeriodico.CodigoISSN = item.ISSN;
                            modelQualisPeriodico.DescricaoAreaAvaliacao = item.AreaAvaliacao;
                            // Convert texto no enum
                            Common.Areas.CUR.Enums.QualisCapes qualisCapes;
                            Enum.TryParse(item.Estrato, out qualisCapes);
                            modelQualisPeriodico.QualisCapes = qualisCapes;

                            // Adiciona o modelo qualis periodico ao modelo periodico
                            modelPeriodico.QualisPeriodico.Add(modelQualisPeriodico);
                        }
                    }

                    // Cria o dicionário para poder relacionar a tabela de qualis com a tabela de periodico.
                    // O método BulkInsertEntity não atualiza os seqs dos registros gravados.
                    var dicionarioQualis = new Dictionary<string, List<QualisPeriodico>>();
                    foreach (var item in modelClassificaPeriodico.Periodicos)
                    {
                        dicionarioQualis.Add(item.Descricao, item.QualisPeriodico.ToList());
                        item.QualisPeriodico = null;
                    }

                    // Grava apenas os periódicos
                    var listaPeriodico = modelClassificaPeriodico.Periodicos.ToList();
                    this.BulkInsertEntity(listaPeriodico);

                    // Recupera os sequenciais dos preiódicos gravados para associação com os itens qualis
                    var keyValuesPeriodicos = SearchProjectionBySpecification(new PeriodicoFilterSpecification() { SeqClassificacaoPeriodico = modelClassificaPeriodico.Seq },
                        p => new { p.Seq, p.Descricao }).ToList();

                    List<string> erros = new List<string>();
                    // Vincula os itens qualis
                    foreach (var keyValuePeriodico in keyValuesPeriodicos)
                    {
                        if (dicionarioQualis.ContainsKey(keyValuePeriodico.Descricao))
                        {
                            var qualis = dicionarioQualis[keyValuePeriodico.Descricao];
                            foreach (var item in qualis)
                            {
                                item.SeqPeriodico = keyValuePeriodico.Seq;
                            }
                        }
                        else
                        {
                            erros.Add(keyValuePeriodico.Descricao);
                        }
                    }
                    if (erros.Count > 0)
                    {
                        transacao.Rollback();
                        throw new SMCApplicationException(string.Format("Erro ao carregar {0}", string.Join("; ", erros)));
                    }

                    var qualisDescricao = dicionarioQualis.Values.SelectMany(s => s).Where(w => string.IsNullOrEmpty(w.DescricaoAreaAvaliacao)).ToList();

                    // Grava os itens qualis
                    var listaQualis = dicionarioQualis.Values.SelectMany(s => s).ToList();
                    QualisPeriodicoDomainService.BulkInsertEntity(listaQualis);

                    // Commit
                    transacao.Commit();
                }
            }
        }

        private List<PeriodicoImportacaoVO> ValidarPlanilhaXLS(PeriodicoVO periodico)
        {
            ///Dicionario com as propriedade do objeto
            List<PeriodicoImportacaoVO> listaImportacao = new List<PeriodicoImportacaoVO>();
            Dictionary<int, string> dic = new Dictionary<int, string>();

            try
            {
                ///Criar um dicionario de validação de ordem
                Dictionary<int, string> dicValidacao = new Dictionary<int, string>();
                dicValidacao.Add(0, "ISSN");
                dicValidacao.Add(1, "Título");
                dicValidacao.Add(2, "Área de Avaliação");
                dicValidacao.Add(3, "Estrato");

                int numCell = 0;
                int numRow = 0;

                foreach (var item in SMCReflectionHelper.GetProperties(typeof(PeriodicoImportacaoVO)))
                {
                    dic.Add(numCell, item.Name);
                    numCell++;
                }
                numCell = 0;

                ///Caminho temporario para escrever o arquivo
                var tempPath = Path.GetTempFileName();
                ///Escrever o arquivo no caminho temporario
                using (FileStream x = new FileStream(tempPath, FileMode.OpenOrCreate))
                {
                    x.Write(periodico.ArquivoAnexado.FileData, 0, periodico.ArquivoAnexado.FileData.Length);
                    x.Close();
                }

                ///Para ler os XLS
                var tudo = Framework.SpreadSheet.Workbook.Load(tempPath);

                foreach (var row in tudo.Worksheets[0].Cells.Rows)
                {
                    if (numRow == 0)
                    {
                        if (row.Value.LastColIndex != 3)
                        {
                            throw new PeriodicoArquivoInvalidoException();
                        }

                        int indexValidacao = 3;

                        ///Valida se a planilha esta na ordem valida
                        for (int i = 0; i < indexValidacao; i++)
                        {
                            if (row.Value.GetCell(i).Value.ToString() != dicValidacao[i])
                            {
                                throw new PeriodicoArquivoInvalidoException();
                            }
                        }
                    }

                    if (numRow > 0)
                    {
                        PeriodicoImportacaoVO periodicoImportacaoVO = new PeriodicoImportacaoVO();

                        foreach (var item in row.Value)
                        {
                            SMCReflectionHelper.SetValue(periodicoImportacaoVO, dic[numCell], item.Value.Value);
                            numCell++;
                        }

                        ///Adiciona os dados a lista de retorno
                        listaImportacao.Add(periodicoImportacaoVO);
                        numCell = 0;
                    }
                    else
                    {
                        numRow++;
                    }
                }
                return listaImportacao;
            }
            catch (Exception ex)
            {
                listaImportacao = ValidarCSV(periodico);
                return listaImportacao;
            }
        }

        private List<PeriodicoImportacaoVO> ValidarPlanilhaXLSX(PeriodicoVO periodico)
        {
            ///Dicionario com as propriedade do objeto
            List<PeriodicoImportacaoVO> listaImportacao = new List<PeriodicoImportacaoVO>();
            Dictionary<int, string> dic = new Dictionary<int, string>();

            try
            {
                ///Criar um dicionario de validação de ordem
                Dictionary<int, string> dicValidacao = new Dictionary<int, string>();
                dicValidacao.Add(0, "ISSN");
                dicValidacao.Add(1, "Título");
                dicValidacao.Add(2, "Área de Avaliação");
                dicValidacao.Add(3, "Estrato");

                int numCell = 0;
                int numRow = 0;

                foreach (var item in SMCReflectionHelper.GetProperties(_type))
                {
                    dic.Add(numCell, item.Name);
                    numCell++;
                }
                numCell = 0;

                ///Caminho temporario para escrever o arquivo
                var tempPath = Path.GetTempFileName();
                ///Escrever o arquivo no caminho temporario
                using (FileStream x = new FileStream(tempPath, FileMode.OpenOrCreate))
                {
                    x.Write(periodico.ArquivoAnexado.FileData, 0, periodico.ArquivoAnexado.FileData.Length);
                    x.Close();
                }

                ///Percorre todas as planilhas da pasta de trabalho
                foreach (var worksheet in Workbook.Worksheets(tempPath))
                {
                    //Caso o arquivo esteja vazio
                    if (worksheet.Rows.Count() == 0)
                        throw new PeriodicoArquivoInvalidoException();

                    ///Percorre todas as linhas da planilha atual
                    for (var row = 0; row <= worksheet.Rows.Count() - 1; row++)
                    {
                        if (numRow == 0)
                        {
                            ///Validação se exite mais ou menos campos
                            if (worksheet.Rows[row].Cells.Count() != 4)
                            {
                                throw new PeriodicoArquivoInvalidoException();
                            }

                            int indexValidacao = 4;

                            ///Valida se a planilha esta na ordem valida
                            for (int i = 0; i < indexValidacao; i++)
                            {
                                if (worksheet.Rows[row].Cells[i].Text.Trim() != dicValidacao[i])
                                {
                                    throw new PeriodicoArquivoInvalidoException();
                                }
                            }
                        }

                        if (numRow > 0)
                        {
                            PeriodicoImportacaoVO periodicoImportacaoVO = new PeriodicoImportacaoVO();

                            ///Percorre todas as céulas da linha atual
                            for (var cell = 0; cell < worksheet.Rows[row].Cells.Count(); cell++)
                            {
                                ///Verifica se as celulas desta linha tem valor
                                if (worksheet.Rows[row].Cells[cell] != null)
                                {
                                    ///Faz o reflection e carregando os dados no objeto
                                    SMCReflectionHelper.SetValue(periodicoImportacaoVO, dic[numCell], worksheet.Rows[row].Cells[cell].IsAmount ? worksheet.Rows[row].Cells[cell].Amount.ToString() : worksheet.Rows[row].Cells[cell].Text);
                                    numCell++;
                                }
                                else
                                {
                                    ///Apaga dos dados do objeto corrente para que o mesmo não seja adcionado a lista de importação
                                    periodicoImportacaoVO = null;
                                    break;
                                }
                            }

                            ///Verifica se não foi apagado o objeto
                            ///Adiciona os dados a lista de retorno
                            if (periodicoImportacaoVO != null)
                            {
                                listaImportacao.Add(periodicoImportacaoVO);
                            }
                            numCell = 0;
                        }
                        else
                        {
                            numRow++; ///Elimina a primeira linha, que contém os títulos das colunas.
                        }
                    }
                }

                return listaImportacao;
            }
            catch (Exception ex)
            {
                if (ex.Message != ExceptionsResource.ERR_PeriodicoArquivoInvalidoException)
                {
                    throw new PeriodicoArquivoTipoInvalidoException();
                }
                else
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Não são utilizadas no momento
        /// </summary>
        /// <param name="periodico"></param>
        /// <returns></returns>
        private List<PeriodicoImportacaoVO> ValidarCSV(PeriodicoVO periodico)
        {
            try
            {
                ///Criar um dicionario de validação de ordem
                Dictionary<int, string> dicValidacao = new Dictionary<int, string>
                {
                    { 0, "ISSN" },
                    { 1, "Título" },
                    { 2, "Área de Avaliação" },
                    { 3, "Estrato" }
                };

                ///Dicionario com as propriedade do objeto
                List<PeriodicoImportacaoVO> listaImportacao = new List<PeriodicoImportacaoVO>();
                Dictionary<int, string> dic = new Dictionary<int, string>();
                int numCell = 0;

                foreach (var item in SMCReflectionHelper.GetProperties(typeof(PeriodicoImportacaoVO)))
                {
                    dic.Add(numCell, item.Name);
                    numCell++;
                }
                numCell = 0;

                ///Caminho temporario para escrever o arquivo
                var tempPath = Path.GetTempFileName();
                ///Escrever o arquivo no caminho temporario
                using (FileStream x = new FileStream(tempPath, FileMode.OpenOrCreate))
                {
                    x.Write(periodico.ArquivoAnexado.FileData, 0, periodico.ArquivoAnexado.FileData.Length);
                    x.Close();
                }

                using (StreamReader stream = new StreamReader(tempPath, Encoding.GetEncoding(1252)))
                {
                    string linha = null;
                    int numLinha = 0;

                    ///Leitura por linha linha
                    while ((linha = stream.ReadLine()) != null)
                    {
                        string[] coluna = linha.Split('\t');

                        if (numLinha == 0)
                        {
                            if (coluna.Length != 4)
                            {
                                throw new PeriodicoArquivoInvalidoException();
                            }

                            int indexValidacao = 4;

                            ///Valida se a planilha esta na ordem valida
                            for (int i = 0; i < indexValidacao; i++)
                            {
                                if (coluna[i].Replace("\"", "").ToString() != dicValidacao[i])
                                {
                                    throw new PeriodicoArquivoInvalidoException();
                                }
                            }
                        }

                        if (numLinha > 0)
                        {
                            PeriodicoImportacaoVO periodicoImportacaoVO = new PeriodicoImportacaoVO();
                            if (coluna == null)
                            {
                                return null;
                            }
                            foreach (var item in coluna)
                            {
                                SMCReflectionHelper.SetValue(periodicoImportacaoVO, dic[numCell], item.Replace("\"", "").ToString());
                                numCell++;
                            }

                            ///Adiciona os dados a lista de retorno
                            listaImportacao.Add(periodicoImportacaoVO);
                            numLinha++;
                            numCell = 0;
                        }
                        else
                        {
                            numLinha++;
                        }
                    }

                    stream.Close();
                }

                return listaImportacao;
            }
            catch (Exception ex)
            {
                if (ex == null)
                {
                    throw new PeriodicoArquivoTipoInvalidoException();
                }
                else
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Não são utilizadas no momento
        /// </summary>
        /// <param name="periodico"></param>
        /// <returns></returns>
        private bool ValidarDatasClassificacaoPeriodico(PeriodicoVO periodico)
        {
            bool retorno = false;

            var listaClassificaoPeriodico = ClassificacaoPeriodicoDomainService.SearchAll().OrderByDescending(o => o.AnoFim);

            foreach (var item in listaClassificaoPeriodico)
            {
                ///Verifica se o periodo da nova classificação capes está incidindo em outra classificação
                ///O período de validade da classificação CAPES, ou seja, o período entre o ano início e o ano fim , não
                ///poderá sobrepor períodos de validade já cadastrados.Em caso de violação, abortar a operação e emitir
                ///a mensagem de erro:
                if ((periodico.AnoInicio >= item.AnoInicio && periodico.AnoInicio <= item.AnoFim)
                 || (periodico.AnoFim >= item.AnoInicio && periodico.AnoFim <= item.AnoFim))
                {
                    throw new PeriodicoArquivoDataInvalidaException();
                }
                else if ((item.AnoInicio >= periodico.AnoInicio && item.AnoInicio <= periodico.AnoFim)
                 || (item.AnoFim >= periodico.AnoInicio && item.AnoFim <= periodico.AnoFim))
                {
                    throw new PeriodicoArquivoDataInvalidaException();
                }
            }

            ///Validar se o arquivo será o novo atual
            ///Se o ano fim da nova classificação CAPES for maior do que o ano fim da classificação CAPES atual,
            ///setar o ind_atual da nova classificação para 1 e da antiga classificação atual para 0.
            if (listaClassificaoPeriodico.Count() > 0)
            {
                if (periodico.AnoInicio > listaClassificaoPeriodico.OrderByDescending(o => o.AnoFim).FirstOrDefault().AnoFim)
                {
                    var classificacaoAtual = listaClassificaoPeriodico.OrderByDescending(o => o.AnoFim).FirstOrDefault();

                    classificacaoAtual.Atual = false;

                    ClassificacaoPeriodicoDomainService.UpdateEntity(classificacaoAtual);
                    retorno = true;
                }
            }
            else
            {
                return true;
            }

            return retorno;
        }
    }
}