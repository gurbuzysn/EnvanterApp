using EnvanterApp.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Commands.Categories.UpdateCategory
{
    public class UpdateCategoryCommandRequest : IRequest<GeneralResponse<UpdateCategoryCommandResponse>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}
