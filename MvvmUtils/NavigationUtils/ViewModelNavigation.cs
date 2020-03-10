using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using MvvmUtils.ViewModel;

namespace MvvmUtils.NavigationUtils
{
    public class ViewModelNavigation
    {

        readonly Page implementor;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="implementor">page</param>
        public ViewModelNavigation(Page implementor)
        {
            this.implementor = implementor;
        }
        /// <summary>
        /// Method to add a page
        /// </summary>
        /// <param name="page">page</param>
        public void Push(Page page)
        {

            implementor.Navigation.PushAsync(page);
        }

        /// <summary>
        /// add a page with animation
        /// </summary>
        /// <param name="page">page</param>
        /// <param name="animate">is animation required</param>
        public void Push(Page page, bool animate)
        {
            implementor.Navigation.PushAsync(page, animate);
            implementor.Navigation.RemovePage(implementor.Navigation.NavigationStack.
                ElementAt(implementor.Navigation.NavigationStack.Count - 2));
        }

        /// <summary>
        /// Push viewmodel
        /// </summary>
        /// <typeparam name="TViewModel">viewmodel</typeparam>
        public void Push<TViewModel>()
            where TViewModel : ViewModelBase
        {
            Push(ViewFactory.CreatePage<TViewModel>());
        }
        /// <summary>
        /// Push viewmodel
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <typeparam name="TParameter"></typeparam>
        /// <param name="parameter"></param>
        public void Push<TViewModel,TParameter>(TParameter parameter)
            where TViewModel : ViewModelBase
        {
            Push(ViewFactory.CreatePage<TViewModel, TParameter>(parameter));
        }

        /// <summary>
        /// remove page from stack
        /// </summary>
        public void Pop()
        {
            implementor.Navigation.PopAsync();
        }

        /// <summary>
        /// remove page from stack with animation
        /// </summary>
        /// <param name="animated">is animation required</param>
        public void Pop(bool animated)
        {
            implementor.Navigation.PopAsync(animated);
        }

        /// <summary>
        /// Remove all pages up to root page
        /// </summary>
        public void PopToRoot()
        {
            implementor.Navigation.PopToRootAsync();
        }

        /// <summary>
        /// Remove all pages up to root page
        /// </summary>
        public async Task PopToRootAsync()
        {
            await implementor.Navigation.PopToRootAsync();
        }

        /// <summary>
        /// Push modal page
        /// </summary>
        public void PushModal(Page page)
        {
            implementor.Navigation.PushModalAsync(page);
        }

        /// <summary>
        /// Push modal viewmodel
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        public void PushModal<TViewModel>()
            where TViewModel : ViewModelBase
        {
            PushModal(ViewFactory.CreatePage<TViewModel>());
        }

        /// <summary>
        /// Push modal viewmodel
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        public void PushModal<TViewModel,TParameter>(TParameter parameter)
            where TViewModel : ViewModelBase
        {
            PushModal(ViewFactory.CreatePage<TViewModel, TParameter>(parameter));
        }

        /// <summary>
        /// remove modal page from stack
        /// </summary>
        public void PopModal()
        {
            var modalParent = implementor;
            while (modalParent.Parent as Page != null)
                modalParent = (Page)modalParent.Parent;

            implementor.Navigation.PopModalAsync();

        }
        /// <summary>
        /// Method to remove page from the stack
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RemovePage<T>()
        {
            var navigationStack = implementor.Navigation.NavigationStack.ToList();
            foreach (Page page in navigationStack)
            {
                if (page.GetType() == typeof(T))
                {
                    implementor.Navigation.RemovePage(page);
                }
            }
        }
        /// <summary>
        /// Method to get viewmodel from stack 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ViewModelBase GetViewModel(int index)
        {
            try
            {
                if (implementor.Navigation.NavigationStack.Count() > index)
                {
                    return (ViewModelBase)this.implementor.Navigation.NavigationStack.ElementAt(index).BindingContext;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }
        /// <summary>
        /// Method to get stack count
        /// </summary>
        /// <returns></returns>
        public int GetViewModelCount()
        {
            try
            {
                return this.implementor.Navigation.NavigationStack.Count();
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
