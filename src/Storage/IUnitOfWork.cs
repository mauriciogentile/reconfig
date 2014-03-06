﻿namespace Reconfig.Storage
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}
