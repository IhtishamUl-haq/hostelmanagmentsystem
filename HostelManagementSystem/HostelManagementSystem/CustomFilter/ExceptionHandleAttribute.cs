using HostelManagementSystem.Models;
using System;
 
 
using System.Web.Mvc;

namespace HostelManagementSystem.CustomFilter
{
    public class ExceptionHandleAttribute:FilterAttribute
    {
        DbHostelManagementSystemEntities _logingError = new DbHostelManagementSystemEntities();
        public void OnException(Exception filterException)
        {
            do
            {
                LogingException logingException = new LogingException
                {
                    ExpMessage = filterException.Message,
                    Type = filterException.GetType().ToString(),
                    ExpTrace = filterException.StackTrace,
                    ExpDate = DateTime.Today
                };
                _logingError.LogingExceptions.Add(logingException);
                _logingError.SaveChanges();
                filterException=filterException.InnerException;
            } while (filterException!=null);  
        }
}
}