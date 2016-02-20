// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the StoryBoardViewContainer.cs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Views
{
    using MvvmCross.Core.ViewModels;
    using MvvmCross.iOS.Views;
    using System;
    using UIKit;

    /// <summary>
    /// Defines the StoryBoardViewContainer type.
    /// </summary>
    public class StoryBoardViewContainer : MvxIosViewsContainer
    {
        /// <summary>
        /// Creates an iOS view from a storyboard.
        /// </summary>
        /// <param name="viewType">Type of the view.</param>
        /// <param name="request">The request.</param>
        /// <returns>A View</returns>
        protected override IMvxIosView CreateViewOfType(
                Type viewType,
                MvxViewModelRequest request)
        {
            UIStoryboard storyboard;

            try
            {
                storyboard = UIStoryboard.FromName(viewType.Name, null);
            }

            catch (Exception)
            {
                //// this is not actually implemented by the ninja coder
                //// you will need to manually create this default storyboard
                //// if required.

                storyboard = UIStoryboard.FromName("StoryBoard", null);
            }

            return (IMvxIosView)storyboard.InstantiateViewController(viewType.Name);
        }
    }
}
