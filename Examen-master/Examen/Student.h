#pragma once
#include "User.h"
#include "exam.h"
#include "Menu.h"
#include "Encryption.h"
#include "Admin_Func.h"
#include "Testing.h"
#include "Admin.h"
#include <fstream>
#include <iostream>
#include <string>


using namespace std;

class Student : public User
{
	exam_file* exam;
	string number;
	string address;
	string FIO;
public:
	Student();
	Student(string name, string pass);
	Student(string name, string pass, string number, string address, string FIO);
	virtual ~Student();
	virtual void menu() override;
	void   setNumber(string number);
	void   setAddress(string address);
	void   setFIO(string fio);
	string getNumber() const;
	string getAddress() const;
	string get_FIO() const;
	void   add(string category, string test);
	void   info();
	void   profil();
	void   viewing_previons_tests() const;
	void   take_a_test();
};


Student::Student() : User(), number("no number"), address("no address"), FIO("no FIO") {};

Student::Student(string name, string pass) : User(name, pass) { info(); }

Student::Student(string name, string pass, string number, string address, string FIO)
	: User(name, pass), number(number), address(address), FIO(FIO)
{
	ofstream out(name + "_inform_on_user.txt");
	if (out.is_open())
	{
		out << name << "\n";
		out << FIO << "\n";
		out << number << "\n";
		out << address << "\n";
	}
};

void Student::info()
{
	string st;
	ifstream f(this->getName() + "_inform_on_user.txt");
	if (f.is_open())
	{
		getline(f, st);
		setName(st);
		getline(f, st);
		setFIO(st);
		getline(f, st);
		setAddress(st);
		getline(f, st);
		setNumber(st);
	}
}

void Student::profil()
{
	cout << "Логін - " << this->getName();
	cout << "\nПІБ - " << this->get_FIO();
	cout << "\nНомер телефону - " << this->getNumber();
	cout << "\nАдресса - " << this->getAddress();
	cout << "\nПароль - " << this->getPassword();
	cout << "\n";
	system("pause");
}

Student::~Student() {}

void	Student::setNumber(string number) { this->number = number; }
void	Student::setAddress(string address) { this->address = address; }
void	Student::setFIO(string fio) { FIO = fio; }
string  Student::getNumber() const { return number; }
string  Student::getAddress() const { return address; }
string  Student::get_FIO() const { return FIO; }

void Student::menu()
{
	do {
		system("cls");
		cout << "\n\n\n\n\n\n\t\t\t\tПрофiль студента : " << get_FIO();
		int choice = Menu::select_vertical(
			{ "Перегляд попереднiх результатiв",
			  "Пройти тест",
			  "Переглянути профіль",
			  "Вийти" },
			HorizontalAlignment::Center);
		switch (choice)
		{
		case 0: viewing_previons_tests();			break;
		case 1: take_a_test();					    break;
		case 2: profil();							break;
		case 3: return;
		}
	} while (true);
}


void Student::viewing_previons_tests() const
{
	ifstream f(this->getName() + "_result.txt");
	string st;
	if (f.is_open())
	{
		while (getline(f, st)) { cout << st << "\n"; }
	}
	else { cout << "По вашим проходженням немає данних\n"; }
	f.close();
	system("pause");
}

void Student::take_a_test()
{
	cout << "~ Існуючі категорії \n";
	test t;
	t.files(base_category);
	cout << "Введіть назву потрібної категорії : \n";
	string name_category, name_test, buff_str;
	getline(cin, name_category);
	while (exam->exam_str(name_category))
	{
		cout << "Ви нічого не написали\n";	getline(cin, name_category);
	}
	system("cls");
	if (exam->exam_base(base_category, name_category) == true)
	{
		system("cls");
		cout << "~ Існуючі тести тесты \n";
		ifstream f(base_tests);
		if (f.is_open()) {
			while (getline(f, buff_str))
			{
				if (exam->exam_category(name_category, buff_str) == true)
				{
					buff_str = regex_replace(buff_str, regex(name_category + "_"), "");
					cout << " - " << buff_str << "\n";
				}
			}
		}
		cout << "Введіть назву теста :\n";
		getline(cin, name_test);
		while (exam->exam_str(name_test))
		{
			cout << "Ви нічого не написали\n";	getline(cin, name_test);
		}
		name_test = name_category + "_" + name_test;
		if (exam->exam_base(base_tests, name_test) == true)
		{
			string answer = this->getName() + "_" + t.get_name_answer(name_test) + ".txt";
			add(name_category, regex_replace(name_test, regex(name_category + "_"), ""));
			t.go_test(name_test + ".txt", answer);
			t.result(this->getName(), t.get_name_answer(name_test) + ".txt", answer);
		}
	}
}


void Student::add(string category, string test)
{
	ofstream out(this->getName() + "_result.txt", ios::app);
	if (out.is_open())
	{
		out << "Категорія : " << category << " тест - " << test << "\n";
	}
}