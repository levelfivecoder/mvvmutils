using System;
namespace MvvmUtils.Models
{
    public class ServiceStatusModel
    {

        public int statusId { get; set; }
        public String Message { get; set; }

        public bool IsSuccess()
        {

            if (statusId == 200)
            {
                return true;
            }

            return false;
        }

        public bool IsUnAuthorized()
        {

            if (statusId == 401)
            {
                return true;
            }

            return false;

        }
        public bool IsConflict()
        {

            if (statusId == 409)
            {
                return true;
            }

            return false;

        }

        public object data { get; set; }

    }
}
