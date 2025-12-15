using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Common.Areas.Shared.Constants;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.Validators;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Framework;
using SMC.Framework.Domain.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class ParceriaIntercambioDomainService : AcademicoContextDomain<ParceriaIntercambio>
    {
        #region DomainService

        private TermoIntercambioDomainService TermoIntercambioDomainService
        {
            get { return Create<TermoIntercambioDomainService>(); }
        }

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService
        {
            get { return Create<InstituicaoNivelTipoVinculoAlunoDomainService>(); }
        }

        private ParceriaIntercambioInstituicaoExternaDomainService ParceriaIntercambioInstituicaoExternaDomainService
        {
            get { return Create<ParceriaIntercambioInstituicaoExternaDomainService>(); }
        }

        #endregion DomainService

        public SMCPagerData<ParceriaIntercambioListarVO> ListarParceriaIntercambio(ParceriaIntercambioFiltroVO filtro)
        {
            var lista = new List<ParceriaIntercambioListarVO>();

            //Implementação para trazer apenas os registros que possuem o tipo de termo de intercambio parametrizado

            //Recupera os seqs dos tipos de termo de intercâmbio que podem ser exibidos na tela
            var seqsTiposTermosIntercambios = InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionAll(x => new
            {
                Itens = x.TiposTermoIntercambio.Select(t => new
                {
                    Seq = t.TipoTermoIntercambio.Seq,
                })
            })?.SelectMany(x => x.Itens.Select(i => i.Seq)).SMCDistinct(i => i).ToList();

            //Cria o spec
            var spec = filtro.Transform<ParceriaIntercambioFilterSpecification>();

            //Vincula os seqs permitidos ao spec para filtragem dos registros
            spec.SeqsTiposTermosIntercambio = seqsTiposTermosIntercambios;

            if (filtro.SeqInstituicaoExterna != null && filtro.SeqInstituicaoExterna.Seq > 0)
            {
                spec.SeqInstituicaoExterna = filtro.SeqInstituicaoExterna.Seq;
            }

            int total;
            var parcerias = SearchBySpecification(spec, out total,
                IncludesParceriaIntercambio.Arquivos
                   | IncludesParceriaIntercambio.TiposTermo
                   | IncludesParceriaIntercambio.TiposTermo_TipoTermoIntercambio
                   | IncludesParceriaIntercambio.InstituicaoEnsino
                   | IncludesParceriaIntercambio.Vigencias
                   | IncludesParceriaIntercambio.Arquivos_ArquivoAnexado
                   | IncludesParceriaIntercambio.InstituicoesExternas
                   | IncludesParceriaIntercambio.InstituicoesExternas_InstituicaoExterna).ToList();

            foreach (var item in parcerias)
            {
                var model = SMCMapperHelper.Create<ParceriaIntercambioListarVO>(item);

                //PreencheVigencia(item, model);

                var vigenciaAtual = item.Vigencias.OrderByDescending(o => o.Seq).FirstOrDefault();
                if (vigenciaAtual != null)
                {
                    model.DataInicio = vigenciaAtual.DataInicio;
                    model.DataFim = vigenciaAtual.DataFim;
                }

                /*Exibir a Instituição de Ensino logada e as Instituições de Ensino Externas que estão associadas à parceria. 
                 * As instituições externas, serão listadas no seguinte formato: "<nome instituição externa>" + '' - " + " < sigla instituição externa> " + '-' + <indicador ativo>.
               < indicador ativo > -Se o valor for "Sim", exibir "Ativo", se o valor for "Não", exibir "Inativo".*/     
                if (model.InstituicoesExternas == null)
                    model.InstituicoesExternas = new List<string>();
                foreach (var obj in item.InstituicoesExternas)
                    model.InstituicoesExternas.Add($"{obj.InstituicaoExterna.Nome} - {obj.InstituicaoExterna.Sigla} ({(obj.Ativo ? "Ativa" : "Inativa")})");

                model.InstituicoesExternas.Add($"{item.InstituicaoEnsino.Nome} - {item.InstituicaoEnsino.Sigla}  ({(item.InstituicaoEnsino.Ativo ? "Ativa" : "Inativa")})");

                if (model.TiposTermo == null)
                    model.TiposTermo = new List<string>();
                //Os tipos de termos serão listados no seguinte formato: < descrição tipo de termo > +'-' + <indicador ativo> Se o valor for "Sim", exibir "Ativo", se o valor for "Não", exibir "Inativo"
                foreach (var obj in item.TiposTermo)
                    model.TiposTermo.Add($"{obj.TipoTermoIntercambio.Descricao} ({(obj.Ativo ? "Ativo" : "Inativo")})" );

                model.TotalTermos = TermoIntercambioDomainService.Count(new TermoIntercambioFilterSpecification() { SeqParceriaIntercambio = item.Seq });

                if (filtro.Ativo.HasValue)
                {
                    if (filtro.Ativo.Value && model.DataInicio <= DateTime.Now)
                    {
                        if (!model.DataFim.HasValue)
                        {
                            lista.Add(model);
                            continue;
                        }
                        else if (model.DataFim >= DateTime.Now)
                        {
                            lista.Add(model);
                        }
                        continue;
                    }
                    else if (filtro.Ativo.Value && model.DataInicio > DateTime.Now)
                    {
                        continue;
                    }
                    else if (!filtro.Ativo.Value)
                    {
                        if (model.DataInicio > DateTime.Now || (model.DataInicio < DateTime.Now && model.DataFim < DateTime.Now))
                        {
                            lista.Add(model);
                        }
                        continue;
                    }
                }

                lista.Add(model);
            }

            return new SMCPagerData<ParceriaIntercambioListarVO>(lista, total);
        }

        private static void PreencheVigencia(ParceriaIntercambio item, ParceriaIntercambioListarVO model)
        {
            double menor = 0;
            foreach (var vigencia in item.Vigencias)
            {
                if (vigencia.DataInicio <= DateTime.Now && vigencia.DataFim.HasValue && vigencia.DataFim >= DateTime.Now)
                {
                    model.DataInicio = vigencia.DataInicio;
                    model.DataFim = vigencia.DataFim;
                    break;
                }
                else
                {
                    double diferencaDias = 0;
                    var totalDiasAproximados = (vigencia.DataInicio - DateTime.Today).TotalDays;
                    diferencaDias = (totalDiasAproximados < 0) ? totalDiasAproximados * (-1) : totalDiasAproximados;
                    if (menor == 0 || diferencaDias < menor)
                    {
                        menor = diferencaDias;
                        model.DataInicio = vigencia.DataInicio;
                        model.DataFim = vigencia.DataFim;
                        continue;
                    }
                }
            }
        }

        public long SalvarParceriaIntercambio(ParceriaIntercambio parceriaIntercambio)
        {
            if (parceriaIntercambio.Seq > default(long))
            {
                var old = SearchByKey(new SMCSeqSpecification<ParceriaIntercambio>(parceriaIntercambio.Seq), a => a.Vigencias, a => a.TiposTermo);
                Validar(parceriaIntercambio, old);
            }
            else
            {
                Validar(parceriaIntercambio);
            }

            // Se o arquivo não foi alterado, atualiza com conteúdo com o que está no banco
            this.EnsureFileIntegrity(parceriaIntercambio.Arquivos, x => x.SeqArquivoAnexado, x => x.ArquivoAnexado);

            SaveEntity(parceriaIntercambio);

            return parceriaIntercambio.Seq;
        }

        private void Validar(ParceriaIntercambio obj, ParceriaIntercambio old = null)
        {
            #region [ Validando o período de vigência ]

            //FIX: A validação de número de vigências esta sendo feito no dominio pois o componente masterdetail modal não valida o valor minimo na viewmode de inclusão
            if (obj.Seq == 0)
            {
                ///É necessário informar um período de vigência.Caso não ocorra, abortar a operação e exibir a seguinte mensagem de erro:
                ///"Inclusão não permitida. É necessário informar o período de vigência."
                if (!obj.Vigencias.SMCAny())
                {
                    throw new ParceriaIntercambioPeriodoNecessarioUmException();
                }

                ///É possível informar somente um período de vigência. Caso não ocorra, abortar a operação e exibir a seguinte mensagem de erro: 
                ///"Inclusão não permitida. É possível informar somente um período de vigência."
                if (obj.Vigencias.Count > 1)
                {
                    throw new ParceriaIntercambioPeriodoPossivelUmException();
                }
            }            

            //NV06 - Só é possível alterar o registro de vigência mais atual, exibir os demais registros desabilitados e sem possibilidade de exclusão.
            //Apenas o registro atual pode ser alterado e excluído.
            //NV10 - Para incluir outro período de vigência é necessário encerrar a vigência corrente.
            //Caso isto não ocorra, abortar a operação e exibir a seguinte mensagem:
            //"Inclusão não permitida. Para incluir outro período é necessário encerrar o período vigente."
            if (obj.Vigencias.Count(a => a.DataFim == null) > 1)
            {
                string s = obj.Seq > 0 ? "Alteração" : "Inclusão";
                throw new Exception($"{s} não permitida. Para incluir outro período é necessário encerrar o período vigente.");
            }


            ///Verificar se existe mais de um período de vigência EXATAMENTE igual a outro.Caso isto ocorra, abortar operação e exibir a seguinte mensagem:
            ///"Alteração não permitida. Não é possível salvar uma parceria com períodos de vigência iguais."
            ///ALTERAÇÃO A REGRA PARA: AO INVES DE VERIFICAR SE EXISTE MAIS DE UM PERIODO DE VIGÊNCIA EXATAMENTE IGUAL. 
            ///VERIFICAR SE O PERIODO ANTERIOR É EXATAMENTE IGUAL. 
            if(obj.Vigencias.Count() > 1)
            {
                var ultimaVigencia = obj.Vigencias.LastOrDefault();
                var penultimaVigencia = obj.Vigencias[obj.Vigencias.Count() - 2];

                if(ultimaVigencia.DataInicio == penultimaVigencia.DataInicio && ultimaVigencia.DataFim == penultimaVigencia.DataFim)
                {
                    throw new ParceriaIntercambioPeriodoExatoIgualException();
                }
            }


            if (old != null)
            {
                long seqVigente = 0;

                var ultimaVigencia = old.Vigencias.OrderByDescending(a => a.Seq).FirstOrDefault();
                seqVigente = ultimaVigencia.Seq;

                //Verificando se a vigência ativa foi apagada
                if (!obj.Vigencias.Any(a => a.Seq == seqVigente))
                {
                    //Verificando se alguma vigência antiga foi apagada
                    if (old.Vigencias.Count(a => a.Seq != seqVigente) > obj.Vigencias.Count(a => a.Seq > default(long)))
                    {
                        throw new AlterarVigenciaAntigaException();
                    }

                    //Certificando que não haverá alteração de vigências antigas...
                    foreach (var item in obj.Vigencias)
                    {
                        if (item.Seq > default(long))
                        {
                            var vigenciaAntiga = old.Vigencias.Where(a => a.Seq == item.Seq).FirstOrDefault();
                            if ((item.DataFim != vigenciaAntiga.DataFim) || (item.DataInicio != vigenciaAntiga.DataInicio))
                            {
                                string s = obj.Seq > 0 ? "Alteração" : "Inclusão";
                                throw new Exception($"{s} não permitida. Apenas o registro atual pode ser alterado ou excluído.");
                            }
                        }
                    }
                }
                else
                {
                    //Verificando se alguma vigência antiga foi apagada
                    if (old.Vigencias.Count(a => a.Seq != seqVigente) > obj.Vigencias.Count(a => a.Seq > default(long) && a.Seq != seqVigente))
                    {
                        throw new AlterarVigenciaAntigaException();
                    }

                    //Verificando se tem vigência nova além da atual já cadastrada.
                    //ParceriaIntercambioVigencia vigenciaAtual;
                    if ((obj.Vigencias.Count(a => a.Seq == default(long)) >= 1) && (!obj.Vigencias.Where(a => a.Seq == seqVigente).FirstOrDefault().DataFim.HasValue))
                    {
                        //Não pode incluir uma nova vigência sem encerrar o período de vigência anterior.
                        string s = obj.Seq > 0 ? "Alteração" : "Inclusão";
                        throw new Exception($"{s} não permitida. Para incluir outro período é necessário encerrar o período vigente.");
                    }

                    //Certificando que não haverá alteração de vigências antigas...
                    foreach (var item in obj.Vigencias.Where(a => a.Seq != seqVigente))
                    {
                        if (item.Seq > default(long))
                        {
                            var vigenciaAntiga = old.Vigencias.Where(a => a.Seq == item.Seq).FirstOrDefault();
                            if ((item.DataFim != vigenciaAntiga.DataFim) || (item.DataInicio != vigenciaAntiga.DataInicio))
                            {
                                string s = obj.Seq > 0 ? "Alteração" : "Inclusão";
                                throw new Exception($"{s} não permitida. Apenas o registro atual pode ser alterado ou excluído.");
                            }
                        }
                    }
                }
            }
            else
            {
                if (obj.Vigencias.Count > 1)
                {
                    string s = obj.Seq > 0 ? "Alteração" : "Inclusão";
                    throw new Exception($"{s} não permitida. Não é possível incluir mais de um período de vigência.");
                }
            }

            #endregion [ Validando o período de vigência ]

            #region [ Validando os termos ]

            if (obj.Seq > default(long))// && vigencia.Seq > default(long))
            {
                //NV12 - Verificar se existem termos para essa parceria.
                //Caso existir, verificar se o período de vigência de todos os termos está dentro do período desta parceria.
                //Caso não esteja, abortar operação e exibir a seguinte mensagem:
                //"Alteração não permitida. Existem termos de intercâmbio associados a esta parceria com o período de vigência fora do período de vigência da parceria."
                
                TermoIntercambioFilterSpecification spec = new TermoIntercambioFilterSpecification();
                spec.SeqParceriaIntercambio = obj.Seq;
                var termos = TermoIntercambioDomainService.SearchBySpecification(spec, a => a.Vigencias).ToList();

                if (termos != null)
                {

                    ParceriaIntercambioVigencia vigenciaParceria = new ParceriaIntercambioVigencia();

                    ///Verifica se tiveram termos adcionados
                    var vigenciasNovas = obj.Vigencias.Where(w => w.Seq == 0).ToList();

                    if (vigenciasNovas.SMCAny())
                    {
                        vigenciaParceria = vigenciasNovas.LastOrDefault();
                    }
                    else
                    {
                        vigenciaParceria = obj.Vigencias.OrderByDescending(o => o.Seq).FirstOrDefault();
                    }

                    var vigenciasTermos = termos.Select(t => t.Vigencias.OrderByDescending(v => v.Seq).FirstOrDefault()).ToList();

                    foreach (var vigenciaTermo in vigenciasTermos)
                    {
                        if (vigenciaTermo != null)
                        {
                            if (vigenciaTermo.DataInicio < vigenciaParceria.DataInicio || (vigenciaParceria.DataFim.HasValue ? vigenciaTermo.DataFim > vigenciaParceria.DataFim : false))
                            {
                                throw new Exception("Alteração não permitida. Existem termos de intercâmbio associados a esta parceria com o período de vigência fora do período de vigência da parceria.");
                            }
                        }
                    }
                }
            }

            #endregion [ Validando os termos ]

            #region [ Validando o tipo de termo de intercâmbio ]

            ///1.Não será permitido excluir um tipo de termo de intercâmbio, se já tiver termo de intercâmbio cadastrado com esse tipo de termo
            ///  para a parceria em questão.Caso ocorra,, abortar a operação e exibir a seguinte mensagem de erro:
            ///“Alteração não permitida.Já existem termos de intercâmbio cadastrado para esta parceria de intercâmbio com esse tipo de termo de intercâmbio.
            if (old != null && old.TiposTermo.Count > 0)
            {
                var existentes = old.TiposTermo.Select(b => b.SeqTipoTermoIntercambio).ToList();
                var salvando = obj.TiposTermo.Select(a => a.SeqTipoTermoIntercambio).ToList();
                var excluindo = existentes.Except(salvando).ToList();

                foreach (var seqTipoTermoIntercambio in excluindo)
                {
                    if (TermoIntercambioDomainService.ExisteTermoIntercambioPorTipoTermoIntercambio(seqTipoTermoIntercambio, old.Seq))
                    {
                        throw new ParceriaIntercambioExisteTermoPorTipoException();
                    }
                }
            }

            var duplicados = obj.TiposTermo.Select(a => a.SeqTipoTermoIntercambio).ToList().GroupBy(x => x).Where(group => group.Count() > 1).Select(group => group.Key).ToList();
            if (duplicados.Count > 0)
            {
                throw new Exception(string.Format("{0} não permitida. Não é possível associar o mesmo tipo de termo de intercâmbio mais de uma vez.", obj.Seq > 0 ? "Alteração" : "Inclusão"));
            }

            #endregion [ Validando o tipo de termo de intercâmbio ]

            #region [ Consistência do(s) arquivo(s) ]

            //RN_PES_011 Consistência arquivos
            //Ao carregar um arquivo, as seguintes verificações devem ser feitas:
            //  1.Não deverá ser permitido carregar arquivos maiores que 25 mega.
            //  Em caso de violação, abortar a operação e exibir a mensagem a seguir:
            //  "Tamanho máximo de arquivo é 25 MB".
            //
            //  2.Permitir carregar somente arquivos com extensão doc, docx, xls, xlsx, jpg, jpeg, png, pdf, rar, zip, ps.
            //  Em caso de violação, abortar a operação e exibir a mensagem a seguir:
            //  "O arquivo <descrição documento> não é permitido. Favor enviar o arquivo em uma das seguintes extensões: doc, docx, xls, xlsx, jpg, jpeg, png, pdf, rar, zip, ps"

            if (obj.Arquivos != null && obj.Arquivos.Count() > 0)
            {
                foreach (var arquivo in obj.Arquivos)
                {
                    string extensao = Path.GetExtension(arquivo.ArquivoAnexado.Nome);
                    if (arquivo.ArquivoAnexado != null && arquivo.ArquivoAnexado.Tamanho > VALIDACAO_ARQUIVO_ANEXADO.TAMANHO_MAXIMO_ARQUIVO_ANEXADO)
                    {
                        throw new Exception("Tamanho máximo de arquivo é 25 MB");
                    }

                    if (string.IsNullOrEmpty(extensao) || !VALIDACAO_ARQUIVO_ANEXADO.EXTENSOES_PERMITIDAS_PARCERIA_TERMO_INTERCAMBIO.Contains(extensao))
                    {
                        throw new Exception(string.Format("O arquivo {0} não é permitido. Favor enviar o arquivo em uma das seguintes extensões: doc, docx, xls, xlsx, jpg, jpeg, png, pdf, rar, zip, ps", arquivo.ArquivoAnexado.Nome));
                    }
                }
            }

            #endregion [ Consistência do(s) arquivo(s) ]

            var validator = new ParceriaIntercambioValidator();
            var results = validator.Validate(obj);
            if (!results.IsValid)
            {
                var errorList = new List<SMCValidationResults>();
                errorList.Add(results);
                throw new SMCInvalidEntityException(errorList);
            }
        }

        private static bool RemoveDaLista(long seq, List<long> lista)
        {
            return lista.Contains(seq);
        }

        /// <summary>
        /// Exlcuir parceria de intercambio
        /// </summary>
        /// <param name="seq">Sequencial da parceria de intercambio</param>
        public void ExcluirParceria(long seq)
        {
            var modelo = this.SearchByKey(new SMCSeqSpecification<ParceriaIntercambio>(seq));

            var termosIntercambioPorParceria = this.TermoIntercambioDomainService.SearchBySpecification(new TermoIntercambioFilterSpecification() { SeqParceriaIntercambio = seq }).ToList();

            if(termosIntercambioPorParceria.Count() > 0)
            {
                throw new ParceriaIntercambioExisteTermoCadastradoException();
            }

            this.DeleteEntity(modelo);
        }
    }
}