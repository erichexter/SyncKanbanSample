using ShortBus;

namespace SyncKanban.Controllers
{
    public class HomeBoardViewModelQuery:IQuery<HomeBoardViewModel>
    {
        public int Id { get; set; }
    }
}