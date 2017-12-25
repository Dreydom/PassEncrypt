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
        private string drive;
        public Encrypt(string Drive)
        {
            this.drive = Drive;
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
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string key = "";
            var r = new Random();
            while (key.Length < x)
            {
                key += chars[r.Next(0, chars.Length)];
            }
            return key;
        }
        /// <summary>
        /// Зашифровывает и записывает новый пароль в нулевой сектор. Возвращает новый ключ шифрования.
        /// </summary>
        /// <param name="password">Пароль, который нужно зашифровать в файле</param>
        public string WritePass(string password)
        {
            string key = GenKey();
            string codpass = Code(password, key);
            byte[] ByteBuffer = new byte[512];//задаем размер буфера
            byte[] temp = new byte[8];
            FileReader fr = new FileReader();
            byte[] oldpass = new byte[8];
            for (int i = 0; i < codpass.Length; i++)
            {
                oldpass[i] = (byte)codpass[i];
            }
            if (fr.OpenWrite(drive))
            {

                for (int i = 54; i < 62; i++)
                {
                    ByteBuffer[i] = oldpass[i - 54];
                }
                int count = fr.Write(ByteBuffer, 0, 512);
                fr.Close();
            }
            return key;
        }
        /// <summary>
        /// Функция проверки пароля в файле. Возвращает true, если пароль совпал; false — если не совпал.
        /// </summary>
        /// <param name="password">Пароль, введенный пользователем</param>
        /// <param name="key">Ключ шифрования</param>
        public bool CheckPass(string password, string key)
        {
            string codpass = Code(password, key);//кодирование пароля
            byte[] ByteBuffer = new byte[512];//задаем размер буфера
            byte[] temp = new byte[8];
            bool flag = true;
            FileReader fr = new FileReader();
            if (fr.OpenRead(drive)) //вызов для чтения
            {
                int count = fr.Read(ByteBuffer, 512);

                for (int i = 54; i < 62; i++)
                {
                    temp[i - 54] = ByteBuffer[i];
                }
                fr.Close();

                byte[] oldpass = new byte[8];
                for (int i = 0; i < codpass.Length; i++)
                {
                    oldpass[i] = (byte)codpass[i];
                }

                for (int i = 0; i < 8; i++)
                {
                    if (temp[i] != oldpass[i])
                        flag = false;
                }
            }
            return flag;
        }
    }
}
