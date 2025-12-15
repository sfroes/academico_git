using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.Validators;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Domain.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.Validation;
using SMC.Localidades.Common.Areas.LOC.Enums;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class ReferenciaFamiliarDomainService : AcademicoContextDomain<ReferenciaFamiliar>
    {
        #region [ Service ]

        private ILocalidadeService LocalidadeService
        {
            get { return this.Create<ILocalidadeService>(); }
        }

        #endregion [ Service ]

        #region [ DomainService ]

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService
        {
            get { return this.Create<PessoaAtuacaoDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Buscar uma referência familiar da pessoa atuação
        /// </summary>
        /// <param name="seq">Sequencial de referência familiar</param>
        /// <returns>Registro de referência familiar</returns>
        public ReferenciaFamiliarVO BuscarReferenciaFamiliar(long seq)
        {
            var registro = this.SearchByKey(new SMCSeqSpecification<ReferenciaFamiliar>(seq),
                                                        IncludesReferenciaFamiliar.Enderecos
                                                      | IncludesReferenciaFamiliar.EnderecosEletronicos
                                                      | IncludesReferenciaFamiliar.Telefones);

            return registro.Transform<ReferenciaFamiliarVO>();

        }

        /// <summary>
        /// Buscar uma lista referências familiares da pessoa atuação
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Lista de referências familiares</returns>
        public SMCPagerData<ReferenciaFamiliarVO> BuscarReferenciasFamiliares(ReferenciaFamiliarFilterSpecification filtros)
        {
            int total;
            var registros = this.SearchBySpecification(filtros, out total,
                                                        IncludesReferenciaFamiliar.Enderecos
                                                      | IncludesReferenciaFamiliar.EnderecosEletronicos
                                                      | IncludesReferenciaFamiliar.Telefones).TransformList<ReferenciaFamiliarVO>();


            foreach (var item in registros)
                item.Enderecos.SMCForEach(f => f.NomePais = LocalidadeService.BuscarPais(f.CodigoPais).Nome);

            return new SMCPagerData<ReferenciaFamiliarVO>(registros, total);
        }

        /// <summary>
        /// Grava uma referência familiar aplicando suas validações e preenchendo o campo EnderecoEletronico com o e-mail informado
        /// </summary>
        /// <param name="referenciaFamiliar">Referência familiar a ser gravada</param>
        /// <returns>Sequencial da referência familiar gravado</returns>
        public long SalvarReferenciaFamiliar(ReferenciaFamiliarVO referenciaFamiliar)
        {
            var modelReferencia = referenciaFamiliar.Transform<ReferenciaFamiliar>();

            // Valida a referencia familiar
            var results = new ReferenciaFamiliarValidator().Validate(modelReferencia);
            if (!results.IsValid)
            {
                var errorList = new List<SMCValidationResults>();
                errorList.Add(results);
                throw new SMCInvalidEntityException(errorList);
            }

            this.SaveEntity(modelReferencia);

            return modelReferencia.Seq;
        }

        /// <summary>
        /// Exclui um registro de referência familiar se tiver alguma outra cadastrada para mesmo pessoa atuação,
        /// </summary>
        /// <param name="seq">Sequencial da referência familiar</param>
        public void ExcluirReferenciaFamiliar(long seq)
        {
            var registro = this.SearchByKey(new SMCSeqSpecification<ReferenciaFamiliar>(seq));

            var listReferencia = this.SearchProjectionBySpecification(new ReferenciaFamiliarFilterSpecification() { SeqPessoaAtuacao = registro.SeqPessoaAtuacao }, p => p.Seq);

            if (listReferencia.SMCCount(c => c != seq) == 0)
                throw new ReferenciaFamiliarExclusaoNaoPermitidaException();
            
            this.DeleteEntity(seq);
        }
    }
}