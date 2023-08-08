#pragma once
#include <iostream>
#include "Menu.h"
#include "User.h"
#include "exam.h"
#include "Student.h"
#include "Admin.h" 
#include "Admin_Func.h"
#include "Encryption.h"
#include <list>
#include <map>
#include <fstream>
#include <string>
#include <regex>


class test
{
	exam_file exam;
public:
	void   add(string name_test, string name_file_answer);
	void   go_test(string name_test, string my_answer)						const;
	void   result(string name, string name_faile_answer, string my_answer)  const;
	string get_name_answer(string name_test)								const;
	void   files(string name_file)										    const;
	void   file_category_tests(string categort)								const;
};

void test::add(string name_test, string name_file_answer)
{
	system("cls");
	ofstream out_test(name_test + ".txt", ios::app);
	ofstream out_answer(name_file_answer + ".txt", ios::app);
	ofstream out_base("base_tests.txt", ios::app);
	size_t count = 1;
	if (out_test.is_open() && out_answer.is_open() && out_base.is_open())
	{
		size_t a;
		do {
			string st;
			cout << "Завдання № " << count << "\n"; getline(cin, st);
			while (exam.exam_str(st))
			{
				cout << "Завдання не може бути пустим, введіть повторно :\n"; getline(cin, st);
			}
			out_test << st << "\n";
			cout << "Варінти відповідей. Формат: 'Номер.Вiдповыдь *пробел*' \n";  
			getline(cin, st); 
			out_test << st << "\n";
			cout << "Правильна відповідь \n"; 
			getline(cin, st); 
			out_answer << st << "\n";
			cout << "Додадти ще запитання?(0 - ні, 1 - так)\n";
			cin >> a;
			while (cin.fail())
			{
				cin.clear();
				cin.ignore(32767, '\n');
				cout << "Додадти ще запитання?(0 - ні, 1 - так)\n";
				cin >> a;
			}
			cin.ignore();
			count++;
			if (a == 0)
			{
				if (exam.exam_base("base_tests.txt", name_test) == false)
				{
					out_base << name_test << "\n";
					cout << "Тест завантажено у базу\n";
					system("pause");
				}
				out_test.close(); out_answer.close(); out_base.close();	return;
			}system("cls");
		} while (true);
	}
	else
	{
		cout << "Файл для запису не був відкритий\n";	system("pause");
	}
}

void test::go_test(string name_test, string my_answer) const
{
	ifstream f(name_test);
	ofstream out(my_answer);
	string str, otv;
	int count = 1;
	if (f.is_open() && out.is_open())
	{
		while (getline(f, str))
		{
			system("cls");
			cout << "Тест - \n";
			cout << "Завдання №" << count << "\n";
			cout << str << "\nВаріанти відповідей "; getline(f, str);
			cout << str << "\n";
			cout << "Ваша відповідь - "; cin >> otv;
			count++;
			out << otv << "\n";
		}f.close(); out.close();
	}
	else
	{
		cout << "Файл не був відкритий\n"; system("pause");
		f.close(); out.close(); return;
	}
}

void test::result(string name, string name_file_answer, string my_answer) const
{
	system("cls");
	int p = 0, n = 0;
	string str, otv;
	ifstream f1(name_file_answer);
	ifstream f2(my_answer);
	ofstream out(name + "_result.txt", ios::app);
	if (f1.is_open() && f2.is_open() && out.is_open())
	{
		while (getline(f1, str) && getline(f2, otv))
		{
			if (str == otv) { p++; }
			else { n++; }
		}
		f1.close(); f2.close();
		double answer = 12 / (p + n);
		answer = answer * p;
		cout << p << " - правельних відповідей\n";
		cout << n << " - неправильних відповідей\n";
		if (p == p + n)
		{
			cout << "12 - балів з 12 \n";
			out << "З цього тесту у вас 12 з 12 \n";
		}
		else
		{
			cout << answer << " - балів из 12 \n";
			out << "З цього тесту у вас " << answer << " з 12 \n";
		}
		system("pause");
		f1.close(); out.close(); f2.close();
		remove(my_answer.c_str());
		return;
	}
	else
	{
		cout << "Файл не був відкритий";
		system("pause");
		f1.close(); out.close(); f2.close();
		return;
	}
}

string test::get_name_answer(string name_test) const
{
	return name_test + "_answer";
}

void test::files(string file_name) const
{
	ifstream f(file_name);
	string st;
	if (f.is_open())
	{
		while (getline(f, st))
		{
			cout << " - " << st << "\n";
		}
	}f.close();
}

void test::file_category_tests(string category) const
{
	string buff_str;
	ifstream f("base_tests.txt");
	if (f.is_open()) {
		while (getline(f, buff_str))
		{
			if (exam.exam_category(category, buff_str) == true)
			{
				buff_str = regex_replace(buff_str, regex(category + "_"), "");
				cout << " - " << buff_str << "\n";
			}
		}
	}
}

