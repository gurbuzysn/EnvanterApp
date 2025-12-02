using EnvanterApp.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Commands.Categories.RemoveCategory
{
    public class RemoveCategoryCommandRequest : IRequest<GeneralResponse<RemoveCategoryCommandResponse>>
    {
        public Guid Id { get; set; }
    }
}
