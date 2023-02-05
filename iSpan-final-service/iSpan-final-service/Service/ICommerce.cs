using iSpan_final_service.Models;
using Microsoft.AspNetCore.Http;

namespace iSpan_final_service.Service
{
    public interface ICommerce
    {
        string GetCallBack(SendToNewebPayIn inModel);
        string GetPeriodCallBack(SendToNewebPayIn inModel);        
        Result GetCallbackResult(IFormCollection form);
    }
}
