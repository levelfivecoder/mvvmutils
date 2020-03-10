using System.Collections.Generic;
using MvvmUtils;
using MvvmUtils.Models;
using MvvmUtils.ViewModel;
using MvvmUtilsExample.BusinessLayer.Models;

namespace MvvmUtilsExample.Application_Layer
{
    public class ImageBrowserViewModel : ViewModelBase<int>
    {
        public ImageBrowserViewModel()
        {

            GetPhotoListAsync();
        }
        ObservableRangeCollection<Image> images = null;

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        /// <value>The images.</value>
        public ObservableRangeCollection<Image> Images
        {
            get { return images; }
            set { SetProperty(ref images, value); }
        }
        /// <summary>
        /// Method to get images
        /// </summary>
        /// <returns></returns>
        private async System.Threading.Tasks.Task GetPhotoListAsync()
        {
            IsBusy = true;
            RepositoryLayer.ImagesRepository repository = new RepositoryLayer.ImagesRepository();
            ServiceStatusModel statusModel = await repository.GetPhotostList();
            if (!statusModel.IsSuccess())
            {
                IsBusy = false;
            }
            else
            {
                Images = new ObservableRangeCollection<Image>();
              
                foreach(Image image in (statusModel.data as Images).images)
                {
                    Images.Add(image);
                }
            }
            Images = new ObservableRangeCollection<Image>(Images);
            IsBusy = false;
        }

        public override void Initialize(int parameter)
        {
           
        }
        public override void ViewAppearing()
        {
            base.ViewAppearing();
        }
        public override void ViewAppeared()
        {
            base.ViewAppeared();
        }
        public override void ViewCreated()
        {
            base.ViewCreated();
        }
        public override void ViewDisappearing()
        {
            base.ViewDisappearing();
        }
        public override void ViewDisappeared()
        {
            base.ViewDisappeared();
        }
        public override void ViewDestroy(bool viewFinishing = true)
        {
            base.ViewDestroy(viewFinishing);
        }
    }
}
