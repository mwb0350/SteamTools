using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using System.Application.UI.ViewModels;

namespace System.Application.UI.Views.Windows
{
    public class LoginOrRegisterWindow : FluentWindow
    {
        readonly TextBox TbPhoneNumber;
        readonly TextBox TbSmsCode;

        public LoginOrRegisterWindow()
        {
            InitializeComponent();
            TbPhoneNumber = this.FindControl<TextBox>(nameof(TbPhoneNumber));
            TbSmsCode = this.FindControl<TextBox>(nameof(TbSmsCode));
            TbPhoneNumber.KeyUp += (_, e) =>
            {
                if (e.Key == Key.Return)
                {
                    TbSmsCode.Focus();
                }
            };
            TbSmsCode.KeyUp += (_, e) =>
            {
                if (e.Key == Key.Return)
                {
                    ((LoginOrRegisterWindowViewModel?)DataContext)?.Submit();
                }
            };
#if DEBUG
            this.AttachDevTools();
#endif
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);
            if (DataContext is LoginOrRegisterWindowViewModel vm)
            {
                vm.Close = Close;
                vm.TbPhoneNumberFocus = TbPhoneNumber.Focus;
                vm.TbSmsCodeFocus = TbSmsCode.Focus;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (DataContext is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}