﻿namespace Budget.DAL.Repositories.Interfaces
{
    public interface IUnitOfWOrk
    {
        ICategoryTypeRepository CategoryTypeRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IFinanceTransactionRepository FinanceTransactionRepository { get; }
    }
}