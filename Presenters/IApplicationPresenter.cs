using Shared.Interfaces;

namespace Presenters
{
    public interface IApplicationPresenter
    {
        void AttachView(IEmployeeView view);
        void DetachView();
        void Initialize();
    }
}