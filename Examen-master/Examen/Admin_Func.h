#pragma once
#include "User.h"
#include "exam.h"
#include "Menu.h"
#include "Student.h"
#include "Encryption.h"
#include "Admin.h"
#include <fstream>
#include <iostream>
#include <string>
using namespace std;



class Admin;

namespace AdminFunc {
	void delete_user(Admin& admin, const string& base_users, const string& temp, exam_file* exam);;
	void add_user(Admin& admin, const std::string& base_users, const std::string& temp, exam_file* exam);
	void user_modification(Admin& admin, const std::string& base_users, const std::string& temp, exam_file* exam);
}



void AdminFunc::user_modification(Admin& admin, const std::string& base_users, const std::string& temp, exam_file* exam)
{
	ofstream out; ifstream f;
	f.open(base_users);
	if (f.is_open())
	{
		ifstream f2(base_users);
		string st;
		string name_student, buff, new_inform;
		cout << "Список существующих пользователей:\n";
		while (getline(f2, st))
		{
			if (st.find("Admin") == string::npos) {
				cout << " - " << st << "\n";
			}
			getline(f2, st);
		}

		cout << "Введіть логін користувача\n";
		getline(cin, name_student);
		while (exam->exam_str(name_student)) 
		{
			cout << "Ви нічого не написали\n";
			getline(cin, name_student);
		}

		while (getline(f, buff))
		{
			if (name_student == buff) {
				cout << "Для перевірки введіть пароль користувача \n";
				string pass_student;
				getline(cin, pass_student);
				while (exam->exam_str(pass_student))
				{
					cout << "Ви нічого не написали\n";	getline(cin, pass_student);
				}
				pass_student = encryptDecrypt(pass_student);
				getline(f, buff);
				if (pass_student == buff)
				{
					f.close();
					system("cls");
					cout << "\n\n\n\n\n\n\t\t\t\tВиберіть що хочете змінити";
					int choice = Menu::select_vertical(
						{ "Login",
						  "Пароль",
						  "ПІБ",
						  "Номер телефону",
						  "Адреса",
						  "Вихід" },
						HorizontalAlignment::Center);
					switch (choice)
					{
					case 0:
						f.open(base_users); out.open(temp);
						cout << "Введіть новий логін\n";
						getline(cin, new_inform);
						while (exam->exam_str(new_inform))
						{
							cout << "Ви нічого не написали\n";	getline(cin, new_inform);
						}
						if (exam->exam_user(new_inform) == false)
						{
							if (f.is_open() && out.is_open())
							{
								while (getline(f, buff))
								{
									if (buff != name_student)
									{
										out << buff << "\n";
									}
									else
									{
										out << new_inform << "\n";
									}
								}
								f.close(); out.close();
								f.open(temp); out.open(base_users);
								if (f.is_open() && out.is_open())
								{
									while (getline(f, buff))
									{
										out << buff << "\n";
									}
								}
								f.close(); out.close();
								f.open(name_student + "_inform_on_user.txt"); out.open(temp);
								if (f.is_open() && out.is_open())
								{
									while (getline(f, buff))
									{
										if (buff != name_student)
										{
											out << buff << "\n";
										}
										else
										{
											out << new_inform << "\n";
										}
									}
									f.close(); out.close();
									name_student += "_inform_on_user.txt";
									remove(name_student.c_str());
									f.open(temp); out.open(new_inform + "_inform_on_user.txt");
									if (f.is_open() && out.is_open())
									{
										while (getline(f, buff))
										{
											out << buff << "\n";
										}
									}
								}
								f.close(); out.close();
							}
						}
						else { cout << "Такий логін вже використовується\n"; system("pause"); }
						return;
					case 1:
						f.open(base_users); out.open(temp);
						if (f.is_open() && out.is_open())
						{
							cout << "Введіть новий пароль\n";
							getline(cin, new_inform);
							while (exam->exam_str(new_inform))
							{
								cout << "Ви нічого не написали\n";	getline(cin, new_inform);
							}
							while (getline(f, buff))
							{
								if (buff == name_student)
								{
									out << name_student << "\n";
									out << encryptDecrypt(new_inform) << "\n";
									getline(f, buff);
								}
								else
								{
									out << buff << "\n";
								}
							}
							f.close(); out.close();
							f.open(temp); out.open(base_users);
							if (f.is_open() && out.is_open())
							{
								while (getline(f, buff))
								{
									out << buff << "\n";
								}
							}
						}break;
					case 2:
						f.open(name_student + "_inform_on_user.txt"); out.open(temp);
						if (f.is_open() && out.is_open())
						{
							getline(f, buff);
							out << buff << "\n";
							cout << "Введіть нове ім'я\n";
							getline(cin, buff);
							while (exam->exam_str(buff))
							{
								cout << "Ви нічого не написали\n";	getline(cin, buff);
							}
							out << buff << "\n";
							getline(f, buff);
							getline(f, buff);
							out << buff << "\n";
							getline(f, buff);
							out << buff << "\n";
							f.close(); out.close();
							f.open(temp); out.open(name_student + "_inform_on_user.txt");
							if (f.is_open() && out.is_open())
							{
								while (getline(f, buff))
								{
									out << buff << "\n";
								}
							}
						}break;
					case 3:
						f.open(name_student + "_inform_on_user.txt"); out.open(temp);
						if (f.is_open() && out.is_open())
						{
							getline(f, buff);
							out << buff << "\n";
							getline(f, buff);
							out << buff << "\n";
							getline(f, buff);
							out << buff << "\n";
							cout << "Введіть новий номер\n";
							getline(cin, buff);
							while (exam->exam_str(buff))
							{
								cout << "Ви нічого не написали\n";	getline(cin, buff);
							}
							out << buff << "\n";
							f.close(); out.close();
							f.open(temp); out.open(name_student + "_inform_on_user.txt");
							if (f.is_open() && out.is_open())
							{
								while (getline(f, buff))
								{
									out << buff << "\n";
								}
							}
						}break;
					case 4:
						f.open(name_student + "_inform_on_user.txt"); out.open(temp);
						if (f.is_open() && out.is_open())
						{
							getline(f, buff);
							out << buff << "\n";
							getline(f, buff);
							out << buff << "\n";
							cout << "Введіть ному адресу\n";
							getline(cin, buff);
							while (exam->exam_str(buff))
							{
								cout << "Ви нічого не написали\n";	getline(cin, buff);
							}
							out << buff << "\n";
							getline(f, buff);
							out << buff << "\n";
							f.close(); out.close();
							f.open(temp); out.open(name_student + "_inform_on_user.txt");
							if (f.is_open() && out.is_open())
							{
								while (getline(f, buff))
								{
									out << buff << "\n";
								}
							}
						}	break;
					case 5: break;
					}
				}
				else { cout << "Не верній пароль\n"; system("pause"); }
			}
		}
	}
	else { cout << "Файл не був відкритий\n"; system("pause"); }
}


void AdminFunc::delete_user(Admin& admin, const string& base_users, const string& temp, exam_file* exam) {
	string name_user;
	ifstream f_user(base_users);
	ofstream out_t(temp);
	if (f_user.is_open() && out_t.is_open())
	{
		ifstream f(base_users);
		string st, buff;
		if (f.is_open())
		{
			getline(f, st);
			getline(f, st);
			while (getline(f, st))
			{
				cout << " - " << st << "\n";	getline(f, st);
			}
			ifstream f_1(base_users);
			cout << "Введіть логін :";
			getline(cin, name_user);
			while (exam->exam_str(name_user))
			{
				cout << "Ви нічого не написали\n";	getline(cin, name_user);
			}
			if (exam->exam_user(name_user) == true)
			{
				while (getline(f_1, buff))
				{
					if (name_user == buff)
					{
						name_user += "_inform_on_user.txt";
						remove(name_user.c_str());
						getline(f_1, buff);
					}
					else
					{
						out_t << buff << "\n";
					}
				}
				f_user.close(); out_t.close();
				ofstream out_user(base_users);
				ifstream f_t(temp);
				if (out_user.is_open() && f_t.is_open())
				{
					while (getline(f_t, buff))
					{
						out_user << buff << "\n";
					}
				}
				out_user.close(); f_t.close();
			}
			else { cout << "Людини з таким логіном немає \n"; system("pause"); }
		}
		else { cout << "Файл не був відкритий\n"; system("pause"); }
		f.close();
	}
	else { cout << "Файл не був відкритий\n"; system("pause"); }
}

void AdminFunc::add_user(Admin& admin, const std::string& base_users, const std::string& temp, exam_file* exam) {
	string new_name, new_pass;
	ofstream out(base_users, ios::app);
	if (out.is_open())
	{
		cout << "Логін :";
		getline(cin, new_name);
		while (exam->exam_str(new_name))
		{
			cout << "Ви нічого не написали\n";	getline(cin, new_name);
		}
		if (exam->exam_base(base_users, new_name) == false)
		{
			cout << "Пароль :";
			getline(cin, new_pass);
			while (exam->exam_str(new_pass))
			{
				cout << "Ви нічого не написали\n";	getline(cin, new_pass);
			}
			new_pass = encryptDecrypt(new_pass);
			if (exam->exam_admin() == true)
			{
				out << new_name << "\n";
				out << new_pass << "\n";
				string new_FIO, new_number, new_address;
				cout << "ПІБ :"; getline(cin, new_FIO);
				cout << "Номер телефону :"; getline(cin, new_number);
				cout << "Адреса :"; getline(cin, new_address);
				User* student = new Student(new_name, new_pass, new_address, new_number, new_FIO);
			}
		}
		else
		{
			cout << "Таке ім'я вже зайняте";
			system("pause");	return;
		}
	}
	else { cout << "Файл не відкрився"; system("pause"); }
}

