using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EPayroll.Behaviors
{
    public class WebViewEventToCommandBehavior : Behavior<View>
    {
        public WebView WebView { get; private set; }

        public static readonly BindableProperty CommandProperty = 
            BindableProperty.Create("Command", typeof(ICommand), typeof(WebViewEventToCommandBehavior), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            WebView = bindable as WebView;
            bindable.BindingContextChanged += OnBindingContextChanged;
            (bindable as WebView).Navigated += Navigated;
        }

        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);
        }

        void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = WebView.BindingContext;
        }

        void Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (Command == null) return;

            if (Command.CanExecute(e.Url))
                Command.Execute(e.Url);
        }
    }
}
