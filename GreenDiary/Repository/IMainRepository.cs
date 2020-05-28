using GreenDiary.Models;
using System.Threading.Tasks;

namespace GreenDiary.Repository
{
    public interface IMainRepository
    {
        Task<BaseResultModel> Mark();
    }
}