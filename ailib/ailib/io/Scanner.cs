using System.Globalization;
using System.Numerics;
using System.Text;

namespace ailib.io
{
    public class Scanner : System.IO.StringReader
    {
        string currentWord;
        CultureInfo en = new CultureInfo("en-US");

        public Scanner(string s) : base(s)
        {
            readNextWord();
        }

        private void readNextWord()
        {
            StringBuilder sb = new StringBuilder();
            int next = this.Read();
            char nextChar = (next < 0)? ' ' : (char)next;
            while (!char.IsWhiteSpace(nextChar))
            {
                sb.Append(nextChar);
                next = this.Read();
                nextChar = (next < 0) ? ' ' : (char)next;
            }
            while (this.Peek() > 0 && char.IsWhiteSpace((char)this.Peek()))
                this.Read();
            currentWord = (sb.Length > 0) ? currentWord = sb.ToString() : null;
        }

        public bool hasNextInt()
        {
            if (currentWord == null)
                return false;
            int dummy;
            return int.TryParse(currentWord, out dummy);
        }

        public int nextInt()
        {
            try
            {
                return int.Parse(currentWord);
            }
            finally
            {
                readNextWord();
            }
        }

        public bool hasNextDouble()
        {
            if (currentWord == null)
                return false;
            double dummy;
            return double.TryParse(currentWord, NumberStyles.Currency, en, out dummy);
        }

        public double nextDouble()
        {
            try
            {
                return double.Parse(currentWord, en);
            }
            finally
            {
                readNextWord();
            }
        }

        public bool hasNext()
        {
            return currentWord != null;
        }

        public BigInteger nextBigInteger()
        {
            try
            {
                return BigInteger.Parse(currentWord);
            }
            finally
            {
                readNextWord();
            }
        }
    }
}
