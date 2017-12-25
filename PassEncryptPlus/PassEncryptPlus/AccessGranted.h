#pragma once
#include "NewPassInput.h"

namespace PassEncryptPlus {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Сводка для AccessGranted
	/// </summary>
	public ref class AccessGranted : public System::Windows::Forms::Form
	{
	private: String^ key;
	public: static String^ keynew;
	public:
		AccessGranted(String^ key)
		{
			InitializeComponent();
			this->key = key;
			richTextBox1->Text = key;
		}

	protected:
		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		~AccessGranted()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::LinkLabel^  linkLabel1;
	protected:
	private: System::Windows::Forms::RichTextBox^  richTextBox1;
	private: System::Windows::Forms::Label^  label1;

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
			this->linkLabel1 = (gcnew System::Windows::Forms::LinkLabel());
			this->richTextBox1 = (gcnew System::Windows::Forms::RichTextBox());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->SuspendLayout();
			// 
			// linkLabel1
			// 
			this->linkLabel1->AutoSize = true;
			this->linkLabel1->LinkColor = System::Drawing::SystemColors::ControlText;
			this->linkLabel1->Location = System::Drawing::Point(79, 78);
			this->linkLabel1->Name = L"linkLabel1";
			this->linkLabel1->Size = System::Drawing::Size(116, 13);
			this->linkLabel1->TabIndex = 5;
			this->linkLabel1->TabStop = true;
			this->linkLabel1->Text = L"ввести новый пароль";
			this->linkLabel1->LinkClicked += gcnew System::Windows::Forms::LinkLabelLinkClickedEventHandler(this, &AccessGranted::linkLabel1_LinkClicked);
			// 
			// richTextBox1
			// 
			this->richTextBox1->AutoWordSelection = true;
			this->richTextBox1->BackColor = System::Drawing::SystemColors::Control;
			this->richTextBox1->BorderStyle = System::Windows::Forms::BorderStyle::None;
			this->richTextBox1->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 25, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->richTextBox1->Location = System::Drawing::Point(93, 29);
			this->richTextBox1->Multiline = false;
			this->richTextBox1->Name = L"richTextBox1";
			this->richTextBox1->ReadOnly = true;
			this->richTextBox1->Size = System::Drawing::Size(88, 40);
			this->richTextBox1->TabIndex = 4;
			this->richTextBox1->Text = L"";
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 10, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label1->Location = System::Drawing::Point(78, 9);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(118, 17);
			this->label1->TabIndex = 3;
			this->label1->Text = L"Ваш новый ключ";
			// 
			// AccessGranted
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(284, 108);
			this->Controls->Add(this->linkLabel1);
			this->Controls->Add(this->richTextBox1);
			this->Controls->Add(this->label1);
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedToolWindow;
			this->Name = L"AccessGranted";
			this->Text = L"Доступ разрешен!";
			this->ResumeLayout(false);
			this->PerformLayout();

		}


	private: System::Void linkLabel1_LinkClicked(System::Object^  sender, System::Windows::Forms::LinkLabelLinkClickedEventArgs^  e) {
		String^ str = key;
		NewPassInput^ newPassInput = gcnew NewPassInput(str);
		
		Hide();
		newPassInput->ShowDialog();
		String^ keynew = NewPassInput::keynew;
		richTextBox1->Text = key = keynew;
		Show();
	}
	};
}
#pragma endregion


