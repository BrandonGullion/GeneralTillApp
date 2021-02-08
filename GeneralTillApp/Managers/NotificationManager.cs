using System;
using System.Collections.Generic;
using System.Linq;
using GeneralTillApp.Models;

namespace GeneralTillApp.Managers
{
    public class NotificationManager
    {
        public object NotificationItem { get; private set; }
        public NotificationStatus Status { get; private set; }

        public enum NotificationStatus
        {
            None,
            Success,
            Failed
        }

        /// <summary>
        /// Prepares the notification depending on the passed in type
        /// </summary>
        /// <typeparam name="T">Generic class that will be used to display the contents name</typeparam>
        /// <param name="obj">data object for prop information</param>
        /// <param name="status">Enum that will help to set CSS and Content information</param>
        public void AddNotification<T>(T obj, NotificationStatus status)
        {
            if (obj != null)
                NotificationItem = obj;

            Status = status;
        }

        /// <summary>
        /// Removes and notification status and nulls to passed object
        /// </summary>
        public void RemoveNotification()
        {
            NotificationItem = null;
            Status = NotificationStatus.None;
        }

        public object GetNotificationType()
        {
            return NotificationItem.GetType();
        }

        /// <summary>
        /// Generates the final Toast options
        /// </summary>
        /// <returns>Notification options that are used to load the toast notifications</returns>
        public NotificationOption GenerateToastOption()
        {
            var notificationOption = new NotificationOption();
         
            if(Status == NotificationStatus.Success)
            {
                notificationOption.ToastTitle = "Success!";

                if (NotificationItem.GetType() == typeof(Product))
                    notificationOption.ToastContent = $"Successfully added {(NotificationItem as Product).Title} to the Database";
                if (NotificationItem.GetType() == typeof(Customer))
                    notificationOption.ToastContent = $"Successfully added {(NotificationItem as Customer).FirstName} to the Database";

                notificationOption.ToastCSS = "e-toast-success";
                return notificationOption;
            }

            if(Status == NotificationStatus.Failed)
            {
                notificationOption.ToastTitle = "Error!";

                if (NotificationItem.GetType() == typeof(Product))
                    notificationOption.ToastContent = $"There was an error adding {(NotificationItem as Product).Title} to the Database";
                if (NotificationItem.GetType() == typeof(Customer))
                    notificationOption.ToastContent = $"There was an error adding {(NotificationItem as Customer).FirstName} to the Database";

                notificationOption.ToastCSS = "e-toast-error";
                return notificationOption;
            }

            // TODO :: Write code to inform there was an error with the notification 
            return notificationOption;
        }
    }
}
