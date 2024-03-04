﻿using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Infra.Data.Context;
using Household.Budget.Infra.Data;

namespace Household.Budget;

public class CategoryData : Data<Category>, ICategoryData
{
    public CategoryData(IMongoDbContext<Category> context)
        : base(context) { }
}
