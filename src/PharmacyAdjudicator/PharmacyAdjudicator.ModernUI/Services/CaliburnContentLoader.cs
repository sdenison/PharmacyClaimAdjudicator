using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using FirstFloor.ModernUI;
using FirstFloor.ModernUI.Windows;

using Caliburn.Micro;


namespace PharmacyAdjudicator.ModernUI.Services 
{
    public class CaliburnContentLoader : IContentLoader
    {
        Task<object> IContentLoader.LoadContentAsync(Uri uri, CancellationToken cancellationToken)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                throw new InvalidOperationException();
            }

            // scheduler ensures LoadContent is executed on the current UI thread
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            return Task.Factory.StartNew(() => LoadContent(uri), cancellationToken, TaskCreationOptions.None, scheduler);
        }

        protected virtual object LoadContent(Uri uri)
        {
            // don't do anything in design mode
            if (ModernUIHelper.IsInDesignMode)
            {
                return null;
            }

            var content = Application.LoadComponent(uri);
            if (content == null)
                return content;

            var vm = ViewModelLocator.LocateForView(content);
            if (vm == null)
                return content;

            if (content is DependencyObject)
            {
                ViewModelBinder.Bind(vm, content as DependencyObject, null);
            }
            return content;
        }
    }
}
