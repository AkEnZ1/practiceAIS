using System;

namespace Shared.Interfaces
{
    public interface IView
    {
        void ShowMessage(string message);
        void ShowError(string error);
    }
}