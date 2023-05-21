using FluentValidation;
using HomeApi.Controllers;

namespace HomeApi.Contracts.Validation
{
    class DeleteDeviceRequestValidator : AbstractValidator<DeleteDeviceRequest>
    {
        public DeleteDeviceRequestValidator()
        {
            RuleFor(_ => _.Id).NotEmpty();
            RuleFor(_ => _.Name).NotEmpty();
            RuleFor(_ => _.RoomLocation).NotEmpty();
        }
    }
}
