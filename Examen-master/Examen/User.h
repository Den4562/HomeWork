#pragma once
#include "Admin.h"
#include <string>


string encryptDecrypt(string toEncrypt);

class User
{
	string name;
	string password;
protected:
	string base_tests = "base_tests.txt";
	string base_users = "base_users.txt";
	string base_category = "base_category.txt";
	string temp = "temp.txt";
public:
	User();
	User(string name, string pass);
	virtual void menu() = 0;
	virtual ~User();
	string getName() const;
	string getPassword() const;
	void   setName(string name);
	void   setPassword(string pass);
	friend class Student;
	friend class Admin;
};

User::User() : name("no name"), password("no pass") {};
User::User(string name, string pass) : name(name) { setPassword(pass); };
User::~User() {}
void   User::setName(string name) { this->name = name; }
string User::getName() const { return name; }
void   User::setPassword(string pass) { password = encryptDecrypt(pass); }
string User::getPassword() const { return encryptDecrypt(password); }
