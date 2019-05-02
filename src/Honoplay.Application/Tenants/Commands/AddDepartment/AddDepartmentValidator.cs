using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Honoplay.Application.Tenants.Commands.AddDepartment
{
    public class AddDepartmentValidator : AbstractValidator<AddDepartmentCommand>
    {
        public AddDepartmentValidator()
        {
            RuleFor(x => x.Departments).NotEmpty().NotNull();
            RuleFor(x => x.TenantId).NotEmpty().NotNull();
        }
    }
}
