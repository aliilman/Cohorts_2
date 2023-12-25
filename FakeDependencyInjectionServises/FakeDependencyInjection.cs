using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cohorts_1.FakeDependencyInjectionServises
{
    public class FakeDependencyInjection : IFakeDependencyInjection
    {
        public void Yazdir(string message)
        {
            // Fake log işlemleri
            Console.WriteLine($"Fake Mesaj: {message}");
        }

    }
}