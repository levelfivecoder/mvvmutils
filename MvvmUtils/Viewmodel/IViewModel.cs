using System;
using System.ComponentModel;

namespace MvvmUtils.Viewmodel
{
    /// <summary>
    /// The View Model, all View-related logic belongs here.
    /// </summary>
    public interface IViewModel : IDisposable
    {
    }

    /// <summary>
    /// The View Model, all View-related logic belongs here.
    /// </summary>
    public interface IViewModel<in TNavigationParameter> : IViewModel
    {
        /// <summary>
        /// Initializes the View Model when it is called by the Presenter,
        /// passing a parameter from the calling View Model
        /// </summary>
        /// <param name="parameter">The parameter passed by the calling View Model.</param>
        void Initialize(TNavigationParameter parameter);
    }
    /// <summary>
    /// The View Model, all View-related logic belongs here.
    /// </summary>
    public interface IViewModel<in TNavigationParameter,out TResult> : IViewModel
    {
        /// <summary>
        /// Initializes the View Model when it is called by the Presenter,
        /// passing a parameter from the calling View Model
        /// </summary>
        /// <param name="parameter">The parameter passed by the calling View Model.</param>
        void Initialize(TNavigationParameter parameter);
    }
}
