using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Entities
{
    public class Product
    {
        public int ID { get; set; }
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public DateTime ProductStartDate { get; set; }

        //public string ProductImage { 
        //    get {
        //        return $"{this.ProductCode}.jpg";
        //    } 
        //}
        public string ProductImage { get; set; }
        public string ProductImagePath { get; set; }
       // public HttpContextBase ImageFile { get; set; }

        public string sProductStartDate
        {
            get
            {
                return this.ProductStartDate.ToString("MM/dd/yyyy");
            }
            set
            {
                this.ProductStartDate= DateTime.Parse(value);
            }

        }
    }
}
