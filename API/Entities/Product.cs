

namespace API.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public string PictureUrl { get; set; }
        public string Type { get; set; }
        public string Genre { get; set; }
        public int QuantityInStock { get; set; }


    }
}


/*  1) Create a simple class that contains the properties related to the product
2) The first property we need is th Id. The get and set allows us to get and set the Id from other parts of our app from this Product
3)  We also need the other properties*/