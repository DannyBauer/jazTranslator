# include "stdafx.h"
# include <iostream>
# include <string>
# include <vector>
using namespace std;
int var;
vector<int> int_stack;
vector<int*>pointer_stack;
int main() {
cout << " " << endl;
cout << "  This code illustrates basic arithmetic" << endl;
cout << "  and logical operations." << endl;
cout << " " << endl;
int_stack.push_back(var);
cout << " Variables are initialized to "zero"" << endl;
cout << " Value of var is:" << endl;
cout << int_stack.back() << endl;
int_stack.pop_back();
cout << " --------------------------------" << endl;
int_stack.push_back(5);
int_stack.push_back(4);
cout << " 5 - 4 = 1" << endl;
int_stack.push_back(int_stack[int_stack.size() - 2] - int_stack[int_stack.size() - 1]);
cout << int_stack.back() << endl;
int_stack.pop_back();
cout << " --------------------------------" << endl;
int_stack.push_back(4);
int_stack.push_back(5);
cout << " 4 - 5 = -1" << endl;
int_stack.push_back(int_stack[int_stack.size() - 2] - int_stack[int_stack.size() - 1]);
cout << int_stack.back() << endl;
int_stack.pop_back();
cout << " --------------------------------" << endl;
int_stack.push_back(5);
int_stack.push_back(4);
cout << " 5 div 4 = 1" << endl;
int_stack.push_back(int_stack[int_stack.size() - 2] % int_stack[int_stack.size() - 1]);
cout << int_stack.back() << endl;
int_stack.pop_back();
cout << " --------------------------------" << endl;
int_stack.push_back(4);
int_stack.push_back(5);
cout << " 4 div 5 = 4" << endl;
int_stack.push_back(int_stack[int_stack.size() - 2] % int_stack[int_stack.size() - 1]);
cout << int_stack.back() << endl;
int_stack.pop_back();
cout << " --------------------------------" << endl;
int_stack.push_back(4);
int_stack.push_back(3);
cout << " 4 / 3 = 1" << endl;
int_stack.push_back(int_stack[int_stack.size() - 2] / int_stack[int_stack.size() - 1]);
cout << int_stack.back() << endl;
int_stack.pop_back();
cout << " --------------------------------" << endl;
int_stack.push_back(3);
int_stack.push_back(4);
cout << " 3 / 4 = 0" << endl;
int_stack.push_back(int_stack[int_stack.size() - 2] / int_stack[int_stack.size() - 1]);
cout << int_stack.back() << endl;
int_stack.pop_back();
cout << " --------------------------------" << endl;
int_stack.push_back(0);
int_stack.push_back(1);
cout << " 0 & 1 = 0" << endl;
int_stack.push_back(int_stack[int_stack.size() - 2] && int_stack[int_stack.size() - 1]);
cout << int_stack.back() << endl;
int_stack.pop_back();
cout << " --------------------------------" << endl;
int_stack.push_back(0);
int_stack.push_back(1);
cout << " 0 | 1 = 1" << endl;
int_stack.push_back(int_stack[int_stack.size() - 2] || int_stack[int_stack.size() - 1]);
cout << int_stack.back() << endl;
int_stack.pop_back();
cout << " --------------------------------" << endl;
int_stack.push_back(0);
cout << " !0 = 1" << endl;
int_stack.push_back(!int_stack[int_stack.size() - 1]);
cout << int_stack.back() << endl;
int_stack.pop_back();
cout << " --------------------------------" << endl;
int_stack.push_back(4);
int_stack.push_back(3);
cout << " 4 <> 3 = 1 " << endl;
if(int_stack[int_stack.size() - 2] == int_stack[int_stack.size() - 1])
int_stack.push_back(0);
else
int_stack.push_back(1);
cout << int_stack.back() << endl;
int_stack.pop_back();
cout << " --------------------------------" << endl;
int_stack.push_back(3);
int_stack.push_back(4);
cout << " 3 <= 4 = 1" << endl;
if(int_stack[int_stack.size() - 2] <= int_stack[int_stack.size() - 1])
int_stack.push_back(1);
else
int_stack.push_back(0);
cout << int_stack.back() << endl;
int_stack.pop_back();
cout << " --------------------------------" << endl;
int_stack.push_back(3);
int_stack.push_back(4);
cout << " 3 >= 4 = 0" << endl;
if(int_stack[int_stack.size() - 2] >= int_stack[int_stack.size() - 1])
int_stack.push_back(1);
else
int_stack.push_back(0);
cout << int_stack.back() << endl;
int_stack.pop_back();
cout << " --------------------------------" << endl;
int_stack.push_back(3);
int_stack.push_back(4);
cout << " 3 < 4 = 1" << endl;
if(int_stack[int_stack.size() - 2] < int_stack[int_stack.size() - 1])
int_stack.push_back(1);
else
int_stack.push_back(0);
cout << int_stack.back() << endl;
int_stack.pop_back();
cout << " --------------------------------" << endl;
int_stack.push_back(3);
int_stack.push_back(4);
cout << " 3 > 4 = 0 " << endl;
if(int_stack[int_stack.size() - 2] > int_stack[int_stack.size() - 1])
int_stack.push_back(1);
else
int_stack.push_back(0);
cout << int_stack.back() << endl;
int_stack.pop_back();
cout << " -------------------------------" << endl;
return 0;
}
