using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Areas.Administrator.Binder
{
    public class HotDealBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProvider = bindingContext.ValueProvider;

            //DateTime HotDealActionEndTime = new DateTime();

            if(
               valueProvider.GetValue("HotDealActionEndTime.Year").ConvertTo(typeof(int)) != null &&
               valueProvider.GetValue("HotDealActionEndTime.Month").ConvertTo(typeof(int)) != null &&
               valueProvider.GetValue("HotDealActionEndTime.Day").ConvertTo(typeof(int)) != null &&
               valueProvider.GetValue("HotDealActionEndTime.Hour").ConvertTo(typeof(int)) != null &&
               valueProvider.GetValue("HotDealActionEndTime.Minute").ConvertTo(typeof(int)) != null &&

               valueProvider.GetValue("HotDealActionStartTime.Year").ConvertTo(typeof(int)) != null &&
               valueProvider.GetValue("HotDealActionStartTime.Month").ConvertTo(typeof(int)) != null &&
               valueProvider.GetValue("HotDealActionStartTime.Day").ConvertTo(typeof(int)) != null &&
               valueProvider.GetValue("HotDealActionStartTime.Hour").ConvertTo(typeof(int)) != null &&
               valueProvider.GetValue("HotDealActionStartTime.Minute").ConvertTo(typeof(int)) != null 
               //&&

               //valueProvider.GetValue("HotDealTime.Year").ConvertTo(typeof(int)) != null &&
               //valueProvider.GetValue("HotDealTime.Month").ConvertTo(typeof(int)) != null &&
               //valueProvider.GetValue("HotDealTime.Day").ConvertTo(typeof(int)) != null &&
               //valueProvider.GetValue("HotDealTime.Hour").ConvertTo(typeof(int)) != null &&
               //valueProvider.GetValue("HotDealTime.Minute").ConvertTo(typeof(int)) != null
               )
            {
                return new HotDeal {
                    HotDealTime = new DateTime(),
                    //year: (int)valueProvider.GetValue("HotDealTime.Year").ConvertTo(typeof(int)),
                    //month: (int)valueProvider.GetValue("HotDealTime.Month").ConvertTo(typeof(int)),
                    //day: (int)valueProvider.GetValue("HotDealTime.Day").ConvertTo(typeof(int)),
                    //hour: (int)valueProvider.GetValue("HotDealTime.Hour").ConvertTo(typeof(int)),
                    //minute: (int)valueProvider.GetValue("HotDealTime.Minute").ConvertTo(typeof(int)),
                    //second:0
                    //),
                HotDealActionStartTime = new DateTime(
                    year: (int)valueProvider.GetValue("HotDealActionStartTime.Year").ConvertTo(typeof(int)),
                    month: (int)valueProvider.GetValue("HotDealActionStartTime.Month").ConvertTo(typeof(int)),
                    day: (int)valueProvider.GetValue("HotDealActionStartTime.Day").ConvertTo(typeof(int)),
                    hour: (int)valueProvider.GetValue("HotDealActionStartTime.Hour").ConvertTo(typeof(int)),
                    minute: (int)valueProvider.GetValue("HotDealActionStartTime.Minute").ConvertTo(typeof(int)),
                    second: 0
                    ),
                    HotDealActionEndTime = new DateTime(
                    year: (int)valueProvider.GetValue("HotDealActionEndTime.Year").ConvertTo(typeof(int)),
                    month: (int)valueProvider.GetValue("HotDealActionEndTime.Month").ConvertTo(typeof(int)),
                    day: (int)valueProvider.GetValue("HotDealActionEndTime.Day").ConvertTo(typeof(int)),
                    hour: (int)valueProvider.GetValue("HotDealActionEndTime.Hour").ConvertTo(typeof(int)),
                    minute: (int)valueProvider.GetValue("HotDealActionEndTime.Minute").ConvertTo(typeof(int)),
                    second: 0
                    ),
                    HotDealToProducts = (string)valueProvider.GetValue("HotDealToProducts").ConvertTo(typeof(string)),
                    IsHotDeal = (bool)valueProvider.GetValue("IsHotDeal").ConvertTo(typeof(bool))
                };
            }
            else
            {
                return null;
            }
        }
    }
}