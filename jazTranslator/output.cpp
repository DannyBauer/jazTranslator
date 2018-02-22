# include "stdafx.h"
# include <iostream>
# include <string>
# include <stack>
using namespace std;
int main() {
stack<int> int_stack;
stack<int*> pointer_stack;
cout << " show " << endl;
cout << " show This code illustrates parameter passing strategy." << endl;
cout << " show " << endl;
int f;
pointer_stack.push(&f);
int_stack.push(5);
*(int *)pointer_stack.top() = int_stack.top();
pointer_stack.pop();
int_stack.pop();
int x;
pointer_stack.push(&x);
int_stack.push(0);
*(int *)pointer_stack.top() = int_stack.top();
pointer_stack.pop();
int_stack.pop();
goto label_beforeFunc;
label_2000:
cout << " show ---------------------------------" << endl;
cout << " show after function work:" << endl;
cout << " show value of x is:" << endl;
int_stack.push(x);
int_stack.pop();
cout << " show value of f is:" << endl;
int_stack.push(f);
int_stack.pop();
cout << " show " << endl;
label_work:
cout << " show and function work may be seen as" << endl;
cout << " show work( INOUT int ff, INOUT int xx )" << endl;
int xx;
pointer_stack.push(&xx);
int_stack.push(xx);
int_stack.push(1);
*(int *)pointer_stack.top() = int_stack.top();
pointer_stack.pop();
int_stack.pop();
int ff;
pointer_stack.push(&ff);
int_stack.push(ff);
int_stack.push(xx);
*(int *)pointer_stack.top() = int_stack.top();
pointer_stack.pop();
int_stack.pop();
label_beforeFunc:
cout << " show before function work:" << endl;
cout << " show value of x is:" << endl;
int_stack.push(x);
int_stack.pop();
cout << " show value of f is:" << endl;
int_stack.push(f);
int_stack.pop();
cout << " show --------------------------------" << endl;
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
cout << " show the call to function work may be seen as" << endl;
cout << " show work( f, x );" << endl;
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
