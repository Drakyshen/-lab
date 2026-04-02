using System;
using System.Collections.Generic;

namespace Lab16_v21
{
    public class HexNumber
    {
        private string hexData;

        public string Value
        {
            get { return hexData; }
            set { if (CheckHex(value)) hexData = value.ToUpper(); }
        }

        public HexNumber()
        {
            hexData = "0";
        }

        public HexNumber(string val)
        {
            if (CheckHex(val)) hexData = val.ToUpper();
            else hexData = "0";
        }

        public HexNumber(HexNumber other)
        {
            this.hexData = other.hexData;
        }

        ~HexNumber()
        {
            hexData = null;
        }

      
        public bool CheckHex(string val)
        {
            if (string.IsNullOrEmpty(val)) return false;
            string allowed = "0123456789ABCDEF";
            foreach (char c in val.ToUpper())
            {
                if (!allowed.Contains(c.ToString())) return false;
            }
            return true;
        }

        public long ToDecimal()
        {
            return Convert.ToInt64(hexData, 16);
        }

        public HexNumber Add(HexNumber other)
        {
            long res = this.ToDecimal() + other.ToDecimal();
            return new HexNumber(Convert.ToString(res, 16));
        }

        public HexNumber Sub(HexNumber other)
        {
            long res = this.ToDecimal() - other.ToDecimal();
            if (res < 0) res = 0; 
            return new HexNumber(Convert.ToString(res, 16));
        }

        public HexNumber Mult(HexNumber other)
        {
            long res = this.ToDecimal() * other.ToDecimal();
            return new HexNumber(Convert.ToString(res, 16));
        }

        public HexNumber Div(HexNumber other)
        {
            long d = other.ToDecimal();
            if (d == 0) return new HexNumber("0");
            return new HexNumber(Convert.ToString(this.ToDecimal() / d, 16));
        }

        public bool IsGreater(HexNumber other) { return this.ToDecimal() > other.ToDecimal(); }
        public bool IsEqual(HexNumber other) { return this.ToDecimal() == other.ToDecimal(); }
    }
}