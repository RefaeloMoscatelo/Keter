using BL;
using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace REST.Controllers
{
    public class ProductController : ApiController
    {
        List<Product> list = new List<Product>();
      
        [HttpGet]
        //[Route("api/Get")]
        public List<Product> Get()
        {
            ProductManager manager = new ProductManager();
            list = manager.GetAll();
            // return list;
            var q =  list.Where(x => x.ProductImagePath != null).ToList();
            return q;
        }
        [HttpGet]
        public Product Get(int id)
        {
            return list.FirstOrDefault(x => x.ID == id);
        }
        [HttpDelete]
        public void Delete(int id)
        {
            ProductManager manager = new ProductManager();
            manager.Delete(id);
        }
       

        [HttpPut]
        public void Update(int id,Product p)
        {
          //  var file = p.ImageFile;
            //byte[] imagebyte = null;
            // file.SaveAs(HttpContext.Current.Server.MapPath("/UploadImage/" + file.FileName));
            //  BinaryReader reader = new BinaryReader(file.InputStream);
            //  imagebyte = reader.ReadBytes(file.ContentLength);

            //< img src = "../Images/bamba.jpg" />
            //    .. / Images / bamba.jpg
            string path = "";
            string[] str;

            if (!p.ProductImagePath.Contains("jpg"))
            {
                //str = p.ProductImage.Split('\\');
                p.ProductImagePath = p.ProductImage;
            }
            else
            {
                str = p.ProductImagePath.Split('\\');
            }
           
            
            //if (!str[0].StartsWith("../Images"))
            //{
            //    path = "../Images/" + str[2];
            //    p.ProductImagePath = path;
            //}

            ProductManager manager = new ProductManager();
            p.ID = id;
          //  p.ProductImage = image;
          //  var productFromserver = manager.get
            manager.Update(p);
        }
        [HttpPost]
        public Product Insert([FromBody] Product p)
        {
            if (!p.ProductImagePath.Contains("Images"))
            {
                string path = "../Images/" + p.ProductImagePath;
                p.ProductImagePath = path;
            }
            
            ProductManager manager = new ProductManager();
            manager.Add(p);
           
            return p;
        }
      


    }

        


}

    
