using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ProductManager
    {
        private Database db;
        public ProductManager()
        {
            db = new Database();
        }

        public List<Product> GetAll()
        {
            List<Product> list = new List<Product>();
            db.Open();
            //  var reader = db.ExecuteReader("select * from [dbo].[Product]");
            var reader = db.ExecuteReader("sp_ProductGetAll");
            while (reader.Read())
            {
                Product tmp = new Product();
                tmp.ID = (int)reader[0];
                if (!reader.IsDBNull(1))
                {
                    tmp.ProductCode = (int)reader[1];
                }
                if (!reader.IsDBNull(2))
                {
                    tmp.ProductName = (string)reader[2];
                }
                if (!reader.IsDBNull(3))
                {
                    tmp.ProductDescription = (string)reader[3];
                }
                if (!reader.IsDBNull(4))
                {
                      tmp.ProductStartDate = (DateTime)reader[4];

                }
                if (!reader.IsDBNull(5))
                {
                    tmp.ProductImage = (string)reader[5];
                }
                if (!reader.IsDBNull(6))
                {
                    tmp.ProductImagePath = (string)reader[6];
                }
                //if (!reader.IsDBNull(7))
                //{
                //    tmp.ImageFile = (System.Web.HttpPostedFileWrapper)reader[7];
                //}

                list.Add(tmp);
            }
            reader.Close();
            db.Close();
           // list.ForEach(x => x.ProductStartDate.ToString("MM/dd/yyyy"));
            return list;
        }

        public void Delete(int Productid)
        {
            try
            {
                db.Open();
                //  string query = "Delete from [dbo].[Product] where ProductID=@id";
                var affected = db.ExecuteNonQuery("sp_ProductDelete", db.CreateParameter("@id", Productid.ToString()));
                if (affected == 0)
                {
                    throw new Exception("No rows affected");
                }
            }
            finally
            {
                db.Close();
            }

        }
       
        public void Update(Product item)
        {
            try
            {
                db.Open();
               // string query = "Update [dbo].[Product] set [ProductDescription]=@Description, where ProductID=@id";
                                                      
                var affected = db.ExecuteNonQuery("sp_ProductUpdate", db.CreateParameter("@id", item.ID.ToString())
                                                        ,db.CreateParameter("@ProductCode", item.ProductCode.ToString())
                                                        ,db.CreateParameter("@ProductName", item.ProductName)
                                                        ,db.CreateParameter("@ProductDecription", item.ProductDescription)
                                                        ,db.CreateParameter("@ProductStartDate", item.sProductStartDate)
                                                        ,db.CreateParameter("@ProductImagePath", item.ProductImagePath));
                if (affected == 0)
                {
                    throw new Exception("No rows affected");
                }
            }
            finally
            {
                db.Close();
            }

        }
        public void Add(Product item)
        {
            try
            {
                db.Open();
                // string query = "Update [dbo].[Product] set [ProductDescription]=@Description, where ProductID=@id";

                var affected = db.ExecuteNonQuery("sp_ProductAdd", db.CreateParameter("@ProductCode", item.ProductCode.ToString())
                                                                 , db.CreateParameter("@ProductName", item.ProductName)
                                                                 , db.CreateParameter("@ProductDecription", item.ProductDescription)
                                                                 , db.CreateParameter("@ProductStartDate", item.ProductStartDate.ToString("yyyy-MM-dd"))
                                                                 ,db.CreateParameter("@ProductImagePath", item.ProductImagePath));
                if (affected == 0)
                {
                    throw new Exception("No rows affected");
                }
            }
            finally
            {
                db.Close();
            }

        }

    }
}
