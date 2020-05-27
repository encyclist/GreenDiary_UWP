using GreenDiary.Models;
using System.Threading.Tasks;

namespace GreenDiary.Repository
{
    public interface ILoginRepository
    {
        Task<BaseResultModel> GetCode(string phone);
    }
}