using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using Caliburn.Micro;
using PharmacyAdjudicator.ModernUI.Shell;
using PharmacyAdjudicator.ModernUI.Extensions;

namespace PharmacyAdjudicator.ModernUI
{
    public class AppBootstrapper : BootstrapperBase // Bootstrapper<IShellViewModel>
    {
        private static CompositionContainer _container;

        public AppBootstrapper()
        {
            StartRuntime();
        }

        public static T GetInstance<T>()
        {
            string contract = AttributedModelServices.GetContractName(typeof(T));

            var sexports = _container.GetExportedValues<object>(contract);
            if (sexports.Count() > 0)
                return sexports.OfType<T>().First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override void Configure()
        {
            // Add New ViewLocator Rule
            //ViewLocator.NameTransformer.AddRule(
            //    @"(?<nsbefore>([A-Za-z_]\w*\.)*)?(?<nsvm>ViewModels\.)(?<nsafter>([A-Za-z_]\w*\.)*)(?<basename>[A-Za-z_]\w*)(?<suffix>ViewModel$)",
            //    @"${nsbefore}Views.${nsafter}${basename}View",
            //    @"(([A-Za-z_]\w*\.)*)?ViewModels\.([A-Za-z_]\w*\.)*[A-Za-z_]\w*ViewModel$"
            //);

            //_container = new CompositionContainer(
            //        new AggregateCatalog(
            //        new AssemblyCatalog(typeof(IShellViewModel).Assembly),
            //        AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>().FirstOrDefault()
            //    )
            //);

            _container = new CompositionContainer(
                    new AggregateCatalog(
                    new AssemblyCatalog(typeof(IShellViewModel).Assembly)
                )
            );

            var batch = new CompositionBatch();
            batch.AddExport<IWindowManager>(() => new WindowManager());
            batch.AddExport<IEventAggregator>(() => new EventAggregator());
            batch.AddExport<Interface.INavigationService>(() => new Services.NavigationService());
            batch.AddExport<Interface.IDialog>(() => new Services.DialogService());
            //batch.AddExport<Interface.IOpenViewModels>(() => new Services.OpenViewModels());
            //
            //batch.AddExport<Shell.IShellViewModel>(() => new ShellViewModel());
            _container.Compose(batch);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;

            var exports = _container.GetExportedValues<object>(contract);
            return exports.FirstOrDefault();
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            var ret = Enumerable.Empty<object>();

            string contract = AttributedModelServices.GetContractName(serviceType);
            return _container.GetExportedValues<object>(contract);
        }

        protected override void BuildUp(object instance)
        {
            _container.SatisfyImportsOnce(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<IShellViewModel>();
        } 
    }
}
