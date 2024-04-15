using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaTestsLinux.Views.UserControls;

namespace AvaloniaTestsLinux.ViewModels
{
    internal class AppViewLocator : ReactiveUI.IViewLocator
    {
        public IViewFor ResolveView<T>(T viewModel, string contract = null) => viewModel switch
        {
            CreateTestUserControlViewModel context => new CreateTestUserControl() { DataContext = context },
            CreateGroupUserControlViewModel context => new CreateGroupUserControl() { DataContext = context },
            MainUserControlViewModel context => new MainUserControl { DataContext = context },
            GroupUserControlViewModel context => new GroupUserControl() { DataContext = context },
            TestUserControlViewModel context => new TestUserControl() { DataContext = context },
            _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
        };
    }
}
