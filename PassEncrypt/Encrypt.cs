using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PassEncrypt
{
    class Encrypt
    {
        private string path;
        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
            }
        }
        private string Code(string input, string key)
        {
            string output = "";
            for (int i = 0, j = 0; i < input.Length; i++, j++)
            {
                if (j == key.Length) j = 0;
                output += (char)(input[i] ^ key[j]);
            }
            return output;
        }
        private string GenKey(int x = 4)
        {
            string key = "";
            var r = new Random();
            while (key.Length < x)
            {
                Char c = (char)r.Next(33, 125);
                if (Char.IsLetterOrDigit(c))
                    key += c;
            }
            return key;
        }
        public string WritePass(string password)
        {
            string key = GenKey();
            File.WriteAllText(path, Code(password, key));
            return key;
        }
        public bool CheckPass(string password, string key)
        {
            if (String.Equals(Code(password, key), File.ReadAllText(path)))
                return true;
            else
                return false;
        }
    }
}
