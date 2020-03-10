using MvvmUtils.NavigationUtils;
using MvvmUtils.Viewmodel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MvvmUtils.ViewModel
{
    /// <summary>
    /// Base view model.
    /// </summary>
    public abstract class ViewModelBase : ObservableObject, IViewModel
    {
        #region Fields
        
        public ViewModelNavigation Navigation { get; set; }
        string otherTitle = string.Empty;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string OtherTitle
        {
            get { return otherTitle; }
            set { SetProperty(ref otherTitle, value); }
        }

        string title = string.Empty;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        string subtitle = string.Empty;

        /// <summary>
        /// Gets or sets the subtitle.
        /// </summary>
        /// <value>The subtitle.</value>
        public string Subtitle
        {
            get { return subtitle; }
            set { SetProperty(ref subtitle, value); }
        }

        string icon = string.Empty;

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public string Icon
        {
            get { return icon; }
            set { SetProperty(ref icon, value); }
        }

        bool isBusy;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (SetProperty(ref isBusy, value))
                    IsNotBusy = !isBusy;
            }
        }

        bool isNotBusy = true;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is not busy.
        /// </summary>
        /// <value><c>true</c> if this instance is not busy; otherwise, <c>false</c>.</value>
        public bool IsNotBusy
        {
            get { return isNotBusy; }
            set
            {
                if (SetProperty(ref isNotBusy, value))
                    IsBusy = !isNotBusy;
            }
        }

        bool canLoadMore = true;

        /// <summary>
        /// Gets or sets a value indicating whether this instance can load more.
        /// </summary>
        /// <value><c>true</c> if this instance can load more; otherwise, <c>false</c>.</value>
        public bool CanLoadMore
        {
            get { return canLoadMore; }
            set { SetProperty(ref canLoadMore, value); }
        }


        string header = string.Empty;

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>The header.</value>
        public string Header
        {
            get { return header; }
            set { SetProperty(ref header, value); }
        }

        string footer = string.Empty;

        /// <summary>
        /// Gets or sets the footer.
        /// </summary>
        /// <value>The footer.</value>
        public string Footer
        {
            get { return footer; }
            set { SetProperty(ref footer, value); }
        }

        public virtual void Dispose()
        {
            
        }
        #endregion
        #region Methods


        public virtual void ViewCreated()
        {
        }

        public virtual void ViewAppearing()
        {
        }

        public virtual void ViewAppeared()
        {
        }

        public virtual void ViewDisappearing()
        {
        }

        public virtual void ViewDisappeared()
        {
        }

        public virtual void ViewDestroy(bool viewFinishing = true)
        {
        }
        public virtual void Initialize()
        {

        }
        #endregion
    }
    public abstract class ViewModelBase<TNavigationParameter> : ViewModelBase, IViewModel<TNavigationParameter>
    {
        /// <summary>
        /// Initializes the View Model,
        /// passing a parameter from the calling View Model
        /// </summary>
        /// <param name="parameter">The parameter passed by the calling View Model.</param>
        public abstract void Initialize(TNavigationParameter parameter);
    }
    #region Commented
    /*
    //TODO: Not possible to name MvxViewModel, name is MvxViewModelResult for now
    public abstract class ViewModelBase<TResult> : ViewModelBase, IMvxViewModelResult<TResult>
    {
        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            if (viewFinishing && CloseCompletionSource != null && !CloseCompletionSource.Task.IsCompleted && !CloseCompletionSource.Task.IsFaulted)
                CloseCompletionSource?.TrySetCanceled();

            base.ViewDestroy(viewFinishing);
        }
    }

    public abstract class ViewModelBase<TParameter, TResult> : ViewModelBase<TResult>, IViewModel<TParameter, TResult>
    {
        public abstract void Prepare(TParameter parameter);
    }
    */
    #endregion
}
