using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenDiary.Repository
{
    public interface IContosoRepository
    {
        ILoginRepository Logins { get; }
    }
}
