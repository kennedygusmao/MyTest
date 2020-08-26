using FluentValidation;
using MT.Domain.Entities;
using System;

namespace MT.Service.Validations
{
    public class CaminhaoValidation : AbstractValidator<Caminhao>
    {
        public CaminhaoValidation()
        {
            RuleFor(f => f.AnoFabricacao)
                  .GreaterThan(0)
                  .WithMessage("O campo {PropertyName} precisa ser fornecido");


            RuleFor(f => f.AnoModelo)
                  .GreaterThan(0)
                  .WithMessage("O campo {PropertyName} precisa ser fornecido");

                      

            RuleFor(f => f).Custom((dto, context) =>
            {

                if (dto.AnoFabricacao != DateTime.Now.Year)
                {
                    context.AddFailure("AnoFabricacao", "O campo ano de fabricação  deve ser o ano atual");
                }


                if (dto.AnoModelo < DateTime.Now.Year || dto.AnoModelo > DateTime.Now.AddYears(1).Year)
                {
                    context.AddFailure("AnoModelo", "O campo ano de modelo  deve ser o ano atual ou ano subsequente");
                }

                if (dto.Modelo == null)
                    context.AddFailure("Modelo", "O campo modelo deve ser informado");


                if (dto.Modelo.Descricao == string.Empty)
                    context.AddFailure("Descricao", "O campo descrição do modelo  deve ser informado");

                if (dto.Modelo.Descricao != "FH" && dto.Modelo.Descricao != "FM")
                    context.AddFailure("Descricao", "Cadastrar apenas caminhões do modelo FH ou FM");

            });
        }
    }
}
