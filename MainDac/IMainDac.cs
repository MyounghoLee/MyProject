using System;
using System.Collections.Generic;
using System.Text;

namespace dac.MainDac
{
    public interface IMainDac
    {
        List<T> SelectProductnfo<T>(string partnerId);
    }
}
