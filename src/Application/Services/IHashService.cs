using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IHashService
    {
        void Create(string value, out byte[] hashedValue, out byte[] key);
        bool Verify(string value, byte[] hashedValue,byte[] keyToCompare);

    }
}
