using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Pessoas.ServiceContract.Areas.PES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class FuncionarioVinculoDomainService : AcademicoContextDomain<FuncionarioVinculo>
    {
        #region [ DomainService ]

        private TipoFuncionarioDomainService TipoFuncionarioDomainService => Create<TipoFuncionarioDomainService>();

        #endregion [ DomainService ]

        #region [ Service ]
        private IPessoaService PessoaService => Create<IPessoaService>();
        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();



        #endregion [ Service ]

        /// <summary>
        /// Busca um vinculo de funcionario com suas depêndencias
        /// </summary>
        /// <param name="seq">Sequencial do funcionario</param>
        /// <returns>Dados do funcionario</returns>
        public FuncionarioVinculoVO BuscarFuncionarioVinculo(long seq)
        {
            FuncionarioVinculoVO funcionarioVinculo = this.SearchByKey(seq).Transform<FuncionarioVinculoVO>();

            if (funcionarioVinculo.SeqEntidadeVinculo != null)
            {
                funcionarioVinculo.SeqTipoEntidade = EntidadeDomainService.BuscarTipoEntidadeporEntidade(funcionarioVinculo.SeqEntidadeVinculo.Value);
            }

            return funcionarioVinculo.Transform<FuncionarioVinculoVO>();
        }

        /// <summary>
        /// Excluir o funcionario informado
        /// </summary>
        /// <param name="seq">Sequencial do funcionário a ser excluído</param>
        public void ExcluirFuncionarioVinculo(long seq)
        {
            var vinculo = this.SearchByKey(seq);
            DeleteEntity(vinculo);
        }

        /// <summary>
        /// Busca vinculos dos funcionários
        /// </summary>
        /// <param name="filtros">Filtros para busca</param>
        /// <returns>Dados paginados dos vinculos do funcionario</returns>
        public SMCPagerData<FuncionarioVinculoListaVO> BuscarVinculosFuncionario(FuncionarioVinculoFilterSpecification spec)
        {
            int total = 0;

            spec.SetOrderByDescending(s => s.DataInicio);

            var retorno = this.SearchProjectionBySpecification(spec, p => new FuncionarioVinculoListaVO()
            {
                Seq = p.Seq,
                SeqFuncionario = p.SeqFuncionario,
                SeqTipoFuncionario = p.SeqTipoFuncionario,
                DescricaoTipoFuncionario = p.TipoFuncionario.DescricaoMasculino,
                DataInicio = p.DataInicio,
                DataFim = p.DataFim,
                DescricaoEntidadeCadastrada = p.EntidadeVinculo.Nome,
                TipoEntidadeCadastrada = p.EntidadeVinculo.TipoEntidade.Descricao
            }, out total).ToList();

            return new SMCPagerData<FuncionarioVinculoListaVO>(retorno, total);
        }

        /// <summary>
        /// Salvar dados do funcionario vinculo
        /// </summary>
        /// <param name="funcionarioVinculoVO">Dados as serem salvos</param>
        /// <returns>Sequencial do funcionario</returns>
        public long SalvarFuncionarioVinculo(FuncionarioVinculoVO funcionarioVinculoVO)
        {
            ValidarDatasVinculo(funcionarioVinculoVO);

            FuncionarioVinculo model = funcionarioVinculoVO.Transform<FuncionarioVinculo>();
            FuncionarioVinculoFilterSpecification spec = new FuncionarioVinculoFilterSpecification() { SeqFuncionario = model.SeqFuncionario };
            TipoRegistroProfissional? tipoRegistroProfissionalFuncionario = this.SearchProjectionBySpecification(spec, p => p.TipoFuncionario.TipoRegistroProfissionalObrigatorio).FirstOrDefault();
            ValidarRegistroProfissional(model.SeqTipoFuncionario, tipoRegistroProfissionalFuncionario);

            ValidarObrigatorioVinculoUnico(model.Seq, model.SeqFuncionario, model.SeqTipoFuncionario, model.DataInicio, model.DataFim);

            this.SaveEntity(model);

            return model.Seq;
        }

        /// <summary>
        /// Não deverá ser permitido que um mesmo funcionáriotenha um vínculo para o mesmo tipo com datas coincidentes.Ex.:
        /// Vínculo 1 Tipo: Reitor Período: 01/01/2021 a(sem data fim)
        /// Vínculo 2 Tipo: Reitor Período: 05/07/2021 a 31/12/2021
        /// </summary>
        ///<param name="funcionarioVinculoVO">Dados funcionario vinculo</param>
        public void ValidarDatasVinculo(FuncionarioVinculoVO funcionarioVinculoVO)
        {
            List<FuncionarioVinculo> vinculos = this.SearchBySpecification(new FuncionarioVinculoFilterSpecification()
            {
                SeqFuncionario = funcionarioVinculoVO.SeqFuncionario,
                SeqTipoFuncionario = funcionarioVinculoVO.SeqTipoFuncionario,
                SeqEntidadeVinculo = funcionarioVinculoVO.SeqEntidadeVinculo
            }).ToList();

            if (vinculos.SMCAny())
            {
                vinculos.ForEach(vinculo =>
                {
                    if (vinculo.Seq != funcionarioVinculoVO.Seq)
                    {
                        if ((funcionarioVinculoVO.DataInicio >= vinculo.DataInicio
                           && (funcionarioVinculoVO.DataInicio <= (vinculo.DataFim ?? DateTime.MaxValue))) ||
                           ((funcionarioVinculoVO.DataFim ?? DateTime.MaxValue) >= vinculo.DataInicio
                           && ((funcionarioVinculoVO.DataFim ?? DateTime.MaxValue) <= (vinculo.DataFim ?? DateTime.MaxValue))) ||
                           (!funcionarioVinculoVO.DataFim.HasValue &&
                             funcionarioVinculoVO.DataInicio <= (vinculo.DataFim ?? DateTime.MaxValue)))
                        {
                            throw new FuncionarioVinculoConcidentesException();
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Se o tipo de funcionário selecionado tiver o campo "Tipo Registro
        /// Profissional Obrigatório" configurado e o os dados dos de registro
        /// profissional do funcionário que está sendo cadastrado estiver vazio ou o
        /// tipo diferente do obrigatyório.: Emitir a mensagem de erro e abortar
        /// operação.
        /// </summary>
        /// <param name="seqTipoFuncionario">Sequencial do tipo de funcionário</param>
        /// <param name="tipoRegistroProfissionalFuncioario">Tipo registro profissional do funcionario</param>
        public void ValidarRegistroProfissional(long seqTipoFuncionario, TipoRegistroProfissional? tipoRegistroProfissionalFuncioario)
        {
            var tipoRegistroProfissionalObrigatorio = TipoFuncionarioDomainService.SearchProjectionByKey(seqTipoFuncionario, p => p.TipoRegistroProfissionalObrigatorio);

            if (tipoRegistroProfissionalObrigatorio.HasValue)
            {
                if (!tipoRegistroProfissionalFuncioario.HasValue ||
                    tipoRegistroProfissionalFuncioario.Value != tipoRegistroProfissionalObrigatorio.Value)
                {
                    throw new FuncionarioVinculoTipoObrigatorioException(tipoRegistroProfissionalObrigatorio.SMCGetDescription());
                }
            }
        }
        /// <summary>
        /// Se o tipo de vínculo do funcionário em questão estiver com o flag
        ///"Obrigatório vínculo único" igual a "Sim" e existir um funcionário com o
        ///mesmo tipo de vínculo configurado cuja vigência coincida com o período
        ///de vigência sendo incluído/alterado, abortar a operação e emitir a
        ///mensagem: "Operação não permitida. O tipo de vínculo de funcionário
        ///"Descrição do tipo de vínculo" pode ser associado apenas a um único
        ///funcionário num mesmo período de vigência
        /// </summary>
        /// <param name="seqTipoFuncionario"></param>
        /// <param name="dataInicio"></param>
        /// <param name="dataFim"></param>
        public void ValidarObrigatorioVinculoUnico(long seq, long seqFuncionario, long seqTipoFuncionario, DateTime dataInicio, DateTime? dataFim = null)
        {
            var funcionario = new FuncionarioVinculoVO();
            //verifica se esta edidando
            if (seq == 0)
            {

                var spec = new FuncionarioVinculoFilterSpecification() { SeqTipoFuncionario = seqTipoFuncionario };

                funcionario = this.SearchProjectionBySpecification(spec, s => new FuncionarioVinculoVO()
                {
                    ObrigatorioVinculoUnico = s.TipoFuncionario.ObrigatorioVinculoUnico,
                    DataInicio = s.DataInicio,
                    DataFim = s.DataFim,
                    DescricaoTipoVinculo = s.TipoFuncionario.DescricaoMasculino
                }).OrderBy(o => o.DataInicio).LastOrDefault();
            }
            else
            {

                var spec = new FuncionarioVinculoFilterSpecification() { Seq = seq };

                var vinculos = this.SearchProjectionBySpecification(spec, s => new FuncionarioVinculoVO()
                {
                    ObrigatorioVinculoUnico = s.TipoFuncionario.ObrigatorioVinculoUnico,
                    DataInicio = s.DataInicio,
                    DataFim = s.DataFim,
                    DescricaoTipoVinculo = s.TipoFuncionario.DescricaoMasculino
                }).OrderBy(o => o.DataInicio).ToList();

                //valida sempre pelo 
                if (vinculos.Count() > 1)
                {
                    funcionario = vinculos[vinculos.Count - 2];
                }
                else
                {
                    funcionario = null;
                }
            }

            if (funcionario != null)
            {

                if (funcionario.ObrigatorioVinculoUnico)
                {

                    var specFuncionarioPossivelVinculo = new FuncionarioVinculoFilterSpecification() { SeqTipoFuncionario = seqTipoFuncionario, VinculoAtivo = true };
                    var funcionarioPossivelVinculo = this.SearchByKey(specFuncionarioPossivelVinculo);

                    if (funcionarioPossivelVinculo != null)
                    {
                        if (dataInicio <= funcionarioPossivelVinculo.DataInicio)
                        {
                            throw new ExisteFuncionarioComMesmoTipoDeVinculoVigenteException(funcionario.DescricaoTipoVinculo);
                        }

                        if (dataFim != null && funcionarioPossivelVinculo.DataFim != null || dataFim == null && funcionarioPossivelVinculo.DataFim != null)
                        {
                            if (dataInicio >= funcionarioPossivelVinculo.DataInicio && dataInicio <= funcionarioPossivelVinculo.DataFim || dataFim <= funcionarioPossivelVinculo.DataFim)
                            {
                                throw new ExisteFuncionarioComMesmoTipoDeVinculoVigenteException(funcionario.DescricaoTipoVinculo);
                            }
                        }

                        if (funcionarioPossivelVinculo.DataFim == null)
                            throw new ExisteFuncionarioComMesmoTipoDeVinculoVigenteException(funcionario.DescricaoTipoVinculo);
                    }
                }

            }
        }

        public List<SMCDatasourceItem> BuscarEntidadesPorVinculoFuncionario(long seqTipoFuncionario)
        {
            var spec = new FuncionarioVinculoFilterSpecification()
            {
                SeqTipoFuncionario = seqTipoFuncionario,
                PossuiEntidade = true
            };

            try
            {
                var entidades = this.SearchProjectionBySpecification(spec, s => new SMCDatasourceItem()
                {
                    Seq = s.EntidadeVinculo.Seq,
                    Descricao = s.EntidadeVinculo.Nome
                }).OrderBy(o => o.Descricao)
                  .GroupBy(g => g.Seq)
                  .Select(s => s.FirstOrDefault())
                  .ToList();

                return entidades;
            }
            catch
            {
                throw new Exception("Falha ao buscar as entidades que possui vínculo com o tipo de vinculo de funcionário selecionado.");
            }
        }
        public SMCDatasourceItem BuscarEntidadesSeq(long seqEntidadeVinculo)
        {
            var spec = new FuncionarioVinculoFilterSpecification()
            {
                SeqEntidadeVinculo = seqEntidadeVinculo
            };

            try
            {
                var entidades = this.SearchProjectionByKey(spec, s => new SMCDatasourceItem()
                {
                    Seq = s.EntidadeVinculo.Seq,
                    Descricao = s.EntidadeVinculo.Nome
                });

                return entidades;
            }
            catch
            {
                return null;
            }
        }

        public ParticipantesTagVO BuscarFuncionarioComVinculoUnico(string token)
        {
            var funcionario = this.SearchByKey(new FuncionarioVinculoFilterSpecification { TokenTipoFuncionario = token, VinculoAtivo = true },
                                               s => s.Funcionario.DadosPessoais,
                                               s => s.Funcionario.EnderecosEletronicos[0].EnderecoEletronico,
                                               s => s.Funcionario.Pessoa);

            var retorno = new ParticipantesTagVO()
            {
                NomeParticipante = funcionario.Funcionario.DadosPessoais.Nome,
                Cpf = funcionario.Funcionario.Pessoa.Cpf,
                EmailParticipante = funcionario.Funcionario.EnderecosEletronicos?.Where(w => w.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email).FirstOrDefault()?.EnderecoEletronico.Descricao,
                TagPosicaoAssinatura = $"[#{token}#]"
            };

            if (retorno == null)
                throw new FuncionarioNaoEncontradoException();

            return retorno;
        }

        public List<ParticipantesTagVO> BuscarFuncionariosEntidadeVinculo(long seqEntidade, string token)
        {
            var funcionarios = this.SearchBySpecification(new FuncionarioVinculoFilterSpecification { SeqEntidadeVinculo = seqEntidade, TokenTipoFuncionario = token, VinculoAtivo = true },
                                                          s => s.Funcionario.DadosPessoais,
                                                          s => s.Funcionario.EnderecosEletronicos[0].EnderecoEletronico,
                                                          s => s.Funcionario.Pessoa).ToList();

            var lista = new List<ParticipantesTagVO>();
            foreach (var funcionario in funcionarios)
            {
                var participante = new ParticipantesTagVO()
                {
                    NomeParticipante = funcionario.Funcionario.DadosPessoais.Nome,
                    Cpf = funcionario.Funcionario.Pessoa.Cpf,
                    EmailParticipante = funcionario.Funcionario.EnderecosEletronicos?.Where(w => w.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email).FirstOrDefault()?.EnderecoEletronico.Descricao,
                    TagPosicaoAssinatura = $"[#{token}#]"
                };
                lista.Add(participante);
            }

            if (!lista.Any())
                throw new FuncionarioNaoEncontradoException();

            return lista;
        }
    }
}