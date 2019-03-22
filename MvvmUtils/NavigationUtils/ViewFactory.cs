using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MvvmUtils.NavigationUtils
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ViewTypeAttribute : Attribute
    {
        public Type ViewType { get; private set; }

        public ViewTypeAttribute(Type viewType)
        {
            ViewType = viewType;
        }
    }

    static public class ViewFactory
    {
        static Dictionary<Type, Type> typeDictionary = new Dictionary<Type, Type>();
        public static void Register<TView, TViewModel>()
            where TView : Page
            where TViewModel : BaseViewModel
        {
            typeDictionary[typeof(TViewModel)] = typeof(TView);
        }
        /// <summary>
        /// Create a page from viewmodel
        /// </summary>
        /// <param name="viewModelType">Viewmodel type</param>
        /// <returns></returns>
        public static Page CreatePage(Type viewModelType)
        {
            Type viewType = null;

            if (typeDictionary.ContainsKey(viewModelType))
            {
                viewType = typeDictionary[viewModelType];
            }
            else
            {
                //	var attribute = viewModelType.GetTypeInfo ().GetCustomAttribute<ViewTypeAttribute> ();
                //	if (attribute == null)
                throw new InvalidOperationException("Unknown View for ViewModelType");
                //	viewType = attribute.ViewType;
                //	typeDictionary[viewModelType] = viewType; // cache
            }

            var page = (Page)Activator.CreateInstance(viewType);
            var viewModel = (BaseViewModel)Activator.CreateInstance(viewModelType);

            viewModel.Navigation = new ViewModelNavigation(page);
            page.BindingContext = viewModel;

            return page;
        }
        /// <summary>
        /// Create a page from viewmodel
        /// </summary>
        /// <param name="viewModel">Viewmodel type</param>
        /// <returns></returns>
        public static Page CreatePage(BaseViewModel viewModel)
        {
            Type viewType = null;
            if (typeDictionary.ContainsKey(viewModel.GetType()))
            {
                viewType = typeDictionary[viewModel.GetType()];
            }
            else
            {
                throw new InvalidOperationException("Unknown View for ViewModel object");
                
            }

            var page = (Page)Activator.CreateInstance(viewType);

            viewModel.Navigation = new ViewModelNavigation(page);
            page.BindingContext = viewModel;

            return page;
        }


        public static Page CreatePage<TViewModel>()
            where TViewModel : BaseViewModel
        {
            var viewModelType = typeof(TViewModel);
            return CreatePage(viewModelType);
        }
    }
}
