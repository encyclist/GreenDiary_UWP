using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenDiary.Repository.Rests
{
    class RestContosoRepository : IContosoRepository
    {
        private readonly string _url;

        public RestContosoRepository(string url)
        {
            _url = url;
        }
        public ILoginRepository Logins => new RestLoginRepository(_url);
    }
}
