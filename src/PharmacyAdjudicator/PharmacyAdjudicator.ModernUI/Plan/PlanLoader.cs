﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using FirstFloor.ModernUI;
using FirstFloor.ModernUI.Windows;

namespace PharmacyAdjudicator.ModernUI.Plan
{
    public class PlanLoader : DefaultContentLoader
    {
        PlanWorkspaceViewModel _planViewModel;

        public PlanLoader(PlanWorkspaceViewModel planViewModel)
        {
            _planViewModel = planViewModel;
        }

        protected override object LoadContent(Uri uri)
        {
            //return base.LoadContent(uri);
            //return new 

            // don't do anything in design mode
            if (ModernUIHelper.IsInDesignMode)
            {
                return null;
            }

            //var content = Application.LoadComponent(uri);
            var plan = _planViewModel.Plans.FirstOrDefault(p => p.PlanId == uri.OriginalString);
            var content = Application.LoadComponent(new Uri("/Plan/PlanEditView.xaml", UriKind.Relative));
            
            if (content == null)
                return content;

            //var vm = ViewModelLocator.LocateForView(content);
            var vm = new PlanEditViewModel(plan);
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
