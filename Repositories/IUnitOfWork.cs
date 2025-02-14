﻿namespace ProductsCategoryAPI.Repositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        Task Commit();
        void Dispose();
    }
}
