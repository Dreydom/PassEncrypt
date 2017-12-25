#pragma once
#include "Encrypt.h"
//#pragma once
//#include "AccessGranted.h"

namespace PassEncryptPlus {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Сводка для NewPassInput
	/// </summary>

	//ref class AccessGranted;


	public ref class NewPassInput : public System::Windows::Forms::Form
	{
	private: String^ key;
	public:
		NewPassInput(String^ key)
		{
			InitializeComponent();
			this->key = key;

		}
	private: Encrypt *encrypt = new Encrypt();
	protected:
		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		~NewPassInput()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Button^  button1;
	protected:
	private: System::Windows::Forms::Label^  label3;
	private: System::Windows::Forms::TextBox^  textBox3;
	private: System::Windows::Forms::Label^  label2;
	private: System::Windows::Forms::TextBox^  textBox2;
	private: System::Windows::Forms::Label^  label1;
	private: System::Windows::Forms::TextBox^  textBox1;
	private: System::Windows::Forms::Label^  label4;

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
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->textBox3 = (gcnew System::Windows::Forms::TextBox());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->textBox2 = (gcnew System::Windows::Forms::TextBox());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->textBox1 = (gcnew System::Windows::Forms::TextBox());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->SuspendLayout();
			this->AcceptButton = button1;
			// 
			// button1
			// 
			this->button1->FlatStyle = System::Windows::Forms::FlatStyle::Popup;
			this->button1->Location = System::Drawing::Point(79, 100);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(120, 25);
			this->button1->TabIndex = 15;
			this->button1->Text = L"Отправить";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &NewPassInput::button1_Click);
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(12, 64);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(90, 13);
			this->label3->TabIndex = 14;
			this->label3->Text = L"Подтверждение";
			// 
			// textBox3
			// 
			this->textBox3->Location = System::Drawing::Point(107, 61);
			this->textBox3->MaxLength = 14;
			this->textBox3->Name = L"textBox3";
			this->textBox3->Size = System::Drawing::Size(165, 21);
			this->textBox3->TabIndex = 13;
			this->textBox3->UseSystemPasswordChar = true;
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(12, 38);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(79, 13);
			this->label2->TabIndex = 12;
			this->label2->Text = L"Новый пароль";
			// 
			// textBox2
			// 
			this->textBox2->Location = System::Drawing::Point(107, 35);
			this->textBox2->MaxLength = 14;
			this->textBox2->Name = L"textBox2";
			this->textBox2->Size = System::Drawing::Size(165, 21);
			this->textBox2->TabIndex = 11;
			this->textBox2->UseSystemPasswordChar = true;
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(12, 12);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(85, 13);
			this->label1->TabIndex = 10;
			this->label1->Text = L"Старый пароль";
			// 
			// textBox1
			// 
			this->textBox1->Location = System::Drawing::Point(107, 9);
			this->textBox1->MaxLength = 14;
			this->textBox1->Name = L"textBox1";
			this->textBox1->Size = System::Drawing::Size(165, 21);
			this->textBox1->TabIndex = 9;
			this->textBox1->UseSystemPasswordChar = true;
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->ForeColor = System::Drawing::Color::Red;
			this->label4->Location = System::Drawing::Point(11, 84);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(0, 13);
			this->label4->TabIndex = 16;
			// 
			// NewPassInput
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(284, 133);
			this->Controls->Add(this->label4);
			this->Controls->Add(this->button1);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->textBox3);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->textBox2);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->textBox1);
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedToolWindow;
			this->Name = L"NewPassInput";
			this->Text = L"Введите новый пароль";
			this->ResumeLayout(false);
			this->PerformLayout();

		}
		public: static String^ keynew;
	private: System::Void button1_Click(System::Object^  sender, System::EventArgs^  e) {
		String^ oldpass = textBox1->Text;
		String^ newpass1 = textBox2->Text;
		String^ newpass2 = textBox3->Text;
		if (String::IsNullOrEmpty(oldpass)
			|| String::IsNullOrEmpty(newpass1)
			|| String::IsNullOrEmpty(newpass2))
		{
			label4->Text = "Заполните поля";
		}
		else if (!encrypt->CheckPass(oldpass, key))
		{
			label4->Text = "Несовпадение старого пароля";

		}
		else if (newpass1 != newpass2)
		{
			label4->Text = "Введенные пароли не совпадают";
		}
		else
		{
			//AccessGranted^ accessGranted = gcnew AccessGranted(str);
		//	accessGranted->Show();
		//	Hide();
			//((AccessGranted ^)(this->Parent))->keynew = encrypt->WritePass(newpass1);
			keynew= encrypt->WritePass(newpass1);
			Hide();
		}
	}
};
}
#pragma endregion


