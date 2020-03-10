using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using MvvmUtils.ViewModel;

namespace MvvmUtils.NavigationUtils
{
    /// <summary>
    /// Factory class to get page instance
    /// </summary>
    public static class ViewFactory
    {
        
        /// <summary>
        /// Create a page from viewmodel
        /// </summary>
        /// <param name="viewModelType">Viewmodel type</param>
        /// <returns></returns>
        public static Page CreatePage(Type viewModelType)
        {
            try
            {
                var viewTypeName = viewModelType.Name.Replace("ViewModel", "Page");
                var viewType = AppDomain.CurrentDomain.GetAssemblies().ToList()
                 .SelectMany(a => a.GetTypes())
                 .FirstOrDefault(t => t.Name.ToString() == viewTypeName);
                var page = (Page)Activator.CreateInstance(viewType);

                var viewModel = (ViewModelBase)Activator.CreateInstance(viewModelType);

                viewModel.Navigation = new ViewModelNavigation(page);
                page.BindingContext = viewModel;

                return page;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Unknown View for ViewModel object");
            }
        }
        /// <summary>
        /// Create a page from viewmodel
        /// </summary>
        /// <typeparam name="TParameter"></typeparam>
        /// <param name="viewModelType"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static Page CreatePage<TParameter>(Type viewModelType, TParameter parameter)
        {
            try
            {
                var viewTypeName = viewModelType.Name.Replace("ViewModel", "Page");
                var viewType = AppDomain.CurrentDomain.GetAssemblies().ToList()
                 .SelectMany(a => a.GetTypes())
                 .FirstOrDefault(t => t.Name.ToString() == viewTypeName);
                var page = (Page)Activator.CreateInstance(viewType);
                var viewModel = (ViewModelBase<TParameter>)Activator.CreateInstance(viewModelType);
                viewModel.Navigation = new ViewModelNavigation(page);
                page.BindingContext = viewModel;
                viewModel.Initialize(parameter);
                return page;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Unknown View for ViewModel object");
            }
        }
        /// <summary>
        /// Create a page from viewmodel
        /// </summary>
        /// <param name="viewModel">Viewmodel type</param>
        /// <returns></returns>
        public static Page CreatePage(ViewModelBase viewModel)
        {
            try
            {
                var viewTypeName = viewModel.GetType().Name.Replace("ViewModel", "Page");
                var viewType = AppDomain.CurrentDomain.GetAssemblies().ToList()
                 .SelectMany(a => a.GetTypes())
                 .FirstOrDefault(t => t.Name.ToString() == viewTypeName);
                var page = (Page)Activator.CreateInstance(viewType);

                if (page == null)
                {
                    throw new InvalidOperationException("Unknown View for ViewModel object");
                }
                viewModel.Navigation = new ViewModelNavigation(page);
                page.BindingContext = viewModel;

                return page;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Unknown View for ViewModel object");
            }
        }

        /// <summary>
        /// Create a page from viewmodel type
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <returns></returns>
        public static Page CreatePage<TViewModel>()
            where TViewModel : ViewModelBase
        {
            var viewModelType = typeof(TViewModel);
            return CreatePage(viewModelType);
        }
        /// <summary>
        /// Create a page from viewmodel type
        /// navigation parameter
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <typeparam name="TParameter"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static Page CreatePage<TViewModel, TParameter>(TParameter parameter)
            where TViewModel : ViewModelBase
        {
            var viewModelType = typeof(TViewModel);
            return CreatePage<TParameter>(viewModelType, parameter);
        }
    }
}
