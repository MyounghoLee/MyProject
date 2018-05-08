using System;

namespace models.Product
{
    public class ProductInfo
    {
        private string _name;
        private int _qty;
        private long _price;

        public ProductInfo()
        {
            _name = string.Empty;
            _qty = 0;
            _price = 0;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public int Qty
        {
            get
            {
                return _qty;
            }
            set
            {
                _qty = value;
            }
        }

        public long Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
            }
        }
    }
}
