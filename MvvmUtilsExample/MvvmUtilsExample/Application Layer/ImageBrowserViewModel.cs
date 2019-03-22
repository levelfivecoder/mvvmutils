using System.Collections.Generic;
using MvvmUtils;
using MvvmUtils.Models;
using MvvmUtilsExample.BusinessLayer.Models;

namespace MvvmUtilsExample.Application_Layer
{
    public class ImageBrowserViewModel : BaseViewModel
    {
        public ImageBrowserViewModel()
        {

            getPhotoListAsync();
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
        private async System.Threading.Tasks.Task getPhotoListAsync()
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
    }
}
