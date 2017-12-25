#ifndef ENCRYPT_H
#define ENCRYPT_H
#pragma once
#include <random>
#include <IO.h>

namespace PassEncryptPlus {
	using namespace System;
	using namespace System::IO;
	public class Encrypt
	{
	public:
		Encrypt();
		~Encrypt();
		const char *Path = "pass.txt";

	private:
		String^ Code(String^ input, String^ key)
		{
			String^ output = "";
			for (int i = 0, j = 0; i < input->Length; i++, j++)
			{
				if (j == key->Length) j = 0;
				output += (char)(input[i] ^ key[j]);
			}
			return output;
		}
		String^ GenKey(int x = 4)
		{
			String^ chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
			String^ key = "";
			while (key->Length < x)
			{
				key += chars[rand() % chars->Length + 0];
			}
			return key;
		}
	public:
		String^ WritePass(String^ password)
		{
			String^ key = GenKey();

			File::WriteAllText("pass.txt", Code(password, key));
			return key;
		}
		bool CheckPass(String^ password, String^ key)
		{
			return (String::Equals(Code(password, key), File::ReadAllText("pass.txt")));
		}
	};
}
#endif