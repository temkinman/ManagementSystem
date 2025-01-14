using BuildingBlocks.Interfaces;
using Catalog.Domain.Entities;

namespace Catalog.Application.Interfaces;

public interface ICategoryRepository : IBaseItemRepository<Category>;