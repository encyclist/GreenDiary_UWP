using GreenDiary.Models;
using System.Threading.Tasks;

namespace GreenDiary.Repository
{
    public interface IMainRepository
    {
        Task<BaseResultModel> Mark();
        Task<BaseResultModel> GetDiaryList(int page, int limit);
        Task<BaseResultModel> GetLibraryList(int limit);
    }
}