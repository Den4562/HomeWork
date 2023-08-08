#pragma once
#include <iostream> 
#include"Menu.h"
#include"User.h"
#include "exam.h"
#include "Admin_Func.h" 
#include "Student.h"
#include "Encryption.h"
#include "Testing.h"
#include<list>
#include<map>
#include <iostream>
#include <fstream>
#include <string.h>
#include <stdio.h>
#include <regex>

using namespace std;


class Admin : public User
{
	string FIO;
	exam_file* exam = nullptr;
public:
	Admin();
	Admin(string name, string pass);
	Admin(string name, string pass, string FIO);
	virtual void menu() override;
	string get_FIO();
	void   setFIO(string name);
	void menegment_user();
	void add_user();
	void delete_user();
	void user_modification();
	void statistic()									  const;
	void statistic_categories(string category)					      const;
	void statistic_test(string name_category, string name_test)const;
	void statistic_user(string user)							  const;
	void menegment_tests();
	void add();
	void del();
	void add_test();
	void delete_test();
	void add_category();
	void delete_category();
	void add_questions();
	void delete_questions();
	void export_txt(string name_file_test, string name_file_user)const;
};

Admin::Admin() :User() {};

Admin::Admin(string name, string pass) : User(name, pass) 
{
	string st;
	ifstream f(this->getName() + "_inform_on_admin.txt");
	if (f.is_open())
	{
		getline(f, st);
		setName(st);
		getline(f, st);
		setFIO(st);
	}
};

Admin::Admin(string name, string pass, string FIO)
{
	ofstream out(name + "_inform_on_admin.txt");
	if (out.is_open())
	{
		out << name << "\n";
		out << FIO << "\n";
		out << pass << "\n";
	}
}

void Admin::setFIO(string name) { FIO = name; }
string Admin::get_FIO()			{ return FIO; }

void Admin::menu()
{
	string name_test, name_category, name_file, name_file_test;
	do {
		system("cls");
		cout << "\n\n\n\n\n\n\t\t\t\tПрофiль адміна : " << get_FIO();
		int choice = Menu::select_vertical(
			{ "Управлiння користувачами",
			  "Перегляд статистики",
			  "Управління тестуваннями",
			  "Загрузить тест в файл",
			  "Вийти"					},
			HorizontalAlignment::Center);
		switch (choice)
		{
		case 0: menegment_user();		break;
		case 1: statistic();		break;
		case 2: menegment_tests();		break;
		case 3: 
			test t;
			t.files(base_category);
			cout << "Назва потрібної категорії :\n";
			getline(cin, name_category);
			if (exam->exam_base(base_category, name_category)) {
				t.file_category_tests(name_category);
				cout << "Назва потрібного тесту :\n";
				getline(cin, name_test);
				while (exam->exam_str(name_test))
				{
					cout << "Ви нічого не написали\n";	getline(cin, name_test);
				}
				if (exam->exam_base(base_tests, name_category + "_" + name_test)) {
					system("cls");
					cout << "Назва вашого файлу - \n";
					getline(cin, name_file);
					while (exam->exam_str(name_file))
					{
						cout << "Ви нічого не написали\n";	getline(cin, name_category);
					}
					ofstream out(name_file + ".txt");
					if (out.is_open())
					{
						out << "Категорія - " << name_category << "\n";
						out << "Назва тесту - " << name_test << "\n";
					}
					out.close();
					name_file_test = name_category + "_" + name_test;
					export_txt(name_file_test, name_file);
				}
			}break;	break;
		case 4: return;
		}
	} while (true);
}

void Admin::menegment_user()
{
	do {
		system("cls");
		cout << "\n\n\n\n\n\n\t\t\t\tУправління користувачами ";
		int choice = Menu::select_vertical(
			{ "Додати користувача",
			  "Видалити користувача",
			  "Змінити інформацію користувача",
			  "Вийти" },
			HorizontalAlignment::Center);
		switch (choice)
		{
		case 0: 	 add_user();			break;
		case 1: 	 delete_user();		    break;
		case 2: 	 user_modification();	break;
		case 3:								return;
		}
	} while (true);
}

void Admin::add_user()
{
	AdminFunc::add_user(*this, base_users, temp, exam);
}

void Admin::delete_user() 
{
	AdminFunc::delete_user(*this, base_users, temp, exam); 
}


void Admin::user_modification()
{
	AdminFunc::user_modification(*this, base_users, temp, exam);
}




void Admin::menegment_tests()
{
	do {
		system("cls");
		cout << "\n\n\n\n\n\n\t\t\t\tУправління тестами ";
		int choice = Menu::select_vertical(
			{ "Додати",
			  "Видалити",
			  "Вийти" },
			HorizontalAlignment::Center);
		switch (choice)
		{
		case 0: add(); break;
		case 1: del(); break;
		case 2: return;
		}
	} while (true);
}

void Admin::add()
{
	do {
		system("cls");
		cout << "\n\n\n\n\n\n\t\t\t\tДодавання ";
		int choice = Menu::select_vertical(
			{ "Додати категорію ",
			  "Додати тест ",
			  "Додати запитання ",
			  "Вийти" },
			HorizontalAlignment::Center);
		switch (choice)
		{
		case 0: add_category();		break;
		case 1: add_test();			break;
		case 2: add_questions();	break;
		case 3: return;
		}
	} while (true);
}

void Admin::del()
{
	do {
		system("cls");
		cout << "\n\n\n\n\n\n\t\t\t\tВидалення ";
		int choice = Menu::select_vertical(
			{ "Видалити категорію",
			  "Видалити тест ",
			  "Видалити запитання",
			  "Вийти" },
			HorizontalAlignment::Center);
		switch (choice)
		{
		case 0: delete_category();		break;
		case 1: delete_test();			break;
		case 2: delete_questions();	break;
		case 3: return;
		}
	} while (true);
}


void Admin::add_test()
{
	cout << "~ Існуючі категорії\n";
	test t;
	t.files(base_category);
	cout << "Введіть назву потрібної категорії: \n";
	string name_category, name_test, buff_str;
	getline(cin, name_category);
	while (exam->exam_str(name_category))
	{
		cout << "Ви нічого не написали\n";	getline(cin, name_category);
	}
	if (exam->exam_base(base_category, name_category) == 1)
	{
		system("cls");
		cout << "~ Існуючі тести \n";
		t.file_category_tests(name_category);
		cout << "Введіть назву потрібного тесту\n";
		getline(cin, name_test);
		while (exam->exam_str(name_test))
		{
			cout << "Ви нічого не написали\n";	getline(cin, name_test);
		}
		name_test = name_category + "_" + name_test;
		if (exam->exam_base(base_tests, name_test) == 0)
		{
			t.add(name_test, t.get_name_answer(name_test));
		}
		else
		{
			cout << "Тест з такою назвою вже є. Хочете додати запитання? 1 - так , 0 - ні\n";
			bool a; cin >> a; cin.ignore();
			if (a == true)
			{
				add_questions();
			}
			else return;
		}
	}
	else
	{
		cout << "Такої категорії нема. Додати нову? ?1 - так , 0 - ні\n";
		bool a; cin >> a; cin.ignore();
		if (a == true)
		{
			add_category();
		}
		else return;
	}
}

void Admin::add_category()
{
	cout << "~ Існуючі категорії \n";
	test t;
	t.files(base_category);
	cout << "Введіть назву нової категорії\n";
	string name_category, buff_str;
	getline(cin, name_category);
	while (exam->exam_str(name_category))
	{
		cout << "Ви нічого не написали\n";	getline(cin, name_category);
	}
	ifstream f(base_category);
	if (f.is_open())
	{
		if (exam->exam_base(base_category, name_category) == 1)
		{
			f.close();
			cout << "Така категорія вже існує\n";
			system("pause");	return;
		}
		f.close();
		ofstream out(base_category, ios::app);
		if (out.is_open())
		{
			out << name_category << "\n";
		}
		else
		{
			cout << "Файл не відкрився\n";
			system("pause");
		}
		out.close();
	}
	else
	{
		cout << "Файл не відкрився"; system("pause");
	}
}


void Admin::add_questions()
{
	test t;
	cout << "~ Існуюючі категорії\n";
	t.files(base_category);
	cout << "Введіть назву потрібної категорії : \n";
	string name_test, name_category, str;
	getline(cin, name_category);
	while (exam->exam_str(name_category))
	{
		cout << "Ви нічого не написали\n";	getline(cin, name_category);
	}
	if (exam->exam_base(base_category, name_category) == 1)
	{
		cout << "~ Існуючі тести \n";
		t.file_category_tests(name_category);
		cout << "Введіть назву потрібного тесту :\n";
		getline(cin, name_test);
		while (exam->exam_str(name_test))
		{
			cout << "Ви нічого не написали\n";	getline(cin, name_test);
		}
		if (exam->exam_base(base_tests, name_category + "_" + name_test) == 1)
		{
			ifstream f(name_category + "_" + name_test + ".txt");
			if (f.is_open())
			{
				t.add(name_category + "_" + name_test, t.get_name_answer(name_category + "_" + name_test));
			}
			f.close();
		}
	}
}


void Admin::delete_test()
{
	cout << "~ Існуючі категорії\n";
	test t;
	t.files(base_category);

	cout << "Введіть назву потрібної категорії : \n";
	string name_category, name_test, buff_str;
	getline(cin, name_category);
	while (exam->exam_str(name_category))
	{
		cout << "Ви нічого не написали\n";
		getline(cin, name_category);
	}

	if (exam->exam_base(base_category, name_category) == 1)
	{
		cout << "~ Існуючі тести \n";
		t.file_category_tests(name_category);

		cout << "Введіть назву потрібного тесту : \n";
		getline(cin, name_test);
		while (exam->exam_str(name_test))
		{
			cout << "Ви нічого не написали\n";
			getline(cin, name_test);
		}

		if (exam->exam_base(base_tests, name_category + "_" + name_test) == 1)
		{
			name_test = name_category + "_" + name_test;
			ifstream f(base_tests);
			ofstream out(temp);

			if (f.is_open() && out.is_open())
			{
				while (getline(f, buff_str))
				{
					if (name_test != buff_str)
					{
						out << buff_str << "\n";
					}
					else
					{
						name_test += ".txt";
						remove(name_test.c_str());
						name_test = regex_replace(name_test, regex(".txt"), "");
						name_test += "_answer.txt";
						remove(name_test.c_str());
						cout << "Тест було видалено\n";
						system("pause");
					}
				}
				f.close();
				out.close();

				ifstream ft(temp);
				ofstream out2(base_tests);
				if (ft.is_open() && out2.is_open())
				{
					while (getline(ft, buff_str))
					{
						out2 << buff_str << "\n";
					}
				}

				remove(temp.c_str());
				ft.close();
				out2.close();
			}
			else
			{
				cout << "Файл не відкрився\n";
				system("pause");
			}
		}
	}
}

void Admin::delete_category()
{
	cout << "~ Існуючі категорії\n";
	test t;
	t.files(base_category);

	cout << "Введіть назву потрібної категорії : \n";
	string name_category;
	getline(cin, name_category);
	while (exam->exam_str(name_category))
	{
		cout << "Ви нічого не написали\n";
		getline(cin, name_category);
	}

	if (exam->exam_base(base_category, name_category) == true)
	{
		string buff;
		ifstream f_test(base_tests);
		ofstream out_temp_test(temp);

		if (f_test.is_open() && out_temp_test.is_open()) {
			while (getline(f_test, buff))
			{
				if (exam->exam_category(name_category, buff) == false)
				{
					out_temp_test << buff << "\n";
				}
				else
				{
					buff += ".txt";
					remove(buff.c_str());
					buff = regex_replace(buff, regex(".txt"), "");
					buff += "_answer.txt";
					remove(buff.c_str());
				}
			}
			f_test.close(); out_temp_test.close();

			ifstream f_temp(temp);
			ofstream out_tests(base_tests);

			if (f_temp.is_open() && out_tests.is_open())
			{
				while (getline(f_temp, buff))
				{
					out_tests << buff << "\n";
				}
				f_temp.close(); out_tests.close();
			}
			remove(temp.c_str());

			ifstream f_category(base_category);
			ofstream out_temp_category(temp);

			if (f_category.is_open() && out_temp_category.is_open())
			{
				while (getline(f_category, buff))
				{
					if (name_category != buff)
					{
						out_temp_category << buff << "\n";
					}
				}
				f_category.close(); out_temp_category.close();
			}

			ifstream f_temp_category(temp);
			ofstream out_category(base_category);

			if (f_temp_category.is_open() && out_category.is_open())
			{
				while (getline(f_temp_category, buff))
				{
					out_category << buff << "\n";
				}
				f_temp_category.close(); out_category.close();
				cout << "Всі тести з цієї категорії було видалено\n";
				system("pause");
			}
			remove(temp.c_str());
		}
	}
	else
	{
		cout << "Файл не відкрився\n";
		system("pause");
	}
}


void Admin::delete_questions()
{
	cout << "~ Існуючі категорії\n";
	test t;
	t.files(base_category);

	cout << "Введіть назву потрібної категорії : \n";
	string name_category, name_test, str;
	int num;
	getline(cin, name_category);
	while (exam->exam_str(name_category))
	{
		cout << "Ви нічого не написали\n";
		getline(cin, name_category);
	}

	if (exam->exam_base(base_category, name_category) == 1)
	{
		cout << "~ Існуючі тести \n";
		t.file_category_tests(name_category);

		cout << "Введіть назву потрібного тесту : \n";
		getline(cin, name_test);
		while (exam->exam_str(name_test))
		{
			cout << "Ви нічого не написали\n";
			getline(cin, name_test);
		}

		if (exam->exam_base(base_tests, name_category + "_" + name_test) == 1)
		{
			int count = 1;
			ifstream f_tests(name_category + "_" + name_test + ".txt");
			if (f_tests.is_open())
			{
				cout << "Номер\tзавдання" << "\n";
				while (getline(f_tests, str))
				{
					cout << count << "\t" << str << "\n";
					getline(f_tests, str);
					count++;
				}
				system("pause");
			}
			f_tests.close();

			ifstream f_tests2(name_category + "_" + name_test + ".txt");
			cout << "Введіть номер завдання\n";
			cin >> num;
			cin.ignore();
			ofstream out_temp(temp);

			if (out_temp.is_open() && f_tests2.is_open())
			{
				for (size_t i = 1; i < num; i++)
				{
					getline(f_tests2, str);
					out_temp << str << "\n";
					getline(f_tests2, str);
					out_temp << str << "\n";
				}

				getline(f_tests2, str);
				getline(f_tests2, str);

				for (size_t i = num + 1; i < count; i++)
				{
					getline(f_tests2, str);
					out_temp << str << "\n";
					getline(f_tests2, str);
					out_temp << str << "\n";
				}
			}

			out_temp.close();
			f_tests2.close();

			ifstream f_temp_(temp);
			ofstream out_test(name_category + "_" + name_test + ".txt");

			if (f_temp_.is_open() && out_test.is_open())
			{
				while (getline(f_temp_, str))
				{
					out_test << str << "\n";
				}
			}

			out_test.close();
			f_temp_.close();
			remove(temp.c_str());

			ifstream f_answer(name_category + "_" + name_test + "_answer.txt");
			ofstream out_temp_answer(temp);

			if (f_answer.is_open() && out_temp_answer.is_open())
			{
				for (size_t i = 1; i < num; i++)
				{
					getline(f_answer, str);
					out_temp_answer << str << "\n";
				}

				getline(f_answer, str);

				for (size_t i = num + 1; i < count; i++)
				{
					getline(f_answer, str);
					out_temp_answer << str << "\n";
				}
			}

			out_temp_answer.close();
			f_answer.close();

			ifstream f_temp_answer(temp);
			ofstream out_answer(name_category + "_" + name_test + "_answer.txt");

			if (f_temp_answer.is_open() && out_answer.is_open())
			{
				while (getline(f_temp_answer, str))
				{
					out_answer << str << "\n";
				}
			}

			out_answer.close();
			f_temp_answer.close();
		}
	}
}


void Admin::export_txt(string name_file_test, string name_file_user) const
{
	ifstream f_test(name_file_test + ".txt");
	ifstream f_test_answer(name_file_test + "_answer.txt");
	ofstream out_test(name_file_user + ".txt", ios::app);
	string buff;
	if (f_test.is_open() && f_test_answer.is_open() && out_test.is_open())
	{
		for (size_t i = 1; getline(f_test, buff); i++)
		{
			out_test << i << ". " << buff << "\n";
			getline(f_test, buff);
			out_test << " ~ " << buff << "\n";
		}
		out_test << "Відповіді : \n";
		for (size_t i = 1; getline(f_test_answer, buff); i++)
		{
			out_test << i << ". " << buff << "\n";
		}
	}	f_test.close(); f_test_answer.close(); out_test.close();
}



void Admin::statistic() const
{
	do {
		string category, test1, user, buff;
		ifstream base;
		test t;

		system("cls");
		cout << "\n\n\n\n\n\n\t\t\t\tСтатистика ";

		int choice = Menu::select_vertical(
			{ "За категорією",
			  "За тестом",
			  "За студентом",
			  "Вийти" },
			HorizontalAlignment::Center);

		switch (choice)
		{
		case 0:
			cout << "~ Існуючі категорії \n";
			t.files(base_category);
			cout << "Введіть категорію\n";
			getline(cin, category);

			while (exam->exam_str(category))
			{
				cout << "Ви нічого не написали\n";
				getline(cin, category);
			}

			if (exam->exam_base(base_category, category))
			{
				system("cls");
				statistic_categories(category);

				base.open(base_tests);
				if (base.is_open())
				{
					while (getline(base, buff))
					{
						if (exam->exam_category(category, buff))
						{
							buff.erase(buff.begin(), buff.begin() + category.size() + 1);
							statistic_test(category, buff);
						}
					}
				}
				base.close();
			}

			system("pause");
			break;
		case 1:
			cout << "~ Існуючі категорії \n";
			t.files(base_category);
			cout << "Введіть категорію\n";
			getline(cin, category);

			while (exam->exam_str(category))
			{
				cout << "Ви нічого не написали\n";
				getline(cin, category);
			}

			if (exam->exam_base(base_category, category))
			{
				system("cls");
				cout << "~ Існуючі тести \n";
				t.file_category_tests(category);
				cout << "Введіть назву теста\n";
				getline(cin, test1);

				while (exam->exam_str(test1))
				{
					cout << "Ви нічого не написали\n";
					getline(cin, test1);
				}

				buff = category + "_" + test1;
				if (exam->exam_base(base_tests, buff))
				{
					system("cls");
					statistic_test(category, test1);
				}
			}

			system("pause");
			break;
		case 2:
			cout << "~ Існуючі юзери\n";
			base.open(base_users);

			if (base.is_open())
			{
				while (getline(base, buff))
				{
					if (buff.find("Admin_") == string::npos)
					{
						cout << buff << "\n";
					}
					getline(base, buff);
				}
				getline(cin, user);

				while (exam->exam_str(user))
				{
					cout << "Ви нічого не написали\n";
					getline(cin, user);
				}

				if (exam->exam_user(user))
				{
					statistic_user(user);
				}
			}

			system("pause");
			break;
		case 3:
			return;
		}
	} while (true);
}

void Admin::statistic_categories(string category) const
{
	string user, inf, buff;
	double summ = 0.0;
	int count = 0;
	size_t size = category.size();
	ifstream base;
	base.open(base_users);
	if (base.is_open()) {
		while (getline(base, user))
		{
			ifstream user_res(user + "_result.txt");
			if (user_res.is_open())
			{
				while (getline(user_res, inf))
				{
					inf.erase(inf.begin(), inf.begin() + 12);
					buff = inf;
					buff.resize(size);
					if (category == buff)
					{
						inf.erase(inf.begin(), inf.begin() + size + 8);
						getline(user_res, inf);
						inf.erase(inf.begin(), inf.begin() + 20);
						inf.resize(2);
						try { summ += stoi(inf); }
						catch (exception& e) { summ += 0; }
						count++;
					}
				}
			}
			getline(base, user);
		}
	}
	base.close();
	if (count != 0) cout << "~Середня оцінка з категорії " << category << " - " << (summ / count) << " балів\n";
	else cout << "~Середня оцінка з категорії " << category << " - " << 0 << " балів\n";
	cout << "Кількість проходжень - " << count << "\n";
	cout << "~Середнi оцінки тестів : \n";
}

void Admin::statistic_test(string name_category, string name_test) const
{
	map<string, double> user_marks;
	map<string, double> count_user;
	string buff, user_buff, user_name;
	double total_marks = 0.0;
	int count = 0;

	ifstream base(base_users);
	if (base.is_open())
	{
		while (getline(base, buff))
		{
			ifstream user(buff + "_result.txt");
			if (user.is_open())
			{
				while (getline(user, user_buff))
				{
					user_name = buff;
					user_marks[user_name];
					user_buff.erase(user_buff.begin(), user_buff.begin() + name_category.size() + 20);
					if (user_buff == name_test)
					{
						getline(user, user_buff);
						user_buff.erase(user_buff.begin(), user_buff.begin() + 20);
						user_buff.resize(2);
						try
						{
							double mark = stod(user_buff);
							total_marks += mark;
							user_marks[user_name] += mark;
						}
						catch (exception& e)
						{
							// Handle exception if needed
						}
						count_user[user_name] += 1;
						count++;
					}
					else
					{
						getline(user, user_buff);
					}
				}
			}
			getline(base, buff);
		}
	}

	base.close();

	if (count != 0)
	{
		cout << "~Середня оцінка з тесту " << name_test << " - " << (total_marks / count) << " балів\n";
	}
	else
	{
		cout << "~Середня оцінка з тесту " << name_test << " - " << 0 << " балів\n";
	}

	cout << "Кількість проходжень - " << count << "\n";

	for (const auto& n : user_marks)
	{
		for (const auto& a : count_user)
		{
			if (n.first == a.first)
			{
				cout << n.first << " - " << n.second / a.second << " балів \n";
			}
		}
	}
}

void Admin::statistic_user(string user) const
{
	map<string, double> m;
	map<string, double> c;
	ifstream user_(user + "_result.txt");
	string buff, test, category;
	size_t n;
	double summ = 0.0;
	if (user_.is_open())
	{
		while (getline(user_, buff))
		{
			buff.erase(buff.begin(), buff.begin() + 12);
			n = buff.find("тест");
			category = buff;
			category.erase(category.begin() + n, category.end());
			test = buff;
			test.erase(test.begin(), test.begin() + n + 7);
			getline(user_, buff);
			buff.erase(buff.begin(), buff.begin() + 20);
			buff.resize(2);
			try { summ = stoi(buff); }
			catch (exception& e) { summ += 0; }
			m["Категорія - " + category + ", тест " + test] += summ;
			c["Категорія - " + category + ", тест " + test] += 1;
		}
		for (const auto& n : m)
		{
			for (const auto& a : c)
			{
				if (n.first == a.first)
				{
					cout << n.first << " - " << n.second / a.second << " балів \n";
				}
			}
		}
	}user_.close();
}