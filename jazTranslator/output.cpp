# include "stdafx.h"
# include <iostream>
# include <string>
# include <vector>
using namespace std;
int f;
int x;
int xx;
int ff;
void work();
vector<int> int_stack;
vector<int*>pointer_stack;
int main() {
cout << " " << endl;
cout << " This code illustrates parameter passing strategy." << endl;
cout << " " << endl;
pointer_stack.push_back(&f);
int_stack.push_back(5);
*(int *)pointer_stack.back() = int_stack.back();
pointer_stack.pop_back();
int_stack.pop_back();
pointer_stack.push_back(&x);
int_stack.push_back(0);
*(int *)pointer_stack.back() = int_stack.back();
pointer_stack.pop_back();
int_stack.pop_back();
goto label_beforeFunc;
label_2000:
cout << " ---------------------------------" << endl;
cout << " after function work:" << endl;
cout << " value of x is:" << endl;
int_stack.push_back(x);
cout << int_stack.back() << endl;
int_stack.pop_back();
cout << " value of f is:" << endl;
int_stack.push_back(f);
cout << int_stack.back() << endl;
int_stack.pop_back();
cout << " " << endl;
return 0;
label_beforeFunc:
cout << " before function work:" << endl;
cout << " value of x is:" << endl;
int_stack.push_back(x);
cout << int_stack.back() << endl;
int_stack.pop_back();
cout << " value of f is:" << endl;
int_stack.push_back(f);
cout << int_stack.back() << endl;
int_stack.pop_back();
cout << " --------------------------------" << endl;
pointer_stack.push_back(&ff);
int_stack.push_back(f);
*(int *)pointer_stack.back() = int_stack.back();
pointer_stack.pop_back();
int_stack.pop_back();
pointer_stack.push_back(&xx);
int_stack.push_back(x);
*(int *)pointer_stack.back() = int_stack.back();
pointer_stack.pop_back();
int_stack.pop_back();
cout << " the call to function work may be seen as" << endl;
cout << " work( f, x );" << endl;
work ();
pointer_stack.push_back(&f);
int_stack.push_back(ff);
*(int *)pointer_stack.back() = int_stack.back();
pointer_stack.pop_back();
int_stack.pop_back();
pointer_stack.push_back(&x);
int_stack.push_back(xx);
*(int *)pointer_stack.back() = int_stack.back();
pointer_stack.pop_back();
int_stack.pop_back();
goto label_2000;
}
void work() {
cout << " and function work may be seen as" << endl;
cout << " work( INOUT int ff, INOUT int xx )" << endl;
pointer_stack.push_back(&xx);
int_stack.push_back(xx);
int_stack.push_back(1);
f = int_stack.back();
int_stack.pop_back();
x = int_stack.back();
int_stack.pop_back();
int_stack.push_back(f + x);
*(int *)pointer_stack.back() = int_stack.back();
pointer_stack.pop_back();
int_stack.pop_back();
pointer_stack.push_back(&ff);
int_stack.push_back(ff);
int_stack.push_back(xx);
xx = int_stack.back();
int_stack.pop_back();
ff = int_stack.back();
int_stack.pop_back();
int_stack.push_back(xx + ff);
*(int *)pointer_stack.back() = int_stack.back();
pointer_stack.pop_back();
int_stack.pop_back();
}
