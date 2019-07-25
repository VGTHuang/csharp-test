using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSandbox
{
    class BigNumber
    {
        public static void Test()
        {
        }


        private string num;
        private bool isPositive = true;
        public string Show { get{ return (this.isPositive?"" : "-") + this.num; } }
        public BigNumber()
        {
            this.num = "0";
        }
        public BigNumber(string num, bool isPositive)
        {
            this.num = num;
            this.isPositive = isPositive;
        }
        public BigNumber(int n)
        {
            this.num = Convert.ToString(n).TrimStart('-');
            if(n < 0)
            {
                this.isPositive = false;
            }
        }
        public BigNumber(short n)
        {
            this.num = Convert.ToString(n).TrimStart('-'); ;
            if (n < 0)
            {
                this.isPositive = false;
            }
        }
        public BigNumber(long n)
        {
            this.num = Convert.ToString(n).TrimStart('-'); ;
            if (n < 0)
            {
                this.isPositive = false;
            }
        }
        public BigNumber(double n)
        {
            this.num = Convert.ToString(n).TrimStart('-'); ;
            if (n < 0)
            {
                this.isPositive = false;
            }
        }
        public BigNumber(float n)
        {
            this.num = Convert.ToString(n).TrimStart('-'); ;
            if (n < 0)
            {
                this.isPositive = false;
            }
        }
        public BigNumber(Byte n)
        {
            this.num = Convert.ToString(n).TrimStart('-'); ;
            if (n < 0)
            {
                this.isPositive = false;
            }
        }

        /// <summary>
        /// returns -1 if a is smaller than b, 0 if a == b; 1
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int Compare(BigNumber a, BigNumber b)
        {
            if (a.isPositive && !b.isPositive)
            {
                if (a.num.Equals("0"))
                {
                    if (b.num.Equals("0"))
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    return 1;
                }
            }
            else if (!a.isPositive && b.isPositive)
            {
                if (b.num.Equals("0"))
                {
                    if (a.num.Equals("0"))
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                int aLen = a.num.Length, bLen = b.num.Length;
                if (aLen > bLen)
                {
                    if (a.isPositive)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else if(aLen < bLen)
                {
                    if (a.isPositive)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    for(int i = 0; i < aLen; i++)
                    {
                        if(a.num[i] > b.num[i])
                        {
                            if (a.isPositive)
                            {
                                return 1;
                            }
                            else
                            {
                                return -1;
                            }
                        }
                        else if (a.num[i] < b.num[i])
                        {
                            if (a.isPositive)
                            {
                                return -1;
                            }
                            else
                            {
                                return 1;
                            }
                        }
                    }
                    return 0;
                }
            }
        }

        public BigNumber Add(BigNumber other)
        {
            BigNumber sum = new BigNumber();



            return sum;
        }

        private static string addTwoStrings(string a, string b)
        {
            int aLen = a.Length;
            int bLen = b.Length;
            int parsum = 0;
            string sum = "";
            int carry = 0;
            int i;
            for (i = 0; i < aLen && i < bLen; i++)
            {
                parsum = (a[aLen - 1 - i] - 48) + (b[bLen - 1 - i] - 48) + carry;
                carry = parsum / 10;
                parsum %= 10;
                sum = (char)(parsum + 48) + sum;
            }
            if(i >= aLen && i >= bLen)
            {
                if(carry == 1)
                {
                    sum = "1" + sum;
                }
                return sum;
            }
            else if (i >= aLen)
            {
                if (carry == 1)
                {
                    string hi = addTwoStrings("1", b.Substring(0, bLen - i));
                    return hi + sum;
                }
                else
                {
                    return b.Substring(0, bLen - i) + sum;
                }
            }
            else
            {
                if (carry == 1)
                {
                    string hi = addTwoStrings("1", a.Substring(0, aLen - i));
                    return hi + sum;
                }
                else
                {
                    return a.Substring(0, aLen - i) + sum;
                }
            }
        }
        
    }
}
