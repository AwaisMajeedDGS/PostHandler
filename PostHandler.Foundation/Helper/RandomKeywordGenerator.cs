﻿namespace PostHandler.Foundation.Helper
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public sealed class RandomKeywordGenerator 
    {

        #region Fields
        private static volatile RandomKeywordGenerator instance;
        private static object syncRoot = new Object();
        private const int DefaultMinimum = 12;
        private const int DefaultMaximum = 15;
        private const int UBoundDigit = 61;
        private RNGCryptoServiceProvider rng;
        private int minSize;
        private int maxSize;
        private bool hasRepeating;
        private bool hasConsecutive;
        private bool hasSymbols;
        private string exclusionSet;
        private char[] pwdCharArray = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789~!@#$%^&*()-_=".ToCharArray();
        #endregion

        private RandomKeywordGenerator()
        {
            this.Minimum = DefaultMinimum;
            this.Maximum = DefaultMaximum;
            this.ConsecutiveCharacters = false;
            this.RepeatCharacters = true;
            this.ExcludeSymbols = false;
            this.Exclusions = null;
            rng = new RNGCryptoServiceProvider();
        }

        private int GetCryptographicRandomNumber(int lBound, int uBound)
        {
            uint urndnum;
            byte[] rndnum = new Byte[4];
            if (lBound == uBound - 1) { return lBound; }
            uint xcludeRndBase = (uint.MaxValue - (uint.MaxValue % (uint)(uBound - lBound)));
            do
            {
                rng.GetBytes(rndnum);
                urndnum = System.BitConverter.ToUInt32(rndnum, 0);
            } while (urndnum >= xcludeRndBase);
            return (int)(urndnum % (uBound - lBound)) + lBound;
        }

        private char GetRandomCharacter()
        {
            int upperBound = pwdCharArray.GetUpperBound(0);
            if (true == this.ExcludeSymbols) { upperBound = RandomKeywordGenerator.UBoundDigit; }
            int randomCharPosition = GetCryptographicRandomNumber(pwdCharArray.GetLowerBound(0), upperBound);
            char randomChar = pwdCharArray[randomCharPosition];
            return randomChar;
        }

        public string Generate()
        {
            var pwdLength = GetCryptographicRandomNumber(this.Minimum, this.Maximum);
            var pwdBuffer = new StringBuilder();
            pwdBuffer.Capacity = this.Maximum;
            char lastCharacter, nextCharacter;
            lastCharacter = nextCharacter = '\n';
            for (int i = 0; i < pwdLength; i++)
            {
                nextCharacter = GetRandomCharacter();
                if (false == this.ConsecutiveCharacters)
                {
                    while (lastCharacter == nextCharacter)
                    {
                        nextCharacter = GetRandomCharacter();
                    }
                }
                if (false == this.RepeatCharacters)
                {
                    var temp = pwdBuffer.ToString();
                    var duplicateIndex = temp.IndexOf(nextCharacter);
                    while (-1 != duplicateIndex)
                    {
                        nextCharacter = GetRandomCharacter();
                        duplicateIndex = temp.IndexOf(nextCharacter);
                    }
                }
                if ((null != this.Exclusions))
                {
                    while (-1 != this.Exclusions.IndexOf(nextCharacter))
                    {
                        nextCharacter = GetRandomCharacter();
                    }
                }
                pwdBuffer.Append(nextCharacter);
                lastCharacter = nextCharacter;
            }
            if (null != pwdBuffer) { return pwdBuffer.ToString(); }
            else { return String.Empty; }
        }

        public static RandomKeywordGenerator Current
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null) { instance = new RandomKeywordGenerator(); }
                    }
                }
                return instance;
            }
        }

        private string Exclusions
        {
            get { return this.exclusionSet; }
            set { this.exclusionSet = value; }
        }

        private int Minimum
        {
            get { return this.minSize; }
            set
            {
                this.minSize = value;
                if (RandomKeywordGenerator.DefaultMinimum > this.minSize)
                {
                    this.minSize = RandomKeywordGenerator.DefaultMinimum;
                }
            }
        }

        private int Maximum
        {
            get { return this.maxSize; }
            set
            {
                this.maxSize = value;
                if (this.minSize >= this.maxSize)
                {
                    this.maxSize = RandomKeywordGenerator.DefaultMaximum;
                }
            }
        }

        private bool ExcludeSymbols
        {
            get { return this.hasSymbols; }
            set { this.hasSymbols = value; }
        }

        private bool RepeatCharacters
        {
            get { return this.hasRepeating; }
            set { this.hasRepeating = value; }
        }

        private bool ConsecutiveCharacters
        {
            get { return this.hasConsecutive; }
            set { this.hasConsecutive = value; }
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_%&$";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
