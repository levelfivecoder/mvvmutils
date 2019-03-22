using System;
using System.Threading.Tasks;
using MvvmUtils.Exceptions;
using MvvmUtils.HttpHandlers;
using MvvmUtils.Models;
using MvvmUtilsExample.BusinessLayer.Models;
using MvvmUtilsExample.ServiceAccessLayer;

namespace MvvmUtilsExample.RepositoryLayer
{
    public class ImagesRepository
    {
        public ImagesRepository()
        {
        }
        public async Task<ServiceStatusModel> GetPhotostList()
        {
            String uri;
            ServiceStatusModel statusModel = new ServiceStatusModel();
            uri = "images/latest";
            var serviceProvider = new ServiceProvider(new ImagesHandler(uri));
            Images response;
            try
            {
                response = (Images)await serviceProvider.GetData();
                statusModel.statusId = 200;
                statusModel.data = response;
            }
            catch (UnSuccessfullStatusCodeException ex)
            {
                statusModel.statusId = ex.StatusCode;
                statusModel.Message = ex.Message;
            }
            catch (Exception exx)
            {

            }
            return statusModel; ;
        }
    }
}
