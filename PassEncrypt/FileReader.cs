using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassEncrypt
{
    class FileReader //класс для работы с носителями информации (жесткий диск - файл) работа с жестким диском - путем открытия файла.
    {
        const uint GENERIC_READ = 0x80000000; //для чтения
        const uint GENERIC_WRITE = 0x40000000; //для записи
        const uint OPEN_EXISTING = 3; //тип открытия файла
        const uint FILE_SHARE_READ = 0x00000001;
        const uint FILE_SHARE_WRITE = 0x00000002;
        System.IntPtr handle; //дескриптор окна
        [System.Runtime.InteropServices.DllImport("kernel32", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        //Kernel32.dll - динамически подключаемая библиотека, предоставляющая приложениям многие базовые функции
        //API Win32, в частности: управление памятью, операции ввода/вывода, создание процессов и потоков и функции синхронизации.
        //System.Runtime.InteropServices - пространство имен
        //DllImport - вызов неуправляемого кода из управляемого приложения
        //"kernel32" - библиотека
        //SetLastError - значение true, чтобы показать, что вызывающий объект вызовет SetLastError
        //ThrowOnUnmappableChar - исключение каждый раз Interop маршалер встречает unmappable характер
        //CharSet = System.Runtime.InteropServices.CharSet.Ansi - Указывает, какой набор знаков должны использовать маршалированные строки
        unsafe static extern System.IntPtr CreateFile
        (
        string FileName, // имя файла
        uint DesiredAccess, // режим доступа
        uint ShareMode, // режим 
        uint SecurityAttributes, // атрибуты безопасности
        uint CreationDisposition, // как создать
        uint FlagsAndAttributes, // атрибуты файла 
        int hTemplateFile // дескриптор файла шаблона
        );
        [System.Runtime.InteropServices.DllImport("kernel32", SetLastError = true)]
        unsafe static extern bool ReadFile
        (
        System.IntPtr hFile, // дескриптор файла
        void* pBuffer, // буфер данных
        int NumberOfBytesToRead, // количество байт для чтения
        int* pNumberOfBytesRead, // количество прочитанных байт 
        int Overlapped // перекрывающий буфер
        );
        [System.Runtime.InteropServices.DllImport("kernel32", SetLastError = true)]
        unsafe static extern bool WriteFile
        (
        System.IntPtr hFile, // дескриптор файла
        void* lpBuffer, // буфер данных 
        int NumberOfBytesToRead, // количество байт для чтения
        int* pNumberOfBytesRead, // количество прочитанных байт 
        int Overlapped // перекрывающийся буфер
        );
        [System.Runtime.InteropServices.DllImport("kernel32", SetLastError = true)]
        unsafe static extern bool CloseHandle
        (
        System.IntPtr hObject // обращение к объекту
        );
        public bool OpenRead(string FileName) //работа с нулевым сектором
        {
            // открытие существующего файла для чтения 
            handle = CreateFile(FileName, GENERIC_READ, FILE_SHARE_READ, 0, OPEN_EXISTING, 0, 0); //в дискрипторе окна вызывается ф-ия создания файла с параметрами чтения и открытия
                                                                                                  //параметры открытия
            if (handle != System.IntPtr.Zero) //если файл существует, озвращаем T, нет - F
                                              //IntPtr - нулевое поле
            {
                return true; //устройство найдено
            }
            else
            {
                return false; //устройство не надено
            }
        }
        public bool OpenWrite(string FileName)
        {
            // открытие существующего файла для записи 
            handle = CreateFile(FileName, GENERIC_WRITE, FILE_SHARE_WRITE, 0, OPEN_EXISTING, 0, 0); //параметры открытия
                                                                                                    //handle - дескриптор окна
            if (handle != System.IntPtr.Zero)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        unsafe public int Read(byte[] buffer, int count) //чтение ячеек flash
                                                         //сборщик мусора среды CLR может 
                                                         //Во избежание этого блок fixed используется для получения указателя на память и его пометки таким образом,
                                                         //что его не смог переместить сборщик мусора. В конце блока fixed память снова становится доступной для перемещения путем сборки мусора.
                                                         //Эта способность называется декларативным закреплением.
        {
            int n = 0;
            fixed (byte* p = buffer)
            {
                if (!ReadFile(handle, p, count, &n, 0)) //параметры чтения
                {
                    return 0;
                }
            }
            return n;
        }
        unsafe public int Write(byte[] buffer, int index, int count) //запись ячеек flash
        {
            int n = 0;
            fixed (byte* p = buffer) //Оператор fixed задает указатель на управляемую переменную
                                     //и "закрепляет" эту переменную во время выполнения оператора.
            {
                if (!WriteFile(handle, p + index, 512, &n, 0)) //параметры записи
                {
                    return 0;
                }
            }
            return n;
        }
        public bool Close()
        //bool - используется для объявления переменных для хранения логических значений, true и false.
        {
            return CloseHandle(handle); //закрыть дискриптор откна
        }
    }
}
