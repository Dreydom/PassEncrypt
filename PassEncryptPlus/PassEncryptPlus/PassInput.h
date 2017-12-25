#pragma once
#include "Encrypt.h"
#include "AccessGranted.h"
#include "KeyInput.h"

namespace PassEncryptPlus {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// —водка дл€ PassInput
	/// </summary>
	public ref class PassInput : public System::Windows::Forms::Form
	{
	public:
		PassInput(void)
		{
			InitializeComponent();
			//
			//TODO: добавьте код конструктора
			//
		}
	private: Encrypt *encrypt = new Encrypt();
	private: System::Windows::Forms::Label^  label2;
	protected:
		/// <summary>
		/// ќсвободить все используемые ресурсы.
		/// </summary>
		~PassInput()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Label^  label1;
	protected:
	private: System::Windows::Forms::TextBox^  textBox1;
	private: System::Windows::Forms::Button^  button1;

	private:
		/// <summary>
		/// ќб€зательна€ переменна€ конструктора.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// “ребуемый метод дл€ поддержки конструктора Ч не измен€йте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		void InitializeComponent(void)
		{
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->textBox1 = (gcnew System::Windows::Forms::TextBox());
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->SuspendLayout();
			this->AcceptButton = button1;
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(12, 33);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(89, 13);
			this->label1->TabIndex = 0;
			this->label1->Text = L"¬ведите пароль";
			// 
			// textBox1
			// 
			this->textBox1->Location = System::Drawing::Point(107, 30);
			this->textBox1->MaxLength = 14;
			this->textBox1->Name = L"textBox1";
			this->textBox1->Size = System::Drawing::Size(165, 21);
			this->textBox1->TabIndex = 1;
			this->textBox1->UseSystemPasswordChar = true;
			// 
			// button1
			// 
			this->button1->FlatStyle = System::Windows::Forms::FlatStyle::Popup;
			this->button1->Location = System::Drawing::Point(78, 74);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(120, 25);
			this->button1->TabIndex = 3;
			this->button1->Text = L"ќтправить";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &PassInput::button1_Click);
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->ForeColor = System::Drawing::Color::Red;
			this->label2->Location = System::Drawing::Point(13, 53);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(0, 13);
			this->label2->TabIndex = 5;
			// 
			// PassInput
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(284, 111);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->button1);
			this->Controls->Add(this->textBox1);
			this->Controls->Add(this->label1);
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedToolWindow;
			this->Name = L"PassInput";
			this->Text = L"¬вод парол€";
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void button1_Click(System::Object^  sender, System::EventArgs^  e) {
		MessageBox::Show(Application::ExecutablePath->ToString());
		String^ pass = textBox1->Text;
		if (String::IsNullOrEmpty(pass))
		{
			label2->Text = "«аполните поле";
		}
		else if (!File::Exists("pass.txt")) {
			String^ str = encrypt->WritePass(pass);
			AccessGranted^ accessGranted = gcnew AccessGranted(str);
			accessGranted->Show();
			Hide();
		}
		else
		{
			KeyInput^ keyInput = gcnew KeyInput(pass);
			keyInput->Show();
			button1->Hide();
		}
	}
	};
}
