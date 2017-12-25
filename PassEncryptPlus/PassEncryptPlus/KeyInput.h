#ifndef KEYINPUT_H
#define KEYINPUT_H
#pragma once
#include "AccessDenied.h"
#include "AccessGranted.h"

namespace PassEncryptPlus {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Сводка для KeyInput
	/// </summary>
	public ref class KeyInput : public System::Windows::Forms::Form
	{
	private:

		String^ pass;
		int errorCounter;
	public:
		KeyInput(String^ pass)
		{
			InitializeComponent();
			this->pass = pass;
			errorCounter = 0;
		}
	private:
		Encrypt *encrypt = new Encrypt();
	protected:
		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		~KeyInput()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Label^  label2;
	protected:
	private: System::Windows::Forms::TextBox^  textBox1;
	private: System::Windows::Forms::Label^  label1;
	private: System::Windows::Forms::Button^  button1;

	private:
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		void InitializeComponent(void)
		{
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->textBox1 = (gcnew System::Windows::Forms::TextBox());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->SuspendLayout();
			this->AcceptButton = button1;
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->ForeColor = System::Drawing::Color::Red;
			this->label2->Location = System::Drawing::Point(23, 34);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(0, 13);
			this->label2->TabIndex = 7;
			// 
			// textBox1
			// 
			this->textBox1->Location = System::Drawing::Point(106, 12);
			this->textBox1->Name = L"textBox1";
			this->textBox1->Size = System::Drawing::Size(145, 21);
			this->textBox1->TabIndex = 6;
			this->textBox1->UseSystemPasswordChar = true;
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(11, 15);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(80, 13);
			this->label1->TabIndex = 5;
			this->label1->Text = L"Введите ключ";
			// 
			// button1
			// 
			this->button1->FlatStyle = System::Windows::Forms::FlatStyle::Popup;
			this->button1->Location = System::Drawing::Point(77, 55);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(120, 25);
			this->button1->TabIndex = 4;
			this->button1->Text = L"Отправить";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &KeyInput::button1_Click);
			// 
			// KeyInput
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(284, 94);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->textBox1);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->button1);
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedToolWindow;
			this->Name = L"KeyInput";
			this->Text = L"Ввод ключа";
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void button1_Click(System::Object^  sender, System::EventArgs^  e) {
		String^ key = textBox1->Text;
		if (String::IsNullOrEmpty(key))
		{
			label2->Text = "Заполните поле";
		}
		else if (encrypt->CheckPass(pass, key))
		{
			errorCounter = 0;
			String^ str = encrypt->WritePass(pass);
			AccessGranted^ accessGranted = gcnew AccessGranted(str);
			Form^ formPassInput = Application::OpenForms[0];
			formPassInput->Hide(); //Прячем первую форму
			Hide(); //Прячем вторую форму
			accessGranted->Show();
		}
		else
		{
			errorCounter++;
			label2->Text = "Неправильный ввод. Попыток осталось —  " + (3 - errorCounter);
			if (errorCounter == 3)
			{
				AccessDenied^ accessDenied = gcnew AccessDenied();
				Form^ formPassInput = Application::OpenForms[0];
				formPassInput->Hide(); //Прячем первую форму
				Hide(); //Прячем вторую форму
				accessDenied->Show();
			}
		}
	}
};
}
#endif