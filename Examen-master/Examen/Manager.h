#pragma once
#include <iostream>�
#include"Menu.h"
#include"User.h"
#include"Admin.h"
#include "exam.h"
#include "Student.h"
#include "Encryption.h"
#include<list>
#include<map>
#include <iostream>
#include <fstream>
#include <string.h>
#include <stdio.h>
#include <regex>
#include <algorithm>
class Testing_system
{
	exam_file exam;
	string file_user = "base_users.txt";
	void login();
	void Registration();
public:
	Testing_system();
	~Testing_system();
	virtual void menu();
};

Testing_system::Testing_system()
{
	ofstream(file_user, ios::app);
	ofstream("base_category.txt", ios::app);
	ofstream("base_tests.txt", ios::app);
}
Testing_system::~Testing_system() {}


string toLower(const string& input) {
	std::string result = input;
	std::transform(result.begin(), result.end(), result.begin(), ::tolower);
	return result;
}


void Testing_system::menu()
{
	do {
		system("cls");
		int choice = Menu::select_vertical(
			{ "��i���",
			"��������������",
			"���������",
			"�����" },
			HorizontalAlignment::Center);
		switch (choice)
		{
		case 0: login();		break;
		case 1: Registration(); break;
		case 2: settingConsole(); break;
		case 3: return;
		}
	} while (true);
}

void Testing_system::Registration()
{
	string new_name, new_pass;
	ofstream out(file_user, ios::app);
	if (!out.is_open())
	{
		cout << "�������: ���� �� ��������\n";
		system("pause");
		return;
	}

	cout << "����: ";
	getline(cin, new_name);
	new_name = toLower(new_name);
	while (exam.exam_str(new_name))
	{
		cout << "�� ����� �� ��������. ������ ���� �� ���: ";
		getline(cin, new_name);
	}

	if (exam.exam_base(file_user, new_name) == false)
	{
		cout << "������: ";
		encryptPass(new_pass);
		while (exam.exam_str(new_pass))
		{
			cout << "\n������ �� ���� ���� ������ ��� ������ ������. ������ ������ �� ���: ";
			encryptPass(new_pass);
		}
		new_pass = encryptDecrypt(new_pass);

		if (exam.exam_admin() == true)
		{
			out << new_name << "\n";
			out << new_pass << "\n";

			string new_FIO, new_number, new_address;
			cout << "\nϲ�: ";
			getline(cin, new_FIO);
			cout << "����� ��������: ";
			getline(cin, new_number);
			cout << "������: ";
			getline(cin, new_address);

			User* student = new Student(new_name, new_pass, new_address, new_number, new_FIO);
			student->menu();
		}
		else
		{
			out << "Admin_" + new_name << "\n";
			out << new_pass << "\n";

			string new_FIO;
			cout << "\nϲ�: ";
			getline(cin, new_FIO);
			while (exam.exam_str(new_FIO))
			{
				cout << "�� ����� �� ��������. ������ ϲ� �� ���: ";
				getline(cin, new_FIO);
			}
			User* admin = new Admin(new_name, new_pass, new_FIO);
			admin->menu();
		}
	}
	else
	{
		cout << "����� ���� ��� ��������\n";
		system("pause");
		return;
	}
}


void Testing_system::login()
{
	string this_name, file_name, pass, file_pass;
	ifstream f(file_user);
	cout << "���� - ";
	getline(cin, this_name);
	this_name = toLower(this_name);
	while (exam.exam_str(this_name))
	{
		cout << "�� ����� �� ��������\n";	getline(cin, this_name);
	}
	if (f.is_open())
	{
		while (getline(f, file_name))
		{
			if ("Admin_" + this_name == file_name)
			{
				cout << "������ - ";	encryptPass(pass);
				while (exam.exam_str(pass))
				{
					cout << "\n������ �� ���� ���� ������ ��� �������� ����� 1 ��������\n";	encryptPass(pass);
				}
				pass = encryptDecrypt(pass);
				getline(f, file_pass);
				if (pass == file_pass)
				{
					User* user = new Admin(this_name, pass);
					user->menu();
					return;
					system("pause");
				}
				else
				{
					cout << "������������ ������";
					system("pause");
					return;
				}
			}
			else if (this_name == file_name)
			{
				cout << "������ - ";	encryptPass(pass);
				while (exam.exam_str(pass))
				{
					cout << "\n������ �� ���� ���� ������ ��� �������� ����� 1 ��������\n";	encryptPass(pass);
				}
				pass = encryptDecrypt(pass);
				getline(f, file_pass);
				if (pass == file_pass)
				{
					User* user = new Student(this_name, pass);
					user->menu();
					return;
					system("pause");
				}
				else
				{
					cout << "������������ ������";
					system("pause");
					return;
				}
			}
		}
		cout << "������������ ���� ";
		system("pause");
	}
}