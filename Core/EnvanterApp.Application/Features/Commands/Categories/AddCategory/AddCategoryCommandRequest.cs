using EnvanterApp.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Commands.Categories.AddCategory
{
    public class AddCategoryCommandRequest : IRequest<GeneralResponse<AddCategoryCommandResponse>>
    {
        public string CategoryName { get; set; }
        public IFormFile? CategoryImage { get; set; }
    }
}
