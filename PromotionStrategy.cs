using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineStrategy
{
    public abstract class PromotionStrategy
    {
        public abstract Dictionary<char, int> DiscountCalculation(Dictionary<char, int> o, List<double> p);
        protected static double _netPrice = 0.0;
    }

    //===============
    //Context
    //===============

    /// <summary>
    /// Context to choose the strategy to use
    /// </summary>
    public class Context
    {
        private PromotionStrategy _promotionStrategy;


        //User has to access context class to choose the appropriate promotion class
        //public Context() { }

        //Set the promotion strategy at run time
        public void SetPromotionStrategy(PromotionStrategy ps)
        {
            _promotionStrategy = ps;
        }

        //Get user input and call apply it to 
        public Dictionary<char, int> ApplyPromotion(Dictionary<char, int> o, List<double> unitPrice)
        {
            return _promotionStrategy.DiscountCalculation(o, p);
        }
    }

    //===============
    //PromotionTypes
    //===============
    public class FixedPromotionType : PromotionStrategy
    {
        public override Dictionary<char, int> DiscountCalculation(Dictionary<char, int> count, List<double> unitPrice)
        {
            while (count['A'] >= 3)
            {
                _netPrice = _netPrice + 130;
                Console.WriteLine("A >= 3, so add 130 ");
                count['A'] = count['A'] - 3;
            }

            while (count['B'] >= 2)
            {
                _netPrice = _netPrice + 45;
                Console.WriteLine("B >= 2, so add 45 ");
                count['B'] = count['B'] - 2;
            }
            Console.WriteLine("After FixedPromotion NetPrice= " + _netPrice);

            return count;
        }
    }

    public class ComboPromotionType : PromotionStrategy
    {
        public override Dictionary<char, int> DiscountCalculation(Dictionary<char, int> count, List<double> unitPrice)
        {
            while (count['C']>=1 && count['D'] >= 1)
            {
                _netPrice = _netPrice + 30;
                --count['C'];
                --count['D'];
            }
            Console.WriteLine("After ComboPromotion NetPrice= " + _netPrice);
            return count;
        }
    }

    public class PercentagePromotionType : PromotionStrategy
    {
        public override Dictionary<char, int> DiscountCalculation(Dictionary<char, int> count, List<double> unitPrice)
        {
            double percent = 1 - (50 / 100.0);
            int i = 0;
            double price = 0.0;
            foreach (var item in count)
            {
                price = price + item.Value * unitPrice[i] * percent;
                i++;
            }
            Console.WriteLine("After NoPromotion NetPrice= " + price);

            return count;
        }
    }


    public class NoPromotionType : PromotionStrategy
    {
        public override Dictionary<char, int> DiscountCalculation(Dictionary<char, int> count, List<double> unitPrice)
        {
            int i = 0;
            double price = 0.0;
            foreach (var item in count)
            {
                price = price + item.Value * unitPrice[i];
                i++;
            }
            Console.WriteLine("After NoPromotion NetPrice= " + price);
            return count;
        }
    }

}