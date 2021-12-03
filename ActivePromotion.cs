namespace PromotionEngineStrategy
{
    public class ActivePromotion
    {
        public static void Main()
        {
            //Call the required Strategy to activate promotion
        }
    }

    ///<summary>
    /// Product Items and its unit price
    ///</summary>
    public class ProductInformation
    {
        //Assign SKU id and its price
        protected Dictionary<char, double> _productItem = new Dictionary<char, double>();

        //List as price and send to strategy (in ascending order)
        public List<double> priceInOrder = new List<double>();

        //constructor having default values of SKU and its unit prices
        public ProductInformation()
        {
            _productItem.Add('A', 50.0);
            _productItem.Add('B', 30.0);
            _productItem.Add('C', 20.0);
            _productItem.Add('D', 15.0);

            //List
            priceInOrder.Add(50.0);
            priceInOrder.Add(30.0);
            priceInOrder.Add(20.0);
            priceInOrder.Add(15.0);
        }

        //Get sku price for specific id 
        public  double GetSKUidPrice(char id)
        {
            double value =0.0;
            if (_productItem.ContainsKey(id))
                 value = _productItem[id];
            return _productItem[id];
        }

        //Set price to SKU id
        public void SetSKUidPrice(char id, double price)
        {
            if (_productItem.ContainsKey(id))
            {
                _productItem[id] = price;
            }
            else
                _productItem.Add(id, price);

        }

    }

    ///<summary>
    ///Customer order SKU id and its count
    ///</summary>
    public class CustomerOrder : ProductInformation
    {
        //SKU id and SKU count customer ordered
        public Dictionary<char, int> listItems = new Dictionary<char, int>();

       
        //Get order from user
        public void GetOrderFromUser(char id, int count)
        {            
            //if (id != ' ')
            if(char.IsLetter(id))
            {
                listItems.Add(id, count);
                //ordercount--;
            }
            else
                Console.WriteLine(" Please enter Alphabets only!");

        }

        //Display the customer order
        public void DisplayOrderCount()
        {
            foreach (var listproduct in listItems)
                Console.WriteLine("Product: {0}, Unit Price: {1} DKK", listproduct.Key, listproduct.Value);
        }

        //Normal Price
        public double PriceCalculator()
        {
            //Get the dictionary value to list for easy calculation
            var orderedCount = listItems.Values.ToList();
            int i = 0;
            double price = 0.0;
            foreach (var item in _productItem)
            {
                price = price + item.Value * orderedCount[i];
                i++;
            }
            return price;

        }
    }//customerOrder


    //Main
    public class OrderPrice
    {
        public static void Main()
        {
            //Fill the SKU ids and their unit prices
            CustomerOrder customerList = new CustomerOrder();

            //Get the order from user
            customerList.GetOrderFromUser('A', 3);
            customerList.GetOrderFromUser('B', 2);
            customerList.GetOrderFromUser('C', 1);
            customerList.GetOrderFromUser('D', 1);

            //Display the list from customer
            customerList.DisplayOrderCount();

            Dictionary<char, int> OriginalCustomerOrder = new Dictionary<char, int>();
            OriginalCustomerOrder = customerList.listItems;

            //create a context
            Context context = new Context();

            //=======================================ACTIVE PROMOTIONS===================================//
            //Apply Fixed price combination for A and B
            Console.WriteLine("Activating fixed price Promotion");
            context.SetPromotionStrategy(new FixedPromotionTypeAB());
            customerList.listItems =context.ApplyPromotion(customerList.listItems, customerList.priceInOrder);

            //Apply Combo price combination for C and D
            Console.WriteLine("Activating combo price Promotion");
            context.SetPromotionStrategy(new ComboPromotionTypeCD());
            customerList.listItems = context.ApplyPromotion(customerList.listItems, customerList.priceInOrder);

            ////Calculate price for missed out products
            //Console.WriteLine("Activating unit price Promotion");
            //context.SetPromotionStrategy(new PercentagePromotionType());
            //customerList.listItems = context.ApplyPromotion(customerList.listItems, customerList.priceInOrder);

            Console.WriteLine("======== Happy Shopping ! ============");

        }
    }

}//end namespace
