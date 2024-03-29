﻿using Finance.Application.ViewModels.Category;
using MediatR;

namespace Finance.Application.Queries.Category.GetById
{
    public class GetCategoryByIdQuery : IRequest<CategoryDetailsViewModel>
    {
        public int Id { get; set; }

        public GetCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }
}
