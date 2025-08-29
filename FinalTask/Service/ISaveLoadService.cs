using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask.Service
{
    internal interface ISaveLoadService<T>
    {
       public void SaveData(T save, string identificator);
        public T LoadData(string identificator);
       

    }
}
