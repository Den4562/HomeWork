#pragma once
#include <string>

std::string encryptDecrypt(std::string toEncrypt);
void encryptPass(std::string& pass);

string encryptDecrypt(string toEncrypt)
{
	char key[3] = { 'A', 'r', 'N' };
	string output = toEncrypt;
	for (int i = 0; i < toEncrypt.size(); i++)
	{
		output[i] = toEncrypt[i] ^ key[i % (sizeof(key) / sizeof(char))];
	}
	return output;
}

void encryptPass(string& pass)
{
	int ch;
	ch = _getch();
	while (ch != 13)
	{
		pass.push_back(ch);
		cout << '*';
		ch = _getch();
	}
}
