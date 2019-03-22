using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

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
            implementor.Navigation.RemovePage(implementor.Navigation.NavigationStack.ElementAt(implementor.Navigation.NavigationStack.Count - 2));
        }

        /// <summary>
        /// Push viewmodel
        /// </summary>
        /// <typeparam name="TViewModel">viewmodel</typeparam>
        public void Push<TViewModel>()
            where TViewModel : BaseViewModel
        {
            Push(ViewFactory.CreatePage<TViewModel>());
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
            where TViewModel : BaseViewModel
        {
            PushModal(ViewFactory.CreatePage<TViewModel>());
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

        public BaseViewModel GetViewModel(int index)
        {
            try
            {
                if (implementor.Navigation.NavigationStack.Count() > index)
                {
                    return (BaseViewModel)this.implementor.Navigation.NavigationStack.ElementAt(index).BindingContext;
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
