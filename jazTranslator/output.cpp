# include "stdafx.h"
# include <iostream>
# include <string>
# include <stack>
using namespace std;
int main() {
int f;
int x;
int xx;
int ff;
stack<int> int_stack;
stack<int*> pointer_stack;
cout << " " << endl;
cout << " This code illustrates parameter passing strategy." << endl;
cout << " " << endl;
pointer_stack.push(&f);
int_stack.push(5);
*(int *)pointer_stack.top() = int_stack.top();
pointer_stack.pop();
int_stack.pop();
pointer_stack.push(&x);
int_stack.push(0);
*(int *)pointer_stack.top() = int_stack.top();
pointer_stack.pop();
int_stack.pop();
goto label_beforeFunc;
label_2000:
cout << " ---------------------------------" << endl;
cout << " after function work:" << endl;
cout << " value of x is:" << endl;
int_stack.push(x);
int_stack.pop();
cout << " value of f is:" << endl;
int_stack.push(f);
int_stack.pop();
cout << " " << endl;
return 0;
label_work:
cout << " and function work may be seen as" << endl;
cout << " work( INOUT int ff, INOUT int xx )" << endl;
pointer_stack.push(&xx);
int_stack.push(xx);
int_stack.push(1);
f = int_stack.top();
int_stack.pop();
x = int_stack.top();
int_stack.pop();
int_stack.push(f + x);
*(int *)pointer_stack.top() = int_stack.top();
pointer_stack.pop();
int_stack.pop();
pointer_stack.push(&ff);
int_stack.push(ff);
int_stack.push(xx);
xx = int_stack.top();
int_stack.pop();
ff = int_stack.top();
int_stack.pop();
int_stack.push(xx + ff);
*(int *)pointer_stack.top() = int_stack.top();
pointer_stack.pop();
int_stack.pop();
label_beforeFunc:
cout << " before function work:" << endl;
cout << " value of x is:" << endl;
int_stack.push(x);
int_stack.pop();
cout << " value of f is:" << endl;
int_stack.push(f);
int_stack.pop();
cout << " --------------------------------" << endl;
pointer_stack.push(&ff);
int_stack.push(f);
*(int *)pointer_stack.top() = int_stack.top();
pointer_stack.pop();
int_stack.pop();
pointer_stack.push(&xx);
int_stack.push(x);
*(int *)pointer_stack.top() = int_stack.top();
pointer_stack.pop();
int_stack.pop();
cout << " the call to function work may be seen as" << endl;
cout << " work( f, x );" << endl;
pointer_stack.push(&f);
int_stack.push(ff);
*(int *)pointer_stack.top() = int_stack.top();
pointer_stack.pop();
int_stack.pop();
pointer_stack.push(&x);
int_stack.push(xx);
*(int *)pointer_stack.top() = int_stack.top();
pointer_stack.pop();
int_stack.pop();
goto label_2000;
}
