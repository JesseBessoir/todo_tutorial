using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Contracts.Services;

public interface IUnitOfWork
{
    void Begin();
    void Commit();
    void Rollback();
    bool IsInTransaction();
}
