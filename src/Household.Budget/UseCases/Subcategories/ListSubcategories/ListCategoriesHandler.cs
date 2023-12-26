﻿using Household.Budget.Contracts.Data;

using MediatR;

namespace Household.Budget.UseCases.Subcategories.ListSubcategories;

public class ListSubcategoriesHandler : IRequestHandler<ListSubcategoriesRequest, ListSubcategoriesResponse>
{
    private readonly ISubcategoryRepository _repository;

    public ListSubcategoriesHandler(ISubcategoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<ListSubcategoriesResponse> Handle(ListSubcategoriesRequest request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllAsync(request.PageSize, request.PageNumber, request.UserId, cancellationToken);
        return new ListSubcategoriesResponse(result);
    }
}