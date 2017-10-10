using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PassEncrypt
{
    /// <summary>
    /// Класс для генерации ключа,  шифрования пароля, записи его в файл и проверки на правильный ввод пароля пользователем.
    /// </summary>
    class Encrypt
    {
        private string path;
        /// <summary>
        /// Путь к файлу с зашифрованным паролем.
        /// </summary>
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
        /// <summary>
        /// Шифрует пароль. Возвращает зашифрованную строку.
        /// </summary>
        /// <param name="input">Пароль, введенный пользователем</param>
        /// <param name="key">Ключ шифрования</param>
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
        /// <summary>
        /// Генерирует новый ключ и возвращает его.
        /// </summary>
        /// <param name="x">Длина ключа (по умолчанию равна 4, необязательный параметр).</param>
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
        /// <summary>
        /// Зашифровывает и записывает новый пароль в файл. Возвращает новый ключ шифрования.
        /// </summary>
        /// <param name="password">Пароль, который нужно зашифровать в файле</param>
        public string WritePass(string password)
        {
            string key = GenKey();
            File.WriteAllText(path, Code(password, key));
            return key;
        }
        /// <summary>
        /// Функция проверки пароля в файле. Возвращает true, если пароль совпал; false — если не совпал.
        /// </summary>
        /// <param name="password">Пароль, введенный пользователем</param>
        /// <param name="key">Ключ шифрования</param>
        public bool CheckPass(string password, string key)
        {
            if (String.Equals(Code(password, key), File.ReadAllText(path))) //тернарные операторы — зло
                return true;
            else
                return false;
        }
    }
}
