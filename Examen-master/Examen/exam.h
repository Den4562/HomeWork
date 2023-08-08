#pragma once
#include <iostream>
#include <fstream>
#include <string>
#include <stdio.h>
#include <regex>
#include "Admin.h"

using namespace std;

struct exam_file
{
    bool exam_base(string base, string str) const;
    bool exam_category(string category, string str) const;
    bool exam_admin() const;
    bool exam_user(string name) const;
    bool exam_str(string str) const;
};

bool exam_file::exam_base(string base, string str) const
{
    ifstream f(base);
    string buff_str;
    if (f.is_open())
    {
        while (getline(f, buff_str))
        {
            if (buff_str == str)
            {
                f.close();
                return true;
            }
        }
    }
    f.close();
    return false;
}

bool exam_file::exam_category(string category, string str) const
{
    size_t s = category.size();
    str.resize(s);
    if (category == str)
    {
        return true;
    }
    else
    {
        return false;
    }
}

bool exam_file::exam_admin() const
{
    string str;
    ifstream f("base_users.txt");
    if (f.is_open())
    {
        while (getline(f, str))
        {
            str.resize(6);
            if ("Admin_" == str)
            {
                return true;
            }
        }
    }
    return false;
}

bool exam_file::exam_user(string name) const
{
    string str;
    ifstream f("base_users.txt");
    if (f.is_open())
    {
        while (getline(f, str))
        {
            if (name == str)
            {
                return true;
            }
        }
    }
    return false;
}

bool exam_file::exam_str(string str) const
{
    if (str.empty() == true || str == " ")
    {
        return true;
    }
    else
    {
        return false;
    }
}