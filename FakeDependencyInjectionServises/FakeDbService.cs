using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cohorts_1.FakeDependencyInjectionServises
{
    public class FakeDbService : IFakeDbService
    {
        public void SaveChange()
        {
            Console.WriteLine("Database Değişiklikleri Kaydetti");
        }
    }
}