import 'package:flutter/material.dart';
import 'package:todo_tutorial/pages/current_list.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      routes: {
        '/': (context) => TodoList(
              title: 'Todo List',
            ),
      },
    );
  }
}
