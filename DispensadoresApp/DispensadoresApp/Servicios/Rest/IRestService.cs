using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace DispensadoresApp.Servicios.Rest
{
    interface IRestService<T> //<T> denotes only the type of the parameter, example if imnot shure 
    {
        Task<Tuple<bool, string>> SendRequest(T model);
    }

}
